namespace QuanLyKhachSan.Forms
{
    partial class FrmDatPhong
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabKhach = new System.Windows.Forms.TabPage();
            this.tabDatPhong = new System.Windows.Forms.TabPage();
            this.tabNhanPhong = new System.Windows.Forms.TabPage();

            this.cboKhach = new System.Windows.Forms.ComboBox(); this.cboNV = new System.Windows.Forms.ComboBox(); this.cboKenh = new System.Windows.Forms.ComboBox();
            this.dgvKhach = new System.Windows.Forms.DataGridView(); this.dgvPhong = new System.Windows.Forms.DataGridView(); this.dgvPhieu = new System.Windows.Forms.DataGridView(); this.dgvChon = new System.Windows.Forms.DataGridView(); this.dgvCT = new System.Windows.Forms.DataGridView(); this.dgvNguoi = new System.Windows.Forms.DataGridView();
            this.txtMaKH = new System.Windows.Forms.TextBox(); this.txtTenKH = new System.Windows.Forms.TextBox(); this.txtCMND = new System.Windows.Forms.TextBox(); this.txtQT = new System.Windows.Forms.TextBox(); this.txtSDT = new System.Windows.Forms.TextBox(); this.txtSoPhieu = new System.Windows.Forms.TextBox(); this.txtPhieuChon = new System.Windows.Forms.TextBox(); this.txtNguoiPhong = new System.Windows.Forms.TextBox(); this.txtNguoiTen = new System.Windows.Forms.TextBox(); this.txtNguoiCMND = new System.Windows.Forms.TextBox(); this.txtNguoiQT = new System.Windows.Forms.TextBox();
            this.dtLap = new System.Windows.Forms.DateTimePicker(); this.dtNhan = new System.Windows.Forms.DateTimePicker(); this.dtTra = new System.Windows.Forms.DateTimePicker();
            this.numCoc = new System.Windows.Forms.NumericUpDown(); this.numSoNguoi = new System.Windows.Forms.NumericUpDown();
            this.btnThemKhach = new System.Windows.Forms.Button(); this.btnThemPhong = new System.Windows.Forms.Button(); this.btnBoPhong = new System.Windows.Forms.Button(); this.btnLapPhieu = new System.Windows.Forms.Button(); this.btnThemNguoi = new System.Windows.Forms.Button(); this.btnNhanPhong = new System.Windows.Forms.Button(); this.btnNoShow = new System.Windows.Forms.Button(); this.btnDong = new System.Windows.Forms.Button();

            // Khai báo Label an toàn cho Designer
            System.Windows.Forms.Label lbl1 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl4 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl5 = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lbl6 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl7 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl8 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl9 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl10 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl11 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl12 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl13 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl14 = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lbl15 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl16 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl17 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl18 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl19 = new System.Windows.Forms.Label();

            this.tabControl1.SuspendLayout(); this.tabKhach.SuspendLayout(); this.tabDatPhong.SuspendLayout(); this.tabNhanPhong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhach)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvPhieu)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvChon)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvCT)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvNguoi)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numCoc)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numSoNguoi)).BeginInit();
            this.SuspendLayout();

            this.tabControl1.Controls.Add(this.tabKhach); this.tabControl1.Controls.Add(this.tabDatPhong); this.tabControl1.Controls.Add(this.tabNhanPhong);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top; this.tabControl1.Location = new System.Drawing.Point(0, 0); this.tabControl1.Size = new System.Drawing.Size(1000, 620);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F);

            // ================= TAB KHÁCH HÀNG =================
            this.tabKhach.Text = "Khách hàng"; this.tabKhach.BackColor = System.Drawing.Color.WhiteSmoke;
            lbl1.Text = "Mã KH:"; lbl1.Location = new System.Drawing.Point(20, 20); lbl1.AutoSize = true; this.tabKhach.Controls.Add(lbl1);
            this.txtMaKH.Location = new System.Drawing.Point(20, 45); this.txtMaKH.Size = new System.Drawing.Size(120, 25); this.tabKhach.Controls.Add(this.txtMaKH);

            lbl2.Text = "Tên Khách Hàng:"; lbl2.Location = new System.Drawing.Point(160, 20); lbl2.AutoSize = true; this.tabKhach.Controls.Add(lbl2);
            this.txtTenKH.Location = new System.Drawing.Point(160, 45); this.txtTenKH.Size = new System.Drawing.Size(200, 25); this.tabKhach.Controls.Add(this.txtTenKH);

            lbl3.Text = "CCCD/CMND:"; lbl3.Location = new System.Drawing.Point(380, 20); lbl3.AutoSize = true; this.tabKhach.Controls.Add(lbl3);
            this.txtCMND.Location = new System.Drawing.Point(380, 45); this.txtCMND.Size = new System.Drawing.Size(150, 25); this.tabKhach.Controls.Add(this.txtCMND);

            lbl4.Text = "Quốc tịch:"; lbl4.Location = new System.Drawing.Point(550, 20); lbl4.AutoSize = true; this.tabKhach.Controls.Add(lbl4);
            this.txtQT.Location = new System.Drawing.Point(550, 45); this.txtQT.Size = new System.Drawing.Size(120, 25); this.tabKhach.Controls.Add(this.txtQT);

            lbl5.Text = "Số điện thoại:"; lbl5.Location = new System.Drawing.Point(690, 20); lbl5.AutoSize = true; this.tabKhach.Controls.Add(lbl5);
            this.txtSDT.Location = new System.Drawing.Point(690, 45); this.txtSDT.Size = new System.Drawing.Size(140, 25); this.tabKhach.Controls.Add(this.txtSDT);

            this.btnThemKhach.Location = new System.Drawing.Point(850, 40); this.btnThemKhach.Size = new System.Drawing.Size(120, 35); this.btnThemKhach.Text = "Lưu Khách"; this.btnThemKhach.BackColor = System.Drawing.Color.FromArgb(0, 122, 204); this.btnThemKhach.ForeColor = System.Drawing.Color.White; this.btnThemKhach.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnThemKhach.Click += new System.EventHandler(this.btnThemKhach_Click); this.tabKhach.Controls.Add(this.btnThemKhach);

            this.dgvKhach.Location = new System.Drawing.Point(20, 95); this.dgvKhach.Size = new System.Drawing.Size(950, 470); this.dgvKhach.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvKhach.BackgroundColor = System.Drawing.Color.White; this.dgvKhach.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; this.tabKhach.Controls.Add(this.dgvKhach);

            // ================= TAB ĐẶT PHÒNG =================
            this.tabDatPhong.Text = "Đặt phòng"; this.tabDatPhong.BackColor = System.Drawing.Color.WhiteSmoke;

            lbl6.Text = "Số phiếu:"; lbl6.Location = new System.Drawing.Point(20, 15); lbl6.AutoSize = true; this.tabDatPhong.Controls.Add(lbl6);
            this.txtSoPhieu.Location = new System.Drawing.Point(20, 40); this.txtSoPhieu.Size = new System.Drawing.Size(120, 25); this.tabDatPhong.Controls.Add(this.txtSoPhieu);

            lbl7.Text = "Khách hàng:"; lbl7.Location = new System.Drawing.Point(160, 15); lbl7.AutoSize = true; this.tabDatPhong.Controls.Add(lbl7);
            this.cboKhach.Location = new System.Drawing.Point(160, 40); this.cboKhach.Size = new System.Drawing.Size(200, 25); this.cboKhach.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.tabDatPhong.Controls.Add(this.cboKhach);

            lbl8.Text = "NV Lập:"; lbl8.Location = new System.Drawing.Point(380, 15); lbl8.AutoSize = true; this.tabDatPhong.Controls.Add(lbl8);
            this.cboNV.Location = new System.Drawing.Point(380, 40); this.cboNV.Size = new System.Drawing.Size(150, 25); this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.tabDatPhong.Controls.Add(this.cboNV);

            lbl9.Text = "Kênh đặt:"; lbl9.Location = new System.Drawing.Point(550, 15); lbl9.AutoSize = true; this.tabDatPhong.Controls.Add(lbl9);
            this.cboKenh.Location = new System.Drawing.Point(550, 40); this.cboKenh.Size = new System.Drawing.Size(120, 25); this.cboKenh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.tabDatPhong.Controls.Add(this.cboKenh);

            lbl10.Text = "Tiền cọc (VNĐ):"; lbl10.Location = new System.Drawing.Point(690, 15); lbl10.AutoSize = true; this.tabDatPhong.Controls.Add(lbl10);
            this.numCoc.Location = new System.Drawing.Point(690, 40); this.numCoc.Size = new System.Drawing.Size(120, 25); this.numCoc.Maximum = 100000000; this.tabDatPhong.Controls.Add(this.numCoc);

            lbl11.Text = "Ngày lập:"; lbl11.Location = new System.Drawing.Point(20, 75); lbl11.AutoSize = true; this.tabDatPhong.Controls.Add(lbl11);
            this.dtLap.Location = new System.Drawing.Point(20, 100); this.dtLap.Size = new System.Drawing.Size(120, 25); this.dtLap.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.tabDatPhong.Controls.Add(this.dtLap);

            lbl12.Text = "Ngày nhận:"; lbl12.Location = new System.Drawing.Point(160, 75); lbl12.AutoSize = true; this.tabDatPhong.Controls.Add(lbl12);
            this.dtNhan.Location = new System.Drawing.Point(160, 100); this.dtNhan.Size = new System.Drawing.Size(120, 25); this.dtNhan.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.tabDatPhong.Controls.Add(this.dtNhan);

            lbl13.Text = "Ngày trả dự kiến:"; lbl13.Location = new System.Drawing.Point(300, 75); lbl13.AutoSize = true; this.tabDatPhong.Controls.Add(lbl13);
            this.dtTra.Location = new System.Drawing.Point(300, 100); this.dtTra.Size = new System.Drawing.Size(120, 25); this.dtTra.Format = System.Windows.Forms.DateTimePickerFormat.Short; this.tabDatPhong.Controls.Add(this.dtTra);

            this.btnLapPhieu.Location = new System.Drawing.Point(820, 90); this.btnLapPhieu.Size = new System.Drawing.Size(150, 35); this.btnLapPhieu.Text = "LẬP PHIẾU ĐẶT"; this.btnLapPhieu.BackColor = System.Drawing.Color.FromArgb(0, 150, 136); this.btnLapPhieu.ForeColor = System.Drawing.Color.White; this.btnLapPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnLapPhieu.Click += new System.EventHandler(this.btnLapPhieu_Click); this.tabDatPhong.Controls.Add(this.btnLapPhieu);

            this.dgvPhong.Location = new System.Drawing.Point(20, 145); this.dgvPhong.Size = new System.Drawing.Size(400, 200); this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvPhong.BackgroundColor = System.Drawing.Color.White; this.tabDatPhong.Controls.Add(this.dgvPhong);

            lbl14.Text = "Số người:"; lbl14.Location = new System.Drawing.Point(440, 170); lbl14.AutoSize = true; this.tabDatPhong.Controls.Add(lbl14);
            this.numSoNguoi.Location = new System.Drawing.Point(440, 195); this.numSoNguoi.Size = new System.Drawing.Size(80, 25); this.numSoNguoi.Minimum = 1; this.numSoNguoi.Value = 1; this.tabDatPhong.Controls.Add(this.numSoNguoi);

            this.btnThemPhong.Location = new System.Drawing.Point(440, 235); this.btnThemPhong.Size = new System.Drawing.Size(80, 35); this.btnThemPhong.Text = "Thêm >"; this.btnThemPhong.BackColor = System.Drawing.Color.FromArgb(224, 224, 224); this.btnThemPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnThemPhong.Click += new System.EventHandler(this.btnThemPhong_Click); this.tabDatPhong.Controls.Add(this.btnThemPhong);
            this.btnBoPhong.Location = new System.Drawing.Point(440, 280); this.btnBoPhong.Size = new System.Drawing.Size(80, 35); this.btnBoPhong.Text = "< Bỏ"; this.btnBoPhong.BackColor = System.Drawing.Color.FromArgb(224, 224, 224); this.btnBoPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnBoPhong.Click += new System.EventHandler(this.btnBoPhong_Click); this.tabDatPhong.Controls.Add(this.btnBoPhong);

            this.dgvChon.Location = new System.Drawing.Point(540, 145); this.dgvChon.Size = new System.Drawing.Size(430, 200); this.dgvChon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvChon.BackgroundColor = System.Drawing.Color.White; this.tabDatPhong.Controls.Add(this.dgvChon);

            this.dgvPhieu.Location = new System.Drawing.Point(20, 360); this.dgvPhieu.Size = new System.Drawing.Size(950, 205); this.dgvPhieu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvPhieu.BackgroundColor = System.Drawing.Color.White; this.dgvPhieu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; this.dgvPhieu.SelectionChanged += new System.EventHandler(this.dgvPhieu_SelectionChanged); this.tabDatPhong.Controls.Add(this.dgvPhieu);

            // ================= TAB NHẬN PHÒNG =================
            this.tabNhanPhong.Text = "Nhận phòng / Người lưu trú"; this.tabNhanPhong.BackColor = System.Drawing.Color.WhiteSmoke;

            lbl15.Text = "Phiếu đang chọn:"; lbl15.Location = new System.Drawing.Point(20, 25); lbl15.AutoSize = true; this.tabNhanPhong.Controls.Add(lbl15);
            this.txtPhieuChon.Location = new System.Drawing.Point(145, 22); this.txtPhieuChon.Size = new System.Drawing.Size(120, 25); this.txtPhieuChon.ReadOnly = true; this.tabNhanPhong.Controls.Add(this.txtPhieuChon);

            this.btnNhanPhong.Location = new System.Drawing.Point(285, 17); this.btnNhanPhong.Size = new System.Drawing.Size(120, 35); this.btnNhanPhong.Text = "Xác nhận Nhận"; this.btnNhanPhong.BackColor = System.Drawing.Color.FromArgb(76, 175, 80); this.btnNhanPhong.ForeColor = System.Drawing.Color.White; this.btnNhanPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnNhanPhong.Click += new System.EventHandler(this.btnNhanPhong_Click); this.tabNhanPhong.Controls.Add(this.btnNhanPhong);
            this.btnNoShow.Location = new System.Drawing.Point(420, 17); this.btnNoShow.Size = new System.Drawing.Size(100, 35); this.btnNoShow.Text = "No-Show"; this.btnNoShow.BackColor = System.Drawing.Color.FromArgb(244, 67, 54); this.btnNoShow.ForeColor = System.Drawing.Color.White; this.btnNoShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnNoShow.Click += new System.EventHandler(this.btnNoShow_Click); this.tabNhanPhong.Controls.Add(this.btnNoShow);

            // DGV Chi tiết đặt (Sử dụng AllCells để chữ không bị che)
            this.dgvCT.Location = new System.Drawing.Point(20, 70); this.dgvCT.Size = new System.Drawing.Size(430, 490); this.dgvCT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells; this.dgvCT.BackgroundColor = System.Drawing.Color.White; this.dgvCT.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left; this.tabNhanPhong.Controls.Add(this.dgvCT);

            lbl16.Text = "Phòng:"; lbl16.Location = new System.Drawing.Point(470, 75); lbl16.AutoSize = true; this.tabNhanPhong.Controls.Add(lbl16);
            this.txtNguoiPhong.Location = new System.Drawing.Point(530, 72); this.txtNguoiPhong.Size = new System.Drawing.Size(60, 25); this.tabNhanPhong.Controls.Add(this.txtNguoiPhong);

            lbl17.Text = "Tên:"; lbl17.Location = new System.Drawing.Point(600, 75); lbl17.AutoSize = true; this.tabNhanPhong.Controls.Add(lbl17);
            this.txtNguoiTen.Location = new System.Drawing.Point(640, 72); this.txtNguoiTen.Size = new System.Drawing.Size(150, 25); this.tabNhanPhong.Controls.Add(this.txtNguoiTen);

            lbl18.Text = "CCCD:"; lbl18.Location = new System.Drawing.Point(800, 75); lbl18.AutoSize = true; this.tabNhanPhong.Controls.Add(lbl18);
            this.txtNguoiCMND.Location = new System.Drawing.Point(850, 72); this.txtNguoiCMND.Size = new System.Drawing.Size(120, 25); this.tabNhanPhong.Controls.Add(this.txtNguoiCMND);

            lbl19.Text = "Quốc tịch:"; lbl19.Location = new System.Drawing.Point(470, 115); lbl19.AutoSize = true; this.tabNhanPhong.Controls.Add(lbl19);
            this.txtNguoiQT.Location = new System.Drawing.Point(550, 112); this.txtNguoiQT.Size = new System.Drawing.Size(100, 25); this.tabNhanPhong.Controls.Add(this.txtNguoiQT);

            this.btnThemNguoi.Location = new System.Drawing.Point(670, 107); this.btnThemNguoi.Size = new System.Drawing.Size(160, 35); this.btnThemNguoi.Text = "Thêm người lưu trú"; this.btnThemNguoi.BackColor = System.Drawing.Color.FromArgb(0, 122, 204); this.btnThemNguoi.ForeColor = System.Drawing.Color.White; this.btnThemNguoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnThemNguoi.Click += new System.EventHandler(this.btnThemNguoi_Click); this.tabNhanPhong.Controls.Add(this.btnThemNguoi);

            this.dgvNguoi.Location = new System.Drawing.Point(470, 160); this.dgvNguoi.Size = new System.Drawing.Size(500, 400); this.dgvNguoi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvNguoi.BackgroundColor = System.Drawing.Color.White; this.dgvNguoi.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right; this.tabNhanPhong.Controls.Add(this.dgvNguoi);

            // Nút đóng Form
            this.btnDong.Location = new System.Drawing.Point(870, 635); this.btnDong.Size = new System.Drawing.Size(100, 35); this.btnDong.Text = "Đóng"; this.btnDong.Click += new System.EventHandler(this.btnDong_Click); this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;

            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.tabControl1); this.Controls.Add(this.btnDong);
            this.Name = "FrmDatPhong"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Quản lý Đặt phòng - Nhận phòng"; this.Load += new System.EventHandler(this.Frm_Load);

            this.tabControl1.ResumeLayout(false); this.tabKhach.ResumeLayout(false); this.tabKhach.PerformLayout(); this.tabDatPhong.ResumeLayout(false); this.tabDatPhong.PerformLayout(); this.tabNhanPhong.ResumeLayout(false); this.tabNhanPhong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhach)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvPhieu)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvChon)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvCT)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvNguoi)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numCoc)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numSoNguoi)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabKhach, tabDatPhong, tabNhanPhong;
        private System.Windows.Forms.ComboBox cboKhach, cboNV, cboKenh;
        private System.Windows.Forms.DataGridView dgvKhach, dgvPhong, dgvPhieu, dgvChon, dgvCT, dgvNguoi;
        private System.Windows.Forms.TextBox txtMaKH, txtTenKH, txtCMND, txtQT, txtSDT, txtSoPhieu, txtPhieuChon, txtNguoiPhong, txtNguoiTen, txtNguoiCMND, txtNguoiQT;
        private System.Windows.Forms.DateTimePicker dtLap, dtNhan, dtTra;
        private System.Windows.Forms.NumericUpDown numCoc, numSoNguoi;
        private System.Windows.Forms.Button btnThemKhach, btnThemPhong, btnBoPhong, btnLapPhieu, btnThemNguoi, btnNhanPhong, btnNoShow, btnDong;
    }
}