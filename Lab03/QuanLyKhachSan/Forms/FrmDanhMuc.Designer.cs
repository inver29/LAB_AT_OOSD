namespace QuanLyKhachSan.Forms
{
    partial class FrmDanhMuc
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabKhu = new System.Windows.Forms.TabPage();
            this.tabNV = new System.Windows.Forms.TabPage();
            this.tabLoaiTN = new System.Windows.Forms.TabPage();
            this.tabDV = new System.Windows.Forms.TabPage();
            this.tabQD = new System.Windows.Forms.TabPage();

            // Khởi tạo các Control
            this.dgvKhu = new System.Windows.Forms.DataGridView(); this.txtKhuMa = new System.Windows.Forms.TextBox(); this.txtKhuTen = new System.Windows.Forms.TextBox(); this.btnThemKhu = new System.Windows.Forms.Button();
            this.dgvNV = new System.Windows.Forms.DataGridView(); this.txtNVMa = new System.Windows.Forms.TextBox(); this.txtNVTen = new System.Windows.Forms.TextBox(); this.txtNVVaiTro = new System.Windows.Forms.TextBox(); this.txtNVSDT = new System.Windows.Forms.TextBox(); this.btnThemNV = new System.Windows.Forms.Button();
            this.dgvLoaiTN = new System.Windows.Forms.DataGridView(); this.txtLoaiMa = new System.Windows.Forms.TextBox(); this.txtLoaiTen = new System.Windows.Forms.TextBox(); this.btnThemLoaiTN = new System.Windows.Forms.Button();
            this.dgvDV = new System.Windows.Forms.DataGridView(); this.txtDVMa = new System.Windows.Forms.TextBox(); this.txtDVTen = new System.Windows.Forms.TextBox(); this.txtDVDVT = new System.Windows.Forms.TextBox(); this.numDVGia = new System.Windows.Forms.NumericUpDown(); this.btnThemDV = new System.Windows.Forms.Button();
            this.dgvQD = new System.Windows.Forms.DataGridView(); this.txtQDMa = new System.Windows.Forms.TextBox(); this.cboQDLoai = new System.Windows.Forms.ComboBox(); this.txtQDMucDo = new System.Windows.Forms.TextBox(); this.numQDTien = new System.Windows.Forms.NumericUpDown(); this.btnThemQD = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();

            this.tabControl1.SuspendLayout(); this.tabKhu.SuspendLayout(); this.tabNV.SuspendLayout(); this.tabLoaiTN.SuspendLayout(); this.tabDV.SuspendLayout(); this.tabQD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhu)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiTN)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvDV)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numDVGia)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvQD)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numQDTien)).BeginInit();
            this.SuspendLayout();

            // TabControl
            this.tabControl1.Controls.Add(this.tabKhu); this.tabControl1.Controls.Add(this.tabNV); this.tabControl1.Controls.Add(this.tabLoaiTN); this.tabControl1.Controls.Add(this.tabDV); this.tabControl1.Controls.Add(this.tabQD);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top; this.tabControl1.Location = new System.Drawing.Point(0, 0); this.tabControl1.Size = new System.Drawing.Size(850, 480);

            // Gắn Controls vào Tab Khu
            this.tabKhu.Text = "Khu vực"; this.tabKhu.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabKhu.Controls.Add(new System.Windows.Forms.Label { Text = "Mã:", Location = new System.Drawing.Point(20, 23), AutoSize = true }); this.tabKhu.Controls.Add(this.txtKhuMa); this.txtKhuMa.Location = new System.Drawing.Point(60, 20); this.txtKhuMa.Size = new System.Drawing.Size(120, 20);
            this.tabKhu.Controls.Add(new System.Windows.Forms.Label { Text = "Tên:", Location = new System.Drawing.Point(200, 23), AutoSize = true }); this.tabKhu.Controls.Add(this.txtKhuTen); this.txtKhuTen.Location = new System.Drawing.Point(240, 20); this.txtKhuTen.Size = new System.Drawing.Size(200, 20);
            this.tabKhu.Controls.Add(this.btnThemKhu); this.btnThemKhu.Location = new System.Drawing.Point(460, 18); this.btnThemKhu.Size = new System.Drawing.Size(100, 25); this.btnThemKhu.Text = "Thêm"; this.btnThemKhu.Click += new System.EventHandler(this.btnThemKhu_Click);
            this.tabKhu.Controls.Add(this.dgvKhu); this.dgvKhu.Location = new System.Drawing.Point(20, 60); this.dgvKhu.Size = new System.Drawing.Size(800, 380); this.dgvKhu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvKhu.BackgroundColor = System.Drawing.Color.White;

            // Tab NV
            this.tabNV.Text = "Nhân viên"; this.tabNV.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabNV.Controls.Add(new System.Windows.Forms.Label { Text = "Mã:", Location = new System.Drawing.Point(20, 23), AutoSize = true }); this.tabNV.Controls.Add(this.txtNVMa); this.txtNVMa.Location = new System.Drawing.Point(50, 20); this.txtNVMa.Size = new System.Drawing.Size(80, 20);
            this.tabNV.Controls.Add(new System.Windows.Forms.Label { Text = "Tên:", Location = new System.Drawing.Point(140, 23), AutoSize = true }); this.tabNV.Controls.Add(this.txtNVTen); this.txtNVTen.Location = new System.Drawing.Point(180, 20); this.txtNVTen.Size = new System.Drawing.Size(150, 20);
            this.tabNV.Controls.Add(new System.Windows.Forms.Label { Text = "Vai trò:", Location = new System.Drawing.Point(340, 23), AutoSize = true }); this.tabNV.Controls.Add(this.txtNVVaiTro); this.txtNVVaiTro.Location = new System.Drawing.Point(390, 20); this.txtNVVaiTro.Size = new System.Drawing.Size(120, 20);
            this.tabNV.Controls.Add(new System.Windows.Forms.Label { Text = "SĐT:", Location = new System.Drawing.Point(520, 23), AutoSize = true }); this.tabNV.Controls.Add(this.txtNVSDT); this.txtNVSDT.Location = new System.Drawing.Point(560, 20); this.txtNVSDT.Size = new System.Drawing.Size(120, 20);
            this.tabNV.Controls.Add(this.btnThemNV); this.btnThemNV.Location = new System.Drawing.Point(700, 18); this.btnThemNV.Size = new System.Drawing.Size(80, 25); this.btnThemNV.Text = "Thêm"; this.btnThemNV.Click += new System.EventHandler(this.btnThemNV_Click);
            this.tabNV.Controls.Add(this.dgvNV); this.dgvNV.Location = new System.Drawing.Point(20, 60); this.dgvNV.Size = new System.Drawing.Size(800, 380); this.dgvNV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvNV.BackgroundColor = System.Drawing.Color.White;

            // Tab Loai TN
            this.tabLoaiTN.Text = "Loại tiện nghi"; this.tabLoaiTN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabLoaiTN.Controls.Add(new System.Windows.Forms.Label { Text = "Mã:", Location = new System.Drawing.Point(20, 23), AutoSize = true }); this.tabLoaiTN.Controls.Add(this.txtLoaiMa); this.txtLoaiMa.Location = new System.Drawing.Point(60, 20); this.txtLoaiMa.Size = new System.Drawing.Size(120, 20);
            this.tabLoaiTN.Controls.Add(new System.Windows.Forms.Label { Text = "Tên:", Location = new System.Drawing.Point(200, 23), AutoSize = true }); this.tabLoaiTN.Controls.Add(this.txtLoaiTen); this.txtLoaiTen.Location = new System.Drawing.Point(240, 20); this.txtLoaiTen.Size = new System.Drawing.Size(200, 20);
            this.tabLoaiTN.Controls.Add(this.btnThemLoaiTN); this.btnThemLoaiTN.Location = new System.Drawing.Point(460, 18); this.btnThemLoaiTN.Size = new System.Drawing.Size(100, 25); this.btnThemLoaiTN.Text = "Thêm"; this.btnThemLoaiTN.Click += new System.EventHandler(this.btnThemLoaiTN_Click);
            this.tabLoaiTN.Controls.Add(this.dgvLoaiTN); this.dgvLoaiTN.Location = new System.Drawing.Point(20, 60); this.dgvLoaiTN.Size = new System.Drawing.Size(800, 380); this.dgvLoaiTN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvLoaiTN.BackgroundColor = System.Drawing.Color.White;

            // Tab DV
            this.tabDV.Text = "Dịch vụ"; this.tabDV.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabDV.Controls.Add(new System.Windows.Forms.Label { Text = "Mã:", Location = new System.Drawing.Point(20, 23), AutoSize = true }); this.tabDV.Controls.Add(this.txtDVMa); this.txtDVMa.Location = new System.Drawing.Point(50, 20); this.txtDVMa.Size = new System.Drawing.Size(100, 20);
            this.tabDV.Controls.Add(new System.Windows.Forms.Label { Text = "Tên:", Location = new System.Drawing.Point(160, 23), AutoSize = true }); this.tabDV.Controls.Add(this.txtDVTen); this.txtDVTen.Location = new System.Drawing.Point(190, 20); this.txtDVTen.Size = new System.Drawing.Size(150, 20);
            this.tabDV.Controls.Add(new System.Windows.Forms.Label { Text = "Đơn vị:", Location = new System.Drawing.Point(350, 23), AutoSize = true }); this.tabDV.Controls.Add(this.txtDVDVT); this.txtDVDVT.Location = new System.Drawing.Point(395, 20); this.txtDVDVT.Size = new System.Drawing.Size(100, 20);
            this.tabDV.Controls.Add(new System.Windows.Forms.Label { Text = "Đơn giá:", Location = new System.Drawing.Point(505, 23), AutoSize = true }); this.tabDV.Controls.Add(this.numDVGia); this.numDVGia.Location = new System.Drawing.Point(560, 20); this.numDVGia.Size = new System.Drawing.Size(120, 20); this.numDVGia.Maximum = 100000000;
            this.tabDV.Controls.Add(this.btnThemDV); this.btnThemDV.Location = new System.Drawing.Point(700, 18); this.btnThemDV.Size = new System.Drawing.Size(80, 25); this.btnThemDV.Text = "Thêm"; this.btnThemDV.Click += new System.EventHandler(this.btnThemDV_Click);
            this.tabDV.Controls.Add(this.dgvDV); this.dgvDV.Location = new System.Drawing.Point(20, 60); this.dgvDV.Size = new System.Drawing.Size(800, 380); this.dgvDV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvDV.BackgroundColor = System.Drawing.Color.White;

            // Tab QD
            this.tabQD.Text = "Quy định đền bù"; this.tabQD.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabQD.Controls.Add(new System.Windows.Forms.Label { Text = "Mã:", Location = new System.Drawing.Point(20, 23), AutoSize = true }); this.tabQD.Controls.Add(this.txtQDMa); this.txtQDMa.Location = new System.Drawing.Point(50, 20); this.txtQDMa.Size = new System.Drawing.Size(80, 20);
            this.tabQD.Controls.Add(new System.Windows.Forms.Label { Text = "Loại TN:", Location = new System.Drawing.Point(140, 23), AutoSize = true }); this.tabQD.Controls.Add(this.cboQDLoai); this.cboQDLoai.Location = new System.Drawing.Point(190, 20); this.cboQDLoai.Size = new System.Drawing.Size(120, 20); this.cboQDLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tabQD.Controls.Add(new System.Windows.Forms.Label { Text = "Mức độ:", Location = new System.Drawing.Point(320, 23), AutoSize = true }); this.tabQD.Controls.Add(this.txtQDMucDo); this.txtQDMucDo.Location = new System.Drawing.Point(375, 20); this.txtQDMucDo.Size = new System.Drawing.Size(120, 20);
            this.tabQD.Controls.Add(new System.Windows.Forms.Label { Text = "Mức tiền:", Location = new System.Drawing.Point(505, 23), AutoSize = true }); this.tabQD.Controls.Add(this.numQDTien); this.numQDTien.Location = new System.Drawing.Point(560, 20); this.numQDTien.Size = new System.Drawing.Size(120, 20); this.numQDTien.Maximum = 100000000;
            this.tabQD.Controls.Add(this.btnThemQD); this.btnThemQD.Location = new System.Drawing.Point(700, 18); this.btnThemQD.Size = new System.Drawing.Size(80, 25); this.btnThemQD.Text = "Thêm"; this.btnThemQD.Click += new System.EventHandler(this.btnThemQD_Click);
            this.tabQD.Controls.Add(this.dgvQD); this.dgvQD.Location = new System.Drawing.Point(20, 60); this.dgvQD.Size = new System.Drawing.Size(800, 380); this.dgvQD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvQD.BackgroundColor = System.Drawing.Color.White;

            // btnDong
            this.btnDong.Location = new System.Drawing.Point(730, 495); this.btnDong.Size = new System.Drawing.Size(100, 35); this.btnDong.Text = "Đóng"; this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.ClientSize = new System.Drawing.Size(850, 545);
            this.Controls.Add(this.tabControl1); this.Controls.Add(this.btnDong);
            this.Name = "FrmDanhMuc"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Danh mục khách sạn"; this.Load += new System.EventHandler(this.FrmDanhMuc_Load);

            this.tabControl1.ResumeLayout(false); this.tabKhu.ResumeLayout(false); this.tabKhu.PerformLayout(); this.tabNV.ResumeLayout(false); this.tabNV.PerformLayout(); this.tabLoaiTN.ResumeLayout(false); this.tabLoaiTN.PerformLayout(); this.tabDV.ResumeLayout(false); this.tabDV.PerformLayout(); this.tabQD.ResumeLayout(false); this.tabQD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhu)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvNV)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiTN)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvDV)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numDVGia)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvQD)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numQDTien)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabKhu, tabNV, tabLoaiTN, tabDV, tabQD;
        private System.Windows.Forms.DataGridView dgvKhu, dgvNV, dgvLoaiTN, dgvDV, dgvQD;
        private System.Windows.Forms.TextBox txtKhuMa, txtKhuTen, txtNVMa, txtNVTen, txtNVVaiTro, txtNVSDT, txtLoaiMa, txtLoaiTen, txtDVMa, txtDVTen, txtDVDVT, txtQDMa, txtQDMucDo;
        private System.Windows.Forms.ComboBox cboQDLoai;
        private System.Windows.Forms.NumericUpDown numDVGia, numQDTien;
        private System.Windows.Forms.Button btnThemKhu, btnThemNV, btnThemLoaiTN, btnThemDV, btnThemQD, btnDong;
    }
}