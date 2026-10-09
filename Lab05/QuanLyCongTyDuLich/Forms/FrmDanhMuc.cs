using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmDanhMuc : Form
    {
        private readonly DanhMucService svc = new DanhMucService();

        public FrmDanhMuc()
        {
            InitializeComponent();
            FormHelper.ApDungGiaoDien(this);
        }

        private void FrmDanhMuc_Load(object sender, EventArgs e) { Tai(); }

        private void Tai()
        {
            dgvPT.DataSource = svc.LayPhuongTien();
            dgvDB.DataSource = svc.LayDiemBan();
            dgvHDV.DataSource = svc.LayHDV();
            dgvDTQ.DataSource = svc.LayDiemThamQuan();
        }

        private void btnThemPT_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThemPhuongTien(txtPTMa.Text, txtPTTen.Text, txtPTGhiChu.Text))) Tai();
        }

        private void btnThemDB_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThemDiemBan(txtDBMa.Text, txtDBTen.Text, txtDBDiaChi.Text, txtDBDT.Text))) Tai();
        }

        private void btnThemHDV_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThemHDV(txtHDVMa.Text, txtHDVTen.Text, txtHDVDT.Text, numLuong.Value))) Tai();
        }

        private void btnThemDTQ_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThemDiemThamQuan(txtDTQMa.Text, txtDTQTen.Text, txtDTQDiaDiem.Text, txtDTQNoiDung.Text, txtDTQYNghia.Text))) Tai();
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
