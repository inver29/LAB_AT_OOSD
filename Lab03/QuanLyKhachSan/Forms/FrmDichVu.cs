using QuanLyKhachSan.Models;
using QuanLyKhachSan.Services;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace QuanLyKhachSan.Forms
{
    public partial class FrmDichVu : Form
    {
        readonly DichVuService s = new DichVuService();
        readonly DanhMucService dm = new DanhMucService();

        public FrmDichVu() { InitializeComponent(); }

        private void Frm_Load(object a, EventArgs e)
        {
            cboLuot.DataSource = s.LayPhieuDangO(); cboLuot.DisplayMember = "SoPhieuDat"; cboLuot.ValueMember = "SoPhieuDat";
            cboDV.DataSource = s.LayDichVu(); cboDV.DisplayMember = "TenDV"; cboDV.ValueMember = "MaDV";
            cboNV.DataSource = dm.LayNhanVien(); cboNV.DisplayMember = "HoTen"; cboNV.ValueMember = "MaNV";
            Tai();
        }

        void Tai()
        {
            if (cboLuot.SelectedValue != null)
                dgvLichSu.DataSource = s.LayLichSu(cboLuot.SelectedValue.ToString());
        }

        string V(System.Windows.Forms.ComboBox c) { return c.SelectedValue == null ? "" : c.SelectedValue.ToString(); }

        private void cboLuot_SelectedIndexChanged(object a, EventArgs e)
        {
            if (cboLuot.SelectedItem is System.Data.DataRowView r)
                txtPhong.Text = Convert.ToString(r["SoPhong"]);
            Tai();
        }

        private void btnGhi_Click(object a, EventArgs e)
        {
            var k = s.GhiNhan(V(cboLuot), txtPhong.Text.Trim(), dtNgay.Value, V(cboNV), V(cboDV), (int)numSL.Value);
            MessageBox.Show(k.ThongBao);
            if (k.ThanhCong) Tai();
        }

        private void btnDong_Click(object a, EventArgs e) { Close(); }
    }
}