using System;
using System.Windows.Forms;
using eShopping.Data;
using eShopping.Models;

namespace eShopping.Forms
{
    public partial class FrmMain : Form
    {
        private readonly GioHang _gio = new GioHang();
        private readonly Repository _repository = new Repository();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            TaiThongKe();
        }

        private void TaiThongKe()
        {
            lblGio.Text = "Giỏ hàng: " + _gio.TongSoLuong + " sản phẩm";

            try
            {
                var row = _repository.LayThongKe().Rows[0];

                lblKetNoi.Text = "SQL Server: kết nối thành công";
                lblKetNoi.ForeColor = System.Drawing.Color.DarkGreen;

                lblThongKe.Text = string.Format(
                    "Sản phẩm: {0}     Khách hàng: {1}     Đơn hàng: {2}",
                    row["SoSanPham"],
                    row["SoKhachHang"],
                    row["SoDonHang"]);
            }
            catch (Exception ex)
            {
                lblKetNoi.Text = "SQL Server: chưa kết nối được";
                lblKetNoi.ForeColor = System.Drawing.Color.Firebrick;
                lblThongKe.Text = "Kiểm tra App.config và CSDL eShopping.";

                MessageBox.Show(
                    ex.Message, "Kết nối SQL",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            using (var form = new FrmSanPham(_gio))
                form.ShowDialog(this);

            TaiThongKe();
        }

        private void btnGioHang_Click(object sender, EventArgs e)
        {
            using (var form = new FrmGioHang(_gio))
                form.ShowDialog(this);

            TaiThongKe();
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            using (var form = new FrmCheckout(_gio))
                form.ShowDialog(this);

            TaiThongKe();
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            TaiThongKe();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}