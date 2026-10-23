using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class QuyetDinhNhanSuService
    {
        public DataTable GetLichSuLuongByNhanVien(int maNV)
        {
            string sql = @"
                SELECT qdl.*, nv.HoTen 
                FROM QuyetDinhLuong qdl
                JOIN NhanVien nv ON qdl.MaNV = nv.MaNV
                WHERE qdl.MaNV = @MaNV 
                ORDER BY qdl.TuNgay DESC, qdl.MaQDL DESC";
            return Db.Query(sql, Db.P("@MaNV", maNV));
        }

        public DataTable GetAllQuyetDinhLuong(string search = null)
        {
            string sql = @"
                SELECT qdl.*, nv.HoTen, pb.TenPhongBan
                FROM QuyetDinhLuong qdl
                JOIN NhanVien nv ON qdl.MaNV = nv.MaNV
                LEFT JOIN QuyetDinhTuyenDung qdtd ON nv.MaNV = qdtd.MaNV
                LEFT JOIN PhongBan pb ON qdtd.MaPhongBan = pb.MaPhongBan
                WHERE (1=1)";
            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND (nv.HoTen LIKE @Search OR qdl.SoQD LIKE @Search)";
            }
            sql += " ORDER BY qdl.TuNgay DESC, qdl.MaQDL DESC";
            return Db.Query(sql, Db.P("@Search", "%" + search?.Trim() + "%"));
        }

        public DataTable GetLichSuChucVuByNhanVien(int maNV)
        {
            string sql = @"
                SELECT qdcv.*, nv.HoTen, cv.TenChucVu
                FROM QuyetDinhChucVu qdcv
                JOIN NhanVien nv ON qdcv.MaNV = nv.MaNV
                JOIN ChucVu cv ON qdcv.MaChucVu = cv.MaChucVu
                WHERE qdcv.MaNV = @MaNV 
                ORDER BY qdcv.TuNgay DESC, qdcv.MaQDCV DESC";
            return Db.Query(sql, Db.P("@MaNV", maNV));
        }

        public DataTable GetAllQuyetDinhChucVu(string search = null)
        {
            string sql = @"
                SELECT qdcv.*, nv.HoTen, cv.TenChucVu, pb.TenPhongBan
                FROM QuyetDinhChucVu qdcv
                JOIN NhanVien nv ON qdcv.MaNV = nv.MaNV
                JOIN ChucVu cv ON qdcv.MaChucVu = cv.MaChucVu
                LEFT JOIN QuyetDinhTuyenDung qdtd ON nv.MaNV = qdtd.MaNV
                LEFT JOIN PhongBan pb ON qdtd.MaPhongBan = pb.MaPhongBan
                WHERE (1=1)";
            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND (nv.HoTen LIKE @Search OR qdcv.SoQD LIKE @Search OR cv.TenChucVu LIKE @Search)";
            }
            sql += " ORDER BY qdcv.TuNgay DESC, qdcv.MaQDCV DESC";
            return Db.Query(sql, Db.P("@Search", "%" + search?.Trim() + "%"));
        }

        /// <summary>
        /// Thêm Quyết định lương mới:
        /// Tự động kết thúc bản ghi lương đang mở vào ngày liền trước (newTuNgay - 1).
        /// Không cho sửa căn cứ của kỳ lương đã chốt.
        /// </summary>
        public void AddQuyetDinhLuong(
            string soQD, int maNV, DateTime ngayQD, DateTime tuNgay, DateTime? denNgay,
            decimal luongCoBan, decimal phuCapLuong, decimal phuCapDocHai)
        {
            if (string.IsNullOrWhiteSpace(soQD))
                throw new ArgumentException("Số quyết định không được để trống.");
            if (luongCoBan <= 0)
                throw new ArgumentException("Lương cơ bản phải lớn hơn 0.");
            if (phuCapLuong < 0 || phuCapDocHai < 0)
                throw new ArgumentException("Phụ cấp không được âm.");
            if (ngayQD.Date > tuNgay.Date)
                throw new ArgumentException("Ngày quyết định không được sau ngày bắt đầu hiệu lực.");
            if (denNgay.HasValue && denNgay.Value.Date < tuNgay.Date)
                throw new ArgumentException("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");

            Db.Transaction((c, tx) =>
            {
                // Kiểm tra trùng SoQD
                string checkSoQD = "SELECT COUNT(*) FROM QuyetDinhLuong WHERE SoQD = @SoQD";
                if (Convert.ToInt32(Db.Scalar(c, tx, checkSoQD, Db.P("@SoQD", soQD.Trim()))) > 0)
                    throw new InvalidOperationException($"Số quyết định '{soQD}' đã tồn tại.");

                // Kiểm tra kỳ lương của TuNgay có bị chốt chưa
                Db.EnsureDateNotInClosedPayroll(c, tx, tuNgay.Date);

                // Khóa kiểm tra các quyết định lương hiện có của nhân viên
                string sqlCurrent = @"
                    SELECT TOP 1 MaQDL, TuNgay, DenNgay 
                    FROM QuyetDinhLuong WITH (UPDLOCK)
                    WHERE MaNV = @MaNV AND (DenNgay IS NULL OR DenNgay >= @TuNgay)
                    ORDER BY TuNgay DESC";
                var dtOld = Db.Query(c, tx, sqlCurrent, Db.P("@MaNV", maNV), Db.P("@TuNgay", tuNgay.Date));

                if (dtOld.Rows.Count > 0)
                {
                    int oldId = Convert.ToInt32(dtOld.Rows[0]["MaQDL"]);
                    DateTime oldTuNgay = Convert.ToDateTime(dtOld.Rows[0]["TuNgay"]);

                    if (tuNgay.Date <= oldTuNgay.Date)
                    {
                        throw new InvalidOperationException($"Ngày bắt đầu của quyết định mới ({tuNgay:dd/MM/yyyy}) phải sau ngày bắt đầu của quyết định trước ({oldTuNgay:dd/MM/yyyy}).");
                    }

                    DateTime newOldDenNgay = tuNgay.Date.AddDays(-1);
                    // Cập nhật kết thúc bản ghi cũ vào ngày liền trước
                    string sqlUpdateOld = "UPDATE QuyetDinhLuong SET DenNgay = @DenNgay WHERE MaQDL = @MaQDL";
                    Db.Execute(c, tx, sqlUpdateOld, Db.P("@DenNgay", newOldDenNgay), Db.P("@MaQDL", oldId));
                }

                // Thêm bản ghi mới
                string sqlInsert = @"
                    INSERT INTO QuyetDinhLuong (SoQD, MaNV, NgayQD, TuNgay, DenNgay, LuongCoBan, PhuCapLuong, PhuCapDocHai)
                    VALUES (@SoQD, @MaNV, @NgayQD, @TuNgay, @DenNgay, @LuongCoBan, @PhuCapLuong, @PhuCapDocHai)";
                Db.Execute(c, tx, sqlInsert,
                    Db.P("@SoQD", soQD.Trim()),
                    Db.P("@MaNV", maNV),
                    Db.P("@NgayQD", ngayQD.Date),
                    Db.P("@TuNgay", tuNgay.Date),
                    Db.P("@DenNgay", (object)denNgay?.Date ?? DBNull.Value),
                    Db.P("@LuongCoBan", luongCoBan),
                    Db.P("@PhuCapLuong", phuCapLuong),
                    Db.P("@PhuCapDocHai", phuCapDocHai));
            });
        }

        /// <summary>
        /// Thêm Quyết định chức vụ mới:
        /// Tự động kết thúc bản ghi chức vụ đang mở vào ngày liền trước (newTuNgay - 1).
        /// </summary>
        public void AddQuyetDinhChucVu(
            string soQD, int maNV, int maChucVu, DateTime ngayQD, DateTime tuNgay, DateTime? denNgay,
            decimal phuCapChucVu)
        {
            if (string.IsNullOrWhiteSpace(soQD))
                throw new ArgumentException("Số quyết định không được để trống.");
            if (phuCapChucVu < 0)
                throw new ArgumentException("Phụ cấp chức vụ không được âm.");
            if (ngayQD.Date > tuNgay.Date)
                throw new ArgumentException("Ngày quyết định không được sau ngày bắt đầu hiệu lực.");
            if (denNgay.HasValue && denNgay.Value.Date < tuNgay.Date)
                throw new ArgumentException("Ngày kết thúc không được nhỏ hơn ngày bắt đầu.");

            Db.Transaction((c, tx) =>
            {
                // Kiểm tra trùng SoQD
                string checkSoQD = "SELECT COUNT(*) FROM QuyetDinhChucVu WHERE SoQD = @SoQD";
                if (Convert.ToInt32(Db.Scalar(c, tx, checkSoQD, Db.P("@SoQD", soQD.Trim()))) > 0)
                    throw new InvalidOperationException($"Số quyết định '{soQD}' đã tồn tại.");

                // Kiểm tra kỳ lương của TuNgay có bị chốt chưa
                Db.EnsureDateNotInClosedPayroll(c, tx, tuNgay.Date);

                // Khóa kiểm tra các quyết định chức vụ hiện có
                string sqlCurrent = @"
                    SELECT TOP 1 MaQDCV, TuNgay, DenNgay 
                    FROM QuyetDinhChucVu WITH (UPDLOCK)
                    WHERE MaNV = @MaNV AND (DenNgay IS NULL OR DenNgay >= @TuNgay)
                    ORDER BY TuNgay DESC";
                var dtOld = Db.Query(c, tx, sqlCurrent, Db.P("@MaNV", maNV), Db.P("@TuNgay", tuNgay.Date));

                if (dtOld.Rows.Count > 0)
                {
                    int oldId = Convert.ToInt32(dtOld.Rows[0]["MaQDCV"]);
                    DateTime oldTuNgay = Convert.ToDateTime(dtOld.Rows[0]["TuNgay"]);

                    if (tuNgay.Date <= oldTuNgay.Date)
                    {
                        throw new InvalidOperationException($"Ngày bắt đầu của quyết định mới ({tuNgay:dd/MM/yyyy}) phải sau ngày bắt đầu của quyết định chức vụ trước ({oldTuNgay:dd/MM/yyyy}).");
                    }

                    DateTime newOldDenNgay = tuNgay.Date.AddDays(-1);
                    string sqlUpdateOld = "UPDATE QuyetDinhChucVu SET DenNgay = @DenNgay WHERE MaQDCV = @MaQDCV";
                    Db.Execute(c, tx, sqlUpdateOld, Db.P("@DenNgay", newOldDenNgay), Db.P("@MaQDCV", oldId));
                }

                // Thêm bản ghi chức vụ mới
                string sqlInsert = @"
                    INSERT INTO QuyetDinhChucVu (SoQD, MaNV, MaChucVu, NgayQD, TuNgay, DenNgay, PhuCapChucVu)
                    VALUES (@SoQD, @MaNV, @MaChucVu, @NgayQD, @TuNgay, @DenNgay, @PhuCapChucVu)";
                Db.Execute(c, tx, sqlInsert,
                    Db.P("@SoQD", soQD.Trim()),
                    Db.P("@MaNV", maNV),
                    Db.P("@MaChucVu", maChucVu),
                    Db.P("@NgayQD", ngayQD.Date),
                    Db.P("@TuNgay", tuNgay.Date),
                    Db.P("@DenNgay", (object)denNgay?.Date ?? DBNull.Value),
                    Db.P("@PhuCapChucVu", phuCapChucVu));
            });
        }
    }
}
