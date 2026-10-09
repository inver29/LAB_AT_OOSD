namespace QuanLyCongTyDuLich.Forms
{
    partial class FrmDanhMuc
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
            this.tabDanhMuc = new System.Windows.Forms.TabControl();
            this.tabPhuongTien = new System.Windows.Forms.TabPage();
            this.btnThemPT = new System.Windows.Forms.Button();
            this.txtPTGhiChu = new System.Windows.Forms.TextBox();
            this.lblPTGhiChu = new System.Windows.Forms.Label();
            this.txtPTTen = new System.Windows.Forms.TextBox();
            this.lblPTTen = new System.Windows.Forms.Label();
            this.txtPTMa = new System.Windows.Forms.TextBox();
            this.lblPTMa = new System.Windows.Forms.Label();
            this.dgvPT = new System.Windows.Forms.DataGridView();
            this.tabDiemBan = new System.Windows.Forms.TabPage();
            this.btnThemDB = new System.Windows.Forms.Button();
            this.txtDBDT = new System.Windows.Forms.TextBox();
            this.lblDBDT = new System.Windows.Forms.Label();
            this.txtDBDiaChi = new System.Windows.Forms.TextBox();
            this.lblDBDiaChi = new System.Windows.Forms.Label();
            this.txtDBTen = new System.Windows.Forms.TextBox();
            this.lblDBTen = new System.Windows.Forms.Label();
            this.txtDBMa = new System.Windows.Forms.TextBox();
            this.lblDBMa = new System.Windows.Forms.Label();
            this.dgvDB = new System.Windows.Forms.DataGridView();
            this.tabHDV = new System.Windows.Forms.TabPage();
            this.numLuong = new System.Windows.Forms.NumericUpDown();
            this.lblLuong = new System.Windows.Forms.Label();
            this.btnThemHDV = new System.Windows.Forms.Button();
            this.txtHDVDT = new System.Windows.Forms.TextBox();
            this.lblHDVDT = new System.Windows.Forms.Label();
            this.txtHDVTen = new System.Windows.Forms.TextBox();
            this.lblHDVTen = new System.Windows.Forms.Label();
            this.txtHDVMa = new System.Windows.Forms.TextBox();
            this.lblHDVMa = new System.Windows.Forms.Label();
            this.dgvHDV = new System.Windows.Forms.DataGridView();
            this.tabDTQ = new System.Windows.Forms.TabPage();
            this.txtDTQYNghia = new System.Windows.Forms.TextBox();
            this.lblDTQYNghia = new System.Windows.Forms.Label();
            this.txtDTQNoiDung = new System.Windows.Forms.TextBox();
            this.lblDTQNoiDung = new System.Windows.Forms.Label();
            this.btnThemDTQ = new System.Windows.Forms.Button();
            this.txtDTQDiaDiem = new System.Windows.Forms.TextBox();
            this.lblDTQDiaDiem = new System.Windows.Forms.Label();
            this.txtDTQTen = new System.Windows.Forms.TextBox();
            this.lblDTQTen = new System.Windows.Forms.Label();
            this.txtDTQMa = new System.Windows.Forms.TextBox();
            this.lblDTQMa = new System.Windows.Forms.Label();
            this.dgvDTQ = new System.Windows.Forms.DataGridView();
            this.btnDong = new System.Windows.Forms.Button();
            this.tabDanhMuc.SuspendLayout();
            this.tabPhuongTien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPT)).BeginInit();
            this.tabDiemBan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDB)).BeginInit();
            this.tabHDV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHDV)).BeginInit();
            this.tabDTQ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDTQ)).BeginInit();
            this.SuspendLayout();
            // 
            // tabDanhMuc
            // 
            this.tabDanhMuc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabDanhMuc.Controls.Add(this.tabPhuongTien);
            this.tabDanhMuc.Controls.Add(this.tabDiemBan);
            this.tabDanhMuc.Controls.Add(this.tabHDV);
            this.tabDanhMuc.Controls.Add(this.tabDTQ);
            this.tabDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabDanhMuc.Location = new System.Drawing.Point(12, 12);
            this.tabDanhMuc.Name = "tabDanhMuc";
            this.tabDanhMuc.SelectedIndex = 0;
            this.tabDanhMuc.Size = new System.Drawing.Size(910, 520);
            this.tabDanhMuc.TabIndex = 0;
            // 
            // tabPhuongTien
            // 
            this.tabPhuongTien.Controls.Add(this.btnThemPT);
            this.tabPhuongTien.Controls.Add(this.txtPTGhiChu);
            this.tabPhuongTien.Controls.Add(this.lblPTGhiChu);
            this.tabPhuongTien.Controls.Add(this.txtPTTen);
            this.tabPhuongTien.Controls.Add(this.lblPTTen);
            this.tabPhuongTien.Controls.Add(this.txtPTMa);
            this.tabPhuongTien.Controls.Add(this.lblPTMa);
            this.tabPhuongTien.Controls.Add(this.dgvPT);
            this.tabPhuongTien.Location = new System.Drawing.Point(4, 29);
            this.tabPhuongTien.Name = "tabPhuongTien";
            this.tabPhuongTien.Padding = new System.Windows.Forms.Padding(3);
            this.tabPhuongTien.Size = new System.Drawing.Size(902, 487);
            this.tabPhuongTien.TabIndex = 0;
            this.tabPhuongTien.Text = "Phương tiện";
            this.tabPhuongTien.UseVisualStyleBackColor = true;
            // 
            // btnThemPT
            // 
            this.btnThemPT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemPT.Location = new System.Drawing.Point(740, 435);
            this.btnThemPT.Name = "btnThemPT";
            this.btnThemPT.Size = new System.Drawing.Size(120, 35);
            this.btnThemPT.TabIndex = 7;
            this.btnThemPT.Text = "Thêm";
            this.btnThemPT.UseVisualStyleBackColor = true;
            this.btnThemPT.Click += new System.EventHandler(this.btnThemPT_Click);
            // 
            // txtPTGhiChu
            // 
            this.txtPTGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPTGhiChu.Location = new System.Drawing.Point(100, 439);
            this.txtPTGhiChu.Name = "txtPTGhiChu";
            this.txtPTGhiChu.Size = new System.Drawing.Size(615, 27);
            this.txtPTGhiChu.TabIndex = 6;
            // 
            // lblPTGhiChu
            // 
            this.lblPTGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPTGhiChu.AutoSize = true;
            this.lblPTGhiChu.Location = new System.Drawing.Point(20, 442);
            this.lblPTGhiChu.Name = "lblPTGhiChu";
            this.lblPTGhiChu.Size = new System.Drawing.Size(61, 20);
            this.lblPTGhiChu.TabIndex = 5;
            this.lblPTGhiChu.Text = "Ghi chú:";
            // 
            // txtPTTen
            // 
            this.txtPTTen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPTTen.Location = new System.Drawing.Point(395, 396);
            this.txtPTTen.Name = "txtPTTen";
            this.txtPTTen.Size = new System.Drawing.Size(320, 27);
            this.txtPTTen.TabIndex = 4;
            // 
            // lblPTTen
            // 
            this.lblPTTen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPTTen.AutoSize = true;
            this.lblPTTen.Location = new System.Drawing.Point(325, 399);
            this.lblPTTen.Name = "lblPTTen";
            this.lblPTTen.Size = new System.Drawing.Size(54, 20);
            this.lblPTTen.TabIndex = 3;
            this.lblPTTen.Text = "Tên PT:";
            // 
            // txtPTMa
            // 
            this.txtPTMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtPTMa.Location = new System.Drawing.Point(100, 396);
            this.txtPTMa.Name = "txtPTMa";
            this.txtPTMa.Size = new System.Drawing.Size(180, 27);
            this.txtPTMa.TabIndex = 2;
            // 
            // lblPTMa
            // 
            this.lblPTMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPTMa.AutoSize = true;
            this.lblPTMa.Location = new System.Drawing.Point(20, 399);
            this.lblPTMa.Name = "lblPTMa";
            this.lblPTMa.Size = new System.Drawing.Size(52, 20);
            this.lblPTMa.TabIndex = 1;
            this.lblPTMa.Text = "Mã PT:";
            // 
            // dgvPT
            // 
            this.dgvPT.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPT.Location = new System.Drawing.Point(6, 6);
            this.dgvPT.Name = "dgvPT";
            this.dgvPT.RowHeadersWidth = 51;
            this.dgvPT.RowTemplate.Height = 24;
            this.dgvPT.Size = new System.Drawing.Size(890, 370);
            this.dgvPT.TabIndex = 0;
            // 
            // tabDiemBan
            // 
            this.tabDiemBan.Controls.Add(this.btnThemDB);
            this.tabDiemBan.Controls.Add(this.txtDBDT);
            this.tabDiemBan.Controls.Add(this.lblDBDT);
            this.tabDiemBan.Controls.Add(this.txtDBDiaChi);
            this.tabDiemBan.Controls.Add(this.lblDBDiaChi);
            this.tabDiemBan.Controls.Add(this.txtDBTen);
            this.tabDiemBan.Controls.Add(this.lblDBTen);
            this.tabDiemBan.Controls.Add(this.txtDBMa);
            this.tabDiemBan.Controls.Add(this.lblDBMa);
            this.tabDiemBan.Controls.Add(this.dgvDB);
            this.tabDiemBan.Location = new System.Drawing.Point(4, 29);
            this.tabDiemBan.Name = "tabDiemBan";
            this.tabDiemBan.Padding = new System.Windows.Forms.Padding(3);
            this.tabDiemBan.Size = new System.Drawing.Size(902, 487);
            this.tabDiemBan.TabIndex = 1;
            this.tabDiemBan.Text = "Điểm bán vé";
            this.tabDiemBan.UseVisualStyleBackColor = true;
            // 
            // btnThemDB
            // 
            this.btnThemDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemDB.Location = new System.Drawing.Point(740, 435);
            this.btnThemDB.Name = "btnThemDB";
            this.btnThemDB.Size = new System.Drawing.Size(120, 35);
            this.btnThemDB.TabIndex = 9;
            this.btnThemDB.Text = "Thêm";
            this.btnThemDB.UseVisualStyleBackColor = true;
            this.btnThemDB.Click += new System.EventHandler(this.btnThemDB_Click);
            // 
            // txtDBDT
            // 
            this.txtDBDT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDBDT.Location = new System.Drawing.Point(475, 439);
            this.txtDBDT.Name = "txtDBDT";
            this.txtDBDT.Size = new System.Drawing.Size(240, 27);
            this.txtDBDT.TabIndex = 8;
            // 
            // lblDBDT
            // 
            this.lblDBDT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDBDT.AutoSize = true;
            this.lblDBDT.Location = new System.Drawing.Point(385, 442);
            this.lblDBDT.Name = "lblDBDT";
            this.lblDBDT.Size = new System.Drawing.Size(81, 20);
            this.lblDBDT.TabIndex = 7;
            this.lblDBDT.Text = "Điện thoại:";
            // 
            // txtDBDiaChi
            // 
            this.txtDBDiaChi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDBDiaChi.Location = new System.Drawing.Point(120, 439);
            this.txtDBDiaChi.Name = "txtDBDiaChi";
            this.txtDBDiaChi.Size = new System.Drawing.Size(240, 27);
            this.txtDBDiaChi.TabIndex = 6;
            // 
            // lblDBDiaChi
            // 
            this.lblDBDiaChi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDBDiaChi.AutoSize = true;
            this.lblDBDiaChi.Location = new System.Drawing.Point(20, 442);
            this.lblDBDiaChi.Name = "lblDBDiaChi";
            this.lblDBDiaChi.Size = new System.Drawing.Size(58, 20);
            this.lblDBDiaChi.TabIndex = 5;
            this.lblDBDiaChi.Text = "Địa chỉ:";
            // 
            // txtDBTen
            // 
            this.txtDBTen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDBTen.Location = new System.Drawing.Point(475, 396);
            this.txtDBTen.Name = "txtDBTen";
            this.txtDBTen.Size = new System.Drawing.Size(240, 27);
            this.txtDBTen.TabIndex = 4;
            // 
            // lblDBTen
            // 
            this.lblDBTen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDBTen.AutoSize = true;
            this.lblDBTen.Location = new System.Drawing.Point(385, 399);
            this.lblDBTen.Name = "lblDBTen";
            this.lblDBTen.Size = new System.Drawing.Size(65, 20);
            this.lblDBTen.TabIndex = 3;
            this.lblDBTen.Text = "Tên điểm:";
            // 
            // txtDBMa
            // 
            this.txtDBMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDBMa.Location = new System.Drawing.Point(120, 396);
            this.txtDBMa.Name = "txtDBMa";
            this.txtDBMa.Size = new System.Drawing.Size(240, 27);
            this.txtDBMa.TabIndex = 2;
            // 
            // lblDBMa
            // 
            this.lblDBMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDBMa.AutoSize = true;
            this.lblDBMa.Location = new System.Drawing.Point(20, 399);
            this.lblDBMa.Name = "lblDBMa";
            this.lblDBMa.Size = new System.Drawing.Size(95, 20);
            this.lblDBMa.TabIndex = 1;
            this.lblDBMa.Text = "Mã điểm bán:";
            // 
            // dgvDB
            // 
            this.dgvDB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDB.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDB.Location = new System.Drawing.Point(6, 6);
            this.dgvDB.Name = "dgvDB";
            this.dgvDB.RowHeadersWidth = 51;
            this.dgvDB.RowTemplate.Height = 24;
            this.dgvDB.Size = new System.Drawing.Size(890, 370);
            this.dgvDB.TabIndex = 0;
            // 
            // tabHDV
            // 
            this.tabHDV.Controls.Add(this.numLuong);
            this.tabHDV.Controls.Add(this.lblLuong);
            this.tabHDV.Controls.Add(this.btnThemHDV);
            this.tabHDV.Controls.Add(this.txtHDVDT);
            this.tabHDV.Controls.Add(this.lblHDVDT);
            this.tabHDV.Controls.Add(this.txtHDVTen);
            this.tabHDV.Controls.Add(this.lblHDVTen);
            this.tabHDV.Controls.Add(this.txtHDVMa);
            this.tabHDV.Controls.Add(this.lblHDVMa);
            this.tabHDV.Controls.Add(this.dgvHDV);
            this.tabHDV.Location = new System.Drawing.Point(4, 29);
            this.tabHDV.Name = "tabHDV";
            this.tabHDV.Padding = new System.Windows.Forms.Padding(3);
            this.tabHDV.Size = new System.Drawing.Size(902, 487);
            this.tabHDV.TabIndex = 2;
            this.tabHDV.Text = "Hướng dẫn viên";
            this.tabHDV.UseVisualStyleBackColor = true;
            // 
            // numLuong
            // 
            this.numLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.numLuong.DecimalPlaces = 0;
            this.numLuong.Increment = new decimal(new int[] { 500000, 0, 0, 0 });
            this.numLuong.Location = new System.Drawing.Point(475, 439);
            this.numLuong.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            this.numLuong.Name = "numLuong";
            this.numLuong.Size = new System.Drawing.Size(240, 27);
            this.numLuong.TabIndex = 9;
            this.numLuong.ThousandsSeparator = true;
            // 
            // lblLuong
            // 
            this.lblLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLuong.AutoSize = true;
            this.lblLuong.Location = new System.Drawing.Point(385, 442);
            this.lblLuong.Name = "lblLuong";
            this.lblLuong.Size = new System.Drawing.Size(78, 20);
            this.lblLuong.TabIndex = 8;
            this.lblLuong.Text = "Lương CB:";
            // 
            // btnThemHDV
            // 
            this.btnThemHDV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemHDV.Location = new System.Drawing.Point(740, 435);
            this.btnThemHDV.Name = "btnThemHDV";
            this.btnThemHDV.Size = new System.Drawing.Size(120, 35);
            this.btnThemHDV.TabIndex = 10;
            this.btnThemHDV.Text = "Thêm";
            this.btnThemHDV.UseVisualStyleBackColor = true;
            this.btnThemHDV.Click += new System.EventHandler(this.btnThemHDV_Click);
            // 
            // txtHDVDT
            // 
            this.txtHDVDT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtHDVDT.Location = new System.Drawing.Point(120, 439);
            this.txtHDVDT.Name = "txtHDVDT";
            this.txtHDVDT.Size = new System.Drawing.Size(240, 27);
            this.txtHDVDT.TabIndex = 7;
            // 
            // lblHDVDT
            // 
            this.lblHDVDT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHDVDT.AutoSize = true;
            this.lblHDVDT.Location = new System.Drawing.Point(20, 442);
            this.lblHDVDT.Name = "lblHDVDT";
            this.lblHDVDT.Size = new System.Drawing.Size(81, 20);
            this.lblHDVDT.TabIndex = 6;
            this.lblHDVDT.Text = "Điện thoại:";
            // 
            // txtHDVTen
            // 
            this.txtHDVTen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHDVTen.Location = new System.Drawing.Point(475, 396);
            this.txtHDVTen.Name = "txtHDVTen";
            this.txtHDVTen.Size = new System.Drawing.Size(240, 27);
            this.txtHDVTen.TabIndex = 4;
            // 
            // lblHDVTen
            // 
            this.lblHDVTen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHDVTen.AutoSize = true;
            this.lblHDVTen.Location = new System.Drawing.Point(385, 399);
            this.lblHDVTen.Name = "lblHDVTen";
            this.lblHDVTen.Size = new System.Drawing.Size(57, 20);
            this.lblHDVTen.TabIndex = 3;
            this.lblHDVTen.Text = "Họ tên:";
            // 
            // txtHDVMa
            // 
            this.txtHDVMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtHDVMa.Location = new System.Drawing.Point(120, 396);
            this.txtHDVMa.Name = "txtHDVMa";
            this.txtHDVMa.Size = new System.Drawing.Size(240, 27);
            this.txtHDVMa.TabIndex = 2;
            // 
            // lblHDVMa
            // 
            this.lblHDVMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHDVMa.AutoSize = true;
            this.lblHDVMa.Location = new System.Drawing.Point(20, 399);
            this.lblHDVMa.Name = "lblHDVMa";
            this.lblHDVMa.Size = new System.Drawing.Size(68, 20);
            this.lblHDVMa.TabIndex = 1;
            this.lblHDVMa.Text = "Mã HDV:";
            // 
            // dgvHDV
            // 
            this.dgvHDV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHDV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHDV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHDV.Location = new System.Drawing.Point(6, 6);
            this.dgvHDV.Name = "dgvHDV";
            this.dgvHDV.RowHeadersWidth = 51;
            this.dgvHDV.RowTemplate.Height = 24;
            this.dgvHDV.Size = new System.Drawing.Size(890, 370);
            this.dgvHDV.TabIndex = 0;
            // 
            // tabDTQ
            // 
            this.tabDTQ.Controls.Add(this.txtDTQYNghia);
            this.tabDTQ.Controls.Add(this.lblDTQYNghia);
            this.tabDTQ.Controls.Add(this.txtDTQNoiDung);
            this.tabDTQ.Controls.Add(this.lblDTQNoiDung);
            this.tabDTQ.Controls.Add(this.btnThemDTQ);
            this.tabDTQ.Controls.Add(this.txtDTQDiaDiem);
            this.tabDTQ.Controls.Add(this.lblDTQDiaDiem);
            this.tabDTQ.Controls.Add(this.txtDTQTen);
            this.tabDTQ.Controls.Add(this.lblDTQTen);
            this.tabDTQ.Controls.Add(this.txtDTQMa);
            this.tabDTQ.Controls.Add(this.lblDTQMa);
            this.tabDTQ.Controls.Add(this.dgvDTQ);
            this.tabDTQ.Location = new System.Drawing.Point(4, 29);
            this.tabDTQ.Name = "tabDTQ";
            this.tabDTQ.Padding = new System.Windows.Forms.Padding(3);
            this.tabDTQ.Size = new System.Drawing.Size(902, 487);
            this.tabDTQ.TabIndex = 3;
            this.tabDTQ.Text = "Điểm tham quan";
            this.tabDTQ.UseVisualStyleBackColor = true;
            // 
            // txtDTQYNghia
            // 
            this.txtDTQYNghia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDTQYNghia.Location = new System.Drawing.Point(475, 439);
            this.txtDTQYNghia.Name = "txtDTQYNghia";
            this.txtDTQYNghia.Size = new System.Drawing.Size(240, 27);
            this.txtDTQYNghia.TabIndex = 11;
            // 
            // lblDTQYNghia
            // 
            this.lblDTQYNghia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDTQYNghia.AutoSize = true;
            this.lblDTQYNghia.Location = new System.Drawing.Point(385, 442);
            this.lblDTQYNghia.Name = "lblDTQYNghia";
            this.lblDTQYNghia.Size = new System.Drawing.Size(61, 20);
            this.lblDTQYNghia.TabIndex = 10;
            this.lblDTQYNghia.Text = "Ý nghĩa:";
            // 
            // txtDTQNoiDung
            // 
            this.txtDTQNoiDung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDTQNoiDung.Location = new System.Drawing.Point(120, 439);
            this.txtDTQNoiDung.Name = "txtDTQNoiDung";
            this.txtDTQNoiDung.Size = new System.Drawing.Size(240, 27);
            this.txtDTQNoiDung.TabIndex = 9;
            // 
            // lblDTQNoiDung
            // 
            this.lblDTQNoiDung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDTQNoiDung.AutoSize = true;
            this.lblDTQNoiDung.Location = new System.Drawing.Point(20, 442);
            this.lblDTQNoiDung.Name = "lblDTQNoiDung";
            this.lblDTQNoiDung.Size = new System.Drawing.Size(74, 20);
            this.lblDTQNoiDung.TabIndex = 8;
            this.lblDTQNoiDung.Text = "Nội dung:";
            // 
            // btnThemDTQ
            // 
            this.btnThemDTQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemDTQ.Location = new System.Drawing.Point(740, 435);
            this.btnThemDTQ.Name = "btnThemDTQ";
            this.btnThemDTQ.Size = new System.Drawing.Size(120, 35);
            this.btnThemDTQ.TabIndex = 12;
            this.btnThemDTQ.Text = "Thêm";
            this.btnThemDTQ.UseVisualStyleBackColor = true;
            this.btnThemDTQ.Click += new System.EventHandler(this.btnThemDTQ_Click);
            // 
            // txtDTQDiaDiem
            // 
            this.txtDTQDiaDiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDTQDiaDiem.Location = new System.Drawing.Point(740, 396);
            this.txtDTQDiaDiem.Name = "txtDTQDiaDiem";
            this.txtDTQDiaDiem.Size = new System.Drawing.Size(150, 27);
            this.txtDTQDiaDiem.TabIndex = 6;
            // 
            // lblDTQDiaDiem
            // 
            this.lblDTQDiaDiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDTQDiaDiem.AutoSize = true;
            this.lblDTQDiaDiem.Location = new System.Drawing.Point(665, 399);
            this.lblDTQDiaDiem.Name = "lblDTQDiaDiem";
            this.lblDTQDiaDiem.Size = new System.Drawing.Size(73, 20);
            this.lblDTQDiaDiem.TabIndex = 5;
            this.lblDTQDiaDiem.Text = "Địa điểm:";
            // 
            // txtDTQTen
            // 
            this.txtDTQTen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDTQTen.Location = new System.Drawing.Point(420, 396);
            this.txtDTQTen.Name = "txtDTQTen";
            this.txtDTQTen.Size = new System.Drawing.Size(235, 27);
            this.txtDTQTen.TabIndex = 4;
            // 
            // lblDTQTen
            // 
            this.lblDTQTen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDTQTen.AutoSize = true;
            this.lblDTQTen.Location = new System.Drawing.Point(340, 399);
            this.lblDTQTen.Name = "lblDTQTen";
            this.lblDTQTen.Size = new System.Drawing.Size(65, 20);
            this.lblDTQTen.TabIndex = 3;
            this.lblDTQTen.Text = "Tên TQ:";
            // 
            // txtDTQMa
            // 
            this.txtDTQMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtDTQMa.Location = new System.Drawing.Point(120, 396);
            this.txtDTQMa.Name = "txtDTQMa";
            this.txtDTQMa.Size = new System.Drawing.Size(200, 27);
            this.txtDTQMa.TabIndex = 2;
            // 
            // lblDTQMa
            // 
            this.lblDTQMa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDTQMa.AutoSize = true;
            this.lblDTQMa.Location = new System.Drawing.Point(20, 399);
            this.lblDTQMa.Name = "lblDTQMa";
            this.lblDTQMa.Size = new System.Drawing.Size(90, 20);
            this.lblDTQMa.TabIndex = 1;
            this.lblDTQMa.Text = "Mã điểm TQ:";
            // 
            // dgvDTQ
            // 
            this.dgvDTQ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDTQ.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDTQ.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDTQ.Location = new System.Drawing.Point(6, 6);
            this.dgvDTQ.Name = "dgvDTQ";
            this.dgvDTQ.RowHeadersWidth = 51;
            this.dgvDTQ.RowTemplate.Height = 24;
            this.dgvDTQ.Size = new System.Drawing.Size(890, 370);
            this.dgvDTQ.TabIndex = 0;
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(802, 540);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(120, 35);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrmDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 595);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.tabDanhMuc);
            this.Name = "FrmDanhMuc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục";
            this.Load += new System.EventHandler(this.FrmDanhMuc_Load);
            this.tabDanhMuc.ResumeLayout(false);
            this.tabPhuongTien.ResumeLayout(false);
            this.tabPhuongTien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPT)).EndInit();
            this.tabDiemBan.ResumeLayout(false);
            this.tabDiemBan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDB)).EndInit();
            this.tabHDV.ResumeLayout(false);
            this.tabHDV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHDV)).EndInit();
            this.tabDTQ.ResumeLayout(false);
            this.tabDTQ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDTQ)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabDanhMuc;
        private System.Windows.Forms.TabPage tabPhuongTien;
        private System.Windows.Forms.TabPage tabDiemBan;
        private System.Windows.Forms.TabPage tabHDV;
        private System.Windows.Forms.TabPage tabDTQ;
        private System.Windows.Forms.DataGridView dgvPT;
        private System.Windows.Forms.Label lblPTMa;
        private System.Windows.Forms.TextBox txtPTMa;
        private System.Windows.Forms.TextBox txtPTTen;
        private System.Windows.Forms.Label lblPTTen;
        private System.Windows.Forms.TextBox txtPTGhiChu;
        private System.Windows.Forms.Label lblPTGhiChu;
        private System.Windows.Forms.Button btnThemPT;
        private System.Windows.Forms.DataGridView dgvDB;
        private System.Windows.Forms.Label lblDBMa;
        private System.Windows.Forms.TextBox txtDBMa;
        private System.Windows.Forms.Label lblDBTen;
        private System.Windows.Forms.TextBox txtDBTen;
        private System.Windows.Forms.Label lblDBDiaChi;
        private System.Windows.Forms.TextBox txtDBDiaChi;
        private System.Windows.Forms.Label lblDBDT;
        private System.Windows.Forms.TextBox txtDBDT;
        private System.Windows.Forms.Button btnThemDB;
        private System.Windows.Forms.DataGridView dgvHDV;
        private System.Windows.Forms.Label lblHDVMa;
        private System.Windows.Forms.TextBox txtHDVMa;
        private System.Windows.Forms.Label lblHDVTen;
        private System.Windows.Forms.TextBox txtHDVTen;
        private System.Windows.Forms.Label lblHDVDT;
        private System.Windows.Forms.TextBox txtHDVDT;
        private System.Windows.Forms.Label lblLuong;
        private System.Windows.Forms.NumericUpDown numLuong;
        private System.Windows.Forms.Button btnThemHDV;
        private System.Windows.Forms.DataGridView dgvDTQ;
        private System.Windows.Forms.Label lblDTQMa;
        private System.Windows.Forms.TextBox txtDTQMa;
        private System.Windows.Forms.Label lblDTQTen;
        private System.Windows.Forms.TextBox txtDTQTen;
        private System.Windows.Forms.Label lblDTQDiaDiem;
        private System.Windows.Forms.TextBox txtDTQDiaDiem;
        private System.Windows.Forms.Label lblDTQNoiDung;
        private System.Windows.Forms.TextBox txtDTQNoiDung;
        private System.Windows.Forms.Label lblDTQYNghia;
        private System.Windows.Forms.TextBox txtDTQYNghia;
        private System.Windows.Forms.Button btnThemDTQ;
        private System.Windows.Forms.Button btnDong;
    }
}
