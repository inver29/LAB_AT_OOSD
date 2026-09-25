using System;
using System.Windows.Forms;
using QuanLyKhachSan.Services;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmPhongTienNghi : Form
    {
        readonly PhongTienNghiService s = new PhongTienNghiService();
        readonly DanhMucService dm = new DanhMucService();

        public FrmPhongTienNghi() { InitializeComponent(); }

        private void Frm_Load(object a, EventArgs e)
        {
            cboKhu.DataSource = dm.LayKhuVuc();
            cboKhu.DisplayMember = "TenKhuVuc"; cboKhu.ValueMember = "MaKhuVuc";

            cboLoai.DataSource = dm.LayLoaiTienNghi();
            cboLoai.DisplayMember = "TenLoaiTN"; cboLoai.ValueMember = "MaLoaiTN";

            cboTN.DataSource = s.LayTienNghi();
            cboTN.DisplayMember = "MaTienNghi"; cboTN.ValueMember = "MaTienNghi";

            cboPhong.DataSource = s.LayPhong();
            cboPhong.DisplayMember = "SoPhong"; cboPhong.ValueMember = "SoPhong";

            cboNV.DataSource = dm.LayNhanVien();
            cboNV.DisplayMember = "HoTen"; cboNV.ValueMember = "MaNV";
            Tai();
        }

        void Tai()
        {
            dgvPhong.DataSource = s.LayPhong();
            dgvTN.DataSource = s.LayTienNghi();
            dgvLD.DataSource = s.LayLapDat();
        }

        void H(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao);
            if (k.ThanhCong) Tai();
        }

        private void btnThemPhong_Click(object a, EventArgs e) { H(s.ThemPhong(txtPhong.Text.Trim(), V(cboKhu), (int)numMax.Value, numGia.Value)); }
        private void btnThemTN_Click(object a, EventArgs e) { H(s.ThemTienNghi(txtMaTN.Text.Trim(), V(cboLoai), (int)numSTT.Value, txtTinhTrang.Text.Trim())); }
        private void btnLapDat_Click(object a, EventArgs e) { H(s.LapDat(txtSoLD.Text.Trim(), V(cboTN), V(cboPhong), dtNgay.Value, txtTTLD.Text.Trim(), V(cboNV), txtGhiChu.Text.Trim())); }

        string V(ComboBox c) { return c.SelectedValue == null ? "" : c.SelectedValue.ToString(); }

        private void btnDong_Click(object a, EventArgs e) { Close(); }
    }
}