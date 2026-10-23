-- BÀI 7 - QUẢN LÝ NHÂN SỰ
-- Script dữ liệu DEMO mẫu (chạy lại an toàn, không nhân đôi, không DROP/DELETE DB)
-- Sử dụng CSDL QuanLyNhanSu_Bai07

USE QuanLyNhanSu_Bai07;
GO

SET NOCOUNT ON;

-- 1. DANH MỤC PHÒNG BAN
IF NOT EXISTS (SELECT 1 FROM PhongBan WHERE TenPhongBan = N'Ban Giám Đốc')
    INSERT INTO PhongBan (TenPhongBan) VALUES (N'Ban Giám Đốc');
IF NOT EXISTS (SELECT 1 FROM PhongBan WHERE TenPhongBan = N'Phòng Hành chính - Nhân sự')
    INSERT INTO PhongBan (TenPhongBan) VALUES (N'Phòng Hành chính - Nhân sự');
IF NOT EXISTS (SELECT 1 FROM PhongBan WHERE TenPhongBan = N'Phòng Kỹ thuật & Sản xuất')
    INSERT INTO PhongBan (TenPhongBan) VALUES (N'Phòng Kỹ thuật & Sản xuất');
IF NOT EXISTS (SELECT 1 FROM PhongBan WHERE TenPhongBan = N'Phòng Kế toán - Tài chính')
    INSERT INTO PhongBan (TenPhongBan) VALUES (N'Phòng Kế toán - Tài chính');
IF NOT EXISTS (SELECT 1 FROM PhongBan WHERE TenPhongBan = N'Phòng Kinh doanh')
    INSERT INTO PhongBan (TenPhongBan) VALUES (N'Phòng Kinh doanh');

-- 2. DANH MỤC CHỨC VỤ
IF NOT EXISTS (SELECT 1 FROM ChucVu WHERE TenChucVu = N'Giám đốc')
    INSERT INTO ChucVu (TenChucVu) VALUES (N'Giám đốc');
IF NOT EXISTS (SELECT 1 FROM ChucVu WHERE TenChucVu = N'Trưởng phòng')
    INSERT INTO ChucVu (TenChucVu) VALUES (N'Trưởng phòng');
IF NOT EXISTS (SELECT 1 FROM ChucVu WHERE TenChucVu = N'Chuyên viên chính')
    INSERT INTO ChucVu (TenChucVu) VALUES (N'Chuyên viên chính');
IF NOT EXISTS (SELECT 1 FROM ChucVu WHERE TenChucVu = N'Kỹ thuật viên')
    INSERT INTO ChucVu (TenChucVu) VALUES (N'Kỹ thuật viên');
IF NOT EXISTS (SELECT 1 FROM ChucVu WHERE TenChucVu = N'Nhân viên')
    INSERT INTO ChucVu (TenChucVu) VALUES (N'Nhân viên');

-- 3. DANH MỤC CA LÀM VIỆC
IF NOT EXISTS (SELECT 1 FROM CaLamViec WHERE TenCa = N'Ca Sáng')
    INSERT INTO CaLamViec (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca Sáng', '08:00:00', '12:00:00');
IF NOT EXISTS (SELECT 1 FROM CaLamViec WHERE TenCa = N'Ca Chiều')
    INSERT INTO CaLamViec (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca Chiều', '13:00:00', '17:00:00');
IF NOT EXISTS (SELECT 1 FROM CaLamViec WHERE TenCa = N'Ca Hành chính')
    INSERT INTO CaLamViec (TenCa, GioBatDau, GioKetThuc) VALUES (N'Ca Hành chính', '08:00:00', '17:00:00');

-- 4. NHÂN VIÊN VÀ QUYẾT ĐỊNH TUYỂN DỤNG
DECLARE @pbBGD int = (SELECT MaPhongBan FROM PhongBan WHERE TenPhongBan = N'Ban Giám Đốc');
DECLARE @pbHCNS int = (SELECT MaPhongBan FROM PhongBan WHERE TenPhongBan = N'Phòng Hành chính - Nhân sự');
DECLARE @pbKTSX int = (SELECT MaPhongBan FROM PhongBan WHERE TenPhongBan = N'Phòng Kỹ thuật & Sản xuất');
DECLARE @pbKTTC int = (SELECT MaPhongBan FROM PhongBan WHERE TenPhongBan = N'Phòng Kế toán - Tài chính');

DECLARE @cvGD int = (SELECT MaChucVu FROM ChucVu WHERE TenChucVu = N'Giám đốc');
DECLARE @cvTP int = (SELECT MaChucVu FROM ChucVu WHERE TenChucVu = N'Trưởng phòng');
DECLARE @cvKTV int = (SELECT MaChucVu FROM ChucVu WHERE TenChucVu = N'Kỹ thuật viên');

-- NV1: Nguyễn Văn An (Giám đốc)
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen = N'Nguyễn Văn An' AND NgaySinh = '1980-05-15')
BEGIN
    INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DienThoai, TrangThai)
    VALUES (N'Nguyễn Văn An', '1980-05-15', N'Nam', '0901234567', N'Chính thức');
    DECLARE @nv1 int = SCOPE_IDENTITY();

    INSERT INTO QuyetDinhTuyenDung (SoQD, MaNV, MaPhongBan, NgayQD, TuNgay, DenNgay, LuongThuViec, NoiDung, KetQua)
    VALUES ('QDTD-2024-001', @nv1, @pbBGD, '2024-01-01', '2024-01-02', '2024-02-29', 25000000, N'Tuyển dụng vị trí Giám đốc điều hành', N'Đạt');

    INSERT INTO HopDong (SoHD, MaNV, LoaiHD, NgayKy, TuNgay, DenNgay, NoiDung)
    VALUES ('HD-2024-001', @nv1, N'Dài hạn', '2024-02-29', '2024-03-01', NULL, N'Hợp đồng lao động không xác định thời hạn');

    INSERT INTO HoSoBaoHiem (MaNV, SoBHXH, SoBHYT, TuNgay, DenNgay, TyLeBHXH, TyLeBHYT)
    VALUES (@nv1, 'BHXH7900112233', 'BHYT7900112233', '2024-03-01', NULL, 0.0800, 0.0150);

    INSERT INTO QuyetDinhLuong (SoQD, MaNV, NgayQD, TuNgay, DenNgay, LuongCoBan, PhuCapLuong, PhuCapDocHai)
    VALUES ('QDL-2024-001', @nv1, '2024-02-29', '2024-03-01', NULL, 30000000, 3000000, 0);

    INSERT INTO QuyetDinhChucVu (SoQD, MaNV, MaChucVu, NgayQD, TuNgay, DenNgay, PhuCapChucVu)
    VALUES ('QDCV-2024-001', @nv1, @cvGD, '2024-02-29', '2024-03-01', NULL, 5000000);
END

-- NV2: Trần Thị Bích (Trưởng phòng Nhân sự)
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen = N'Trần Thị Bích' AND NgaySinh = '1985-08-20')
BEGIN
    INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DienThoai, TrangThai)
    VALUES (N'Trần Thị Bích', '1985-08-20', N'Nữ', '0912345678', N'Chính thức');
    DECLARE @nv2 int = SCOPE_IDENTITY();

    INSERT INTO QuyetDinhTuyenDung (SoQD, MaNV, MaPhongBan, NgayQD, TuNgay, DenNgay, LuongThuViec, NoiDung, KetQua)
    VALUES ('QDTD-2024-002', @nv2, @pbHCNS, '2024-03-01', '2024-03-05', '2024-04-30', 15000000, N'Tuyển dụng Trưởng phòng Nhân sự', N'Đạt');

    INSERT INTO HopDong (SoHD, MaNV, LoaiHD, NgayKy, TuNgay, DenNgay, NoiDung)
    VALUES ('HD-2024-002', @nv2, N'Dài hạn', '2024-04-30', '2024-05-01', NULL, N'Hợp đồng lao động dài hạn');

    INSERT INTO HoSoBaoHiem (MaNV, SoBHXH, SoBHYT, TuNgay, DenNgay, TyLeBHXH, TyLeBHYT)
    VALUES (@nv2, 'BHXH7900223344', 'BHYT7900223344', '2024-05-01', NULL, 0.0800, 0.0150);

    INSERT INTO QuyetDinhLuong (SoQD, MaNV, NgayQD, TuNgay, DenNgay, LuongCoBan, PhuCapLuong, PhuCapDocHai)
    VALUES ('QDL-2024-002', @nv2, '2024-04-30', '2024-05-01', NULL, 18000000, 1500000, 0);

    INSERT INTO QuyetDinhChucVu (SoQD, MaNV, MaChucVu, NgayQD, TuNgay, DenNgay, PhuCapChucVu)
    VALUES ('QDCV-2024-002', @nv2, @cvTP, '2024-04-30', '2024-05-01', NULL, 2500000);
END

-- NV3: Lê Hoàng Cường (Kỹ sư sản xuất - HĐ dài hạn, có phụ cấp độc hại)
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen = N'Lê Hoàng Cường' AND NgaySinh = '1992-11-10')
BEGIN
    INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DienThoai, TrangThai)
    VALUES (N'Lê Hoàng Cường', '1992-11-10', N'Nam', '0923456789', N'Chính thức');
    DECLARE @nv3 int = SCOPE_IDENTITY();

    INSERT INTO QuyetDinhTuyenDung (SoQD, MaNV, MaPhongBan, NgayQD, TuNgay, DenNgay, LuongThuViec, NoiDung, KetQua)
    VALUES ('QDTD-2025-003', @nv3, @pbKTSX, '2025-01-02', '2025-01-05', '2025-02-28', 10000000, N'Tuyển dụng Kỹ sư pha chế sơn', N'Đạt');

    INSERT INTO HopDong (SoHD, MaNV, LoaiHD, NgayKy, TuNgay, DenNgay, NoiDung)
    VALUES ('HD-2025-003', @nv3, N'Dài hạn', '2025-02-28', '2025-03-01', NULL, N'Hợp đồng chính thức dài hạn');

    INSERT INTO HoSoBaoHiem (MaNV, SoBHXH, SoBHYT, TuNgay, DenNgay, TyLeBHXH, TyLeBHYT)
    VALUES (@nv3, 'BHXH7900334455', 'BHYT7900334455', '2025-03-01', NULL, 0.0800, 0.0150);

    INSERT INTO QuyetDinhLuong (SoQD, MaNV, NgayQD, TuNgay, DenNgay, LuongCoBan, PhuCapLuong, PhuCapDocHai)
    VALUES ('QDL-2025-003', @nv3, '2025-02-28', '2025-03-01', NULL, 12000000, 1000000, 800000);

    INSERT INTO QuyetDinhChucVu (SoQD, MaNV, MaChucVu, NgayQD, TuNgay, DenNgay, PhuCapChucVu)
    VALUES ('QDCV-2025-003', @nv3, @cvKTV, '2025-02-28', '2025-03-01', NULL, 1000000);
END

-- NV4: Phạm Minh Dũng (Kỹ thuật viên - HĐ ngắn hạn 1 năm, không trích BHXH)
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen = N'Phạm Minh Dũng' AND NgaySinh = '1995-04-25')
BEGIN
    INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DienThoai, TrangThai)
    VALUES (N'Phạm Minh Dũng', '1995-04-25', N'Nam', '0934567890', N'Chính thức');
    DECLARE @nv4 int = SCOPE_IDENTITY();

    INSERT INTO QuyetDinhTuyenDung (SoQD, MaNV, MaPhongBan, NgayQD, TuNgay, DenNgay, LuongThuViec, NoiDung, KetQua)
    VALUES ('QDTD-2025-004', @nv4, @pbKTSX, '2025-06-01', '2025-06-02', '2025-07-31', 8000000, N'Thử việc Kỹ thuật viên bảo trì', N'Đạt');

    INSERT INTO HopDong (SoHD, MaNV, LoaiHD, NgayKy, TuNgay, DenNgay, NoiDung)
    VALUES ('HD-2025-004', @nv4, N'Ngắn hạn', '2025-07-31', '2025-08-01', '2026-12-31', N'Hợp đồng thời hạn 1 năm 5 tháng');

    INSERT INTO QuyetDinhLuong (SoQD, MaNV, NgayQD, TuNgay, DenNgay, LuongCoBan, PhuCapLuong, PhuCapDocHai)
    VALUES ('QDL-2025-004', @nv4, '2025-07-31', '2025-08-01', NULL, 9000000, 500000, 500000);
END

-- NV5: Hoàng Thu Em (Thử việc trong tháng 09/2026, kết quả Đạt, chuyển sang Chờ hợp đồng)
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen = N'Hoàng Thu Em' AND NgaySinh = '1998-09-12')
BEGIN
    INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DienThoai, TrangThai)
    VALUES (N'Hoàng Thu Em', '1998-09-12', N'Nữ', '0945678901', N'Chờ hợp đồng');
    DECLARE @nv5 int = SCOPE_IDENTITY();

    INSERT INTO QuyetDinhTuyenDung (SoQD, MaNV, MaPhongBan, NgayQD, TuNgay, DenNgay, LuongThuViec, NoiDung, KetQua)
    VALUES ('QDTD-2026-005', @nv5, @pbHCNS, '2026-07-30', '2026-08-01', '2026-09-30', 7000000, N'Thử việc Chuyên viên Tuyển dụng', N'Đạt');
END

-- NV6: Vũ Trọng Phát (Thử việc Không đạt, Kết thúc thử việc)
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen = N'Vũ Trọng Phát' AND NgaySinh = '1996-03-18')
BEGIN
    INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DienThoai, TrangThai)
    VALUES (N'Vũ Trọng Phát', '1996-03-18', N'Nam', '0956789012', N'Kết thúc thử việc');
    DECLARE @nv6 int = SCOPE_IDENTITY();

    INSERT INTO QuyetDinhTuyenDung (SoQD, MaNV, MaPhongBan, NgayQD, TuNgay, DenNgay, LuongThuViec, NoiDung, KetQua)
    VALUES ('QDTD-2026-006', @nv6, @pbKTSX, '2026-06-25', '2026-07-01', '2026-08-31', 6500000, N'Thử việc vận hành máy', N'Không đạt');
END

-- NV7: Đặng Lan Hương (Đang thử việc tháng 10/2026, chưa hết hạn để demo chặn đánh giá sớm)
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen = N'Đặng Lan Hương' AND NgaySinh = '1999-12-05')
BEGIN
    INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, DienThoai, TrangThai)
    VALUES (N'Đặng Lan Hương', '1999-12-05', N'Nữ', '0967890123', N'Thử việc');
    DECLARE @nv7 int = SCOPE_IDENTITY();

    INSERT INTO QuyetDinhTuyenDung (SoQD, MaNV, MaPhongBan, NgayQD, TuNgay, DenNgay, LuongThuViec, NoiDung, KetQua)
    VALUES ('QDTD-2026-007', @nv7, @pbKTTC, '2026-09-28', '2026-10-01', '2026-11-30', 7500000, N'Thử việc Kế toán viên', NULL);
END

-- 5. CHẤM CÔNG VÀ PHỤ TRỘI THÁNG 09/2026 (Đã kết thúc, đầy đủ để tính & chốt bảng lương)
DECLARE @idNV3 int = (SELECT MaNV FROM NhanVien WHERE HoTen = N'Lê Hoàng Cường');
DECLARE @idNV4 int = (SELECT MaNV FROM NhanVien WHERE HoTen = N'Phạm Minh Dũng');
DECLARE @idNV5 int = (SELECT MaNV FROM NhanVien WHERE HoTen = N'Hoàng Thu Em');
DECLARE @caSang int = (SELECT MaCa FROM CaLamViec WHERE TenCa = N'Ca Sáng');
DECLARE @caChieu int = (SELECT MaCa FROM CaLamViec WHERE TenCa = N'Ca Chiều');

-- Nạp công mẫu 20 ngày trong tháng 09/2026 cho NV3, NV4, NV5
-- Mỗi ngày 2 ca: Ca Sáng (4h), Ca Chiều (4h) = 8h/ngày x 20 ngày = 160h
DECLARE @curDate date = '2026-09-01';
WHILE @curDate <= '2026-09-28'
BEGIN
    -- Bỏ qua Thứ 7, Chủ Nhật (DATEPART(WEEKDAY) = 1 (CN), 7 (T7))
    IF DATEPART(WEEKDAY, @curDate) NOT IN (1, 7)
    BEGIN
        -- NV3
        IF NOT EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @idNV3 AND Ngay = @curDate AND MaCa = @caSang)
            INSERT INTO ChamCong (MaNV, Ngay, MaCa, SoGioThucTe) VALUES (@idNV3, @curDate, @caSang, 4.00);
        IF NOT EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @idNV3 AND Ngay = @curDate AND MaCa = @caChieu)
            INSERT INTO ChamCong (MaNV, Ngay, MaCa, SoGioThucTe) VALUES (@idNV3, @curDate, @caChieu, 4.00);

        -- NV4
        IF NOT EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @idNV4 AND Ngay = @curDate AND MaCa = @caSang)
            INSERT INTO ChamCong (MaNV, Ngay, MaCa, SoGioThucTe) VALUES (@idNV4, @curDate, @caSang, 4.00);
        IF NOT EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @idNV4 AND Ngay = @curDate AND MaCa = @caChieu)
            INSERT INTO ChamCong (MaNV, Ngay, MaCa, SoGioThucTe) VALUES (@idNV4, @curDate, @caChieu, 4.00);

        -- NV5
        IF NOT EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @idNV5 AND Ngay = @curDate AND MaCa = @caSang)
            INSERT INTO ChamCong (MaNV, Ngay, MaCa, SoGioThucTe) VALUES (@idNV5, @curDate, @caSang, 4.00);
        IF NOT EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @idNV5 AND Ngay = @curDate AND MaCa = @caChieu)
            INSERT INTO ChamCong (MaNV, Ngay, MaCa, SoGioThucTe) VALUES (@idNV5, @curDate, @caChieu, 4.00);
    END
    SET @curDate = DATEADD(DAY, 1, @curDate);
END

-- Phiếu phụ trội tháng 09/2026: Đã xác nhận
IF NOT EXISTS (SELECT 1 FROM PhieuPhuTroi WHERE SoPhieu = 'PT-202609-01')
BEGIN
    INSERT INTO PhieuPhuTroi (SoPhieu, NgayPhuTroi, GioBatDau, SoGio, HinhThuc, TrangThai)
    VALUES ('PT-202609-01', '2026-09-15', '17:30:00', 3.00, N'Tăng ca', N'Đã xác nhận');
    DECLARE @pt1 int = SCOPE_IDENTITY();

    INSERT INTO ChiTietPhuTroi (MaPhieuPT, MaNV) VALUES (@pt1, @idNV3);
    INSERT INTO ChiTietPhuTroi (MaPhieuPT, MaNV) VALUES (@pt1, @idNV4);
END

-- 6. QUYẾT ĐỊNH THƯỞNG PHẠT THÁNG 09/2026
DECLARE @idNV1 int = (SELECT MaNV FROM NhanVien WHERE HoTen = N'Nguyễn Văn An');
DECLARE @idNV2 int = (SELECT MaNV FROM NhanVien WHERE HoTen = N'Trần Thị Bích');

-- QĐ1: Thưởng qua lương cá nhân NV3 tháng 09/2026 (Đã ký)
IF NOT EXISTS (SELECT 1 FROM QuyetDinhThuongPhat WHERE SoQD = 'QD-KT-202609-01')
BEGIN
    INSERT INTO QuyetDinhThuongPhat 
    (SoQD, LoaiQD, MaNV, MaPhongBan, NgayLap, NoiDung, SoTien, CachThanhToan, KyApDung, TrangThai, MaNguoiKy, NgayKy)
    VALUES 
    ('QD-KT-202609-01', N'Khen thưởng', @idNV3, NULL, '2026-09-10', N'Thưởng sáng kiến kỹ thuật sơn mới', 1000000, N'Qua lương', '2026-09-01', N'Đã ký', @idNV1, '2026-09-12');
    DECLARE @qd1 int = SCOPE_IDENTITY();

    INSERT INTO ChiTietQuyetDinh (MaQD, MaNV, SoTien, MaBangLuong)
    VALUES (@qd1, @idNV3, 1000000, NULL);
END

-- QĐ2: Kỷ luật qua lương cá nhân NV4 tháng 09/2026 (Đã ký)
IF NOT EXISTS (SELECT 1 FROM QuyetDinhThuongPhat WHERE SoQD = 'QD-KL-202609-02')
BEGIN
    INSERT INTO QuyetDinhThuongPhat 
    (SoQD, LoaiQD, MaNV, MaPhongBan, NgayLap, NoiDung, SoTien, CachThanhToan, KyApDung, TrangThai, MaNguoiKy, NgayKy)
    VALUES 
    ('QD-KL-202609-02', N'Kỷ luật', @idNV4, NULL, '2026-09-18', N'Vi phạm quy định bảo hộ lao động', 200000, N'Qua lương', '2026-09-01', N'Đã ký', @idNV1, '2026-09-20');
    DECLARE @qd2 int = SCOPE_IDENTITY();

    INSERT INTO ChiTietQuyetDinh (MaQD, MaNV, SoTien, MaBangLuong)
    VALUES (@qd2, @idNV4, 200000, NULL);
END

-- QĐ3: Khen thưởng Trực tiếp cá nhân NV2 (Đã ký, chưa lập phiếu Chi - để demo lập phiếu Thu/Chi)
IF NOT EXISTS (SELECT 1 FROM QuyetDinhThuongPhat WHERE SoQD = 'QD-KT-202609-03')
BEGIN
    INSERT INTO QuyetDinhThuongPhat 
    (SoQD, LoaiQD, MaNV, MaPhongBan, NgayLap, NoiDung, SoTien, CachThanhToan, KyApDung, TrangThai, MaNguoiKy, NgayKy)
    VALUES 
    ('QD-KT-202609-03', N'Khen thưởng', @idNV2, NULL, '2026-09-22', N'Khen thưởng hoàn thành xuất sắc đợt tuyển dụng Q3', 2000000, N'Trực tiếp', NULL, N'Đã ký', @idNV1, '2026-09-24');
END

-- QĐ4: Kỷ luật Trực tiếp cá nhân NV4 (Đã ký và ĐÃ lập phiếu Thu - để demo kiểm tra chặn lập lặp)
IF NOT EXISTS (SELECT 1 FROM QuyetDinhThuongPhat WHERE SoQD = 'QD-KL-202609-04')
BEGIN
    INSERT INTO QuyetDinhThuongPhat 
    (SoQD, LoaiQD, MaNV, MaPhongBan, NgayLap, NoiDung, SoTien, CachThanhToan, KyApDung, TrangThai, MaNguoiKy, NgayKy)
    VALUES 
    ('QD-KL-202609-04', N'Kỷ luật', @idNV4, NULL, '2026-09-10', N'Bồi thường hỏng dụng cụ làm việc', 300000, N'Trực tiếp', NULL, N'Đã ký', @idNV1, '2026-09-11');
    DECLARE @qd4 int = SCOPE_IDENTITY();

    INSERT INTO PhieuThuChi (SoPhieu, MaQD, NgayLap, LoaiPhieu, SoTien)
    VALUES ('PTC-THU-001', @qd4, '2026-09-12', N'Thu', 300000);
END

-- 7. DỮ LIỆU THÁNG 10/2026 (Tháng đang mở - có ca chưa chấm, có phụ trội kế hoạch, có quyết định nháp để thử các điều kiện chặn)
IF NOT EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @idNV3 AND Ngay = '2026-10-05' AND MaCa = @caSang)
    INSERT INTO ChamCong (MaNV, Ngay, MaCa, SoGioThucTe) VALUES (@idNV3, '2026-10-05', @caSang, NULL); -- Chưa chấm

IF NOT EXISTS (SELECT 1 FROM PhieuPhuTroi WHERE SoPhieu = 'PT-202610-PLAN')
BEGIN
    INSERT INTO PhieuPhuTroi (SoPhieu, NgayPhuTroi, GioBatDau, SoGio, HinhThuc, TrangThai)
    VALUES ('PT-202610-PLAN', '2026-10-25', '18:00:00', 2.00, N'Tăng ca', N'Kế hoạch');
    DECLARE @ptPlan int = SCOPE_IDENTITY();
    INSERT INTO ChiTietPhuTroi (MaPhieuPT, MaNV) VALUES (@ptPlan, @idNV3);
END

IF NOT EXISTS (SELECT 1 FROM QuyetDinhThuongPhat WHERE SoQD = 'QD-KT-202610-DRAFT')
BEGIN
    INSERT INTO QuyetDinhThuongPhat 
    (SoQD, LoaiQD, MaNV, MaPhongBan, NgayLap, NoiDung, SoTien, CachThanhToan, KyApDung, TrangThai, MaNguoiKy, NgayKy)
    VALUES 
    ('QD-KT-202610-DRAFT', N'Khen thưởng', @idNV3, NULL, '2026-10-15', N'Đề xuất khen thưởng tiến độ dự án', 500000, N'Qua lương', '2026-10-01', N'Nháp', NULL, NULL);
    DECLARE @qdDraft int = SCOPE_IDENTITY();
    INSERT INTO ChiTietQuyetDinh (MaQD, MaNV, SoTien, MaBangLuong) VALUES (@qdDraft, @idNV3, 500000, NULL);
END

PRINT N'Nạp dữ liệu mẫu DEMO thành công!';
GO
