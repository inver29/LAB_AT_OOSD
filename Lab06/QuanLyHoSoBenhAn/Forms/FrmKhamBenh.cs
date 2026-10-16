using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmKhamBenh : Form
    {
        private readonly KhamBenhService service = new KhamBenhService();
        private int maPhieu;
        private bool loading;
        public FrmKhamBenh()
        {
            InitializeComponent(); FormHelper.Style(this);
            txtThanNhiet.MaxLength = 5; txtNhipTim.MaxLength = 5; txtHuyetAp.MaxLength = 15;
            numTienKham.Value = 100000;
            Shown += (s, e) => FormHelper.Run(this, () => { LoadDanhMuc(); LoadData(); });
            btnSearch.Click += (s, e) => FormHelper.Run(this, LoadData);
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; FormHelper.Run(this, LoadData); } };
            btnReload.Click += (s, e) => FormHelper.Run(this, () => { txtSearch.Clear(); LoadDanhMuc(); LoadData(); });
            gridKham.SelectionChanged += (s, e) => { if (!loading) FormHelper.Run(this, ChonPhieu); };
            btnNew.Click += (s, e) => NhapMoi();
            btnTao.Click += (s, e) => FormHelper.Run(this, () =>
            {
                int id = service.TaoPhieu(FormHelper.Id(cbHoSo), FormHelper.Id(cbBacSi), FormHelper.Id(cbKhoa), numTienKham.Value);
                txtSearch.Clear(); LoadData(); FormHelper.SelectId(gridKham, "MaPhieuKham", id);
                FormHelper.Success(this, "Đã tạo phiếu " + id + ". Nhập thông tin và lưu kết quả khám.");
            });
            btnLuu.Click += (s, e) => FormHelper.Run(this, () => Luu(false));
            btnHoanTat.Click += (s, e) => FormHelper.Run(this, () => Luu(true));
        }
        private void LoadDanhMuc()
        {
            var dm = new DanhMucService();
            FormHelper.Bind(cbHoSo, new HoSoService().ChonHoSo(), "MaHSBA", "HienThi");
            FormHelper.Bind(cbBacSi, dm.BacSi(), "MaNV", "HoTen"); FormHelper.Bind(cbKhoa, dm.Khoa(), "MaKhoa", "TenKhoa");
        }
        private void LoadData()
        {
            loading = true;
            try
            {
                gridKham.DataSource = service.DanhSach(txtSearch.Text);
                FormHelper.Columns(gridKham, "MaPhieuKham", "Mã phiếu", "HoTen", "Bệnh nhân", "BacSi", "Bác sĩ", "NgayKham", "Ngày khám", "TrangThai", "Trạng thái", "TienKham", "Tiền khám");
            }
            finally { loading = false; }
            if (gridKham.Rows.Count > 0) ChonPhieu(); else NhapMoi();
        }
        private void ChonPhieu()
        {
            DataRow row = FormHelper.Row(gridKham); if (row == null) return;
            maPhieu = Convert.ToInt32(row["MaPhieuKham"]);
            cbHoSo.SelectedValue = row["MaHSBA"]; cbBacSi.SelectedValue = row["MaBacSi"]; cbKhoa.SelectedValue = row["MaKhoa"];
            numTienKham.Value = Math.Min(numTienKham.Maximum, Convert.ToDecimal(row["TienKham"]));
            txtTrieuChung.Text = row["TrieuChung"].ToString(); txtChanDoan.Text = row["ChanDoan"].ToString();
            txtThanNhiet.Text = row["ThanNhiet"].ToString(); txtNhipTim.Text = row["NhipTim"].ToString(); txtHuyetAp.Text = row["HuyetAp"].ToString();
            bool writable = row["TrangThai"].ToString() != "Đã thanh toán";
            SetMode(false, writable);
            lblStatus.Text = "Phiếu " + maPhieu + " • " + row["TrangThai"] + (writable ? " • Cập nhật thông tin rồi lưu / hoàn tất khám." : " • Chỉ xem thông tin.");
        }
        private void SetMode(bool isNew, bool writable)
        {
            cbHoSo.Enabled = cbBacSi.Enabled = cbKhoa.Enabled = numTienKham.Enabled = isNew;
            txtTrieuChung.ReadOnly = txtChanDoan.ReadOnly = txtThanNhiet.ReadOnly = txtNhipTim.ReadOnly = txtHuyetAp.ReadOnly = isNew || !writable;
            btnTao.Enabled = isNew; btnLuu.Enabled = btnHoanTat.Enabled = !isNew && writable;
        }
        private void NhapMoi()
        {
            loading = true; gridKham.ClearSelection(); gridKham.CurrentCell = null; loading = false;
            maPhieu = 0; txtTrieuChung.Clear(); txtChanDoan.Clear(); txtThanNhiet.Clear(); txtNhipTim.Clear(); txtHuyetAp.Clear();
            numTienKham.Value = 100000; SetMode(true, true);
            lblStatus.Text = "Chọn hồ sơ, bác sĩ, khoa và tiền khám điều trị, sau đó bấm Tạo phiếu.";
        }
        private void Luu(bool hoanTat)
        {
            if (maPhieu == 0) throw new InvalidOperationException("Hãy chọn phiếu khám cần cập nhật.");
            decimal? nhiet = null; short? nhip = null;
            if (!string.IsNullOrWhiteSpace(txtThanNhiet.Text))
            {
                decimal d;
                if (!decimal.TryParse(txtThanNhiet.Text.Trim().Replace(',', '.'), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out d))
                    throw new InvalidOperationException("Thân nhiệt không hợp lệ (ví dụ: 36,5).");
                nhiet = d;
            }
            if (!string.IsNullOrWhiteSpace(txtNhipTim.Text))
            { short n; if (!short.TryParse(txtNhipTim.Text.Trim(), out n)) throw new InvalidOperationException("Nhịp tim phải là số nguyên dương."); nhip = n; }
            int id = maPhieu;
            service.LuuKham(id, new KhamInput { TrieuChung = txtTrieuChung.Text, ChanDoan = txtChanDoan.Text, ThanNhiet = nhiet, NhipTim = nhip, HuyetAp = txtHuyetAp.Text, HoanTat = hoanTat });
            LoadData(); FormHelper.SelectId(gridKham, "MaPhieuKham", id);
            FormHelper.Success(this, hoanTat ? "Đã hoàn tất khám. Có thể chuyển sang kê toa hoặc thanh toán." : "Đã lưu thông tin khám.");
        }
    }
}
