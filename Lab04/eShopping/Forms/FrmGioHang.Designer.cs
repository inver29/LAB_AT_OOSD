using System.Drawing;
using System.Windows.Forms;
using eShopping.Models;

namespace eShopping.Forms
{
    partial class FrmGioHang
    {
        private DataGridView dgvGio;
        private NumericUpDown numSoLuong;
        private Label lblTong;

        private Button btnCapNhat;
        private Button btnXoa;
        private Button btnLamRong;
        private Button btnLayGia;
        private Button btnCheckout;
        private Button btnDong;

        private void InitializeComponent()
        {
            SuspendLayout();

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(12)
            };

            layout.ColumnStyles.Add(
                new ColumnStyle(SizeType.Percent, 100));

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));

            var title = new Label
            {
                Text = "GIỎ HÀNG",
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.MidnightBlue
            };

            dgvGio = new DataGridView
            {
                Name = "dgvGio",
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

            dgvGio.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    Name = "MaSP",
                    DataPropertyName = "MaSP",
                    Visible = false
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "TenSP",
                    DataPropertyName = "TenSP",
                    HeaderText = "Tên sản phẩm",
                    FillWeight = 200
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "SoLuong",
                    DataPropertyName = "SoLuong",
                    HeaderText = "Số lượng",
                    FillWeight = 70
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "DonGia",
                    DataPropertyName = "DonGia",
                    HeaderText = "Đơn giá",
                    FillWeight = 100,
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

            var thaoTac = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 8, 0, 0),
                WrapContents = true
            };

            numSoLuong = new NumericUpDown
            {
                Name = "numSoLuong",
                Width = 80,
                Minimum = 1,
                Maximum = QuyTac.SoLuongToiDa,
                Value = 1
            };

            btnCapNhat = new Button
            {
                Name = "btnCapNhat",
                Text = "Cập nhật",
                Width = 105,
                Height = 35
            };

            btnXoa = new Button
            {
                Name = "btnXoa",
                Text = "Xóa dòng",
                Width = 100,
                Height = 35
            };

            btnLamRong = new Button
            {
                Name = "btnLamRong",
                Text = "Làm rỗng",
                Width = 100,
                Height = 35
            };

            btnLayGia = new Button
            {
                Name = "btnLayGia",
                Text = "Lấy giá từ SQL",
                Width = 140,
                Height = 35
            };

            btnCheckout = new Button
            {
                Name = "btnCheckout",
                Text = "Đặt hàng",
                Width = 120,
                Height = 35
            };

            btnDong = new Button
            {
                Name = "btnDong",
                Text = "Đóng",
                Width = 90,
                Height = 35
            };

            lblTong = new Label
            {
                Name = "lblTong",
                AutoSize = true,
                Margin = new Padding(3, 12, 3, 3)
            };

            thaoTac.Controls.AddRange(new Control[]
            {
                new Label
                {
                    Text = "Số lượng:", AutoSize = true,
                    Margin = new Padding(3, 8, 8, 3)
                },
                numSoLuong, btnCapNhat, btnXoa, btnLamRong,
                btnLayGia, btnCheckout, btnDong, lblTong
            });

            thaoTac.SetFlowBreak(btnDong, true);

            layout.Controls.Add(title, 0, 0);
            layout.Controls.Add(dgvGio, 0, 1);
            layout.Controls.Add(thaoTac, 0, 2);
            Controls.Add(layout);

            dgvGio.SelectionChanged += dgvGio_SelectionChanged;
            btnCapNhat.Click += btnCapNhat_Click;
            btnXoa.Click += btnXoa_Click;
            btnLamRong.Click += btnLamRong_Click;
            btnLayGia.Click += btnLayGia_Click;
            btnCheckout.Click += btnCheckout_Click;
            btnDong.Click += btnDong_Click;
            Load += FrmGioHang_Load;

            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1080, 580);
            MinimumSize = new Size(1000, 500);
            StartPosition = FormStartPosition.CenterParent;
            Name = "FrmGioHang";
            Text = "e-SHOPPING - Giỏ hàng";

            ResumeLayout(false);
        }
    }
}