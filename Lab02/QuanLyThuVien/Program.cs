using System;
using System.Windows.Forms;
using QuanLyThuVien.Forms;

namespace QuanLyThuVien
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Chạy Form mới tạo
            Application.Run(new MainForm());
        }
    }
}