using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public class FrmThuongPhat : Form
    {
        private readonly ThuongPhatService service = new ThuongPhatService();
        private readonly DanhMucService danhMucService = new DanhMucService();
        private readonly HoSoHopDongService hoSoService = new HoSoHopDongService();

        private DataGridView dgvQuyetDinh, dgvPhanBo;
        private TextBox txtSearch;
        private ComboBox cboLocLoai, cboLocTrangThai;

        // Form lập QĐ
        private TextBox txtSoQD, txtNoiDung;
        private ComboBox cboLoaiQD, cboCachThanhToan;
        private RadioButton radCaNhan, radTapThe;
        private ComboBox cboNhanVien, cboPhongBan;
        private NumericUpDown numSoTien;
        private DateTimePicker dtpNgayLap, dtpKyApDung;
        private DataTable dtNhapPhanBo;

        // Ký duyệt
        private int selectedMaQD = 0;
        private Label lblSelectedQD;
        private DateTimePicker dtpNgayKy;
        private ComboBox cboNguoiKy;

        public FrmThuongPhat()
        {
            InitializeComponent();
            FormHelper.Style(this);
            Shown += (s, e) => FormHelper.Run(this, LoadData);
        }

        private void InitializeComponent()
        {
            this.Text = "Khen thưởng & Kỷ luật";
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
                Text = "Quản lý Khen thưởng & Kỷ luật (Lập quyết định & Ký duyệt)",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 65, 94),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            root.Controls.Add(lblTitle, 0, 0);

            var layout = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5
            };
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));  // Search
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // SplitGrids
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F)); // Editor
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));  // Actions
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));  // Status

            // 1. Search Bar
            var pnlSearch = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            pnlSearch.Controls.Add(new Label { Text = "Tìm kiếm:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            txtSearch = new TextBox { Width = 160, Margin = new Padding(0, 4, 15, 0) };
            txtSearch.TextChanged += (s, e) => LoadQuyetDinhList();
            pnlSearch.Controls.Add(txtSearch);

            pnlSearch.Controls.Add(new Label { Text = "Loại:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            cboLocLoai = new ComboBox { Width = 130, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(0, 4, 15, 0) };
            cboLocLoai.Items.AddRange(new object[] { "Tất cả", "Khen thưởng", "Kỷ luật" });
            cboLocLoai.SelectedIndex = 0;
            cboLocLoai.SelectedIndexChanged += (s, e) => LoadQuyetDinhList();
            pnlSearch.Controls.Add(cboLocLoai);

            pnlSearch.Controls.Add(new Label { Text = "Trạng thái:", AutoSize = true, Margin = new Padding(0, 8, 8, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            cboLocTrangThai = new ComboBox { Width = 110, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(0, 4, 15, 0) };
            cboLocTrangThai.Items.AddRange(new object[] { "Tất cả", "Nháp", "Đã ký" });
            cboLocTrangThai.SelectedIndex = 0;
            cboLocTrangThai.SelectedIndexChanged += (s, e) => LoadQuyetDinhList();
            pnlSearch.Controls.Add(cboLocTrangThai);

            var btnReload = new Button { Text = "Làm mới", Width = 100, Margin = new Padding(0, 2, 10, 0) };
            btnReload.Click += (s, e) => FormHelper.Run(this, LoadQuyetDinhList);
            pnlSearch.Controls.Add(btnReload);
            layout.Controls.Add(pnlSearch, 0, 0);

            // 2. Grids Split
            var splitGrids = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical, SplitterDistance = 640 };

            var grpQD = new GroupBox { Text = " Danh sách Quyết định Thưởng / Phạt ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvQuyetDinh = new DataGridView { Dock = DockStyle.Fill };
            dgvQuyetDinh.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvQuyetDinh);
                if (row != null) {
                    selectedMaQD = Convert.ToInt32(row["MaQD"]);
                    string soQD = row["SoQD"].ToString();
                    string loai = row["LoaiQD"].ToString();
                    decimal tien = Convert.ToDecimal(row["SoTien"]);
                    string tt = row["TrangThai"].ToString();
                    lblSelectedQD.Text = $"Đang chọn: [{soQD}] {loai} • Số tiền: {FormHelper.Money(tien)} • Trạng thái: {tt}";
                    LoadPhanBoList(selectedMaQD);
                }
            };
            grpQD.Controls.Add(dgvQuyetDinh);
            splitGrids.Panel1.Controls.Add(grpQD);

            var grpPhanBo = new GroupBox { Text = " Chi tiết phân bổ cho nhân viên ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvPhanBo = new DataGridView { Dock = DockStyle.Fill };
            grpPhanBo.Controls.Add(dgvPhanBo);
            splitGrids.Panel2.Controls.Add(grpPhanBo);
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

            txtSoQD = new TextBox();
            cboLoaiQD = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cboLoaiQD.Items.AddRange(new object[] { "Khen thưởng", "Kỷ luật" });
            cboLoaiQD.SelectedIndex = 0;

            dtpNgayLap = new DateTimePicker { Format = DateTimePickerFormat.Short, Value = DateTime.Today };
            numSoTien = new NumericUpDown { Maximum = 1000000000, Increment = 500000, Value = 1000000, ThousandsSeparator = true };
            numSoTien.ValueChanged += (s, e) => TinhLaiPhanBoTapThe();

            // Panel đối tượng
            var pnlDoiTuong = new Panel { Dock = DockStyle.Fill };
            radCaNhan = new RadioButton { Text = "Cá nhân", Location = new Point(0, 2), AutoSize = true, Checked = true };
            radTapThe = new RadioButton { Text = "Tập thể (PB)", Location = new Point(85, 2), AutoSize = true };
            radCaNhan.CheckedChanged += (s, e) => OnDoiTuongChanged();
            pnlDoiTuong.Controls.Add(radCaNhan);
            pnlDoiTuong.Controls.Add(radTapThe);

            // Panel chọn NV / PB
            var pnlChonDoiTuong = new Panel { Dock = DockStyle.Fill };
            cboNhanVien = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList };
            cboPhongBan = new ComboBox { Dock = DockStyle.Fill, DropDownStyle = ComboBoxStyle.DropDownList, Visible = false };
            cboPhongBan.SelectedIndexChanged += (s, e) => {
                if (radTapThe.Checked && cboCachThanhToan.Text == "Qua lương") LoadNhanVienPhongBanNhap();
            };
            pnlChonDoiTuong.Controls.Add(cboNhanVien);
            pnlChonDoiTuong.Controls.Add(cboPhongBan);

            cboCachThanhToan = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
            cboCachThanhToan.Items.AddRange(new object[] { "Qua lương", "Trực tiếp" });
            cboCachThanhToan.SelectedIndex = 0;
            cboCachThanhToan.SelectedIndexChanged += (s, e) => {
                bool quaLuong = cboCachThanhToan.Text == "Qua lương";
                dtpKyApDung.Enabled = quaLuong;
                if (quaLuong && radTapThe.Checked) LoadNhanVienPhongBanNhap();
            };

            dtpKyApDung = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "MM/yyyy" };
            txtNoiDung = new TextBox { Text = "Khen thưởng hoàn thành xuất sắc nhiệm vụ" };

            // Row 0
            editor.Controls.Add(FormHelper.CreateField("Số QĐ *", txtSoQD), 0, 0);
            editor.Controls.Add(FormHelper.CreateField("Loại QĐ *", cboLoaiQD), 1, 0);
            editor.Controls.Add(FormHelper.CreateField("Ngày lập *", dtpNgayLap), 2, 0);
            editor.Controls.Add(FormHelper.CreateField("Số tiền (đ) *", numSoTien), 3, 0);

            // Row 1
            editor.Controls.Add(FormHelper.CreateField("Hình thức nhận", pnlDoiTuong), 0, 1);
            editor.Controls.Add(FormHelper.CreateField("Cá nhân / Phòng ban *", pnlChonDoiTuong), 1, 1);
            editor.Controls.Add(FormHelper.CreateField("Thanh toán *", cboCachThanhToan), 2, 1);
            editor.Controls.Add(FormHelper.CreateField("Kỳ lương áp dụng", dtpKyApDung), 3, 1);
            layout.Controls.Add(editor, 0, 2);

            // 4. Action Buttons & Director Sign
            var actions = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false, AutoScroll = true };
            var btnLuuQDDraft = new Button { Text = "1. Lập quyết định (Nháp)", Width = 210, Margin = new Padding(0, 4, 15, 0) };
            btnLuuQDDraft.Click += (s, e) => {
                int? maNV = radCaNhan.Checked ? (int?)Convert.ToInt32(cboNhanVien.SelectedValue) : null;
                int? maPB = radTapThe.Checked ? (int?)Convert.ToInt32(cboPhongBan.SelectedValue) : null;

                var dictPhanBo = new Dictionary<int, decimal>();
                if (cboCachThanhToan.Text == "Qua lương") {
                    if (radCaNhan.Checked) {
                        if (!maNV.HasValue) { MessageBox.Show(this, "Chưa chọn nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                        dictPhanBo.Add(maNV.Value, numSoTien.Value);
                    } else {
                        if (dtNhapPhanBo == null || dtNhapPhanBo.Rows.Count == 0) {
                            MessageBox.Show(this, "Phòng ban không có nhân viên nào để phân bổ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
                        }
                        foreach (DataRow r in dtNhapPhanBo.Rows) {
                            int id = Convert.ToInt32(r["MaNV"]);
                            decimal val = Convert.ToDecimal(r["SoTienPhanBo"]);
                            dictPhanBo.Add(id, val);
                        }
                    }
                }

                FormHelper.Run(this, () => {
                    service.LapQuyetDinh(
                        txtSoQD.Text, cboLoaiQD.Text, maNV, maPB, dtpNgayLap.Value,
                        txtNoiDung.Text, numSoTien.Value, cboCachThanhToan.Text,
                        cboCachThanhToan.Text == "Qua lương" ? (DateTime?)dtpKyApDung.Value : null,
                        dictPhanBo);

                    FormHelper.Success(this, "Lập quyết định thành công ở trạng thái Nháp! Chờ Giám đốc ký duyệt.");
                    txtSoQD.Clear();
                    LoadQuyetDinhList();
                });
            };
            actions.Controls.Add(btnLuuQDDraft);

            actions.Controls.Add(new Label { Text = "Người ký (GĐ):", AutoSize = true, Margin = new Padding(0, 10, 5, 0), Font = new Font("Segoe UI", 9.5F, FontStyle.Bold), UseMnemonic = false });
            cboNguoiKy = new ComboBox { Width = 180, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(0, 6, 10, 0) };
            actions.Controls.Add(cboNguoiKy);

            dtpNgayKy = new DateTimePicker { Width = 115, Format = DateTimePickerFormat.Short, Value = DateTime.Today, Margin = new Padding(0, 6, 12, 0) };
            actions.Controls.Add(dtpNgayKy);

            var btnKyDuyet = new Button { Text = "2. Giám đốc Ký duyệt (Khóa)", Width = 230, Margin = new Padding(0, 4, 10, 0), BackColor = Color.FromArgb(39, 174, 96) };
            btnKyDuyet.Click += (s, e) => {
                if (selectedMaQD <= 0) { MessageBox.Show(this, "Vui lòng chọn quyết định cần ký trong bảng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (cboNguoiKy.SelectedValue == null) { MessageBox.Show(this, "Vui lòng chọn người ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                int maNguoiKy = Convert.ToInt32(cboNguoiKy.SelectedValue);

                FormHelper.Run(this, () => {
                    service.KyQuyetDinh(selectedMaQD, maNguoiKy, dtpNgayKy.Value);
                    FormHelper.Success(this, "Ký duyệt quyết định thành công! Nội dung và phân bổ đã được khóa an toàn.");
                    LoadQuyetDinhList();
                    LoadPhanBoList(selectedMaQD);
                });
            };
            actions.Controls.Add(btnKyDuyet);

            var btnXoaDraft = new Button { Text = "Xóa QĐ Nháp", Width = 120, Margin = new Padding(0, 4, 10, 0), BackColor = Color.FromArgb(192, 57, 43) };
            btnXoaDraft.Click += (s, e) => {
                if (selectedMaQD <= 0) { MessageBox.Show(this, "Vui lòng chọn quyết định cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (MessageBox.Show(this, "Bạn có chắc chắn muốn xóa quyết định Nháp này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    FormHelper.Run(this, () => {
                        service.XoaQuyetDinhNhap(selectedMaQD);
                        FormHelper.Success(this, "Đã xóa quyết định Nháp thành công!");
                        selectedMaQD = 0;
                        LoadQuyetDinhList();
                    });
                }
            };
            actions.Controls.Add(btnXoaDraft);
            layout.Controls.Add(actions, 0, 3);

            // 5. Status
            lblSelectedQD = new Label {
                Text = "Quyết định khen thưởng / kỷ luật sau khi Giám đốc ký duyệt sẽ được khóa cố định.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = FormHelper.Ink,
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                UseMnemonic = false
            };
            layout.Controls.Add(lblSelectedQD, 0, 4);

            root.Controls.Add(layout, 0, 1);
            this.Controls.Add(root);
        }

        private void OnDoiTuongChanged()
        {
            if (radCaNhan.Checked) {
                cboNhanVien.Visible = true;
                cboPhongBan.Visible = false;
            } else {
                cboNhanVien.Visible = false;
                cboPhongBan.Visible = true;
                if (cboCachThanhToan.Text == "Qua lương") {
                    LoadNhanVienPhongBanNhap();
                }
            }
        }

        private void LoadNhanVienPhongBanNhap()
        {
            if (cboPhongBan.SelectedValue == null) return;
            if (!int.TryParse(cboPhongBan.SelectedValue.ToString(), out int maPB)) return;

            var dt = service.GetNhanVienByPhongBan(maPB);
            dtNhapPhanBo = new DataTable();
            dtNhapPhanBo.Columns.Add("MaNV", typeof(int));
            dtNhapPhanBo.Columns.Add("HoTen", typeof(string));
            dtNhapPhanBo.Columns.Add("SoTienPhanBo", typeof(decimal));

            if (dt.Rows.Count > 0) {
                decimal tongTien = numSoTien.Value;
                decimal chiaDeu = Math.Floor(tongTien / dt.Rows.Count);
                decimal du = tongTien - (chiaDeu * dt.Rows.Count);

                for (int i = 0; i < dt.Rows.Count; i++) {
                    decimal tienDauNguoi = chiaDeu + (i == 0 ? du : 0);
                    dtNhapPhanBo.Rows.Add(dt.Rows[i]["MaNV"], dt.Rows[i]["HoTen"], tienDauNguoi);
                }
            }
        }

        private void TinhLaiPhanBoTapThe()
        {
            if (radTapThe.Checked && cboCachThanhToan.Text == "Qua lương")
                LoadNhanVienPhongBanNhap();
        }

        private void LoadData()
        {
            var dtNV = hoSoService.GetAllNhanVien(null, "Tất cả");
            FormHelper.Bind(cboNhanVien, dtNV.Copy(), "MaNV", "HoTen");
            FormHelper.Bind(cboNguoiKy, dtNV.Copy(), "MaNV", "HoTen");

            var dtPB = danhMucService.GetAllPhongBan();
            FormHelper.Bind(cboPhongBan, dtPB, "MaPhongBan", "TenPhongBan");

            LoadQuyetDinhList();
        }

        private void LoadQuyetDinhList()
        {
            string loai = cboLocLoai.SelectedItem?.ToString();
            string trangThai = cboLocTrangThai.SelectedItem?.ToString();
            string search = txtSearch.Text;

            dgvQuyetDinh.DataSource = service.GetAllQuyetDinhThuongPhat(loai, trangThai, search);
            FormHelper.Columns(dgvQuyetDinh,
                "SoQD", "Số QĐ",
                "LoaiQD", "Loại",
                "SoTien", "Số tiền",
                "CachThanhToan", "Hình thức",
                "KyApDung", "Kỳ",
                "TrangThai", "Trạng thái");
        }

        private void LoadPhanBoList(int maQD)
        {
            dgvPhanBo.DataSource = service.GetChiTietPhanBo(maQD);
            FormHelper.Columns(dgvPhanBo,
                "MaNV", "Mã NV",
                "HoTen", "Nhân viên",
                "SoTien", "Tiền phân bổ");
        }
    }
}
