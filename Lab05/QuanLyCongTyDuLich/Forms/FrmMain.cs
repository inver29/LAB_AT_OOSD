using System;
using System.Windows.Forms;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            FormHelper.ApDungGiaoDien(this);
        }

        private void Mo(Form f)
        {
            using (f) f.ShowDialog(this);
        }

        private void btnDanhMuc_Click(object sender, EventArgs e) { Mo(new FrmDanhMuc()); }
        private void btnTour_Click(object sender, EventArgs e) { Mo(new FrmTour()); }
        private void btnChuyenLe_Click(object sender, EventArgs e) { Mo(new FrmChuyenLe()); }
        private void btnDangKyLe_Click(object sender, EventArgs e) { Mo(new FrmDangKyLe()); }
        private void btnDangKyDoan_Click(object sender, EventArgs e) { Mo(new FrmDangKyDoan()); }
        private void btnPhanCong_Click(object sender, EventArgs e) { Mo(new FrmPhanCongHDV()); }
        private void btnKetThuc_Click(object sender, EventArgs e) { Mo(new FrmKetThucKhaoSat()); }
        private void btnThongKe_Click(object sender, EventArgs e) { Mo(new FrmLuongThongKe()); }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thực sự muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        }
    }
}
