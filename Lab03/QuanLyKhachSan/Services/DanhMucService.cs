using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Services
{
    public class DanhMucService
    {
        public DataTable LayKhuVuc() => Db.Query("SELECT * FROM KhuVuc ORDER BY MaKhuVuc");
        public DataTable LayNhanVien() => Db.Query("SELECT * FROM NhanVien ORDER BY MaNV");
        public DataTable LayLoaiTienNghi() => Db.Query("SELECT * FROM LoaiTienNghi ORDER BY MaLoaiTN");
        public DataTable LayDichVu() => Db.Query("SELECT * FROM DichVu ORDER BY MaDV");

        public DataTable LayQuyDinhDenBu()
        {
            string sql = "SELECT q.*, l.TenLoaiTN FROM QuyDinhDenBu q JOIN LoaiTienNghi l ON q.MaLoaiTN=l.MaLoaiTN ORDER BY q.MaQuyDinh";
            return Db.Query(sql);
        }

        public KetQuaXuLy ThemKhu(string ma, string ten)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten))
                return KetQuaXuLy.Fail("Mã khu vực và tên khu vực không được để trống.");

            try
            {
                Db.Execute("INSERT INTO KhuVuc(MaKhuVuc, TenKhuVuc) VALUES(@m, @t)",
                    new SqlParameter("@m", ma),
                    new SqlParameter("@t", ten));
                return KetQuaXuLy.Ok("Đã thêm khu vực thành công.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy ThemNhanVien(string ma, string ten, string vaiTro, string sdt)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(vaiTro))
                return KetQuaXuLy.Fail("Thông tin bắt buộc của nhân viên chưa đầy đủ.");

            try
            {
                Db.Execute("INSERT INTO NhanVien(MaNV, HoTen, VaiTro, SoDienThoai) VALUES(@m, @t, @v, @s)",
                    new SqlParameter("@m", ma),
                    new SqlParameter("@t", ten),
                    new SqlParameter("@v", vaiTro),
                    new SqlParameter("@s", string.IsNullOrWhiteSpace(sdt) ? (object)DBNull.Value : sdt));
                return KetQuaXuLy.Ok("Đã thêm nhân viên thành công.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy ThemLoaiTN(string ma, string ten)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten))
                return KetQuaXuLy.Fail("Thông tin loại tiện nghi không được để trống.");

            try
            {
                Db.Execute("INSERT INTO LoaiTienNghi(MaLoaiTN, TenLoaiTN) VALUES(@m, @t)",
                    new SqlParameter("@m", ma),
                    new SqlParameter("@t", ten));
                return KetQuaXuLy.Ok("Đã thêm loại tiện nghi.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy ThemDichVu(string ma, string ten, string dvt, decimal gia)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(dvt) || gia < 0)
                return KetQuaXuLy.Fail("Thông tin dịch vụ không hợp lệ hoặc đơn giá âm.");

            try
            {
                Db.Execute("INSERT INTO DichVu(MaDV, TenDV, DonViTinh, DonGia) VALUES(@m, @t, @d, @g)",
                    new SqlParameter("@m", ma),
                    new SqlParameter("@t", ten),
                    new SqlParameter("@d", dvt),
                    new SqlParameter("@g", gia));
                return KetQuaXuLy.Ok("Đã thêm dịch vụ mới.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy ThemQuyDinh(string ma, string loai, string muc, decimal tien)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(loai) || string.IsNullOrWhiteSpace(muc) || tien < 0)
                return KetQuaXuLy.Fail("Quy định đền bù không hợp lệ.");

            try
            {
                Db.Execute("INSERT INTO QuyDinhDenBu(MaQuyDinh, MaLoaiTN, MucDoThietHai, MucDenBu) VALUES(@m, @l, @u, @t)",
                    new SqlParameter("@m", ma),
                    new SqlParameter("@l", loai),
                    new SqlParameter("@u", muc),
                    new SqlParameter("@t", tien));
                return KetQuaXuLy.Ok("Đã thêm quy định đền bù.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }
    }
}