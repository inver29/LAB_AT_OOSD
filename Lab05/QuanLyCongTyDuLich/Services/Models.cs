using System;

namespace QuanLyCongTyDuLich.Services
{
    /// <summary>Kết quả của mọi thao tác nghiệp vụ; Form chỉ đọc ThanhCong và ThongBao.</summary>
    public class KetQuaXuLy
    {
        public bool ThanhCong { get; private set; }
        public string ThongBao { get; private set; }
        public static KetQuaXuLy Ok(string thongBao) { return new KetQuaXuLy { ThanhCong = true, ThongBao = thongBao }; }
        public static KetQuaXuLy Fail(string thongBao) { return new KetQuaXuLy { ThanhCong = false, ThongBao = thongBao }; }
    }

    /// <summary>Một người cùng đi trong danh sách đoàn (bắt buộc khi đoàn mua bảo hiểm).</summary>
    public class ThanhVienDoanItem
    {
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string SoGiayTo { get; set; }
    }

    /// <summary>Hằng số nghiệp vụ lấy từ đề.</summary>
    public static class QuyDinh
    {
        public const int MocKhachDoan = 12;   // đoàn: > 12 người; lẻ: < 12 người
        public const string Le = "LE";
        public const string Doan = "DOAN";
        public const string MoDangKy = "Mở đăng ký";
        public const string DongDangKy = "Đóng đăng ký";
        public const string DaDangKy = "Đã đăng ký";
        public const string HuyMatCoc = "Hủy - mất cọc";
        public const string HoanTatThanhToan = "Đã hoàn tất thanh toán";
    }
}
