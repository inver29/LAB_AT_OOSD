using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using eShopping.Forms;

namespace eShopping
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Thread.CurrentThread.CurrentCulture =
                new CultureInfo("vi-VN");

            Thread.CurrentThread.CurrentUICulture =
                new CultureInfo("vi-VN");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}