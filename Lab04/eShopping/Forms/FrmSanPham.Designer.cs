using System.Drawing;
using System.Windows.Forms;
using eShopping.Models;

namespace eShopping.Forms
{
    partial class FrmSanPham
    {
        private TextBox txtTuKhoa;
        private ComboBox cboNhom;
        private NumericUpDown numSoLuong;
        private DataGridView dgvSanPham;

        private Button btnTim;
        private Button btnTaiLai;
        private Button btnThemGio;
        private Button btnDong;

        private Label lblSoDong;
        private Label lblGio;

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

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 65));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));

            var timKiem = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 10, 0, 0),
                WrapContents = false
            };

            txtTuKhoa = new TextBox
            {
                Name = "txtTuKhoa",
                Width = 245,
                MaxLength = 200
            };

            cboNhom = new ComboBox
            {
                Name = "cboNhom",
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            btnTim = new Button
            {
                Name = "btnTim",
                Text = "Tìm kiếm",
                Width = 105,
                Height = 33
            };

            btnTaiLai = new Button
            {
                Name = "btnTaiLai",
                Text = "Tải lại",
                Width = 100,
                Height = 33
            };

            timKiem.Controls.AddRange(new Control[]
            {
                new Label
                {
                    Text = "Từ khóa:", AutoSize = true,
                    Margin = new Padding(3, 7, 8, 3)
                },
                txtTuKhoa,
                new Label
                {
                    Text = "Nhóm:", AutoSize = true,
                    Margin = new Padding(15, 7, 8, 3)
                },
                cboNhom, btnTim, btnTaiLai
            });

            dgvSanPham = new DataGridView
            {
                Name = "dgvSanPham",
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

            dgvSanPham.Columns.AddRange(
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
                    FillWeight = 180
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "TenNhom",
                    DataPropertyName = "TenNhom",
                    HeaderText = "Nhóm sản phẩm",
                    FillWeight = 110
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "NhaSanXuat",
                    DataPropertyName = "NhaSanXuat",
                    HeaderText = "Nhà sản xuất",
                    FillWeight = 100
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "GiaHienHanh",
                    DataPropertyName = "GiaHienHanh",
                    HeaderText = "Đơn giá",
                    FillWeight = 85,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "N0",
                        Alignment = DataGridViewContentAlignment.MiddleRight
                    }
                },
                new DataGridViewCheckBoxColumn
                {
                    Name = "TinhTrang",
                    DataPropertyName = "TinhTrang",
                    HeaderText = "Còn hàng",
                    FillWeight = 65
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
                Width = 90,
                Minimum = 1,
                Maximum = QuyTac.SoLuongToiDa,
                Value = 1
            };

            btnThemGio = new Button
            {
                Name = "btnThemGio",
                Text = "Thêm vào giỏ",
                Width = 155,
                Height = 35
            };

            btnDong = new Button
            {
                Name = "btnDong",
                Text = "Đóng",
                Width = 90,
                Height = 35
            };

            lblSoDong = new Label
            {
                Name = "lblSoDong",
                AutoSize = true,
                Margin = new Padding(15, 8, 10, 3)
            };

            lblGio = new Label
            {
                Name = "lblGio",
                AutoSize = true,
                Margin = new Padding(10, 8, 3, 3)
            };

            thaoTac.Controls.AddRange(new Control[]
            {
                new Label
                {
                    Text = "Số lượng:", AutoSize = true,
                    Margin = new Padding(3, 8, 8, 3)
                },
                numSoLuong, btnThemGio, btnDong, lblSoDong, lblGio
            });

            layout.Controls.Add(timKiem, 0, 0);
            layout.Controls.Add(dgvSanPham, 0, 1);
            layout.Controls.Add(thaoTac, 0, 2);
            Controls.Add(layout);

            btnTim.Click += btnTim_Click;
            btnTaiLai.Click += btnTaiLai_Click;
            btnThemGio.Click += btnThemGio_Click;
            btnDong.Click += btnDong_Click;
            Load += FrmSanPham_Load;

            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1080, 600);
            MinimumSize = new Size(1000, 500);
            StartPosition = FormStartPosition.CenterParent;
            Name = "FrmSanPham";
            Text = "e-SHOPPING - Sản phẩm";

            ResumeLayout(false);
        }
    }
}