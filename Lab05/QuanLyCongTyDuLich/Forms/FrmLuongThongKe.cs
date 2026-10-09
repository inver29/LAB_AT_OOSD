using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmLuongThongKe : Form
    {
        private readonly ThongKeService svc = new ThongKeService();

        public FrmLuongThongKe()
        {
            InitializeComponent();
            FormHelper.ApDungGiaoDien(this);
        }

        private void FrmLuongThongKe_Load(object sender, EventArgs e)
        {
            lblGhiChuLuong.Left = btnLuong.Right + 16;
            numThang.Value = DateTime.Today.Month;
            numNam.Value = DateTime.Today.Year;
            dtTu.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtDen.Value = DateTime.Today;
        }

        private void btnLuong_Click(object sender, EventArgs e)
        {
            dgvLuong.DataSource = svc.LuongHDV((int)numThang.Value, (int)numNam.Value);
        }

        private void btnTongHop_Click(object sender, EventArgs e)
        {
            if (dtDen.Value.Date < dtTu.Value.Date) { MessageBox.Show("Đến ngày không được trước từ ngày."); return; }
            dgvTongHop.DataSource = svc.TongHop(dtTu.Value, dtDen.Value);
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
