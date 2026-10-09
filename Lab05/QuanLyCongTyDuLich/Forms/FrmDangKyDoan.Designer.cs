namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmDangKyDoan
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
            this.grpDoan = new System.Windows.Forms.GroupBox();
            this.txtDaiDien = new System.Windows.Forms.TextBox();
            this.lblDaiDien = new System.Windows.Forms.Label();
            this.txtDT = new System.Windows.Forms.TextBox();
            this.lblDT = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtTenCQ = new System.Windows.Forms.TextBox();
            this.lblTenCQ = new System.Windows.Forms.Label();
            this.txtMaDoan = new System.Windows.Forms.TextBox();
            this.lblMaDoan = new System.Windows.Forms.Label();
            this.grpDangKy = new System.Windows.Forms.GroupBox();
            this.lblTong = new System.Windows.Forms.Label();
            this.lblTongTieuDe = new System.Windows.Forms.Label();
            this.lblKetThuc = new System.Windows.Forms.Label();
            this.lblKetThucTieuDe = new System.Windows.Forms.Label();
            this.chkBH = new System.Windows.Forms.CheckBox();
            this.numCoc = new System.Windows.Forms.NumericUpDown();
            this.lblCoc = new System.Windows.Forms.Label();
            this.txtDon = new System.Windows.Forms.TextBox();
            this.lblDon = new System.Windows.Forms.Label();
            this.numNguoi = new System.Windows.Forms.NumericUpDown();
            this.lblNguoi = new System.Windows.Forms.Label();
            this.dtDi = new System.Windows.Forms.DateTimePicker();
            this.lblNgayDi = new System.Windows.Forms.Label();
            this.cboTour = new System.Windows.Forms.ComboBox();
            this.lblTour = new System.Windows.Forms.Label();
            this.txtSo = new System.Windows.Forms.TextBox();
            this.lblSo = new System.Windows.Forms.Label();
            this.lblThanhVien = new System.Windows.Forms.Label();
            this.dgvThanhVien = new System.Windows.Forms.DataGridView();
            this.btnDangKy = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.lblDanhSach = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            this.grpDoan.SuspendLayout();
            this.grpDangKy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNguoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThanhVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDoan
            // 
            this.grpDoan.Controls.Add(this.txtDaiDien);
            this.grpDoan.Controls.Add(this.lblDaiDien);
            this.grpDoan.Controls.Add(this.txtDT);
            this.grpDoan.Controls.Add(this.lblDT);
            this.grpDoan.Controls.Add(this.txtDiaChi);
            this.grpDoan.Controls.Add(this.lblDiaChi);
            this.grpDoan.Controls.Add(this.txtTenCQ);
            this.grpDoan.Controls.Add(this.lblTenCQ);
            this.grpDoan.Controls.Add(this.txtMaDoan);
            this.grpDoan.Controls.Add(this.lblMaDoan);
            this.grpDoan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDoan.Location = new System.Drawing.Point(12, 12);
            this.grpDoan.Name = "grpDoan";
            this.grpDoan.Size = new System.Drawing.Size(460, 205);
            this.grpDoan.TabIndex = 0;
            this.grpDoan.TabStop = false;
            this.grpDoan.Text = "Thông tin đoàn khách";
            // 
            // txtDaiDien
            // 
            this.txtDaiDien.Location = new System.Drawing.Point(135, 163);
            this.txtDaiDien.Name = "txtDaiDien";
            this.txtDaiDien.Size = new System.Drawing.Size(305, 27);
            this.txtDaiDien.TabIndex = 9;
            // 
            // lblDaiDien
            // 
            this.lblDaiDien.AutoSize = true;
            this.lblDaiDien.Location = new System.Drawing.Point(15, 166);
            this.lblDaiDien.Name = "lblDaiDien";
            this.lblDaiDien.Size = new System.Drawing.Size(105, 20);
            this.lblDaiDien.TabIndex = 8;
            this.lblDaiDien.Text = "Người đại diện:";
            // 
            // txtDT
            // 
            this.txtDT.Location = new System.Drawing.Point(135, 128);
            this.txtDT.Name = "txtDT";
            this.txtDT.Size = new System.Drawing.Size(305, 27);
            this.txtDT.TabIndex = 7;
            // 
            // lblDT
            // 
            this.lblDT.AutoSize = true;
            this.lblDT.Location = new System.Drawing.Point(15, 131);
            this.lblDT.Name = "lblDT";
            this.lblDT.Size = new System.Drawing.Size(81, 20);
            this.lblDT.TabIndex = 6;
            this.lblDT.Text = "Điện thoại:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(135, 93);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(305, 27);
            this.txtDiaChi.TabIndex = 5;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Location = new System.Drawing.Point(15, 96);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(58, 20);
            this.lblDiaChi.TabIndex = 4;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtTenCQ
            // 
            this.txtTenCQ.Location = new System.Drawing.Point(135, 58);
            this.txtTenCQ.Name = "txtTenCQ";
            this.txtTenCQ.Size = new System.Drawing.Size(305, 27);
            this.txtTenCQ.TabIndex = 3;
            // 
            // lblTenCQ
            // 
            this.lblTenCQ.AutoSize = true;
            this.lblTenCQ.Location = new System.Drawing.Point(15, 61);
            this.lblTenCQ.Name = "lblTenCQ";
            this.lblTenCQ.Size = new System.Drawing.Size(117, 20);
            this.lblTenCQ.TabIndex = 2;
            this.lblTenCQ.Text = "Cơ quan/gia đình:";
            // 
            // txtMaDoan
            // 
            this.txtMaDoan.Location = new System.Drawing.Point(135, 23);
            this.txtMaDoan.Name = "txtMaDoan";
            this.txtMaDoan.Size = new System.Drawing.Size(180, 27);
            this.txtMaDoan.TabIndex = 1;
            // 
            // lblMaDoan
            // 
            this.lblMaDoan.AutoSize = true;
            this.lblMaDoan.Location = new System.Drawing.Point(15, 26);
            this.lblMaDoan.Name = "lblMaDoan";
            this.lblMaDoan.Size = new System.Drawing.Size(72, 20);
            this.lblMaDoan.TabIndex = 0;
            this.lblMaDoan.Text = "Mã đoàn:";
            // 
            // grpDangKy
            // 
            this.grpDangKy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDangKy.Controls.Add(this.lblTong);
            this.grpDangKy.Controls.Add(this.lblTongTieuDe);
            this.grpDangKy.Controls.Add(this.lblKetThuc);
            this.grpDangKy.Controls.Add(this.lblKetThucTieuDe);
            this.grpDangKy.Controls.Add(this.chkBH);
            this.grpDangKy.Controls.Add(this.numCoc);
            this.grpDangKy.Controls.Add(this.lblCoc);
            this.grpDangKy.Controls.Add(this.txtDon);
            this.grpDangKy.Controls.Add(this.lblDon);
            this.grpDangKy.Controls.Add(this.numNguoi);
            this.grpDangKy.Controls.Add(this.lblNguoi);
            this.grpDangKy.Controls.Add(this.dtDi);
            this.grpDangKy.Controls.Add(this.lblNgayDi);
            this.grpDangKy.Controls.Add(this.cboTour);
            this.grpDangKy.Controls.Add(this.lblTour);
            this.grpDangKy.Controls.Add(this.txtSo);
            this.grpDangKy.Controls.Add(this.lblSo);
            this.grpDangKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDangKy.Location = new System.Drawing.Point(485, 12);
            this.grpDangKy.Name = "grpDangKy";
            this.grpDangKy.Size = new System.Drawing.Size(517, 205);
            this.grpDangKy.TabIndex = 1;
            this.grpDangKy.TabStop = false;
            this.grpDangKy.Text = "Đăng ký tour";
            // 
            // lblTong
            // 
            this.lblTong.AutoSize = true;
            this.lblTong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTong.ForeColor = System.Drawing.Color.Brown;
            this.lblTong.Location = new System.Drawing.Point(340, 166);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(37, 23);
            this.lblTong.TabIndex = 16;
            this.lblTong.Text = "0 đ";
            // 
            // lblTongTieuDe
            // 
            this.lblTongTieuDe.AutoSize = true;
            this.lblTongTieuDe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTieuDe.Location = new System.Drawing.Point(230, 168);
            this.lblTongTieuDe.Name = "lblTongTieuDe";
            this.lblTongTieuDe.Size = new System.Drawing.Size(107, 20);
            this.lblTongTieuDe.TabIndex = 15;
            this.lblTongTieuDe.Text = "Tổng dự kiến:";
            // 
            // lblKetThuc
            // 
            this.lblKetThuc.AutoSize = true;
            this.lblKetThuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKetThuc.Location = new System.Drawing.Point(100, 168);
            this.lblKetThuc.Name = "lblKetThuc";
            this.lblKetThuc.Size = new System.Drawing.Size(15, 20);
            this.lblKetThuc.TabIndex = 14;
            this.lblKetThuc.Text = "-";
            // 
            // lblKetThucTieuDe
            // 
            this.lblKetThucTieuDe.AutoSize = true;
            this.lblKetThucTieuDe.Location = new System.Drawing.Point(10, 168);
            this.lblKetThucTieuDe.Name = "lblKetThucTieuDe";
            this.lblKetThucTieuDe.Size = new System.Drawing.Size(91, 20);
            this.lblKetThucTieuDe.TabIndex = 13;
            this.lblKetThucTieuDe.Text = "Kết thúc DK:";
            // 
            // chkBH
            // 
            this.chkBH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBH.AutoSize = true;
            this.chkBH.Location = new System.Drawing.Point(340, 131);
            this.chkBH.Name = "chkBH";
            this.chkBH.Size = new System.Drawing.Size(126, 24);
            this.chkBH.TabIndex = 12;
            this.chkBH.Text = "Mua bảo hiểm";
            this.chkBH.UseVisualStyleBackColor = true;
            this.chkBH.CheckedChanged += new System.EventHandler(this.chkBH_CheckedChanged);
            // 
            // numCoc
            // 
            this.numCoc.DecimalPlaces = 0;
            this.numCoc.Increment = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numCoc.Location = new System.Drawing.Point(90, 128);
            this.numCoc.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numCoc.Name = "numCoc";
            this.numCoc.Size = new System.Drawing.Size(130, 27);
            this.numCoc.TabIndex = 11;
            this.numCoc.ThousandsSeparator = true;
            // 
            // lblCoc
            // 
            this.lblCoc.AutoSize = true;
            this.lblCoc.Location = new System.Drawing.Point(10, 131);
            this.lblCoc.Name = "lblCoc";
            this.lblCoc.Size = new System.Drawing.Size(69, 20);
            this.lblCoc.TabIndex = 10;
            this.lblCoc.Text = "Tiền cọc:";
            // 
            // txtDon
            // 
            this.txtDon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDon.Location = new System.Drawing.Point(90, 93);
            this.txtDon.Name = "txtDon";
            this.txtDon.Size = new System.Drawing.Size(415, 27);
            this.txtDon.TabIndex = 9;
            // 
            // lblDon
            // 
            this.lblDon.AutoSize = true;
            this.lblDon.Location = new System.Drawing.Point(10, 96);
            this.lblDon.Name = "lblDon";
            this.lblDon.Size = new System.Drawing.Size(73, 20);
            this.lblDon.TabIndex = 8;
            this.lblDon.Text = "Điểm đón:";
            // 
            // numNguoi
            // 
            this.numNguoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numNguoi.Location = new System.Drawing.Point(340, 58);
            this.numNguoi.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numNguoi.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numNguoi.Name = "numNguoi";
            this.numNguoi.Size = new System.Drawing.Size(90, 27);
            this.numNguoi.TabIndex = 7;
            this.numNguoi.Value = new decimal(new int[] { 15, 0, 0, 0 });
            this.numNguoi.ValueChanged += new System.EventHandler(this.TinhTong);
            // 
            // lblNguoi
            // 
            this.lblNguoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNguoi.AutoSize = true;
            this.lblNguoi.Location = new System.Drawing.Point(265, 61);
            this.lblNguoi.Name = "lblNguoi";
            this.lblNguoi.Size = new System.Drawing.Size(72, 20);
            this.lblNguoi.TabIndex = 6;
            this.lblNguoi.Text = "Số người:";
            // 
            // dtDi
            // 
            this.dtDi.CustomFormat = "dd/MM/yyyy";
            this.dtDi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDi.Location = new System.Drawing.Point(90, 58);
            this.dtDi.Name = "dtDi";
            this.dtDi.Size = new System.Drawing.Size(130, 27);
            this.dtDi.TabIndex = 5;
            this.dtDi.ValueChanged += new System.EventHandler(this.TinhTong);
            // 
            // lblNgayDi
            // 
            this.lblNgayDi.AutoSize = true;
            this.lblNgayDi.Location = new System.Drawing.Point(10, 61);
            this.lblNgayDi.Name = "lblNgayDi";
            this.lblNgayDi.Size = new System.Drawing.Size(64, 20);
            this.lblNgayDi.TabIndex = 4;
            this.lblNgayDi.Text = "Ngày đi:";
            // 
            // cboTour
            // 
            this.cboTour.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTour.FormattingEnabled = true;
            this.cboTour.Location = new System.Drawing.Point(280, 23);
            this.cboTour.Name = "cboTour";
            this.cboTour.Size = new System.Drawing.Size(225, 28);
            this.cboTour.TabIndex = 3;
            this.cboTour.SelectedIndexChanged += new System.EventHandler(this.TinhTong);
            // 
            // lblTour
            // 
            this.lblTour.AutoSize = true;
            this.lblTour.Location = new System.Drawing.Point(235, 26);
            this.lblTour.Name = "lblTour";
            this.lblTour.Size = new System.Drawing.Size(41, 20);
            this.lblTour.TabIndex = 2;
            this.lblTour.Text = "Tour:";
            // 
            // txtSo
            // 
            this.txtSo.Location = new System.Drawing.Point(90, 23);
            this.txtSo.Name = "txtSo";
            this.txtSo.Size = new System.Drawing.Size(130, 27);
            this.txtSo.TabIndex = 1;
            // 
            // lblSo
            // 
            this.lblSo.AutoSize = true;
            this.lblSo.Location = new System.Drawing.Point(10, 26);
            this.lblSo.Name = "lblSo";
            this.lblSo.Size = new System.Drawing.Size(68, 20);
            this.lblSo.TabIndex = 0;
            this.lblSo.Text = "Số phiếu:";
            // 
            // lblThanhVien
            // 
            this.lblThanhVien.AutoSize = true;
            this.lblThanhVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThanhVien.Location = new System.Drawing.Point(12, 226);
            this.lblThanhVien.Name = "lblThanhVien";
            this.lblThanhVien.Size = new System.Drawing.Size(462, 20);
            this.lblThanhVien.TabIndex = 2;
            this.lblThanhVien.Text = "Danh sách người cùng đi (bắt buộc đủ số người khi mua bảo hiểm):";
            // 
            // dgvThanhVien
            // 
            this.dgvThanhVien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvThanhVien.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThanhVien.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThanhVien.Location = new System.Drawing.Point(12, 249);
            this.dgvThanhVien.Name = "dgvThanhVien";
            this.dgvThanhVien.RowHeadersWidth = 51;
            this.dgvThanhVien.RowTemplate.Height = 24;
            this.dgvThanhVien.Size = new System.Drawing.Size(990, 115);
            this.dgvThanhVien.TabIndex = 3;
            // 
            // btnDangKy
            // 
            this.btnDangKy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDangKy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangKy.Location = new System.Drawing.Point(650, 372);
            this.btnDangKy.Name = "btnDangKy";
            this.btnDangKy.Size = new System.Drawing.Size(165, 36);
            this.btnDangKy.TabIndex = 4;
            this.btnDangKy.Text = "Lập phiếu đăng ký";
            this.btnDangKy.UseVisualStyleBackColor = true;
            this.btnDangKy.Click += new System.EventHandler(this.btnDangKy_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.Location = new System.Drawing.Point(830, 372);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(170, 36);
            this.btnHuy.TabIndex = 5;
            this.btnHuy.Text = "Hủy phiếu (mất cọc)";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // lblDanhSach
            // 
            this.lblDanhSach.AutoSize = true;
            this.lblDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDanhSach.Location = new System.Drawing.Point(12, 388);
            this.lblDanhSach.Name = "lblDanhSach";
            this.lblDanhSach.Size = new System.Drawing.Size(175, 20);
            this.lblDanhSach.TabIndex = 6;
            this.lblDanhSach.Text = "Các phiếu đăng ký đoàn:";
            // 
            // dgv
            // 
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(12, 415);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 51;
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(990, 160);
            this.dgv.TabIndex = 7;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(882, 585);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 35);
            this.btnDong.TabIndex = 8;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmDangKyDoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 650);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.lblDanhSach);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDangKy);
            this.Controls.Add(this.dgvThanhVien);
            this.Controls.Add(this.lblThanhVien);
            this.Controls.Add(this.grpDangKy);
            this.Controls.Add(this.grpDoan);
            this.Name = "FrmDangKyDoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu đăng ký theo đoàn";
            this.Load += new System.EventHandler(this.FrmDangKyDoan_Load);
            this.grpDoan.ResumeLayout(false);
            this.grpDoan.PerformLayout();
            this.grpDangKy.ResumeLayout(false);
            this.grpDangKy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNguoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThanhVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDoan;
        private System.Windows.Forms.TextBox txtDaiDien;
        private System.Windows.Forms.Label lblDaiDien;
        private System.Windows.Forms.TextBox txtDT;
        private System.Windows.Forms.Label lblDT;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtTenCQ;
        private System.Windows.Forms.Label lblTenCQ;
        private System.Windows.Forms.TextBox txtMaDoan;
        private System.Windows.Forms.Label lblMaDoan;
        private System.Windows.Forms.GroupBox grpDangKy;
        private System.Windows.Forms.TextBox txtSo;
        private System.Windows.Forms.Label lblSo;
        private System.Windows.Forms.Label lblTour;
        private System.Windows.Forms.ComboBox cboTour;
        private System.Windows.Forms.Label lblNgayDi;
        private System.Windows.Forms.DateTimePicker dtDi;
        private System.Windows.Forms.Label lblNguoi;
        private System.Windows.Forms.NumericUpDown numNguoi;
        private System.Windows.Forms.Label lblDon;
        private System.Windows.Forms.TextBox txtDon;
        private System.Windows.Forms.Label lblCoc;
        private System.Windows.Forms.NumericUpDown numCoc;
        private System.Windows.Forms.CheckBox chkBH;
        private System.Windows.Forms.Label lblKetThucTieuDe;
        private System.Windows.Forms.Label lblKetThuc;
        private System.Windows.Forms.Label lblTongTieuDe;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.Label lblThanhVien;
        private System.Windows.Forms.DataGridView dgvThanhVien;
        private System.Windows.Forms.Button btnDangKy;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Label lblDanhSach;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnDong;
    }
}
