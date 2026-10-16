using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class ToaThuocService
    {
        public DataTable ChiTiet(int phieu)
        {
            return Db.Query(@"SELECT ct.STT,ct.MaThuoc,t.TenThuoc,ct.SoLuong,ct.DonGia,
ct.SoLuong*ct.DonGia AS ThanhTien,ct.LieuDung,ct.CachDung
FROM ToaThuoc toa JOIN ChiTietToaThuoc ct ON ct.MaToa=toa.MaToa JOIN Thuoc t ON t.MaThuoc=ct.MaThuoc
WHERE toa.MaPhieuKham=@ma ORDER BY ct.STT", Db.P("@ma", phieu));
        }
        public DataTable ThongTin(int phieu)
        { return Db.Query("SELECT MaToa,NgayKe,LoiDan FROM ToaThuoc WHERE MaPhieuKham=@ma", Db.P("@ma", phieu)); }

        public int LapToa(int phieu, string loiDan, IList<DongThuoc> lines)
        {
            string dan = NghiepVu.Text(loiDan, "lời dặn", 500, false);
            if (lines == null || lines.Count == 0 || lines.Count > 100) throw new InvalidOperationException("Toa thuốc cần từ 1 đến 100 dòng thuốc.");
            if (lines.Any(x => x == null)) throw new InvalidOperationException("Dòng thuốc không hợp lệ.");
            if (lines.Select(x => x.MaThuoc).Distinct().Count() != lines.Count) throw new InvalidOperationException("Thuốc bị trùng. Hãy gộp thành một dòng.");
            foreach (var line in lines)
            {
                if (line.SoLuong <= 0 || line.SoLuong > 100000) throw new InvalidOperationException("Số lượng thuốc phải từ 1 đến 100.000.");
                NghiepVu.Text(line.LieuDung, "liều dùng", 100);
                NghiepVu.Text(line.CachDung, "cách dùng", 200);
            }
            return Db.Transaction((c, tx) =>
            {
                string status = NghiepVu.KhoaPhieu(c, tx, phieu);
                if (status != "Hoàn tất khám") throw new InvalidOperationException("Chỉ kê toa sau khi hoàn tất khám và trước thanh toán.");
                NghiepVu.KiemTraXetNghiem(c, tx, phieu);
                if (Convert.ToInt32(Db.Scalar(c, tx, "SELECT COUNT(*) FROM ToaThuoc WHERE MaPhieuKham=@ma", Db.P("@ma", phieu))) > 0)
                    throw new InvalidOperationException("Phiếu này đã có toa thuốc. Không lập thêm toa thứ hai.");
                int toa = Convert.ToInt32(Db.Scalar(c, tx, "INSERT ToaThuoc(MaPhieuKham,NgayKe,LoiDan) VALUES(@ma,GETDATE(),@dan); SELECT CAST(SCOPE_IDENTITY() AS int);", Db.P("@ma", phieu), Db.P("@dan", dan)));
                for (int i = 0; i < lines.Count; i++)
                {
                    var line = lines[i];
                    // Đơn giá được đọc tại lúc lưu, không tin giá gửi từ giao diện.
                    object gia = Db.Scalar(c, tx, "SELECT DonGia FROM Thuoc WHERE MaThuoc=@ma", Db.P("@ma", line.MaThuoc));
                    if (gia == null) throw new InvalidOperationException("Thuốc không còn tồn tại. Hãy làm mới danh mục.");
                    Db.Execute(c, tx, @"INSERT ChiTietToaThuoc(MaToa,STT,MaThuoc,SoLuong,DonGia,LieuDung,CachDung)
VALUES(@toa,@stt,@thuoc,@sl,@gia,@lieu,@cach)", Db.P("@toa", toa), Db.P("@stt", (short)(i + 1)),
                        Db.P("@thuoc", line.MaThuoc), Db.P("@sl", line.SoLuong), Db.P("@gia", Convert.ToDecimal(gia)),
                        Db.P("@lieu", line.LieuDung.Trim()), Db.P("@cach", line.CachDung.Trim()));
                }
                return toa;
            });
        }
    }
}
