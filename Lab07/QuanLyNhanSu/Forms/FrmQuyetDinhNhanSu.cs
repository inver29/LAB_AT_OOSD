using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public class FrmQuyetDinhNhanSu : Form
    {
        private readonly QuyetDinhNhanSuService service = new QuyetDinhNhanSuService();
        private readonly DanhMucService danhMucService = new DanhMucService();
        private readonly HoSoHopDongService hoSoService = new HoSoHopDongService();
        private TabControl tabControl;

        // Tab Lương
        private DataGridView dgvQDLuong, dgvLichSuLuong;
        private ComboBox cboNhanVienLuong;
        private TextBox txtSoQDLuong, txtSearchLuong;
        private DateTimePicker dtpNgayQDLuong, dtpTuNgayLuong, dtpDenNgayLuong;
        private NumericUpDown numLuongCoBan, numPCLuong, numPCDocHai;
        private Button btnLuuLuong, btnLamMoiLuong;

        // Tab Chức vụ
        private DataGridView dgvQDChucVu, dgvLichSuChucVu;
        private ComboBox cboNhanVienChucVu, cboChucVu;
        private TextBox txtSoQDChucVu, txtSearchChucVu, txtGhiChuCV;
        private DateTimePicker dtpNgayQDChucVu, dtpTuNgayChucVu, dtpDenNgayChucVu;
        private NumericUpDown numPCChucVu;
        private Button btnLuuChucVu, btnLamMoiCV;

        public FrmQuyetDinhNhanSu()
        {
            InitializeComponent();
            FormHelper.Style(this);
            Shown += (s, e) => FormHelper.Run(this, LoadData);
        }

        private void InitializeComponent()
        {
            this.Text = "Quyết định nhân sự (Lương & Chức vụ)";
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
                Text = "Quản lý Quyết định Lương & Chức vụ",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 65, 94),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            root.Controls.Add(lblTitle, 0, 0);

            tabControl = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };

            var tabLuong = new TabPage("Quyết định Lương");
            InitTabLuong(tabLuong);
            tabControl.TabPages.Add(tabLuong);

            var tabChucVu = new TabPage("Quyết định Chức vụ");
            InitTabChucVu(tabChucVu);
            tabControl.TabPages.Add(tabChucVu);

            root.Controls.Add(tabControl, 0, 1);
            this.Controls.Add(root);
        }

        private void InitTabLuong(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 5
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));  // Search
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // SplitGrids
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 135F)); // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Search Bar
            var pnlSearch = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            pnlSearch.Controls.Add(new Label { Text = "Tìm kiếm:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            txtSearchLuong = new TextBox { Width = 240, Margin = new Padding(0, 4, 15, 0) };
            txtSearchLuong.TextChanged += (s, e) => LoadQDLuongList();
            pnlSearch.Controls.Add(txtSearchLuong);

            var btnReloadL = new Button { Text = "Làm mới", Width = 110, Margin = new Padding(0, 2, 10, 0) };
            btnReloadL.Click += (s, e) => FormHelper.Run(this, LoadQDLuongList);
            pnlSearch.Controls.Add(btnReloadL);
            layout.Controls.Add(pnlSearch, 0, 0);

            // 2. Grids Split
            var splitGrids = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical, SplitterDistance = 620 };

            var grpAll = new GroupBox { Text = " Danh sách tất cả Quyết định Lương ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvQDLuong = new DataGridView { Dock = DockStyle.Fill };
            dgvQDLuong.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvQDLuong);
                if (row != null) {
                    int maNV = Convert.ToInt32(row["MaNV"]);
                    LoadLichSuLuong(maNV);
                    if (cboNhanVienLuong.Items.Count > 0) cboNhanVienLuong.SelectedValue = maNV;
                }
            };
            grpAll.Controls.Add(dgvQDLuong);
            splitGrids.Panel1.Controls.Add(grpAll);

            var grpHist = new GroupBox { Text = " Lịch sử diễn biến lương của nhân viên đang chọn ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvLichSuLuong = new DataGridView { Dock = DockStyle.Fill };
            grpHist.Controls.Add(dgvLichSuLuong);
            splitGrids.Panel2.Controls.Add(grpHist);
            layout.Controls.Add(splitGrids, 0, 1);

            // 3. Editor Panel
            var editor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 2,
                Margin = new Padding(0, 4, 0, 4)
            };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            cboNhanVienLuong = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cboNhanVienLuong.SelectedIndexChanged += (s, e) => {
                if (cboNhanVienLuong.SelectedValue != null && int.TryParse(cboNhanVienLuong.SelectedValue.ToString(), out int maNV)) {
                    LoadLichSuLuong(maNV);
                    txtSoQDLuong.Text = $"QDL-{DateTime.Today.Year}-{maNV:D3}";
                }
            };

            txtSoQDLuong = new TextBox();
            dtpNgayQDLuong = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            dtpTuNgayLuong = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            dtpDenNgayLuong = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddYears(1) };

            numLuongCoBan = new NumericUpDown { Maximum = 500000000, Increment = 500000, Value = 12000000, ThousandsSeparator = true };
            numPCLuong = new NumericUpDown { Maximum = 100000000, Increment = 100000, Value = 1000000, ThousandsSeparator = true };
            numPCDocHai = new NumericUpDown { Maximum = 100000000, Increment = 100000, Value = 0, ThousandsSeparator = true };

            // Row 0
            editor.Controls.Add(FormHelper.CreateField("Nhân viên *", cboNhanVienLuong), 0, 0);
            editor.Controls.Add(FormHelper.CreateField("Số QĐ lương *", txtSoQDLuong), 1, 0);
            editor.Controls.Add(FormHelper.CreateField("Ngày ký QĐ *", dtpNgayQDLuong), 2, 0);
            editor.Controls.Add(FormHelper.CreateField("Hiệu lực từ ngày *", dtpTuNgayLuong), 3, 0);

            // Row 1
            editor.Controls.Add(FormHelper.CreateField("Lương cơ bản (đ) *", numLuongCoBan), 0, 1);
            editor.Controls.Add(FormHelper.CreateField("Phụ cấp lương (đ)", numPCLuong), 1, 1);
            editor.Controls.Add(FormHelper.CreateField("Phụ cấp độc hại (đ)", numPCDocHai), 2, 1);
            editor.Controls.Add(FormHelper.CreateField("Đến ngày (nếu có)", dtpDenNgayLuong), 3, 1);
            layout.Controls.Add(editor, 0, 2);

            // 4. Action Buttons
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            btnLamMoiLuong = new Button { Text = "Nhập mới", Width = 120, Margin = new Padding(0, 4, 10, 0) };
            btnLamMoiLuong.Click += (s, e) => {
                txtSoQDLuong.Clear();
                dtpTuNgayLuong.Value = DateTime.Today;
                dtpDenNgayLuong.Value = DateTime.Today.AddYears(1);
            };
            actions.Controls.Add(btnLamMoiLuong);

            btnLuuLuong = new Button { Text = "Lưu Quyết định Lương mới", Width = 240, Margin = new Padding(0, 4, 10, 0) };
            btnLuuLuong.Click += (s, e) => {
                if (cboNhanVienLuong.SelectedValue == null) { MessageBox.Show(this, "Vui lòng chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                int maNV = Convert.ToInt32(cboNhanVienLuong.SelectedValue);

                FormHelper.Run(this, () => {
                    service.AddQuyetDinhLuong(
                        txtSoQDLuong.Text, maNV, dtpNgayQDLuong.Value, dtpTuNgayLuong.Value, null,
                        numLuongCoBan.Value, numPCLuong.Value, numPCDocHai.Value);

                    FormHelper.Success(this, "Thêm quyết định lương thành công! Lịch sử hiệu lực đã được cập nhật không chồng chéo.");
                    LoadQDLuongList();
                    LoadLichSuLuong(maNV);
                });
            };
            actions.Controls.Add(btnLuuLuong);
            layout.Controls.Add(actions, 0, 3);

            // 5. Status
            var lblStatus = new Label {
                Text = "Lịch sử lương có hiệu lực liên tục, hệ thống tự động kết thúc quyết định cũ khi áp dụng quyết định mới.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 4);

            tab.Controls.Add(layout);
        }

        private void InitTabChucVu(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 5
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));  // Search
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // SplitGrids
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 135F)); // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Search Bar
            var pnlSearch = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            pnlSearch.Controls.Add(new Label { Text = "Tìm kiếm:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            txtSearchChucVu = new TextBox { Width = 240, Margin = new Padding(0, 4, 15, 0) };
            txtSearchChucVu.TextChanged += (s, e) => LoadQDChucVuList();
            pnlSearch.Controls.Add(txtSearchChucVu);

            var btnReloadCV = new Button { Text = "Làm mới", Width = 110, Margin = new Padding(0, 2, 10, 0) };
            btnReloadCV.Click += (s, e) => FormHelper.Run(this, LoadQDChucVuList);
            pnlSearch.Controls.Add(btnReloadCV);
            layout.Controls.Add(pnlSearch, 0, 0);

            // 2. Grids Split
            var splitGrids = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical, SplitterDistance = 620 };

            var grpAll = new GroupBox { Text = " Danh sách tất cả Quyết định Bổ nhiệm Chức vụ ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvQDChucVu = new DataGridView { Dock = DockStyle.Fill };
            dgvQDChucVu.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvQDChucVu);
                if (row != null) {
                    int maNV = Convert.ToInt32(row["MaNV"]);
                    LoadLichSuChucVu(maNV);
                    if (cboNhanVienChucVu.Items.Count > 0) cboNhanVienChucVu.SelectedValue = maNV;
                }
            };
            grpAll.Controls.Add(dgvQDChucVu);
            splitGrids.Panel1.Controls.Add(grpAll);

            var grpHist = new GroupBox { Text = " Lịch sử bổ nhiệm chức vụ của nhân viên đang chọn ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvLichSuChucVu = new DataGridView { Dock = DockStyle.Fill };
            grpHist.Controls.Add(dgvLichSuChucVu);
            splitGrids.Panel2.Controls.Add(grpHist);
            layout.Controls.Add(splitGrids, 0, 1);

            // 3. Editor Panel
            var editor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 2,
                Margin = new Padding(0, 4, 0, 4)
            };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            cboNhanVienChucVu = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cboNhanVienChucVu.SelectedIndexChanged += (s, e) => {
                if (cboNhanVienChucVu.SelectedValue != null && int.TryParse(cboNhanVienChucVu.SelectedValue.ToString(), out int maNV)) {
                    LoadLichSuChucVu(maNV);
                    txtSoQDChucVu.Text = $"QDCV-{DateTime.Today.Year}-{maNV:D3}";
                }
            };

            cboChucVu = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            txtSoQDChucVu = new TextBox();
            dtpNgayQDChucVu = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            dtpTuNgayChucVu = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            dtpDenNgayChucVu = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddYears(1) };
            numPCChucVu = new NumericUpDown { Maximum = 100000000, Increment = 100000, Value = 1500000, ThousandsSeparator = true };
            txtGhiChuCV = new TextBox { Text = "Bổ nhiệm chức vụ mới" };

            // Row 0
            editor.Controls.Add(FormHelper.CreateField("Nhân viên *", cboNhanVienChucVu), 0, 0);
            editor.Controls.Add(FormHelper.CreateField("Chức vụ *", cboChucVu), 1, 0);
            editor.Controls.Add(FormHelper.CreateField("Số QĐ chức vụ *", txtSoQDChucVu), 2, 0);
            editor.Controls.Add(FormHelper.CreateField("Ngày ký QĐ *", dtpNgayQDChucVu), 3, 0);

            // Row 1
            editor.Controls.Add(FormHelper.CreateField("Hiệu lực từ ngày *", dtpTuNgayChucVu), 0, 1);
            editor.Controls.Add(FormHelper.CreateField("Phụ cấp chức vụ (đ)", numPCChucVu), 1, 1);
            editor.Controls.Add(FormHelper.CreateField("Đến ngày (nếu có)", dtpDenNgayChucVu), 2, 1);
            editor.Controls.Add(FormHelper.CreateField("Ghi chú quyết định", txtGhiChuCV), 3, 1);
            layout.Controls.Add(editor, 0, 2);

            // 4. Action Buttons
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            btnLamMoiCV = new Button { Text = "Nhập mới", Width = 120, Margin = new Padding(0, 4, 10, 0) };
            btnLamMoiCV.Click += (s, e) => {
                txtSoQDChucVu.Clear();
                dtpTuNgayChucVu.Value = DateTime.Today;
                dtpDenNgayChucVu.Value = DateTime.Today.AddYears(1);
            };
            actions.Controls.Add(btnLamMoiCV);

            btnLuuChucVu = new Button { Text = "Lưu Quyết định Bổ nhiệm mới", Width = 260, Margin = new Padding(0, 4, 10, 0) };
            btnLuuChucVu.Click += (s, e) => {
                if (cboNhanVienChucVu.SelectedValue == null) { MessageBox.Show(this, "Vui lòng chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (cboChucVu.SelectedValue == null) { MessageBox.Show(this, "Vui lòng chọn chức vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                int maNV = Convert.ToInt32(cboNhanVienChucVu.SelectedValue);
                int maCV = Convert.ToInt32(cboChucVu.SelectedValue);

                FormHelper.Run(this, () => {
                    service.AddQuyetDinhChucVu(
                        txtSoQDChucVu.Text, maNV, maCV, dtpNgayQDChucVu.Value, dtpTuNgayChucVu.Value, null, numPCChucVu.Value);

                    FormHelper.Success(this, "Thêm quyết định bổ nhiệm chức vụ thành công! Lịch sử bổ nhiệm đã được cập nhật.");
                    LoadQDChucVuList();
                    LoadLichSuChucVu(maNV);
                });
            };
            actions.Controls.Add(btnLuuChucVu);
            layout.Controls.Add(actions, 0, 3);

            // 5. Status
            var lblStatus = new Label {
                Text = "Nhân viên giữ một chức vụ chính thức tại một thời điểm, tự động cập nhật lịch sử bổ nhiệm.",
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
            var dtNV = hoSoService.GetAllNhanVien(null, "Tất cả");
            FormHelper.Bind(cboNhanVienLuong, dtNV, "MaNV", "HoTen");
            FormHelper.Bind(cboNhanVienChucVu, dtNV, "MaNV", "HoTen");

            var dtCV = danhMucService.GetAllChucVu();
            FormHelper.Bind(cboChucVu, dtCV, "MaChucVu", "TenChucVu");

            LoadQDLuongList();
            LoadQDChucVuList();

            if (cboNhanVienLuong.SelectedValue != null && int.TryParse(cboNhanVienLuong.SelectedValue.ToString(), out int maNV))
                LoadLichSuLuong(maNV);

            if (cboNhanVienChucVu.SelectedValue != null && int.TryParse(cboNhanVienChucVu.SelectedValue.ToString(), out int maNVCV))
                LoadLichSuChucVu(maNVCV);
        }

        private void LoadQDLuongList()
        {
            dgvQDLuong.DataSource = service.GetAllQuyetDinhLuong(txtSearchLuong?.Text);
            FormHelper.Columns(dgvQDLuong,
                "SoQDLuong", "Số QĐ",
                "HoTen", "Nhân viên",
                "TenPhongBan", "Phòng ban",
                "TuNgayLuong", "Từ ngày",
                "LuongCoBan", "Lương CB",
                "TrangThai", "Trạng thái");
        }

        private void LoadLichSuLuong(int maNV)
        {
            dgvLichSuLuong.DataSource = service.GetLichSuLuongByNhanVien(maNV);
            FormHelper.Columns(dgvLichSuLuong,
                "SoQDLuong", "Số QĐ",
                "TuNgay", "Từ ngày",
                "DenNgay", "Đến ngày",
                "LuongCoBan", "Lương CB",
                "TrangThai", "Trạng thái");
        }

        private void LoadQDChucVuList()
        {
            dgvQDChucVu.DataSource = service.GetAllQuyetDinhChucVu(txtSearchChucVu?.Text);
            FormHelper.Columns(dgvQDChucVu,
                "SoQDChucVu", "Số QĐ",
                "HoTen", "Nhân viên",
                "TenChucVu", "Chức vụ",
                "TuNgayChucVu", "Từ ngày",
                "PhuCapChucVu", "PC Chức vụ",
                "TrangThai", "Trạng thái");
        }

        private void LoadLichSuChucVu(int maNV)
        {
            dgvLichSuChucVu.DataSource = service.GetLichSuChucVuByNhanVien(maNV);
            FormHelper.Columns(dgvLichSuChucVu,
                "SoQDChucVu", "Số QĐ",
                "TenChucVu", "Chức vụ",
                "TuNgay", "Từ ngày",
                "DenNgay", "Đến ngày",
                "PhuCapChucVu", "PC Chức vụ",
                "TrangThai", "Trạng thái");
        }
    }
}
