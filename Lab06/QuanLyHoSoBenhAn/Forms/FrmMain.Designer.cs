namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.root = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cards = new System.Windows.Forms.TableLayoutPanel();
            this.btnHoSo = new System.Windows.Forms.Button();
            this.btnKham = new System.Windows.Forms.Button();
            this.btnToa = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.root.SuspendLayout();
            this.cards.SuspendLayout();
            this.SuspendLayout();
            this.root.Dock = System.Windows.Forms.DockStyle.Fill;
            this.root.Padding = new System.Windows.Forms.Padding(48, 28, 48, 30);
            this.root.ColumnCount = 1;
            this.root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.RowCount = 3;
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 108F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Quản lý hồ sơ bệnh án";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cards.ColumnCount = 2;
            this.cards.RowCount = 2;
            this.cards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.cards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.btnHoSo.Name = "btnHoSo";
            this.btnHoSo.Text = "Hồ sơ bệnh nhân";
            this.btnHoSo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHoSo.Margin = new System.Windows.Forms.Padding(0, 0, 12, 12);
            this.btnHoSo.TabIndex = 0;
            this.btnKham.Name = "btnKham";
            this.btnKham.Text = "Khám bệnh";
            this.btnKham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKham.Margin = new System.Windows.Forms.Padding(12, 0, 0, 12);
            this.btnKham.TabIndex = 1;
            this.btnToa.Name = "btnToa";
            this.btnToa.Text = "Toa thuốc";
            this.btnToa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToa.Margin = new System.Windows.Forms.Padding(0, 12, 12, 0);
            this.btnToa.TabIndex = 2;
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Text = "Thanh toán viện phí";
            this.btnThanhToan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnThanhToan.Margin = new System.Windows.Forms.Padding(12, 12, 0, 0);
            this.btnThanhToan.TabIndex = 3;
            this.cards.Controls.Add(this.btnHoSo, 0, 0);
            this.cards.Controls.Add(this.btnKham, 1, 0);
            this.cards.Controls.Add(this.btnToa, 0, 1);
            this.cards.Controls.Add(this.btnThanhToan, 1, 1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Text = "Thoát";
            this.btnExit.Size = new System.Drawing.Size(150, 42);
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.Margin = new System.Windows.Forms.Padding(0, 18, 0, 0);
            this.btnExit.TabIndex = 4;
            this.root.Controls.Add(this.lblTitle, 0, 0);
            this.root.Controls.Add(this.cards, 0, 1);
            this.root.Controls.Add(this.btnExit, 0, 2);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 600);
            this.MinimumSize = new System.Drawing.Size(780, 540);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Text = "Quản lý hồ sơ bệnh án";
            this.Controls.Add(this.root);
            this.cards.ResumeLayout(false);
            this.root.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        private System.Windows.Forms.TableLayoutPanel root, cards;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnHoSo, btnKham, btnToa, btnThanhToan, btnExit;
    }
}
