using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class ThuChiService
    {
        public DataTable GetQuyetDinhChuaThanhToan()
        {
            string sql = @"
                SELECT 
                    qd.MaQD,
                    qd.SoQD,
                    qd.LoaiQD,
                    CASE WHEN qd.LoaiQD = N'Khen thưởng' THEN N'Chi' ELSE N'Thu' END AS LoaiPhieuDuKien,
                    qd.SoTien,
                    qd.NgayLap,
                    qd.NgayKy,
                    qd.NoiDung,
                    nv.HoTen AS TenNhanVien,
                    pb.TenPhongBan,
                    nk.HoTen AS TenNguoiKy
                FROM QuyetDinhThuongPhat qd
                LEFT JOIN NhanVien nv ON qd.MaNV = nv.MaNV
                LEFT JOIN PhongBan pb ON qd.MaPhongBan = pb.MaPhongBan
                LEFT JOIN NhanVien nk ON qd.MaNguoiKy = nk.MaNV
                LEFT JOIN PhieuThuChi ptc ON qd.MaQD = ptc.MaQD
                WHERE qd.CachThanhToan = N'Trực tiếp' 
                  AND qd.TrangThai = N'Đã ký'
                  AND ptc.MaPhieu IS NULL
                ORDER BY qd.NgayKy DESC";

            return Db.Query(sql);
        }

        public DataTable GetAllPhieuThuChi(string loaiPhieu = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            string sql = @"
                SELECT 
                    ptc.MaPhieu,
                    ptc.SoPhieu,
                    ptc.NgayLap AS NgayLapPhieu,
                    ptc.LoaiPhieu,
                    ptc.SoTien,
                    qd.SoQD,
                    qd.LoaiQD,
                    qd.NgayKy,
                    nv.HoTen AS TenNhanVien,
                    pb.TenPhongBan,
                    qd.NoiDung
                FROM PhieuThuChi ptc
                JOIN QuyetDinhThuongPhat qd ON ptc.MaQD = qd.MaQD
                LEFT JOIN NhanVien nv ON qd.MaNV = nv.MaNV
                LEFT JOIN PhongBan pb ON qd.MaPhongBan = pb.MaPhongBan
                WHERE (1=1)";

            if (!string.IsNullOrWhiteSpace(loaiPhieu) && loaiPhieu != "Tất cả")
                sql += " AND ptc.LoaiPhieu = @LoaiPhieu";
            if (fromDate.HasValue)
                sql += " AND ptc.NgayLap >= @FromDate";
            if (toDate.HasValue)
                sql += " AND ptc.NgayLap <= @ToDate";

            sql += " ORDER BY ptc.NgayLap DESC, ptc.MaPhieu DESC";

            return Db.Query(sql,
                Db.P("@LoaiPhieu", loaiPhieu),
                Db.P("@FromDate", fromDate.HasValue ? (object)fromDate.Value.Date : DBNull.Value),
                Db.P("@ToDate", toDate.HasValue ? (object)toDate.Value.Date : DBNull.Value));
        }

        public void LapPhieuThuChi(string soPhieu, int maQD, DateTime ngayLapPhieu)
        {
            if (string.IsNullOrWhiteSpace(soPhieu))
                throw new ArgumentException("Số phiếu thu/chi không được để trống.");

            Db.Transaction((c, tx) =>
            {
                // Kiểm tra trùng SoPhieu
                string checkSoPhieu = "SELECT COUNT(*) FROM PhieuThuChi WHERE SoPhieu = @SoPhieu";
                if (Convert.ToInt32(Db.Scalar(c, tx, checkSoPhieu, Db.P("@SoPhieu", soPhieu.Trim()))) > 0)
                    throw new InvalidOperationException($"Số phiếu '{soPhieu}' đã tồn tại.");

                // Khóa đọc quyết định
                string sqlQD = @"
                    SELECT SoQD, LoaiQD, CachThanhToan, TrangThai, SoTien, NgayKy
                    FROM QuyetDinhThuongPhat WITH (UPDLOCK, HOLDLOCK)
                    WHERE MaQD = @MaQD";
                var dtQD = Db.Query(c, tx, sqlQD, Db.P("@MaQD", maQD));
                if (dtQD.Rows.Count == 0)
                    throw new InvalidOperationException("Không tìm thấy quyết định tương ứng.");

                string cachThanhToan = dtQD.Rows[0]["CachThanhToan"].ToString();
                string trangThai = dtQD.Rows[0]["TrangThai"].ToString();
                string loaiQD = dtQD.Rows[0]["LoaiQD"].ToString();
                decimal soTien = Convert.ToDecimal(dtQD.Rows[0]["SoTien"]);
                DateTime ngayKy = Convert.ToDateTime(dtQD.Rows[0]["NgayKy"]);

                if (cachThanhToan != "Trực tiếp")
                    throw new InvalidOperationException("Chỉ quyết định có hình thức thanh toán 'Trực tiếp' mới được lập phiếu Thu/Chi.");
                if (trangThai != "Đã ký")
                    throw new InvalidOperationException("Quyết định chưa được Giám đốc ký, không thể thực hiện thu/chi tiền.");
                if (ngayLapPhieu.Date < ngayKy.Date)
                    throw new InvalidOperationException($"Ngày lập phiếu ({ngayLapPhieu:dd/MM/yyyy}) không được trước ngày ký quyết định ({ngayKy:dd/MM/yyyy}).");

                // Kiểm tra xem đã có phiếu thu/chi cho MaQD này chưa
                string checkPaid = "SELECT COUNT(*) FROM PhieuThuChi WHERE MaQD = @MaQD";
                if (Convert.ToInt32(Db.Scalar(c, tx, checkPaid, Db.P("@MaQD", maQD))) > 0)
                    throw new InvalidOperationException("Quyết định này đã được lập phiếu thanh toán trước đó. Không được thanh toán lặp.");

                // Thưởng -> Chi; Phạt -> Thu
                string loaiPhieu = loaiQD == "Khen thưởng" ? "Chi" : "Thu";

                string sqlInsert = @"
                    INSERT INTO PhieuThuChi (SoPhieu, MaQD, NgayLap, LoaiPhieu, SoTien)
                    VALUES (@SoPhieu, @MaQD, @NgayLap, @LoaiPhieu, @SoTien)";
                Db.Execute(c, tx, sqlInsert,
                    Db.P("@SoPhieu", soPhieu.Trim()),
                    Db.P("@MaQD", maQD),
                    Db.P("@NgayLap", ngayLapPhieu.Date),
                    Db.P("@LoaiPhieu", loaiPhieu),
                    Db.P("@SoTien", soTien));
            });
        }
    }
}
