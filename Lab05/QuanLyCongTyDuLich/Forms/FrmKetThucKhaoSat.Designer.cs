namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmKetThucKhaoSat
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
            this.tabKT = new System.Windows.Forms.TabControl();
            this.tabThanhToanPage = new System.Windows.Forms.TabPage();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.numTien = new System.Windows.Forms.NumericUpDown();
            this.lblSoTien = new System.Windows.Forms.Label();
            this.dtTT = new System.Windows.Forms.DateTimePicker();
            this.lblNgayTT = new System.Windows.Forms.Label();
            this.txtSoDK = new System.Windows.Forms.TextBox();
            this.lblSoDK = new System.Windows.Forms.Label();
            this.txtSoTT = new System.Windows.Forms.TextBox();
            this.lblSoTT = new System.Windows.Forms.Label();
            this.dgvDoan = new System.Windows.Forms.DataGridView();
            this.tabKhaoSatPage = new System.Windows.Forms.TabPage();
            this.btnGhiPH = new System.Windows.Forms.Button();
            this.txtGopY = new System.Windows.Forms.TextBox();
            this.lblGopY = new System.Windows.Forms.Label();
            this.numDiem = new System.Windows.Forms.NumericUpDown();
            this.lblDiem = new System.Windows.Forms.Label();
            this.dtPH = new System.Windows.Forms.DateTimePicker();
            this.lblNgayPH = new System.Windows.Forms.Label();
            this.txtKSChon = new System.Windows.Forms.TextBox();
            this.lblKSChon = new System.Windows.Forms.Label();
            this.dgvKS = new System.Windows.Forms.DataGridView();
            this.btnGui = new System.Windows.Forms.Button();
            this.dtGui = new System.Windows.Forms.DateTimePicker();
            this.lblNgayGui = new System.Windows.Forms.Label();
            this.txtMaKS = new System.Windows.Forms.TextBox();
            this.lblMaKS = new System.Windows.Forms.Label();
            this.cboDangKy = new System.Windows.Forms.ComboBox();
            this.lblDangKy = new System.Windows.Forms.Label();
            this.cboLoaiKS = new System.Windows.Forms.ComboBox();
            this.lblLoaiKS = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.tabKT.SuspendLayout();
            this.tabThanhToanPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoan)).BeginInit();
            this.tabKhaoSatPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKS)).BeginInit();
            this.SuspendLayout();
            // 
            // tabKT
            // 
            this.tabKT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabKT.Controls.Add(this.tabThanhToanPage);
            this.tabKT.Controls.Add(this.tabKhaoSatPage);
            this.tabKT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabKT.Location = new System.Drawing.Point(12, 12);
            this.tabKT.Name = "tabKT";
            this.tabKT.SelectedIndex = 0;
            this.tabKT.Size = new System.Drawing.Size(960, 525);
            this.tabKT.TabIndex = 0;
            // 
            // tabThanhToanPage
            // 
            this.tabThanhToanPage.Controls.Add(this.btnThanhToan);
            this.tabThanhToanPage.Controls.Add(this.txtGhiChu);
            this.tabThanhToanPage.Controls.Add(this.lblGhiChu);
            this.tabThanhToanPage.Controls.Add(this.numTien);
            this.tabThanhToanPage.Controls.Add(this.lblSoTien);
            this.tabThanhToanPage.Controls.Add(this.dtTT);
            this.tabThanhToanPage.Controls.Add(this.lblNgayTT);
            this.tabThanhToanPage.Controls.Add(this.txtSoDK);
            this.tabThanhToanPage.Controls.Add(this.lblSoDK);
            this.tabThanhToanPage.Controls.Add(this.txtSoTT);
            this.tabThanhToanPage.Controls.Add(this.lblSoTT);
            this.tabThanhToanPage.Controls.Add(this.dgvDoan);
            this.tabThanhToanPage.Location = new System.Drawing.Point(4, 29);
            this.tabThanhToanPage.Name = "tabThanhToanPage";
            this.tabThanhToanPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabThanhToanPage.Size = new System.Drawing.Size(952, 492);
            this.tabThanhToanPage.TabIndex = 0;
            this.tabThanhToanPage.Text = "Thanh toán sau tour (đoàn)";
            this.tabThanhToanPage.UseVisualStyleBackColor = true;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThanhToan.Location = new System.Drawing.Point(780, 440);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(160, 36);
            this.btnThanhToan.TabIndex = 11;
            this.btnThanhToan.Text = "Ghi nhận thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = true;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChu.Location = new System.Drawing.Point(380, 444);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(380, 27);
            this.txtGhiChu.TabIndex = 10;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new System.Drawing.Point(310, 447);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(61, 20);
            this.lblGhiChu.TabIndex = 9;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // numTien
            // 
            this.numTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numTien.DecimalPlaces = 0;
            this.numTien.Increment = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.numTien.Location = new System.Drawing.Point(120, 444);
            this.numTien.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numTien.Name = "numTien";
            this.numTien.Size = new System.Drawing.Size(170, 27);
            this.numTien.TabIndex = 8;
            this.numTien.ThousandsSeparator = true;
            // 
            // lblSoTien
            // 
            this.lblSoTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSoTien.AutoSize = true;
            this.lblSoTien.Location = new System.Drawing.Point(15, 447);
            this.lblSoTien.Name = "lblSoTien";
            this.lblSoTien.Size = new System.Drawing.Size(58, 20);
            this.lblSoTien.TabIndex = 7;
            this.lblSoTien.Text = "Số tiền:";
            // 
            // dtTT
            // 
            this.dtTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dtTT.CustomFormat = "dd/MM/yyyy";
            this.dtTT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTT.Location = new System.Drawing.Point(740, 401);
            this.dtTT.Name = "dtTT";
            this.dtTT.Size = new System.Drawing.Size(140, 27);
            this.dtTT.TabIndex = 6;
            // 
            // lblNgayTT
            // 
            this.lblNgayTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNgayTT.AutoSize = true;
            this.lblNgayTT.Location = new System.Drawing.Point(610, 404);
            this.lblNgayTT.Name = "lblNgayTT";
            this.lblNgayTT.Size = new System.Drawing.Size(124, 20);
            this.lblNgayTT.TabIndex = 5;
            this.lblNgayTT.Text = "Ngày thanh toán:";
            // 
            // txtSoDK
            // 
            this.txtSoDK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSoDK.Location = new System.Drawing.Point(380, 401);
            this.txtSoDK.Name = "txtSoDK";
            this.txtSoDK.ReadOnly = true;
            this.txtSoDK.Size = new System.Drawing.Size(200, 27);
            this.txtSoDK.TabIndex = 4;
            // 
            // lblSoDK
            // 
            this.lblSoDK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSoDK.AutoSize = true;
            this.lblSoDK.Location = new System.Drawing.Point(310, 404);
            this.lblSoDK.Name = "lblSoDK";
            this.lblSoDK.Size = new System.Drawing.Size(68, 20);
            this.lblSoDK.TabIndex = 3;
            this.lblSoDK.Text = "Phiếu đoàn:";
            // 
            // txtSoTT
            // 
            this.txtSoTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSoTT.Location = new System.Drawing.Point(120, 401);
            this.txtSoTT.Name = "txtSoTT";
            this.txtSoTT.Size = new System.Drawing.Size(170, 27);
            this.txtSoTT.TabIndex = 2;
            // 
            // lblSoTT
            // 
            this.lblSoTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSoTT.AutoSize = true;
            this.lblSoTT.Location = new System.Drawing.Point(15, 404);
            this.lblSoTT.Name = "lblSoTT";
            this.lblSoTT.Size = new System.Drawing.Size(103, 20);
            this.lblSoTT.TabIndex = 1;
            this.lblSoTT.Text = "Số thanh toán:";
            // 
            // dgvDoan
            // 
            this.dgvDoan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDoan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoan.Location = new System.Drawing.Point(6, 6);
            this.dgvDoan.Name = "dgvDoan";
            this.dgvDoan.RowHeadersWidth = 51;
            this.dgvDoan.RowTemplate.Height = 24;
            this.dgvDoan.Size = new System.Drawing.Size(940, 380);
            this.dgvDoan.TabIndex = 0;
            this.dgvDoan.SelectionChanged += new System.EventHandler(this.dgvDoan_SelectionChanged);
            // 
            // tabKhaoSatPage
            // 
            this.tabKhaoSatPage.Controls.Add(this.btnGhiPH);
            this.tabKhaoSatPage.Controls.Add(this.txtGopY);
            this.tabKhaoSatPage.Controls.Add(this.lblGopY);
            this.tabKhaoSatPage.Controls.Add(this.numDiem);
            this.tabKhaoSatPage.Controls.Add(this.lblDiem);
            this.tabKhaoSatPage.Controls.Add(this.dtPH);
            this.tabKhaoSatPage.Controls.Add(this.lblNgayPH);
            this.tabKhaoSatPage.Controls.Add(this.txtKSChon);
            this.tabKhaoSatPage.Controls.Add(this.lblKSChon);
            this.tabKhaoSatPage.Controls.Add(this.dgvKS);
            this.tabKhaoSatPage.Controls.Add(this.btnGui);
            this.tabKhaoSatPage.Controls.Add(this.dtGui);
            this.tabKhaoSatPage.Controls.Add(this.lblNgayGui);
            this.tabKhaoSatPage.Controls.Add(this.txtMaKS);
            this.tabKhaoSatPage.Controls.Add(this.lblMaKS);
            this.tabKhaoSatPage.Controls.Add(this.cboDangKy);
            this.tabKhaoSatPage.Controls.Add(this.lblDangKy);
            this.tabKhaoSatPage.Controls.Add(this.cboLoaiKS);
            this.tabKhaoSatPage.Controls.Add(this.lblLoaiKS);
            this.tabKhaoSatPage.Location = new System.Drawing.Point(4, 29);
            this.tabKhaoSatPage.Name = "tabKhaoSatPage";
            this.tabKhaoSatPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabKhaoSatPage.Size = new System.Drawing.Size(952, 492);
            this.tabKhaoSatPage.TabIndex = 1;
            this.tabKhaoSatPage.Text = "Khảo sát khách hàng";
            this.tabKhaoSatPage.UseVisualStyleBackColor = true;
            // 
            // btnGhiPH
            // 
            this.btnGhiPH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGhiPH.Location = new System.Drawing.Point(790, 444);
            this.btnGhiPH.Name = "btnGhiPH";
            this.btnGhiPH.Size = new System.Drawing.Size(150, 36);
            this.btnGhiPH.TabIndex = 18;
            this.btnGhiPH.Text = "Ghi nhận góp ý";
            this.btnGhiPH.UseVisualStyleBackColor = true;
            this.btnGhiPH.Click += new System.EventHandler(this.btnGhiPH_Click);
            // 
            // txtGopY
            // 
            this.txtGopY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGopY.Location = new System.Drawing.Point(85, 448);
            this.txtGopY.Name = "txtGopY";
            this.txtGopY.Size = new System.Drawing.Size(690, 27);
            this.txtGopY.TabIndex = 17;
            // 
            // lblGopY
            // 
            this.lblGopY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGopY.AutoSize = true;
            this.lblGopY.Location = new System.Drawing.Point(15, 451);
            this.lblGopY.Name = "lblGopY";
            this.lblGopY.Size = new System.Drawing.Size(51, 20);
            this.lblGopY.TabIndex = 16;
            this.lblGopY.Text = "Góp ý:";
            // 
            // numDiem
            // 
            this.numDiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numDiem.Location = new System.Drawing.Point(620, 407);
            this.numDiem.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            this.numDiem.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numDiem.Name = "numDiem";
            this.numDiem.Size = new System.Drawing.Size(60, 27);
            this.numDiem.TabIndex = 15;
            this.numDiem.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // lblDiem
            // 
            this.lblDiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDiem.AutoSize = true;
            this.lblDiem.Location = new System.Drawing.Point(530, 410);
            this.lblDiem.Name = "lblDiem";
            this.lblDiem.Size = new System.Drawing.Size(81, 20);
            this.lblDiem.TabIndex = 14;
            this.lblDiem.Text = "Điểm (1-5):";
            // 
            // dtPH
            // 
            this.dtPH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtPH.CustomFormat = "dd/MM/yyyy";
            this.dtPH.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPH.Location = new System.Drawing.Point(375, 407);
            this.dtPH.Name = "dtPH";
            this.dtPH.Size = new System.Drawing.Size(130, 27);
            this.dtPH.TabIndex = 13;
            // 
            // lblNgayPH
            // 
            this.lblNgayPH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNgayPH.AutoSize = true;
            this.lblNgayPH.Location = new System.Drawing.Point(260, 410);
            this.lblNgayPH.Name = "lblNgayPH";
            this.lblNgayPH.Size = new System.Drawing.Size(109, 20);
            this.lblNgayPH.TabIndex = 12;
            this.lblNgayPH.Text = "Ngày phản hồi:";
            // 
            // txtKSChon
            // 
            this.txtKSChon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtKSChon.Location = new System.Drawing.Point(105, 407);
            this.txtKSChon.Name = "txtKSChon";
            this.txtKSChon.ReadOnly = true;
            this.txtKSChon.Size = new System.Drawing.Size(130, 27);
            this.txtKSChon.TabIndex = 11;
            // 
            // lblKSChon
            // 
            this.lblKSChon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblKSChon.AutoSize = true;
            this.lblKSChon.Location = new System.Drawing.Point(15, 410);
            this.lblKSChon.Name = "lblKSChon";
            this.lblKSChon.Size = new System.Drawing.Size(84, 20);
            this.lblKSChon.TabIndex = 10;
            this.lblKSChon.Text = "Phiếu chọn:";
            // 
            // dgvKS
            // 
            this.dgvKS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvKS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKS.Location = new System.Drawing.Point(6, 95);
            this.dgvKS.Name = "dgvKS";
            this.dgvKS.RowHeadersWidth = 51;
            this.dgvKS.RowTemplate.Height = 24;
            this.dgvKS.Size = new System.Drawing.Size(940, 295);
            this.dgvKS.TabIndex = 9;
            this.dgvKS.SelectionChanged += new System.EventHandler(this.dgvKS_SelectionChanged);
            // 
            // btnGui
            // 
            this.btnGui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGui.Location = new System.Drawing.Point(790, 50);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(150, 36);
            this.btnGui.TabIndex = 8;
            this.btnGui.Text = "Gửi phiếu khảo sát";
            this.btnGui.UseVisualStyleBackColor = true;
            this.btnGui.Click += new System.EventHandler(this.btnGui_Click);
            // 
            // dtGui
            // 
            this.dtGui.CustomFormat = "dd/MM/yyyy";
            this.dtGui.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtGui.Location = new System.Drawing.Point(85, 54);
            this.dtGui.Name = "dtGui";
            this.dtGui.Size = new System.Drawing.Size(140, 27);
            this.dtGui.TabIndex = 7;
            // 
            // lblNgayGui
            // 
            this.lblNgayGui.AutoSize = true;
            this.lblNgayGui.Location = new System.Drawing.Point(10, 57);
            this.lblNgayGui.Name = "lblNgayGui";
            this.lblNgayGui.Size = new System.Drawing.Size(72, 20);
            this.lblNgayGui.TabIndex = 6;
            this.lblNgayGui.Text = "Ngày gửi:";
            // 
            // txtMaKS
            // 
            this.txtMaKS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaKS.Location = new System.Drawing.Point(790, 14);
            this.txtMaKS.Name = "txtMaKS";
            this.txtMaKS.Size = new System.Drawing.Size(150, 27);
            this.txtMaKS.TabIndex = 5;
            // 
            // lblMaKS
            // 
            this.lblMaKS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaKS.AutoSize = true;
            this.lblMaKS.Location = new System.Drawing.Point(730, 17);
            this.lblMaKS.Name = "lblMaKS";
            this.lblMaKS.Size = new System.Drawing.Size(55, 20);
            this.lblMaKS.TabIndex = 4;
            this.lblMaKS.Text = "Mã KS:";
            // 
            // cboDangKy
            // 
            this.cboDangKy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDangKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDangKy.FormattingEnabled = true;
            this.cboDangKy.Location = new System.Drawing.Point(375, 14);
            this.cboDangKy.Name = "cboDangKy";
            this.cboDangKy.Size = new System.Drawing.Size(340, 28);
            this.cboDangKy.TabIndex = 3;
            // 
            // lblDangKy
            // 
            this.lblDangKy.AutoSize = true;
            this.lblDangKy.Location = new System.Drawing.Point(225, 17);
            this.lblDangKy.Name = "lblDangKy";
            this.lblDangKy.Size = new System.Drawing.Size(144, 20);
            this.lblDangKy.TabIndex = 2;
            this.lblDangKy.Text = "ĐK đã kết thúc tour:";
            // 
            // cboLoaiKS
            // 
            this.cboLoaiKS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiKS.FormattingEnabled = true;
            this.cboLoaiKS.Location = new System.Drawing.Point(95, 14);
            this.cboLoaiKS.Name = "cboLoaiKS";
            this.cboLoaiKS.Size = new System.Drawing.Size(115, 28);
            this.cboLoaiKS.TabIndex = 1;
            this.cboLoaiKS.SelectedIndexChanged += new System.EventHandler(this.cboLoaiKS_SelectedIndexChanged);
            // 
            // lblLoaiKS
            // 
            this.lblLoaiKS.AutoSize = true;
            this.lblLoaiKS.Location = new System.Drawing.Point(10, 17);
            this.lblLoaiKS.Name = "lblLoaiKS";
            this.lblLoaiKS.Size = new System.Drawing.Size(81, 20);
            this.lblLoaiKS.TabIndex = 0;
            this.lblLoaiKS.Text = "Loại khách:";
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(852, 545);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 35);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmKetThucKhaoSat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 610);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.tabKT);
            this.Name = "FrmKetThucKhaoSat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kết thúc tour - thanh toán đoàn - khảo sát";
            this.Load += new System.EventHandler(this.FrmKetThucKhaoSat_Load);
            this.tabKT.ResumeLayout(false);
            this.tabThanhToanPage.ResumeLayout(false);
            this.tabThanhToanPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoan)).EndInit();
            this.tabKhaoSatPage.ResumeLayout(false);
            this.tabKhaoSatPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabKT;
        private System.Windows.Forms.TabPage tabThanhToanPage;
        private System.Windows.Forms.TabPage tabKhaoSatPage;
        private System.Windows.Forms.DataGridView dgvDoan;
        private System.Windows.Forms.Label lblSoTT;
        private System.Windows.Forms.TextBox txtSoTT;
        private System.Windows.Forms.Label lblSoDK;
        private System.Windows.Forms.TextBox txtSoDK;
        private System.Windows.Forms.Label lblNgayTT;
        private System.Windows.Forms.DateTimePicker dtTT;
        private System.Windows.Forms.Label lblSoTien;
        private System.Windows.Forms.NumericUpDown numTien;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Label lblLoaiKS;
        private System.Windows.Forms.ComboBox cboLoaiKS;
        private System.Windows.Forms.Label lblDangKy;
        private System.Windows.Forms.ComboBox cboDangKy;
        private System.Windows.Forms.Label lblMaKS;
        private System.Windows.Forms.TextBox txtMaKS;
        private System.Windows.Forms.Label lblNgayGui;
        private System.Windows.Forms.DateTimePicker dtGui;
        private System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.DataGridView dgvKS;
        private System.Windows.Forms.Label lblKSChon;
        private System.Windows.Forms.TextBox txtKSChon;
        private System.Windows.Forms.Label lblNgayPH;
        private System.Windows.Forms.DateTimePicker dtPH;
        private System.Windows.Forms.Label lblDiem;
        private System.Windows.Forms.NumericUpDown numDiem;
        private System.Windows.Forms.Label lblGopY;
        private System.Windows.Forms.TextBox txtGopY;
        private System.Windows.Forms.Button btnGhiPH;
        private System.Windows.Forms.Button btnDong;
    }
}
