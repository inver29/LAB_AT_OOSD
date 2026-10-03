using System.Drawing;
using System.Windows.Forms;

namespace eShopping.Forms
{
    partial class FrmMain
    {
        private Label lblTitle;
        private Label lblKetNoi;
        private Label lblThongKe;
        private Label lblGio;

        private Button btnSanPham;
        private Button btnGioHang;
        private Button btnCheckout;
        private Button btnKiemTra;
        private Button btnThoat;

        private void InitializeComponent()
        {
            SuspendLayout();

            lblTitle = new Label
            {
                Name = "lblTitle",
                Text = "HỆ THỐNG e-SHOPPING",
                Font = new Font("Segoe UI", 21F, FontStyle.Bold),
                ForeColor = Color.MidnightBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(20, 25),
                Size = new Size(800, 60)
            };

            btnSanPham = new Button
            {
                Name = "btnSanPham",
                Text = "Sản phẩm",
                Location = new Point(70, 120),
                Size = new Size(210, 75),
                BackColor = Color.White
            };

            btnGioHang = new Button
            {
                Name = "btnGioHang",
                Text = "Giỏ hàng",
                Location = new Point(315, 120),
                Size = new Size(210, 75),
                BackColor = Color.White
            };

            btnCheckout = new Button
            {
                Name = "btnCheckout",
                Text = "Đặt hàng",
                Location = new Point(560, 120),
                Size = new Size(210, 75),
                BackColor = Color.White
            };

            lblKetNoi = new Label
            {
                Name = "lblKetNoi",
                Text = "SQL Server: đang kiểm tra...",
                Location = new Point(70, 225),
                Size = new Size(700, 28)
            };

            lblThongKe = new Label
            {
                Name = "lblThongKe",
                Location = new Point(70, 265),
                Size = new Size(700, 28)
            };

            lblGio = new Label
            {
                Name = "lblGio",
                Location = new Point(70, 305),
                Size = new Size(400, 28)
            };

            btnKiemTra = new Button
            {
                Name = "btnKiemTra",
                Text = "Kiểm tra kết nối",
                Location = new Point(440, 350),
                Size = new Size(180, 40)
            };

            btnThoat = new Button
            {
                Name = "btnThoat",
                Text = "Thoát",
                Location = new Point(640, 350),
                Size = new Size(130, 40)
            };

            btnSanPham.Click += btnSanPham_Click;
            btnGioHang.Click += btnGioHang_Click;
            btnCheckout.Click += btnCheckout_Click;
            btnKiemTra.Click += btnKiemTra_Click;
            btnThoat.Click += btnThoat_Click;
            Load += FrmMain_Load;

            Controls.AddRange(new Control[]
            {
                lblTitle, btnSanPham, btnGioHang, btnCheckout,
                lblKetNoi, lblThongKe, lblGio, btnKiemTra, btnThoat
            });

            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 11F);
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(840, 420);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Name = "FrmMain";
            Text = "e-SHOPPING - Trang chủ";

            ResumeLayout(false);
        }
    }
}