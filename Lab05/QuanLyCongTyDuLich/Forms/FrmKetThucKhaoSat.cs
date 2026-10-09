using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmKetThucKhaoSat : Form
    {
        private readonly KetThucService svc = new KetThucService();

        public FrmKetThucKhaoSat()
        {
            InitializeComponent();
            FormHelper.ApDungGiaoDien(this);
        }

        private void FrmKetThucKhaoSat_Load(object sender, EventArgs e)
        {
            cboLoaiKS.Items.AddRange(new object[] { QuyDinh.Le, QuyDinh.Doan });
            cboLoaiKS.SelectedIndex = 0;
            TaiThanhToan();
            TaiKhaoSat();
        }

        private void TaiThanhToan() { dgvDoan.DataSource = svc.DoanCanThanhToan(); }
        private void TaiKhaoSat() { dgvKS.DataSource = svc.LayKhaoSat(); }

        private void dgvDoan_SelectionChanged(object sender, EventArgs e)
        {
            txtSoDK.Text = FormHelper.O(dgvDoan, "SoDKDoan");
            string con = FormHelper.O(dgvDoan, "ConLai");
            if (con != "")
            {
                decimal conVal;
                if (decimal.TryParse(con, out conVal))
                    numTien.Value = Math.Max(0, Math.Min(numTien.Maximum, conVal));
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThanhToanDoan(txtSoTT.Text, txtSoDK.Text, dtTT.Value, numTien.Value, txtGhiChu.Text))) TaiThanhToan();
        }

        private void cboLoaiKS_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormHelper.Nap(cboDangKy, svc.LayDangKyChoKhaoSat(cboLoaiKS.Text), "HienThi", "Ma");
        }

        private void btnGui_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.GuiKhaoSat(txtMaKS.Text, cboLoaiKS.Text, FormHelper.Gia(cboDangKy), dtGui.Value)))
            {
                TaiKhaoSat();
                cboLoaiKS_SelectedIndexChanged(sender, e);
            }
        }

        private void dgvKS_SelectionChanged(object sender, EventArgs e)
        {
            txtKSChon.Text = FormHelper.O(dgvKS, "MaKhaoSat");
        }

        private void btnGhiPH_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.GhiPhanHoi(txtKSChon.Text, dtPH.Value, (int)numDiem.Value, txtGopY.Text))) TaiKhaoSat();
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
