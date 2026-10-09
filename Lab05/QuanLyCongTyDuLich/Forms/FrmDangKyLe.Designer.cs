namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmDangKyLe
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
            this.lblSo = new System.Windows.Forms.Label();
            this.txtSo = new System.Windows.Forms.TextBox();
            this.lblChuyen = new System.Windows.Forms.Label();
            this.cboChuyen = new System.Windows.Forms.ComboBox();
            this.lblDiemBan = new System.Windows.Forms.Label();
            this.cboDiemBan = new System.Windows.Forms.ComboBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblDT = new System.Windows.Forms.Label();
            this.txtDT = new System.Windows.Forms.TextBox();
            this.lblNguoi = new System.Windows.Forms.Label();
            this.numNguoi = new System.Windows.Forms.NumericUpDown();
            this.lblThanhTienTieuDe = new System.Windows.Forms.Label();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numNguoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSo
            // 
            this.lblSo.AutoSize = true;
            this.lblSo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSo.Location = new System.Drawing.Point(20, 20);
            this.lblSo.Name = "lblSo";
            this.lblSo.Size = new System.Drawing.Size(86, 20);
            this.lblSo.TabIndex = 0;
            this.lblSo.Text = "Số đăng ký:";
            // 
            // txtSo
            // 
            this.txtSo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSo.Location = new System.Drawing.Point(120, 17);
            this.txtSo.Name = "txtSo";
            this.txtSo.Size = new System.Drawing.Size(160, 27);
            this.txtSo.TabIndex = 1;
            // 
            // lblChuyen
            // 
            this.lblChuyen.AutoSize = true;
            this.lblChuyen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChuyen.Location = new System.Drawing.Point(310, 20);
            this.lblChuyen.Name = "lblChuyen";
            this.lblChuyen.Size = new System.Drawing.Size(60, 20);
            this.lblChuyen.TabIndex = 2;
            this.lblChuyen.Text = "Chuyến:";
            // 
            // cboChuyen
            // 
            this.cboChuyen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboChuyen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChuyen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboChuyen.FormattingEnabled = true;
            this.cboChuyen.Location = new System.Drawing.Point(380, 17);
            this.cboChuyen.Name = "cboChuyen";
            this.cboChuyen.Size = new System.Drawing.Size(540, 28);
            this.cboChuyen.TabIndex = 3;
            this.cboChuyen.SelectedIndexChanged += new System.EventHandler(this.TinhTien);
            // 
            // lblDiemBan
            // 
            this.lblDiemBan.AutoSize = true;
            this.lblDiemBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiemBan.Location = new System.Drawing.Point(20, 60);
            this.lblDiemBan.Name = "lblDiemBan";
            this.lblDiemBan.Size = new System.Drawing.Size(95, 20);
            this.lblDiemBan.TabIndex = 4;
            this.lblDiemBan.Text = "Điểm bán vé:";
            // 
            // cboDiemBan
            // 
            this.cboDiemBan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDiemBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDiemBan.FormattingEnabled = true;
            this.cboDiemBan.Location = new System.Drawing.Point(120, 57);
            this.cboDiemBan.Name = "cboDiemBan";
            this.cboDiemBan.Size = new System.Drawing.Size(180, 28);
            this.cboDiemBan.TabIndex = 5;
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.Location = new System.Drawing.Point(310, 60);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(115, 20);
            this.lblTen.TabIndex = 6;
            this.lblTen.Text = "Người đăng ký:";
            // 
            // txtTen
            // 
            this.txtTen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTen.Location = new System.Drawing.Point(430, 57);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(220, 27);
            this.txtTen.TabIndex = 7;
            // 
            // lblDT
            // 
            this.lblDT.AutoSize = true;
            this.lblDT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDT.Location = new System.Drawing.Point(670, 60);
            this.lblDT.Name = "lblDT";
            this.lblDT.Size = new System.Drawing.Size(81, 20);
            this.lblDT.TabIndex = 8;
            this.lblDT.Text = "Điện thoại:";
            // 
            // txtDT
            // 
            this.txtDT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDT.Location = new System.Drawing.Point(760, 57);
            this.txtDT.Name = "txtDT";
            this.txtDT.Size = new System.Drawing.Size(160, 27);
            this.txtDT.TabIndex = 9;
            // 
            // lblNguoi
            // 
            this.lblNguoi.AutoSize = true;
            this.lblNguoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNguoi.Location = new System.Drawing.Point(20, 105);
            this.lblNguoi.Name = "lblNguoi";
            this.lblNguoi.Size = new System.Drawing.Size(72, 20);
            this.lblNguoi.TabIndex = 10;
            this.lblNguoi.Text = "Số người:";
            // 
            // numNguoi
            // 
            this.numNguoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numNguoi.Location = new System.Drawing.Point(120, 103);
            this.numNguoi.Maximum = new decimal(new int[] { 11, 0, 0, 0 });
            this.numNguoi.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numNguoi.Name = "numNguoi";
            this.numNguoi.Size = new System.Drawing.Size(80, 27);
            this.numNguoi.TabIndex = 11;
            this.numNguoi.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.numNguoi.ValueChanged += new System.EventHandler(this.TinhTien);
            // 
            // lblThanhTienTieuDe
            // 
            this.lblThanhTienTieuDe.AutoSize = true;
            this.lblThanhTienTieuDe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThanhTienTieuDe.Location = new System.Drawing.Point(230, 105);
            this.lblThanhTienTieuDe.Name = "lblThanhTienTieuDe";
            this.lblThanhTienTieuDe.Size = new System.Drawing.Size(101, 23);
            this.lblThanhTienTieuDe.TabIndex = 12;
            this.lblThanhTienTieuDe.Text = "Thành tiền:";
            // 
            // lblThanhTien
            // 
            this.lblThanhTien.AutoSize = true;
            this.lblThanhTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThanhTien.ForeColor = System.Drawing.Color.Brown;
            this.lblThanhTien.Location = new System.Drawing.Point(340, 103);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Size = new System.Drawing.Size(43, 25);
            this.lblThanhTien.TabIndex = 13;
            this.lblThanhTien.Text = "0 đ";
            // 
            // btnDangKy
            // 
            this.btnDangKy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDangKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKy.Location = new System.Drawing.Point(700, 98);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(220, 36);
            this.btnDangKy.TabIndex = 14;
            this.btnDangKy.Text = "Đăng ký và thanh toán vé";
            this.btnDangKy.UseVisualStyleBackColor = true;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(24, 150);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(896, 360);
            this.dgv.TabIndex = 15;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(800, 525);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 35);
            this.btnDong.TabIndex = 16;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmDangKyLe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 580);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.lblThanhTien);
            this.Controls.Add(this.lblThanhTienTieuDe);
            this.Controls.Add(this.numNguoi);
            this.Controls.Add(this.lblNguoi);
            this.Controls.Add(this.txtDT);
            this.Controls.Add(this.lblDT);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.lblTen);
            this.Controls.Add(this.cboDiemBan);
            this.Controls.Add(this.lblDiemBan);
            this.Controls.Add(this.cboChuyen);
            this.Controls.Add(this.lblChuyen);
            this.Controls.Add(this.txtSo);
            this.Controls.Add(this.lblSo);
            this.Name = "FrmDangKyLe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng ký khách lẻ theo chuyến";
            this.Load += new System.EventHandler(this.FrmDangKyLe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numNguoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSo;
        private System.Windows.Forms.TextBox txtSo;
        private System.Windows.Forms.Label lblChuyen;
        private System.Windows.Forms.ComboBox cboChuyen;
        private System.Windows.Forms.Label lblDiemBan;
        private System.Windows.Forms.ComboBox cboDiemBan;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblDT;
        private System.Windows.Forms.TextBox txtDT;
        private System.Windows.Forms.Label lblNguoi;
        private System.Windows.Forms.NumericUpDown numNguoi;
        private System.Windows.Forms.Label lblThanhTienTieuDe;
        private System.Windows.Forms.Label lblThanhTien;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnDong;
    }
}
