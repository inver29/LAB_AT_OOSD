using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public class FrmHoSoHopDong : Form
    {
        private readonly HoSoHopDongService service = new HoSoHopDongService();
        private readonly DanhMucService danhMucService = new DanhMucService();
        private TabControl tabControl;

        // Tab 1 Controls
        private DataGridView dgvHoSo;
        private TextBox txtSearch;
        private ComboBox cboLocTrangThai;
        private TextBox txtHoTen, txtDienThoai, txtSoQDTD, txtNoiDungTD;
        private DateTimePicker dtpNgaySinh, dtpNgayQD, dtpTuNgayTV, dtpDenNgayTV;
        private ComboBox cboGioiTinh, cboPhongBan;
        private NumericUpDown numLuongTV;
        private Button btnNhapMoi, btnTiepNhan;
        private bool loadingHoSo = false;

        // Tab 2 Controls
        private DataGridView dgvDanhGia;
        private Label lblNVChonDanhGia;
        private TextBox txtTenNVDanhGia, txtHanTVDanhGia;
        private DateTimePicker dtpNgayDanhGia;
        private Button btnDat, btnKhongDat;
        private int selectedMaNVDanhGia = 0;

        // Tab 3 Controls
        private DataGridView dgvChoKyHD;
        private Label lblNVChonKyHD;
        private TextBox txtSoHD, txtNoiDungHD, txtSoBHXH, txtSoBHYT, txtSoQDLuong;
        private ComboBox cboLoaiHD;
        private DateTimePicker dtpNgayKyHD, dtpTuNgayHD, dtpDenNgayHD;
        private NumericUpDown numTyLeBHXH, numTyLeBHYT, numLuongCoBan, numPCLuong;
        private Button btnKyHD;
        private int selectedMaNVHD = 0;
        private DateTime? selectedDenNgayThuViec = null;

        public FrmHoSoHopDong()
        {
            InitializeComponent();
            FormHelper.Style(this);
            Shown += (s, e) => FormHelper.Run(this, LoadData);
        }

        private void InitializeComponent()
        {
            this.Text = "Hồ sơ, Thử việc & Hợp đồng";
            this.ClientSize = new Size(1200, 790);
            this.MinimumSize = new Size(1050, 680);

            var root = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 12, 20, 15),
                ColumnCount = 1,
                RowCount = 2
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var lblTitle = new Label {
                Text = "Quản lý Hồ sơ, Thử việc, Hợp đồng & Bảo hiểm",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 65, 94),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            root.Controls.Add(lblTitle, 0, 0);

            tabControl = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };

            var tab1 = new TabPage("Hồ sơ & Tuyển dụng");
            InitTabHoSo(tab1);
            tabControl.TabPages.Add(tab1);

            var tab2 = new TabPage("Đánh giá Thử việc");
            InitTabDanhGia(tab2);
            tabControl.TabPages.Add(tab2);

            var tab3 = new TabPage("Ký Hợp đồng & Bảo hiểm");
            InitTabKyHopDong(tab3);
            tabControl.TabPages.Add(tab3);

            root.Controls.Add(tabControl, 0, 1);
            this.Controls.Add(root);
        }

        private void InitTabHoSo(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 5
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));  // Search
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F)); // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Search Bar
            var pnlSearch = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            pnlSearch.Controls.Add(new Label { Text = "Tìm kiếm:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            txtSearch = new TextBox { Width = 220, Margin = new Padding(0, 4, 15, 0) };
            txtSearch.TextChanged += (s, e) => LoadHoSoList();
            pnlSearch.Controls.Add(txtSearch);

            pnlSearch.Controls.Add(new Label { Text = "Trạng thái:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            cboLocTrangThai = new ComboBox { Width = 160, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(0, 4, 15, 0) };
            cboLocTrangThai.Items.AddRange(new object[] { "Tất cả", "Thử việc", "Chờ hợp đồng", "Chính thức", "Kết thúc thử việc" });
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndexChanged += (s, e) => LoadHoSoList();
            pnlSearch.Controls.Add(cboLocTrangThai);

            var btnLamMoi = new Button { Text = "Làm mới", Width = 110, Margin = new Padding(0, 2, 10, 0) };
            btnLamMoi.Click += (s, e) => FormHelper.Run(this, LoadHoSoList);
            pnlSearch.Controls.Add(btnLamMoi);
            layout.Controls.Add(pnlSearch, 0, 0);

            // 2. DataGridView
            dgvHoSo = new DataGridView { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 8) };
            dgvHoSo.SelectionChanged += (s, e) => {
                if (loadingHoSo) return;
                var row = FormHelper.Row(dgvHoSo);
                if (row != null)
                {
                    txtHoTen.Text = row["HoTen"]?.ToString();
                    if (row["NgaySinh"] != DBNull.Value) dtpNgaySinh.Value = Convert.ToDateTime(row["NgaySinh"]);
                    cboGioiTinh.Text = row["GioiTinh"]?.ToString() ?? "Nam";
                    txtDienThoai.Text = row["DienThoai"]?.ToString();
                    txtSoQDTD.Text = row["SoQDTuyenDung"]?.ToString();
                    if (row["MaPhongBan"] != DBNull.Value) cboPhongBan.SelectedValue = row["MaPhongBan"];
                    if (row["LuongThuViec"] != DBNull.Value) numLuongTV.Value = Convert.ToDecimal(row["LuongThuViec"]);
                    if (row["ThuViecTuNgay"] != DBNull.Value) dtpTuNgayTV.Value = Convert.ToDateTime(row["ThuViecTuNgay"]);
                    if (row["ThuViecDenNgay"] != DBNull.Value) dtpDenNgayTV.Value = Convert.ToDateTime(row["ThuViecDenNgay"]);
                }
            };
            layout.Controls.Add(dgvHoSo, 0, 1);

            // 3. Editor Panel (TableLayoutPanel: 4 columns x 3 rows)
            var editor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 3,
                Margin = new Padding(0, 4, 0, 4)
            };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));

            // Controls
            txtHoTen = new TextBox();
            dtpNgaySinh = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = new DateTime(1995, 1, 1) };
            cboGioiTinh = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboGioiTinh.SelectedIndex = 0;
            txtDienThoai = new TextBox();

            txtSoQDTD = new TextBox();
            cboPhongBan = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            dtpNgayQD = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            numLuongTV = new NumericUpDown { Maximum = 500000000, Increment = 500000, Value = 7000000, ThousandsSeparator = true };

            dtpTuNgayTV = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            dtpDenNgayTV = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddMonths(2) };
            txtNoiDungTD = new TextBox { Text = "Tuyển dụng nhân sự thử việc 2 tháng" };

            // Row 0
            editor.Controls.Add(FormHelper.CreateField("Họ và tên *", txtHoTen), 0, 0);
            editor.Controls.Add(FormHelper.CreateField("Ngày sinh *", dtpNgaySinh), 1, 0);
            editor.Controls.Add(FormHelper.CreateField("Giới tính", cboGioiTinh), 2, 0);
            editor.Controls.Add(FormHelper.CreateField("Điện thoại *", txtDienThoai), 3, 0);

            // Row 1
            editor.Controls.Add(FormHelper.CreateField("Số QĐ tuyển dụng *", txtSoQDTD), 0, 1);
            editor.Controls.Add(FormHelper.CreateField("Phòng ban *", cboPhongBan), 1, 1);
            editor.Controls.Add(FormHelper.CreateField("Ngày QĐ *", dtpNgayQD), 2, 1);
            editor.Controls.Add(FormHelper.CreateField("Lương thử việc (đ) *", numLuongTV), 3, 1);

            // Row 2
            editor.Controls.Add(FormHelper.CreateField("Thử việc từ ngày *", dtpTuNgayTV), 0, 2);
            editor.Controls.Add(FormHelper.CreateField("Thử việc đến ngày *", dtpDenNgayTV), 1, 2);
            var fldNoiDung = FormHelper.CreateField("Nội dung tuyển dụng", txtNoiDungTD);
            editor.Controls.Add(fldNoiDung, 2, 2);
            editor.SetColumnSpan(fldNoiDung, 2);

            layout.Controls.Add(editor, 0, 2);

            // 4. Action Buttons
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            btnNhapMoi = new Button { Text = "Nhập mới", Width = 120, Margin = new Padding(0, 4, 10, 0) };
            btnNhapMoi.Click += (s, e) => {
                txtHoTen.Clear();
                txtDienThoai.Clear();
                txtSoQDTD.Clear();
                txtNoiDungTD.Text = "Tuyển dụng nhân sự thử việc 2 tháng";
                dtpTuNgayTV.Value = DateTime.Today;
                dtpDenNgayTV.Value = DateTime.Today.AddMonths(2);
                txtHoTen.Focus();
            };
            actions.Controls.Add(btnNhapMoi);

            btnTiepNhan = new Button { Text = "Tiếp nhận thử việc mới", Width = 220, Margin = new Padding(0, 4, 10, 0) };
            btnTiepNhan.Click += (s, e) => FormHelper.Run(this, () => {
                if (cboPhongBan.SelectedValue == null) throw new InvalidOperationException("Vui lòng chọn phòng ban.");
                int maPB = Convert.ToInt32(cboPhongBan.SelectedValue);
                service.TiepNhanThuViec(
                    txtHoTen.Text, dtpNgaySinh.Value, cboGioiTinh.Text, txtDienThoai.Text,
                    txtSoQDTD.Text, maPB, dtpNgayQD.Value, dtpTuNgayTV.Value, dtpDenNgayTV.Value,
                    numLuongTV.Value, txtNoiDungTD.Text);

                FormHelper.Success(this, "Tiếp nhận nhân viên thử việc mới thành công!");
                btnNhapMoi.PerformClick();
                LoadData();
            });
            actions.Controls.Add(btnTiepNhan);
            layout.Controls.Add(actions, 0, 3);

            // 5. Status text
            var lblStatus = new Label {
                Text = "Chọn nhân viên trong danh sách để xem chi tiết hoặc bấm 'Nhập mới' để tiếp nhận nhân viên thử việc.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 4);

            tab.Controls.Add(layout);
        }

        private void InitTabDanhGia(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 5
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));  // Header
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));  // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Header
            var pnlTop = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            lblNVChonDanhGia = new Label {
                Text = "Vui lòng chọn nhân viên trong danh sách dưới đây để ghi nhận kết quả đánh giá thử việc.",
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 0),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = FormHelper.Primary,
                UseMnemonic = false
            };
            pnlTop.Controls.Add(lblNVChonDanhGia);
            layout.Controls.Add(pnlTop, 0, 0);

            // 2. DataGridView
            dgvDanhGia = new DataGridView { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 8) };
            dgvDanhGia.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvDanhGia);
                if (row != null) {
                    selectedMaNVDanhGia = Convert.ToInt32(row["MaNV"]);
                    string hoTen = row["HoTen"].ToString();
                    DateTime denNgay = Convert.ToDateTime(row["ThuViecDenNgay"]);
                    string ketQua = row["KetQuaThuViec"]?.ToString();
                    lblNVChonDanhGia.Text = $"Đang chọn: [{selectedMaNVDanhGia}] {hoTen} • Hạn thử việc: {denNgay:dd/MM/yyyy} • KQ: {(string.IsNullOrEmpty(ketQua) ? "Chưa đánh giá" : ketQua)}";
                    txtTenNVDanhGia.Text = $"[{selectedMaNVDanhGia}] {hoTen}";
                    txtHanTVDanhGia.Text = denNgay.ToString("dd/MM/yyyy");
                    dtpNgayDanhGia.Value = DateTime.Today;
                }
            };
            layout.Controls.Add(dgvDanhGia, 0, 1);

            // 3. Editor Panel
            var editor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                Margin = new Padding(0, 4, 0, 4)
            };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            txtTenNVDanhGia = new TextBox { ReadOnly = true };
            txtHanTVDanhGia = new TextBox { ReadOnly = true };
            dtpNgayDanhGia = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };

            editor.Controls.Add(FormHelper.CreateField("Nhân viên đang chọn", txtTenNVDanhGia), 0, 0);
            editor.Controls.Add(FormHelper.CreateField("Hạn thử việc", txtHanTVDanhGia), 1, 0);
            editor.Controls.Add(FormHelper.CreateField("Ngày đánh giá *", dtpNgayDanhGia), 2, 0);
            layout.Controls.Add(editor, 0, 2);

            // 4. Action Buttons
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            btnDat = new Button { Text = "Xác nhận: ĐẠT (Chờ ký HĐ)", Width = 240, Margin = new Padding(0, 4, 12, 0) };
            btnDat.Click += (s, e) => {
                if (selectedMaNVDanhGia <= 0) { MessageBox.Show(this, "Vui lòng chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                FormHelper.Run(this, () => {
                    service.DanhGiaThuViec(selectedMaNVDanhGia, "Đạt", dtpNgayDanhGia.Value);
                    FormHelper.Success(this, "Ghi nhận kết quả ĐẠT thành công! Nhân viên chuyển sang trạng thái 'Chờ hợp đồng'.");
                    LoadData();
                });
            };
            actions.Controls.Add(btnDat);

            btnKhongDat = new Button { Text = "Xác nhận: KHÔNG ĐẠT", Width = 210, Margin = new Padding(0, 4, 12, 0), BackColor = Color.FromArgb(192, 57, 43) };
            btnKhongDat.Click += (s, e) => {
                if (selectedMaNVDanhGia <= 0) { MessageBox.Show(this, "Vui lòng chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                FormHelper.Run(this, () => {
                    service.DanhGiaThuViec(selectedMaNVDanhGia, "Không đạt", dtpNgayDanhGia.Value);
                    FormHelper.Success(this, "Ghi nhận kết quả KHÔNG ĐẠT. Nhân viên chuyển sang trạng thái 'Kết thúc thử việc'.");
                    LoadData();
                });
            };
            actions.Controls.Add(btnKhongDat);
            layout.Controls.Add(actions, 0, 3);

            // 5. Status
            var lblStatus = new Label {
                Text = "Chọn nhân viên trong danh sách để ghi nhận kết quả đánh giá thử việc. Kết quả Đạt mới được ký hợp đồng.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 4);

            tab.Controls.Add(layout);
        }

        private void InitTabKyHopDong(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 5
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));  // Header
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F)); // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Header
            var pnlTop = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            lblNVChonKyHD = new Label {
                Text = "Vui lòng chọn nhân viên ĐẠT thử việc trong danh sách dưới đây để ký Hợp đồng lao động & Thiết lập hồ sơ.",
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 0),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = FormHelper.Primary,
                UseMnemonic = false
            };
            pnlTop.Controls.Add(lblNVChonKyHD);
            layout.Controls.Add(pnlTop, 0, 0);

            // 2. DataGridView
            dgvChoKyHD = new DataGridView { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 8) };
            dgvChoKyHD.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvChoKyHD);
                if (row != null) {
                    selectedMaNVHD = Convert.ToInt32(row["MaNV"]);
                    string hoTen = row["HoTen"].ToString();
                    selectedDenNgayThuViec = Convert.ToDateTime(row["ThuViecDenNgay"]);
                    lblNVChonKyHD.Text = $"Đang chọn: [{selectedMaNVHD}] {hoTen} • Hết hạn TV: {selectedDenNgayThuViec:dd/MM/yyyy} (ĐẠT)";

                    dtpNgayKyHD.Value = selectedDenNgayThuViec.Value;
                    dtpTuNgayHD.Value = selectedDenNgayThuViec.Value.AddDays(1);
                    txtSoHD.Text = $"HD-{DateTime.Today.Year}-{selectedMaNVHD:D3}";
                    txtSoQDLuong.Text = $"QDL-{DateTime.Today.Year}-{selectedMaNVHD:D3}";
                    txtSoBHXH.Text = $"BHXH{selectedMaNVHD:D6}";
                    txtSoBHYT.Text = $"BHYT{selectedMaNVHD:D8}";
                }
            };
            layout.Controls.Add(dgvChoKyHD, 0, 1);

            // 3. Editor Panel (TableLayoutPanel: 4 columns x 3 rows)
            var editor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 3,
                Margin = new Padding(0, 4, 0, 4)
            };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));

            txtSoHD = new TextBox();
            cboLoaiHD = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiHD.Items.AddRange(new object[] { "Dài hạn", "Ngắn hạn" });
            cboLoaiHD.SelectedIndex = 0;
            cboLoaiHD.SelectedIndexChanged += (s, e) => {
                bool isDaiHan = cboLoaiHD.Text == "Dài hạn";
                txtSoBHXH.Enabled = isDaiHan;
                txtSoBHYT.Enabled = isDaiHan;
                numTyLeBHXH.Enabled = isDaiHan;
                numTyLeBHYT.Enabled = isDaiHan;
                dtpDenNgayHD.Enabled = !isDaiHan;
            };

            dtpNgayKyHD = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            dtpTuNgayHD = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            dtpDenNgayHD = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddYears(1), Enabled = false };

            txtSoBHXH = new TextBox();
            txtSoBHYT = new TextBox();
            txtSoQDLuong = new TextBox();

            numLuongCoBan = new NumericUpDown { Maximum = 500000000, Increment = 500000, Value = 10000000, ThousandsSeparator = true };
            numPCLuong = new NumericUpDown { Maximum = 100000000, Increment = 100000, Value = 1000000, ThousandsSeparator = true };
            numTyLeBHXH = new NumericUpDown { DecimalPlaces = 3, Increment = 0.005m, Value = 0.080m };
            numTyLeBHYT = new NumericUpDown { DecimalPlaces = 3, Increment = 0.005m, Value = 0.015m };
            txtNoiDungHD = new TextBox { Text = "Hợp đồng lao động chính thức theo quy định công ty" };

            // Row 0
            editor.Controls.Add(FormHelper.CreateField("Số Hợp đồng *", txtSoHD), 0, 0);
            editor.Controls.Add(FormHelper.CreateField("Loại hợp đồng *", cboLoaiHD), 1, 0);
            editor.Controls.Add(FormHelper.CreateField("Ngày ký HĐ *", dtpNgayKyHD), 2, 0);
            editor.Controls.Add(FormHelper.CreateField("Hiệu lực từ ngày *", dtpTuNgayHD), 3, 0);

            // Row 1
            editor.Controls.Add(FormHelper.CreateField("Đến ngày (Ngắn hạn)", dtpDenNgayHD), 0, 1);
            editor.Controls.Add(FormHelper.CreateField("Số sổ BHXH (Dài hạn) *", txtSoBHXH), 1, 1);
            editor.Controls.Add(FormHelper.CreateField("Số thẻ BHYT (Dài hạn) *", txtSoBHYT), 2, 1);
            editor.Controls.Add(FormHelper.CreateField("Số QĐ Lương chính thức *", txtSoQDLuong), 3, 1);

            // Row 2
            editor.Controls.Add(FormHelper.CreateField("Lương cơ bản (đ) *", numLuongCoBan), 0, 2);
            editor.Controls.Add(FormHelper.CreateField("Phụ cấp lương (đ)", numPCLuong), 1, 2);
            editor.Controls.Add(FormHelper.CreateField("Tỷ lệ BHXH NLĐ (0.080)", numTyLeBHXH), 2, 2);
            editor.Controls.Add(FormHelper.CreateField("Tỷ lệ BHYT NLĐ (0.015)", numTyLeBHYT), 3, 2);

            layout.Controls.Add(editor, 0, 2);

            // 4. Action Buttons
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            btnKyHD = new Button { Text = "KÝ HỢP ĐỒNG & THIẾT LẬP HỒ SƠ CHÍNH THỨC", Width = 360, Margin = new Padding(0, 4, 12, 0) };
            btnKyHD.Click += (s, e) => {
                if (selectedMaNVHD <= 0) { MessageBox.Show(this, "Vui lòng chọn nhân viên cần ký hợp đồng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                FormHelper.Run(this, () => {
                    DateTime? denNgay = cboLoaiHD.Text == "Ngắn hạn" ? (DateTime?)dtpDenNgayHD.Value : null;
                    service.KyHopDong(
                        selectedMaNVHD, txtSoHD.Text, cboLoaiHD.Text, dtpNgayKyHD.Value, dtpTuNgayHD.Value, denNgay,
                        txtNoiDungHD.Text, txtSoBHXH.Text, txtSoBHYT.Text, numTyLeBHXH.Value, numTyLeBHYT.Value,
                        txtSoQDLuong.Text, numLuongCoBan.Value, numPCLuong.Value, 0);

                    FormHelper.Success(this, "Ký hợp đồng và thiết lập hồ sơ thành công! Trạng thái nhân viên chuyển sang 'Chính thức'.");
                    LoadData();
                });
            };
            actions.Controls.Add(btnKyHD);
            layout.Controls.Add(actions, 0, 3);

            // 5. Status
            var lblStatus = new Label {
                Text = "Chọn nhân viên Đạt thử việc để ký hợp đồng và thiết lập hồ sơ lao động chính thức.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 4);

            tab.Controls.Add(layout);
        }

        private void LoadData()
        {
            var dtPB = danhMucService.GetAllPhongBan();
            FormHelper.Bind(cboPhongBan, dtPB, "MaPhongBan", "TenPhongBan");

            LoadHoSoList();
            LoadDanhGiaList();
            LoadChoKyHDList();
        }

        private void LoadHoSoList()
        {
            loadingHoSo = true;
            try
            {
                string search = txtSearch?.Text;
                string trangThai = cboLocTrangThai?.SelectedItem?.ToString();
                dgvHoSo.DataSource = service.GetAllNhanVien(search, trangThai);
                FormHelper.Columns(dgvHoSo,
                    "MaNV", "Mã NV",
                    "HoTen", "Họ và tên",
                    "TenPhongBan", "Phòng ban",
                    "TrangThai", "Trạng thái",
                    "DienThoai", "Điện thoại",
                    "LuongThuViec", "Lương TV");
            }
            finally
            {
                loadingHoSo = false;
            }
        }

        private void LoadDanhGiaList()
        {
            dgvDanhGia.DataSource = service.GetAllNhanVien(null, "Tất cả");
            FormHelper.Columns(dgvDanhGia,
                "MaNV", "Mã NV",
                "HoTen", "Họ và tên",
                "TenPhongBan", "Phòng ban",
                "ThuViecTuNgay", "Thử việc từ",
                "ThuViecDenNgay", "Hết hạn TV",
                "KetQuaThuViec", "Kết quả",
                "TrangThai", "Trạng thái");
        }

        private void LoadChoKyHDList()
        {
            dgvChoKyHD.DataSource = service.GetAllNhanVien(null, "Chờ hợp đồng");
            FormHelper.Columns(dgvChoKyHD,
                "MaNV", "Mã NV",
                "HoTen", "Họ và tên",
                "TenPhongBan", "Phòng ban",
                "ThuViecDenNgay", "Hết hạn TV",
                "KetQuaThuViec", "Kết quả TV",
                "TrangThai", "Trạng thái");
        }
    }
}
