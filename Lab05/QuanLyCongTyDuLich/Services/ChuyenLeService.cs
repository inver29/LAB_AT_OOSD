using System;
using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    /// <summary>Lịch chuyến cho khách lẻ: ngày về tính theo số ngày của tour.</summary>
    public class ChuyenLeService
    {
        public DataTable LayChuyen()
        {
            return Db.Query("SELECT c.MaChuyen, c.MaTour, t.TenTour, c.NgayDi, c.NgayVe, c.DiaDiemDon, c.TrangThai FROM ChuyenLe c JOIN Tour t ON c.MaTour = t.MaTour ORDER BY c.NgayDi DESC");
        }

        public DataTable LayChuyenMo()
        {
            return Db.Query("SELECT c.MaChuyen, c.MaChuyen + ' - ' + t.TenTour + ' (' + CONVERT(varchar(10), c.NgayDi, 103) + ')' AS HienThi, t.DonGiaKhach FROM ChuyenLe c JOIN Tour t ON c.MaTour = t.MaTour WHERE c.TrangThai = @tt ORDER BY c.NgayDi",
                Db.P("@tt", QuyDinh.MoDangKy));
        }

        public KetQuaXuLy ThemChuyen(string ma, string maTour, DateTime ngayDi, string diaDiemDon)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(maTour) || string.IsNullOrWhiteSpace(diaDiemDon)) return KetQuaXuLy.Fail("Thông tin chuyến chưa đầy đủ.");
            try
            {
                object o = Db.Scalar("SELECT SoNgay FROM Tour WHERE MaTour = @t AND DangMoBan = 1", Db.P("@t", maTour));
                if (o == null) return KetQuaXuLy.Fail("Tour không tồn tại hoặc chưa mở bán.");
                DateTime ngayVe = ngayDi.Date.AddDays(Convert.ToInt32(o) - 1);
                Db.Execute("INSERT INTO ChuyenLe(MaChuyen, MaTour, NgayDi, NgayVe, DiaDiemDon, TrangThai) VALUES(@m, @t, @d, @v, @dd, @tt)",
                    Db.P("@m", ma), Db.P("@t", maTour), Db.P("@d", ngayDi.Date), Db.P("@v", ngayVe), Db.P("@dd", diaDiemDon), Db.P("@tt", QuyDinh.MoDangKy));
                return KetQuaXuLy.Ok("Đã tạo chuyến; ngày về " + ngayVe.ToString("dd/MM/yyyy") + ".");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy DongDangKy(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma)) return KetQuaXuLy.Fail("Chưa chọn chuyến.");
            int n = Db.Execute("UPDATE ChuyenLe SET TrangThai = @tt WHERE MaChuyen = @m", Db.P("@tt", QuyDinh.DongDangKy), Db.P("@m", ma));
            return n > 0 ? KetQuaXuLy.Ok("Đã đóng đăng ký chuyến " + ma + ".") : KetQuaXuLy.Fail("Không tìm thấy chuyến.");
        }
    }
}
