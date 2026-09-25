namespace QuanLyKhachSan.Forms
{
    partial class FrmDichVu
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.cboLuot = new System.Windows.Forms.ComboBox();
            this.cboDV = new System.Windows.Forms.ComboBox();
            this.cboNV = new System.Windows.Forms.ComboBox();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.txtPhong = new System.Windows.Forms.TextBox();
            this.dtNgay = new System.Windows.Forms.DateTimePicker();
            this.numSL = new System.Windows.Forms.NumericUpDown();
            this.btnGhi = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.lblPhieu = new System.Windows.Forms.Label();
            this.lblPhong = new System.Windows.Forms.Label();
            this.lblDichVu = new System.Windows.Forms.Label();
            this.lblNgay = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblNV = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSL)).BeginInit();
            this.SuspendLayout();
            // Lables
            this.lblPhieu.AutoSize = true; this.lblPhieu.Location = new System.Drawing.Point(30, 25); this.lblPhieu.Text = "Phiếu lưu trú:";
            this.lblPhong.AutoSize = true; this.lblPhong.Location = new System.Drawing.Point(300, 25); this.lblPhong.Text = "Phòng:";
            this.lblDichVu.AutoSize = true; this.lblDichVu.Location = new System.Drawing.Point(520, 25); this.lblDichVu.Text = "Dịch vụ:";
            this.lblNgay.AutoSize = true; this.lblNgay.Location = new System.Drawing.Point(30, 65); this.lblNgay.Text = "Ngày sử dụng:";
            this.lblSoLuong.AutoSize = true; this.lblSoLuong.Location = new System.Drawing.Point(300, 65); this.lblSoLuong.Text = "Số lượng:";
            this.lblNV.AutoSize = true; this.lblNV.Location = new System.Drawing.Point(520, 65); this.lblNV.Text = "Nhân viên:";
            // Inputs
            this.cboLuot.Location = new System.Drawing.Point(115, 22); this.cboLuot.Size = new System.Drawing.Size(150, 21); this.cboLuot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtPhong.Location = new System.Drawing.Point(360, 22); this.txtPhong.Size = new System.Drawing.Size(120, 20); this.txtPhong.ReadOnly = true;
            this.cboDV.Location = new System.Drawing.Point(585, 22); this.cboDV.Size = new System.Drawing.Size(160, 21); this.cboDV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dtNgay.Location = new System.Drawing.Point(115, 62); this.dtNgay.Size = new System.Drawing.Size(150, 20); this.dtNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.numSL.Location = new System.Drawing.Point(360, 62); this.numSL.Size = new System.Drawing.Size(120, 20); this.numSL.Minimum = 1; this.numSL.Value = 1;
            this.cboNV.Location = new System.Drawing.Point(585, 62); this.cboNV.Size = new System.Drawing.Size(160, 21); this.cboNV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // Buttons
            this.btnGhi.Location = new System.Drawing.Point(780, 20); this.btnGhi.Size = new System.Drawing.Size(100, 30); this.btnGhi.Text = "Ghi nhận";
            this.btnDong.Location = new System.Drawing.Point(780, 60); this.btnDong.Size = new System.Drawing.Size(100, 30); this.btnDong.Text = "Đóng";
            // DataGridView
            this.dgvLichSu.Location = new System.Drawing.Point(30, 110);
            this.dgvLichSu.Size = new System.Drawing.Size(850, 320);
            this.dgvLichSu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvLichSu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSu.BackgroundColor = System.Drawing.Color.White;
            // Events
            this.cboLuot.SelectedIndexChanged += new System.EventHandler(this.cboLuot_SelectedIndexChanged);
            this.btnGhi.Click += new System.EventHandler(this.btnGhi_Click);
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // FrmDichVu
            this.ClientSize = new System.Drawing.Size(910, 461);
            this.Controls.Add(this.lblPhieu); this.Controls.Add(this.cboLuot);
            this.Controls.Add(this.lblPhong); this.Controls.Add(this.txtPhong);
            this.Controls.Add(this.lblDichVu); this.Controls.Add(this.cboDV);
            this.Controls.Add(this.lblNgay); this.Controls.Add(this.dtNgay);
            this.Controls.Add(this.lblSoLuong); this.Controls.Add(this.numSL);
            this.Controls.Add(this.lblNV); this.Controls.Add(this.cboNV);
            this.Controls.Add(this.btnGhi); this.Controls.Add(this.btnDong);
            this.Controls.Add(this.dgvLichSu);
            this.Name = "FrmDichVu"; this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; this.Text = "Sử dụng dịch vụ";
            this.Load += new System.EventHandler(this.Frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSL)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox cboLuot, cboDV, cboNV;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.TextBox txtPhong;
        private System.Windows.Forms.DateTimePicker dtNgay;
        private System.Windows.Forms.NumericUpDown numSL;
        private System.Windows.Forms.Button btnGhi, btnDong;
        private System.Windows.Forms.Label lblPhieu, lblPhong, lblDichVu, lblNgay, lblSoLuong, lblNV;
    }
}