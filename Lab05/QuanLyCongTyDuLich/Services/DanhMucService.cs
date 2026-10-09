using System;
using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    public class DanhMucService
    {
        public DataTable LayPhuongTien() { return Db.Query("SELECT MaPT, TenPT, GhiChu FROM PhuongTien ORDER BY TenPT"); }
        public DataTable LayDiemBan() { return Db.Query("SELECT MaDiemBan, TenDiemBan, DiaChi, DienThoai FROM DiemBanVe ORDER BY TenDiemBan"); }
        public DataTable LayHDV() { return Db.Query("SELECT MaHDV, HoTen, DienThoai, LuongCoBan, DangLamViec FROM HuongDanVien ORDER BY HoTen"); }
        public DataTable LayDiemThamQuan() { return Db.Query("SELECT MaDiemTQ, TenDiemTQ, DiaDiem, NoiDung, YNghia FROM DiemThamQuan ORDER BY TenDiemTQ"); }

        public KetQuaXuLy ThemPhuongTien(string ma, string ten, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten)) return KetQuaXuLy.Fail("Mã và tên phương tiện không được để trống.");
            return Chay("INSERT INTO PhuongTien(MaPT, TenPT, GhiChu) VALUES(@m, @t, @g)", "Đã thêm phương tiện.",
                Db.P("@m", ma), Db.P("@t", ten), Db.P("@g", ghiChu));
        }

        public KetQuaXuLy ThemDiemBan(string ma, string ten, string diaChi, string dienThoai)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(diaChi)) return KetQuaXuLy.Fail("Thông tin điểm bán vé chưa đầy đủ.");
            return Chay("INSERT INTO DiemBanVe(MaDiemBan, TenDiemBan, DiaChi, DienThoai) VALUES(@m, @t, @d, @p)", "Đã thêm điểm bán vé.",
                Db.P("@m", ma), Db.P("@t", ten), Db.P("@d", diaChi), Db.P("@p", dienThoai));
        }

        public KetQuaXuLy ThemHDV(string ma, string ten, string dienThoai, decimal luongCoBan)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten) || luongCoBan < 0) return KetQuaXuLy.Fail("Thông tin hướng dẫn viên không hợp lệ.");
            return Chay("INSERT INTO HuongDanVien(MaHDV, HoTen, DienThoai, LuongCoBan, DangLamViec) VALUES(@m, @t, @p, @l, 1)", "Đã thêm hướng dẫn viên.",
                Db.P("@m", ma), Db.P("@t", ten), Db.P("@p", dienThoai), Db.P("@l", luongCoBan));
        }

        public KetQuaXuLy ThemDiemThamQuan(string ma, string ten, string diaChi, string noiDung, string yNghia)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(diaChi)) return KetQuaXuLy.Fail("Thông tin điểm tham quan chưa đầy đủ.");
            return Chay("INSERT INTO DiemThamQuan(MaDiemTQ, TenDiemTQ, DiaDiem, NoiDung, YNghia) VALUES(@m, @t, @d, @n, @y)", "Đã thêm điểm tham quan.",
                Db.P("@m", ma), Db.P("@t", ten), Db.P("@d", diaChi), Db.P("@n", noiDung), Db.P("@y", yNghia));
        }

        private static KetQuaXuLy Chay(string sql, string thongBao, params System.Data.SqlClient.SqlParameter[] ps)
        {
            try { Db.Execute(sql, ps); return KetQuaXuLy.Ok(thongBao); }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }
    }
}
