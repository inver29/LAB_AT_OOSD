-- ==============================================================================
-- HỆ THỐNG QUẢN LÝ CHO THUÊ VĂN PHÒNG CAO ỐC - BÀI LAB 08
-- Sinh viên thực hiện: Nguyễn Hoài An - MSSV: 1250080245 - Lớp: 12_ĐH_CNPM1
-- Script 01: Tạo Cơ sở dữ liệu và các bảng nghiệp vụ, ràng buộc toàn vẹn
-- ==============================================================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'QuanLyChoThueVanPhong_Bai08')
BEGIN
    CREATE DATABASE QuanLyChoThueVanPhong_Bai08;
END
GO

USE QuanLyChoThueVanPhong_Bai08;
GO

-- 1. BẢNG TẦNG (Tang)
IF OBJECT_ID('dbo.Tang', 'U') IS NOT NULL DROP TABLE dbo.Tang;
CREATE TABLE dbo.Tang (
    MaTang VARCHAR(20) NOT NULL,
    TenTang NVARCHAR(50) NOT NULL,
    ViTriDoCao INT NOT NULL DEFAULT 1,
    HeSoGiaTang FLOAT NOT NULL DEFAULT 1.0,
    MoTa NVARCHAR(255) NULL,
    CONSTRAINT PK_Tang PRIMARY KEY (MaTang),
    CONSTRAINT CK_Tang_ViTri CHECK (ViTriDoCao >= 1)
);
GO

-- 2. BẢNG PHÒNG VĂN PHÒNG (Phong)
IF OBJECT_ID('dbo.Phong', 'U') IS NOT NULL DROP TABLE dbo.Phong;
CREATE TABLE dbo.Phong (
    MaPhong VARCHAR(20) NOT NULL,
    MaTang VARCHAR(20) NOT NULL,
    TenPhong NVARCHAR(100) NOT NULL,
    DienTich FLOAT NOT NULL,
    HuongPhong NVARCHAR(50) NOT NULL,
    DonGiaTheoM2 DECIMAL(18, 2) NOT NULL,
    TinhTrang NVARCHAR(50) NOT NULL DEFAULT N'Trống',
    MoTaKetCau NVARCHAR(500) NULL,
    CONSTRAINT PK_Phong PRIMARY KEY (MaPhong),
    CONSTRAINT FK_Phong_Tang FOREIGN KEY (MaTang) REFERENCES dbo.Tang(MaTang),
    CONSTRAINT CK_Phong_DienTich CHECK (DienTich > 0),
    CONSTRAINT CK_Phong_DonGia CHECK (DonGiaTheoM2 > 0),
    CONSTRAINT CK_Phong_TinhTrang CHECK (TinhTrang IN (N'Trống', N'Đang cho thuê', N'Đang bảo trì/tu sửa', N'Đang vi phạm'))
);
GO

-- 3. BẢNG KHÁCH HÀNG / DOANH NGHIỆP THUÊ (KhachHang)
IF OBJECT_ID('dbo.KhachHang', 'U') IS NOT NULL DROP TABLE dbo.KhachHang;
CREATE TABLE dbo.KhachHang (
    MaKH VARCHAR(20) NOT NULL,
    TenCongTy NVARCHAR(200) NOT NULL,
    NguoiDaiDien NVARCHAR(100) NOT NULL,
    ChucVuDaiDien NVARCHAR(100) NULL,
    MaSoThue VARCHAR(20) NOT NULL,
    SoDienThoai VARCHAR(20) NOT NULL,
    Email VARCHAR(100) NULL,
    DiaChiTruSo NVARCHAR(255) NULL,
    CONSTRAINT PK_KhachHang PRIMARY KEY (MaKH),
    CONSTRAINT UQ_KhachHang_MST UNIQUE (MaSoThue)
);
GO

-- 4. BẢNG HỢP ĐỒNG THUÊ VĂN PHÒNG (HopDongThue)
IF OBJECT_ID('dbo.HopDongThue', 'U') IS NOT NULL DROP TABLE dbo.HopDongThue;
CREATE TABLE dbo.HopDongThue (
    MaHD VARCHAR(20) NOT NULL,
    SoHopDong VARCHAR(50) NOT NULL,
    MaKH VARCHAR(20) NOT NULL,
    NgayKy DATE NOT NULL,
    NgayBatDau DATE NOT NULL,
    NgayHetHan DATE NOT NULL,
    ThoiGianThue INT NOT NULL, -- Ít nhất 6 tháng
    TienCoc DECIMAL(18, 2) NOT NULL DEFAULT 0,
    TongTienThueThang DECIMAL(18, 2) NOT NULL DEFAULT 0,
    TienTraTruoc6Thang DECIMAL(18, 2) NOT NULL DEFAULT 0,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang hiệu lực',
    CONSTRAINT PK_HopDongThue PRIMARY KEY (MaHD),
    CONSTRAINT FK_HopDong_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH),
    CONSTRAINT UQ_HopDong_SoHD UNIQUE (SoHopDong),
    CONSTRAINT CK_HopDong_ThoiGianThue CHECK (ThoiGianThue >= 6),
    CONSTRAINT CK_HopDong_Ngay CHECK (NgayHetHan > NgayBatDau),
    CONSTRAINT CK_HopDong_TrangThai CHECK (TrangThai IN (N'Mới ký', N'Đang hiệu lực', N'Đã gia hạn', N'Tạm dừng vi phạm', N'Đã thanh lý'))
);
GO

-- 5. BẢNG CHI TIẾT HỢP ĐỒNG THUÊ (ChiTietHopDongThue)
-- Khách có thể thuê cùng lúc nhiều văn phòng
IF OBJECT_ID('dbo.ChiTietHopDongThue', 'U') IS NOT NULL DROP TABLE dbo.ChiTietHopDongThue;
CREATE TABLE dbo.ChiTietHopDongThue (
    MaHD VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    DienTichThue FLOAT NOT NULL,
    DonGiaThueThang DECIMAL(18, 2) NOT NULL,
    NgayBanGiao DATE NOT NULL,
    CONSTRAINT PK_ChiTietHopDongThue PRIMARY KEY (MaHD, MaPhong),
    CONSTRAINT FK_CTHD_HopDong FOREIGN KEY (MaHD) REFERENCES dbo.HopDongThue(MaHD),
    CONSTRAINT FK_CTHD_Phong FOREIGN KEY (MaPhong) REFERENCES dbo.Phong(MaPhong),
    CONSTRAINT CK_CTHD_DonGia CHECK (DonGiaThueThang > 0)
);
GO

-- 6. BẢNG PHIẾU THANH TOÁN TIỀN THUÊ (PhieuThanhToanTienThue)
-- Khách trả trước 6 tháng đầu, từ tháng thứ 7 trả vào đầu mỗi tháng
IF OBJECT_ID('dbo.PhieuThanhToanTienThue', 'U') IS NOT NULL DROP TABLE dbo.PhieuThanhToanTienThue;
CREATE TABLE dbo.PhieuThanhToanTienThue (
    MaPhieuThu VARCHAR(20) NOT NULL,
    MaHD VARCHAR(20) NOT NULL,
    DotThanhToan NVARCHAR(100) NOT NULL, -- 'Trả trước 6 tháng đầu' hoặc 'Tiền thuê tháng 7/2026'
    KyThang INT NOT NULL,
    KyNam INT NOT NULL,
    SoTien DECIMAL(18, 2) NOT NULL,
    NgayThu DATE NOT NULL,
    HinhThucThanhToan NVARCHAR(50) NOT NULL DEFAULT N'Chuyển khoản',
    NguoiThu NVARCHAR(100) NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đã thu',
    CONSTRAINT PK_PhieuThanhToan PRIMARY KEY (MaPhieuThu),
    CONSTRAINT FK_PhieuThu_HopDong FOREIGN KEY (MaHD) REFERENCES dbo.HopDongThue(MaHD),
    CONSTRAINT CK_PhieuThu_SoTien CHECK (SoTien > 0)
);
GO

-- 7. BẢNG DANH MỤC DỊCH VỤ (DichVu)
IF OBJECT_ID('dbo.DichVu', 'U') IS NOT NULL DROP TABLE dbo.DichVu;
CREATE TABLE dbo.DichVu (
    MaDV VARCHAR(20) NOT NULL,
    TenDV NVARCHAR(100) NOT NULL,
    DonGia DECIMAL(18, 2) NOT NULL,
    DonViTinh NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(255) NULL,
    CONSTRAINT PK_DichVu PRIMARY KEY (MaDV),
    CONSTRAINT CK_DichVu_DonGia CHECK (DonGia >= 0)
);
GO

-- 8. BẢNG HÓA ĐƠN DỊCH VỤ HÀNG THÁNG (HoaDonDichVu)
-- Chi phí điện, nước, internet... thanh toán vào cuối tháng
IF OBJECT_ID('dbo.HoaDonDichVu', 'U') IS NOT NULL DROP TABLE dbo.HoaDonDichVu;
CREATE TABLE dbo.HoaDonDichVu (
    MaHDDV VARCHAR(20) NOT NULL,
    MaHD VARCHAR(20) NOT NULL,
    KyThang INT NOT NULL,
    KyNam INT NOT NULL,
    NgayLap DATE NOT NULL,
    NgayHetHanThanhToan DATE NOT NULL,
    TongTienDichVu DECIMAL(18, 2) NOT NULL DEFAULT 0,
    TrangThaiThanhToan NVARCHAR(50) NOT NULL DEFAULT N'Chưa thanh toán',
    NgayThanhToan DATE NULL,
    CONSTRAINT PK_HoaDonDichVu PRIMARY KEY (MaHDDV),
    CONSTRAINT FK_HDDV_HopDong FOREIGN KEY (MaHD) REFERENCES dbo.HopDongThue(MaHD),
    CONSTRAINT CK_HDDV_TrangThai CHECK (TrangThaiThanhToan IN (N'Chưa thanh toán', N'Đã thanh toán', N'Quá hạn'))
);
GO

-- 9. BẢNG CHI TIẾT HÓA ĐƠN DỊCH VỤ (ChiTietDichVu)
IF OBJECT_ID('dbo.ChiTietDichVu', 'U') IS NOT NULL DROP TABLE dbo.ChiTietDichVu;
CREATE TABLE dbo.ChiTietDichVu (
    MaHDDV VARCHAR(20) NOT NULL,
    MaDV VARCHAR(20) NOT NULL,
    ChiSoDau FLOAT NULL,
    ChiSoCuoi FLOAT NULL,
    SoLuongTieuThu FLOAT NOT NULL,
    DonGia DECIMAL(18, 2) NOT NULL,
    ThanhTien DECIMAL(18, 2) NOT NULL,
    CONSTRAINT PK_ChiTietDichVu PRIMARY KEY (MaHDDV, MaDV),
    CONSTRAINT FK_CTDV_HoaDon FOREIGN KEY (MaHDDV) REFERENCES dbo.HoaDonDichVu(MaHDDV),
    CONSTRAINT FK_CTDV_DichVu FOREIGN KEY (MaDV) REFERENCES dbo.DichVu(MaDV),
    CONSTRAINT CK_CTDV_ThanhTien CHECK (ThanhTien >= 0)
);
GO

-- 10. BẢNG PHIẾU GIA HẠN THUÊ VĂN PHÒNG (PhieuGiaHan)
-- Quy định: thời gian gia hạn <= 1/2 thời gian thuê gốc; không còn nợ tiền phòng & dịch vụ
IF OBJECT_ID('dbo.PhieuGiaHan', 'U') IS NOT NULL DROP TABLE dbo.PhieuGiaHan;
CREATE TABLE dbo.PhieuGiaHan (
    MaPGH VARCHAR(20) NOT NULL,
    SoPGH VARCHAR(50) NOT NULL,
    MaHD VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    NgayLap DATE NOT NULL,
    ThoiGianGiaHan INT NOT NULL, -- Số tháng gia hạn
    NgayBatDauGiaHan DATE NOT NULL,
    NgayKetThucGiaHan DATE NOT NULL,
    DonGiaGiaHanThang DECIMAL(18, 2) NOT NULL,
    NguoiDuyet NVARCHAR(100) NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đã phê duyệt',
    GhiChu NVARCHAR(255) NULL,
    CONSTRAINT PK_PhieuGiaHan PRIMARY KEY (MaPGH),
    CONSTRAINT FK_PGH_HopDong FOREIGN KEY (MaHD) REFERENCES dbo.HopDongThue(MaHD),
    CONSTRAINT FK_PGH_Phong FOREIGN KEY (MaPhong) REFERENCES dbo.Phong(MaPhong),
    CONSTRAINT UQ_PhieuGiaHan_SoPGH UNIQUE (SoPGH),
    CONSTRAINT CK_PGH_ThoiGian CHECK (ThoiGianGiaHan > 0)
);
GO

-- 11. BẢNG NHÂN VIÊN VĂN PHÒNG (NhanVienVanPhong)
-- Phục vụ công tác an toàn, an ninh của cao ốc
IF OBJECT_ID('dbo.NhanVienVanPhong', 'U') IS NOT NULL DROP TABLE dbo.NhanVienVanPhong;
CREATE TABLE dbo.NhanVienVanPhong (
    MaNV VARCHAR(20) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    HinhAnh NVARCHAR(255) NULL,
    MaKH VARCHAR(20) NOT NULL, -- Thuộc công ty nào
    ChucVu NVARCHAR(100) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL, -- Ở phòng mấy
    MaTang VARCHAR(20) NOT NULL,  -- Ở tầng mấy
    SoCCCD VARCHAR(20) NOT NULL,
    SoDienThoai VARCHAR(20) NOT NULL,
    NgayTiepNhan DATE NOT NULL,
    TrangThaiLamViec NVARCHAR(50) NOT NULL DEFAULT N'Đang làm việc',
    CONSTRAINT PK_NhanVienVanPhong PRIMARY KEY (MaNV),
    CONSTRAINT FK_NV_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH),
    CONSTRAINT FK_NV_Phong FOREIGN KEY (MaPhong) REFERENCES dbo.Phong(MaPhong),
    CONSTRAINT FK_NV_Tang FOREIGN KEY (MaTang) REFERENCES dbo.Tang(MaTang),
    CONSTRAINT UQ_NhanVien_CCCD UNIQUE (SoCCCD)
);
GO

-- 12. BẢNG THẺ GIỮ XE (TheGiuXe)
-- Cấp cho nhân viên làm việc tại cao ốc để quản lý việc gửi xe
IF OBJECT_ID('dbo.TheGiuXe', 'U') IS NOT NULL DROP TABLE dbo.TheGiuXe;
CREATE TABLE dbo.TheGiuXe (
    MaTheXe VARCHAR(20) NOT NULL,
    MaNV VARCHAR(20) NOT NULL,
    BienSoXe VARCHAR(20) NOT NULL,
    LoaiXe NVARCHAR(50) NOT NULL DEFAULT N'Xe máy',
    NhanHieu NVARCHAR(50) NULL,
    MauXe NVARCHAR(50) NULL,
    NgayCap DATE NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang hoạt động',
    CONSTRAINT PK_TheGiuXe PRIMARY KEY (MaTheXe),
    CONSTRAINT FK_TheXe_NhanVien FOREIGN KEY (MaNV) REFERENCES dbo.NhanVienVanPhong(MaNV),
    CONSTRAINT CK_TheXe_TrangThai CHECK (TrangThai IN (N'Đang hoạt động', N'Bị khóa/Mất thẻ', N'Đã thu hồi'))
);
GO

-- 13. BẢNG BIÊN BẢN KIỂM TRA ĐỊNH KỲ (BienBanKiemTra)
-- Cách 1 năm kiểm tra tình trạng các phòng để tu sửa kịp thời
IF OBJECT_ID('dbo.BienBanKiemTra', 'U') IS NOT NULL DROP TABLE dbo.BienBanKiemTra;
CREATE TABLE dbo.BienBanKiemTra (
    MaBBKT VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    NgayKiemTra DATE NOT NULL,
    NguoiKiemTra NVARCHAR(100) NOT NULL,
    TinhTrangHienTai NVARCHAR(500) NOT NULL,
    CoViPhamKetCau BIT NOT NULL DEFAULT 0,
    KeHoachTuSua NVARCHAR(500) NULL,
    KetLuan NVARCHAR(255) NOT NULL,
    CONSTRAINT PK_BienBanKiemTra PRIMARY KEY (MaBBKT),
    CONSTRAINT FK_BBKT_Phong FOREIGN KEY (MaPhong) REFERENCES dbo.Phong(MaPhong)
);
GO

-- 14. BẢNG QUYẾT ĐỊNH XỬ PHẠT VI PHẠM KẾT CẤU (QuyetDinhXuPhat)
-- Xử phạt nếu các phòng tự ý thay đổi, chỉnh sửa kết cấu văn phòng
IF OBJECT_ID('dbo.QuyetDinhXuPhat', 'U') IS NOT NULL DROP TABLE dbo.QuyetDinhXuPhat;
CREATE TABLE dbo.QuyetDinhXuPhat (
    MaQDXP VARCHAR(20) NOT NULL,
    MaBBKT VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    MaKH VARCHAR(20) NOT NULL,
    NgayLap DATE NOT NULL,
    HanhViViPham NVARCHAR(500) NOT NULL,
    MucPhat DECIMAL(18, 2) NOT NULL,
    BienPhapKhacPhuc NVARCHAR(500) NOT NULL,
    HanNopPhat DATE NOT NULL,
    TrangThaiNop NVARCHAR(50) NOT NULL DEFAULT N'Chưa nộp',
    NgayNopPhat DATE NULL,
    CONSTRAINT PK_QuyetDinhXuPhat PRIMARY KEY (MaQDXP),
    CONSTRAINT FK_QDXP_BBKT FOREIGN KEY (MaBBKT) REFERENCES dbo.BienBanKiemTra(MaBBKT),
    CONSTRAINT FK_QDXP_Phong FOREIGN KEY (MaPhong) REFERENCES dbo.Phong(MaPhong),
    CONSTRAINT FK_QDXP_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH),
    CONSTRAINT CK_QDXP_MucPhat CHECK (MucPhat >= 0),
    CONSTRAINT CK_QDXP_TrangThai CHECK (TrangThaiNop IN (N'Chưa nộp', N'Đã nộp'))
);
GO

-- 15. BẢNG KẾ HOẠCH TU SỬA BẢO TRÌ (KeHoachTuSua)
IF OBJECT_ID('dbo.KeHoachTuSua', 'U') IS NOT NULL DROP TABLE dbo.KeHoachTuSua;
CREATE TABLE dbo.KeHoachTuSua (
    MaKHTS VARCHAR(20) NOT NULL,
    MaBBKT VARCHAR(20) NOT NULL,
    MaPhong VARCHAR(20) NOT NULL,
    NoiDungTuSua NVARCHAR(500) NOT NULL,
    ChiPhiDuKien DECIMAL(18, 2) NOT NULL DEFAULT 0,
    NgayBatDau DATE NOT NULL,
    NgayHoanTatDuKien DATE NOT NULL,
    DonViThiCong NVARCHAR(150) NULL,
    TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Lên kế hoạch',
    CONSTRAINT PK_KeHoachTuSua PRIMARY KEY (MaKHTS),
    CONSTRAINT FK_KHTS_BBKT FOREIGN KEY (MaBBKT) REFERENCES dbo.BienBanKiemTra(MaBBKT),
    CONSTRAINT FK_KHTS_Phong FOREIGN KEY (MaPhong) REFERENCES dbo.Phong(MaPhong)
);
GO

-- ==============================================================================
-- TRIGGER KIỂM TRA ĐIỀU KIỆN LẬP PHIẾU GIA HẠN THUÊ VĂN PHÒNG
-- 1. Thời gian gia hạn <= 1/2 thời gian thuê gốc
-- 2. Không còn nợ tiền thuê mặt bằng
-- 3. Đã thanh toán đầy đủ các hóa đơn dịch vụ đã sử dụng
-- ==============================================================================
IF OBJECT_ID('dbo.TR_KiemTraDieuKienGiaHan', 'TR') IS NOT NULL DROP TRIGGER dbo.TR_KiemTraDieuKienGiaHan;
GO
CREATE TRIGGER dbo.TR_KiemTraDieuKienGiaHan
ON dbo.PhieuGiaHan
FOR INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra 1: Thời gian gia hạn <= 1/2 thời gian thuê gốc
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN dbo.HopDongThue hd ON i.MaHD = hd.MaHD
        WHERE i.ThoiGianGiaHan > (hd.ThoiGianThue / 2.0)
    )
    BEGIN
        RAISERROR (N'Lỗi nghiệp vụ: Thời gian gia hạn không được vượt quá 1/2 thời gian thuê của hợp đồng gốc!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Kiểm tra 2: Hóa đơn dịch vụ chưa thanh toán
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN dbo.HoaDonDichVu hddv ON i.MaHD = hddv.MaHD
        WHERE hddv.TrangThaiThanhToan <> N'Đã thanh toán'
    )
    BEGIN
        RAISERROR (N'Lỗi nghiệp vụ: Hợp đồng còn nợ hóa đơn dịch vụ chưa thanh toán, không được phép gia hạn!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO
