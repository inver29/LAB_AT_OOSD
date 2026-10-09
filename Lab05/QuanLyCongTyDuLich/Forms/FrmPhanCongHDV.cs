using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmPhanCongHDV : Form
    {
        private readonly PhanCongService svc = new PhanCongService();

        public FrmPhanCongHDV()
        {
            InitializeComponent();
            FormHelper.ApDungGiaoDien(this);
        }

        private void FrmPhanCongHDV_Load(object sender, EventArgs e)
        {
            FormHelper.Nap(cboHDV, svc.LayHDVDangLam(), "HienThi", "MaHDV");
            cboLoai.Items.AddRange(new object[] { QuyDinh.Le, QuyDinh.Doan });
            cboLoai.SelectedIndex = 0;
            Tai();
        }

        private void Tai() { dgv.DataSource = svc.LayDanhSach(); }

        private void cboLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormHelper.Nap(cboDoiTuong, svc.LayDoiTuong(cboLoai.Text), "HienThi", "Ma");
        }

        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.PhanCong(txtMaPC.Text, FormHelper.Gia(cboHDV), cboLoai.Text, FormHelper.Gia(cboDoiTuong), numThuLao.Value)))
            {
                Tai();
                cboLoai_SelectedIndexChanged(sender, e);
            }
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
