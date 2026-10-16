using System;
using System.Data;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmThanhToan : Form
    {
        private readonly ThanhToanService service = new ThanhToanService();
        private bool loading;
        private int maPhieu;
        private decimal tong;
        public FrmThanhToan()
        {
            InitializeComponent(); FormHelper.Style(this);
            Shown += (s, e) => FormHelper.Run(this, LoadData);
            btnSearch.Click += (s, e) => FormHelper.Run(this, LoadData);
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; FormHelper.Run(this, LoadData); } };
            btnReload.Click += (s, e) => { txtSearch.Clear(); FormHelper.Run(this, LoadData); };
            gridPhieu.SelectionChanged += (s, e) => { if (!loading) FormHelper.Run(this, ChonPhieu); };
            btnThanhToan.Click += (s, e) => FormHelper.Run(this, ThanhToan);
        }
        private void LoadData()
        {
            loading = true;
            try
            {
                gridPhieu.DataSource = service.DanhSach(txtSearch.Text);
                FormHelper.Columns(gridPhieu, "MaPhieuKham", "Mã phiếu", "HoTen", "Bệnh nhân", "NgayKham", "Ngày khám", "TrangThai", "Trạng thái", "TongVienPhi", "Đã thu (đ)", "NgayThanhToan", "Ngày thanh toán");
            }
            finally { loading = false; }
            ChonPhieu();
        }
        private void ChonPhieu()
        {
            DataRow row = FormHelper.Row(gridPhieu); maPhieu = row == null ? 0 : Convert.ToInt32(row["MaPhieuKham"]);
            DataTable costs = service.ChiPhi(maPhieu); gridChiPhi.DataSource = costs;
            FormHelper.Columns(gridChiPhi, "Nhom", "Khoản thu", "NoiDung", "Nội dung", "SoLuong", "Số lượng", "DonGia", "Đơn giá (đ)", "ThanhTien", "Thành tiền (đ)");
            tong = 0; foreach (DataRow cost in costs.Rows) tong += Convert.ToDecimal(cost["ThanhTien"]);
            bool paid = row != null && row["TrangThai"].ToString() == "Đã thanh toán";
            lblTong.Text = (paid ? "Đã thanh toán: " : "Tổng viện phí: ") + FormHelper.Money(paid ? Convert.ToDecimal(row["TongVienPhi"]) : tong);
            btnThanhToan.Enabled = row != null && !paid;
            lblStatus.Text = row == null ? "Chưa có phiếu đủ điều kiện. Hoàn tất khám trước khi thanh toán."
                : "Phiếu " + maPhieu + " • " + row["HoTen"] + (paid ? " • Chỉ xem, không thu tiền lần nữa." : " • Kiểm tra các khoản thu trước khi xác nhận.");
        }
        private void ThanhToan()
        {
            if (maPhieu == 0) throw new InvalidOperationException("Hãy chọn phiếu khám.");
            if (MessageBox.Show(this, "Xác nhận đã thu " + FormHelper.Money(tong) + " cho phiếu " + maPhieu + "?", "Thanh toán viện phí", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            int id = maPhieu; service.ThanhToan(id, tong); LoadData(); FormHelper.SelectId(gridPhieu, "MaPhieuKham", id);
            FormHelper.Success(this, "Đã ghi nhận thanh toán phiếu " + id + ".");
        }
    }
}
