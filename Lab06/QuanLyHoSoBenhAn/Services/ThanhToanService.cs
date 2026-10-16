using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class ThanhToanService
    {
        private const string SqlChiPhi = @"SELECT N'Khám bệnh' AS Nhom, N'Phí khám' AS NoiDung, 1 AS SoLuong, TienKham AS DonGia, TienKham AS ThanhTien
FROM PhieuKhamBenh WHERE MaPhieuKham=@ma
UNION ALL SELECT N'Xét nghiệm',l.TenLoaiXN,1,x.DonGia,x.DonGia FROM ChiTietXetNghiem x JOIN LoaiXetNghiem l ON l.MaLoaiXN=x.MaLoaiXN WHERE x.MaPhieuKham=@ma
UNION ALL SELECT N'Thuốc',t.TenThuoc,ct.SoLuong,ct.DonGia,ct.SoLuong*ct.DonGia FROM ToaThuoc toa
JOIN ChiTietToaThuoc ct ON ct.MaToa=toa.MaToa JOIN Thuoc t ON t.MaThuoc=ct.MaThuoc WHERE toa.MaPhieuKham=@ma";
        public DataTable ChiPhi(int ma) { return Db.Query(SqlChiPhi, Db.P("@ma", ma)); }
        public DataTable DanhSach(string keyword = "")
        {
            return Db.Query(@"SELECT p.MaPhieuKham,h.MaHSBA,b.HoTen,p.NgayKham,p.TrangThai,p.TongVienPhi,p.NgayThanhToan
FROM PhieuKhamBenh p JOIN HoSoBenhAn h ON h.MaHSBA=p.MaHSBA JOIN BenhNhan b ON b.MaBN=h.MaBN
WHERE p.TrangThai IN(N'Hoàn tất khám',N'Đã thanh toán') AND (@q=N'' OR b.HoTen LIKE N'%'+@q+N'%' OR CONVERT(varchar(12),p.MaPhieuKham)=@q)
ORDER BY CASE WHEN p.TrangThai=N'Hoàn tất khám' THEN 0 ELSE 1 END,p.MaPhieuKham DESC", Db.P("@q", (keyword ?? "").Trim()));
        }
        public decimal ThanhToan(int ma, decimal tongDaXem)
        {
            return Db.Transaction((c, tx) =>
            {
                if (NghiepVu.KhoaPhieu(c, tx, ma) != "Hoàn tất khám")
                    throw new InvalidOperationException("Phiếu chưa hoàn tất khám hoặc đã thanh toán. Hãy làm mới danh sách.");
                NghiepVu.KiemTraXetNghiem(c, tx, ma);
                DataTable table = Db.Query(c, tx, SqlChiPhi, Db.P("@ma", ma));
                decimal tong = 0; foreach (DataRow row in table.Rows) tong += Convert.ToDecimal(row["ThanhTien"]);
                if (tong != tongDaXem) throw new InvalidOperationException("Chi phí vừa thay đổi. Hãy làm mới và kiểm tra lại trước khi thu.");
                if (tong > 999999999999m) throw new InvalidOperationException("Tổng tiền vượt giới hạn lưu trữ.");
                Db.Execute(c, tx, "UPDATE PhieuKhamBenh SET TongVienPhi=@tong,NgayThanhToan=GETDATE(),TrangThai=N'Đã thanh toán' WHERE MaPhieuKham=@ma", Db.P("@tong", tong), Db.P("@ma", ma));
                return tong;
            });
        }
    }
}
