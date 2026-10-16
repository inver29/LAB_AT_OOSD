using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmToaThuoc : Form
    {
        private readonly ToaThuocService service = new ToaThuocService();
        private DataTable cart;
        private bool loading, readonlyToa;
        private int maPhieu;
        public FrmToaThuoc()
        {
            InitializeComponent(); FormHelper.Style(this);
            numSoLuong.Minimum = 1; numSoLuong.Maximum = 100000; numSoLuong.Value = 1;
            txtLieuDung.MaxLength = 100; txtCachDung.MaxLength = 200;
            Shown += (s, e) => FormHelper.Run(this, LoadData);
            cbPhieu.SelectedIndexChanged += (s, e) => { if (!loading) FormHelper.Run(this, ChonPhieu); };
            cbThuoc.SelectedIndexChanged += (s, e) => CapNhatGia();
            btnThem.Click += (s, e) => FormHelper.Run(this, ThemThuoc);
            btnXoa.Click += (s, e) => { if (!readonlyToa && gridToa.CurrentRow != null) { cart.Rows.RemoveAt(gridToa.CurrentRow.Index); TongTien(); } };
            btnLuu.Click += (s, e) => FormHelper.Run(this, LuuToa);
            btnReload.Click += (s, e) => FormHelper.Run(this, () => { if (ChoBoBanNhap()) LoadData(); });
            FormClosing += (s, e) => { if (!ChoBoBanNhap()) e.Cancel = true; };
        }
        private bool ChoBoBanNhap()
        {
            return readonlyToa || cart == null || cart.Rows.Count == 0 || MessageBox.Show(this,
                "Toa đang nhập chưa lưu. Bỏ các dòng đang nhập?", "Toa chưa lưu", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        private void LoadData()
        {
            loading = true;
            try
            {
                FormHelper.Bind(cbPhieu, new KhamBenhService().DanhSach(), "MaPhieuKham", "HienThi");
                FormHelper.Bind(cbThuoc, new DanhMucService().Thuoc(), "MaThuoc", "HienThi");
                maPhieu = 0; cart = null;
            }
            finally { loading = false; }
            ChonPhieu(); CapNhatGia();
        }
        private void ChonPhieu()
        {
            int next = FormHelper.Id(cbPhieu);
            if (next == maPhieu && cart != null) return;
            if (!ChoBoBanNhap())
            { loading = true; cbPhieu.SelectedValue = maPhieu; loading = false; return; }
            maPhieu = next;
            cart = service.ChiTiet(maPhieu); var info = service.ThongTin(maPhieu);
            bool hasToa = info.Rows.Count > 0;
            var visit = cbPhieu.SelectedItem as DataRowView;
            bool completed = visit != null && visit["TrangThai"].ToString() == "Hoàn tất khám";
            readonlyToa = hasToa || !completed;
            txtLoiDan.Text = hasToa ? info.Rows[0]["LoiDan"].ToString() : "";
            txtLoiDan.ReadOnly = txtLieuDung.ReadOnly = txtCachDung.ReadOnly = readonlyToa;
            cbThuoc.Enabled = numSoLuong.Enabled = btnThem.Enabled = btnXoa.Enabled = btnLuu.Enabled = !readonlyToa;
            gridToa.DataSource = cart;
            FormHelper.Columns(gridToa, "STT", "STT", "TenThuoc", "Thuốc", "SoLuong", "Số lượng", "DonGia", "Đơn giá", "ThanhTien", "Thành tiền", "LieuDung", "Liều dùng", "CachDung", "Cách dùng");
            txtLieuDung.Clear(); txtCachDung.Clear(); numSoLuong.Value = 1;
            TongTien();
        }
        private void CapNhatGia()
        {
            var row = cbThuoc.SelectedItem as DataRowView;
            lblDonGia.Text = row == null ? "—" : FormHelper.Money(Convert.ToDecimal(row["DonGia"]));
        }
        private void ThemThuoc()
        {
            if (readonlyToa) throw new InvalidOperationException("Phiếu chưa hoàn tất hoặc đã có toa thuốc.");
            int thuoc = FormHelper.Id(cbThuoc);
            var row = cbThuoc.SelectedItem as DataRowView;
            if (row == null || thuoc == 0) throw new InvalidOperationException("Hãy chọn thuốc.");
            if (cart.AsEnumerable().Any(x => x.Field<int>("MaThuoc") == thuoc)) throw new InvalidOperationException("Thuốc đã có trong toa. Bỏ dòng cũ nếu cần nhập lại số lượng.");
            string lieu = NghiepVu.Text(txtLieuDung.Text, "liều dùng", 100);
            string cach = NghiepVu.Text(txtCachDung.Text, "cách dùng", 200);
            decimal gia = Convert.ToDecimal(row["DonGia"]);
            cart.Rows.Add(cart.Rows.Count + 1, thuoc, row["HienThi"].ToString(), (int)numSoLuong.Value, gia, numSoLuong.Value * gia, lieu, cach);
            for (int i = 0; i < cart.Rows.Count; i++) cart.Rows[i]["STT"] = i + 1;
            txtLieuDung.Clear(); txtCachDung.Clear(); numSoLuong.Value = 1; TongTien();
        }
        private void TongTien()
        {
            decimal tong = 0; if (cart != null) foreach (DataRow row in cart.Rows) tong += Convert.ToDecimal(row["ThanhTien"]);
            lblStatus.Text = (readonlyToa ? "Chỉ xem • " : "Toa đang nhập • ") + (cart == null ? 0 : cart.Rows.Count) + " dòng • Tiền thuốc: " + FormHelper.Money(tong);
            if (readonlyToa && cart != null && cart.Rows.Count == 0) lblStatus.Text = "Chọn phiếu Hoàn tất khám và chưa thanh toán để lập toa mới.";
        }
        private void LuuToa()
        {
            if (readonlyToa) throw new InvalidOperationException("Toa hiện tại chỉ được xem.");
            var lines = new List<DongThuoc>();
            foreach (DataRow row in cart.Rows) lines.Add(new DongThuoc { MaThuoc = Convert.ToInt32(row["MaThuoc"]), SoLuong = Convert.ToInt32(row["SoLuong"]), LieuDung = row["LieuDung"].ToString(), CachDung = row["CachDung"].ToString() });
            int toa = service.LapToa(maPhieu, txtLoiDan.Text, lines);
            readonlyToa = true; int id = maPhieu; maPhieu = 0; cbPhieu.SelectedValue = id; ChonPhieu();
            FormHelper.Success(this, "Đã lưu toa " + toa + " và toàn bộ chi tiết thuốc.");
        }
    }
}
