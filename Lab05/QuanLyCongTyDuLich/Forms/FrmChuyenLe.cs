using System;
using System.Data;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    public partial class FrmChuyenLe : Form
    {
        private readonly ChuyenLeService svc = new ChuyenLeService();
        private readonly TourService tour = new TourService();

        public FrmChuyenLe()
        {
            InitializeComponent();
            FormHelper.ApDungGiaoDien(this);
        }

        private void FrmChuyenLe_Load(object sender, EventArgs e)
        {
            lblNgayVe.Left = lblNgayVeTieuDe.Right + 8;
            FormHelper.Nap(cboTour, tour.LayTourMoBan(), "HienThi", "MaTour");
            dtDi.Value = DateTime.Today.AddDays(7);
            Tai();
            TinhNgayVe(sender, e);
        }

        private void Tai() { dgv.DataSource = svc.LayChuyen(); }

        private void TinhNgayVe(object sender, EventArgs e)
        {
            var r = cboTour.SelectedItem as DataRowView;
            lblNgayVe.Text = r == null ? "-" : dtDi.Value.Date.AddDays(Convert.ToInt32(r["SoNgay"]) - 1).ToString("dd/MM/yyyy");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.ThemChuyen(txtMa.Text, FormHelper.Gia(cboTour), dtDi.Value, txtDon.Text))) Tai();
        }

        private void btnDongDK_Click(object sender, EventArgs e)
        {
            if (FormHelper.Bao(svc.DongDangKy(FormHelper.O(dgv, "MaChuyen")))) Tai();
        }

        private void btnDong_Click(object sender, EventArgs e) { Close(); }
    }
}
