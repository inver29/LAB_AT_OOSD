using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class ThuongPhatService
    {
        public DataTable GetAllQuyetDinhThuongPhat(string loaiQD = null, string trangThai = null, string search = null)
        {
            string sql = @"
                SELECT 
                    qd.MaQD,
                    qd.SoQD,
                    qd.LoaiQD,
                    qd.NgayLap,
                    qd.SoTien,
                    qd.CachThanhToan,
                    qd.KyApDung,
                    qd.TrangThai,
                    qd.NoiDung,
                    qd.MaNV,
                    nv.HoTen AS TenNhanVien,
                    qd.MaPhongBan,
                    pb.TenPhongBan,
                    qd.MaNguoiKy,
                    nk.HoTen AS TenNguoiKy,
                    qd.NgayKy,
                    CASE WHEN ptc.MaPhieu IS NOT NULL THEN N'Đã thu/chi' ELSE N'Chưa thu/chi' END AS TrangThaiThuChi
                FROM QuyetDinhThuongPhat qd
                LEFT JOIN NhanVien nv ON qd.MaNV = nv.MaNV
                LEFT JOIN PhongBan pb ON qd.MaPhongBan = pb.MaPhongBan
                LEFT JOIN NhanVien nk ON qd.MaNguoiKy = nk.MaNV
                LEFT JOIN PhieuThuChi ptc ON qd.MaQD = ptc.MaQD
                WHERE (1=1)";

            if (!string.IsNullOrWhiteSpace(loaiQD) && loaiQD != "Tất cả")
                sql += " AND qd.LoaiQD = @LoaiQD";
            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
                sql += " AND qd.TrangThai = @TrangThai";
            if (!string.IsNullOrWhiteSpace(search))
                sql += " AND (qd.SoQD LIKE @Search OR nv.HoTen LIKE @Search OR pb.TenPhongBan LIKE @Search)";

            sql += " ORDER BY qd.NgayLap DESC, qd.MaQD DESC";

            return Db.Query(sql,
                Db.P("@LoaiQD", loaiQD),
                Db.P("@TrangThai", trangThai),
                Db.P("@Search", "%" + search?.Trim() + "%"));
        }

        public DataTable GetChiTietPhanBo(int maQD)
        {
            string sql = @"
                SELECT 
                    ct.MaQD,
                    ct.MaNV,
                    nv.HoTen,
                    pb.TenPhongBan,
                    ct.SoTien,
                    ct.MaBangLuong
                FROM ChiTietQuyetDinh ct
                JOIN NhanVien nv ON ct.MaNV = nv.MaNV
                LEFT JOIN QuyetDinhTuyenDung qdtd ON nv.MaNV = qdtd.MaNV
                LEFT JOIN PhongBan pb ON qdtd.MaPhongBan = pb.MaPhongBan
                WHERE ct.MaQD = @MaQD
                ORDER BY ct.MaNV";
            return Db.Query(sql, Db.P("@MaQD", maQD));
        }

        public DataTable GetNhanVienByPhongBan(int maPhongBan)
        {
            string sql = @"
                SELECT nv.MaNV, nv.HoTen, nv.TrangThai
                FROM NhanVien nv
                JOIN QuyetDinhTuyenDung qd ON nv.MaNV = qd.MaNV
                WHERE qd.MaPhongBan = @MaPhongBan AND nv.TrangThai <> N'Kết thúc thử việc'
                ORDER BY nv.MaNV";
            return Db.Query(sql, Db.P("@MaPhongBan", maPhongBan));
        }

        /// <summary>
        /// Lập Quyết định Thưởng/Phạt (trạng thái ban đầu: Nháp).
        /// </summary>
        public int LapQuyetDinh(
            string soQD, string loaiQD, int? maNV, int? maPhongBan, DateTime ngayLap,
            string noiDung, decimal soTien, string cachThanhToan, DateTime? kyApDung,
            Dictionary<int, decimal> danhSachPhanBo = null)
        {
            if (string.IsNullOrWhiteSpace(soQD))
                throw new ArgumentException("Số quyết định không được để trống.");
            if (loaiQD != "Khen thưởng" && loaiQD != "Kỷ luật")
                throw new ArgumentException("Loại quyết định phải là 'Khen thưởng' hoặc 'Kỷ luật'.");
            if (soTien <= 0)
                throw new ArgumentException("Số tiền phải lớn hơn 0.");
            if (maNV.HasValue == maPhongBan.HasValue)
                throw new ArgumentException("Phải chọn đúng một đối tượng: Cá nhân hoặc Phòng ban.");
            if (cachThanhToan != "Trực tiếp" && cachThanhToan != "Qua lương")
                throw new ArgumentException("Cách thanh toán phải là 'Trực tiếp' hoặc 'Qua lương'.");

            DateTime? kyNormalized = null;
            if (cachThanhToan == "Qua lương")
            {
                if (!kyApDung.HasValue)
                    throw new ArgumentException("Thanh toán Qua lương bắt buộc phải chọn kỳ áp dụng.");
                // Chuẩn hóa về ngày mùng 1 đầu tháng theo ràng buộc DAY(KyApDung) = 1
                kyNormalized = new DateTime(kyApDung.Value.Year, kyApDung.Value.Month, 1);
            }

            return Db.Transaction((c, tx) =>
            {
                // Kiểm tra trùng SoQD
                string checkSoQD = "SELECT COUNT(*) FROM QuyetDinhThuongPhat WHERE SoQD = @SoQD";
                if (Convert.ToInt32(Db.Scalar(c, tx, checkSoQD, Db.P("@SoQD", soQD.Trim()))) > 0)
                    throw new InvalidOperationException($"Số quyết định '{soQD}' đã tồn tại.");

                if (cachThanhToan == "Qua lương")
                {
                    Db.AcquirePayrollLock(c, tx, kyNormalized.Value.Year, kyNormalized.Value.Month);
                    Db.EnsurePayrollNotClosed(c, tx, kyNormalized.Value.Year, kyNormalized.Value.Month);

                    // Kiểm tra phân bổ
                    if (maNV.HasValue)
                    {
                        danhSachPhanBo = new Dictionary<int, decimal> { { maNV.Value, soTien } };
                    }
                    else
                    {
                        if (danhSachPhanBo == null || danhSachPhanBo.Count == 0)
                            throw new ArgumentException("Quyết định tập thể qua lương phải có danh sách phân bổ cho các thành viên.");

                        decimal tongPhanBo = danhSachPhanBo.Values.Sum();
                        if (Math.Round(tongPhanBo, 2) != Math.Round(soTien, 2))
                        {
                            throw new InvalidOperationException($"Tổng tiền phân bổ ({tongPhanBo:N0}) không khớp với tổng tiền quyết định ({soTien:N0}).");
                        }
                    }
                }

                // Insert QuyetDinhThuongPhat
                string sqlInsertQD = @"
                    INSERT INTO QuyetDinhThuongPhat 
                    (SoQD, LoaiQD, MaNV, MaPhongBan, NgayLap, NoiDung, SoTien, CachThanhToan, KyApDung, TrangThai, MaNguoiKy, NgayKy)
                    VALUES 
                    (@SoQD, @LoaiQD, @MaNV, @MaPhongBan, @NgayLap, @NoiDung, @SoTien, @CachThanhToan, @KyApDung, N'Nháp', NULL, NULL);
                    SELECT SCOPE_IDENTITY();";

                int maQD = Convert.ToInt32(Db.Scalar(c, tx, sqlInsertQD,
                    Db.P("@SoQD", soQD.Trim()),
                    Db.P("@LoaiQD", loaiQD),
                    Db.P("@MaNV", (object)maNV ?? DBNull.Value),
                    Db.P("@MaPhongBan", (object)maPhongBan ?? DBNull.Value),
                    Db.P("@NgayLap", ngayLap.Date),
                    Db.P("@NoiDung", noiDung ?? string.Empty),
                    Db.P("@SoTien", soTien),
                    Db.P("@CachThanhToan", cachThanhToan),
                    Db.P("@KyApDung", (object)kyNormalized?.Date ?? DBNull.Value)));

                // Nếu Qua lương: Lưu ChiTietQuyetDinh
                if (cachThanhToan == "Qua lương" && danhSachPhanBo != null)
                {
                    foreach (var kvp in danhSachPhanBo)
                    {
                        if (kvp.Value <= 0)
                            throw new ArgumentException($"Số tiền phân bổ cho nhân viên {kvp.Key} phải lớn hơn 0.");

                        string sqlInsertCT = "INSERT INTO ChiTietQuyetDinh (MaQD, MaNV, SoTien, MaBangLuong) VALUES (@MaQD, @MaNV, @SoTien, NULL)";
                        Db.Execute(c, tx, sqlInsertCT,
                            Db.P("@MaQD", maQD),
                            Db.P("@MaNV", kvp.Key),
                            Db.P("@SoTien", kvp.Value));
                    }
                }

                return maQD;
            });
        }

        /// <summary>
        /// Giám đốc ký quyết định thưởng / phạt.
        /// Sau khi ký, nội dung và phân bổ bị khóa; chỉ MaBangLuong được cập nhật khi chốt bảng lương.
        /// </summary>
        public void KyQuyetDinh(int maQD, int maNguoiKy, DateTime ngayKy)
        {
            if (ngayKy.Date > DateTime.Today)
                throw new ArgumentException("Ngày ký không được nằm trong tương lai.");

            Db.Transaction((c, tx) =>
            {
                var dt = Db.Query(c, tx, "SELECT NgayLap, CachThanhToan, KyApDung, TrangThai FROM QuyetDinhThuongPhat WITH (UPDLOCK) WHERE MaQD = @MaQD", Db.P("@MaQD", maQD));
                if (dt.Rows.Count == 0) throw new InvalidOperationException("Quyết định không tồn tại.");

                string trangThai = dt.Rows[0]["TrangThai"].ToString();
                if (trangThai == "Đã ký")
                    throw new InvalidOperationException("Quyết định này đã được ký trước đó.");

                DateTime ngayLap = Convert.ToDateTime(dt.Rows[0]["NgayLap"]);
                if (ngayKy.Date < ngayLap.Date)
                    throw new InvalidOperationException($"Ngày ký ({ngayKy:dd/MM/yyyy}) không được trước ngày lập quyết định ({ngayLap:dd/MM/yyyy}).");

                string cachThanhToan = dt.Rows[0]["CachThanhToan"].ToString();
                if (cachThanhToan == "Qua lương")
                {
                    DateTime ky = Convert.ToDateTime(dt.Rows[0]["KyApDung"]);
                    Db.AcquirePayrollLock(c, tx, ky.Year, ky.Month);
                    Db.EnsurePayrollNotClosed(c, tx, ky.Year, ky.Month);
                }

                string sql = "UPDATE QuyetDinhThuongPhat SET TrangThai = N'Đã ký', MaNguoiKy = @MaNguoiKy, NgayKy = @NgayKy WHERE MaQD = @MaQD";
                Db.Execute(c, tx, sql,
                    Db.P("@MaNguoiKy", maNguoiKy),
                    Db.P("@NgayKy", ngayKy.Date),
                    Db.P("@MaQD", maQD));
            });
        }

        public void XoaQuyetDinhNhap(int maQD)
        {
            Db.Transaction((c, tx) =>
            {
                var dt = Db.Query(c, tx, "SELECT TrangThai, CachThanhToan, KyApDung FROM QuyetDinhThuongPhat WITH (UPDLOCK) WHERE MaQD = @MaQD", Db.P("@MaQD", maQD));
                if (dt.Rows.Count == 0) throw new InvalidOperationException("Quyết định không tồn tại.");

                string trangThai = dt.Rows[0]["TrangThai"].ToString();
                if (trangThai == "Đã ký")
                    throw new InvalidOperationException("Không thể xóa quyết định đã được ký. Quyết định đã ký bị khóa vĩnh viễn.");

                Db.Execute(c, tx, "DELETE FROM ChiTietQuyetDinh WHERE MaQD = @MaQD", Db.P("@MaQD", maQD));
                Db.Execute(c, tx, "DELETE FROM QuyetDinhThuongPhat WHERE MaQD = @MaQD", Db.P("@MaQD", maQD));
            });
        }
    }
}
