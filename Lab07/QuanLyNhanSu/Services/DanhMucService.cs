using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class DanhMucService
    {
        // ======================= PHÒNG BAN =======================
        public DataTable GetAllPhongBan()
        {
            return Db.Query("SELECT MaPhongBan, TenPhongBan FROM PhongBan ORDER BY MaPhongBan");
        }

        public void AddPhongBan(string tenPhongBan)
        {
            if (string.IsNullOrWhiteSpace(tenPhongBan))
                throw new ArgumentException("Tên phòng ban không được để trống.");

            string sql = "INSERT INTO PhongBan (TenPhongBan) VALUES (@TenPhongBan)";
            Db.Execute(Db.Open(), null, sql, Db.P("@TenPhongBan", tenPhongBan.Trim()));
        }

        public void UpdatePhongBan(int maPhongBan, string tenPhongBan)
        {
            if (string.IsNullOrWhiteSpace(tenPhongBan))
                throw new ArgumentException("Tên phòng ban không được để trống.");

            string sql = "UPDATE PhongBan SET TenPhongBan = @TenPhongBan WHERE MaPhongBan = @MaPhongBan";
            Db.Execute(Db.Open(), null, sql, Db.P("@TenPhongBan", tenPhongBan.Trim()), Db.P("@MaPhongBan", maPhongBan));
        }

        public void DeletePhongBan(int maPhongBan)
        {
            // Kiểm tra tham chiếu
            string sqlCheck = @"SELECT 
                (SELECT COUNT(*) FROM QuyetDinhTuyenDung WHERE MaPhongBan = @MaPhongBan) +
                (SELECT COUNT(*) FROM QuyetDinhThuongPhat WHERE MaPhongBan = @MaPhongBan)";
            int refCount = Convert.ToInt32(Db.Scalar(Db.Open(), null, sqlCheck, Db.P("@MaPhongBan", maPhongBan)));
            if (refCount > 0)
                throw new InvalidOperationException("Không thể xóa phòng ban đang có quyết định tuyển dụng hoặc thưởng phạt tham chiếu.");

            string sql = "DELETE FROM PhongBan WHERE MaPhongBan = @MaPhongBan";
            Db.Execute(Db.Open(), null, sql, Db.P("@MaPhongBan", maPhongBan));
        }

        // ======================= CHỨC VỤ =======================
        public DataTable GetAllChucVu()
        {
            return Db.Query("SELECT MaChucVu, TenChucVu FROM ChucVu ORDER BY MaChucVu");
        }

        public void AddChucVu(string tenChucVu)
        {
            if (string.IsNullOrWhiteSpace(tenChucVu))
                throw new ArgumentException("Tên chức vụ không được để trống.");

            string sql = "INSERT INTO ChucVu (TenChucVu) VALUES (@TenChucVu)";
            Db.Execute(Db.Open(), null, sql, Db.P("@TenChucVu", tenChucVu.Trim()));
        }

        public void UpdateChucVu(int maChucVu, string tenChucVu)
        {
            if (string.IsNullOrWhiteSpace(tenChucVu))
                throw new ArgumentException("Tên chức vụ không được để trống.");

            string sql = "UPDATE ChucVu SET TenChucVu = @TenChucVu WHERE MaChucVu = @MaChucVu";
            Db.Execute(Db.Open(), null, sql, Db.P("@TenChucVu", tenChucVu.Trim()), Db.P("@MaChucVu", maChucVu));
        }

        public void DeleteChucVu(int maChucVu)
        {
            string sqlCheck = "SELECT COUNT(*) FROM QuyetDinhChucVu WHERE MaChucVu = @MaChucVu";
            int refCount = Convert.ToInt32(Db.Scalar(Db.Open(), null, sqlCheck, Db.P("@MaChucVu", maChucVu)));
            if (refCount > 0)
                throw new InvalidOperationException("Không thể xóa chức vụ đang có quyết định chức vụ tham chiếu.");

            string sql = "DELETE FROM ChucVu WHERE MaChucVu = @MaChucVu";
            Db.Execute(Db.Open(), null, sql, Db.P("@MaChucVu", maChucVu));
        }

        // ======================= CA LÀM VIỆC =======================
        public DataTable GetAllCaLamViec()
        {
            return Db.Query("SELECT MaCa, TenCa, GioBatDau, GioKetThuc FROM CaLamViec ORDER BY GioBatDau");
        }

        public void AddCaLamViec(string tenCa, TimeSpan gioBatDau, TimeSpan gioKetThuc)
        {
            if (string.IsNullOrWhiteSpace(tenCa))
                throw new ArgumentException("Tên ca không được để trống.");
            if (gioBatDau >= gioKetThuc)
                throw new ArgumentException("Giờ bắt đầu phải nhỏ hơn giờ kết thúc (không qua đêm).");

            string sql = "INSERT INTO CaLamViec (TenCa, GioBatDau, GioKetThuc) VALUES (@TenCa, @GioBatDau, @GioKetThuc)";
            Db.Execute(Db.Open(), null, sql, 
                Db.P("@TenCa", tenCa.Trim()), 
                Db.P("@GioBatDau", gioBatDau), 
                Db.P("@GioKetThuc", gioKetThuc));
        }

        public void UpdateCaLamViec(int maCa, string tenCa, TimeSpan gioBatDau, TimeSpan gioKetThuc)
        {
            if (string.IsNullOrWhiteSpace(tenCa))
                throw new ArgumentException("Tên ca không được để trống.");
            if (gioBatDau >= gioKetThuc)
                throw new ArgumentException("Giờ bắt đầu phải nhỏ hơn giờ kết thúc (không qua đêm).");

            // Quy tắc: Không sửa giờ ca đã được phân trong ChamCong; chỉ đổi tên hoặc thêm ca mới
            string sqlCheck = "SELECT COUNT(*) FROM ChamCong WHERE MaCa = @MaCa";
            int assignedCount = Convert.ToInt32(Db.Scalar(Db.Open(), null, sqlCheck, Db.P("@MaCa", maCa)));
            if (assignedCount > 0)
            {
                // Kiểm tra xem giờ có bị thay đổi không
                string sqlGetHours = "SELECT GioBatDau, GioKetThuc FROM CaLamViec WHERE MaCa = @MaCa";
                var dt = Db.Query(sqlGetHours, Db.P("@MaCa", maCa));
                if (dt.Rows.Count > 0)
                {
                    TimeSpan oldStart = (TimeSpan)dt.Rows[0]["GioBatDau"];
                    TimeSpan oldEnd = (TimeSpan)dt.Rows[0]["GioKetThuc"];
                    if (oldStart != gioBatDau || oldEnd != gioKetThuc)
                    {
                        throw new InvalidOperationException("Ca làm việc này đã được phân công chấm công. Theo quy định, không được sửa giờ ca đã phân; chỉ được đổi tên hoặc thêm ca mới.");
                    }
                }
            }

            string sql = "UPDATE CaLamViec SET TenCa = @TenCa, GioBatDau = @GioBatDau, GioKetThuc = @GioKetThuc WHERE MaCa = @MaCa";
            Db.Execute(Db.Open(), null, sql, 
                Db.P("@TenCa", tenCa.Trim()), 
                Db.P("@GioBatDau", gioBatDau), 
                Db.P("@GioKetThuc", gioKetThuc), 
                Db.P("@MaCa", maCa));
        }

        public void DeleteCaLamViec(int maCa)
        {
            string sqlCheck = "SELECT COUNT(*) FROM ChamCong WHERE MaCa = @MaCa";
            int refCount = Convert.ToInt32(Db.Scalar(Db.Open(), null, sqlCheck, Db.P("@MaCa", maCa)));
            if (refCount > 0)
                throw new InvalidOperationException("Không thể xóa ca làm việc đã có phân công chấm công.");

            string sql = "DELETE FROM CaLamViec WHERE MaCa = @MaCa";
            Db.Execute(Db.Open(), null, sql, Db.P("@MaCa", maCa));
        }
    }
}
