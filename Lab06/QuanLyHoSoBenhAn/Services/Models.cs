using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyHoSoBenhAn.Data;

namespace QuanLyHoSoBenhAn.Services
{
    public class BenhNhanInput
    {
        public string HoTen, GioiTinh, DiaChi, DienThoai, Anh3x4;
        public DateTime NgaySinh;
        public decimal PhiThuongNien;
    }
    public class KhamInput
    {
        public string TrieuChung, HuyetAp, ChanDoan;
        public decimal? ThanNhiet;
        public short? NhipTim;
        public bool HoanTat;
    }
    public class DongThuoc
    {
        public int MaThuoc, SoLuong;
        public string LieuDung, CachDung;
    }
    public static class NghiepVu
    {
        public static string Text(string value, string label, int max, bool required = true)
        {
            value = (value ?? "").Trim();
            if (required && value.Length == 0) throw new InvalidOperationException("Vui lòng nhập " + label + ".");
            if (value.Length > max) throw new InvalidOperationException(label + " tối đa " + max + " ký tự.");
            return value;
        }
        public static string KhoaPhieu(SqlConnection c, SqlTransaction tx, int ma)
        {
            object value = Db.Scalar(c, tx, "SELECT TrangThai FROM PhieuKhamBenh WITH(UPDLOCK,HOLDLOCK) WHERE MaPhieuKham=@ma", Db.P("@ma", ma));
            if (value == null) throw new InvalidOperationException("Phiếu khám không tồn tại. Vui lòng làm mới.");
            return Convert.ToString(value);
        }
        public static void KiemTraBacSi(SqlConnection c, SqlTransaction tx, int ma)
        {
            if (Convert.ToInt32(Db.Scalar(c, tx, "SELECT COUNT(*) FROM NhanVien WHERE MaNV=@ma AND VaiTro='BacSi'", Db.P("@ma", ma))) != 1)
                throw new InvalidOperationException("Nhân viên được chọn không phải bác sĩ.");
        }
        public static void KiemTraXetNghiem(SqlConnection c, SqlTransaction tx, int ma)
        {
            if (Convert.ToInt32(Db.Scalar(c, tx, "SELECT COUNT(*) FROM ChiTietXetNghiem WHERE MaPhieuKham=@ma AND (KetQua IS NULL OR LTRIM(RTRIM(KetQua))='' OR NgayKetQua IS NULL)", Db.P("@ma", ma))) > 0)
                throw new InvalidOperationException("Phiếu còn xét nghiệm chưa có kết quả, chưa thể hoàn tất.");
        }
    }
    public class DanhMucService
    {
        public DataTable BacSi() { return Db.Query("SELECT MaNV, HoTen FROM NhanVien WHERE VaiTro='BacSi' ORDER BY HoTen"); }
        public DataTable Khoa() { return Db.Query("SELECT MaKhoa, TenKhoa FROM KhoaDieuTri ORDER BY TenKhoa"); }
        public DataTable Thuoc() { return Db.Query("SELECT MaThuoc, TenThuoc + N' — ' + HamLuong + N' (' + DonViTinh + N')' AS HienThi, DonGia FROM Thuoc ORDER BY TenThuoc"); }
        public DataTable TongQuan()
        {
            return Db.Query(@"SELECT (SELECT COUNT(*) FROM HoSoBenhAn) HoSo,
(SELECT COUNT(*) FROM PhieuKhamBenh WHERE TrangThai<>N'Đã thanh toán') DangXuLy,
(SELECT COUNT(*) FROM PhieuKhamBenh WHERE TrangThai=N'Hoàn tất khám') ChoThu,
(SELECT COUNT(*) FROM PhieuKhamBenh WHERE TrangThai=N'Đã thanh toán') DaThu");
        }
    }
}
