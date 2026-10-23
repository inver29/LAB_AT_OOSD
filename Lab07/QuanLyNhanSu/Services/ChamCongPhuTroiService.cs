using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class ChamCongPhuTroiService
    {
        // ======================= PHÂN CA & CHẤM CÔNG =======================

        public DataTable GetBangChamCong(DateTime fromDate, DateTime toDate, int? maPhongBan = null, int? maNV = null)
        {
            string sql = @"
                SELECT 
                    cc.MaNV,
                    nv.HoTen,
                    pb.TenPhongBan,
                    cc.Ngay,
                    cc.MaCa,
                    ca.TenCa,
                    ca.GioBatDau,
                    ca.GioKetThuc,
                    CAST(DATEDIFF(MINUTE, ca.GioBatDau, ca.GioKetThuc) / 60.0 AS decimal(5,2)) AS GioQuyDinh,
                    cc.SoGioThucTe,
                    CASE 
                        WHEN cc.SoGioThucTe IS NULL THEN N'Chưa chấm'
                        WHEN cc.SoGioThucTe = 0 THEN N'Vắng mặt'
                        ELSE N'Đã làm'
                    END AS TrangThaiChamCong
                FROM ChamCong cc
                JOIN NhanVien nv ON cc.MaNV = nv.MaNV
                JOIN CaLamViec ca ON cc.MaCa = ca.MaCa
                LEFT JOIN QuyetDinhTuyenDung qdtd ON nv.MaNV = qdtd.MaNV
                LEFT JOIN PhongBan pb ON qdtd.MaPhongBan = pb.MaPhongBan
                WHERE cc.Ngay BETWEEN @FromDate AND @ToDate";

            if (maPhongBan.HasValue && maPhongBan.Value > 0)
                sql += " AND pb.MaPhongBan = @MaPhongBan";
            if (maNV.HasValue && maNV.Value > 0)
                sql += " AND cc.MaNV = @MaNV";

            sql += " ORDER BY cc.Ngay DESC, nv.MaNV, ca.GioBatDau";

            return Db.Query(sql,
                Db.P("@FromDate", fromDate.Date),
                Db.P("@ToDate", toDate.Date),
                Db.P("@MaPhongBan", (object)maPhongBan ?? DBNull.Value),
                Db.P("@MaNV", (object)maNV ?? DBNull.Value));
        }

        public void PhanCa(int maNV, DateTime ngay, int maCa)
        {
            Db.Transaction((c, tx) =>
            {
                // Khóa kỳ lương và kiểm tra kỳ lương chưa chốt
                Db.AcquirePayrollLock(c, tx, ngay.Year, ngay.Month);
                Db.EnsureDateNotInClosedPayroll(c, tx, ngay);

                // 1. Kiểm tra ngày nằm trong thời gian thử việc hoặc hợp đồng
                string sqlCheckActive = @"
                    SELECT 
                        (SELECT COUNT(*) FROM QuyetDinhTuyenDung WHERE MaNV = @MaNV AND TuNgay <= @Ngay AND DenNgay >= @Ngay) +
                        (SELECT COUNT(*) FROM HopDong WHERE MaNV = @MaNV AND TuNgay <= @Ngay AND (DenNgay IS NULL OR DenNgay >= @Ngay))";
                int activeCount = Convert.ToInt32(Db.Scalar(c, tx, sqlCheckActive, Db.P("@MaNV", maNV), Db.P("@Ngay", ngay.Date)));
                if (activeCount == 0)
                {
                    throw new InvalidOperationException($"Nhân viên {maNV} không có quyết định thử việc hoặc hợp đồng còn hiệu lực vào ngày {ngay:dd/MM/yyyy}.");
                }

                // 2. Lấy giờ ca cần phân
                var dtCa = Db.Query(c, tx, "SELECT GioBatDau, GioKetThuc FROM CaLamViec WHERE MaCa = @MaCa", Db.P("@MaCa", maCa));
                if (dtCa.Rows.Count == 0) throw new InvalidOperationException("Ca làm việc không tồn tại.");
                TimeSpan start = (TimeSpan)dtCa.Rows[0]["GioBatDau"];
                TimeSpan end = (TimeSpan)dtCa.Rows[0]["GioKetThuc"];

                // 3. Kiểm tra trùng/chồng với ca khác cùng ngày của nhân viên [start, end)
                string sqlCheckShiftOverlap = @"
                    SELECT COUNT(*) FROM ChamCong cc
                    JOIN CaLamViec ca ON cc.MaCa = ca.MaCa
                    WHERE cc.MaNV = @MaNV AND cc.Ngay = @Ngay
                      AND (ca.GioBatDau < @End AND @Start < ca.GioKetThuc)";
                int shiftOverlap = Convert.ToInt32(Db.Scalar(c, tx, sqlCheckShiftOverlap,
                    Db.P("@MaNV", maNV),
                    Db.P("@Ngay", ngay.Date),
                    Db.P("@Start", start),
                    Db.P("@End", end)));
                if (shiftOverlap > 0)
                {
                    throw new InvalidOperationException("Ca làm việc bị trùng hoặc chồng giờ với ca đã được phân trong ngày của nhân viên.");
                }

                // 4. Kiểm tra chồng với phụ trội cùng ngày của nhân viên
                string sqlCheckOtOverlap = @"
                    SELECT COUNT(*) FROM ChiTietPhuTroi ct
                    JOIN PhieuPhuTroi p ON ct.MaPhieuPT = p.MaPhieuPT
                    WHERE ct.MaNV = @MaNV AND p.NgayPhuTroi = @Ngay
                      AND (p.GioBatDau < @End AND @Start < DATEADD(SECOND, CAST(p.SoGio * 3600 AS int), CAST(p.GioBatDau AS time)))";
                int otOverlap = Convert.ToInt32(Db.Scalar(c, tx, sqlCheckOtOverlap,
                    Db.P("@MaNV", maNV),
                    Db.P("@Ngay", ngay.Date),
                    Db.P("@Start", start),
                    Db.P("@End", end)));
                if (otOverlap > 0)
                {
                    throw new InvalidOperationException("Ca làm việc bị chồng giờ với thời gian phụ trội đã lập của nhân viên trong ngày.");
                }

                // 5. Thêm ChamCong với SoGioThucTe = NULL (chưa chấm)
                string sqlInsert = "INSERT INTO ChamCong (MaNV, Ngay, MaCa, SoGioThucTe) VALUES (@MaNV, @Ngay, @MaCa, NULL)";
                Db.Execute(c, tx, sqlInsert, Db.P("@MaNV", maNV), Db.P("@Ngay", ngay.Date), Db.P("@MaCa", maCa));
            });
        }

        public void XoaPhanCa(int maNV, DateTime ngay, int maCa)
        {
            Db.Transaction((c, tx) =>
            {
                Db.AcquirePayrollLock(c, tx, ngay.Year, ngay.Month);
                Db.EnsureDateNotInClosedPayroll(c, tx, ngay);

                string sql = "DELETE FROM ChamCong WHERE MaNV = @MaNV AND Ngay = @Ngay AND MaCa = @MaCa";
                Db.Execute(c, tx, sql, Db.P("@MaNV", maNV), Db.P("@Ngay", ngay.Date), Db.P("@MaCa", maCa));
            });
        }

        public void ChamCongThucTe(int maNV, DateTime ngay, int maCa, decimal? soGioThucTe)
        {
            // Kiểm tra không chấm công tương lai
            if (ngay.Date > DateTime.Today)
            {
                throw new InvalidOperationException("Không được chấm công cho ngày trong tương lai.");
            }

            Db.Transaction((c, tx) =>
            {
                Db.AcquirePayrollLock(c, tx, ngay.Year, ngay.Month);
                Db.EnsureDateNotInClosedPayroll(c, tx, ngay);

                if (soGioThucTe.HasValue)
                {
                    if (soGioThucTe.Value < 0)
                        throw new ArgumentException("Số giờ thực tế không được âm (0 là vắng mặt).");

                    // Lấy thời lượng ca
                    var dtCa = Db.Query(c, tx, "SELECT GioBatDau, GioKetThuc FROM CaLamViec WHERE MaCa = @MaCa", Db.P("@MaCa", maCa));
                    if (dtCa.Rows.Count == 0) throw new InvalidOperationException("Ca làm việc không tồn tại.");
                    TimeSpan start = (TimeSpan)dtCa.Rows[0]["GioBatDau"];
                    TimeSpan end = (TimeSpan)dtCa.Rows[0]["GioKetThuc"];
                    decimal maxHours = (decimal)(end - start).TotalHours;

                    if (soGioThucTe.Value > maxHours)
                    {
                        throw new ArgumentException($"Số giờ làm thực tế ({soGioThucTe.Value}h) không được vượt quá thời lượng ca quy định ({maxHours}h).");
                    }
                }

                string sql = "UPDATE ChamCong SET SoGioThucTe = @SoGioThucTe WHERE MaNV = @MaNV AND Ngay = @Ngay AND MaCa = @MaCa";
                Db.Execute(c, tx, sql,
                    Db.P("@SoGioThucTe", (object)soGioThucTe ?? DBNull.Value),
                    Db.P("@MaNV", maNV),
                    Db.P("@Ngay", ngay.Date),
                    Db.P("@MaCa", maCa));
            });
        }

        // ======================= PHIẾU PHỤ TRỘI =======================

        public DataTable GetAllPhieuPhuTroi(DateTime fromDate, DateTime toDate, string trangThai = null)
        {
            string sql = @"
                SELECT 
                    p.MaPhieuPT,
                    p.SoPhieu,
                    p.NgayPhuTroi,
                    p.GioBatDau,
                    p.SoGio,
                    p.HinhThuc,
                    p.TrangThai,
                    COUNT(ct.MaNV) AS SoNguoiThamGia
                FROM PhieuPhuTroi p
                LEFT JOIN ChiTietPhuTroi ct ON p.MaPhieuPT = ct.MaPhieuPT
                WHERE p.NgayPhuTroi BETWEEN @FromDate AND @ToDate";

            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
            {
                sql += " AND p.TrangThai = @TrangThai";
            }
            sql += " GROUP BY p.MaPhieuPT, p.SoPhieu, p.NgayPhuTroi, p.GioBatDau, p.SoGio, p.HinhThuc, p.TrangThai ORDER BY p.NgayPhuTroi DESC, p.MaPhieuPT DESC";

            return Db.Query(sql, Db.P("@FromDate", fromDate.Date), Db.P("@ToDate", toDate.Date), Db.P("@TrangThai", trangThai));
        }

        public DataTable GetNhanVienByPhieuPhuTroi(int maPhieuPT)
        {
            string sql = @"
                SELECT nv.MaNV, nv.HoTen, pb.TenPhongBan
                FROM ChiTietPhuTroi ct
                JOIN NhanVien nv ON ct.MaNV = nv.MaNV
                LEFT JOIN QuyetDinhTuyenDung qdtd ON nv.MaNV = qdtd.MaNV
                LEFT JOIN PhongBan pb ON qdtd.MaPhongBan = pb.MaPhongBan
                WHERE ct.MaPhieuPT = @MaPhieuPT
                ORDER BY nv.MaNV";
            return Db.Query(sql, Db.P("@MaPhieuPT", maPhieuPT));
        }

        public void TaoPhieuPhuTroi(
            string soPhieu, DateTime ngayPT, TimeSpan gioBatDau, decimal soGio,
            string hinhThuc, List<int> danhSachMaNV)
        {
            if (string.IsNullOrWhiteSpace(soPhieu))
                throw new ArgumentException("Số phiếu phụ trội không được để trống.");
            if (soGio <= 0 || soGio > 24)
                throw new ArgumentException("Số giờ phụ trội phải lớn hơn 0 và không quá 24 giờ.");
            if (gioBatDau.TotalSeconds + (double)soGio * 3600 > 86400)
                throw new ArgumentException("Thời gian phụ trội vượt quá 24:00 trong ngày (không làm qua đêm).");
            if (hinhThuc != "Tăng ca" && hinhThuc != "Ngày lễ" && hinhThuc != "Ngày nghỉ")
                throw new ArgumentException("Hình thức phụ trội phải là 'Tăng ca', 'Ngày lễ' hoặc 'Ngày nghỉ'.");
            if (danhSachMaNV == null || danhSachMaNV.Count == 0)
                throw new ArgumentException("Danh sách phụ trội phải có ít nhất một nhân viên.");

            List<int> distinctNV = danhSachMaNV.Distinct().ToList();

            Db.Transaction((c, tx) =>
            {
                Db.AcquirePayrollLock(c, tx, ngayPT.Year, ngayPT.Month);
                Db.EnsureDateNotInClosedPayroll(c, tx, ngayPT);

                // Kiểm tra trùng SoPhieu
                string checkSoPhieu = "SELECT COUNT(*) FROM PhieuPhuTroi WHERE SoPhieu = @SoPhieu";
                if (Convert.ToInt32(Db.Scalar(c, tx, checkSoPhieu, Db.P("@SoPhieu", soPhieu.Trim()))) > 0)
                    throw new InvalidOperationException($"Số phiếu '{soPhieu}' đã tồn tại.");

                TimeSpan otStart = gioBatDau;
                TimeSpan otEnd = gioBatDau.Add(TimeSpan.FromHours((double)soGio));

                foreach (int maNV in distinctNV)
                {
                    // 1. Kiểm tra hiệu lực thử việc hoặc HĐ
                    string sqlCheckActive = @"
                        SELECT 
                            (SELECT COUNT(*) FROM QuyetDinhTuyenDung WHERE MaNV = @MaNV AND TuNgay <= @Ngay AND DenNgay >= @Ngay) +
                            (SELECT COUNT(*) FROM HopDong WHERE MaNV = @MaNV AND TuNgay <= @Ngay AND (DenNgay IS NULL OR DenNgay >= @Ngay))";
                    int active = Convert.ToInt32(Db.Scalar(c, tx, sqlCheckActive, Db.P("@MaNV", maNV), Db.P("@Ngay", ngayPT.Date)));
                    if (active == 0)
                        throw new InvalidOperationException($"Nhân viên {maNV} không có hiệu lực thử việc/hợp đồng vào ngày phụ trội {ngayPT:dd/MM/yyyy}.");

                    // 2. Kiểm tra chồng giờ với ca làm việc
                    string sqlCheckCaOverlap = @"
                        SELECT COUNT(*) FROM ChamCong cc
                        JOIN CaLamViec ca ON cc.MaCa = ca.MaCa
                        WHERE cc.MaNV = @MaNV AND cc.Ngay = @Ngay
                          AND (ca.GioBatDau < @OtEnd AND @OtStart < ca.GioKetThuc)";
                    int caOverlap = Convert.ToInt32(Db.Scalar(c, tx, sqlCheckCaOverlap,
                        Db.P("@MaNV", maNV),
                        Db.P("@Ngay", ngayPT.Date),
                        Db.P("@OtStart", otStart),
                        Db.P("@OtEnd", otEnd)));
                    if (caOverlap > 0)
                        throw new InvalidOperationException($"Khoảng giờ phụ trội của nhân viên {maNV} bị chồng với ca làm việc trong ngày.");

                    // 3. Kiểm tra chồng giờ với phiếu phụ trội khác trong ngày
                    string sqlCheckOtherOt = @"
                        SELECT COUNT(*) FROM ChiTietPhuTroi ct
                        JOIN PhieuPhuTroi p ON ct.MaPhieuPT = p.MaPhieuPT
                        WHERE ct.MaNV = @MaNV AND p.NgayPhuTroi = @Ngay
                          AND (p.GioBatDau < @OtEnd AND @OtStart < DATEADD(SECOND, CAST(p.SoGio * 3600 AS int), CAST(p.GioBatDau AS time)))";
                    int otOverlap = Convert.ToInt32(Db.Scalar(c, tx, sqlCheckOtherOt,
                        Db.P("@MaNV", maNV),
                        Db.P("@Ngay", ngayPT.Date),
                        Db.P("@OtStart", otStart),
                        Db.P("@OtEnd", otEnd)));
                    if (otOverlap > 0)
                        throw new InvalidOperationException($"Khoảng giờ phụ trội của nhân viên {maNV} bị chồng với một phiếu phụ trội khác trong ngày.");
                }

                // Insert PhieuPhuTroi
                string sqlInsertPT = @"
                    INSERT INTO PhieuPhuTroi (SoPhieu, NgayPhuTroi, GioBatDau, SoGio, HinhThuc, TrangThai)
                    VALUES (@SoPhieu, @NgayPhuTroi, @GioBatDau, @SoGio, @HinhThuc, N'Kế hoạch');
                    SELECT SCOPE_IDENTITY();";
                int maPhieuPT = Convert.ToInt32(Db.Scalar(c, tx, sqlInsertPT,
                    Db.P("@SoPhieu", soPhieu.Trim()),
                    Db.P("@NgayPhuTroi", ngayPT.Date),
                    Db.P("@GioBatDau", gioBatDau),
                    Db.P("@SoGio", soGio),
                    Db.P("@HinhThuc", hinhThuc)));

                // Insert ChiTietPhuTroi
                foreach (int maNV in distinctNV)
                {
                    string sqlInsertCT = "INSERT INTO ChiTietPhuTroi (MaPhieuPT, MaNV) VALUES (@MaPhieuPT, @MaNV)";
                    Db.Execute(c, tx, sqlInsertCT, Db.P("@MaPhieuPT", maPhieuPT), Db.P("@MaNV", maNV));
                }
            });
        }

        public void XacNhanPhuTroi(int maPhieuPT)
        {
            Db.Transaction((c, tx) =>
            {
                var dt = Db.Query(c, tx, "SELECT NgayPhuTroi, TrangThai FROM PhieuPhuTroi WITH (UPDLOCK) WHERE MaPhieuPT = @MaPhieuPT", Db.P("@MaPhieuPT", maPhieuPT));
                if (dt.Rows.Count == 0) throw new InvalidOperationException("Phiếu phụ trội không tồn tại.");

                DateTime ngayPT = Convert.ToDateTime(dt.Rows[0]["NgayPhuTroi"]);
                Db.AcquirePayrollLock(c, tx, ngayPT.Year, ngayPT.Month);
                Db.EnsureDateNotInClosedPayroll(c, tx, ngayPT);

                string sql = "UPDATE PhieuPhuTroi SET TrangThai = N'Đã xác nhận' WHERE MaPhieuPT = @MaPhieuPT";
                Db.Execute(c, tx, sql, Db.P("@MaPhieuPT", maPhieuPT));
            });
        }

        public void MoLaiPhieuPhuTroi(int maPhieuPT)
        {
            Db.Transaction((c, tx) =>
            {
                var dt = Db.Query(c, tx, "SELECT NgayPhuTroi, TrangThai FROM PhieuPhuTroi WITH (UPDLOCK) WHERE MaPhieuPT = @MaPhieuPT", Db.P("@MaPhieuPT", maPhieuPT));
                if (dt.Rows.Count == 0) throw new InvalidOperationException("Phiếu phụ trội không tồn tại.");

                DateTime ngayPT = Convert.ToDateTime(dt.Rows[0]["NgayPhuTroi"]);
                Db.AcquirePayrollLock(c, tx, ngayPT.Year, ngayPT.Month);
                Db.EnsureDateNotInClosedPayroll(c, tx, ngayPT);

                string sql = "UPDATE PhieuPhuTroi SET TrangThai = N'Kế hoạch' WHERE MaPhieuPT = @MaPhieuPT";
                Db.Execute(c, tx, sql, Db.P("@MaPhieuPT", maPhieuPT));
            });
        }

        public void XoaPhieuPhuTroi(int maPhieuPT)
        {
            Db.Transaction((c, tx) =>
            {
                var dt = Db.Query(c, tx, "SELECT NgayPhuTroi, TrangThai FROM PhieuPhuTroi WITH (UPDLOCK) WHERE MaPhieuPT = @MaPhieuPT", Db.P("@MaPhieuPT", maPhieuPT));
                if (dt.Rows.Count == 0) throw new InvalidOperationException("Phiếu phụ trội không tồn tại.");

                DateTime ngayPT = Convert.ToDateTime(dt.Rows[0]["NgayPhuTroi"]);
                Db.AcquirePayrollLock(c, tx, ngayPT.Year, ngayPT.Month);
                Db.EnsureDateNotInClosedPayroll(c, tx, ngayPT);

                Db.Execute(c, tx, "DELETE FROM ChiTietPhuTroi WHERE MaPhieuPT = @MaPhieuPT", Db.P("@MaPhieuPT", maPhieuPT));
                Db.Execute(c, tx, "DELETE FROM PhieuPhuTroi WHERE MaPhieuPT = @MaPhieuPT", Db.P("@MaPhieuPT", maPhieuPT));
            });
        }
    }
}
