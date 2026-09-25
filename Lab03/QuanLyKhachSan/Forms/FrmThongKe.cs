using System;
using System.Windows.Forms;
using QuanLyKhachSan.Services;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmThongKe : Form
    {
        readonly ThongKeService s = new ThongKeService();

        public FrmThongKe() { InitializeComponent(); }

        private void btnTK_Click(object a, EventArgs e)
        {
            if (dtDen.Value.Date < dtTu.Value.Date)
            {
                MessageBox.Show("Đến ngày không được trước từ ngày.");
                return;
            }
            dgvTongHop.DataSource = s.TongHop(dtTu.Value, dtDen.Value);
            dgvDV.DataSource = s.DichVu(dtTu.Value, dtDen.Value);
        }

        private void btnDong_Click(object a, EventArgs e) { Close(); }
    }
}