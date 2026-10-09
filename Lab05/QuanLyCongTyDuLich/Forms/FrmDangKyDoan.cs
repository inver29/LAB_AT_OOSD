using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmDangKyDoan : Form
    {
        private readonly DangKyDoanService svc = new DangKyDoanService();
        private readonly BindingList<ThanhVienDoanItem> thanhVien = new BindingList<ThanhVienDoanItem>();

        public FrmDangKyDoan()
        {
            InitializeComponent();
            FormHelper.ApDungGiaoDien(this);
            dgvThanhVien.ReadOnly = false;
            dgvThanhVien.AllowUserToAddRows = true;
            dgvThanhVien.AllowUserToDeleteRows = true;
        }

        private void FrmDangKyDoan_Load(object sender, EventArgs e)
        {
            lblKetThuc.Left = lblKetThucTieuDe.Right + 6;
            lblTong.Left = lblTongTieuDe.Right + 6;
            FormHelper.Nap(cboTour, new TourService().LayTourMoBan(), "HienThi", "MaTour");
            dtDi.Value = DateTime.Today.AddDays(14);
            dgvThanhVien.DataSource = thanhVien;
            Tai();
            TinhTong(sender, e);
            chkBH_CheckedChanged(sender, e);
        }

        private void Tai() { dgv.DataSource = svc.LayDanhSach(); }

        private void TinhTong(object sender, EventArgs e)
        {
            var r = cboTour.SelectedItem as DataRowView;
            if (r == null) { lblTong.Text = "0 đ"; lblKetThuc.Text = "-"; return; }
            lblTong.Text = (Convert.ToDecimal(r["DonGiaKhach"]) * numNguoi.Value).ToString("N0") + " đ";
            lblKetThuc.Text = dtDi.Value.Date.AddDays(Convert.ToInt32(r["SoNgay"]) - 1).ToString("dd/MM/yyyy");
        }

        private void chkBH_CheckedChanged(object sender, EventArgs e)
        {
            dgvThanhVien.Enabled = chkBH.Checked;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            dgvThanhVien.EndEdit();
            var ds = thanhVien.Where(x => x != null && !string.IsNullOrWhiteSpace(x.HoTen)).ToList();
            var k = svc.DangKy(txtSo.Text, txtMaDoan.Text, txtTenCQ.Text, txtDiaChi.Text, txtDT.Text, txtDaiDien.Text,
                FormHelper.Gia(cboTour), dtDi.Value, (int)numNguoi.Value, txtDon.Text, chkBH.Checked, numCoc.Value, ds);
            if (FormHelper.Bao(k)) { thanhVien.Clear(); Tai(); }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            string so = FormHelper.O(dgv, "SoDKDoan");
            if (so == "") { MessageBox.Show("Chọn phiếu đăng ký đoàn cần hủy."); return; }
            if (MessageBox.Show("Đoàn không đi sẽ mất tiền cọc. Hủy phiếu " + so + "?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            if (FormHelper.Bao(svc.HuyDangKy(so))) Tai();
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
