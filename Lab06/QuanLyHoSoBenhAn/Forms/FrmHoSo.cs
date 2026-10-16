using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Services;

namespace QuanLyHoSoBenhAn.Forms
{
    public partial class FrmHoSo : Form
    {
        private readonly HoSoService service = new HoSoService();
        private int maHoSo;
        private string anh = "";
        private bool loading;
        public FrmHoSo()
        {
            InitializeComponent(); FormHelper.Style(this);
            txtHoTen.MaxLength = 100; txtDiaChi.MaxLength = 200; txtDienThoai.MaxLength = 15;
            cbGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" }); cbGioiTinh.SelectedIndex = 0;
            dtSinh.MaxDate = DateTime.Today; dtSinh.Value = DateTime.Today.AddYears(-25);
            numPhi.Value = 200000;
            Shown += (s, e) => FormHelper.Run(this, LoadData);
            btnSearch.Click += (s, e) => FormHelper.Run(this, LoadData);
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; FormHelper.Run(this, LoadData); } };
            btnReload.Click += (s, e) => { txtSearch.Clear(); FormHelper.Run(this, LoadData); };
            gridHoSo.SelectionChanged += (s, e) => { if (!loading) FormHelper.Run(this, ChonHoSo); };
            btnNew.Click += (s, e) => NhapMoi();
            btnAnh.Click += (s, e) => FormHelper.Run(this, ChonAnh);
            btnDangKy.Click += (s, e) => FormHelper.Run(this, DangKy);
            btnGiaHan.Click += (s, e) => FormHelper.Run(this, GiaHan);
            FormClosed += (s, e) => { if (picAnh.Image != null) picAnh.Image.Dispose(); };
        }
        private void LoadData()
        {
            loading = true;
            try
            {
                gridHoSo.DataSource = service.DanhSach(txtSearch.Text);
                FormHelper.Columns(gridHoSo, "MaHSBA", "Mã hồ sơ", "HoTen", "Họ tên", "NgaySinh", "Ngày sinh",
                    "DienThoai", "Điện thoại", "HanPhi", "Hạn phí", "TinhTrangPhi", "Tình trạng phí");
            }
            finally { loading = false; }
            if (gridHoSo.Rows.Count > 0) ChonHoSo(); else NhapMoi();
        }
        private void ChonHoSo()
        {
            DataRow row = FormHelper.Row(gridHoSo); if (row == null) return;
            maHoSo = Convert.ToInt32(row["MaHSBA"]);
            txtHoTen.Text = row["HoTen"].ToString(); txtDiaChi.Text = row["DiaChi"].ToString();
            txtDienThoai.Text = row["DienThoai"].ToString(); dtSinh.Value = Convert.ToDateTime(row["NgaySinh"]);
            cbGioiTinh.SelectedItem = row["GioiTinh"].ToString(); numPhi.Value = Math.Min(numPhi.Maximum, Convert.ToDecimal(row["PhiThuongNien"]));
            anh = row["Anh3x4"].ToString(); HienAnh(anh);
            lblHoSo.Text = "HSBA " + maHoSo + " • Cấp ngày " + Convert.ToDateTime(row["NgayCap"]).ToString("dd/MM/yyyy");
            lblHanPhi.Text = Convert.ToDateTime(row["HanPhi"]).ToString("dd/MM/yyyy") + " • " + row["TinhTrangPhi"];
            SetMode(false);
            btnGiaHan.Enabled = Convert.ToDateTime(row["HanPhi"]).Date <= DateTime.Today;
            lblStatus.Text = "Đang xem hồ sơ " + maHoSo + ". Chọn Nhập mới để đăng ký bệnh nhân khác.";
        }
        private void SetMode(bool isNew)
        {
            txtHoTen.ReadOnly = txtDiaChi.ReadOnly = txtDienThoai.ReadOnly = !isNew;
            dtSinh.Enabled = cbGioiTinh.Enabled = btnAnh.Enabled = isNew;
            btnDangKy.Enabled = isNew; btnGiaHan.Enabled = !isNew;
        }
        private void NhapMoi()
        {
            loading = true; gridHoSo.ClearSelection(); gridHoSo.CurrentCell = null; loading = false;
            maHoSo = 0; anh = ""; txtHoTen.Clear(); txtDiaChi.Clear(); txtDienThoai.Clear();
            dtSinh.Value = DateTime.Today.AddYears(-25); cbGioiTinh.SelectedIndex = 0; numPhi.Value = 200000;
            HienAnh(""); lblHoSo.Text = "Cấp mã tự động sau khi đăng ký"; lblHanPhi.Text = "Ngày đóng phí + 1 năm";
            SetMode(true); lblStatus.Text = "Nhập đủ thông tin (*) và chọn ảnh 3×4, sau đó bấm Đăng ký và cấp hồ sơ."; txtHoTen.Focus();
        }
        private void ChonAnh()
        {
            using (var dialog = new OpenFileDialog { Filter = "Ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png", Title = "Chọn ảnh bệnh nhân" })
                if (dialog.ShowDialog(this) == DialogResult.OK)
                { HienAnh(dialog.FileName); anh = dialog.FileName; }
        }
        private void HienAnh(string path)
        {
            if (picAnh.Image != null) { var old = picAnh.Image; picAnh.Image = null; old.Dispose(); }
            if (string.IsNullOrWhiteSpace(path)) return;
            if (!Path.IsPathRooted(path)) path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            if (!File.Exists(path)) return;
            using (var image = Image.FromFile(path)) picAnh.Image = new Bitmap(image);
        }
        private void DangKy()
        {
            if (maHoSo != 0) throw new InvalidOperationException("Hãy chọn Nhập mới trước khi đăng ký.");
            if (!File.Exists(anh)) throw new InvalidOperationException("Hãy chọn một ảnh 3×4 hợp lệ.");
            string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuanLyHoSoBenhAn", "Anh");
            Directory.CreateDirectory(folder);
            string saved = Path.Combine(folder, Guid.NewGuid().ToString("N") + Path.GetExtension(anh));
            File.Copy(anh, saved);
            int id;
            try
            {
                id = service.DangKy(new BenhNhanInput { HoTen = txtHoTen.Text, NgaySinh = dtSinh.Value.Date,
                    GioiTinh = cbGioiTinh.Text, DiaChi = txtDiaChi.Text, DienThoai = txtDienThoai.Text, Anh3x4 = saved, PhiThuongNien = numPhi.Value });
            }
            catch { File.Delete(saved); throw; }
            txtSearch.Clear(); LoadData(); FormHelper.SelectId(gridHoSo, "MaHSBA", id);
            FormHelper.Success(this, "Đã đăng ký và cấp hồ sơ số " + id + ".");
        }
        private void GiaHan()
        {
            if (maHoSo == 0) throw new InvalidOperationException("Hãy chọn hồ sơ.");
            if (MessageBox.Show(this, "Gia hạn hồ sơ " + maHoSo + " với phí " + FormHelper.Money(numPhi.Value) + "?", "Gia hạn phí", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            int id = maHoSo; service.GiaHan(id, numPhi.Value); LoadData(); FormHelper.SelectId(gridHoSo, "MaHSBA", id);
            FormHelper.Success(this, "Đã gia hạn phí thêm một năm kể từ hôm nay.");
        }
    }
}
