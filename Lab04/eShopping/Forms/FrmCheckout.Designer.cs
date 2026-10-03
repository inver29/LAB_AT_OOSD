using System.Drawing;
using System.Windows.Forms;

namespace eShopping.Forms
{
    partial class FrmCheckout
    {
        private ComboBox cboKhachHang;
        private ComboBox cboKhuVuc;
        private ComboBox cboLoaiGiao;
        private ComboBox cboLoaiThe;

        private TextBox txtNguoiNhan;
        private TextBox txtDiaChi;
        private TextBox txtDienThoai;

        private Label lblTong;

        private Button btnTinhTong;
        private Button btnLuu;
        private Button btnTaiDon;
        private Button btnDong;

        private DataGridView dgvDon;
        private DataGridView dgvChiTiet;

        private void InitializeComponent()
        {
            SuspendLayout();

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(12)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 265));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 45));

            var thongTin = new GroupBox
            {
                Text = "Thông tin đặt hàng",
                Dock = DockStyle.Fill
            };

            cboKhachHang = new ComboBox
            {
                Name = "cboKhachHang",
                Location = new Point(130, 30),
                Size = new Size(310, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboLoaiThe = new ComboBox
            {
                Name = "cboLoaiThe",
                Location = new Point(650, 30),
                Size = new Size(370, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            txtNguoiNhan = new TextBox
            {
                Name = "txtNguoiNhan",
                Location = new Point(130, 70),
                Size = new Size(310, 28),
                MaxLength = 100
            };

            txtDiaChi = new TextBox
            {
                Name = "txtDiaChi",
                Location = new Point(650, 70),
                Size = new Size(370, 28),
                MaxLength = 250
            };

            txtDienThoai = new TextBox
            {
                Name = "txtDienThoai",
                Location = new Point(130, 110),
                Size = new Size(210, 28),
                MaxLength = 20
            };

            cboKhuVuc = new ComboBox
            {
                Name = "cboKhuVuc",
                Location = new Point(455, 110),
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cboLoaiGiao = new ComboBox
            {
                Name = "cboLoaiGiao",
                Location = new Point(770, 110),
                Size = new Size(250, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            lblTong = new Label
            {
                Name = "lblTong",
                Location = new Point(20, 150),
                Size = new Size(1000, 55),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.MidnightBlue
            };

            btnTinhTong = new Button
            {
                Name = "btnTinhTong",
                Text = "Tính tổng",
                Location = new Point(20, 212),
                Size = new Size(120, 35)
            };

            btnLuu = new Button
            {
                Name = "btnLuu",
                Text = "Lưu đơn chờ thanh toán",
                Location = new Point(155, 212),
                Size = new Size(235, 35),
                Enabled = false
            };

            btnTaiDon = new Button
            {
                Name = "btnTaiDon",
                Text = "Tải danh sách đơn",
                Location = new Point(410, 212),
                Size = new Size(175, 35)
            };

            btnDong = new Button
            {
                Name = "btnDong",
                Text = "Đóng",
                Location = new Point(610, 212),
                Size = new Size(100, 35)
            };

            thongTin.Controls.AddRange(new Control[]
            {
                new Label
                {
                    Text = "Khách hàng:",
                    Location = new Point(20, 34), AutoSize = true
                },
                cboKhachHang,
                new Label
                {
                    Text = "Loại thẻ:",
                    Location = new Point(550, 34), AutoSize = true
                },
                cboLoaiThe,
                new Label
                {
                    Text = "Người nhận:",
                    Location = new Point(20, 74), AutoSize = true
                },
                txtNguoiNhan,
                new Label
                {
                    Text = "Địa chỉ:",
                    Location = new Point(550, 74), AutoSize = true
                },
                txtDiaChi,
                new Label
                {
                    Text = "Điện thoại:",
                    Location = new Point(20, 114), AutoSize = true
                },
                txtDienThoai,
                new Label
                {
                    Text = "Khu vực:",
                    Location = new Point(365, 114), AutoSize = true
                },
                cboKhuVuc,
                new Label
                {
                    Text = "Loại giao:",
                    Location = new Point(680, 114), AutoSize = true
                },
                cboLoaiGiao,
                lblTong, btnTinhTong, btnLuu, btnTaiDon, btnDong
            });

            dgvDon = new DataGridView
            {
                Name = "dgvDon",
                Dock = DockStyle.Fill,
                ReadOnly = true,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BackgroundColor = Color.White
            };

            dgvDon.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = "MaDon",
                    DataPropertyName = "MaDon",
                    HeaderText = "Mã đơn",
                    FillWeight = 230,
                    MinimumWidth = 280
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "KhachHang",
                    DataPropertyName = "KhachHang",
                    HeaderText = "Khách hàng",
                    FillWeight = 140
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "ThoiDiem",
                    DataPropertyName = "ThoiDiem",
                    HeaderText = "Thời điểm",
                    FillWeight = 140,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "dd/MM/yyyy HH:mm"
                    }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "TongTien",
                    DataPropertyName = "TongTien",
                    HeaderText = "Tổng tiền",
                    FillWeight = 100,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "N0",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "TrangThai",
                    DataPropertyName = "TrangThai",
                    HeaderText = "Trạng thái",
                    FillWeight = 120
                });

            dgvChiTiet = new DataGridView
            {
                Name = "dgvChiTiet",
                Dock = DockStyle.Fill,
                ReadOnly = true,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BackgroundColor = Color.White
            };

            dgvChiTiet.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = "TenSPLucDat",
                    DataPropertyName = "TenSPLucDat",
                    HeaderText = "Sản phẩm lúc đặt",
                    FillWeight = 200
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "SoLuong",
                    DataPropertyName = "SoLuong",
                    HeaderText = "Số lượng",
                    FillWeight = 65
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "DonGiaLucDat",
                    DataPropertyName = "DonGiaLucDat",
                    HeaderText = "Đơn giá lúc đặt",
                    FillWeight = 110,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "N0",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "ThanhTien",
                    DataPropertyName = "ThanhTien",
                    HeaderText = "Thành tiền",
                    FillWeight = 110,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "N0",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                });

            layout.Controls.Add(thongTin, 0, 0);

            layout.Controls.Add(new Label
            {
                Text = "DANH SÁCH ĐƠN ĐÃ LƯU TRONG SQL",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            }, 0, 1);

            layout.Controls.Add(dgvDon, 0, 2);

            layout.Controls.Add(new Label
            {
                Text = "CHI TIẾT ĐƠN ĐANG CHỌN",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            }, 0, 3);

            layout.Controls.Add(dgvChiTiet, 0, 4);
            Controls.Add(layout);

            cboKhachHang.SelectedIndexChanged +=
                cboKhachHang_SelectedIndexChanged;

            cboKhuVuc.SelectedIndexChanged += HuyBaoGia;
            cboLoaiGiao.SelectedIndexChanged += HuyBaoGia;
            cboLoaiThe.SelectedIndexChanged += HuyBaoGia;

            btnTinhTong.Click += btnTinhTong_Click;
            btnLuu.Click += btnLuu_Click;
            btnTaiDon.Click += btnTaiDon_Click;
            btnDong.Click += btnDong_Click;

            dgvDon.SelectionChanged += dgvDon_SelectionChanged;
            Load += FrmCheckout_Load;

            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1120, 730);
            MinimumSize = new Size(1100, 650);
            StartPosition = FormStartPosition.CenterParent;
            Name = "FrmCheckout";
            Text = "e-SHOPPING - Đặt hàng";

            ResumeLayout(false);
        }
    }
}