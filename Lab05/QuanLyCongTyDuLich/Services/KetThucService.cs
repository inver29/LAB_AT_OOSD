using System;
using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    /// <summary>Kết thúc tour: thanh toán kinh phí đoàn sau chuyến và khảo sát khách hàng.</summary>
    public class KetThucService
    {
        public DataTable DoanCanThanhToan()
        {
            return Db.Query(@"SELECT d.SoDKDoan, k.TenCoQuanDaiDien, t.TenTour, d.NgayKetThucDuKien, d.TongTienDuKien, d.TienCoc,
                                       ISNULL(SUM(tt.SoTien), 0) AS DaTraSauTour, d.TongTienDuKien - d.TienCoc - ISNULL(SUM(tt.SoTien), 0) AS ConLai, d.TrangThai
                                FROM DangKyDoan d JOIN DoanKhach k ON d.MaDoan = k.MaDoan JOIN Tour t ON d.MaTour = t.MaTour
                                LEFT JOIN ThanhToanDoan tt ON d.SoDKDoan = tt.SoDKDoan
                                WHERE d.TrangThai <> @huy
                                GROUP BY d.SoDKDoan, k.TenCoQuanDaiDien, t.TenTour, d.NgayKetThucDuKien, d.TongTienDuKien, d.TienCoc, d.TrangThai
                                ORDER BY d.NgayKetThucDuKien DESC", Db.P("@huy", QuyDinh.HuyMatCoc));
        }

        public KetQuaXuLy ThanhToanDoan(string soTT, string soDK, DateTime ngay, decimal soTien, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(soTT) || string.IsNullOrWhiteSpace(soDK) || soTien <= 0) return KetQuaXuLy.Fail("Thông tin thanh toán không hợp lệ.");
            try
            {
                var dt = Db.Query("SELECT NgayKetThucDuKien, TongTienDuKien, TienCoc, TrangThai FROM DangKyDoan WHERE SoDKDoan = @s", Db.P("@s", soDK));
                if (dt.Rows.Count == 0) return KetQuaXuLy.Fail("Không tìm thấy phiếu đăng ký đoàn.");
                if (Convert.ToString(dt.Rows[0]["TrangThai"]) != QuyDinh.DaDangKy) return KetQuaXuLy.Fail("Phiếu đã hủy hoặc đã thanh toán đủ.");
                if (ngay.Date <= Convert.ToDateTime(dt.Rows[0]["NgayKetThucDuKien"]).Date) return KetQuaXuLy.Fail("Kinh phí đoàn chỉ thanh toán sau khi kết thúc chuyến tham quan.");
                decimal tong = Convert.ToDecimal(dt.Rows[0]["TongTienDuKien"]), coc = Convert.ToDecimal(dt.Rows[0]["TienCoc"]);
                decimal da = Convert.ToDecimal(Db.Scalar("SELECT ISNULL(SUM(SoTien), 0) FROM ThanhToanDoan WHERE SoDKDoan = @s", Db.P("@s", soDK)));
                decimal conLai = tong - coc - da;
                if (soTien > conLai) return KetQuaXuLy.Fail("Số tiền vượt số còn phải thanh toán (" + conLai.ToString("N0") + " đ).");
                Db.Execute("INSERT INTO ThanhToanDoan(SoTT, SoDKDoan, NgayThanhToan, SoTien, GhiChu) VALUES(@m, @s, @n, @t, @g)",
                    Db.P("@m", soTT), Db.P("@s", soDK), Db.P("@n", ngay), Db.P("@t", soTien), Db.P("@g", ghiChu));
                if (soTien == conLai)
                {
                    Db.Execute("UPDATE DangKyDoan SET TrangThai = @tt WHERE SoDKDoan = @s", Db.P("@tt", QuyDinh.HoanTatThanhToan), Db.P("@s", soDK));
                    return KetQuaXuLy.Ok("Đã ghi nhận thanh toán; đoàn đã hoàn tất thanh toán.");
                }
                return KetQuaXuLy.Ok("Đã ghi nhận thanh toán; còn lại " + (conLai - soTien).ToString("N0") + " đ.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        /// <summary>Các đăng ký đã kết thúc tour và chưa được gửi khảo sát.</summary>
        public DataTable LayDangKyChoKhaoSat(string loai)
        {
            if (loai == QuyDinh.Le)
                return Db.Query(@"SELECT d.SoDKLe AS Ma, d.SoDKLe + ' - ' + d.TenNguoiDangKy AS HienThi FROM DangKyLe d JOIN ChuyenLe c ON d.MaChuyen = c.MaChuyen
                                    WHERE c.NgayVe < CAST(GETDATE() AS date) AND NOT EXISTS(SELECT 1 FROM KhaoSat k WHERE k.SoDKLe = d.SoDKLe) ORDER BY d.SoDKLe");
            return Db.Query(@"SELECT d.SoDKDoan AS Ma, d.SoDKDoan + ' - ' + k.TenCoQuanDaiDien AS HienThi FROM DangKyDoan d JOIN DoanKhach k ON d.MaDoan = k.MaDoan
                                WHERE d.TrangThai <> @huy AND d.NgayKetThucDuKien < CAST(GETDATE() AS date)
                                  AND NOT EXISTS(SELECT 1 FROM KhaoSat s WHERE s.SoDKDoan = d.SoDKDoan) ORDER BY d.SoDKDoan", Db.P("@huy", QuyDinh.HuyMatCoc));
        }

        public DataTable LayKhaoSat()
        {
            return Db.Query("SELECT MaKhaoSat, LoaiKhach, ISNULL(SoDKLe, SoDKDoan) AS SoDangKy, NgayGui, NgayPhanHoi, DiemDanhGia, GopY FROM KhaoSat ORDER BY NgayGui DESC");
        }

        public KetQuaXuLy GuiKhaoSat(string maKS, string loai, string soDK, DateTime ngayGui)
        {
            if (string.IsNullOrWhiteSpace(maKS) || string.IsNullOrWhiteSpace(soDK) || (loai != QuyDinh.Le && loai != QuyDinh.Doan)) return KetQuaXuLy.Fail("Thông tin khảo sát chưa đầy đủ.");
            try
            {
                object o = loai == QuyDinh.Le
                    ? Db.Scalar("SELECT c.NgayVe FROM DangKyLe d JOIN ChuyenLe c ON d.MaChuyen = c.MaChuyen WHERE d.SoDKLe = @s", Db.P("@s", soDK))
                    : Db.Scalar("SELECT NgayKetThucDuKien FROM DangKyDoan WHERE SoDKDoan = @s AND TrangThai <> @huy", Db.P("@s", soDK), Db.P("@huy", QuyDinh.HuyMatCoc));
                if (o == null) return KetQuaXuLy.Fail("Không tìm thấy đăng ký hợp lệ.");
                if (ngayGui.Date <= Convert.ToDateTime(o).Date) return KetQuaXuLy.Fail("Phiếu khảo sát chỉ gửi sau khi kết thúc tour.");
                Db.Execute("INSERT INTO KhaoSat(MaKhaoSat, LoaiKhach, SoDKLe, SoDKDoan, NgayGui) VALUES(@m, @l, @le, @doan, @g)",
                    Db.P("@m", maKS), Db.P("@l", loai), Db.P("@le", loai == QuyDinh.Le ? soDK : null), Db.P("@doan", loai == QuyDinh.Doan ? soDK : null), Db.P("@g", ngayGui.Date));
                return KetQuaXuLy.Ok("Đã gửi phiếu khảo sát " + maKS + ".");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy GhiPhanHoi(string maKS, DateTime ngayPhanHoi, int diem, string gopY)
        {
            if (string.IsNullOrWhiteSpace(maKS)) return KetQuaXuLy.Fail("Chưa chọn phiếu khảo sát.");
            if (diem < 1 || diem > 5) return KetQuaXuLy.Fail("Điểm đánh giá phải từ 1 đến 5.");
            try
            {
                object g = Db.Scalar("SELECT NgayGui FROM KhaoSat WHERE MaKhaoSat = @m", Db.P("@m", maKS));
                if (g == null) return KetQuaXuLy.Fail("Không tìm thấy phiếu khảo sát.");
                if (ngayPhanHoi.Date < Convert.ToDateTime(g).Date) return KetQuaXuLy.Fail("Ngày phản hồi không được trước ngày gửi.");
                Db.Execute("UPDATE KhaoSat SET NgayPhanHoi = @n, DiemDanhGia = @d, GopY = @y WHERE MaKhaoSat = @m",
                    Db.P("@n", ngayPhanHoi.Date), Db.P("@d", diem), Db.P("@y", gopY), Db.P("@m", maKS));
                return KetQuaXuLy.Ok("Đã ghi nhận góp ý của khách hàng.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }
    }
}
