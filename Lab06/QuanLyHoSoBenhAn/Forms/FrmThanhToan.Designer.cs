namespace QuanLyHoSoBenhAn.Forms
{
    partial class FrmThanhToan
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.root = new System.Windows.Forms.TableLayoutPanel(); this.root.Name = "root";
            this.root.Dock = System.Windows.Forms.DockStyle.Fill; this.root.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12); this.root.ColumnCount = 1;
            this.root.RowCount = 7;
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.lblTitle = new System.Windows.Forms.Label(); this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "Thanh toán viện phí"; this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold); this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill; this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.root.Controls.Add(this.lblTitle, 0, 0);
            this.searchBar = new System.Windows.Forms.FlowLayoutPanel(); this.searchBar.Name = "searchBar";
            this.searchBar.Dock = System.Windows.Forms.DockStyle.Fill; this.searchBar.WrapContents = false; this.searchBar.AutoScroll = true;
            this.root.Controls.Add(this.searchBar, 0, 1);
            this.txtSearch = new System.Windows.Forms.TextBox(); this.txtSearch.Name = "txtSearch";
            this.txtSearch.Width = 300; this.txtSearch.MaxLength=100; this.txtSearch.Margin=new System.Windows.Forms.Padding(0, 10, 12, 0);
            this.searchBar.Controls.Add(this.txtSearch);
            this.btnSearch = new System.Windows.Forms.Button(); this.btnSearch.Name = "btnSearch";
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Size = new System.Drawing.Size(120, 38); this.btnSearch.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.searchBar.Controls.Add(this.btnSearch);
            this.btnReload = new System.Windows.Forms.Button(); this.btnReload.Name = "btnReload";
            this.btnReload.Text = "Làm mới";
            this.btnReload.Size = new System.Drawing.Size(120, 38); this.btnReload.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.searchBar.Controls.Add(this.btnReload);
            this.gridPhieu = new System.Windows.Forms.DataGridView(); this.gridPhieu.Name = "gridPhieu";
            this.gridPhieu.Dock = System.Windows.Forms.DockStyle.Fill; this.gridPhieu.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.root.Controls.Add(this.gridPhieu, 0, 2);
            this.gridChiPhi = new System.Windows.Forms.DataGridView(); this.gridChiPhi.Name = "gridChiPhi";
            this.gridChiPhi.Dock = System.Windows.Forms.DockStyle.Fill; this.gridChiPhi.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.root.Controls.Add(this.gridChiPhi, 0, 3);
            this.lblTong = new System.Windows.Forms.Label(); this.lblTong.Name = "lblTong";
            this.lblTong.Text = "Tổng viện phí: 0 đ";
            this.lblTong.Dock=System.Windows.Forms.DockStyle.Fill; this.lblTong.TextAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.root.Controls.Add(this.lblTong, 0, 4);
            this.lblTong.Font=new System.Drawing.Font("Segoe UI",18F,System.Drawing.FontStyle.Bold);
            this.actions = new System.Windows.Forms.FlowLayoutPanel(); this.actions.Name = "actions";
            this.actions.Dock = System.Windows.Forms.DockStyle.Fill; this.actions.WrapContents = false; this.actions.AutoScroll = true;
            this.root.Controls.Add(this.actions, 0, 5);
            this.btnThanhToan = new System.Windows.Forms.Button(); this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Text = "Xác nhận thanh toán";
            this.btnThanhToan.Size = new System.Drawing.Size(240, 38); this.btnThanhToan.Margin = new System.Windows.Forms.Padding(0, 4, 10, 0);
            this.actions.Controls.Add(this.btnThanhToan);
            this.lblStatus = new System.Windows.Forms.Label(); this.lblStatus.Name = "lblStatus";
            this.lblStatus.Text = "Chọn phiếu để xem chi phí khám, xét nghiệm và thuốc.";
            this.lblStatus.Dock=System.Windows.Forms.DockStyle.Fill; this.lblStatus.TextAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.root.Controls.Add(this.lblStatus, 0, 6);
            this.AutoScaleDimensions=new System.Drawing.SizeF(7F,17F); this.AutoScaleMode=System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize=new System.Drawing.Size(1200,790); this.MinimumSize=new System.Drawing.Size(1100,760); this.Text="FrmThanhToan — Quản lý hồ sơ bệnh án";
            this.Font=new System.Drawing.Font("Segoe UI",10F); this.Controls.Add(this.root); this.ResumeLayout(false);
        }
        private System.Windows.Forms.TableLayoutPanel root;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel searchBar;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.DataGridView gridPhieu;
        private System.Windows.Forms.DataGridView gridChiPhi;
        private System.Windows.Forms.Label lblTong;
        private System.Windows.Forms.FlowLayoutPanel actions;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Label lblStatus;
    }
}
