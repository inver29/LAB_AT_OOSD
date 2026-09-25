namespace QuanLyKhachSan.Forms
{
    partial class FrmThongKe
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.dtTu = new System.Windows.Forms.DateTimePicker();
            this.dtDen = new System.Windows.Forms.DateTimePicker();
            this.dgvTongHop = new System.Windows.Forms.DataGridView();
            this.dgvDV = new System.Windows.Forms.DataGridView();
            this.btnTK = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.lblDichVu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTongHop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDV)).BeginInit();
            this.SuspendLayout();
            // Labels
            this.lblTuNgay.AutoSize = true; this.lblTuNgay.Location = new System.Drawing.Point(30, 30); this.lblTuNgay.Text = "Từ ngày:";
            this.lblDenNgay.AutoSize = true; this.lblDenNgay.Location = new System.Drawing.Point(230, 30); this.lblDenNgay.Text = "Đến ngày:";
            this.lblDichVu.AutoSize = true; this.lblDichVu.Location = new System.Drawing.Point(30, 190); this.lblDichVu.Text = "Dịch vụ sử dụng:";
            // Inputs
            this.dtTu.Location = new System.Drawing.Point(90, 27); this.dtTu.Size = new System.Drawing.Size(120, 20); this.dtTu.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtDen.Location = new System.Drawing.Point(300, 27); this.dtDen.Size = new System.Drawing.Size(120, 20); this.dtDen.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            // Buttons
            this.btnTK.Location = new System.Drawing.Point(450, 22); this.btnTK.Size = new System.Drawing.Size(100, 30); this.btnTK.Text = "Thống kê";
            this.btnDong.Location = new System.Drawing.Point(570, 22); this.btnDong.Size = new System.Drawing.Size(100, 30); this.btnDong.Text = "Đóng";
            // DataGridView TongHop
            this.dgvTongHop.Location = new System.Drawing.Point(30, 75);
            this.dgvTongHop.Size = new System.Drawing.Size(720, 90);
            this.dgvTongHop.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTongHop.BackgroundColor = System.Drawing.Color.White;
            this.dgvTongHop.AllowUserToAddRows = false;
            // DataGridView DichVu
            this.dgvDV.Location = new System.Drawing.Point(30, 215);
            this.dgvDV.Size = new System.Drawing.Size(720, 280);
            this.dgvDV.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvDV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDV.BackgroundColor = System.Drawing.Color.White;
            // Events
            this.btnTK.Click += new System.EventHandler(this.btnTK_Click);
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // FrmThongKe
            this.ClientSize = new System.Drawing.Size(784, 521);
            this.Controls.Add(this.lblTuNgay); this.Controls.Add(this.dtTu);
            this.Controls.Add(this.lblDenNgay); this.Controls.Add(this.dtDen);
            this.Controls.Add(this.btnTK); this.Controls.Add(this.btnDong);
            this.Controls.Add(this.dgvTongHop);
            this.Controls.Add(this.lblDichVu); this.Controls.Add(this.dgvDV);
            this.Name = "FrmThongKe"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Thống kê khách sạn";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTongHop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDV)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }

        private System.Windows.Forms.DateTimePicker dtTu, dtDen;
        private System.Windows.Forms.DataGridView dgvTongHop, dgvDV;
        private System.Windows.Forms.Button btnTK, btnDong;
        private System.Windows.Forms.Label lblTuNgay, lblDenNgay, lblDichVu;
    }
}