using System;
using System.Windows.Forms;
using QuanLyKhachSan.Services;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDanhMuc : Form
    {
        readonly DanhMucService s = new DanhMucService();
        public FrmDanhMuc() { InitializeComponent(); }

        private void FrmDanhMuc_Load(object a, EventArgs e) { Tai(); }

        void Tai()
        {
            dgvKhu.DataSource = s.LayKhuVuc();
            dgvNV.DataSource = s.LayNhanVien();
            dgvLoaiTN.DataSource = s.LayLoaiTienNghi();
            dgvDV.DataSource = s.LayDichVu();
            dgvQD.DataSource = s.LayQuyDinhDenBu();
            cboQDLoai.DataSource = s.LayLoaiTienNghi();
            cboQDLoai.DisplayMember = "TenLoaiTN";
            cboQDLoai.ValueMember = "MaLoaiTN";
        }

        void H(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao);
            if (k.ThanhCong) Tai();
        }

        private void btnThemKhu_Click(object a, EventArgs e) { H(s.ThemKhu(txtKhuMa.Text.Trim(), txtKhuTen.Text.Trim())); }
        private void btnThemNV_Click(object a, EventArgs e) { H(s.ThemNhanVien(txtNVMa.Text.Trim(), txtNVTen.Text.Trim(), txtNVVaiTro.Text.Trim(), txtNVSDT.Text.Trim())); }
        private void btnThemLoaiTN_Click(object a, EventArgs e) { H(s.ThemLoaiTN(txtLoaiMa.Text.Trim(), txtLoaiTen.Text.Trim())); }
        private void btnThemDV_Click(object a, EventArgs e) { H(s.ThemDichVu(txtDVMa.Text.Trim(), txtDVTen.Text.Trim(), txtDVDVT.Text.Trim(), numDVGia.Value)); }

        private void btnThemQD_Click(object a, EventArgs e)
        {
            H(s.ThemQuyDinh(txtQDMa.Text.Trim(), cboQDLoai.SelectedValue == null ? "" : cboQDLoai.SelectedValue.ToString(), txtQDMucDo.Text.Trim(), numQDTien.Value));
        }

        private void btnDong_Click(object a, EventArgs e) { Close(); }
    }
}