using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Services;

namespace QuanLyNhanSu.Forms
{
    public class FrmBangLuong : Form
    {
        private readonly BangLuongService service = new BangLuongService();

        private NumericUpDown numThang, numNam, numGioChuanH;
        private Label lblTrangThaiKy, lblTongKet;
        private Button btnTinhNhap, btnKiemTraChot, btnChotLuong, btnTaiLai;
        private DataGridView dgvChiTiet;
        private DataGridView dgvLichSuBangLuong;
        private TextBox txtPhieuLuongChiTiet;

        private int currentMaBangLuong = 0;
        private string currentTrangThai = "Chưa lập";

        public FrmBangLuong()
        {
            InitializeComponent();
            FormHelper.Style(this);
            Shown += (s, e) => FormHelper.Run(this, () => {
                LoadBangLuongList();
                CheckCurrentPayrollStatus();
            });
        }

        private void InitializeComponent()
        {
            this.Text = "Bảng lương & Phiếu lương";
            this.ClientSize = new Size(1200, 790);
            this.MinimumSize = new Size(1050, 680);

            var root = new TableLayoutPanel {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 12, 20, 15),
                ColumnCount = 1,
                RowCount = 3
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));  // Title
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));  // Action Bar
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Split middle

            var lblTitle = new Label {
                Text = "Quản lý Bảng Lương Tháng & Phiếu Lương Chi Tiết",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 65, 94),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };
            root.Controls.Add(lblTitle, 0, 0);

            // 1. Action Bar (2 rows of controls)
            var pnlTop = new Panel { Dock = DockStyle.Fill };

            var pnlRow1 = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 42, WrapContents = false, AutoScroll = true };
            pnlRow1.Controls.Add(new Label { Text = "Tháng:", AutoSize = true, Margin = new Padding(0, 8, 5, 0), Font = new Font("Segoe UI", 10F, FontStyle.Bold), UseMnemonic = false });
            numThang = new NumericUpDown { Width = 55, Minimum = 1, Maximum = 12, Value = 9, Margin = new Padding(0, 4, 15, 0) };
            numThang.ValueChanged += (s, e) => CheckCurrentPayrollStatus();
            pnlRow1.Controls.Add(numThang);

            pnlRow1.Controls.Add(new Label { Text = "Năm:", AutoSize = true, Margin = new Padding(0, 8, 5, 0), Font = new Font("Segoe UI", 10F, FontStyle.Bold), UseMnemonic = false });
            numNam = new NumericUpDown { Width = 75, Minimum = 2000, Maximum = 2100, Value = 2026, Margin = new Padding(0, 4, 15, 0) };
            numNam.ValueChanged += (s, e) => CheckCurrentPayrollStatus();
            pnlRow1.Controls.Add(numNam);

            pnlRow1.Controls.Add(new Label { Text = "Giờ chuẩn tháng (H):", AutoSize = true, Margin = new Padding(0, 8, 5, 0), Font = new Font("Segoe UI", 10F, FontStyle.Bold), UseMnemonic = false });
            numGioChuanH = new NumericUpDown { Width = 70, DecimalPlaces = 1, Minimum = 1, Maximum = 744, Value = 176, Margin = new Padding(0, 4, 15, 0) };
            pnlRow1.Controls.Add(numGioChuanH);

            lblTrangThaiKy = new Label {
                Text = "Trạng thái kỳ: [CHƯA LẬP]",
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 0),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = FormHelper.Primary,
                UseMnemonic = false
            };
            pnlRow1.Controls.Add(lblTrangThaiKy);
            pnlTop.Controls.Add(pnlRow1);

            var pnlRow2 = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 45, WrapContents = false, AutoScroll = true };
            btnTinhNhap = new Button { Text = "1. TÍNH NHÁP BẢNG LƯƠNG", Width = 215, Margin = new Padding(0, 2, 10, 0) };
            btnTinhNhap.Click += (s, e) => FormHelper.Run(this, () => {
                service.TinhVaLuuBangLuong((int)numThang.Value, (int)numNam.Value, numGioChuanH.Value, false);
                FormHelper.Success(this, "Tính nháp bảng lương thành công! Dữ liệu đã được lưu tạm ở trạng thái 'Nháp'.");
                CheckCurrentPayrollStatus();
                LoadBangLuongList();
            });
            pnlRow2.Controls.Add(btnTinhNhap);

            btnKiemTraChot = new Button { Text = "2. KIỂM TRA ĐIỀU KIỆN CHỐT", Width = 230, Margin = new Padding(0, 2, 10, 0), BackColor = Color.FromArgb(243, 156, 18) };
            btnKiemTraChot.Click += (s, e) => {
                var errs = service.KiemTraDieuKienChotLuong((int)numThang.Value, (int)numNam.Value);
                if (errs.Count == 0) {
                    MessageBox.Show(this, "TẤT CẢ ĐIỀU KIỆN ĐÃ ĐẠT!\n- Đã hết tháng.\n- Đã chấm công đầy đủ (không còn giờ NULL).\n- Không còn phiếu phụ trội Kế hoạch.\n- Không còn quyết định Nháp qua lương thuộc kỳ.\n\n-> Kỳ lương đã sẵn sàng để CHỐT KHÓA.", "Kiểm tra hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    string msg = "CHƯA ĐỦ ĐIỀU KIỆN ĐỂ CHỐT LƯƠNG:\n\n" + string.Join("\n\n", errs);
                    MessageBox.Show(this, msg, "Cảnh báo điều kiện", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            pnlRow2.Controls.Add(btnKiemTraChot);

            btnChotLuong = new Button { Text = "3. CHỐT BẢNG LƯƠNG (KHÓA KỲ)", Width = 260, Margin = new Padding(0, 2, 10, 0), BackColor = Color.FromArgb(192, 57, 43) };
            btnChotLuong.Click += (s, e) => {
                var confirm = MessageBox.Show(this,
                    $"Bạn có chắc chắn muốn CHỐT BẢNG LƯƠNG tháng {(int)numThang.Value:D2}/{numNam.Value}?\n\nTheo quy định:\n- Hệ thống sẽ tính lại toàn bộ từ nguồn đã khóa.\n- Cập nhật Mã bảng lương vào từng phân bổ thưởng/phạt.\n- Khóa vĩnh viễn kỳ lương, KHÔNG CHO PHÉP CHỈNH SỬA nữa.",
                    "Xác nhận Chốt Kỳ Lương", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes) {
                    FormHelper.Run(this, () => {
                        service.TinhVaLuuBangLuong((int)numThang.Value, (int)numNam.Value, numGioChuanH.Value, true);
                        FormHelper.Success(this, "CHỐT BẢNG LƯƠNG THÀNH CÔNG! Kỳ lương đã chuyển sang trạng thái 'Đã chốt' và bị khóa an toàn.");
                        CheckCurrentPayrollStatus();
                        LoadBangLuongList();
                    });
                }
            };
            pnlRow2.Controls.Add(btnChotLuong);

            btnTaiLai = new Button { Text = "Xem lại", Width = 95, Margin = new Padding(0, 2, 10, 0) };
            btnTaiLai.Click += (s, e) => FormHelper.Run(this, CheckCurrentPayrollStatus);
            pnlRow2.Controls.Add(btnTaiLai);

            pnlTop.Controls.Add(pnlRow2);
            root.Controls.Add(pnlTop, 0, 1);

            // 2. Split Middle
            var splitMiddle = new SplitContainer { Dock = DockStyle.Fill, Orientation = Orientation.Vertical, SplitterDistance = 670 };

            // Left: Grid chi tiết & Tổng kết
            var pnlLeft = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2 };
            pnlLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));

            var grpChiTiet = new GroupBox { Text = " Chi tiết Bảng Lương tháng đang chọn ", Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F, FontStyle.Bold) };
            dgvChiTiet = new DataGridView { Dock = DockStyle.Fill };
            dgvChiTiet.CellClick += (s, e) => {
                if (e.RowIndex >= 0) HienThiPhieuLuongCaNhan(e.RowIndex);
            };
            grpChiTiet.Controls.Add(dgvChiTiet);
            pnlLeft.Controls.Add(grpChiTiet, 0, 0);

            lblTongKet = new Label {
                Text = "Tổng thực lĩnh kỳ: 0 VNĐ | Tổng nhân sự: 0 người",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = FormHelper.Ink,
                UseMnemonic = false
            };
            pnlLeft.Controls.Add(lblTongKet, 0, 1);
            splitMiddle.Panel1.Controls.Add(pnlLeft);

            // Right: TabControl (Phiếu lương cá nhân / Lịch sử các kỳ)
            var tabRight = new TabControl { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9.5F) };

            var tabPhieu = new TabPage("Phiếu Lương Chi Tiết");
            txtPhieuLuongChiTiet = new TextBox {
                Dock = DockStyle.Fill, Multiline = true, ReadOnly = true, ScrollBars = ScrollBars.Vertical,
                BackColor = Color.FromArgb(250, 252, 255), Font = new Font("Consolas", 9.5F), ForeColor = FormHelper.Ink
            };
            tabPhieu.Controls.Add(txtPhieuLuongChiTiet);
            tabRight.TabPages.Add(tabPhieu);

            var tabLichSu = new TabPage("Lịch Sử Các Kỳ");
            dgvLichSuBangLuong = new DataGridView { Dock = DockStyle.Fill };
            dgvLichSuBangLuong.CellClick += (s, e) => {
                var row = FormHelper.Row(dgvLichSuBangLuong);
                if (row != null) {
                    numThang.Value = Convert.ToInt32(row["Thang"]);
                    numNam.Value = Convert.ToInt32(row["Nam"]);
                }
            };
            tabLichSu.Controls.Add(dgvLichSuBangLuong);
            tabRight.TabPages.Add(tabLichSu);

            splitMiddle.Panel2.Controls.Add(tabRight);
            root.Controls.Add(splitMiddle, 0, 2);

            this.Controls.Add(root);
        }

        private void CheckCurrentPayrollStatus()
        {
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;

            var dt = service.GetBangLuongByThangNam(thang, nam);
            if (dt.Rows.Count > 0)
            {
                currentMaBangLuong = Convert.ToInt32(dt.Rows[0]["MaBangLuong"]);
                currentTrangThai = dt.Rows[0]["TrangThai"].ToString();
                numGioChuanH.Value = Convert.ToDecimal(dt.Rows[0]["GioChuanThang"]);

                lblTrangThaiKy.Text = $"Trạng thái kỳ {thang:D2}/{nam}: [{currentTrangThai.ToUpper()}] (Mã BL: {currentMaBangLuong})";
                if (currentTrangThai == "Đã chốt")
                {
                    lblTrangThaiKy.ForeColor = Color.FromArgb(192, 57, 43);
                    btnTinhNhap.Enabled = false;
                    btnChotLuong.Enabled = false;
                }
                else
                {
                    lblTrangThaiKy.ForeColor = FormHelper.Primary;
                    btnTinhNhap.Enabled = true;
                    btnChotLuong.Enabled = true;
                }

                LoadChiTietGrid(currentMaBangLuong);
            }
            else
            {
                currentMaBangLuong = 0;
                currentTrangThai = "Chưa lập";
                lblTrangThaiKy.Text = $"Trạng thái kỳ {thang:D2}/{nam}: [CHƯA LẬP]";
                lblTrangThaiKy.ForeColor = Color.Gray;
                btnTinhNhap.Enabled = true;
                btnChotLuong.Enabled = true;
                dgvChiTiet.DataSource = null;
                txtPhieuLuongChiTiet.Clear();
                lblTongKet.Text = "Tổng thực lĩnh: 0 VNĐ | Số nhân viên: 0 người";
            }
        }

        private void LoadChiTietGrid(int maBangLuong)
        {
            var dt = service.GetChiTietBangLuong(maBangLuong);
            dgvChiTiet.DataSource = dt;

            decimal tong = 0;
            foreach (DataRow r in dt.Rows) tong += Convert.ToDecimal(r["ThucLinh"]);
            lblTongKet.Text = $"Tổng thực lĩnh kỳ: {FormHelper.Money(tong)} | Tổng nhân sự: {dt.Rows.Count} người";

            FormHelper.Columns(dgvChiTiet,
                "MaNV", "Mã NV",
                "HoTen", "Nhân viên",
                "TenPhongBan", "Phòng ban",
                "TienCong", "Tiền công",
                "TienPhuTroi", "Phụ trội",
                "PhuCap", "Phụ cấp",
                "ThucLinh", "THỰC LĨNH");

            if (dt.Rows.Count > 0)
                HienThiPhieuLuongCaNhan(0);
        }

        private void HienThiPhieuLuongCaNhan(int rowIndex)
        {
            if (dgvChiTiet.Rows.Count <= rowIndex) return;
            var r = dgvChiTiet.Rows[rowIndex];

            int maNV = Convert.ToInt32(r.Cells["MaNV"].Value);
            string hoTen = r.Cells["HoTen"].Value.ToString();
            string pb = r.Cells["TenPhongBan"].Value?.ToString();
            decimal gioThuong = Convert.ToDecimal(r.Cells["GioThuong"].Value);
            decimal gioPT = Convert.ToDecimal(r.Cells["GioPhuTroi"].Value);
            decimal tienCong = Convert.ToDecimal(r.Cells["TienCong"].Value);
            decimal tienPT = Convert.ToDecimal(r.Cells["TienPhuTroi"].Value);
            decimal phuCap = Convert.ToDecimal(r.Cells["PhuCap"].Value);
            decimal thuong = Convert.ToDecimal(r.Cells["TienThuong"].Value);
            decimal phat = Convert.ToDecimal(r.Cells["TienPhat"].Value);
            decimal bhxh = Convert.ToDecimal(r.Cells["TruBHXH"].Value);
            decimal bhyt = Convert.ToDecimal(r.Cells["TruBHYT"].Value);
            decimal thucLinh = Convert.ToDecimal(r.Cells["ThucLinh"].Value);

            txtPhieuLuongChiTiet.Text =
                $"==========================================\r\n" +
                $"         PHIẾU LƯƠNG NHÂN VIÊN           \r\n" +
                $"       Kỳ: {numThang.Value:D2}/{numNam.Value} (Trạng thái: {currentTrangThai})\r\n" +
                $"==========================================\r\n" +
                $"Mã NV:        NV{maNV:D3}\r\n" +
                $"Họ và tên:    {hoTen}\r\n" +
                $"Phòng ban:    {pb}\r\n" +
                $"------------------------------------------\r\n" +
                $"1. THỜI GIAN LÀM VIỆC:\r\n" +
                $"   - Giờ thường chuẩn:   {gioThuong,8:N1} giờ\r\n" +
                $"   - Giờ phụ trội (x1.5):{gioPT,8:N1} giờ\r\n" +
                $"------------------------------------------\r\n" +
                $"2. CÁC KHOẢN THU NHẬP:\r\n" +
                $"   (+) Tiền công thường: {tienCong,14:N0} đ\r\n" +
                $"   (+) Tiền phụ trội:    {tienPT,14:N0} đ\r\n" +
                $"   (+) Phụ cấp:          {phuCap,14:N0} đ\r\n" +
                $"   (+) Khen thưởng:      {thuong,14:N0} đ\r\n" +
                $"------------------------------------------\r\n" +
                $"3. CÁC KHOẢN KHẤU TRỪ:\r\n" +
                $"   (-) Kỷ luật (Phạt):   {phat,14:N0} đ\r\n" +
                $"   (-) Bảo hiểm XH (8%): {bhxh,14:N0} đ\r\n" +
                $"   (-) Bảo hiểm YT(1.5%):{bhyt,14:N0} đ\r\n" +
                $"==========================================\r\n" +
                $"==> THỰC LĨNH:           {thucLinh,14:N0} đ\r\n" +
                $"==========================================\r\n" +
                $" (Tính toán tự động theo quy định kỳ lương)";
        }

        private void LoadBangLuongList()
        {
            dgvLichSuBangLuong.DataSource = service.GetAllBangLuong();
            FormHelper.Columns(dgvLichSuBangLuong,
                "MaBangLuong", "Mã BL",
                "KyLuong", "Kỳ",
                "NgayLap", "Ngày lập",
                "TongThucLinh", "Tổng lĩnh",
                "TrangThai", "Trạng thái",
                "NgayChot", "Ngày chốt");
        }
    }
}
