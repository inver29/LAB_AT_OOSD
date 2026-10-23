using System;

namespace QuanLyNhanSu.Common
{
    public static class CurrentContext
    {
        public static string CurrentRole { get; set; } = "Quản trị / Toàn quyền (Demo)";
        public static int? CurrentMaNV { get; set; } = 1;
        public static string CurrentHoTen { get; set; } = "Nguyễn Văn Giám Đốc";

        public static bool IsAdmin => CurrentRole == "Quản trị / Toàn quyền (Demo)";
        public static bool IsNhanSu => IsAdmin || CurrentRole == "Nhân sự";
        public static bool IsTruongBoPhan => IsAdmin || CurrentRole == "Trưởng bộ phận";
        public static bool IsBanChamCong => IsAdmin || CurrentRole == "Ban chấm công";
        public static bool IsKeToan => IsAdmin || CurrentRole == "Kế toán";
        public static bool IsGiamDoc => IsAdmin || CurrentRole == "Giám đốc";
    }
}
