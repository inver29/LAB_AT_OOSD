using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent(); FormHelper.Style(this);
            StartPosition = FormStartPosition.CenterScreen;
            foreach (var button in new[] { btnHoSo, btnKham, btnToa, btnThanhToan })
            {
                button.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Padding = new Padding(16);
                button.BackColor = Color.FromArgb(232, 240, 248);
                button.ForeColor = Color.FromArgb(35, 77, 115);
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.FromArgb(197, 213, 229);
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(217, 231, 244);
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(201, 221, 240);
            }
            lblTitle.ForeColor = Color.FromArgb(35, 65, 94);
            btnExit.BackColor = Color.FromArgb(230, 235, 241);
            btnExit.ForeColor = FormHelper.Ink;
            btnExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(215, 223, 233);
            btnExit.FlatAppearance.MouseDownBackColor = Color.FromArgb(201, 211, 224);
            btnHoSo.Click += (s, e) => MoForm(() => new FrmHoSo());
            btnKham.Click += (s, e) => MoForm(() => new FrmKhamBenh());
            btnToa.Click += (s, e) => MoForm(() => new FrmToaThuoc());
            btnThanhToan.Click += (s, e) => MoForm(() => new FrmThanhToan());
            btnExit.Click += (s, e) => Close();
        }
        private void MoForm(Func<Form> factory)
        {
            FormHelper.Run(this, () => { using (var form = factory()) form.ShowDialog(this); });
        }
    }
}
