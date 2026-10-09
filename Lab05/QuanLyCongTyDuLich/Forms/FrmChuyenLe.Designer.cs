namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmChuyenLe
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
            this.lblMa = new System.Windows.Forms.Label();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.lblTour = new System.Windows.Forms.Label();
            this.cboTour = new System.Windows.Forms.ComboBox();
            this.lblNgayDi = new System.Windows.Forms.Label();
            this.dtDi = new System.Windows.Forms.DateTimePicker();
            this.lblNgayVeTieuDe = new System.Windows.Forms.Label();
            this.lblNgayVe = new System.Windows.Forms.Label();
            this.lblDon = new System.Windows.Forms.Label();
            this.txtDon = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnDongDK = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMa
            // 
            this.lblMa.AutoSize = true;
            this.lblMa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMa.Location = new System.Drawing.Point(20, 20);
            this.lblMa.Name = "lblMa";
            this.lblMa.Size = new System.Drawing.Size(83, 20);
            this.lblMa.TabIndex = 0;
            this.lblMa.Text = "Mã chuyến:";
            // 
            // txtMa
            // 
            this.txtMa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMa.Location = new System.Drawing.Point(120, 17);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(180, 27);
            this.txtMa.TabIndex = 1;
            // 
            // lblTour
            // 
            this.lblTour.AutoSize = true;
            this.lblTour.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTour.Location = new System.Drawing.Point(340, 20);
            this.lblTour.Name = "lblTour";
            this.lblTour.Size = new System.Drawing.Size(41, 20);
            this.lblTour.TabIndex = 2;
            this.lblTour.Text = "Tour:";
            // 
            // cboTour
            // 
            this.cboTour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTour.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTour.FormattingEnabled = true;
            this.cboTour.Location = new System.Drawing.Point(400, 17);
            this.cboTour.Name = "cboTour";
            this.cboTour.Size = new System.Drawing.Size(480, 28);
            this.cboTour.TabIndex = 3;
            this.cboTour.SelectedIndexChanged += new System.EventHandler(this.TinhNgayVe);
            // 
            // lblNgayDi
            // 
            this.lblNgayDi.AutoSize = true;
            this.lblNgayDi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayDi.Location = new System.Drawing.Point(20, 60);
            this.lblNgayDi.Name = "lblNgayDi";
            this.lblNgayDi.Size = new System.Drawing.Size(64, 20);
            this.lblNgayDi.TabIndex = 4;
            this.lblNgayDi.Text = "Ngày đi:";
            // 
            // dtDi
            // 
            this.dtDi.CustomFormat = "dd/MM/yyyy";
            this.dtDi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDi.Location = new System.Drawing.Point(120, 57);
            this.dtDi.Name = "dtDi";
            this.dtDi.Size = new System.Drawing.Size(180, 27);
            this.dtDi.TabIndex = 5;
            this.dtDi.ValueChanged += new System.EventHandler(this.TinhNgayVe);
            // 
            // lblNgayVeTieuDe
            // 
            this.lblNgayVeTieuDe.AutoSize = true;
            this.lblNgayVeTieuDe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayVeTieuDe.Location = new System.Drawing.Point(340, 60);
            this.lblNgayVeTieuDe.Name = "lblNgayVeTieuDe";
            this.lblNgayVeTieuDe.Size = new System.Drawing.Size(68, 20);
            this.lblNgayVeTieuDe.TabIndex = 6;
            this.lblNgayVeTieuDe.Text = "Ngày về:";
            // 
            // lblNgayVe
            // 
            this.lblNgayVe.AutoSize = true;
            this.lblNgayVe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayVe.Location = new System.Drawing.Point(420, 58);
            this.lblNgayVe.Name = "lblNgayVe";
            this.lblNgayVe.Size = new System.Drawing.Size(17, 23);
            this.lblNgayVe.TabIndex = 7;
            this.lblNgayVe.Text = "-";
            // 
            // lblDon
            // 
            this.lblDon.AutoSize = true;
            this.lblDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDon.Location = new System.Drawing.Point(20, 100);
            this.lblDon.Name = "lblDon";
            this.lblDon.Size = new System.Drawing.Size(102, 20);
            this.lblDon.TabIndex = 8;
            this.lblDon.Text = "Địa điểm đón:";
            // 
            // txtDon
            // 
            this.txtDon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDon.Location = new System.Drawing.Point(120, 97);
            this.txtDon.Name = "txtDon";
            this.txtDon.Size = new System.Drawing.Size(500, 27);
            this.txtDon.TabIndex = 9;
            // 
            // btnThem
            // 
            this.btnThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(640, 93);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(115, 35);
            this.btnThem.TabIndex = 10;
            this.btnThem.Text = "Tạo chuyến";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnDongDK
            // 
            this.btnDongDK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDongDK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDongDK.Location = new System.Drawing.Point(765, 93);
            this.btnDongDK.Name = "btnDongDK";
            this.btnDongDK.Size = new System.Drawing.Size(115, 35);
            this.btnDongDK.TabIndex = 11;
            this.btnDongDK.Text = "Đóng đăng ký";
            this.btnDongDK.UseVisualStyleBackColor = true;
            this.btnDongDK.Click += new System.EventHandler(this.btnDongDK_Click);
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(24, 145);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(856, 360);
            this.dgv.TabIndex = 12;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(765, 520);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(115, 35);
            this.btnDong.TabIndex = 13;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmChuyenLe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 580);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnDongDK);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtDon);
            this.Controls.Add(this.lblDon);
            this.Controls.Add(this.lblNgayVe);
            this.Controls.Add(this.lblNgayVeTieuDe);
            this.Controls.Add(this.dtDi);
            this.Controls.Add(this.lblNgayDi);
            this.Controls.Add(this.cboTour);
            this.Controls.Add(this.lblTour);
            this.Controls.Add(this.txtMa);
            this.Controls.Add(this.lblMa);
            this.Name = "FrmChuyenLe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch chuyến khách lẻ";
            this.Load += new System.EventHandler(this.FrmChuyenLe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.Label lblTour;
        private System.Windows.Forms.ComboBox cboTour;
        private System.Windows.Forms.Label lblNgayDi;
        private System.Windows.Forms.DateTimePicker dtDi;
        private System.Windows.Forms.Label lblNgayVeTieuDe;
        private System.Windows.Forms.Label lblNgayVe;
        private System.Windows.Forms.Label lblDon;
        private System.Windows.Forms.TextBox txtDon;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnDongDK;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnDong;
    }
}
