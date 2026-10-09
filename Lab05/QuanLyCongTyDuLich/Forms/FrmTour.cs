using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmTour : Form
    {
        private readonly TourService svc = new TourService();
        private readonly DanhMucService dm = new DanhMucService();

        public FrmTour()
        {
            InitializeComponent();
            FormHelper.ApDungGiaoDien(this);
        }

        private void FrmTour_Load(object sender, EventArgs e)
        {
            cboTour.Left = lblTourChon.Right + 12;
            FormHelper.Nap(cboPT, dm.LayPhuongTien(), "TenPT", "MaPT");
            FormHelper.Nap(cboDTQ, dm.LayDiemThamQuan(), "TenDiemTQ", "MaDiemTQ");
            TaiTour();
        }

        private void TaiTour()
        {
            dgvTour.DataSource = svc.LayTour();
            FormHelper.Nap(cboTour, svc.LayTour(), "TenTour", "MaTour");
            TaiChiTiet();
        }

        private void TaiChiTiet()
        {
            string ma = FormHelper.Gia(cboTour);
            dgvDiemDung.DataSource = svc.LayDiemDung(ma);
            dgvChang.DataSource = svc.LayChang(ma);
            dgvTQ.DataSource = svc.LayDiemTQTour(ma);
        }

        private void cboTour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTour.ValueMember == "MaTour") TaiChiTiet();
        }

        private void chkKS_CheckedChanged(object sender, EventArgs e) { numSao.Enabled = chkKS.Checked; }

        private void btnThemTour_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThemTour(txtMa.Text, txtTen.Text, (int)numNgay.Value, (int)numDem.Value, numGia.Value, txtMoTa.Text))) TaiTour();
        }

        private void btnThemDD_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThemDiemDung(FormHelper.Gia(cboTour), (int)numThuTu.Value, txtDiemDung.Text, chkDoiPT.Checked, chkAn.Checked,
                chkKS.Checked, (int)numSao.Value, txtGhiChuDD.Text))) TaiChiTiet();
        }

        private void btnThemChang_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThemChang(FormHelper.Gia(cboTour), (int)numChang.Value, FormHelper.Gia(cboPT), txtGhiChuPT.Text))) TaiChiTiet();
        }

        private void btnThemTQ_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThemDiemTQTour(FormHelper.Gia(cboTour), FormHelper.Gia(cboDTQ), (int)numThuTuTQ.Value))) TaiChiTiet();
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
