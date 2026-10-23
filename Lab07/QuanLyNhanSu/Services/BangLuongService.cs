using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyNhanSu.Data;

namespace QuanLyNhanSu.Services
{
    public class BangLuongService
    {
        public class PayrollCalculationItem
        {
            public int MaNV { get; set; }
            public string HoTen { get; set; }
            public string TenPhongBan { get; set; }
            public decimal GioThuong { get; set; }
            public decimal GioPhuTroi { get; set; }
            public decimal TienCong { get; set; }
            public decimal TienPhuTroi { get; set; }
            public decimal PhuCap { get; set; }
            public decimal TienThuong { get; set; }
            public decimal TienPhat { get; set; }
            public decimal TruBHXH { get; set; }
            public decimal TruBHYT { get; set; }
            public decimal ThucLinh { get; set; }
        }

        public DataTable GetAllBangLuong()
        {
            string sql = @"
                SELECT 
                    bl.MaBangLuong,
                    bl.Thang,
                    bl.Nam,
                    bl.GioChuanThang,
                    bl.NgayLap,
                    bl.TrangThai,
                    COUNT(ct.MaNV) AS SoNhanVien,
                    ISNULL(SUM(ct.ThucLinh), 0) AS TongThucLinh
                FROM BangLuong bl
                LEFT JOIN ChiTietBangLuong ct ON bl.MaBangLuong = ct.MaBangLuong
                GROUP BY bl.MaBangLuong, bl.Thang, bl.Nam, bl.GioChuanThang, bl.NgayLap, bl.TrangThai
                ORDER BY bl.Nam DESC, bl.Thang DESC";
            return Db.Query(sql);
        }

        public DataTable GetChiTietBangLuong(int maBangLuong)
        {
            string sql = @"
                SELECT 
                    ct.MaBangLuong,
                    ct.MaNV,
                    nv.HoTen,
                    pb.TenPhongBan,
                    ct.GioThuong,
                    ct.GioPhuTroi,
                    ct.TienCong,
                    ct.TienPhuTroi,
                    ct.PhuCap,
                    ct.TienThuong,
                    ct.TienPhat,
                    ct.TruBHXH,
                    ct.TruBHYT,
                    ct.ThucLinh
                FROM ChiTietBangLuong ct
                JOIN NhanVien nv ON ct.MaNV = nv.MaNV
                LEFT JOIN QuyetDinhTuyenDung qdtd ON nv.MaNV = qdtd.MaNV
                LEFT JOIN PhongBan pb ON qdtd.MaPhongBan = pb.MaPhongBan
                WHERE ct.MaBangLuong = @MaBangLuong
                ORDER BY ct.MaNV";
            return Db.Query(sql, Db.P("@MaBangLuong", maBangLuong));
        }

        public DataTable GetBangLuongByThangNam(int thang, int nam)
        {
            string sql = "SELECT * FROM BangLuong WHERE Thang = @Thang AND Nam = @Nam";
            return Db.Query(sql, Db.P("@Thang", thang), Db.P("@Nam", nam));
        }

        public List<string> KiemTraDieuKienChotLuong(int thang, int nam)
        {
            var errors = new List<string>();
            DateTime firstDay = new DateTime(nam, thang, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            // 1. Kiểm tra hết tháng
            if (DateTime.Today <= lastDay)
            {
                errors.Add($"Chưa hết tháng {thang:D2}/{nam} (ngày kết thúc là {lastDay:dd/MM/yyyy}). Theo quy định, chỉ được chốt lương sau khi tháng đã kết thúc.");
            }

            // 2. Kiểm tra giờ công NULL
            string sqlNull = "SELECT COUNT(*) FROM ChamCong WHERE Ngay BETWEEN @FirstDay AND @LastDay AND SoGioThucTe IS NULL";
            int countNull = Convert.ToInt32(Db.Scalar(Db.Open(), null, sqlNull, Db.P("@FirstDay", firstDay), Db.P("@LastDay", lastDay)));
            if (countNull > 0)
            {
                errors.Add($"Còn {countNull} bản ghi phân ca chưa được chấm công (giờ công NULL). Phải chấm công đầy đủ trước khi chốt.");
            }

            // 3. Kiểm tra phụ trội Kế hoạch
            string sqlPlan = "SELECT COUNT(*) FROM PhieuPhuTroi WHERE NgayPhuTroi BETWEEN @FirstDay AND @LastDay AND TrangThai = N'Kế hoạch'";
            int countPlan = Convert.ToInt32(Db.Scalar(Db.Open(), null, sqlPlan, Db.P("@FirstDay", firstDay), Db.P("@LastDay", lastDay)));
            if (countPlan > 0)
            {
                errors.Add($"Còn {countPlan} phiếu phụ trội đang ở trạng thái 'Kế hoạch'. Phải xác nhận hoặc hủy bỏ trước khi chốt lương.");
            }

            // 4. Kiểm tra quyết định Nháp Qua lương
            string sqlDraftQD = "SELECT COUNT(*) FROM QuyetDinhThuongPhat WHERE KyApDung = @FirstDay AND CachThanhToan = N'Qua lương' AND TrangThai = N'Nháp'";
            int countDraftQD = Convert.ToInt32(Db.Scalar(Db.Open(), null, sqlDraftQD, Db.P("@FirstDay", firstDay)));
            if (countDraftQD > 0)
            {
                errors.Add($"Còn {countDraftQD} quyết định thưởng/phạt qua lương của kỳ đang ở trạng thái 'Nháp'. Giám đốc phải ký hoặc hủy trước khi chốt.");
            }

            return errors;
        }

        /// <summary>
        /// Tính toán và Lưu Bảng Lương (Trạng thái Nháp hoặc Đã chốt).
        /// Mọi thao tác đều dùng khóa kỳ lương và thực hiện trong transaction.
        /// </summary>
        public int TinhVaLuuBangLuong(int thang, int nam, decimal gioChuanThang, bool chotBangLuong)
        {
            if (thang < 1 || thang > 12)
                throw new ArgumentException("Tháng phải từ 1 đến 12.");
            if (nam < 2000)
                throw new ArgumentException("Năm phải từ 2000 trở đi.");
            if (gioChuanThang <= 0 || gioChuanThang > 744)
                throw new ArgumentException("Số giờ chuẩn tháng H phải lớn hơn 0 và không vượt quá 744 giờ.");

            DateTime firstDay = new DateTime(nam, thang, 1);
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);

            return Db.Transaction((c, tx) =>
            {
                // 1. Lấy khóa kỳ lương
                Db.AcquirePayrollLock(c, tx, nam, thang);

                // 2. Kiểm tra nếu đã chốt thì không được sửa
                string checkClosed = "SELECT COUNT(*) FROM BangLuong WITH (UPDLOCK, HOLDLOCK) WHERE Thang = @Thang AND Nam = @Nam AND TrangThai = N'Đã chốt'";
                if (Convert.ToInt32(Db.Scalar(c, tx, checkClosed, Db.P("@Thang", thang), Db.P("@Nam", nam))) > 0)
                {
                    throw new InvalidOperationException($"Bảng lương tháng {thang:D2}/{nam} đã chốt. Không được phép chỉnh sửa hoặc tính lại!");
                }

                // 3. Nếu là hành động CHỐT, kiểm tra các điều kiện bắt buộc
                if (chotBangLuong)
                {
                    if (DateTime.Today <= lastDay)
                    {
                        throw new InvalidOperationException($"Chưa hết tháng {thang:D2}/{nam} (ngày kết thúc là {lastDay:dd/MM/yyyy}). Chỉ được chốt lương sau khi tháng đã kết thúc.");
                    }

                    string sqlNull = "SELECT COUNT(*) FROM ChamCong WHERE Ngay BETWEEN @FirstDay AND @LastDay AND SoGioThucTe IS NULL";
                    int countNull = Convert.ToInt32(Db.Scalar(c, tx, sqlNull, Db.P("@FirstDay", firstDay), Db.P("@LastDay", lastDay)));
                    if (countNull > 0)
                    {
                        throw new InvalidOperationException($"Còn {countNull} ca phân công chưa chấm công (giờ công NULL). Không thể chốt lương!");
                    }

                    string sqlPlan = "SELECT COUNT(*) FROM PhieuPhuTroi WHERE NgayPhuTroi BETWEEN @FirstDay AND @LastDay AND TrangThai = N'Kế hoạch'";
                    int countPlan = Convert.ToInt32(Db.Scalar(c, tx, sqlPlan, Db.P("@FirstDay", firstDay), Db.P("@LastDay", lastDay)));
                    if (countPlan > 0)
                    {
                        throw new InvalidOperationException($"Còn {countPlan} phiếu phụ trội đang ở trạng thái 'Kế hoạch'. Phải xác nhận hoặc xóa bỏ trước khi chốt.");
                    }

                    string sqlDraftQD = "SELECT COUNT(*) FROM QuyetDinhThuongPhat WHERE KyApDung = @FirstDay AND CachThanhToan = N'Qua lương' AND TrangThai = N'Nháp'";
                    int countDraftQD = Convert.ToInt32(Db.Scalar(c, tx, sqlDraftQD, Db.P("@FirstDay", firstDay)));
                    if (countDraftQD > 0)
                    {
                        throw new InvalidOperationException($"Còn {countDraftQD} quyết định thưởng/phạt qua lương đang ở trạng thái 'Nháp'. Giám đốc phải ký hoặc hủy bỏ trước khi chốt.");
                    }
                }

                // 4. Lấy danh sách nhân viên cần tính lương trong kỳ:
                // Giao thử việc với tháng, HOẶC giao hợp đồng với tháng, HOẶC có tên trong phân bổ thưởng/phạt qua lương của kỳ
                string sqlEmployees = @"
                    SELECT DISTINCT MaNV FROM (
                        SELECT MaNV FROM QuyetDinhTuyenDung WHERE TuNgay <= @LastDay AND DenNgay >= @FirstDay
                        UNION
                        SELECT MaNV FROM HopDong WHERE TuNgay <= @LastDay AND (DenNgay IS NULL OR DenNgay >= @FirstDay)
                        UNION
                        SELECT ct.MaNV FROM ChiTietQuyetDinh ct
                        JOIN QuyetDinhThuongPhat qd ON ct.MaQD = qd.MaQD
                        WHERE qd.KyApDung = @FirstDay AND qd.CachThanhToan = N'Qua lương'
                    ) T";
                var dtNV = Db.Query(c, tx, sqlEmployees, Db.P("@FirstDay", firstDay), Db.P("@LastDay", lastDay));

                var calculatedList = new List<PayrollCalculationItem>();

                foreach (DataRow rowNV in dtNV.Rows)
                {
                    int maNV = Convert.ToInt32(rowNV["MaNV"]);

                    // Lấy thông tin họ tên, phòng ban
                    string sqlNVInfo = @"
                        SELECT nv.HoTen, pb.TenPhongBan 
                        FROM NhanVien nv 
                        LEFT JOIN QuyetDinhTuyenDung qd ON nv.MaNV = qd.MaNV
                        LEFT JOIN PhongBan pb ON qd.MaPhongBan = pb.MaPhongBan
                        WHERE nv.MaNV = @MaNV";
                    var dtInfo = Db.Query(c, tx, sqlNVInfo, Db.P("@MaNV", maNV));
                    string hoTen = dtInfo.Rows.Count > 0 ? dtInfo.Rows[0]["HoTen"].ToString() : $"NV {maNV}";
                    string tenPhongBan = dtInfo.Rows.Count > 0 ? dtInfo.Rows[0]["TenPhongBan"]?.ToString() : "";

                    // Lấy hồ sơ tuyển dụng (thử việc)
                    var dtQDTD = Db.Query(c, tx, "SELECT TuNgay, DenNgay, LuongThuViec FROM QuyetDinhTuyenDung WHERE MaNV = @MaNV", Db.P("@MaNV", maNV));
                    // Lấy tất cả hợp đồng
                    var dtHD = Db.Query(c, tx, "SELECT LoaiHD, TuNgay, DenNgay FROM HopDong WHERE MaNV = @MaNV", Db.P("@MaNV", maNV));
                    // Lấy quyết định lương
                    var dtQDL = Db.Query(c, tx, "SELECT TuNgay, DenNgay, LuongCoBan, PhuCapLuong, PhuCapDocHai FROM QuyetDinhLuong WHERE MaNV = @MaNV", Db.P("@MaNV", maNV));
                    // Lấy quyết định chức vụ
                    var dtQDCV = Db.Query(c, tx, "SELECT TuNgay, DenNgay, PhuCapChucVu FROM QuyetDinhChucVu WHERE MaNV = @MaNV", Db.P("@MaNV", maNV));
                    // Lấy hồ sơ bảo hiểm
                    var dtBH = Db.Query(c, tx, "SELECT TuNgay, DenNgay, TyLeBHXH, TyLeBHYT FROM HoSoBaoHiem WHERE MaNV = @MaNV", Db.P("@MaNV", maNV));

                    // Lấy dữ liệu chấm công trong tháng của nhân viên
                    var dtCC = Db.Query(c, tx, "SELECT Ngay, SoGioThucTe FROM ChamCong WHERE MaNV = @MaNV AND Ngay BETWEEN @FirstDay AND @LastDay",
                        Db.P("@MaNV", maNV), Db.P("@FirstDay", firstDay), Db.P("@LastDay", lastDay));

                    // Lấy dữ liệu phụ trội đã xác nhận trong tháng của nhân viên
                    var dtPT = Db.Query(c, tx, @"
                        SELECT p.NgayPhuTroi, p.SoGio 
                        FROM ChiTietPhuTroi ct
                        JOIN PhieuPhuTroi p ON ct.MaPhieuPT = p.MaPhieuPT
                        WHERE ct.MaNV = @MaNV AND p.NgayPhuTroi BETWEEN @FirstDay AND @LastDay AND p.TrangThai = N'Đã xác nhận'",
                        Db.P("@MaNV", maNV), Db.P("@FirstDay", firstDay), Db.P("@LastDay", lastDay));

                    decimal rawGioThuong = 0;
                    decimal rawGioPhuTroi = 0;
                    decimal rawTienCong = 0;
                    decimal rawTienPhuTroi = 0;
                    decimal rawPhuCap = 0;
                    decimal rawTruBHXH = 0;
                    decimal rawTruBHYT = 0;

                    // Tập hợp tất cả các ngày nhân viên có chấm công hoặc phụ trội trong tháng
                    var distinctDays = new HashSet<DateTime>();
                    foreach (DataRow r in dtCC.Rows)
                        distinctDays.Add(Convert.ToDateTime(r["Ngay"]).Date);
                    foreach (DataRow r in dtPT.Rows)
                        distinctDays.Add(Convert.ToDateTime(r["NgayPhuTroi"]).Date);

                    foreach (DateTime d in distinctDays)
                    {
                        // Giờ làm thường của ngày d
                        decimal normalHours = 0;
                        foreach (DataRow r in dtCC.Rows)
                        {
                            if (Convert.ToDateTime(r["Ngay"]).Date == d)
                            {
                                if (r["SoGioThucTe"] != DBNull.Value)
                                    normalHours += Convert.ToDecimal(r["SoGioThucTe"]);
                            }
                        }

                        // Giờ phụ trội của ngày d
                        decimal otHours = 0;
                        foreach (DataRow r in dtPT.Rows)
                        {
                            if (Convert.ToDateTime(r["NgayPhuTroi"]).Date == d)
                            {
                                otHours += Convert.ToDecimal(r["SoGio"]);
                            }
                        }

                        if (normalHours == 0 && otHours == 0)
                            continue;

                        // Xác định căn cứ lương ngày d:
                        // 1. Kiểm tra ngày d có thuộc giai đoạn thử việc không?
                        bool isThuViec = false;
                        decimal luongThuViec = 0;
                        if (dtQDTD.Rows.Count > 0)
                        {
                            DateTime tvTu = Convert.ToDateTime(dtQDTD.Rows[0]["TuNgay"]).Date;
                            DateTime tvDen = Convert.ToDateTime(dtQDTD.Rows[0]["DenNgay"]).Date;
                            if (d >= tvTu && d <= tvDen)
                            {
                                isThuViec = true;
                                luongThuViec = Convert.ToDecimal(dtQDTD.Rows[0]["LuongThuViec"]);
                            }
                        }

                        // 2. Kiểm tra ngày d có thuộc hợp đồng không?
                        bool isHopDong = false;
                        string loaiHD = null;
                        foreach (DataRow r in dtHD.Rows)
                        {
                            DateTime hdTu = Convert.ToDateTime(r["TuNgay"]).Date;
                            DateTime? hdDen = r["DenNgay"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(r["DenNgay"]).Date : null;
                            if (d >= hdTu && (!hdDen.HasValue || d <= hdDen.Value))
                            {
                                isHopDong = true;
                                loaiHD = r["LoaiHD"].ToString();
                                break;
                            }
                        }

                        decimal R = 0;
                        decimal dailyPhuCap = 0;
                        decimal dailyBHXH = 0;
                        decimal dailyBHYT = 0;

                        if (isThuViec)
                        {
                            // Thử việc: R = Lương thử việc / H; không có phụ cấp riêng, không bảo hiểm
                            R = luongThuViec / gioChuanThang;
                        }
                        else if (isHopDong)
                        {
                            // Chính thức: Tìm Quyết định lương có hiệu lực ngày d
                            DataRow qdlRow = null;
                            foreach (DataRow r in dtQDL.Rows)
                            {
                                DateTime lTu = Convert.ToDateTime(r["TuNgay"]).Date;
                                DateTime? lDen = r["DenNgay"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(r["DenNgay"]).Date : null;
                                if (d >= lTu && (!lDen.HasValue || d <= lDen.Value))
                                {
                                    qdlRow = r;
                                    break;
                                }
                            }

                            if (qdlRow == null)
                            {
                                throw new InvalidOperationException($"Nhân viên {hoTen} (Mã: {maNV}) có ngày làm việc {d:dd/MM/yyyy} nhưng thiếu Quyết định lương có hiệu lực!");
                            }

                            decimal luongCoBan = Convert.ToDecimal(qdlRow["LuongCoBan"]);
                            decimal phuCapLuong = Convert.ToDecimal(qdlRow["PhuCapLuong"]);
                            decimal phuCapDocHai = Convert.ToDecimal(qdlRow["PhuCapDocHai"]);

                            R = luongCoBan / gioChuanThang;

                            // Tìm phụ cấp chức vụ ngày d
                            decimal phuCapChucVu = 0;
                            foreach (DataRow r in dtQDCV.Rows)
                            {
                                DateTime cvTu = Convert.ToDateTime(r["TuNgay"]).Date;
                                DateTime? cvDen = r["DenNgay"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(r["DenNgay"]).Date : null;
                                if (d >= cvTu && (!cvDen.HasValue || d <= cvDen.Value))
                                {
                                    phuCapChucVu = Convert.ToDecimal(r["PhuCapChucVu"]);
                                    break;
                                }
                            }

                            // Phụ cấp = giờ thường * (phụ cấp lương + độc hại + chức vụ) / H
                            dailyPhuCap = normalHours * (phuCapLuong + phuCapDocHai + phuCapChucVu) / gioChuanThang;

                            // Bảo hiểm nếu HĐ dài hạn
                            if (loaiHD == "Dài hạn")
                            {
                                DataRow bhRow = null;
                                foreach (DataRow r in dtBH.Rows)
                                {
                                    DateTime bhTu = Convert.ToDateTime(r["TuNgay"]).Date;
                                    DateTime? bhDen = r["DenNgay"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(r["DenNgay"]).Date : null;
                                    if (d >= bhTu && (!bhDen.HasValue || d <= bhDen.Value))
                                    {
                                        bhRow = r;
                                        break;
                                    }
                                }

                                if (bhRow == null)
                                {
                                    throw new InvalidOperationException($"Nhân viên {hoTen} (Mã: {maNV}) có HĐ dài hạn ngày {d:dd/MM/yyyy} nhưng thiếu Hồ sơ bảo hiểm có hiệu lực!");
                                }

                                decimal tyLeBHXH = Convert.ToDecimal(bhRow["TyLeBHXH"]);
                                decimal tyLeBHYT = Convert.ToDecimal(bhRow["TyLeBHYT"]);

                                // Trừ BHXH và BHYT = (LuongCoBan / H) * normalHours * TyLe
                                dailyBHXH = (luongCoBan / gioChuanThang) * normalHours * tyLeBHXH;
                                dailyBHYT = (luongCoBan / gioChuanThang) * normalHours * tyLeBHYT;
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException($"Nhân viên {hoTen} (Mã: {maNV}) có phát sinh công vào ngày {d:dd/MM/yyyy} nằm ngoài cả thời gian thử việc lẫn hợp đồng!");
                        }

                        rawGioThuong += normalHours;
                        rawGioPhuTroi += otHours;
                        rawTienCong += normalHours * R;
                        rawTienPhuTroi += otHours * R * 1.5m;
                        rawPhuCap += dailyPhuCap;
                        rawTruBHXH += dailyBHXH;
                        rawTruBHYT += dailyBHYT;
                    }

                    // Thưởng / Phạt từ phân bổ quyết định Qua lương đã ký của kỳ
                    string sqlTP = @"
                        SELECT qd.LoaiQD, ct.SoTien
                        FROM ChiTietQuyetDinh ct
                        JOIN QuyetDinhThuongPhat qd ON ct.MaQD = qd.MaQD
                        WHERE ct.MaNV = @MaNV 
                          AND qd.KyApDung = @FirstDay 
                          AND qd.CachThanhToan = N'Qua lương'
                          AND qd.TrangThai = N'Đã ký'";
                    var dtTP = Db.Query(c, tx, sqlTP, Db.P("@MaNV", maNV), Db.P("@FirstDay", firstDay));

                    decimal rawTienThuong = 0;
                    decimal rawTienPhat = 0;
                    foreach (DataRow r in dtTP.Rows)
                    {
                        string loai = r["LoaiQD"].ToString();
                        decimal tien = Convert.ToDecimal(r["SoTien"]);
                        if (loai == "Khen thưởng") rawTienThuong += tien;
                        else if (loai == "Kỷ luật") rawTienPhat += tien;
                    }

                    // Làm tròn từng khoản 2 chữ số (MidpointRounding.AwayFromZero)
                    decimal tienCong = Math.Round(rawTienCong, 2, MidpointRounding.AwayFromZero);
                    decimal tienPhuTroi = Math.Round(rawTienPhuTroi, 2, MidpointRounding.AwayFromZero);
                    decimal phuCap = Math.Round(rawPhuCap, 2, MidpointRounding.AwayFromZero);
                    decimal tienThuong = Math.Round(rawTienThuong, 2, MidpointRounding.AwayFromZero);
                    decimal tienPhat = Math.Round(rawTienPhat, 2, MidpointRounding.AwayFromZero);
                    decimal truBHXH = Math.Round(rawTruBHXH, 2, MidpointRounding.AwayFromZero);
                    decimal truBHYT = Math.Round(rawTruBHYT, 2, MidpointRounding.AwayFromZero);

                    decimal thucLinh = tienCong + tienPhuTroi + phuCap + tienThuong - tienPhat - truBHXH - truBHYT;

                    if (thucLinh < 0)
                    {
                        throw new InvalidOperationException($"Nhân viên {hoTen} (Mã: {maNV}) có thực lĩnh âm ({thucLinh:N2}). Theo quy định, không thể chốt bảng lương!");
                    }

                    calculatedList.Add(new PayrollCalculationItem
                    {
                        MaNV = maNV,
                        HoTen = hoTen,
                        TenPhongBan = tenPhongBan,
                        GioThuong = rawGioThuong,
                        GioPhuTroi = rawGioPhuTroi,
                        TienCong = tienCong,
                        TienPhuTroi = tienPhuTroi,
                        PhuCap = phuCap,
                        TienThuong = tienThuong,
                        TienPhat = tienPhat,
                        TruBHXH = truBHXH,
                        TruBHYT = truBHYT,
                        ThucLinh = thucLinh
                    });
                }

                // 5. Lưu vào Database (BangLuong & ChiTietBangLuong)
                // Kiểm tra xem bảng lương đã có bản ghi nào chưa
                string sqlGetBL = "SELECT MaBangLuong FROM BangLuong WHERE Thang = @Thang AND Nam = @Nam";
                var dtBL = Db.Query(c, tx, sqlGetBL, Db.P("@Thang", thang), Db.P("@Nam", nam));
                int maBangLuong;
                string newStatus = chotBangLuong ? "Đã chốt" : "Nháp";

                if (dtBL.Rows.Count > 0)
                {
                    maBangLuong = Convert.ToInt32(dtBL.Rows[0]["MaBangLuong"]);
                    // Xóa chi tiết cũ nếu đang là bản Nháp
                    Db.Execute(c, tx, "DELETE FROM ChiTietBangLuong WHERE MaBangLuong = @MaBangLuong", Db.P("@MaBangLuong", maBangLuong));
                    // Cập nhật thông tin bảng lương
                    string sqlUpdateBL = @"
                        UPDATE BangLuong 
                        SET GioChuanThang = @GioChuanThang, NgayLap = @NgayLap, TrangThai = @TrangThai 
                        WHERE MaBangLuong = @MaBangLuong";
                    Db.Execute(c, tx, sqlUpdateBL,
                        Db.P("@GioChuanThang", gioChuanThang),
                        Db.P("@NgayLap", DateTime.Today),
                        Db.P("@TrangThai", newStatus),
                        Db.P("@MaBangLuong", maBangLuong));
                }
                else
                {
                    string sqlInsertBL = @"
                        INSERT INTO BangLuong (Thang, Nam, GioChuanThang, NgayLap, TrangThai)
                        VALUES (@Thang, @Nam, @GioChuanThang, @NgayLap, @TrangThai);
                        SELECT SCOPE_IDENTITY();";
                    maBangLuong = Convert.ToInt32(Db.Scalar(c, tx, sqlInsertBL,
                        Db.P("@Thang", thang),
                        Db.P("@Nam", nam),
                        Db.P("@GioChuanThang", gioChuanThang),
                        Db.P("@NgayLap", DateTime.Today),
                        Db.P("@TrangThai", newStatus)));
                }

                // Insert các dòng ChiTietBangLuong
                foreach (var item in calculatedList)
                {
                    string sqlInsertCT = @"
                        INSERT INTO ChiTietBangLuong 
                        (MaBangLuong, MaNV, GioThuong, GioPhuTroi, TienCong, TienPhuTroi, PhuCap, TienThuong, TienPhat, TruBHXH, TruBHYT, ThucLinh)
                        VALUES 
                        (@MaBangLuong, @MaNV, @GioThuong, @GioPhuTroi, @TienCong, @TienPhuTroi, @PhuCap, @TienThuong, @TienPhat, @TruBHXH, @TruBHYT, @ThucLinh)";
                    Db.Execute(c, tx, sqlInsertCT,
                        Db.P("@MaBangLuong", maBangLuong),
                        Db.P("@MaNV", item.MaNV),
                        Db.P("@GioThuong", item.GioThuong),
                        Db.P("@GioPhuTroi", item.GioPhuTroi),
                        Db.P("@TienCong", item.TienCong),
                        Db.P("@TienPhuTroi", item.TienPhuTroi),
                        Db.P("@PhuCap", item.PhuCap),
                        Db.P("@TienThuong", item.TienThuong),
                        Db.P("@TienPhat", item.TienPhat),
                        Db.P("@TruBHXH", item.TruBHXH),
                        Db.P("@TruBHYT", item.TruBHYT),
                        Db.P("@ThucLinh", item.ThucLinh));
                }

                // 6. Nếu chốt: Cập nhật MaBangLuong vào ChiTietQuyetDinh để đánh dấu đã áp dụng vào bảng lương
                if (chotBangLuong)
                {
                    string sqlUpdateCTQD = @"
                        UPDATE ct 
                        SET ct.MaBangLuong = @MaBangLuong
                        FROM ChiTietQuyetDinh ct
                        JOIN QuyetDinhThuongPhat qd ON ct.MaQD = qd.MaQD
                        WHERE qd.KyApDung = @FirstDay 
                          AND qd.CachThanhToan = N'Qua lương'
                          AND qd.TrangThai = N'Đã ký'";
                    Db.Execute(c, tx, sqlUpdateCTQD, Db.P("@MaBangLuong", maBangLuong), Db.P("@FirstDay", firstDay));
                }

                return maBangLuong;
            });
        }
    }
}
