namespace QuanLyKhachSan.Forms
{
    partial class FrmPhongTienNghi
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPhong = new System.Windows.Forms.TabPage();
            this.tabTN = new System.Windows.Forms.TabPage();
            this.tabLD = new System.Windows.Forms.TabPage();

            this.cboKhu = new System.Windows.Forms.ComboBox(); this.cboLoai = new System.Windows.Forms.ComboBox(); this.cboTN = new System.Windows.Forms.ComboBox(); this.cboPhong = new System.Windows.Forms.ComboBox(); this.cboNV = new System.Windows.Forms.ComboBox();
            this.dgvPhong = new System.Windows.Forms.DataGridView(); this.dgvTN = new System.Windows.Forms.DataGridView(); this.dgvLD = new System.Windows.Forms.DataGridView();
            this.txtPhong = new System.Windows.Forms.TextBox(); this.numMax = new System.Windows.Forms.NumericUpDown(); this.numGia = new System.Windows.Forms.NumericUpDown(); this.txtMaTN = new System.Windows.Forms.TextBox(); this.numSTT = new System.Windows.Forms.NumericUpDown(); this.txtTinhTrang = new System.Windows.Forms.TextBox();
            this.txtSoLD = new System.Windows.Forms.TextBox(); this.dtNgay = new System.Windows.Forms.DateTimePicker(); this.txtTTLD = new System.Windows.Forms.TextBox(); this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnThemPhong = new System.Windows.Forms.Button(); this.btnThemTN = new System.Windows.Forms.Button(); this.btnLapDat = new System.Windows.Forms.Button(); this.btnDong = new System.Windows.Forms.Button();

            this.tabControl1.SuspendLayout(); this.tabPhong.SuspendLayout(); this.tabTN.SuspendLayout(); this.tabLD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvTN)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvLD)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numMax)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numGia)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numSTT)).BeginInit();
            this.SuspendLayout();

            this.tabControl1.Controls.Add(this.tabPhong); this.tabControl1.Controls.Add(this.tabTN); this.tabControl1.Controls.Add(this.tabLD);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top; this.tabControl1.Location = new System.Drawing.Point(0, 0); this.tabControl1.Size = new System.Drawing.Size(900, 480);

            // Tab Phong
            this.tabPhong.Text = "Phòng"; this.tabPhong.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPhong.Controls.Add(new System.Windows.Forms.Label { Text = "Số phòng:", Location = new System.Drawing.Point(20, 23), AutoSize = true }); this.tabPhong.Controls.Add(this.txtPhong); this.txtPhong.Location = new System.Drawing.Point(85, 20); this.txtPhong.Size = new System.Drawing.Size(100, 20);
            this.tabPhong.Controls.Add(new System.Windows.Forms.Label { Text = "Khu vực:", Location = new System.Drawing.Point(200, 23), AutoSize = true }); this.tabPhong.Controls.Add(this.cboKhu); this.cboKhu.Location = new System.Drawing.Point(260, 20); this.cboKhu.Size = new System.Drawing.Size(120, 21); this.cboKhu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tabPhong.Controls.Add(new System.Windows.Forms.Label { Text = "Sức chứa:", Location = new System.Drawing.Point(400, 23), AutoSize = true }); this.tabPhong.Controls.Add(this.numMax); this.numMax.Location = new System.Drawing.Point(465, 20); this.numMax.Size = new System.Drawing.Size(80, 20); this.numMax.Minimum = 1;
            this.tabPhong.Controls.Add(new System.Windows.Forms.Label { Text = "Đơn giá:", Location = new System.Drawing.Point(565, 23), AutoSize = true }); this.tabPhong.Controls.Add(this.numGia); this.numGia.Location = new System.Drawing.Point(620, 20); this.numGia.Size = new System.Drawing.Size(120, 20); this.numGia.Maximum = 100000000;
            this.tabPhong.Controls.Add(this.btnThemPhong); this.btnThemPhong.Location = new System.Drawing.Point(760, 18); this.btnThemPhong.Size = new System.Drawing.Size(100, 25); this.btnThemPhong.Text = "Thêm phòng"; this.btnThemPhong.Click += new System.EventHandler(this.btnThemPhong_Click);
            this.tabPhong.Controls.Add(this.dgvPhong); this.dgvPhong.Location = new System.Drawing.Point(20, 60); this.dgvPhong.Size = new System.Drawing.Size(850, 380); this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvPhong.BackgroundColor = System.Drawing.Color.White;

            // Tab Tiện Nghi
            this.tabTN.Text = "Tiện nghi"; this.tabTN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabTN.Controls.Add(new System.Windows.Forms.Label { Text = "Mã TN:", Location = new System.Drawing.Point(20, 23), AutoSize = true }); this.tabTN.Controls.Add(this.txtMaTN); this.txtMaTN.Location = new System.Drawing.Point(75, 20); this.txtMaTN.Size = new System.Drawing.Size(120, 20);
            this.tabTN.Controls.Add(new System.Windows.Forms.Label { Text = "Loại TN:", Location = new System.Drawing.Point(215, 23), AutoSize = true }); this.tabTN.Controls.Add(this.cboLoai); this.cboLoai.Location = new System.Drawing.Point(270, 20); this.cboLoai.Size = new System.Drawing.Size(150, 21); this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tabTN.Controls.Add(new System.Windows.Forms.Label { Text = "STT:", Location = new System.Drawing.Point(440, 23), AutoSize = true }); this.tabTN.Controls.Add(this.numSTT); this.numSTT.Location = new System.Drawing.Point(475, 20); this.numSTT.Size = new System.Drawing.Size(80, 20); this.numSTT.Minimum = 1;
            this.tabTN.Controls.Add(new System.Windows.Forms.Label { Text = "Tình trạng:", Location = new System.Drawing.Point(575, 23), AutoSize = true }); this.tabTN.Controls.Add(this.txtTinhTrang); this.txtTinhTrang.Location = new System.Drawing.Point(640, 20); this.txtTinhTrang.Size = new System.Drawing.Size(100, 20);
            this.tabTN.Controls.Add(this.btnThemTN); this.btnThemTN.Location = new System.Drawing.Point(760, 18); this.btnThemTN.Size = new System.Drawing.Size(100, 25); this.btnThemTN.Text = "Thêm tiện nghi"; this.btnThemTN.Click += new System.EventHandler(this.btnThemTN_Click);
            this.tabTN.Controls.Add(this.dgvTN); this.dgvTN.Location = new System.Drawing.Point(20, 60); this.dgvTN.Size = new System.Drawing.Size(850, 380); this.dgvTN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvTN.BackgroundColor = System.Drawing.Color.White;

            // Tab Lắp Đặt
            this.tabLD.Text = "Lắp đặt / Luân chuyển"; this.tabLD.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabLD.Controls.Add(new System.Windows.Forms.Label { Text = "Số phiếu:", Location = new System.Drawing.Point(20, 23), AutoSize = true }); this.tabLD.Controls.Add(this.txtSoLD); this.txtSoLD.Location = new System.Drawing.Point(80, 20); this.txtSoLD.Size = new System.Drawing.Size(100, 20);
            this.tabLD.Controls.Add(new System.Windows.Forms.Label { Text = "Tiện nghi:", Location = new System.Drawing.Point(200, 23), AutoSize = true }); this.tabLD.Controls.Add(this.cboTN); this.cboTN.Location = new System.Drawing.Point(265, 20); this.cboTN.Size = new System.Drawing.Size(120, 21); this.cboTN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tabLD.Controls.Add(new System.Windows.Forms.Label { Text = "Phòng:", Location = new System.Drawing.Point(405, 23), AutoSize = true }); this.tabLD.Controls.Add(this.cboPhong); this.cboPhong.Location = new System.Drawing.Point(455, 20); this.cboPhong.Size = new System.Drawing.Size(100, 21); this.cboPhong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tabLD.Controls.Add(new System.Windows.Forms.Label { Text = "Ngày lắp:", Location = new System.Drawing.Point(575, 23), AutoSize = true }); this.tabLD.Controls.Add(this.dtNgay); this.dtNgay.Location = new System.Drawing.Point(635, 20); this.dtNgay.Size = new System.Drawing.Size(110, 20); this.dtNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.tabLD.Controls.Add(new System.Windows.Forms.Label { Text = "Tình trạng:", Location = new System.Drawing.Point(20, 58), AutoSize = true }); this.tabLD.Controls.Add(this.txtTTLD); this.txtTTLD.Location = new System.Drawing.Point(80, 55); this.txtTTLD.Size = new System.Drawing.Size(100, 20);
            this.tabLD.Controls.Add(new System.Windows.Forms.Label { Text = "NV:", Location = new System.Drawing.Point(200, 58), AutoSize = true }); this.tabLD.Controls.Add(this.cboNV); this.cboNV.Location = new System.Drawing.Point(265, 55); this.cboNV.Size = new System.Drawing.Size(120, 21); this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tabLD.Controls.Add(new System.Windows.Forms.Label { Text = "Ghi chú:", Location = new System.Drawing.Point(405, 58), AutoSize = true }); this.tabLD.Controls.Add(this.txtGhiChu); this.txtGhiChu.Location = new System.Drawing.Point(455, 55); this.txtGhiChu.Size = new System.Drawing.Size(290, 20);
            this.tabLD.Controls.Add(this.btnLapDat); this.btnLapDat.Location = new System.Drawing.Point(760, 52); this.btnLapDat.Size = new System.Drawing.Size(100, 25); this.btnLapDat.Text = "Lập phiếu"; this.btnLapDat.Click += new System.EventHandler(this.btnLapDat_Click);

            this.tabLD.Controls.Add(this.dgvLD); this.dgvLD.Location = new System.Drawing.Point(20, 95); this.dgvLD.Size = new System.Drawing.Size(850, 345); this.dgvLD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvLD.BackgroundColor = System.Drawing.Color.White;

            this.btnDong.Location = new System.Drawing.Point(770, 495); this.btnDong.Size = new System.Drawing.Size(100, 35); this.btnDong.Text = "Đóng"; this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.ClientSize = new System.Drawing.Size(900, 545);
            this.Controls.Add(this.tabControl1); this.Controls.Add(this.btnDong);
            this.Name = "FrmPhongTienNghi"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Phòng - Tiện nghi - Lắp đặt"; this.Load += new System.EventHandler(this.Frm_Load);

            this.tabControl1.ResumeLayout(false); this.tabPhong.ResumeLayout(false); this.tabPhong.PerformLayout(); this.tabTN.ResumeLayout(false); this.tabTN.PerformLayout(); this.tabLD.ResumeLayout(false); this.tabLD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvTN)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvLD)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numMax)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numGia)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numSTT)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPhong, tabTN, tabLD;
        private System.Windows.Forms.ComboBox cboKhu, cboLoai, cboTN, cboPhong, cboNV;
        private System.Windows.Forms.DataGridView dgvPhong, dgvTN, dgvLD;
        private System.Windows.Forms.TextBox txtPhong, txtMaTN, txtTinhTrang, txtSoLD, txtTTLD, txtGhiChu;
        private System.Windows.Forms.NumericUpDown numMax, numGia, numSTT;
        private System.Windows.Forms.DateTimePicker dtNgay;
        private System.Windows.Forms.Button btnThemPhong, btnThemTN, btnLapDat, btnDong;
    }
}