namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmTour
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
            this.lblTourChon = new System.Windows.Forms.Label();
            this.cboTour = new System.Windows.Forms.ComboBox();
            this.tabTour = new System.Windows.Forms.TabControl();
            this.tabTourPage = new System.Windows.Forms.TabPage();
            this.btnThemTour = new System.Windows.Forms.Button();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.numGia = new System.Windows.Forms.NumericUpDown();
            this.lblGia = new System.Windows.Forms.Label();
            this.numDem = new System.Windows.Forms.NumericUpDown();
            this.lblDem = new System.Windows.Forms.Label();
            this.numNgay = new System.Windows.Forms.NumericUpDown();
            this.lblNgay = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.lblMa = new System.Windows.Forms.Label();
            this.dgvTour = new System.Windows.Forms.DataGridView();
            this.tabDiemDungPage = new System.Windows.Forms.TabPage();
            this.btnThemDD = new System.Windows.Forms.Button();
            this.txtGhiChuDD = new System.Windows.Forms.TextBox();
            this.lblGhiChuDD = new System.Windows.Forms.Label();
            this.numSao = new System.Windows.Forms.NumericUpDown();
            this.lblSao = new System.Windows.Forms.Label();
            this.chkKS = new System.Windows.Forms.CheckBox();
            this.chkAn = new System.Windows.Forms.CheckBox();
            this.chkDoiPT = new System.Windows.Forms.CheckBox();
            this.txtDiemDung = new System.Windows.Forms.TextBox();
            this.lblDiemDung = new System.Windows.Forms.Label();
            this.numThuTu = new System.Windows.Forms.NumericUpDown();
            this.lblThuTu = new System.Windows.Forms.Label();
            this.dgvDiemDung = new System.Windows.Forms.DataGridView();
            this.tabChangPage = new System.Windows.Forms.TabPage();
            this.lblChangGhiChu = new System.Windows.Forms.Label();
            this.btnThemChang = new System.Windows.Forms.Button();
            this.txtGhiChuPT = new System.Windows.Forms.TextBox();
            this.lblGhiChuPT = new System.Windows.Forms.Label();
            this.cboPT = new System.Windows.Forms.ComboBox();
            this.lblPT = new System.Windows.Forms.Label();
            this.numChang = new System.Windows.Forms.NumericUpDown();
            this.lblChang = new System.Windows.Forms.Label();
            this.dgvChang = new System.Windows.Forms.DataGridView();
            this.tabTQPage = new System.Windows.Forms.TabPage();
            this.btnThemTQ = new System.Windows.Forms.Button();
            this.numThuTuTQ = new System.Windows.Forms.NumericUpDown();
            this.lblThuTuTQ = new System.Windows.Forms.Label();
            this.cboDTQ = new System.Windows.Forms.ComboBox();
            this.lblDTQ = new System.Windows.Forms.Label();
            this.dgvTQ = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            this.tabTour.SuspendLayout();
            this.tabTourPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTour)).BeginInit();
            this.tabDiemDungPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThuTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiemDung)).BeginInit();
            this.tabChangPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChang)).BeginInit();
            this.tabTQPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThuTuTQ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTQ)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTourChon
            // 
            this.lblTourChon.AutoSize = true;
            this.lblTourChon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTourChon.Location = new System.Drawing.Point(12, 16);
            this.lblTourChon.Name = "lblTourChon";
            this.lblTourChon.Size = new System.Drawing.Size(262, 20);
            this.lblTourChon.TabIndex = 0;
            this.lblTourChon.Text = "Tour đang chọn (cho các tab hành trình):";
            // 
            // cboTour
            // 
            this.cboTour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTour.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTour.FormattingEnabled = true;
            this.cboTour.Location = new System.Drawing.Point(360, 12);
            this.cboTour.Name = "cboTour";
            this.cboTour.Size = new System.Drawing.Size(420, 28);
            this.cboTour.TabIndex = 1;
            this.cboTour.SelectedIndexChanged += new System.EventHandler(this.cboTour_SelectedIndexChanged);
            // 
            // tabTour
            // 
            this.tabTour.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabTour.Controls.Add(this.tabTourPage);
            this.tabTour.Controls.Add(this.tabDiemDungPage);
            this.tabTour.Controls.Add(this.tabChangPage);
            this.tabTour.Controls.Add(this.tabTQPage);
            this.tabTour.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabTour.Location = new System.Drawing.Point(12, 50);
            this.tabTour.Name = "tabTour";
            this.tabTour.SelectedIndex = 0;
            this.tabTour.Size = new System.Drawing.Size(960, 520);
            this.tabTour.TabIndex = 2;
            // 
            // tabTourPage
            // 
            this.tabTourPage.Controls.Add(this.btnThemTour);
            this.tabTourPage.Controls.Add(this.txtMoTa);
            this.tabTourPage.Controls.Add(this.lblMoTa);
            this.tabTourPage.Controls.Add(this.numGia);
            this.tabTourPage.Controls.Add(this.lblGia);
            this.tabTourPage.Controls.Add(this.numDem);
            this.tabTourPage.Controls.Add(this.lblDem);
            this.tabTourPage.Controls.Add(this.numNgay);
            this.tabTourPage.Controls.Add(this.lblNgay);
            this.tabTourPage.Controls.Add(this.txtTen);
            this.tabTourPage.Controls.Add(this.lblTen);
            this.tabTourPage.Controls.Add(this.txtMa);
            this.tabTourPage.Controls.Add(this.lblMa);
            this.tabTourPage.Controls.Add(this.dgvTour);
            this.tabTourPage.Location = new System.Drawing.Point(4, 29);
            this.tabTourPage.Name = "tabTourPage";
            this.tabTourPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabTourPage.Size = new System.Drawing.Size(952, 487);
            this.tabTourPage.TabIndex = 0;
            this.tabTourPage.Text = "Tour";
            this.tabTourPage.UseVisualStyleBackColor = true;
            // 
            // btnThemTour
            // 
            this.btnThemTour.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemTour.Location = new System.Drawing.Point(820, 435);
            this.btnThemTour.Name = "btnThemTour";
            this.btnThemTour.Size = new System.Drawing.Size(120, 35);
            this.btnThemTour.TabIndex = 13;
            this.btnThemTour.Text = "Thêm tour";
            this.btnThemTour.UseVisualStyleBackColor = true;
            this.btnThemTour.Click += new System.EventHandler(this.btnThemTour_Click);
            // 
            // txtMoTa
            // 
            this.txtMoTa.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMoTa.Location = new System.Drawing.Point(110, 439);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(690, 27);
            this.txtMoTa.TabIndex = 12;
            // 
            // lblMoTa
            // 
            this.lblMoTa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(15, 442);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(51, 20);
            this.lblMoTa.TabIndex = 11;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // numGia
            // 
            this.numGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numGia.DecimalPlaces = 0;
            this.numGia.Increment = new decimal(new int[] { 100000, 0, 0, 0 });
            this.numGia.Location = new System.Drawing.Point(670, 396);
            this.numGia.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numGia.Name = "numGia";
            this.numGia.Size = new System.Drawing.Size(130, 27);
            this.numGia.TabIndex = 10;
            this.numGia.ThousandsSeparator = true;
            // 
            // lblGia
            // 
            this.lblGia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGia.AutoSize = true;
            this.lblGia.Location = new System.Drawing.Point(555, 399);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(107, 20);
            this.lblGia.TabIndex = 9;
            this.lblGia.Text = "Đơn giá/khách:";
            // 
            // numDem
            // 
            this.numDem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numDem.Location = new System.Drawing.Point(450, 396);
            this.numDem.Name = "numDem";
            this.numDem.Size = new System.Drawing.Size(80, 27);
            this.numDem.TabIndex = 8;
            // 
            // lblDem
            // 
            this.lblDem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDem.AutoSize = true;
            this.lblDem.Location = new System.Drawing.Point(380, 399);
            this.lblDem.Name = "lblDem";
            this.lblDem.Size = new System.Drawing.Size(63, 20);
            this.lblDem.TabIndex = 7;
            this.lblDem.Text = "Số đêm:";
            // 
            // numNgay
            // 
            this.numNgay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numNgay.Location = new System.Drawing.Point(280, 396);
            this.numNgay.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numNgay.Name = "numNgay";
            this.numNgay.Size = new System.Drawing.Size(80, 27);
            this.numNgay.TabIndex = 6;
            this.numNgay.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblNgay
            // 
            this.lblNgay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(210, 399);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(66, 20);
            this.lblNgay.TabIndex = 5;
            this.lblNgay.Text = "Số ngày:";
            // 
            // txtTen
            // 
            this.txtTen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTen.Location = new System.Drawing.Point(380, 356);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(420, 27);
            this.txtTen.TabIndex = 4;
            // 
            // lblTen
            // 
            this.lblTen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(305, 359);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(68, 20);
            this.lblTen.TabIndex = 3;
            this.lblTen.Text = "Tên tour:";
            // 
            // txtMa
            // 
            this.txtMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtMa.Location = new System.Drawing.Point(110, 356);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(170, 27);
            this.txtMa.TabIndex = 2;
            // 
            // lblMa
            // 
            this.lblMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblMa.AutoSize = true;
            this.lblMa.Location = new System.Drawing.Point(15, 359);
            this.lblMa.Name = "lblMa";
            this.lblMa.Size = new System.Drawing.Size(66, 20);
            this.lblMa.TabIndex = 1;
            this.lblMa.Text = "Mã tour:";
            // 
            // dgvTour
            // 
            this.dgvTour.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTour.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTour.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTour.Location = new System.Drawing.Point(6, 6);
            this.dgvTour.Name = "dgvTour";
            this.dgvTour.RowHeadersWidth = 51;
            this.dgvTour.RowTemplate.Height = 24;
            this.dgvTour.Size = new System.Drawing.Size(940, 335);
            this.dgvTour.TabIndex = 0;
            // 
            // tabDiemDungPage
            // 
            this.tabDiemDungPage.Controls.Add(this.btnThemDD);
            this.tabDiemDungPage.Controls.Add(this.txtGhiChuDD);
            this.tabDiemDungPage.Controls.Add(this.lblGhiChuDD);
            this.tabDiemDungPage.Controls.Add(this.numSao);
            this.tabDiemDungPage.Controls.Add(this.lblSao);
            this.tabDiemDungPage.Controls.Add(this.chkKS);
            this.tabDiemDungPage.Controls.Add(this.chkAn);
            this.tabDiemDungPage.Controls.Add(this.chkDoiPT);
            this.tabDiemDungPage.Controls.Add(this.txtDiemDung);
            this.tabDiemDungPage.Controls.Add(this.lblDiemDung);
            this.tabDiemDungPage.Controls.Add(this.numThuTu);
            this.tabDiemDungPage.Controls.Add(this.lblThuTu);
            this.tabDiemDungPage.Controls.Add(this.dgvDiemDung);
            this.tabDiemDungPage.Location = new System.Drawing.Point(4, 29);
            this.tabDiemDungPage.Name = "tabDiemDungPage";
            this.tabDiemDungPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabDiemDungPage.Size = new System.Drawing.Size(952, 487);
            this.tabDiemDungPage.TabIndex = 1;
            this.tabDiemDungPage.Text = "Điểm dừng";
            this.tabDiemDungPage.UseVisualStyleBackColor = true;
            // 
            // btnThemDD
            // 
            this.btnThemDD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemDD.Location = new System.Drawing.Point(820, 435);
            this.btnThemDD.Name = "btnThemDD";
            this.btnThemDD.Size = new System.Drawing.Size(120, 35);
            this.btnThemDD.TabIndex = 12;
            this.btnThemDD.Text = "Thêm điểm dừng";
            this.btnThemDD.UseVisualStyleBackColor = true;
            this.btnThemDD.Click += new System.EventHandler(this.btnThemDD_Click);
            // 
            // txtGhiChuDD
            // 
            this.txtGhiChuDD.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChuDD.Location = new System.Drawing.Point(100, 439);
            this.txtGhiChuDD.Name = "txtGhiChuDD";
            this.txtGhiChuDD.Size = new System.Drawing.Size(700, 27);
            this.txtGhiChuDD.TabIndex = 11;
            // 
            // lblGhiChuDD
            // 
            this.lblGhiChuDD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGhiChuDD.AutoSize = true;
            this.lblGhiChuDD.Location = new System.Drawing.Point(15, 442);
            this.lblGhiChuDD.Name = "lblGhiChuDD";
            this.lblGhiChuDD.Size = new System.Drawing.Size(61, 20);
            this.lblGhiChuDD.TabIndex = 10;
            this.lblGhiChuDD.Text = "Ghi chú:";
            // 
            // numSao
            // 
            this.numSao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numSao.Enabled = false;
            this.numSao.Location = new System.Drawing.Point(540, 396);
            this.numSao.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            this.numSao.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            this.numSao.Name = "numSao";
            this.numSao.Size = new System.Drawing.Size(60, 27);
            this.numSao.TabIndex = 9;
            this.numSao.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // lblSao
            // 
            this.lblSao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSao.AutoSize = true;
            this.lblSao.Location = new System.Drawing.Point(460, 399);
            this.lblSao.Name = "lblSao";
            this.lblSao.Size = new System.Drawing.Size(73, 20);
            this.lblSao.TabIndex = 8;
            this.lblSao.Text = "Hạng sao:";
            // 
            // chkKS
            // 
            this.chkKS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkKS.AutoSize = true;
            this.chkKS.Location = new System.Drawing.Point(320, 398);
            this.chkKS.Name = "chkKS";
            this.chkKS.Size = new System.Drawing.Size(117, 24);
            this.chkKS.TabIndex = 7;
            this.chkKS.Text = "Có khách sạn";
            this.chkKS.UseVisualStyleBackColor = true;
            this.chkKS.CheckedChanged += new System.EventHandler(this.chkKS_CheckedChanged);
            // 
            // chkAn
            // 
            this.chkAn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAn.AutoSize = true;
            this.chkAn.Location = new System.Drawing.Point(190, 398);
            this.chkAn.Name = "chkAn";
            this.chkAn.Size = new System.Drawing.Size(98, 24);
            this.chkAn.TabIndex = 6;
            this.chkAn.Text = "Có nơi ăn";
            this.chkAn.UseVisualStyleBackColor = true;
            // 
            // chkDoiPT
            // 
            this.chkDoiPT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDoiPT.AutoSize = true;
            this.chkDoiPT.Location = new System.Drawing.Point(20, 398);
            this.chkDoiPT.Name = "chkDoiPT";
            this.chkDoiPT.Size = new System.Drawing.Size(147, 24);
            this.chkDoiPT.TabIndex = 5;
            this.chkDoiPT.Text = "Đổi phương tiện";
            this.chkDoiPT.UseVisualStyleBackColor = true;
            // 
            // txtDiemDung
            // 
            this.txtDiemDung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiemDung.Location = new System.Drawing.Point(320, 356);
            this.txtDiemDung.Name = "txtDiemDung";
            this.txtDiemDung.Size = new System.Drawing.Size(480, 27);
            this.txtDiemDung.TabIndex = 4;
            // 
            // lblDiemDung
            // 
            this.lblDiemDung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDiemDung.AutoSize = true;
            this.lblDiemDung.Location = new System.Drawing.Point(205, 359);
            this.lblDiemDung.Name = "lblDiemDung";
            this.lblDiemDung.Size = new System.Drawing.Size(109, 20);
            this.lblDiemDung.TabIndex = 3;
            this.lblDiemDung.Text = "Tên điểm dừng:";
            // 
            // numThuTu
            // 
            this.numThuTu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numThuTu.Location = new System.Drawing.Point(100, 356);
            this.numThuTu.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numThuTu.Name = "numThuTu";
            this.numThuTu.Size = new System.Drawing.Size(80, 27);
            this.numThuTu.TabIndex = 2;
            this.numThuTu.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblThuTu
            // 
            this.lblThuTu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblThuTu.AutoSize = true;
            this.lblThuTu.Location = new System.Drawing.Point(15, 359);
            this.lblThuTu.Name = "lblThuTu";
            this.lblThuTu.Size = new System.Drawing.Size(54, 20);
            this.lblThuTu.TabIndex = 1;
            this.lblThuTu.Text = "Thứ tự:";
            // 
            // dgvDiemDung
            // 
            this.dgvDiemDung.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDiemDung.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDiemDung.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiemDung.Location = new System.Drawing.Point(6, 6);
            this.dgvDiemDung.Name = "dgvDiemDung";
            this.dgvDiemDung.RowHeadersWidth = 51;
            this.dgvDiemDung.RowTemplate.Height = 24;
            this.dgvDiemDung.Size = new System.Drawing.Size(940, 335);
            this.dgvDiemDung.TabIndex = 0;
            // 
            // tabChangPage
            // 
            this.tabChangPage.Controls.Add(this.lblChangGhiChu);
            this.tabChangPage.Controls.Add(this.btnThemChang);
            this.tabChangPage.Controls.Add(this.txtGhiChuPT);
            this.tabChangPage.Controls.Add(this.lblGhiChuPT);
            this.tabChangPage.Controls.Add(this.cboPT);
            this.tabChangPage.Controls.Add(this.lblPT);
            this.tabChangPage.Controls.Add(this.numChang);
            this.tabChangPage.Controls.Add(this.lblChang);
            this.tabChangPage.Controls.Add(this.dgvChang);
            this.tabChangPage.Location = new System.Drawing.Point(4, 29);
            this.tabChangPage.Name = "tabChangPage";
            this.tabChangPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabChangPage.Size = new System.Drawing.Size(952, 487);
            this.tabChangPage.TabIndex = 2;
            this.tabChangPage.Text = "Phương tiện theo chặng";
            this.tabChangPage.UseVisualStyleBackColor = true;
            // 
            // lblChangGhiChu
            // 
            this.lblChangGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblChangGhiChu.AutoSize = true;
            this.lblChangGhiChu.ForeColor = System.Drawing.Color.DimGray;
            this.lblChangGhiChu.Location = new System.Drawing.Point(15, 445);
            this.lblChangGhiChu.Name = "lblChangGhiChu";
            this.lblChangGhiChu.Size = new System.Drawing.Size(534, 20);
            this.lblChangGhiChu.TabIndex = 8;
            this.lblChangGhiChu.Text = "Chặng k là đoạn đi tới điểm dừng thứ k; một chặng có thể dùng nhiều phương tiện.";
            // 
            // btnThemChang
            // 
            this.btnThemChang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemChang.Location = new System.Drawing.Point(800, 396);
            this.btnThemChang.Name = "btnThemChang";
            this.btnThemChang.Size = new System.Drawing.Size(140, 35);
            this.btnThemChang.TabIndex = 7;
            this.btnThemChang.Text = "Gắn phương tiện";
            this.btnThemChang.UseVisualStyleBackColor = true;
            this.btnThemChang.Click += new System.EventHandler(this.btnThemChang_Click);
            // 
            // txtGhiChuPT
            // 
            this.txtGhiChuPT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGhiChuPT.Location = new System.Drawing.Point(100, 400);
            this.txtGhiChuPT.Name = "txtGhiChuPT";
            this.txtGhiChuPT.Size = new System.Drawing.Size(680, 27);
            this.txtGhiChuPT.TabIndex = 6;
            // 
            // lblGhiChuPT
            // 
            this.lblGhiChuPT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblGhiChuPT.AutoSize = true;
            this.lblGhiChuPT.Location = new System.Drawing.Point(15, 403);
            this.lblGhiChuPT.Name = "lblGhiChuPT";
            this.lblGhiChuPT.Size = new System.Drawing.Size(61, 20);
            this.lblGhiChuPT.TabIndex = 5;
            this.lblGhiChuPT.Text = "Ghi chú:";
            // 
            // cboPT
            // 
            this.cboPT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboPT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPT.FormattingEnabled = true;
            this.cboPT.Location = new System.Drawing.Point(320, 356);
            this.cboPT.Name = "cboPT";
            this.cboPT.Size = new System.Drawing.Size(260, 28);
            this.cboPT.TabIndex = 4;
            // 
            // lblPT
            // 
            this.lblPT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPT.AutoSize = true;
            this.lblPT.Location = new System.Drawing.Point(220, 359);
            this.lblPT.Name = "lblPT";
            this.lblPT.Size = new System.Drawing.Size(92, 20);
            this.lblPT.TabIndex = 3;
            this.lblPT.Text = "Phương tiện:";
            // 
            // numChang
            // 
            this.numChang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numChang.Location = new System.Drawing.Point(100, 356);
            this.numChang.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numChang.Name = "numChang";
            this.numChang.Size = new System.Drawing.Size(80, 27);
            this.numChang.TabIndex = 2;
            this.numChang.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblChang
            // 
            this.lblChang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblChang.AutoSize = true;
            this.lblChang.Location = new System.Drawing.Point(15, 359);
            this.lblChang.Name = "lblChang";
            this.lblChang.Size = new System.Drawing.Size(73, 20);
            this.lblChang.TabIndex = 1;
            this.lblChang.Text = "Chặng thứ:";
            // 
            // dgvChang
            // 
            this.dgvChang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChang.Location = new System.Drawing.Point(6, 6);
            this.dgvChang.Name = "dgvChang";
            this.dgvChang.RowHeadersWidth = 51;
            this.dgvChang.RowTemplate.Height = 24;
            this.dgvChang.Size = new System.Drawing.Size(940, 335);
            this.dgvChang.TabIndex = 0;
            // 
            // tabTQPage
            // 
            this.tabTQPage.Controls.Add(this.btnThemTQ);
            this.tabTQPage.Controls.Add(this.numThuTuTQ);
            this.tabTQPage.Controls.Add(this.lblThuTuTQ);
            this.tabTQPage.Controls.Add(this.cboDTQ);
            this.tabTQPage.Controls.Add(this.lblDTQ);
            this.tabTQPage.Controls.Add(this.dgvTQ);
            this.tabTQPage.Location = new System.Drawing.Point(4, 29);
            this.tabTQPage.Name = "tabTQPage";
            this.tabTQPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabTQPage.Size = new System.Drawing.Size(952, 487);
            this.tabTQPage.TabIndex = 3;
            this.tabTQPage.Text = "Điểm tham quan";
            this.tabTQPage.UseVisualStyleBackColor = true;
            // 
            // btnThemTQ
            // 
            this.btnThemTQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemTQ.Location = new System.Drawing.Point(820, 390);
            this.btnThemTQ.Name = "btnThemTQ";
            this.btnThemTQ.Size = new System.Drawing.Size(120, 35);
            this.btnThemTQ.TabIndex = 5;
            this.btnThemTQ.Text = "Gắn điểm TQ";
            this.btnThemTQ.UseVisualStyleBackColor = true;
            this.btnThemTQ.Click += new System.EventHandler(this.btnThemTQ_Click);
            // 
            // numThuTuTQ
            // 
            this.numThuTuTQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numThuTuTQ.Location = new System.Drawing.Point(540, 394);
            this.numThuTuTQ.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numThuTuTQ.Name = "numThuTuTQ";
            this.numThuTuTQ.Size = new System.Drawing.Size(80, 27);
            this.numThuTuTQ.TabIndex = 4;
            this.numThuTuTQ.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblThuTuTQ
            // 
            this.lblThuTuTQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblThuTuTQ.AutoSize = true;
            this.lblThuTuTQ.Location = new System.Drawing.Point(475, 397);
            this.lblThuTuTQ.Name = "lblThuTuTQ";
            this.lblThuTuTQ.Size = new System.Drawing.Size(54, 20);
            this.lblThuTuTQ.TabIndex = 3;
            this.lblThuTuTQ.Text = "Thứ tự:";
            // 
            // cboDTQ
            // 
            this.cboDTQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cboDTQ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDTQ.FormattingEnabled = true;
            this.cboDTQ.Location = new System.Drawing.Point(145, 394);
            this.cboDTQ.Name = "cboDTQ";
            this.cboDTQ.Size = new System.Drawing.Size(300, 28);
            this.cboDTQ.TabIndex = 2;
            // 
            // lblDTQ
            // 
            this.lblDTQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDTQ.AutoSize = true;
            this.lblDTQ.Location = new System.Drawing.Point(15, 397);
            this.lblDTQ.Name = "lblDTQ";
            this.lblDTQ.Size = new System.Drawing.Size(117, 20);
            this.lblDTQ.TabIndex = 1;
            this.lblDTQ.Text = "Điểm tham quan:";
            // 
            // dgvTQ
            // 
            this.dgvTQ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTQ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTQ.Location = new System.Drawing.Point(6, 6);
            this.dgvTQ.Name = "dgvTQ";
            this.dgvTQ.RowHeadersWidth = 51;
            this.dgvTQ.RowTemplate.Height = 24;
            this.dgvTQ.Size = new System.Drawing.Size(940, 360);
            this.dgvTQ.TabIndex = 0;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(852, 580);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 35);
            this.btnDong.TabIndex = 3;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmTour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 635);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.tabTour);
            this.Controls.Add(this.cboTour);
            this.Controls.Add(this.lblTourChon);
            this.Name = "FrmTour";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tour - hành trình";
            this.Load += new System.EventHandler(this.FrmTour_Load);
            this.tabTour.ResumeLayout(false);
            this.tabTourPage.ResumeLayout(false);
            this.tabTourPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTour)).EndInit();
            this.tabDiemDungPage.ResumeLayout(false);
            this.tabDiemDungPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numThuTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiemDung)).EndInit();
            this.tabChangPage.ResumeLayout(false);
            this.tabChangPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numChang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChang)).EndInit();
            this.tabTQPage.ResumeLayout(false);
            this.tabTQPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThuTuTQ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTQ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTourChon;
        private System.Windows.Forms.ComboBox cboTour;
        private System.Windows.Forms.TabControl tabTour;
        private System.Windows.Forms.TabPage tabTourPage;
        private System.Windows.Forms.TabPage tabDiemDungPage;
        private System.Windows.Forms.TabPage tabChangPage;
        private System.Windows.Forms.TabPage tabTQPage;
        private System.Windows.Forms.DataGridView dgvTour;
        private System.Windows.Forms.Label lblMa;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.NumericUpDown numNgay;
        private System.Windows.Forms.Label lblDem;
        private System.Windows.Forms.NumericUpDown numDem;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.NumericUpDown numGia;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Button btnThemTour;
        private System.Windows.Forms.DataGridView dgvDiemDung;
        private System.Windows.Forms.Label lblThuTu;
        private System.Windows.Forms.NumericUpDown numThuTu;
        private System.Windows.Forms.Label lblDiemDung;
        private System.Windows.Forms.TextBox txtDiemDung;
        private System.Windows.Forms.CheckBox chkDoiPT;
        private System.Windows.Forms.CheckBox chkAn;
        private System.Windows.Forms.CheckBox chkKS;
        private System.Windows.Forms.Label lblSao;
        private System.Windows.Forms.NumericUpDown numSao;
        private System.Windows.Forms.Label lblGhiChuDD;
        private System.Windows.Forms.TextBox txtGhiChuDD;
        private System.Windows.Forms.Button btnThemDD;
        private System.Windows.Forms.DataGridView dgvChang;
        private System.Windows.Forms.Label lblChang;
        private System.Windows.Forms.NumericUpDown numChang;
        private System.Windows.Forms.Label lblPT;
        private System.Windows.Forms.ComboBox cboPT;
        private System.Windows.Forms.Label lblGhiChuPT;
        private System.Windows.Forms.TextBox txtGhiChuPT;
        private System.Windows.Forms.Button btnThemChang;
        private System.Windows.Forms.Label lblChangGhiChu;
        private System.Windows.Forms.DataGridView dgvTQ;
        private System.Windows.Forms.Label lblDTQ;
        private System.Windows.Forms.ComboBox cboDTQ;
        private System.Windows.Forms.Label lblThuTuTQ;
        private System.Windows.Forms.NumericUpDown numThuTuTQ;
        private System.Windows.Forms.Button btnThemTQ;
        private System.Windows.Forms.Button btnDong;
    }
}
