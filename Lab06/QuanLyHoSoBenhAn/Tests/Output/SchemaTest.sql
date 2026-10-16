SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;
GO
-- BÀI 5 - QUẢN LÝ HỒ SƠ BỆNH ÁN
-- SQL Server. Chạy trên cơ sở dữ liệu mới, không chạy lại trên bảng đã có.
-- Tạo CSDL rồi chọn đúng CSDL trước khi chạy các phần tiếp theo.
IF DB_ID(N'Bai05_AppTest_f6b052798f4a') IS NULL
    CREATE DATABASE Bai05_AppTest_f6b052798f4a;
GO
USE Bai05_AppTest_f6b052798f4a;
GO

-- 1. Bệnh nhân
CREATE TABLE BenhNhan (
    MaBN int IDENTITY(1,1) PRIMARY KEY,
    HoTen nvarchar(100) NOT NULL,
    NgaySinh date NOT NULL,
    GioiTinh nvarchar(10) NULL,
    DiaChi nvarchar(200) NOT NULL,
    DienThoai varchar(15) NOT NULL,
    Anh3x4 nvarchar(255) NOT NULL
 );
GO

-- 2. Hồ sơ bệnh án
CREATE TABLE HoSoBenhAn (
    MaHSBA int IDENTITY(1,1) PRIMARY KEY,
    MaBN int NOT NULL UNIQUE REFERENCES BenhNhan(MaBN),
    NgayCap date NOT NULL,
    NgayDongPhi date NOT NULL,
    HanPhi date NOT NULL,
    PhiThuongNien decimal(12,0) NOT NULL CHECK (PhiThuongNien > 0) ,
    CHECK (NgayDongPhi >= NgayCap AND HanPhi = DATEADD(year, 1, NgayDongPhi))
 );
GO

-- 3. Nhân viên
CREATE TABLE NhanVien (
    MaNV int IDENTITY(1,1) PRIMARY KEY,
    HoTen nvarchar(100) NOT NULL,
    VaiTro varchar(20) NOT NULL CHECK (VaiTro IN ('TiepNhanCSKH','BacSi','XetNghiem','DuocSi','ThuNgan','QuanTri')),
    DienThoai varchar(15) NULL
 );
GO

-- 4. Khoa điều trị
CREATE TABLE KhoaDieuTri (
    MaKhoa int IDENTITY(1,1) PRIMARY KEY,
    TenKhoa nvarchar(100) NOT NULL UNIQUE
 );
GO

-- 5. Lịch hẹn khám
CREATE TABLE LichHenKham (
    MaLichHen int IDENTITY(1,1) PRIMARY KEY,
    MaHSBA int NOT NULL REFERENCES HoSoBenhAn(MaHSBA),
    MaBacSi int NOT NULL REFERENCES NhanVien(MaNV),
    NgayHen datetime2(0) NOT NULL,
    TrangThai nvarchar(20) NOT NULL CHECK (TrangThai IN (N'Đã hẹn',N'Đã đến',N'Đã hủy'))
 );
GO

-- 6. Phiếu khám bệnh
CREATE TABLE PhieuKhamBenh (
    MaPhieuKham int IDENTITY(1,1) PRIMARY KEY,
    MaHSBA int NOT NULL REFERENCES HoSoBenhAn(MaHSBA),
    MaBacSi int NOT NULL REFERENCES NhanVien(MaNV),
    MaKhoa int NOT NULL REFERENCES KhoaDieuTri(MaKhoa),
    MaLichHen int NULL REFERENCES LichHenKham(MaLichHen),
    NgayKham datetime2(0) NOT NULL,
    LoaiKham nvarchar(20) NOT NULL CHECK (LoaiKham IN (N'Điều trị',N'Định kỳ')),
    TrieuChung nvarchar(500) NULL,
    ThanNhiet decimal(3,1) NULL CHECK (ThanNhiet BETWEEN 30 AND 45),
    HuyetAp varchar(15) NULL,
    NhipTim smallint NULL CHECK (NhipTim > 0),
    ChanDoan nvarchar(500) NULL,
    TrangThai nvarchar(30) NOT NULL CHECK (TrangThai IN (N'Chờ khám',N'Đang khám',N'Chờ xét nghiệm',N'Hoàn tất khám',N'Đã thanh toán')),
    TienKham decimal(12,0) NOT NULL CHECK (TienKham >= 0),
    TongVienPhi decimal(12,0) NULL CHECK (TongVienPhi >= 0),
    NgayThanhToan datetime2(0) NULL ,
    CHECK ((TrangThai = N'Đã thanh toán' AND NgayThanhToan IS NOT NULL AND TongVienPhi IS NOT NULL) OR (TrangThai <> N'Đã thanh toán' AND NgayThanhToan IS NULL AND TongVienPhi IS NULL))
 );
GO

-- 7. Loại xét nghiệm
CREATE TABLE LoaiXetNghiem (
    MaLoaiXN int IDENTITY(1,1) PRIMARY KEY,
    TenLoaiXN nvarchar(100) NOT NULL UNIQUE,
    DonGia decimal(12,0) NOT NULL CHECK (DonGia >= 0)
 );
GO

-- 8. Chi tiết xét nghiệm
CREATE TABLE ChiTietXetNghiem (
    MaPhieuKham int NOT NULL REFERENCES PhieuKhamBenh(MaPhieuKham),
    MaLoaiXN int NOT NULL REFERENCES LoaiXetNghiem(MaLoaiXN),
    KetQua nvarchar(500) NULL,
    DonViDo nvarchar(30) NULL,
    NgayKetQua datetime2(0) NULL,
    DonGia decimal(12,0) NOT NULL CHECK (DonGia >= 0) ,
    PRIMARY KEY (MaPhieuKham, MaLoaiXN),
    CHECK ((KetQua IS NULL AND NgayKetQua IS NULL) OR (KetQua IS NOT NULL AND NgayKetQua IS NOT NULL))
 );
GO

-- 9. Thuốc
CREATE TABLE Thuoc (
    MaThuoc int IDENTITY(1,1) PRIMARY KEY,
    TenThuoc nvarchar(100) NOT NULL,
    DonViTinh nvarchar(20) NOT NULL,
    HamLuong nvarchar(50) NOT NULL,
    DonGia decimal(12,0) NOT NULL CHECK (DonGia >= 0)
 );
GO

-- 10. Toa thuốc
CREATE TABLE ToaThuoc (
    MaToa int IDENTITY(1,1) PRIMARY KEY,
    MaPhieuKham int NOT NULL UNIQUE REFERENCES PhieuKhamBenh(MaPhieuKham),
    NgayKe datetime2(0) NOT NULL,
    LoiDan nvarchar(500) NULL
 );
GO

-- 11. Chi tiết toa thuốc
CREATE TABLE ChiTietToaThuoc (
    MaToa int NOT NULL REFERENCES ToaThuoc(MaToa),
    STT smallint NOT NULL CHECK (STT > 0),
    MaThuoc int NOT NULL REFERENCES Thuoc(MaThuoc),
    SoLuong int NOT NULL CHECK (SoLuong > 0),
    DonGia decimal(12,0) NOT NULL CHECK (DonGia >= 0),
    LieuDung nvarchar(100) NOT NULL,
    CachDung nvarchar(200) NOT NULL ,
    PRIMARY KEY (MaToa, STT)
 );
GO

-- 12. Theo dõi điều trị
CREATE TABLE TheoDoiDieuTri (
    MaTheoDoi int IDENTITY(1,1) PRIMARY KEY,
    MaPhieuKham int NOT NULL UNIQUE REFERENCES PhieuKhamBenh(MaPhieuKham),
    MaNVCSKH int NOT NULL REFERENCES NhanVien(MaNV),
    NgayGhi datetime2(0) NOT NULL,
    DienBien nvarchar(500) NOT NULL,
    PhanUngThuoc nvarchar(500) NULL,
    HieuQua nvarchar(500) NULL
 );
GO

-- Một lịch hẹn được tiếp nhận tối đa một lần. Cho phép nhiều phiếu không có lịch hẹn.
CREATE UNIQUE INDEX UQ_PhieuKham_LichHen
ON PhieuKhamBenh(MaLichHen) WHERE MaLichHen IS NOT NULL;
GO