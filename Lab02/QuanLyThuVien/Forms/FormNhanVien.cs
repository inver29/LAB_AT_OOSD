using System;
using System.Data;
using System.Windows.Forms;
using QuanLyThuVien.Data;

namespace QuanLyThuVien.Forms
{
    public partial class FormNhanVien : Form
    {
        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string sql = @"SELECT 
                                MaNhanVien AS [Mã NV], 
                                Ho + ' ' + Ten AS [Họ Tên], 
                                Phai AS [Giới Tính], 
                                CONVERT(VARCHAR(10), NgaySinh, 103) AS [Ngày Sinh], 
                                ChucVu AS [Chức Vụ], 
                                SoDienThoai AS [SĐT]
                               FROM dbo.NhanVien";

                DataTable dt = Db.Query(sql);
                dgvNhanVien.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy dữ liệu nhân viên: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}