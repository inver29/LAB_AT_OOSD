namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmLuongThongKe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabTK = new System.Windows.Forms.TabControl();
            this.tabLuongPage = new System.Windows.Forms.TabPage();
            this.lblGhiChuLuong = new System.Windows.Forms.Label();
            this.btnLuong = new System.Windows.Forms.Button();
            this.numNam = new System.Windows.Forms.NumericUpDown();
            this.lblNam = new System.Windows.Forms.Label();
            this.numThang = new System.Windows.Forms.NumericUpDown();
            this.lblThang = new System.Windows.Forms.Label();
            this.dgvLuong = new System.Windows.Forms.DataGridView();
            this.tabTongHopPage = new System.Windows.Forms.TabPage();
            this.btnTongHop = new System.Windows.Forms.Button();
            this.dtDen = new System.Windows.Forms.DateTimePicker();
            this.lblDen = new System.Windows.Forms.Label();
            this.dtTu = new System.Windows.Forms.DateTimePicker();
            this.lblTu = new System.Windows.Forms.Label();
            this.dgvTongHop = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            this.tabTK.SuspendLayout();
            this.tabLuongPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuong)).BeginInit();
            this.tabTongHopPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTongHop)).BeginInit();
            this.SuspendLayout();
            // 
            // tabTK
            // 
            this.tabTK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabTK.Controls.Add(this.tabLuongPage);
            this.tabTK.Controls.Add(this.tabTongHopPage);
            this.tabTK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabTK.Location = new System.Drawing.Point(12, 12);
            this.tabTK.Name = "tabTK";
            this.tabTK.SelectedIndex = 0;
            this.tabTK.Size = new System.Drawing.Size(910, 500);
            this.tabTK.TabIndex = 0;
            // 
            // tabLuongPage
            // 
            this.tabLuongPage.Controls.Add(this.lblGhiChuLuong);
            this.tabLuongPage.Controls.Add(this.btnLuong);
            this.tabLuongPage.Controls.Add(this.numNam);
            this.tabLuongPage.Controls.Add(this.lblNam);
            this.tabLuongPage.Controls.Add(this.numThang);
            this.tabLuongPage.Controls.Add(this.lblThang);
            this.tabLuongPage.Controls.Add(this.dgvLuong);
            this.tabLuongPage.Location = new System.Drawing.Point(4, 29);
            this.tabLuongPage.Name = "tabLuongPage";
            this.tabLuongPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabLuongPage.Size = new System.Drawing.Size(902, 467);
            this.tabLuongPage.TabIndex = 0;
            this.tabLuongPage.Text = "Lương hướng dẫn viên";
            this.tabLuongPage.UseVisualStyleBackColor = true;
            // 
            // lblGhiChuLuong
            // 
            this.lblGhiChuLuong.AutoSize = true;
            this.lblGhiChuLuong.ForeColor = System.Drawing.Color.DimGray;
            this.lblGhiChuLuong.Location = new System.Drawing.Point(440, 20);
            this.lblGhiChuLuong.Name = "lblGhiChuLuong";
            this.lblGhiChuLuong.Size = new System.Drawing.Size(428, 20);
            this.lblGhiChuLuong.TabIndex = 6;
            this.lblGhiChuLuong.Text = "Lương = lương căn bản + thù lao các tour kết thúc trong tháng";
            // 
            // btnLuong
            // 
            this.btnLuong.Location = new System.Drawing.Point(310, 13);
            this.btnLuong.Name = "btnLuong";
            this.btnLuong.Size = new System.Drawing.Size(110, 35);
            this.btnLuong.TabIndex = 5;
            this.btnLuong.Text = "Tính lương";
            this.btnLuong.UseVisualStyleBackColor = true;
            this.btnLuong.Click += new System.EventHandler(this.btnLuong_Click);
            // 
            // numNam
            // 
            this.numNam.Location = new System.Drawing.Point(210, 17);
            this.numNam.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.numNam.Minimum = new decimal(new int[] { 2000, 0, 0, 0 });
            this.numNam.Name = "numNam";
            this.numNam.Size = new System.Drawing.Size(80, 27);
            this.numNam.TabIndex = 4;
            this.numNam.Value = new decimal(new int[] { 2026, 0, 0, 0 });
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Location = new System.Drawing.Point(160, 20);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(44, 20);
            this.lblNam.TabIndex = 3;
            this.lblNam.Text = "Năm:";
            // 
            // numThang
            // 
            this.numThang.Location = new System.Drawing.Point(75, 17);
            this.numThang.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            this.numThang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numThang.Name = "numThang";
            this.numThang.Size = new System.Drawing.Size(65, 27);
            this.numThang.TabIndex = 2;
            this.numThang.Value = new decimal(new int[] { 9, 0, 0, 0 });
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Location = new System.Drawing.Point(15, 20);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(53, 20);
            this.lblThang.TabIndex = 1;
            this.lblThang.Text = "Tháng:";
            // 
            // dgvLuong
            // 
            this.dgvLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLuong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLuong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLuong.Location = new System.Drawing.Point(6, 60);
            this.dgvLuong.Name = "dgvLuong";
            this.dgvLuong.RowHeadersWidth = 51;
            this.dgvLuong.RowTemplate.Height = 24;
            this.dgvLuong.Size = new System.Drawing.Size(890, 400);
            this.dgvLuong.TabIndex = 0;
            // 
            // tabTongHopPage
            // 
            this.tabTongHopPage.Controls.Add(this.btnTongHop);
            this.tabTongHopPage.Controls.Add(this.dtDen);
            this.tabTongHopPage.Controls.Add(this.lblDen);
            this.tabTongHopPage.Controls.Add(this.dtTu);
            this.tabTongHopPage.Controls.Add(this.lblTu);
            this.tabTongHopPage.Controls.Add(this.dgvTongHop);
            this.tabTongHopPage.Location = new System.Drawing.Point(4, 29);
            this.tabTongHopPage.Name = "tabTongHopPage";
            this.tabTongHopPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabTongHopPage.Size = new System.Drawing.Size(902, 467);
            this.tabTongHopPage.TabIndex = 1;
            this.tabTongHopPage.Text = "Thống kê tổng hợp";
            this.tabTongHopPage.UseVisualStyleBackColor = true;
            // 
            // btnTongHop
            // 
            this.btnTongHop.Location = new System.Drawing.Point(540, 13);
            this.btnTongHop.Name = "btnTongHop";
            this.btnTongHop.Size = new System.Drawing.Size(120, 35);
            this.btnTongHop.TabIndex = 5;
            this.btnTongHop.Text = "Thống kê";
            this.btnTongHop.UseVisualStyleBackColor = true;
            this.btnTongHop.Click += new System.EventHandler(this.btnTongHop_Click);
            // 
            // dtDen
            // 
            this.dtDen.CustomFormat = "dd/MM/yyyy";
            this.dtDen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDen.Location = new System.Drawing.Point(365, 17);
            this.dtDen.Name = "dtDen";
            this.dtDen.Size = new System.Drawing.Size(140, 27);
            this.dtDen.TabIndex = 4;
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Location = new System.Drawing.Point(285, 20);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(75, 20);
            this.lblDen.TabIndex = 3;
            this.lblDen.Text = "Đến ngày:";
            // 
            // dtTu
            // 
            this.dtTu.CustomFormat = "dd/MM/yyyy";
            this.dtTu.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTu.Location = new System.Drawing.Point(90, 17);
            this.dtTu.Name = "dtTu";
            this.dtTu.Size = new System.Drawing.Size(140, 27);
            this.dtTu.TabIndex = 2;
            // 
            // lblTu
            // 
            this.lblTu.AutoSize = true;
            this.lblTu.Location = new System.Drawing.Point(20, 20);
            this.lblTu.Name = "lblTu";
            this.lblTu.Size = new System.Drawing.Size(65, 20);
            this.lblTu.TabIndex = 1;
            this.lblTu.Text = "Từ ngày:";
            // 
            // dgvTongHop
            // 
            this.dgvTongHop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTongHop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTongHop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTongHop.Location = new System.Drawing.Point(6, 60);
            this.dgvTongHop.Name = "dgvTongHop";
            this.dgvTongHop.RowHeadersWidth = 51;
            this.dgvTongHop.RowTemplate.Height = 24;
            this.dgvTongHop.Size = new System.Drawing.Size(890, 400);
            this.dgvTongHop.TabIndex = 0;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(802, 520);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 35);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmLuongThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 580);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.tabTK);
            this.Name = "FrmLuongThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lương hướng dẫn viên - thống kê";
            this.Load += new System.EventHandler(this.FrmLuongThongKe_Load);
            this.tabTK.ResumeLayout(false);
            this.tabLuongPage.ResumeLayout(false);
            this.tabLuongPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuong)).EndInit();
            this.tabTongHopPage.ResumeLayout(false);
            this.tabTongHopPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTongHop)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabTK;
        private System.Windows.Forms.TabPage tabLuongPage;
        private System.Windows.Forms.TabPage tabTongHopPage;
        private System.Windows.Forms.DataGridView dgvLuong;
        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.NumericUpDown numThang;
        private System.Windows.Forms.Label lblNam;
        private System.Windows.Forms.NumericUpDown numNam;
        private System.Windows.Forms.Button btnLuong;
        private System.Windows.Forms.Label lblGhiChuLuong;
        private System.Windows.Forms.DataGridView dgvTongHop;
        private System.Windows.Forms.Label lblTu;
        private System.Windows.Forms.DateTimePicker dtTu;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.DateTimePicker dtDen;
        private System.Windows.Forms.Button btnTongHop;
        private System.Windows.Forms.Button btnDong;
    }
}
