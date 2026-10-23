using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public class FrmDanhMuc : Form
    {
        private TabControl tabControl;
        private readonly DanhMucService service = new DanhMucService();

        // Tab Phòng Ban
        private DataGridView dgvPhongBan;
        private TextBox txtTenPhongBan;
        private int selectedPhongBanId = 0;

        // Tab Chức Vụ
        private DataGridView dgvChucVu;
        private TextBox txtTenChucVu;
        private int selectedChucVuId = 0;

        // Tab Ca Làm Việc
        private DataGridView dgvCaLamViec;
        private TextBox txtTenCa;
        private DateTimePicker dtpGioBatDau;
        private DateTimePicker dtpGioKetThuc;
        private int selectedCaId = 0;

        public FrmDanhMuc()
        {
            InitializeComponent();
            FormHelper.Style(this);
            Shown += (s, e) => FormHelper.Run(this, LoadData);
        }

        private void InitializeComponent()
        {
            this.Text = "Quản lý danh mục";
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
                Text = "Quản lý danh mục hệ thống",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 65, 94),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            root.Controls.Add(lblTitle, 0, 0);

            tabControl = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F) };

            var tabPB = new TabPage("Phòng Ban");
            InitTabPhongBan(tabPB);
            tabControl.TabPages.Add(tabPB);

            var tabCV = new TabPage("Chức Vụ");
            InitTabChucVu(tabCV);
            tabControl.TabPages.Add(tabCV);

            var tabCa = new TabPage("Ca Làm Việc");
            InitTabCaLamViec(tabCa);
            tabControl.TabPages.Add(tabCa);

            root.Controls.Add(tabControl, 0, 1);
            this.Controls.Add(root);
        }

        private void InitTabPhongBan(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 4
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));  // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Grid
            dgvPhongBan = new DataGridView { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 8) };
            dgvPhongBan.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvPhongBan);
                if (row != null) {
                    selectedPhongBanId = Convert.ToInt32(row["MaPhongBan"]);
                    txtTenPhongBan.Text = row["TenPhongBan"].ToString();
                }
            };
            layout.Controls.Add(dgvPhongBan, 0, 0);

            // 2. Editor
            var editor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 4, 0, 4)
            };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            txtTenPhongBan = new TextBox();
            editor.Controls.Add(FormHelper.CreateField("Tên phòng ban *", txtTenPhongBan), 0, 0);
            layout.Controls.Add(editor, 0, 1);

            // 3. Actions
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            var btnThem = new Button { Text = "Thêm mới", Width = 110, Margin = new Padding(0, 4, 10, 0) };
            btnThem.Click += (s, e) => FormHelper.Run(this, () => {
                service.AddPhongBan(txtTenPhongBan.Text);
                FormHelper.Success(this, "Thêm phòng ban thành công!");
                txtTenPhongBan.Clear();
                selectedPhongBanId = 0;
                LoadPhongBan();
            });
            actions.Controls.Add(btnThem);

            var btnSua = new Button { Text = "Cập nhật", Width = 110, Margin = new Padding(0, 4, 10, 0) };
            btnSua.Click += (s, e) => FormHelper.Run(this, () => {
                if (selectedPhongBanId <= 0) throw new InvalidOperationException("Vui lòng chọn phòng ban cần sửa trong bảng.");
                service.UpdatePhongBan(selectedPhongBanId, txtTenPhongBan.Text);
                FormHelper.Success(this, "Cập nhật phòng ban thành công!");
                LoadPhongBan();
            });
            actions.Controls.Add(btnSua);

            var btnXoa = new Button { Text = "Xóa", Width = 90, Margin = new Padding(0, 4, 10, 0), BackColor = Color.FromArgb(192, 57, 43) };
            btnXoa.Click += (s, e) => {
                if (selectedPhongBanId <= 0) { MessageBox.Show(this, "Vui lòng chọn phòng ban cần xóa trong bảng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (MessageBox.Show(this, "Bạn có chắc chắn muốn xóa phòng ban này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    FormHelper.Run(this, () => {
                        service.DeletePhongBan(selectedPhongBanId);
                        FormHelper.Success(this, "Xóa phòng ban thành công!");
                        txtTenPhongBan.Clear();
                        selectedPhongBanId = 0;
                        LoadPhongBan();
                    });
                }
            };
            actions.Controls.Add(btnXoa);
            layout.Controls.Add(actions, 0, 2);

            // 4. Status
            var lblStatus = new Label {
                Text = "Chọn dòng phòng ban để sửa hoặc xóa. Không xóa được phòng ban đang có nhân viên.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 3);

            tab.Controls.Add(layout);
        }

        private void InitTabChucVu(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 4
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));  // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Grid
            dgvChucVu = new DataGridView { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 8) };
            dgvChucVu.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvChucVu);
                if (row != null) {
                    selectedChucVuId = Convert.ToInt32(row["MaChucVu"]);
                    txtTenChucVu.Text = row["TenChucVu"].ToString();
                }
            };
            layout.Controls.Add(dgvChucVu, 0, 0);

            // 2. Editor
            var editor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 4, 0, 4)
            };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            txtTenChucVu = new TextBox();
            editor.Controls.Add(FormHelper.CreateField("Tên chức vụ *", txtTenChucVu), 0, 0);
            layout.Controls.Add(editor, 0, 1);

            // 3. Actions
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            var btnThem = new Button { Text = "Thêm mới", Width = 110, Margin = new Padding(0, 4, 10, 0) };
            btnThem.Click += (s, e) => FormHelper.Run(this, () => {
                service.AddChucVu(txtTenChucVu.Text);
                FormHelper.Success(this, "Thêm chức vụ thành công!");
                txtTenChucVu.Clear();
                selectedChucVuId = 0;
                LoadChucVu();
            });
            actions.Controls.Add(btnThem);

            var btnSua = new Button { Text = "Cập nhật", Width = 110, Margin = new Padding(0, 4, 10, 0) };
            btnSua.Click += (s, e) => FormHelper.Run(this, () => {
                if (selectedChucVuId <= 0) throw new InvalidOperationException("Vui lòng chọn chức vụ cần sửa trong bảng.");
                service.UpdateChucVu(selectedChucVuId, txtTenChucVu.Text);
                FormHelper.Success(this, "Cập nhật chức vụ thành công!");
                LoadChucVu();
            });
            actions.Controls.Add(btnSua);

            var btnXoa = new Button { Text = "Xóa", Width = 90, Margin = new Padding(0, 4, 10, 0), BackColor = Color.FromArgb(192, 57, 43) };
            btnXoa.Click += (s, e) => {
                if (selectedChucVuId <= 0) { MessageBox.Show(this, "Vui lòng chọn chức vụ cần xóa trong bảng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (MessageBox.Show(this, "Bạn có chắc chắn muốn xóa chức vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    FormHelper.Run(this, () => {
                        service.DeleteChucVu(selectedChucVuId);
                        FormHelper.Success(this, "Xóa chức vụ thành công!");
                        txtTenChucVu.Clear();
                        selectedChucVuId = 0;
                        LoadChucVu();
                    });
                }
            };
            actions.Controls.Add(btnXoa);
            layout.Controls.Add(actions, 0, 2);

            // 4. Status
            var lblStatus = new Label {
                Text = "Chọn dòng chức vụ để sửa hoặc xóa. Không xóa được chức vụ đang có quyết định bổ nhiệm liên kết.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 3);

            tab.Controls.Add(layout);
        }

        private void InitTabCaLamViec(TabPage tab)
        {
            tab.BackColor = Color.White;
            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(12),
                ColumnCount = 1,
                RowCount = 4
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Grid
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));  // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Grid
            dgvCaLamViec = new DataGridView { Dock = DockStyle.Fill, Margin = new Padding(0, 4, 0, 8) };
            dgvCaLamViec.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvCaLamViec);
                if (row != null) {
                    selectedCaId = Convert.ToInt32(row["MaCa"]);
                    txtTenCa.Text = row["TenCa"].ToString();
                    TimeSpan bd = (TimeSpan)row["GioBatDau"];
                    TimeSpan kt = (TimeSpan)row["GioKetThuc"];
                    dtpGioBatDau.Value = DateTime.Today.Add(bd);
                    dtpGioKetThuc.Value = DateTime.Today.Add(kt);
                }
            };
            layout.Controls.Add(dgvCaLamViec, 0, 0);

            // 2. Editor
            var editor = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                Margin = new Padding(0, 4, 0, 4)
            };
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            editor.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            txtTenCa = new TextBox();
            dtpGioBatDau = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "HH:mm", ShowUpDown = true, Value = DateTime.Today.AddHours(8) };
            dtpGioKetThuc = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "HH:mm", ShowUpDown = true, Value = DateTime.Today.AddHours(12) };

            editor.Controls.Add(FormHelper.CreateField("Tên ca làm việc *", txtTenCa), 0, 0);
            editor.Controls.Add(FormHelper.CreateField("Giờ bắt đầu *", dtpGioBatDau), 1, 0);
            editor.Controls.Add(FormHelper.CreateField("Giờ kết thúc *", dtpGioKetThuc), 2, 0);
            layout.Controls.Add(editor, 0, 1);

            // 3. Actions
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            var btnThem = new Button { Text = "Thêm mới", Width = 110, Margin = new Padding(0, 4, 10, 0) };
            btnThem.Click += (s, e) => FormHelper.Run(this, () => {
                service.AddCaLamViec(txtTenCa.Text, dtpGioBatDau.Value.TimeOfDay, dtpGioKetThuc.Value.TimeOfDay);
                FormHelper.Success(this, "Thêm ca làm việc thành công!");
                txtTenCa.Clear();
                selectedCaId = 0;
                LoadCaLamViec();
            });
            actions.Controls.Add(btnThem);

            var btnSua = new Button { Text = "Cập nhật", Width = 110, Margin = new Padding(0, 4, 10, 0) };
            btnSua.Click += (s, e) => FormHelper.Run(this, () => {
                if (selectedCaId <= 0) throw new InvalidOperationException("Vui lòng chọn ca cần sửa trong bảng.");
                service.UpdateCaLamViec(selectedCaId, txtTenCa.Text, dtpGioBatDau.Value.TimeOfDay, dtpGioKetThuc.Value.TimeOfDay);
                FormHelper.Success(this, "Cập nhật ca làm việc thành công!");
                LoadCaLamViec();
            });
            actions.Controls.Add(btnSua);

            var btnXoa = new Button { Text = "Xóa", Width = 90, Margin = new Padding(0, 4, 10, 0), BackColor = Color.FromArgb(192, 57, 43) };
            btnXoa.Click += (s, e) => {
                if (selectedCaId <= 0) { MessageBox.Show(this, "Vui lòng chọn ca cần xóa trong bảng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (MessageBox.Show(this, "Bạn có chắc chắn muốn xóa ca làm việc này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    FormHelper.Run(this, () => {
                        service.DeleteCaLamViec(selectedCaId);
                        FormHelper.Success(this, "Xóa ca làm việc thành công!");
                        txtTenCa.Clear();
                        selectedCaId = 0;
                        LoadCaLamViec();
                    });
                }
            };
            actions.Controls.Add(btnXoa);
            layout.Controls.Add(actions, 0, 2);

            // 4. Status
            var lblStatus = new Label {
                Text = "Ca làm việc dùng để phân ca cho nhân viên. Không xóa được ca đã có phân công chấm công.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblStatus, 0, 3);

            tab.Controls.Add(layout);
        }

        private void LoadData()
        {
            LoadPhongBan();
            LoadChucVu();
            LoadCaLamViec();
        }

        private void LoadPhongBan()
        {
            dgvPhongBan.DataSource = service.GetAllPhongBan();
            FormHelper.Columns(dgvPhongBan, "MaPhongBan", "Mã PB", "TenPhongBan", "Tên phòng ban");
        }

        private void LoadChucVu()
        {
            dgvChucVu.DataSource = service.GetAllChucVu();
            FormHelper.Columns(dgvChucVu, "MaChucVu", "Mã CV", "TenChucVu", "Tên chức vụ");
        }

        private void LoadCaLamViec()
        {
            dgvCaLamViec.DataSource = service.GetAllCaLamViec();
            FormHelper.Columns(dgvCaLamViec, "MaCa", "Mã ca", "TenCa", "Tên ca", "GioBatDau", "Giờ bắt đầu", "GioKetThuc", "Giờ kết thúc");
        }
    }
}
