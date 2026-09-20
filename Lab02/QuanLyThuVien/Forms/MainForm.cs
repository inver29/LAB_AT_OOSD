using System;
using System.Windows.Forms;

namespace QuanLyThuVien.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            FormNhanVien frm = new FormNhanVien();
            frm.ShowDialog();
        }

        private void btnDauSach_Click(object sender, EventArgs e)
        {
            FormSach frm = new FormSach();
            frm.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}