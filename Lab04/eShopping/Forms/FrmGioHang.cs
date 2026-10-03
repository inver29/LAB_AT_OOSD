using System;
using System.Windows.Forms;
using eShopping.Models;
using eShopping.Services;

namespace eShopping.Forms
{
    public partial class FrmGioHang : Form
    {
        private readonly GioHang _gio;
        private readonly GioHangService _service;

        public FrmGioHang(GioHang gio)
        {
            _gio = gio;
            _service = new GioHangService(gio);
            InitializeComponent();
        }

        private void FrmGioHang_Load(object sender, EventArgs e)
        {
            dgvGio.DataSource = _gio.ChiTiet;
            HienTong();
        }

        private GioHangItem DongDangChon()
        {
            if (dgvGio.CurrentRow == null)
                throw new InvalidOperationException(
                    "Hãy chọn sản phẩm trong giỏ.");

            var item = dgvGio.CurrentRow.DataBoundItem as GioHangItem;

            if (item == null)
                throw new InvalidOperationException("Dòng giỏ không hợp lệ.");

            return item;
        }

        private void HienTong()
        {
            lblTong.Text = string.Format(
                "Số lượng: {0} | Tiền hàng tham khảo: {1:N0} đ",
                _gio.TongSoLuong, _gio.TongTien);
        }

        private void dgvGio_SelectionChanged(object sender, EventArgs e)
        {
            var item = dgvGio.CurrentRow == null
                ? null
                : dgvGio.CurrentRow.DataBoundItem as GioHangItem;

            if (item != null)
                numSoLuong.Value = item.SoLuong;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                var item = DongDangChon();

                _service.CapNhat(
                    item.MaSP, (int)numSoLuong.Value);

                HienTong();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var item = DongDangChon();
                _service.Xoa(item.MaSP);
                HienTong();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void btnLamRong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Xóa toàn bộ sản phẩm trong giỏ?",
                "Giỏ hàng",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            _service.LamRong();
            HienTong();
        }

        private void btnLayGia_Click(object sender, EventArgs e)
        {
            try
            {
                _service.CapNhatGiaTuSql();
                HienTong();

                MessageBox.Show(
                    "Đã lấy lại giá và tình trạng sản phẩm từ SQL.",
                    "Giỏ hàng");
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_gio.ChiTiet.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống.", "Giỏ hàng");
                return;
            }

            using (var form = new FrmCheckout(_gio))
                form.ShowDialog(this);

            _gio.ChiTiet.ResetBindings();
            HienTong();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BaoLoi(Exception ex)
        {
            MessageBox.Show(
                ex.Message, "Giỏ hàng",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}