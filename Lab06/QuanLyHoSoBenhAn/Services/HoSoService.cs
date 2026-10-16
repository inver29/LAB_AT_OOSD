using System;
using System.Data;
using System.Text.RegularExpressions;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class HoSoService
    {
        public DataTable DanhSach(string keyword = "")
        {
            return Db.Query(@"SELECT h.MaHSBA, b.MaBN, b.HoTen, b.NgaySinh, b.GioiTinh, b.DienThoai,
b.DiaChi, h.NgayCap, h.NgayDongPhi, h.HanPhi, h.PhiThuongNien, b.Anh3x4,
CASE WHEN h.HanPhi > CAST(GETDATE() AS date) THEN N'Còn hạn' ELSE N'Hết hạn' END AS TinhTrangPhi
FROM BenhNhan b JOIN HoSoBenhAn h ON b.MaBN=h.MaBN
WHERE @q=N'' OR b.HoTen LIKE N'%'+@q+N'%' OR b.DienThoai LIKE '%'+@q+'%' OR CONVERT(varchar(12),h.MaHSBA)=@q
ORDER BY h.MaHSBA DESC", Db.P("@q", (keyword ?? "").Trim()));
        }
        public DataTable ChonHoSo()
        { return Db.Query("SELECT h.MaHSBA, CONVERT(varchar(12),h.MaHSBA)+N' — '+b.HoTen AS HienThi FROM HoSoBenhAn h JOIN BenhNhan b ON b.MaBN=h.MaBN ORDER BY h.MaHSBA DESC"); }

        public int DangKy(BenhNhanInput input)
        {
            if (input == null) throw new InvalidOperationException("Thiếu thông tin bệnh nhân.");
            string ten = NghiepVu.Text(input.HoTen, "họ tên", 100);
            string diaChi = NghiepVu.Text(input.DiaChi, "địa chỉ", 200);
            string dt = NghiepVu.Text(input.DienThoai, "điện thoại", 15);
            string anh = NghiepVu.Text(input.Anh3x4, "ảnh 3×4", 255);
            if (!Regex.IsMatch(dt, @"^\+?[0-9]{9,14}$")) throw new InvalidOperationException("Điện thoại cần 9–14 chữ số, có thể bắt đầu bằng +.");
            if (input.NgaySinh.Date > DateTime.Today) throw new InvalidOperationException("Ngày sinh không được ở tương lai.");
            if (input.PhiThuongNien <= 0 || input.PhiThuongNien > 999999999999m || decimal.Truncate(input.PhiThuongNien) != input.PhiThuongNien)
                throw new InvalidOperationException("Phí thường niên phải là số nguyên dương.");
            string gt = NghiepVu.Text(input.GioiTinh, "giới tính", 10, false);
            return Db.Transaction((c, tx) =>
            {
                if (Convert.ToInt32(Db.Scalar(c, tx, @"SELECT COUNT(*) FROM BenhNhan WITH(UPDLOCK,HOLDLOCK)
WHERE HoTen=@ten AND NgaySinh=@sinh AND DienThoai=@dt", Db.P("@ten", ten), Db.P("@sinh", input.NgaySinh.Date), Db.P("@dt", dt))) > 0)
                    throw new InvalidOperationException("Bệnh nhân này đã được đăng ký. Hãy tra cứu hồ sơ trước.");
                int bn = Convert.ToInt32(Db.Scalar(c, tx, @"INSERT BenhNhan(HoTen,NgaySinh,GioiTinh,DiaChi,DienThoai,Anh3x4)
VALUES(@ten,@sinh,@gt,@dc,@dt,@anh); SELECT CAST(SCOPE_IDENTITY() AS int);",
                    Db.P("@ten", ten), Db.P("@sinh", input.NgaySinh.Date), Db.P("@gt", gt), Db.P("@dc", diaChi), Db.P("@dt", dt), Db.P("@anh", anh)));
                return Convert.ToInt32(Db.Scalar(c, tx, @"INSERT HoSoBenhAn(MaBN,NgayCap,NgayDongPhi,HanPhi,PhiThuongNien)
VALUES(@bn,CAST(GETDATE() AS date),CAST(GETDATE() AS date),DATEADD(year,1,CAST(GETDATE() AS date)),@phi);
SELECT CAST(SCOPE_IDENTITY() AS int);", Db.P("@bn", bn), Db.P("@phi", input.PhiThuongNien)));
            });
        }
        public void GiaHan(int maHSBA, decimal phi)
        {
            if (phi <= 0 || phi > 999999999999m || decimal.Truncate(phi) != phi) throw new InvalidOperationException("Phí thường niên không hợp lệ.");
            Db.Transaction((c, tx) =>
            {
                var row = Db.Query(c, tx, "SELECT HanPhi FROM HoSoBenhAn WITH(UPDLOCK,HOLDLOCK) WHERE MaHSBA=@ma", Db.P("@ma", maHSBA));
                if (row.Rows.Count == 0) throw new InvalidOperationException("Hãy chọn hồ sơ cần gia hạn.");
                if (Convert.ToDateTime(row.Rows[0][0]).Date > DateTime.Today) throw new InvalidOperationException("Hồ sơ còn hạn phí, chưa cần gia hạn.");
                Db.Execute(c, tx, @"UPDATE HoSoBenhAn SET NgayDongPhi=CAST(GETDATE() AS date),
HanPhi=DATEADD(year,1,CAST(GETDATE() AS date)),PhiThuongNien=@phi WHERE MaHSBA=@ma", Db.P("@ma", maHSBA), Db.P("@phi", phi));
                return true;
            });
        }
    }
}
