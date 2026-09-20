using System;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Forms
{
    public partial class FormSach : Form
    {
        public FormSach()
        {
            InitializeComponent();
        }

        private void FormSach_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string sql = @"SELECT 
                                s.MaDauSach AS [Mã Sách], 
                                s.TenSach AS [Tên Sách], 
                                s.NamXuatBan AS [Năm XB], 
                                s.SoLuongHienCo AS [Số Lượng Khả Dụng], 
                                tl.TenTheLoai AS [Thể Loại]
                               FROM dbo.DauSach s
                               INNER JOIN dbo.TheLoai tl ON s.MaTheLoai = tl.MaTheLoai";

                DataTable dt = Db.Query(sql);
                dgvSach.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu sách: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}