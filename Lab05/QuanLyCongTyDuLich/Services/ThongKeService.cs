using System;
using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    public class ThongKeService
    {
        /// <summary>Lương tháng = lương căn bản + tổng thù lao các tour kết thúc trong tháng.</summary>
        public DataTable LuongHDV(int thang, int nam)
        {
            return Db.Query(@"SELECT h.MaHDV, h.HoTen, h.LuongCoBan,
                                       COUNT(p.MaPC) AS SoTour, ISNULL(SUM(p.ThuLaoTour), 0) AS LuongTheoTour,
                                       h.LuongCoBan + ISNULL(SUM(p.ThuLaoTour), 0) AS TongLuong
                                FROM HuongDanVien h
                                LEFT JOIN PhanCongHDV p ON h.MaHDV = p.MaHDV AND MONTH(p.NgayKetThuc) = @th AND YEAR(p.NgayKetThuc) = @na
                                WHERE h.DangLamViec = 1
                                GROUP BY h.MaHDV, h.HoTen, h.LuongCoBan ORDER BY h.HoTen", Db.P("@th", thang), Db.P("@na", nam));
        }

        public DataTable TongHop(DateTime tu, DateTime den)
        {
            return Db.Query(@"SELECT N'Đăng ký khách lẻ' AS ChiSo, COUNT(*) AS SoLuong, ISNULL(SUM(ThanhTien), 0) AS GiaTri FROM DangKyLe WHERE CAST(NgayDangKy AS date) BETWEEN @tu AND @den
                                UNION ALL SELECT N'Đăng ký đoàn (không tính phiếu hủy)', COUNT(*), ISNULL(SUM(TongTienDuKien), 0) FROM DangKyDoan WHERE CAST(NgayDangKy AS date) BETWEEN @tu AND @den AND TrangThai <> @huy
                                UNION ALL SELECT N'Phiếu đoàn hủy - mất cọc', COUNT(*), ISNULL(SUM(TienCoc), 0) FROM DangKyDoan WHERE CAST(NgayDangKy AS date) BETWEEN @tu AND @den AND TrangThai = @huy
                                UNION ALL SELECT N'Thanh toán sau tour của đoàn', COUNT(*), ISNULL(SUM(SoTien), 0) FROM ThanhToanDoan WHERE CAST(NgayThanhToan AS date) BETWEEN @tu AND @den
                                UNION ALL SELECT N'Khảo sát có phản hồi (giá trị = điểm trung bình)', COUNT(*), CAST(ISNULL(AVG(CAST(DiemDanhGia AS decimal(5,2))), 0) AS decimal(18,2)) FROM KhaoSat WHERE NgayPhanHoi BETWEEN @tu AND @den",
                Db.P("@tu", tu.Date), Db.P("@den", den.Date), Db.P("@huy", QuyDinh.HuyMatCoc));
        }
    }
}
