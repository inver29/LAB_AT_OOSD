using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class HoSoHopDongService
    {
        public DataTable GetAllNhanVien(string search = null, string trangThai = null)
        {
            string sql = @"
                SELECT 
                    nv.MaNV,
                    nv.HoTen,
                    nv.NgaySinh,
                    nv.GioiTinh,
                    nv.DienThoai,
                    nv.TrangThai,
                    pb.TenPhongBan,
                    qd.SoQD AS SoQDTuyenDung,
                    qd.TuNgay AS ThuViecTuNgay,
                    qd.DenNgay AS ThuViecDenNgay,
                    qd.LuongThuViec,
                    qd.KetQua AS KetQuaThuViec,
                    hd.SoHD,
                    hd.LoaiHD,
                    hd.TuNgay AS HDTuNgay,
                    hd.DenNgay AS HDDenNgay
                FROM NhanVien nv
                LEFT JOIN QuyetDinhTuyenDung qd ON nv.MaNV = qd.MaNV
                LEFT JOIN PhongBan pb ON qd.MaPhongBan = pb.MaPhongBan
                OUTER APPLY (
                    SELECT TOP 1 * FROM HopDong 
                    WHERE MaNV = nv.MaNV 
                    ORDER BY TuNgay DESC, MaHD DESC
                ) hd
                WHERE (1=1)";

            if (!string.IsNullOrWhiteSpace(search))
            {
                sql += " AND (nv.HoTen LIKE @Search OR nv.DienThoai LIKE @Search OR qd.SoQD LIKE @Search OR hd.SoHD LIKE @Search)";
            }
            if (!string.IsNullOrWhiteSpace(trangThai) && trangThai != "Tất cả")
            {
                sql += " AND nv.TrangThai = @TrangThai";
            }
            sql += " ORDER BY nv.MaNV DESC";

            return Db.Query(sql, 
                Db.P("@Search", "%" + search?.Trim() + "%"), 
                Db.P("@TrangThai", trangThai));
        }

        public DataTable GetHopDongByNhanVien(int maNV)
        {
            string sql = "SELECT * FROM HopDong WHERE MaNV = @MaNV ORDER BY TuNgay DESC";
            return Db.Query(sql, Db.P("@MaNV", maNV));
        }

        public DataTable GetHoSoBaoHiemByNhanVien(int maNV)
        {
            string sql = "SELECT * FROM HoSoBaoHiem WHERE MaNV = @MaNV ORDER BY TuNgay DESC";
            return Db.Query(sql, Db.P("@MaNV", maNV));
        }

        /// <summary>
        /// Tiếp nhận nhân viên thử việc mới: Tạo NhanVien + QuyetDinhTuyenDung nguyên tử.
        /// </summary>
        public int TiepNhanThuViec(
            string hoTen, DateTime ngaySinh, string gioiTinh, string dienThoai,
            string soQD, int maPhongBan, DateTime ngayQD, DateTime tuNgay, DateTime denNgay,
            decimal luongThuViec, string noiDung)
        {
            if (string.IsNullOrWhiteSpace(hoTen))
                throw new ArgumentException("Họ tên không được để trống.");
            if (string.IsNullOrWhiteSpace(soQD))
                throw new ArgumentException("Số quyết định tuyển dụng không được để trống.");
            if (ngayQD > tuNgay)
                throw new ArgumentException("Ngày quyết định phải nhỏ hơn hoặc bằng ngày bắt đầu thử việc.");
            if (denNgay < tuNgay)
                throw new ArgumentException("Ngày kết thúc thử việc phải lớn hơn hoặc bằng ngày bắt đầu thử việc.");
            if (luongThuViec <= 0)
                throw new ArgumentException("Lương thử việc phải lớn hơn 0.");

            return Db.Transaction((c, tx) =>
            {
                // Kiểm tra trùng SoQD
                string checkSoQD = "SELECT COUNT(*) FROM QuyetDinhTuyenDung WHERE SoQD = @SoQD";
                if (Convert.ToInt32(Db.Scalar(c, tx, checkSoQD, Db.P("@SoQD", soQD.Trim()))) > 0)
                    throw new InvalidOperationException($"Số quyết định '{soQD}' đã tồn tại trong hệ thống.");

                // Insert NhanVien
                string sqlNV = @"INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DienThoai, TrangThai)
                                 VALUES (@HoTen, @NgaySinh, @GioiTinh, @DienThoai, N'Thử việc');
                                 SELECT SCOPE_IDENTITY();";
                int maNV = Convert.ToInt32(Db.Scalar(c, tx, sqlNV,
                    Db.P("@HoTen", hoTen.Trim()),
                    Db.P("@NgaySinh", ngaySinh.Date),
                    Db.P("@GioiTinh", gioiTinh),
                    Db.P("@DienThoai", dienThoai),
                    Db.P("@TrangThai", "Thử việc")));

                // Insert QuyetDinhTuyenDung
                string sqlQDTD = @"INSERT INTO QuyetDinhTuyenDung 
                    (SoQD, MaNV, MaPhongBan, NgayQD, TuNgay, DenNgay, LuongThuViec, NoiDung, KetQua)
                    VALUES (@SoQD, @MaNV, @MaPhongBan, @NgayQD, @TuNgay, @DenNgay, @LuongThuViec, @NoiDung, NULL)";
                Db.Execute(c, tx, sqlQDTD,
                    Db.P("@SoQD", soQD.Trim()),
                    Db.P("@MaNV", maNV),
                    Db.P("@MaPhongBan", maPhongBan),
                    Db.P("@NgayQD", ngayQD.Date),
                    Db.P("@TuNgay", tuNgay.Date),
                    Db.P("@DenNgay", denNgay.Date),
                    Db.P("@LuongThuViec", luongThuViec),
                    Db.P("@NoiDung", noiDung ?? string.Empty));

                return maNV;
            });
        }

        /// <summary>
        /// Ghi nhận kết quả thử việc: Đạt -> Chờ hợp đồng, Không đạt -> Kết thúc thử việc.
        /// Chỉ ghi khi đã hết thử việc.
        /// </summary>
        public void DanhGiaThuViec(int maNV, string ketQua, DateTime ngayDanhGia)
        {
            if (ketQua != "Đạt" && ketQua != "Không đạt")
                throw new ArgumentException("Kết quả đánh giá phải là 'Đạt' hoặc 'Không đạt'.");

            Db.Transaction((c, tx) =>
            {
                string sqlGetQDTD = "SELECT DenNgay, KetQua FROM QuyetDinhTuyenDung WITH (UPDLOCK) WHERE MaNV = @MaNV";
                var dt = Db.Query(c, tx, sqlGetQDTD, Db.P("@MaNV", maNV));
                if (dt.Rows.Count == 0)
                    throw new InvalidOperationException("Không tìm thấy quyết định tuyển dụng của nhân viên này.");

                DateTime denNgayThuViec = Convert.ToDateTime(dt.Rows[0]["DenNgay"]);
                if (ngayDanhGia.Date < denNgayThuViec.Date)
                {
                    throw new InvalidOperationException($"Chưa hết thời gian thử việc (kết thúc ngày {denNgayThuViec:dd/MM/yyyy}). Không thể đánh giá kết quả sớm hơn ngày kết thúc thử việc.");
                }

                string trangThaiMoi = ketQua == "Đạt" ? "Chờ hợp đồng" : "Kết thúc thử việc";

                string sqlUpdateQD = "UPDATE QuyetDinhTuyenDung SET KetQua = @KetQua WHERE MaNV = @MaNV";
                Db.Execute(c, tx, sqlUpdateQD, Db.P("@KetQua", ketQua), Db.P("@MaNV", maNV));

                string sqlUpdateNV = "UPDATE NhanVien SET TrangThai = @TrangThai WHERE MaNV = @MaNV";
                Db.Execute(c, tx, sqlUpdateNV, Db.P("@TrangThai", trangThaiMoi), Db.P("@MaNV", maNV));
            });
        }

        /// <summary>
        /// Ký hợp đồng lao động:
        /// - Chỉ ký khi Đạt thử việc, hết thử việc.
        /// - NgayKy >= DenNgayThuViec và NgayKy <= TuNgayHD.
        /// - TuNgayHD > DenNgayThuViec.
        /// - Không chồng lấn thời gian hợp đồng.
        /// - Nếu Dài hạn: lưu kèm HoSoBaoHiem và Quyết định lương ban đầu trong cùng giao dịch.
        /// </summary>
        public void KyHopDong(
            int maNV, string soHD, string loaiHD, DateTime ngayKy, DateTime tuNgay, DateTime? denNgay, string noiDung,
            // Thông tin bảo hiểm (nếu Dài hạn)
            string soBHXH = null, string soBHYT = null, decimal tyLeBHXH = 0.08m, decimal tyLeBHYT = 0.015m,
            // Thông tin lương cơ bản ban đầu (nếu có)
            string soQDLuong = null, decimal luongCoBan = 0, decimal phuCapLuong = 0, decimal phuCapDocHai = 0)
        {
            if (string.IsNullOrWhiteSpace(soHD))
                throw new ArgumentException("Số hợp đồng không được để trống.");
            if (loaiHD != "Dài hạn" && loaiHD != "Ngắn hạn")
                throw new ArgumentException("Loại hợp đồng phải là 'Dài hạn' hoặc 'Ngắn hạn'.");
            if (loaiHD == "Ngắn hạn" && !denNgay.HasValue)
                throw new ArgumentException("Hợp đồng ngắn hạn bắt buộc phải có ngày kết thúc.");
            if (denNgay.HasValue && denNgay.Value.Date < tuNgay.Date)
                throw new ArgumentException("Ngày kết thúc hợp đồng không được nhỏ hơn ngày bắt đầu.");
            if (ngayKy.Date > tuNgay.Date)
                throw new ArgumentException("Ngày ký hợp đồng không được sau ngày bắt đầu hợp đồng.");

            Db.Transaction((c, tx) =>
            {
                // 1. Kiểm tra QĐ thử việc
                string sqlQDTD = "SELECT DenNgay, KetQua FROM QuyetDinhTuyenDung WITH (UPDLOCK) WHERE MaNV = @MaNV";
                var dtQDTD = Db.Query(c, tx, sqlQDTD, Db.P("@MaNV", maNV));
                if (dtQDTD.Rows.Count == 0)
                    throw new InvalidOperationException("Nhân viên chưa có quyết định tuyển dụng.");

                string ketQua = dtQDTD.Rows[0]["KetQua"] != DBNull.Value ? dtQDTD.Rows[0]["KetQua"].ToString() : null;
                DateTime denNgayThuViec = Convert.ToDateTime(dtQDTD.Rows[0]["DenNgay"]);

                if (ketQua != "Đạt")
                    throw new InvalidOperationException("Chỉ được ký hợp đồng với nhân viên có kết quả thử việc 'Đạt'.");
                if (ngayKy.Date < denNgayThuViec.Date)
                    throw new InvalidOperationException($"Ngày ký hợp đồng ({ngayKy:dd/MM/yyyy}) không được trước ngày hết hạn thử việc ({denNgayThuViec:dd/MM/yyyy}).");
                if (tuNgay.Date <= denNgayThuViec.Date)
                    throw new InvalidOperationException($"Hợp đồng phải bắt đầu sau ngày hết hạn thử việc ({denNgayThuViec:dd/MM/yyyy}).");

                // 2. Kiểm tra trùng SoHD
                string checkSoHD = "SELECT COUNT(*) FROM HopDong WHERE SoHD = @SoHD";
                if (Convert.ToInt32(Db.Scalar(c, tx, checkSoHD, Db.P("@SoHD", soHD.Trim()))) > 0)
                    throw new InvalidOperationException($"Số hợp đồng '{soHD}' đã tồn tại.");

                // 3. Kiểm tra chồng chéo hợp đồng của nhân viên
                string sqlCheckOverlap = @"
                    SELECT COUNT(*) FROM HopDong 
                    WHERE MaNV = @MaNV 
                      AND (DenNgay IS NULL OR DenNgay >= @TuNgay)
                      AND (@DenNgay IS NULL OR TuNgay <= @DenNgay)";
                int overlapCount = Convert.ToInt32(Db.Scalar(c, tx, sqlCheckOverlap,
                    Db.P("@MaNV", maNV),
                    Db.P("@TuNgay", tuNgay.Date),
                    Db.P("@DenNgay", (object)denNgay?.Date ?? DBNull.Value)));
                if (overlapCount > 0)
                    throw new InvalidOperationException("Thời gian hiệu lực của hợp đồng bị chồng chéo với hợp đồng khác của nhân viên này.");

                // 4. Lưu Hợp đồng
                string sqlHD = @"INSERT INTO HopDong (SoHD, MaNV, LoaiHD, NgayKy, TuNgay, DenNgay, NoiDung)
                                 VALUES (@SoHD, @MaNV, @LoaiHD, @NgayKy, @TuNgay, @DenNgay, @NoiDung)";
                Db.Execute(c, tx, sqlHD,
                    Db.P("@SoHD", soHD.Trim()),
                    Db.P("@MaNV", maNV),
                    Db.P("@LoaiHD", loaiHD),
                    Db.P("@NgayKy", ngayKy.Date),
                    Db.P("@TuNgay", tuNgay.Date),
                    Db.P("@DenNgay", (object)denNgay?.Date ?? DBNull.Value),
                    Db.P("@NoiDung", noiDung ?? string.Empty));

                // 5. Cập nhật trạng thái nhân viên -> Chính thức
                string sqlUpdateNV = "UPDATE NhanVien SET TrangThai = N'Chính thức' WHERE MaNV = @MaNV";
                Db.Execute(c, tx, sqlUpdateNV, Db.P("@MaNV", maNV));

                // 6. Nếu hợp đồng dài hạn: Lưu hồ sơ bảo hiểm
                if (loaiHD == "Dài hạn")
                {
                    if (string.IsNullOrWhiteSpace(soBHXH) || string.IsNullOrWhiteSpace(soBHYT))
                        throw new ArgumentException("Hợp đồng dài hạn bắt buộc phải có Số sổ BHXH và Số thẻ BHYT.");
                    if (tyLeBHXH < 0 || tyLeBHXH > 1 || tyLeBHYT < 0 || tyLeBHYT > 1)
                        throw new ArgumentException("Tỷ lệ bảo hiểm phải nằm trong khoảng từ 0 đến 1 (ví dụ 0.08 cho 8%).");

                    string sqlBH = @"INSERT INTO HoSoBaoHiem (MaNV, SoBHXH, SoBHYT, TuNgay, DenNgay, TyLeBHXH, TyLeBHYT)
                                     VALUES (@MaNV, @SoBHXH, @SoBHYT, @TuNgay, @DenNgay, @TyLeBHXH, @TyLeBHYT)";
                    Db.Execute(c, tx, sqlBH,
                        Db.P("@MaNV", maNV),
                        Db.P("@SoBHXH", soBHXH.Trim()),
                        Db.P("@SoBHYT", soBHYT.Trim()),
                        Db.P("@TuNgay", tuNgay.Date),
                        Db.P("@DenNgay", (object)denNgay?.Date ?? DBNull.Value),
                        Db.P("@TyLeBHXH", tyLeBHXH),
                        Db.P("@TyLeBHYT", tyLeBHYT));
                }

                // 7. Nếu có kèm lương cơ bản ban đầu
                if (!string.IsNullOrWhiteSpace(soQDLuong) && luongCoBan > 0)
                {
                    string checkSoQDL = "SELECT COUNT(*) FROM QuyetDinhLuong WHERE SoQD = @SoQD";
                    if (Convert.ToInt32(Db.Scalar(c, tx, checkSoQDL, Db.P("@SoQD", soQDLuong.Trim()))) == 0)
                    {
                        string sqlQDL = @"INSERT INTO QuyetDinhLuong (SoQD, MaNV, NgayQD, TuNgay, DenNgay, LuongCoBan, PhuCapLuong, PhuCapDocHai)
                                          VALUES (@SoQD, @MaNV, @NgayQD, @TuNgay, @DenNgay, @LuongCoBan, @PhuCapLuong, @PhuCapDocHai)";
                        Db.Execute(c, tx, sqlQDL,
                            Db.P("@SoQD", soQDLuong.Trim()),
                            Db.P("@MaNV", maNV),
                            Db.P("@NgayQD", ngayKy.Date),
                            Db.P("@TuNgay", tuNgay.Date),
                            Db.P("@DenNgay", (object)denNgay?.Date ?? DBNull.Value),
                            Db.P("@LuongCoBan", luongCoBan),
                            Db.P("@PhuCapLuong", phuCapLuong),
                            Db.P("@PhuCapDocHai", phuCapDocHai));
                    }
                }
            });
        }
    }
}
