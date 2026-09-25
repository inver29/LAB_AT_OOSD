namespace QuanLyKhachSan.Forms
{
    partial class FrmTraPhong
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.cboDat = new System.Windows.Forms.ComboBox(); this.cboNV = new System.Windows.Forms.ComboBox(); this.cboNV2 = new System.Windows.Forms.ComboBox(); this.cboHT = new System.Windows.Forms.ComboBox();
            this.dgvPhong = new System.Windows.Forms.DataGridView(); this.dgvHD = new System.Windows.Forms.DataGridView(); this.dgvTN = new System.Windows.Forms.DataGridView(); this.dgvDBChon = new System.Windows.Forms.DataGridView();
            this.txtPhong = new System.Windows.Forms.TextBox(); this.txtSoDB = new System.Windows.Forms.TextBox(); this.txtMucDo = new System.Windows.Forms.TextBox(); this.txtSoHD = new System.Windows.Forms.TextBox(); this.txtHDChon = new System.Windows.Forms.TextBox(); this.txtMaTT = new System.Windows.Forms.TextBox();
            this.numDenBu = new System.Windows.Forms.NumericUpDown(); this.numSoNgay = new System.Windows.Forms.NumericUpDown(); this.numTienTT = new System.Windows.Forms.NumericUpDown();
            this.btnThemDB = new System.Windows.Forms.Button(); this.btnLapDB = new System.Windows.Forms.Button(); this.btnLapHD = new System.Windows.Forms.Button(); this.btnThanhToan = new System.Windows.Forms.Button(); this.btnTraPhong = new System.Windows.Forms.Button(); this.btnDong = new System.Windows.Forms.Button();

            System.Windows.Forms.Label lbl1 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl2 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl3 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl4 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl5 = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lbl6 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl7 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl8 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl9 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl10 = new System.Windows.Forms.Label(); System.Windows.Forms.Label lbl11 = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvHD)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvTN)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvDBChon)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numDenBu)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numSoNgay)).BeginInit(); ((System.ComponentModel.ISupportInitialize)(this.numTienTT)).BeginInit();
            this.SuspendLayout();

            this.dgvPhong.SelectionChanged += new System.EventHandler(this.dgvPhong_SelectionChanged);
            this.dgvHD.SelectionChanged += new System.EventHandler(this.dgvHD_SelectionChanged);
            this.btnThemDB.Click += new System.EventHandler(this.btnThemDB_Click);
            this.btnLapDB.Click += new System.EventHandler(this.btnLapDB_Click);
            this.btnLapHD.Click += new System.EventHandler(this.btnLapHD_Click);
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            this.btnTraPhong.Click += new System.EventHandler(this.btnTraPhong_Click);
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);

            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            // Row 1: Phiếu lưu trú
            lbl1.Text = "Chọn phiếu đang ở:"; lbl1.Location = new System.Drawing.Point(20, 23); lbl1.AutoSize = true; this.Controls.Add(lbl1);
            this.cboDat.Location = new System.Drawing.Point(160, 20); this.cboDat.Size = new System.Drawing.Size(200, 25); this.cboDat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.Controls.Add(this.cboDat);

            // Row 2: Grids
            this.dgvPhong.Location = new System.Drawing.Point(20, 60); this.dgvPhong.Size = new System.Drawing.Size(300, 180); this.dgvPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvPhong.BackgroundColor = System.Drawing.Color.White; this.Controls.Add(this.dgvPhong);
            this.dgvTN.Location = new System.Drawing.Point(340, 60); this.dgvTN.Size = new System.Drawing.Size(360, 180); this.dgvTN.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells; this.dgvTN.BackgroundColor = System.Drawing.Color.White; this.Controls.Add(this.dgvTN);
            this.dgvDBChon.Location = new System.Drawing.Point(720, 60); this.dgvDBChon.Size = new System.Drawing.Size(340, 180); this.dgvDBChon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells; this.dgvDBChon.BackgroundColor = System.Drawing.Color.White; this.Controls.Add(this.dgvDBChon);

            // Row 3: Đền bù inputs
            lbl2.Text = "Phiếu Đền Bù:"; lbl2.Location = new System.Drawing.Point(20, 263); lbl2.AutoSize = true; this.Controls.Add(lbl2);
            this.txtSoDB.Location = new System.Drawing.Point(120, 260); this.txtSoDB.Size = new System.Drawing.Size(120, 25); this.Controls.Add(this.txtSoDB);

            this.txtPhong.Location = new System.Drawing.Point(250, 260); this.txtPhong.Size = new System.Drawing.Size(60, 25); this.txtPhong.Visible = false; this.Controls.Add(this.txtPhong);

            lbl3.Text = "Mức độ hỏng/mất:"; lbl3.Location = new System.Drawing.Point(340, 263); lbl3.AutoSize = true; this.Controls.Add(lbl3);
            this.txtMucDo.Location = new System.Drawing.Point(475, 260); this.txtMucDo.Size = new System.Drawing.Size(150, 25); this.Controls.Add(this.txtMucDo);

            lbl4.Text = "Số tiền:"; lbl4.Location = new System.Drawing.Point(640, 263); lbl4.AutoSize = true; this.Controls.Add(lbl4);
            this.numDenBu.Location = new System.Drawing.Point(695, 260); this.numDenBu.Size = new System.Drawing.Size(120, 25); this.numDenBu.Maximum = 100000000; this.Controls.Add(this.numDenBu);

            this.btnThemDB.Location = new System.Drawing.Point(830, 255); this.btnThemDB.Size = new System.Drawing.Size(120, 32); this.btnThemDB.Text = "Thêm tài sản"; this.btnThemDB.BackColor = System.Drawing.Color.FromArgb(224, 224, 224); this.btnThemDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.Controls.Add(this.btnThemDB);

            lbl5.Text = "NV:"; lbl5.Location = new System.Drawing.Point(20, 303); lbl5.AutoSize = true; this.Controls.Add(lbl5);
            this.cboNV.Location = new System.Drawing.Point(60, 300); this.cboNV.Size = new System.Drawing.Size(150, 25); this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.Controls.Add(this.cboNV);

            this.btnLapDB.Location = new System.Drawing.Point(230, 295); this.btnLapDB.Size = new System.Drawing.Size(120, 35); this.btnLapDB.Text = "Lập Phiếu ĐB"; this.btnLapDB.BackColor = System.Drawing.Color.FromArgb(244, 67, 54); this.btnLapDB.ForeColor = System.Drawing.Color.White; this.btnLapDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.Controls.Add(this.btnLapDB);

            // Divider Line
            System.Windows.Forms.Label divider = new System.Windows.Forms.Label();
            divider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D; divider.Location = new System.Drawing.Point(20, 350); divider.Size = new System.Drawing.Size(1040, 2); this.Controls.Add(divider);

            // Row 4: Hóa đơn inputs
            lbl6.Text = "Số Hóa Đơn:"; lbl6.Location = new System.Drawing.Point(20, 378); lbl6.AutoSize = true; this.Controls.Add(lbl6);
            this.txtSoHD.Location = new System.Drawing.Point(110, 375); this.txtSoHD.Size = new System.Drawing.Size(120, 25); this.Controls.Add(this.txtSoHD);

            lbl7.Text = "Số ngày lưu trú:"; lbl7.Location = new System.Drawing.Point(250, 378); lbl7.AutoSize = true; this.Controls.Add(lbl7);
            this.numSoNgay.Location = new System.Drawing.Point(365, 375); this.numSoNgay.Size = new System.Drawing.Size(80, 25); this.numSoNgay.Minimum = 1; this.numSoNgay.Value = 1; this.Controls.Add(this.numSoNgay);

            lbl8.Text = "NV Lập HĐ:"; lbl8.Location = new System.Drawing.Point(470, 378); lbl8.AutoSize = true; this.Controls.Add(lbl8);
            this.cboNV2.Location = new System.Drawing.Point(555, 375); this.cboNV2.Size = new System.Drawing.Size(150, 25); this.cboNV2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.Controls.Add(this.cboNV2);

            this.btnLapHD.Location = new System.Drawing.Point(730, 370); this.btnLapHD.Size = new System.Drawing.Size(150, 35); this.btnLapHD.Text = "TỔNG HỢP HÓA ĐƠN"; this.btnLapHD.BackColor = System.Drawing.Color.FromArgb(0, 150, 136); this.btnLapHD.ForeColor = System.Drawing.Color.White; this.btnLapHD.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.Controls.Add(this.btnLapHD);

            // Row 5: Hóa đơn Grid
            this.dgvHD.Location = new System.Drawing.Point(20, 420); this.dgvHD.Size = new System.Drawing.Size(1040, 160); this.dgvHD.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill; this.dgvHD.BackgroundColor = System.Drawing.Color.White; this.Controls.Add(this.dgvHD);

            // Row 6: Thanh toán
            lbl9.Text = "Mã Giao Dịch:"; lbl9.Location = new System.Drawing.Point(20, 608); lbl9.AutoSize = true; this.Controls.Add(lbl9);
            this.txtMaTT.Location = new System.Drawing.Point(120, 605); this.txtMaTT.Size = new System.Drawing.Size(100, 25); this.Controls.Add(this.txtMaTT);

            this.txtHDChon.Location = new System.Drawing.Point(230, 605); this.txtHDChon.Size = new System.Drawing.Size(80, 25); this.txtHDChon.ReadOnly = true; this.Controls.Add(this.txtHDChon);

            lbl10.Text = "Hình thức:"; lbl10.Location = new System.Drawing.Point(330, 608); lbl10.AutoSize = true; this.Controls.Add(lbl10);
            this.cboHT.Location = new System.Drawing.Point(410, 605); this.cboHT.Size = new System.Drawing.Size(120, 25); this.cboHT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.Controls.Add(this.cboHT);

            lbl11.Text = "Số tiền thu:"; lbl11.Location = new System.Drawing.Point(550, 608); lbl11.AutoSize = true; this.Controls.Add(lbl11);
            this.numTienTT.Location = new System.Drawing.Point(635, 605); this.numTienTT.Size = new System.Drawing.Size(120, 25); this.numTienTT.Maximum = 1000000000; this.Controls.Add(this.numTienTT);

            this.btnThanhToan.Location = new System.Drawing.Point(770, 600); this.btnThanhToan.Size = new System.Drawing.Size(120, 35); this.btnThanhToan.Text = "Thu tiền"; this.btnThanhToan.BackColor = System.Drawing.Color.FromArgb(0, 122, 204); this.btnThanhToan.ForeColor = System.Drawing.Color.White; this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.Controls.Add(this.btnThanhToan);

            this.btnTraPhong.Location = new System.Drawing.Point(910, 600); this.btnTraPhong.Size = new System.Drawing.Size(150, 35); this.btnTraPhong.Text = "HOÀN TẤT TRẢ"; this.btnTraPhong.BackColor = System.Drawing.Color.FromArgb(76, 175, 80); this.btnTraPhong.ForeColor = System.Drawing.Color.White; this.btnTraPhong.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.Controls.Add(this.btnTraPhong);

            this.btnDong.Location = new System.Drawing.Point(960, 660); this.btnDong.Size = new System.Drawing.Size(100, 35); this.btnDong.Text = "Đóng"; this.btnDong.Click += new System.EventHandler(this.btnDong_Click); this.btnDong.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right; this.Controls.Add(this.btnDong);

            this.ClientSize = new System.Drawing.Size(1080, 710);
            this.Name = "FrmTraPhong"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Trả phòng - Thanh toán"; this.Load += new System.EventHandler(this.Frm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvHD)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvTN)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.dgvDBChon)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numDenBu)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numSoNgay)).EndInit(); ((System.ComponentModel.ISupportInitialize)(this.numTienTT)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox cboDat, cboNV, cboNV2, cboHT;
        private System.Windows.Forms.DataGridView dgvPhong, dgvHD, dgvTN, dgvDBChon;
        private System.Windows.Forms.TextBox txtPhong, txtSoDB, txtMucDo, txtSoHD, txtHDChon, txtMaTT;
        private System.Windows.Forms.NumericUpDown numDenBu, numSoNgay, numTienTT;
        private System.Windows.Forms.Button btnThemDB, btnLapDB, btnLapHD, btnThanhToan, btnTraPhong, btnDong;
    }
}