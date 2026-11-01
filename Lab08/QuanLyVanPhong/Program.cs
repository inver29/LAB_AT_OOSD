using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
namespace QuanLyVanPhong
{
 internal static class Program
 {
  [STAThread] static void Main()
  {
   Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
   Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("vi-VN");
   Application.EnableVisualStyles();
   Application.SetCompatibleTextRenderingDefault(false);
   Application.Run(new Forms.FrmMain());
  }
 }
}
