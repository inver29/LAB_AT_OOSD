using System;
using System.Data;
using System.Windows.Forms;
using eShopping.Models;
using eShopping.Services;

namespace eShopping.Forms
{
    public partial class FrmCheckout : Form
    {
        private readonly CheckoutService _service;
        private BaoGia _baoGia;
        private bool _dangNap;

        public FrmCheckout(GioHang gio)
        {
            _service = new CheckoutService(gio);
            InitializeComponent();
        }

        private void FrmCheckout_Load(object sender, EventArgs e)
        {
            try
            {
                _dangNap = true;

                cboKhachHang.DisplayMember = "HoTen";
                cboKhachHang.ValueMember = "MaKH";
                cboKhachHang.DataSource = _service.LayKhachHang();

                cboKhuVuc.DisplayMember = "KhuVuc";
                cboKhuVuc.ValueMember = "KhuVuc";
                cboKhuVuc.DataSource = _service.LayKhuVuc();

                cboLoaiGiao.DisplayMember = "Ten";
                cboLoaiGiao.ValueMember = "Ma";
                cboLoaiGiao.DataSource = new[]
                {
                    new LuaChon { Ma = "Thuong", Ten = "Giao thường" },
                    new LuaChon { Ma = "Nhanh", Ten = "Chuyển phát nhanh" },
                    new LuaChon
                    {
                        Ma = "TrongNgay",
                        Ten = "Chuyển phát nhanh trong ngày"
                    }
                };

                cboLoaiThe.DisplayMember = "Ten";
                cboLoaiThe.ValueMember = "Ma";
                cboLoaiThe.DataSource = new[]
                {
                    new LuaChon { Ma = "VISA", Ten = "Visa" },
                    new LuaChon { Ma = "MasterCard", Ten = "MasterCard" },
                    new LuaChon { Ma = "Discover", Ten = "Discover" },
                    new LuaChon
                    {
                        Ma = "AmericanExpress",
                        Ten = "American Express"
                    }
                };

                dgvDon.DataSource = _service.LayDonHang();
            }
            catch (Exception ex)
            {
                BaoLoi(ex);
            }
            finally
            {
                _dangNap = false;
            }

            DienNguoiNhan();
            HuyBaoGia(null, EventArgs.Empty);
            HienChiTietDon();
        }

        private void DienNguoiNhan()
        {
            var row = cboKhachHang.SelectedItem as DataRowView;

            if (row == null)
                return;

            txtNguoiNhan.Text = Convert.ToString(row["HoTen"]);
            txtDiaChi.Text = Convert.ToString(row["DiaChi"]);
            txtDienThoai.Text = Convert.ToString(row["DienThoai"]);
        }

        private void cboKhachHang_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (_dangNap)
                return;

            DienNguoiNhan();
            HuyBaoGia(sender, e);
        }

        private void HuyBaoGia(object sender, EventArgs e)
        {
            if (_dangNap)
                return;

            _baoGia = null;
            btnLuu.Enabled = false;
            lblTong.Text = "Chọn thông tin giao hàng và bấm Tính tổng.";
        }

        private string GiaTri(ComboBox combo, string tenTruong)
        {
            string value = Convert.ToString(combo.SelectedValue);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException(
                    "Hãy chọn " + tenTruong + ".");
            }

            return value;
        }

        private void btnTinhTong_Click(object sender, EventArgs e)
        {
            try
            {
                _baoGia = null;
                btnLuu.Enabled = false;

                _baoGia = _service.TinhTien(
                    GiaTri(cboKhuVuc, "khu vực"),
                    GiaTri(cboLoaiGiao, "loại giao"),
                    GiaTri(cboLoaiThe, "loại thẻ"));

                lblTong.Text = string.Format(
                    "Tiền hàng: {0:N0} đ   |   Phí giao: {1:N0} đ\n" +
                    "Lệ phí thẻ: {2:N0} đ   |   Tổng tiền: {3:N0} đ",
                    _baoGia.TienHang,
                    _baoGia.PhiGiaoHang,
                    _baoGia.LePhiThe,
                    _baoGia.TongTien);

                btnLuu.Enabled = true;
            }
            catch (Exception ex)
            {
                lblTong.Text = "Chưa tính được tổng tiền.";
                BaoLoi(ex);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(cboKhachHang.SelectedValue is Guid))
                {
                    throw new InvalidOperationException(
                        "Hãy chọn khách hàng có sẵn trong SQL.");
                }

                if (_baoGia == null)
                {
                    throw new InvalidOperationException(
                        "Hãy bấm Tính tổng trước khi lưu đơn.");
                }

                if (MessageBox.Show(
                    string.Format(
                        "Lưu đơn chờ thanh toán với tổng tiền {0:N0} đ?",
                        _baoGia.TongTien),
                    "Xác nhận đặt hàng",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                btnLuu.Enabled = false;

                var yeuCau = new YeuCauDatHang
                {
                    MaKH = (Guid)cboKhachHang.SelectedValue,
                    HoTenNguoiNhan = txtNguoiNhan.Text,
                    DiaChiNguoiNhan = txtDiaChi.Text,
                    DienThoaiNguoiNhan = txtDienThoai.Text,
                    KhuVuc = GiaTri(cboKhuVuc, "khu vực"),
                    LoaiGiao = GiaTri(cboLoaiGiao, "loại giao"),
                    LoaiThe = GiaTri(cboLoaiThe, "loại thẻ")
                };

                var ketQua = _service.LuuDon(yeuCau, _baoGia);

                _baoGia = null;
                lblTong.Text = "Đã lưu đơn vào SQL. Trạng thái: Chờ thanh toán.";

                MessageBox.Show(
                    "Đã lưu đơn và chi tiết đơn vào SQL.\n\n" +
                    "Mã đơn: " + ketQua.MaDon + "\n" +
                    "Mã tham chiếu: " + ketQua.MaThamChieu + "\n" +
                    "Tổng tiền: " + ketQua.TongTien.ToString("N0") + " đ",
                    "Đặt hàng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                TaiDanhSachDon();
            }
            catch (Exception ex)
            {
                _baoGia = null;
                btnLuu.Enabled = false;
                lblTong.Text = "Chưa lưu được đơn. Kiểm tra và tính tổng lại.";
                BaoLoi(ex);
            }
        }

        private void TaiDanhSachDon()
        {
            try
            {
                _dangNap = true;
                dgvDon.DataSource = _service.LayDonHang();
            }
            finally
            {
                _dangNap = false;
            }

            HienChiTietDon();
        }

        private void btnTaiDon_Click(object sender, EventArgs e)
        {
            try { TaiDanhSachDon(); }
            catch (Exception ex) { BaoLoi(ex); }
        }

        private void dgvDon_SelectionChanged(object sender, EventArgs e)
        {
            if (!_dangNap)
                HienChiTietDon();
        }

        private void HienChiTietDon()
        {
            try
            {
                if (dgvDon.CurrentRow == null ||
                    !(dgvDon.CurrentRow.Cells["MaDon"].Value is Guid))
                {
                    dgvChiTiet.DataSource = null;
                    return;
                }

                Guid maDon = (Guid)dgvDon.CurrentRow
                    .Cells["MaDon"].Value;

                dgvChiTiet.DataSource = _service.LayChiTietDon(maDon);
            }
            catch (Exception ex)
            {
                dgvChiTiet.DataSource = null;
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
                ex.Message, "Đặt hàng",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}