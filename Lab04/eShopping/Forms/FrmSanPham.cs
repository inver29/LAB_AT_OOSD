using System;
using System.Windows.Forms;
using eShopping.Models;
using eShopping.Services;

namespace eShopping.Forms
{
    public partial class FrmSanPham : Form
    {
        private readonly SanPhamService _sanPhamService =
            new SanPhamService();

        private readonly GioHangService _gioService;
        private readonly GioHang _gio;

        public FrmSanPham(GioHang gio)
        {
            _gio = gio;
            _gioService = new GioHangService(gio);
            InitializeComponent();
        }

        private void FrmSanPham_Load(object sender, EventArgs e)
        {
            try
            {
                cboNhom.DisplayMember = "TenNhom";
                cboNhom.ValueMember = "MaNhom";
                cboNhom.DataSource = _sanPhamService.LayNhom();

                TaiSanPham();
                HienThongTinGio();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void TaiSanPham()
        {
            Guid? maNhom = null;

            if (cboNhom.SelectedValue is Guid)
            {
                Guid ma = (Guid)cboNhom.SelectedValue;

                if (ma != Guid.Empty)
                    maNhom = ma;
            }

            dgvSanPham.DataSource =
                _sanPhamService.TimKiem(txtTuKhoa.Text, maNhom);

            lblSoDong.Text =
                "Số sản phẩm: " + dgvSanPham.Rows.Count;
        }

        private void HienThongTinGio()
        {
            lblGio.Text = string.Format(
                "Giỏ: {0} sản phẩm | Tiền hàng: {1:N0} đ",
                _gio.TongSoLuong, _gio.TongTien);
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try { TaiSanPham(); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtTuKhoa.Clear();

            if (cboNhom.Items.Count > 0)
                cboNhom.SelectedIndex = 0;

            try { TaiSanPham(); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void btnThemGio_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSanPham.CurrentRow == null)
                    throw new InvalidOperationException(
                        "Hãy chọn một sản phẩm.");

                Guid maSP = (Guid)dgvSanPham.CurrentRow
                    .Cells["MaSP"].Value;

                _gioService.Them(maSP, (int)numSoLuong.Value);

                HienThongTinGio();

                MessageBox.Show(
                    "Đã thêm sản phẩm vào giỏ.",
                    "Giỏ hàng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BaoLoi(Exception ex)
        {
            MessageBox.Show(
                ex.Message, "Sản phẩm",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}