SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;
GO

-- Lab08 - Quản lý cho thuê văn phòng ở cao ốc
-- Chạy một lần trên SQL Server; không xóa database/bảng đã có.
CREATE DATABASE QuanLyVanPhong_Lab08;
GO
USE QuanLyVanPhong_Lab08;
GO

-- 1. Tang
CREATE TABLE Tang (
    MaTang int IDENTITY PRIMARY KEY,
    SoTang int NOT NULL UNIQUE
);
GO

-- 2. CongTy
CREATE TABLE CongTy (
    MaCongTy int IDENTITY PRIMARY KEY,
    TenCongTy nvarchar(150) NOT NULL,
    DaiDien nvarchar(100) NOT NULL,
    DienThoai varchar(20) NOT NULL,
    DiaChi nvarchar(200) NULL
);
GO

-- 3. VanPhong
CREATE TABLE VanPhong (
    MaVanPhong int IDENTITY PRIMARY KEY,
    MaTang int NOT NULL REFERENCES Tang(MaTang),
    KyHieu nvarchar(20) NOT NULL UNIQUE,
    Huong nvarchar(30) NOT NULL,
    DienTich decimal(10,2) NOT NULL CHECK (DienTich > 0),
    DonGiaThang decimal(18,2) NOT NULL CHECK (DonGiaThang > 0),
    TinhTrang nvarchar(20) NOT NULL CHECK (TinhTrang IN (N'Đạt',N'Cần sửa'))
);
GO

-- 4. HopDong
CREATE TABLE HopDong (
    MaHopDong int IDENTITY PRIMARY KEY,
    SoHopDong nvarchar(20) NOT NULL UNIQUE,
    MaCongTy int NOT NULL REFERENCES CongTy(MaCongTy),
    NgayKy date NOT NULL,
    TuNgay date NOT NULL,
    DenNgay date NOT NULL,
    NoiDung nvarchar(1000) NOT NULL,
    CHECK (NgayKy <= TuNgay),
    CHECK (DenNgay >= DATEADD(month, 6, TuNgay)),
    CHECK (DenNgay = DATEADD(month, DATEDIFF(month, TuNgay, DenNgay), TuNgay))
);
GO

-- 5. ChiTietHopDong
CREATE TABLE ChiTietHopDong (
    MaChiTietHD int IDENTITY PRIMARY KEY,
    MaHopDong int NOT NULL REFERENCES HopDong(MaHopDong),
    MaVanPhong int NOT NULL REFERENCES VanPhong(MaVanPhong),
    DonGiaThang decimal(18,2) NOT NULL CHECK (DonGiaThang > 0),
    DenNgayHienTai date NOT NULL,
    UNIQUE (MaHopDong, MaVanPhong)
);
GO

-- 6. PhieuGiaHan
CREATE TABLE PhieuGiaHan (
    MaGiaHan int IDENTITY PRIMARY KEY,
    SoPhieu nvarchar(20) NOT NULL UNIQUE,
    MaHopDong int NOT NULL REFERENCES HopDong(MaHopDong),
    NgayLap date NOT NULL,
    NgayDoiChieuPhi date NOT NULL,
    SoThang int NOT NULL CHECK (SoThang > 0),
    CHECK (NgayDoiChieuPhi = NgayLap)
);
GO

-- 7. ChiTietGiaHan
CREATE TABLE ChiTietGiaHan (
    MaGiaHan int NOT NULL REFERENCES PhieuGiaHan(MaGiaHan),
    MaChiTietHD int NOT NULL REFERENCES ChiTietHopDong(MaChiTietHD),
    TuNgay date NOT NULL,
    DenNgayMoi date NOT NULL,
    PRIMARY KEY (MaGiaHan, MaChiTietHD),
    CHECK (DenNgayMoi > TuNgay)
);
GO

-- 8. DichVu
CREATE TABLE DichVu (
    MaDichVu int IDENTITY PRIMARY KEY,
    TenDichVu nvarchar(100) NOT NULL UNIQUE,
    DonViTinh nvarchar(20) NOT NULL,
    DonGia decimal(18,2) NOT NULL CHECK (DonGia >= 0)
);
GO

-- 9. HoaDon
CREATE TABLE HoaDon (
    MaHoaDon int IDENTITY PRIMARY KEY,
    SoHoaDon nvarchar(20) NOT NULL UNIQUE,
    MaHopDong int NOT NULL REFERENCES HopDong(MaHopDong),
    Loai nvarchar(20) NOT NULL CHECK (Loai IN (N'Tiền thuê',N'Dịch vụ')),
    TuNgay date NOT NULL,
    DenNgay date NOT NULL,
    NgayLap date NOT NULL,
    NgayThanhToan date NULL,
    CHECK (DenNgay > TuNgay),
    CHECK (NgayThanhToan IS NULL OR NgayThanhToan >= NgayLap)
);
GO

-- 10. ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    MaHoaDon int NOT NULL REFERENCES HoaDon(MaHoaDon),
    STT smallint NOT NULL CHECK (STT > 0),
    MaChiTietHD int NOT NULL REFERENCES ChiTietHopDong(MaChiTietHD),
    MaDichVu int NULL REFERENCES DichVu(MaDichVu),
    SoLuong decimal(12,2) NOT NULL CHECK (SoLuong > 0),
    DonGia decimal(18,2) NOT NULL CHECK (DonGia >= 0),
    PRIMARY KEY (MaHoaDon, STT)
);
GO

-- 11. NhanVien
CREATE TABLE NhanVien (
    MaNhanVien int IDENTITY PRIMARY KEY,
    MaChiTietHD int NOT NULL REFERENCES ChiTietHopDong(MaChiTietHD),
    HoTen nvarchar(100) NOT NULL,
    ChucVu nvarchar(100) NOT NULL,
    Anh nvarchar(255) NOT NULL,
    DangLamViec bit NOT NULL DEFAULT 1
);
GO

-- 12. TheGiuXe
CREATE TABLE TheGiuXe (
    MaThe int IDENTITY PRIMARY KEY,
    MaNhanVien int NOT NULL REFERENCES NhanVien(MaNhanVien),
    LoaiXe nvarchar(30) NOT NULL,
    BienSo varchar(20) NOT NULL,
    NgayCap date NOT NULL,
    ConHieuLuc bit NOT NULL DEFAULT 1
);
GO

-- 13. KiemTra
CREATE TABLE KiemTra (
    MaKiemTra int IDENTITY PRIMARY KEY,
    MaVanPhong int NOT NULL REFERENCES VanPhong(MaVanPhong),
    NgayKiemTra date NOT NULL,
    KetQua nvarchar(20) NOT NULL CHECK (KetQua IN (N'Đạt',N'Cần sửa')),
    GhiChu nvarchar(500) NULL
);
GO

-- 14. ViPham
CREATE TABLE ViPham (
    MaViPham int IDENTITY PRIMARY KEY,
    MaChiTietHD int NOT NULL REFERENCES ChiTietHopDong(MaChiTietHD),
    MaKiemTra int NULL REFERENCES KiemTra(MaKiemTra),
    NgayLap date NOT NULL,
    NoiDung nvarchar(500) NOT NULL,
    TienPhat decimal(18,2) NOT NULL CHECK (TienPhat > 0)
);
GO

-- Chỉ cho một thẻ còn hiệu lực ứng với cùng một biển số.
CREATE UNIQUE INDEX UX_TheGiuXe_BienSo ON TheGiuXe(BienSo)
WHERE ConHieuLuc = 1;
GO