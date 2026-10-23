using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public class FrmChamCongPhuTroi : Form
    {
        private readonly ChamCongPhuTroiService service = new ChamCongPhuTroiService();
        private readonly DanhMucService danhMucService = new DanhMucService();
        private readonly HoSoHopDongService hoSoService = new HoSoHopDongService();
        private TabControl tabControl;

        // Tab 1: Phân ca
        private ComboBox cboNVPhanCa, cboCaLamViec;
        private DateTimePicker dtpNgayPhanCa, dtpLocPhanCaTu, dtpLocPhanCaDen;
        private DataGridView dgvPhanCa;

        // Tab 2: Chấm công
        private DataGridView dgvChamCong;
        private DateTimePicker dtpChamCongTu, dtpChamCongDen;
        private ComboBox cboPhongBanChamCong;
        private NumericUpDown numGioThucTe;
        private CheckBox chkVangMat, chkChuaCham;
        private TextBox txtCaDangCham;
        private int selectedMaNVCC = 0;
        private DateTime selectedNgayCC = DateTime.Today;
        private int selectedMaCaCC = 0;

        // Tab 3: Phụ trội
        private DataGridView dgvPhieuPT, dgvNVPhuTroi;
        private TextBox txtSoPhieuPT;
        private DateTimePicker dtpNgayPT, dtpGioBatDauPT;
        private NumericUpDown numSoGioPT;
        private ComboBox cboHinhThucPT;
        private CheckedListBox clbNhanVienPT;
        private int selectedMaPhieuPT = 0;

        public FrmChamCongPhuTroi()
        {
            InitializeComponent();
            FormHelper.Style(this);
            Shown += (s, e) => FormHelper.Run(this, LoadData);
        }

        private void InitializeComponent()
        {
            this.Text = "Phân ca, Chấm công & Phụ trội";
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
                Text = "Quản lý Phân ca, Chấm công & Phụ trội",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 65, 94),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            root.Controls.Add(lblTitle, 0, 0);

            tabControl = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };

            var tabPhanCa = new TabPage("Phân ca làm việc");
            InitTabPhanCa(tabPhanCa);
            tabControl.TabPages.Add(tabPhanCa);

            var tabChamCong = new TabPage("Chấm công thực tế");
            InitTabChamCong(tabChamCong);
            tabControl.TabPages.Add(tabChamCong);

            var tabPhuTroi = new TabPage("Phiếu Phụ trội");
            InitTabPhuTroi(tabPhuTroi);
            tabControl.TabPages.Add(tabPhuTroi);

            root.Controls.Add(tabControl, 0, 1);
            this.Controls.Add(root);
        }

        private void InitTabPhanCa(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 5
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));  // Filter
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));  // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Filter bar
            var pnlFilter = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            pnlFilter.Controls.Add(new Label { Text = "Xem từ ngày:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            dtpLocPhanCaTu = new DateTimePicker { Width = 115, Format = DateTimePickerFormat.Short, Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), Margin = new Padding(0, 4, 15, 0) };
            pnlFilter.Controls.Add(dtpLocPhanCaTu);

            pnlFilter.Controls.Add(new Label { Text = "Đến ngày:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            dtpLocPhanCaDen = new DateTimePicker { Width = 115, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddDays(7), Margin = new Padding(0, 4, 15, 0) };
            pnlFilter.Controls.Add(dtpLocPhanCaDen);

            var btnLocPhanCa = new Button { Text = "Tải danh sách", Width = 120, Margin = new Padding(0, 2, 10, 0) };
            btnLocPhanCa.Click += (s, e) => FormHelper.Run(this, LoadPhanCaList);
            pnlFilter.Controls.Add(btnLocPhanCa);
            layout.Controls.Add(pnlFilter, 0, 0);

            // 2. DataGridView
            dgvPhanCa = new DataGridView { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 8) };
            layout.Controls.Add(dgvPhanCa, 0, 1);

            // 3. Editor
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

            cboNVPhanCa = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            dtpNgayPhanCa = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            cboCaLamViec = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };

            editor.Controls.Add(FormHelper.CreateField("Nhân viên *", cboNVPhanCa), 0, 0);
            editor.Controls.Add(FormHelper.CreateField("Ngày phân ca *", dtpNgayPhanCa), 1, 0);
            editor.Controls.Add(FormHelper.CreateField("Ca làm việc *", cboCaLamViec), 2, 0);
            layout.Controls.Add(editor, 0, 2);

            // 4. Actions
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            var btnPhanCa = new Button { Text = "Phân công Ca", Width = 150, Margin = new Padding(0, 4, 10, 0) };
            btnPhanCa.Click += (s, e) => {
                if (cboNVPhanCa.SelectedValue == null) { MessageBox.Show(this, "Vui lòng chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (cboCaLamViec.SelectedValue == null) { MessageBox.Show(this, "Vui lòng chọn ca.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                int maNV = Convert.ToInt32(cboNVPhanCa.SelectedValue);
                int maCa = Convert.ToInt32(cboCaLamViec.SelectedValue);

                FormHelper.Run(this, () => {
                    service.PhanCa(maNV, dtpNgayPhanCa.Value, maCa);
                    FormHelper.Success(this, "Phân ca thành công! Trạng thái chấm công ban đầu là Chưa chấm (NULL).");
                    LoadPhanCaList();
                });
            };
            actions.Controls.Add(btnPhanCa);

            var btnXoaPhanCa = new Button { Text = "Xóa phân ca chọn", Width = 160, Margin = new Padding(0, 4, 10, 0), BackColor = Color.FromArgb(192, 57, 43) };
            btnXoaPhanCa.Click += (s, e) => {
                var row = FormHelper.Row(dgvPhanCa);
                if (row == null) { MessageBox.Show(this, "Vui lòng chọn dòng phân ca cần xóa trong bảng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                int maNV = Convert.ToInt32(row["MaNV"]);
                DateTime ngay = Convert.ToDateTime(row["Ngay"]);
                int maCa = Convert.ToInt32(row["MaCa"]);

                if (MessageBox.Show(this, $"Xác nhận xóa phân ca của nhân viên ngày {ngay:dd/MM/yyyy}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    FormHelper.Run(this, () => {
                        service.XoaPhanCa(maNV, ngay, maCa);
                        FormHelper.Success(this, "Đã xóa phân ca thành công!");
                        LoadPhanCaList();
                    });
                }
            };
            actions.Controls.Add(btnXoaPhanCa);
            layout.Controls.Add(actions, 0, 3);

            // 5. Status
            var lblStatus = new Label {
                Text = "Chọn ca làm việc và ngày để phân công ca cho nhân viên.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 4);

            tab.Controls.Add(layout);
        }

        private void InitTabChamCong(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 5
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));  // Filter
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));  // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Filter
            var pnlFilter = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            pnlFilter.Controls.Add(new Label { Text = "Từ ngày:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            dtpChamCongTu = new DateTimePicker { Width = 115, Format = DateTimePickerFormat.Short, Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), Margin = new Padding(0, 4, 15, 0) };
            pnlFilter.Controls.Add(dtpChamCongTu);

            pnlFilter.Controls.Add(new Label { Text = "Đến ngày:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            dtpChamCongDen = new DateTimePicker { Width = 115, Format = DateTimePickerFormat.Short, Value = DateTime.Today, Margin = new Padding(0, 4, 15, 0) };
            pnlFilter.Controls.Add(dtpChamCongDen);

            pnlFilter.Controls.Add(new Label { Text = "Phòng ban:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            cboPhongBanChamCong = new ComboBox { Width = 200, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(0, 4, 15, 0) };
            pnlFilter.Controls.Add(cboPhongBanChamCong);

            var btnXemChamCong = new Button { Text = "Lọc dữ liệu", Width = 110, Margin = new Padding(0, 2, 10, 0) };
            btnXemChamCong.Click += (s, e) => FormHelper.Run(this, LoadChamCongList);
            pnlFilter.Controls.Add(btnXemChamCong);
            layout.Controls.Add(pnlFilter, 0, 0);

            // 2. DataGridView
            dgvChamCong = new DataGridView { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 8) };
            dgvChamCong.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvChamCong);
                if (row != null) {
                    selectedMaNVCC = Convert.ToInt32(row["MaNV"]);
                    string hoTen = row["HoTen"].ToString();
                    selectedNgayCC = Convert.ToDateTime(row["Ngay"]);
                    selectedMaCaCC = Convert.ToInt32(row["MaCa"]);
                    string tenCa = row["TenCa"].ToString();
                    decimal gioQuyDinh = Convert.ToDecimal(row["GioQuyDinh"]);

                    txtCaDangCham.Text = $"[{selectedMaNVCC}] {hoTen} • {selectedNgayCC:dd/MM/yyyy} • {tenCa} ({gioQuyDinh}h)";

                    object val = row["SoGioThucTe"];
                    if (val == DBNull.Value || val == null) {
                        chkChuaCham.Checked = true;
                        chkVangMat.Checked = false;
                        numGioThucTe.Value = gioQuyDinh;
                    } else {
                        chkChuaCham.Checked = false;
                        decimal h = Convert.ToDecimal(val);
                        if (h == 0) chkVangMat.Checked = true;
                        else { chkVangMat.Checked = false; numGioThucTe.Value = h; }
                    }
                }
            };
            layout.Controls.Add(dgvChamCong, 0, 1);

            // 3. Editor
            var editor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Margin = new Padding(0, 4, 0, 4)
            };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            txtCaDangCham = new TextBox { ReadOnly = true };
            numGioThucTe = new NumericUpDown { DecimalPlaces = 2, Increment = 0.5m, Maximum = 24, Value = 4 };

            chkVangMat = new CheckBox { Text = "Vắng mặt (0 giờ)", AutoSize = true, Margin = new Padding(0, 8, 0, 0) };
            chkVangMat.CheckedChanged += (s, e) => {
                if (chkVangMat.Checked) {
                    chkChuaCham.Checked = false;
                    numGioThucTe.Value = 0;
                    numGioThucTe.Enabled = false;
                } else if (!chkChuaCham.Checked) {
                    numGioThucTe.Enabled = true;
                }
            };

            chkChuaCham = new CheckBox { Text = "Chưa chấm (NULL)", AutoSize = true, Margin = new Padding(0, 8, 0, 0) };
            chkChuaCham.CheckedChanged += (s, e) => {
                if (chkChuaCham.Checked) {
                    chkVangMat.Checked = false;
                    numGioThucTe.Enabled = false;
                } else if (!chkVangMat.Checked) {
                    numGioThucTe.Enabled = true;
                }
            };

            editor.Controls.Add(FormHelper.CreateField("Ca làm việc đang chọn", txtCaDangCham), 0, 0);
            editor.Controls.Add(FormHelper.CreateField("Số giờ thực tế (h) *", numGioThucTe), 1, 0);
            editor.Controls.Add(FormHelper.CreateField("Trạng thái vắng", chkVangMat), 2, 0);
            editor.Controls.Add(FormHelper.CreateField("Trạng thái treo", chkChuaCham), 3, 0);
            layout.Controls.Add(editor, 0, 2);

            // 4. Actions
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            var btnLuuCC = new Button { Text = "LƯU KẾT QUẢ CHẤM CÔNG", Width = 250, Margin = new Padding(0, 4, 10, 0) };
            btnLuuCC.Click += (s, e) => {
                if (selectedMaNVCC <= 0) { MessageBox.Show(this, "Vui lòng chọn ca cần chấm công trong bảng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                FormHelper.Run(this, () => {
                    decimal? gio = null;
                    if (!chkChuaCham.Checked) gio = chkVangMat.Checked ? 0 : numGioThucTe.Value;
                    service.ChamCongThucTe(selectedMaNVCC, selectedNgayCC, selectedMaCaCC, gio);
                    FormHelper.Success(this, "Cập nhật giờ công thực tế thành công!");
                    LoadChamCongList();
                });
            };
            actions.Controls.Add(btnLuuCC);
            layout.Controls.Add(actions, 0, 3);

            // 5. Status
            var lblStatus = new Label {
                Text = "Chọn ca làm việc trong danh sách để cập nhật số giờ thực tế hoặc xác nhận vắng mặt.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 4);

            tab.Controls.Add(layout);
        }

        private void InitTabPhuTroi(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 5
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));  // Header
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // SplitGrids
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 135F)); // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Header
            var lblHdr = new Label {
                Text = "Quản lý và Duyệt Phiếu Phụ trội làm thêm giờ (Tăng ca, Nghỉ lễ, Ngày nghỉ)",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = FormHelper.Primary,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            layout.Controls.Add(lblHdr, 0, 0);

            // 2. Grids Split
            var splitGrids = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical, SplitterDistance = 620 };

            var grpPhieu = new GroupBox { Text = " Danh sách Phiếu Phụ trội ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvPhieuPT = new DataGridView { Dock = DockStyle.Fill };
            dgvPhieuPT.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvPhieuPT);
                if (row != null) {
                    selectedMaPhieuPT = Convert.ToInt32(row["MaPhieuPT"]);
                    LoadNhanVienPT(selectedMaPhieuPT);
                }
            };
            grpPhieu.Controls.Add(dgvPhieuPT);
            splitGrids.Panel1.Controls.Add(grpPhieu);

            var grpNVPT = new GroupBox { Text = " Nhân viên tham gia phụ trội ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvNVPhuTroi = new DataGridView { Dock = DockStyle.Fill };
            grpNVPT.Controls.Add(dgvNVPhuTroi);
            splitGrids.Panel2.Controls.Add(grpNVPT);
            layout.Controls.Add(splitGrids, 0, 1);

            // 3. Editor Panel
            var pnlEditor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 4, 0, 4)
            };
            pnlEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            pnlEditor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            pnlEditor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Left: input controls (TableLayoutPanel 2 rows x 3 cols)
            var pnlInputs = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 2 };
            pnlInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            pnlInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            pnlInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            pnlInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            pnlInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            txtSoPhieuPT = new TextBox();
            dtpNgayPT = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            dtpGioBatDauPT = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "HH:mm", ShowUpDown = true };
            numSoGioPT = new NumericUpDown { DecimalPlaces = 1, Increment = 0.5m, Maximum = 24, Value = 2 };
            cboHinhThucPT = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cboHinhThucPT.Items.AddRange(new object[] { "Tăng ca", "Ngày lễ", "Ngày nghỉ" });
            cboHinhThucPT.SelectedIndex = 0;

            pnlInputs.Controls.Add(FormHelper.CreateField("Số phiếu *", txtSoPhieuPT), 0, 0);
            pnlInputs.Controls.Add(FormHelper.CreateField("Ngày làm *", dtpNgayPT), 1, 0);
            pnlInputs.Controls.Add(FormHelper.CreateField("Giờ bắt đầu *", dtpGioBatDauPT), 2, 0);
            pnlInputs.Controls.Add(FormHelper.CreateField("Số giờ *", numSoGioPT), 0, 1);
            pnlInputs.Controls.Add(FormHelper.CreateField("Hình thức *", cboHinhThucPT), 1, 1);
            pnlEditor.Controls.Add(pnlInputs, 0, 0);

            // Right: CheckedListBox for employees
            clbNhanVienPT = new CheckedListBox { Dock = DockStyle.Fill, CheckOnClick = true };
            var pnlClb = FormHelper.CreateField("Chọn nhân viên tham gia phụ trội *", clbNhanVienPT);
            pnlEditor.Controls.Add(pnlClb, 1, 0);

            layout.Controls.Add(pnlEditor, 0, 2);

            // 4. Actions
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            var btnTaoPT = new Button { Text = "LẬP KẾ HOẠCH PHỤ TRỘI", Width = 210, Margin = new Padding(0, 4, 10, 0) };
            btnTaoPT.Click += (s, e) => {
                var selectedNVs = new List<int>();
                foreach (var item in clbNhanVienPT.CheckedItems) {
                    var r = item as DataRowView;
                    if (r != null) selectedNVs.Add(Convert.ToInt32(r["MaNV"]));
                }
                if (selectedNVs.Count == 0) { MessageBox.Show(this, "Vui lòng chọn ít nhất một nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                FormHelper.Run(this, () => {
                    service.TaoPhieuPhuTroi(txtSoPhieuPT.Text, dtpNgayPT.Value, dtpGioBatDauPT.Value.TimeOfDay, numSoGioPT.Value, cboHinhThucPT.Text, selectedNVs);
                    FormHelper.Success(this, "Lập phiếu phụ trội thành công (Trạng thái: Kế hoạch)!");
                    txtSoPhieuPT.Clear();
                    LoadPhieuPTList();
                });
            };
            actions.Controls.Add(btnTaoPT);

            var btnXacNhanPT = new Button { Text = "Ban Chấm công: XÁC NHẬN HOÀN THÀNH", Width = 290, Margin = new Padding(0, 4, 10, 0), BackColor = Color.FromArgb(39, 174, 96) };
            btnXacNhanPT.Click += (s, e) => {
                if (selectedMaPhieuPT <= 0) { MessageBox.Show(this, "Vui lòng chọn phiếu phụ trội cần xác nhận.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                FormHelper.Run(this, () => {
                    service.XacNhanPhuTroi(selectedMaPhieuPT);
                    FormHelper.Success(this, "Xác nhận phiếu phụ trội thành công! Phiếu đã đủ điều kiện tính tiền khi chốt lương.");
                    LoadPhieuPTList();
                });
            };
            actions.Controls.Add(btnXacNhanPT);

            var btnMoLaiPT = new Button { Text = "Mở lại (Kế hoạch)", Width = 150, Margin = new Padding(0, 4, 10, 0) };
            btnMoLaiPT.Click += (s, e) => {
                if (selectedMaPhieuPT <= 0) { MessageBox.Show(this, "Vui lòng chọn phiếu phụ trội cần mở lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                FormHelper.Run(this, () => {
                    service.MoLaiPhieuPhuTroi(selectedMaPhieuPT);
                    FormHelper.Success(this, "Đã mở lại phiếu phụ trội về trạng thái Kế hoạch.");
                    LoadPhieuPTList();
                });
            };
            actions.Controls.Add(btnMoLaiPT);

            var btnHuyPT = new Button { Text = "Hủy / Xóa phiếu", Width = 140, Margin = new Padding(0, 4, 10, 0), BackColor = Color.FromArgb(192, 57, 43) };
            btnHuyPT.Click += (s, e) => {
                if (selectedMaPhieuPT <= 0) { MessageBox.Show(this, "Vui lòng chọn phiếu phụ trội cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (MessageBox.Show(this, "Xác nhận xóa phiếu phụ trội này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    FormHelper.Run(this, () => {
                        service.XoaPhieuPhuTroi(selectedMaPhieuPT);
                        FormHelper.Success(this, "Đã xóa phiếu phụ trội thành công!");
                        selectedMaPhieuPT = 0;
                        LoadPhieuPTList();
                    });
                }
            };
            actions.Controls.Add(btnHuyPT);
            layout.Controls.Add(actions, 0, 3);

            // 5. Status
            var lblStatus = new Label {
                Text = "Phiếu phụ trội phải được Ban Chấm công xác nhận hoàn tất mới được tính vào bảng lương.",
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
            FormHelper.Bind(cboNVPhanCa, dtNV, "MaNV", "HoTen");

            var dtCa = danhMucService.GetAllCaLamViec();
            FormHelper.Bind(cboCaLamViec, dtCa, "MaCa", "TenCa");

            var dtPB = danhMucService.GetAllPhongBan();
            var rowAll = dtPB.NewRow();
            rowAll["MaPhongBan"] = 0;
            rowAll["TenPhongBan"] = "Tất cả phòng ban";
            dtPB.Rows.InsertAt(rowAll, 0);
            FormHelper.Bind(cboPhongBanChamCong, dtPB, "MaPhongBan", "TenPhongBan");

            clbNhanVienPT.DataSource = dtNV;
            clbNhanVienPT.DisplayMember = "HoTen";
            clbNhanVienPT.ValueMember = "MaNV";

            LoadPhanCaList();
            LoadChamCongList();
            LoadPhieuPTList();
        }

        private void LoadPhanCaList()
        {
            dgvPhanCa.DataSource = service.GetBangChamCong(dtpLocPhanCaTu.Value, dtpLocPhanCaDen.Value);
            FormHelper.Columns(dgvPhanCa,
                "MaNV", "Mã NV",
                "HoTen", "Họ tên",
                "TenPhongBan", "Phòng ban",
                "Ngay", "Ngày làm",
                "TenCa", "Ca làm việc",
                "GioBatDau", "Giờ BĐ",
                "GioKetThuc", "Giờ KT");
        }

        private void LoadChamCongList()
        {
            int? maPB = null;
            if (cboPhongBanChamCong.SelectedValue != null && int.TryParse(cboPhongBanChamCong.SelectedValue.ToString(), out int id) && id > 0)
                maPB = id;

            dgvChamCong.DataSource = service.GetBangChamCong(dtpChamCongTu.Value, dtpChamCongDen.Value, maPB);
            FormHelper.Columns(dgvChamCong,
                "MaNV", "Mã NV",
                "HoTen", "Họ tên",
                "TenPhongBan", "Phòng ban",
                "Ngay", "Ngày",
                "TenCa", "Ca",
                "SoGioThucTe", "Giờ TT",
                "TrangThaiCong", "Trạng thái");
        }

        private void LoadPhieuPTList()
        {
            dgvPhieuPT.DataSource = service.GetAllPhieuPhuTroi(DateTime.Today.AddMonths(-3), DateTime.Today.AddMonths(3));
            FormHelper.Columns(dgvPhieuPT,
                "SoPhieuPT", "Số phiếu",
                "NgayPhuTroi", "Ngày làm",
                "GioBatDau", "Giờ BĐ",
                "SoGio", "Số giờ",
                "HinhThuc", "Hình thức",
                "TrangThai", "Trạng thái");

            if (dgvPhieuPT.Rows.Count > 0) {
                var row = FormHelper.Row(dgvPhieuPT);
                if (row != null) {
                    selectedMaPhieuPT = Convert.ToInt32(row["MaPhieuPT"]);
                    LoadNhanVienPT(selectedMaPhieuPT);
                }
            } else {
                dgvNVPhuTroi.DataSource = null;
            }
        }

        private void LoadNhanVienPT(int maPhieu)
        {
            dgvNVPhuTroi.DataSource = service.GetNhanVienByPhieuPhuTroi(maPhieu);
            FormHelper.Columns(dgvNVPhuTroi,
                "MaNV", "Mã NV",
                "HoTen", "Họ tên",
                "TenPhongBan", "Phòng ban",
                "XacNhan", "Xác nhận",
                "GioXacNhan", "Giờ XN");
        }
    }
}
