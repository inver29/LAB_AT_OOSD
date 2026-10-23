using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public class FrmThuChi : Form
    {
        private readonly ThuChiService service = new ThuChiService();
        private TabControl tabControl;

        // Tab 1: Lập phiếu
        private DataGridView dgvChuaThuChi;
        private Label lblSelectedQD;
        private TextBox txtSoPhieu, txtLoaiPhieuPreview, txtSoTienPreview;
        private DateTimePicker dtpNgayLapPhieu;
        private int selectedMaQD = 0;
        private DateTime selectedNgayKy = DateTime.Today;

        // Tab 2: Lịch sử phiếu
        private DataGridView dgvDaThuChi;
        private ComboBox cboLocLoaiPhieu;
        private DateTimePicker dtpFromDate, dtpToDate;

        public FrmThuChi()
        {
            InitializeComponent();
            FormHelper.Style(this);
            Shown += (s, e) => FormHelper.Run(this, LoadData);
        }

        private void InitializeComponent()
        {
            this.Text = "Thu chi tài chính";
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
                Text = "Quản lý Thu Chi Tài chính (Theo quyết định Khen thưởng / Kỷ luật trực tiếp)",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 65, 94),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            root.Controls.Add(lblTitle, 0, 0);

            tabControl = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };

            var tabLap = new TabPage("Lập Phiếu Thu / Chi");
            InitTabLap(tabLap);
            tabControl.TabPages.Add(tabLap);

            var tabLichSu = new TabPage("Danh sách Phiếu đã lập");
            InitTabLichSu(tabLichSu);
            tabControl.TabPages.Add(tabLichSu);

            root.Controls.Add(tabControl, 0, 1);
            this.Controls.Add(root);
        }

        private void InitTabLap(TabPage tab)
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
            lblSelectedQD = new Label {
                Text = "Vui lòng chọn quyết định trực tiếp ĐÃ KÝ trong danh sách dưới đây để lập phiếu thanh toán.",
                AutoSize = true,
                Margin = new Padding(0, 10, 0, 0),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = FormHelper.Primary,
                UseMnemonic = false
            };
            pnlTop.Controls.Add(lblSelectedQD);
            layout.Controls.Add(pnlTop, 0, 0);

            // 2. DataGridView
            dgvChuaThuChi = new DataGridView { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 8) };
            dgvChuaThuChi.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvChuaThuChi);
                if (row != null) {
                    selectedMaQD = Convert.ToInt32(row["MaQD"]);
                    string soQD = row["SoQD"].ToString();
                    string loai = row["LoaiQD"].ToString();
                    string loaiPhieu = row["LoaiPhieuDuKien"].ToString();
                    decimal tien = Convert.ToDecimal(row["SoTien"]);
                    selectedNgayKy = Convert.ToDateTime(row["NgayKy"]);

                    lblSelectedQD.Text = $"Đang chọn: [{soQD}] {loai} • Ngày ký: {selectedNgayKy:dd/MM/yyyy}";
                    txtLoaiPhieuPreview.Text = loaiPhieu.ToUpper();
                    txtSoTienPreview.Text = FormHelper.Money(tien);
                    txtSoPhieu.Text = $"PTC-{loaiPhieu.ToUpper()}-{DateTime.Today.Year}-{selectedMaQD:D3}";
                    dtpNgayLapPhieu.Value = selectedNgayKy > DateTime.Today ? selectedNgayKy : DateTime.Today;
                }
            };
            layout.Controls.Add(dgvChuaThuChi, 0, 1);

            // 3. Editor Panel
            var editor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1,
                Margin = new Padding(0, 4, 0, 4)
            };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            txtLoaiPhieuPreview = new TextBox { ReadOnly = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = FormHelper.Primary };
            txtSoTienPreview = new TextBox { ReadOnly = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(192, 57, 43) };
            txtSoPhieu = new TextBox();
            dtpNgayLapPhieu = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };

            editor.Controls.Add(FormHelper.CreateField("Loại phiếu tự động", txtLoaiPhieuPreview), 0, 0);
            editor.Controls.Add(FormHelper.CreateField("Số tiền thanh toán", txtSoTienPreview), 1, 0);
            editor.Controls.Add(FormHelper.CreateField("Số phiếu thu/chi *", txtSoPhieu), 2, 0);
            editor.Controls.Add(FormHelper.CreateField("Ngày lập phiếu *", dtpNgayLapPhieu), 3, 0);
            layout.Controls.Add(editor, 0, 2);

            // 4. Action Buttons
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            var btnLapPhieu = new Button { Text = "XÁC NHẬN LẬP PHIẾU THU / CHI", Width = 300, Margin = new Padding(0, 4, 10, 0), BackColor = Color.FromArgb(39, 174, 96) };
            btnLapPhieu.Click += (s, e) => {
                if (selectedMaQD <= 0) { MessageBox.Show(this, "Vui lòng chọn quyết định cần thanh toán trong bảng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                FormHelper.Run(this, () => {
                    service.LapPhieuThuChi(txtSoPhieu.Text, selectedMaQD, dtpNgayLapPhieu.Value);
                    FormHelper.Success(this, "Lập phiếu Thu / Chi thành công! Quyết định đã được đánh dấu hoàn tất thanh toán.");
                    selectedMaQD = 0;
                    LoadData();
                });
            };
            actions.Controls.Add(btnLapPhieu);
            layout.Controls.Add(actions, 0, 3);

            // 5. Status
            var lblStatus = new Label {
                Text = "Lập phiếu thu / chi trực tiếp một lần cho các quyết định khen thưởng hoặc kỷ luật đã ký.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 4);

            tab.Controls.Add(layout);
        }

        private void InitTabLichSu(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 3
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));  // Filter
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Filter
            var pnlFilter = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            pnlFilter.Controls.Add(new Label { Text = "Loại phiếu:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            cboLocLoaiPhieu = new ComboBox { Width = 130, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(0, 4, 15, 0) };
            cboLocLoaiPhieu.Items.AddRange(new object[] { "Tất cả", "Thu", "Chi" });
            cboLocLoaiPhieu.SelectedIndex = 0;
            pnlFilter.Controls.Add(cboLocLoaiPhieu);

            pnlFilter.Controls.Add(new Label { Text = "Từ ngày:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            dtpFromDate = new DateTimePicker { Width = 115, Format = DateTimePickerFormat.Short, Value = DateTime.Today.AddMonths(-3), Margin = new Padding(0, 4, 15, 0) };
            pnlFilter.Controls.Add(dtpFromDate);

            pnlFilter.Controls.Add(new Label { Text = "Đến ngày:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            dtpToDate = new DateTimePicker { Width = 115, Format = DateTimePickerFormat.Short, Value = DateTime.Today, Margin = new Padding(0, 4, 15, 0) };
            pnlFilter.Controls.Add(dtpToDate);

            var btnFilter = new Button { Text = "Lọc dữ liệu", Width = 110, Margin = new Padding(0, 2, 10, 0) };
            btnFilter.Click += (s, e) => FormHelper.Run(this, LoadLichSuList);
            pnlFilter.Controls.Add(btnFilter);
            layout.Controls.Add(pnlFilter, 0, 0);

            // 2. DataGridView
            dgvDaThuChi = new DataGridView { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 8) };
            layout.Controls.Add(dgvDaThuChi, 0, 1);

            // 3. Status
            var lblStatus = new Label {
                Text = "Lịch sử các chứng từ thu chi trực tiếp đã phát sinh và hoàn tất trong hệ thống.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 2);

            tab.Controls.Add(layout);
        }

        private void LoadData()
        {
            LoadChuaThuChiList();
            LoadLichSuList();
        }

        private void LoadChuaThuChiList()
        {
            dgvChuaThuChi.DataSource = service.GetQuyetDinhChuaThanhToan();
            FormHelper.Columns(dgvChuaThuChi,
                "SoQD", "Số QĐ",
                "LoaiQD", "Loại QĐ",
                "LoaiPhieuDuKien", "Loại phiếu",
                "SoTien", "Số tiền",
                "NgayKy", "Ngày ký",
                "TrangThai", "Trạng thái");
        }

        private void LoadLichSuList()
        {
            string loai = cboLocLoaiPhieu.SelectedItem?.ToString();
            dgvDaThuChi.DataSource = service.GetAllPhieuThuChi(loai, dtpFromDate.Value, dtpToDate.Value);
            FormHelper.Columns(dgvDaThuChi,
                "SoPhieu", "Số phiếu",
                "LoaiPhieu", "Loại",
                "SoTien", "Số tiền",
                "NgayLap", "Ngày lập",
                "SoQD", "Số QĐ nguồn",
                "LoaiQD", "Loại QĐ");
        }
    }
}
