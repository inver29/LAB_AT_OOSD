using System;
using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    /// <summary>Phân công hướng dẫn viên theo chuyến lẻ (đúng 1 HDV) hoặc theo đoàn (có thể nhiều HDV), không chồng chéo lịch.</summary>
    public class PhanCongService
    {
        public DataTable LayDanhSach()
        {
            return Db.Query(@"SELECT p.MaPC, p.MaHDV, h.HoTen, p.LoaiDoiTuong, ISNULL(p.MaChuyen, p.SoDKDoan) AS DoiTuong, p.NgayBatDau, p.NgayKetThuc, p.ThuLaoTour
                                FROM PhanCongHDV p JOIN HuongDanVien h ON p.MaHDV = h.MaHDV ORDER BY p.NgayBatDau DESC");
        }

        public DataTable LayHDVDangLam() { return Db.Query("SELECT MaHDV, MaHDV + ' - ' + HoTen AS HienThi FROM HuongDanVien WHERE DangLamViec = 1 ORDER BY HoTen"); }

        /// <summary>Đối tượng phân công: chuyến lẻ chưa có HDV, hoặc phiếu đoàn đang hiệu lực.</summary>
        public DataTable LayDoiTuong(string loai)
        {
            if (loai == QuyDinh.Le)
                return Db.Query(@"SELECT c.MaChuyen AS Ma, c.MaChuyen + ' - ' + t.TenTour + ' (' + CONVERT(varchar(10), c.NgayDi, 103) + ')' AS HienThi
                                    FROM ChuyenLe c JOIN Tour t ON c.MaTour = t.MaTour
                                    WHERE NOT EXISTS(SELECT 1 FROM PhanCongHDV p WHERE p.MaChuyen = c.MaChuyen) ORDER BY c.NgayDi");
            return Db.Query(@"SELECT d.SoDKDoan AS Ma, d.SoDKDoan + ' - ' + k.TenCoQuanDaiDien + ' (' + CONVERT(varchar(10), d.NgayDi, 103) + ')' AS HienThi
                                FROM DangKyDoan d JOIN DoanKhach k ON d.MaDoan = k.MaDoan WHERE d.TrangThai = @tt ORDER BY d.NgayDi", Db.P("@tt", QuyDinh.DaDangKy));
        }

        public KetQuaXuLy PhanCong(string maPC, string maHDV, string loai, string maDoiTuong, decimal thuLao)
        {
            if (string.IsNullOrWhiteSpace(maPC) || string.IsNullOrWhiteSpace(maHDV) || string.IsNullOrWhiteSpace(maDoiTuong)
                || (loai != QuyDinh.Le && loai != QuyDinh.Doan) || thuLao < 0)
                return KetQuaXuLy.Fail("Thông tin phân công không hợp lệ.");
            try
            {
                DataTable dt;
                if (loai == QuyDinh.Le)
                {
                    dt = Db.Query("SELECT NgayDi, NgayVe FROM ChuyenLe WHERE MaChuyen = @m", Db.P("@m", maDoiTuong));
                    if (dt.Rows.Count == 0) return KetQuaXuLy.Fail("Không tìm thấy chuyến khách lẻ.");
                    if (Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM PhanCongHDV WHERE MaChuyen = @m", Db.P("@m", maDoiTuong))) > 0)
                        return KetQuaXuLy.Fail("Mỗi chuyến khách lẻ chỉ được phân công một hướng dẫn viên.");
                }
                else
                {
                    dt = Db.Query("SELECT NgayDi, NgayKetThucDuKien FROM DangKyDoan WHERE SoDKDoan = @m AND TrangThai = @tt", Db.P("@m", maDoiTuong), Db.P("@tt", QuyDinh.DaDangKy));
                    if (dt.Rows.Count == 0) return KetQuaXuLy.Fail("Không tìm thấy phiếu đoàn đang hiệu lực.");
                }
                DateTime bd = Convert.ToDateTime(dt.Rows[0][0]).Date, kt = Convert.ToDateTime(dt.Rows[0][1]).Date;
                int trung = Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM PhanCongHDV WHERE MaHDV = @h AND NgayBatDau <= @kt AND NgayKetThuc >= @bd",
                    Db.P("@h", maHDV), Db.P("@bd", bd), Db.P("@kt", kt)));
                if (trung > 0) return KetQuaXuLy.Fail("Hướng dẫn viên bị chồng chéo lịch từ " + bd.ToString("dd/MM/yyyy") + " đến " + kt.ToString("dd/MM/yyyy") + ".");
                Db.Execute(@"INSERT INTO PhanCongHDV(MaPC, MaHDV, LoaiDoiTuong, MaChuyen, SoDKDoan, NgayBatDau, NgayKetThuc, ThuLaoTour)
                             VALUES(@pc, @h, @l, @c, @d, @bd, @kt, @tl)",
                    Db.P("@pc", maPC), Db.P("@h", maHDV), Db.P("@l", loai), Db.P("@c", loai == QuyDinh.Le ? maDoiTuong : null),
                    Db.P("@d", loai == QuyDinh.Doan ? maDoiTuong : null), Db.P("@bd", bd), Db.P("@kt", kt), Db.P("@tl", thuLao));
                return KetQuaXuLy.Ok("Đã phân công hướng dẫn viên.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }
    }
}
