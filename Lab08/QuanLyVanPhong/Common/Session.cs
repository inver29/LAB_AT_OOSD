using System;
namespace QuanLyVanPhong.Common
{
 public static class Session
 {
  // Mô phỏng vai trò trong phạm vi bài lab, chưa phải cơ chế đăng nhập.
  public static string Role { get; set; } = "Toàn quyền (demo)";
  public static void Require(params string[] roles)
  {
   if (Role == "Toàn quyền (demo)" || Array.IndexOf(roles, Role) >= 0) return;
   throw new InvalidOperationException("Vai trò hiện tại không được thực hiện thao tác này.");
  }
 }
}
