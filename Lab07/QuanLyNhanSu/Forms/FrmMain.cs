using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyNhanSu.Common;

namespace QuanLyNhanSu.Forms
{
    public class FrmMain : Form
    {
        private TableLayoutPanel root;
        private Label lblTitle;
        private Panel pnlHeader;
        private Label lblSub;
        private ComboBox cboVaiTroDemo;
        private TableLayoutPanel cards;
        private Button btnHoSo, btnQuyetDinh, btnChamCong, btnThuongPhat, btnThuChi, btnBangLuong, btnDanhMuc;
        private Button btnExit;

        public FrmMain()
        {
            InitializeComponent();
            FormHelper.Style(this);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Style all card buttons exactly as in Lab06
            var cardButtons = new[] { btnHoSo, btnQuyetDinh, btnChamCong, btnThuongPhat, btnThuChi, btnBangLuong, btnDanhMuc };
            foreach (var button in cardButtons)
            {
                button.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
                button.TextAlign = ContentAlignment.MiddleCenter;
                button.Padding = new Padding(12);
                button.BackColor = Color.FromArgb(232, 240, 248);
                button.ForeColor = Color.FromArgb(35, 77, 115);
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.FromArgb(197, 213, 229);
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(217, 231, 244);
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(201, 221, 240);
                button.UseVisualStyleBackColor = false;
            }

            lblTitle.ForeColor = Color.FromArgb(35, 65, 94);

            btnExit.BackColor = Color.FromArgb(230, 235, 241);
            btnExit.ForeColor = FormHelper.Ink;
            btnExit.FlatAppearance.MouseOverBackColor = Color.FromArgb(215, 223, 233);
            btnExit.FlatAppearance.MouseDownBackColor = Color.FromArgb(201, 211, 224);

            // Gán sự kiện mở form dạng ShowDialog như Lab06
            btnHoSo.Click += (s, e) => MoForm(() => new FrmHoSoHopDong());
            btnQuyetDinh.Click += (s, e) => MoForm(() => new FrmQuyetDinhNhanSu());
            btnChamCong.Click += (s, e) => MoForm(() => new FrmChamCongPhuTroi());
            btnThuongPhat.Click += (s, e) => MoForm(() => new FrmThuongPhat());
            btnThuChi.Click += (s, e) => MoForm(() => new FrmThuChi());
            btnBangLuong.Click += (s, e) => MoForm(() => new FrmBangLuong());
            btnDanhMuc.Click += (s, e) => MoForm(() => new FrmDanhMuc());
            btnExit.Click += (s, e) => Close();
        }

        private void InitializeComponent()
        {
            this.root = new TableLayoutPanel();
            this.pnlHeader = new Panel();
            this.lblTitle = new Label();
            this.lblSub = new Label();
            this.cboVaiTroDemo = new ComboBox();
            this.cards = new TableLayoutPanel();

            this.btnHoSo = new Button();
            this.btnQuyetDinh = new Button();
            this.btnChamCong = new Button();
            this.btnThuongPhat = new Button();
            this.btnThuChi = new Button();
            this.btnBangLuong = new Button();
            this.btnDanhMuc = new Button();
            this.btnExit = new Button();

            this.SuspendLayout();
            this.root.SuspendLayout();
            this.cards.SuspendLayout();

            // Form
            this.ClientSize = new Size(1060, 680);
            this.MinimumSize = new Size(950, 600);
            this.Text = "Quản lý nhân sự";
            this.Controls.Add(this.root);

            // Root layout (TableLayoutPanel - 1 column, 3 rows)
            this.root.Dock = DockStyle.Fill;
            this.root.Padding = new Padding(48, 20, 48, 25);
            this.root.ColumnCount = 1;
            this.root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.root.RowCount = 3;
            this.root.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F)); // Header
            this.root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));  // Cards
            this.root.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));  // Exit button

            // 1. Header panel (Title + Role Selector)
            this.pnlHeader.Dock = DockStyle.Fill;
            this.pnlHeader.Margin = new Padding(0);

            this.lblTitle.Text = "Quản lý nhân sự";
            this.lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Height = 50;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.pnlHeader.Controls.Add(this.lblTitle);

            var pnlSubBar = new Panel { Dock = DockStyle.Bottom, Height = 34 };
            this.lblSub.Text = "Hệ thống quản trị nhân sự, tiền lương & bảo hiểm";
            this.lblSub.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.lblSub.ForeColor = Color.FromArgb(100, 115, 130);
            this.lblSub.Location = new Point(0, 7);
            this.lblSub.AutoSize = true;
            pnlSubBar.Controls.Add(this.lblSub);

            var lblVaiTro = new Label {
                Text = "Vai trò (Demo):",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = FormHelper.Ink,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(620, 7),
                AutoSize = true
            };
            pnlSubBar.Controls.Add(lblVaiTro);

            this.cboVaiTroDemo.Font = new Font("Segoe UI", 9.5F);
            this.cboVaiTroDemo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboVaiTroDemo.Width = 220;
            this.cboVaiTroDemo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.cboVaiTroDemo.Location = new Point(735, 4);
            this.cboVaiTroDemo.Items.AddRange(new object[] {
                "Quản trị / Toàn quyền (Demo)",
                "Nhân sự",
                "Trưởng bộ phận",
                "Ban chấm công",
                "Kế toán",
                "Giám đốc"
            });
            this.cboVaiTroDemo.SelectedIndex = 0;
            this.cboVaiTroDemo.SelectedIndexChanged += (s, e) => {
                CurrentContext.CurrentRole = this.cboVaiTroDemo.SelectedItem.ToString();
            };
            pnlSubBar.Controls.Add(this.cboVaiTroDemo);

            this.pnlHeader.Controls.Add(pnlSubBar);
            this.root.Controls.Add(this.pnlHeader, 0, 0);

            // 2. Cards Grid (4 columns x 2 rows)
            this.cards.Dock = DockStyle.Fill;
            this.cards.ColumnCount = 4;
            this.cards.RowCount = 2;
            this.cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.cards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.cards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

            // Row 0
            this.btnHoSo.Text = "Hồ sơ &\nHợp đồng";
            this.btnHoSo.Dock = DockStyle.Fill;
            this.btnHoSo.Margin = new Padding(0, 0, 10, 10);
            this.cards.Controls.Add(this.btnHoSo, 0, 0);

            this.btnQuyetDinh.Text = "Quyết định\nLương & Chức vụ";
            this.btnQuyetDinh.Dock = DockStyle.Fill;
            this.btnQuyetDinh.Margin = new Padding(10, 0, 10, 10);
            this.cards.Controls.Add(this.btnQuyetDinh, 1, 0);

            this.btnChamCong.Text = "Phân ca, Chấm công\n& Phụ trội";
            this.btnChamCong.Dock = DockStyle.Fill;
            this.btnChamCong.Margin = new Padding(10, 0, 10, 10);
            this.cards.Controls.Add(this.btnChamCong, 2, 0);

            this.btnThuongPhat.Text = "Khen thưởng\n& Kỷ luật";
            this.btnThuongPhat.Dock = DockStyle.Fill;
            this.btnThuongPhat.Margin = new Padding(10, 0, 0, 10);
            this.cards.Controls.Add(this.btnThuongPhat, 3, 0);

            // Row 1
            this.btnThuChi.Text = "Thu chi\nTài chính";
            this.btnThuChi.Dock = DockStyle.Fill;
            this.btnThuChi.Margin = new Padding(0, 10, 10, 0);
            this.cards.Controls.Add(this.btnThuChi, 0, 1);

            this.btnBangLuong.Text = "Bảng lương\n& Phiếu lương";
            this.btnBangLuong.Dock = DockStyle.Fill;
            this.btnBangLuong.Margin = new Padding(10, 10, 10, 0);
            this.cards.Controls.Add(this.btnBangLuong, 1, 1);

            this.btnDanhMuc.Text = "Quản lý\nDanh mục";
            this.btnDanhMuc.Dock = DockStyle.Fill;
            this.btnDanhMuc.Margin = new Padding(10, 10, 0, 0);
            this.cards.Controls.Add(this.btnDanhMuc, 2, 1);
            this.cards.SetColumnSpan(this.btnDanhMuc, 2);

            this.root.Controls.Add(this.cards, 0, 1);

            // 3. Exit button
            this.btnExit.Text = "Thoát";
            this.btnExit.Size = new Size(160, 42);
            this.btnExit.Anchor = AnchorStyles.None;
            this.btnExit.Margin = new Padding(0, 12, 0, 0);
            this.btnExit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.root.Controls.Add(this.btnExit, 0, 2);

            this.cards.ResumeLayout(false);
            this.root.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void MoForm(Func<Form> factory)
        {
            FormHelper.Run(this, () =>
            {
                using (var form = factory())
                {
                    form.ShowDialog(this);
                }
            });
        }
    }
}
