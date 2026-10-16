using System;
using System.Data;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class KhamBenhService
    {
        public DataTable DanhSach(string keyword = "")
        {
            return Db.Query(@"SELECT p.MaPhieuKham,p.MaHSBA,b.HoTen,p.MaBacSi,n.HoTen AS BacSi,p.MaKhoa,k.TenKhoa,
p.NgayKham,p.LoaiKham,p.TrangThai,p.TienKham,p.TrieuChung,p.ThanNhiet,p.HuyetAp,p.NhipTim,p.ChanDoan,
CONVERT(varchar(12),p.MaPhieuKham)+N' — '+b.HoTen+N' | '+p.TrangThai AS HienThi
FROM PhieuKhamBenh p JOIN HoSoBenhAn h ON h.MaHSBA=p.MaHSBA JOIN BenhNhan b ON b.MaBN=h.MaBN
JOIN NhanVien n ON n.MaNV=p.MaBacSi JOIN KhoaDieuTri k ON k.MaKhoa=p.MaKhoa
WHERE @q=N'' OR b.HoTen LIKE N'%'+@q+N'%' OR CONVERT(varchar(12),p.MaPhieuKham)=@q
ORDER BY p.MaPhieuKham DESC", Db.P("@q", (keyword ?? "").Trim()));
        }
        public int TaoPhieu(int hoSo, int bacSi, int khoa, decimal tienKham)
        {
            if (tienKham < 0 || tienKham > 999999999999m || decimal.Truncate(tienKham) != tienKham)
                throw new InvalidOperationException("Tiền khám phải là số nguyên không âm.");
            return Db.Transaction((c, tx) =>
            {
                if (Convert.ToInt32(Db.Scalar(c, tx, "SELECT COUNT(*) FROM HoSoBenhAn WHERE MaHSBA=@ma", Db.P("@ma", hoSo))) != 1)
                    throw new InvalidOperationException("Hãy chọn hồ sơ bệnh án hợp lệ.");
                NghiepVu.KiemTraBacSi(c, tx, bacSi);
                if (Convert.ToInt32(Db.Scalar(c, tx, "SELECT COUNT(*) FROM KhoaDieuTri WHERE MaKhoa=@ma", Db.P("@ma", khoa))) != 1)
                    throw new InvalidOperationException("Hãy chọn khoa điều trị hợp lệ.");
                return Convert.ToInt32(Db.Scalar(c, tx, @"INSERT PhieuKhamBenh(MaHSBA,MaBacSi,MaKhoa,NgayKham,LoaiKham,TrangThai,TienKham)
VALUES(@hs,@bs,@k,GETDATE(),N'Điều trị',N'Chờ khám',@tien); SELECT CAST(SCOPE_IDENTITY() AS int);",
                    Db.P("@hs", hoSo), Db.P("@bs", bacSi), Db.P("@k", khoa), Db.P("@tien", tienKham)));
            });
        }
        public void LuuKham(int ma, KhamInput input)
        {
            if (input == null) throw new InvalidOperationException("Thiếu thông tin khám.");
            string trieuChung = NghiepVu.Text(input.TrieuChung, "triệu chứng", 500);
            string chanDoan = NghiepVu.Text(input.ChanDoan, "chẩn đoán", 500, input.HoanTat);
            string huyetAp = NghiepVu.Text(input.HuyetAp, "huyết áp", 15, false);
            if (input.ThanNhiet.HasValue && (input.ThanNhiet < 30 || input.ThanNhiet > 45)) throw new InvalidOperationException("Thân nhiệt cần trong khoảng 30–45°C.");
            if (input.NhipTim.HasValue && input.NhipTim <= 0) throw new InvalidOperationException("Nhịp tim phải lớn hơn 0.");
            Db.Transaction((c, tx) =>
            {
                string status = NghiepVu.KhoaPhieu(c, tx, ma);
                if (status == "Đã thanh toán") throw new InvalidOperationException("Phiếu đã thanh toán, không thể sửa thông tin khám.");
                if (input.HoanTat) NghiepVu.KiemTraXetNghiem(c, tx, ma);
                Db.Execute(c, tx, @"UPDATE PhieuKhamBenh SET TrieuChung=@tc,ThanNhiet=@tn,HuyetAp=@ha,NhipTim=@nt,
ChanDoan=@cd,TrangThai=@tt WHERE MaPhieuKham=@ma", Db.P("@ma", ma), Db.P("@tc", trieuChung),
                    Db.P("@tn", input.ThanNhiet.HasValue ? (object)input.ThanNhiet.Value : null), Db.P("@ha", huyetAp),
                    Db.P("@nt", input.NhipTim.HasValue ? (object)input.NhipTim.Value : null), Db.P("@cd", chanDoan),
                    Db.P("@tt", input.HoanTat ? "Hoàn tất khám" : "Đang khám"));
                return true;
            });
        }
    }
}
