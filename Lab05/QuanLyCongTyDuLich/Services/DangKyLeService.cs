using System;
using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    /// <summary>Khách lẻ (dưới 12 người) đăng ký theo chuyến tại điểm bán vé, mua và thanh toán vé ngay.</summary>
    public class DangKyLeService
    {
        public DataTable LayDanhSach()
        {
            return Db.Query(@"SELECT d.SoDKLe, d.MaChuyen, t.TenTour, c.NgayDi, c.NgayVe, b.TenDiemBan, d.TenNguoiDangKy, d.DienThoai, d.SoNguoi, d.ThanhTien, d.TrangThai
                                FROM DangKyLe d JOIN ChuyenLe c ON d.MaChuyen = c.MaChuyen JOIN Tour t ON c.MaTour = t.MaTour
                                JOIN DiemBanVe b ON d.MaDiemBan = b.MaDiemBan ORDER BY d.NgayDangKy DESC");
        }

        public KetQuaXuLy DangKy(string soDK, string maChuyen, string maDiemBan, string ten, string dienThoai, int soNguoi)
        {
            if (string.IsNullOrWhiteSpace(soDK) || string.IsNullOrWhiteSpace(maChuyen) || string.IsNullOrWhiteSpace(maDiemBan)
                || string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(dienThoai))
                return KetQuaXuLy.Fail("Thông tin đăng ký khách lẻ chưa đầy đủ.");
            if (soNguoi <= 0) return KetQuaXuLy.Fail("Số người phải lớn hơn 0.");
            if (soNguoi >= QuyDinh.MocKhachDoan)
                return KetQuaXuLy.Fail("Khách lẻ phải dưới 12 người. Từ 13 người trở lên đăng ký theo đoàn; đúng 12 người đề không quy định.");
            try
            {
                object o = Db.Scalar("SELECT t.DonGiaKhach FROM ChuyenLe c JOIN Tour t ON c.MaTour = t.MaTour WHERE c.MaChuyen = @c AND c.TrangThai = @tt",
                    Db.P("@c", maChuyen), Db.P("@tt", QuyDinh.MoDangKy));
                if (o == null) return KetQuaXuLy.Fail("Chuyến không tồn tại hoặc đã đóng đăng ký.");
                decimal thanhTien = Convert.ToDecimal(o) * soNguoi;
                Db.Execute(@"INSERT INTO DangKyLe(SoDKLe, MaChuyen, MaDiemBan, NgayDangKy, TenNguoiDangKy, DienThoai, SoNguoi, ThanhTien, DaThanhToan, TrangThai)
                             VALUES(@s, @c, @b, SYSDATETIME(), @t, @p, @n, @tt, 1, @st)",
                    Db.P("@s", soDK), Db.P("@c", maChuyen), Db.P("@b", maDiemBan), Db.P("@t", ten), Db.P("@p", dienThoai),
                    Db.P("@n", soNguoi), Db.P("@tt", thanhTien), Db.P("@st", QuyDinh.DaDangKy));
                return KetQuaXuLy.Ok("Đã đăng ký và thanh toán vé. Thành tiền: " + thanhTien.ToString("N0") + " đ.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }
    }
}
