-- BÀI 7 - QUẢN LÝ NHÂN SỰ
-- SQL Server. Chạy trên CSDL mới; không chạy lại CREATE TABLE khi đã có bảng.
IF DB_ID(N'QuanLyNhanSu_Bai07') IS NULL
    CREATE DATABASE QuanLyNhanSu_Bai07;
GO
USE QuanLyNhanSu_Bai07;
GO

-- 1. PhongBan
CREATE TABLE PhongBan (
    MaPhongBan int IDENTITY PRIMARY KEY,
    TenPhongBan nvarchar(100) NOT NULL UNIQUE
 );
GO

-- 2. NhanVien
CREATE TABLE NhanVien (
    MaNV int IDENTITY PRIMARY KEY,
    HoTen nvarchar(100) NOT NULL,
    NgaySinh date NOT NULL,
    GioiTinh nvarchar(10) NULL,
    DienThoai varchar(15) NULL,
    TrangThai nvarchar(30) NOT NULL CHECK (TrangThai IN (N'Thử việc',N'Chờ hợp đồng',N'Chính thức',N'Kết thúc thử việc'))
 );
GO

-- 3. QuyetDinhTuyenDung
CREATE TABLE QuyetDinhTuyenDung (
    MaQDTD int IDENTITY PRIMARY KEY,
    SoQD nvarchar(20) NOT NULL UNIQUE,
    MaNV int NOT NULL UNIQUE REFERENCES NhanVien(MaNV),
    MaPhongBan int NOT NULL REFERENCES PhongBan(MaPhongBan),
    NgayQD date NOT NULL,
    TuNgay date NOT NULL,
    DenNgay date NOT NULL,
    LuongThuViec decimal(18,2) NOT NULL CHECK (LuongThuViec > 0),
    NoiDung nvarchar(1000) NOT NULL,
    KetQua nvarchar(20) NULL CHECK (KetQua IN (N'Đạt',N'Không đạt')),
    CHECK (NgayQD <= TuNgay AND DenNgay >= TuNgay)
 );
GO

-- 4. HopDong
CREATE TABLE HopDong (
    MaHD int IDENTITY PRIMARY KEY,
    SoHD nvarchar(20) NOT NULL UNIQUE,
    MaNV int NOT NULL REFERENCES NhanVien(MaNV),
    LoaiHD nvarchar(20) NOT NULL CHECK (LoaiHD IN (N'Dài hạn',N'Ngắn hạn')),
    NgayKy date NOT NULL,
    TuNgay date NOT NULL,
    DenNgay date NULL,
    NoiDung nvarchar(1000) NOT NULL,
    CHECK (NgayKy <= TuNgay AND (DenNgay IS NULL OR DenNgay >= TuNgay)),
    CHECK (LoaiHD <> N'Ngắn hạn' OR DenNgay IS NOT NULL)
 );
GO

-- 5. ChucVu
CREATE TABLE ChucVu (
    MaChucVu int IDENTITY PRIMARY KEY,
    TenChucVu nvarchar(100) NOT NULL UNIQUE
 );
GO

-- 6. QuyetDinhChucVu
CREATE TABLE QuyetDinhChucVu (
    MaQDCV int IDENTITY PRIMARY KEY,
    SoQD nvarchar(20) NOT NULL UNIQUE,
    MaNV int NOT NULL REFERENCES NhanVien(MaNV),
    MaChucVu int NOT NULL REFERENCES ChucVu(MaChucVu),
    NgayQD date NOT NULL,
    TuNgay date NOT NULL,
    DenNgay date NULL,
    PhuCapChucVu decimal(18,2) NOT NULL CHECK (PhuCapChucVu >= 0),
    CHECK (NgayQD <= TuNgay AND (DenNgay IS NULL OR DenNgay >= TuNgay))
 );
GO

-- 7. QuyetDinhLuong
CREATE TABLE QuyetDinhLuong (
    MaQDL int IDENTITY PRIMARY KEY,
    SoQD nvarchar(20) NOT NULL UNIQUE,
    MaNV int NOT NULL REFERENCES NhanVien(MaNV),
    NgayQD date NOT NULL,
    TuNgay date NOT NULL,
    DenNgay date NULL,
    LuongCoBan decimal(18,2) NOT NULL CHECK (LuongCoBan > 0),
    PhuCapLuong decimal(18,2) NOT NULL CHECK (PhuCapLuong >= 0),
    PhuCapDocHai decimal(18,2) NOT NULL CHECK (PhuCapDocHai >= 0),
    CHECK (NgayQD <= TuNgay AND (DenNgay IS NULL OR DenNgay >= TuNgay))
 );
GO

-- 8. HoSoBaoHiem
CREATE TABLE HoSoBaoHiem (
    MaBH int IDENTITY PRIMARY KEY,
    MaNV int NOT NULL REFERENCES NhanVien(MaNV),
    SoBHXH varchar(30) NOT NULL,
    SoBHYT varchar(30) NOT NULL,
    TuNgay date NOT NULL,
    DenNgay date NULL,
    TyLeBHXH decimal(5,4) NOT NULL CHECK (TyLeBHXH BETWEEN 0 AND 1),
    TyLeBHYT decimal(5,4) NOT NULL CHECK (TyLeBHYT BETWEEN 0 AND 1),
    CHECK (DenNgay IS NULL OR DenNgay >= TuNgay)
 );
GO

-- 9. CaLamViec
CREATE TABLE CaLamViec (
    MaCa int IDENTITY PRIMARY KEY,
    TenCa nvarchar(50) NOT NULL UNIQUE,
    GioBatDau time(0) NOT NULL,
    GioKetThuc time(0) NOT NULL,
    CHECK (GioBatDau < GioKetThuc)
 );
GO

-- 10. ChamCong
CREATE TABLE ChamCong (
    MaNV int NOT NULL REFERENCES NhanVien(MaNV),
    Ngay date NOT NULL,
    MaCa int NOT NULL REFERENCES CaLamViec(MaCa),
    SoGioThucTe decimal(5,2) NULL CHECK (SoGioThucTe BETWEEN 0 AND 24),
    PRIMARY KEY (MaNV, Ngay, MaCa)
 );
GO

-- 11. PhieuPhuTroi
CREATE TABLE PhieuPhuTroi (
    MaPhieuPT int IDENTITY PRIMARY KEY,
    SoPhieu nvarchar(20) NOT NULL UNIQUE,
    NgayPhuTroi date NOT NULL,
    GioBatDau time(0) NOT NULL,
    SoGio decimal(5,2) NOT NULL CHECK (SoGio > 0 AND SoGio <= 24),
    CHECK (DATEDIFF(SECOND, CAST('00:00:00' AS time), GioBatDau) + SoGio * 3600 <= 86400),
    HinhThuc nvarchar(20) NOT NULL CHECK (HinhThuc IN (N'Tăng ca',N'Ngày lễ',N'Ngày nghỉ')),
    TrangThai nvarchar(20) NOT NULL CHECK (TrangThai IN (N'Kế hoạch',N'Đã xác nhận'))
 );
GO

-- 12. ChiTietPhuTroi
CREATE TABLE ChiTietPhuTroi (
    MaPhieuPT int NOT NULL REFERENCES PhieuPhuTroi(MaPhieuPT),
    MaNV int NOT NULL REFERENCES NhanVien(MaNV),
    PRIMARY KEY (MaPhieuPT, MaNV)
 );
GO

-- 13. BangLuong
CREATE TABLE BangLuong (
    MaBangLuong int IDENTITY PRIMARY KEY,
    Thang int NOT NULL CHECK (Thang BETWEEN 1 AND 12),
    Nam int NOT NULL CHECK (Nam >= 2000),
    GioChuanThang decimal(6,2) NOT NULL CHECK (GioChuanThang > 0 AND GioChuanThang <= 744),
    NgayLap date NOT NULL,
    TrangThai nvarchar(20) NOT NULL CHECK (TrangThai IN (N'Nháp',N'Đã chốt')),
    UNIQUE (Thang, Nam)
 );
GO

-- 14. ChiTietBangLuong
CREATE TABLE ChiTietBangLuong (
    MaBangLuong int NOT NULL REFERENCES BangLuong(MaBangLuong),
    MaNV int NOT NULL REFERENCES NhanVien(MaNV),
    GioThuong decimal(6,2) NOT NULL CHECK (GioThuong >= 0),
    GioPhuTroi decimal(6,2) NOT NULL CHECK (GioPhuTroi >= 0),
    TienCong decimal(18,2) NOT NULL CHECK (TienCong >= 0),
    TienPhuTroi decimal(18,2) NOT NULL CHECK (TienPhuTroi >= 0),
    PhuCap decimal(18,2) NOT NULL CHECK (PhuCap >= 0),
    TienThuong decimal(18,2) NOT NULL CHECK (TienThuong >= 0),
    TienPhat decimal(18,2) NOT NULL CHECK (TienPhat >= 0),
    TruBHXH decimal(18,2) NOT NULL CHECK (TruBHXH >= 0),
    TruBHYT decimal(18,2) NOT NULL CHECK (TruBHYT >= 0),
    ThucLinh decimal(18,2) NOT NULL CHECK (ThucLinh >= 0),
    PRIMARY KEY (MaBangLuong, MaNV),
    CHECK (ThucLinh = TienCong + TienPhuTroi + PhuCap + TienThuong - TienPhat - TruBHXH - TruBHYT)
 );
GO

-- 15. QuyetDinhThuongPhat
CREATE TABLE QuyetDinhThuongPhat (
    MaQD int IDENTITY PRIMARY KEY,
    SoQD nvarchar(20) NOT NULL UNIQUE,
    LoaiQD nvarchar(20) NOT NULL CHECK (LoaiQD IN (N'Khen thưởng',N'Kỷ luật')),
    MaNV int NULL REFERENCES NhanVien(MaNV),
    MaPhongBan int NULL REFERENCES PhongBan(MaPhongBan),
    NgayLap date NOT NULL,
    NoiDung nvarchar(1000) NOT NULL,
    SoTien decimal(18,2) NOT NULL CHECK (SoTien > 0),
    CachThanhToan nvarchar(20) NOT NULL CHECK (CachThanhToan IN (N'Trực tiếp',N'Qua lương')),
    KyApDung date NULL,
    TrangThai nvarchar(20) NOT NULL CHECK (TrangThai IN (N'Nháp',N'Đã ký')),
    MaNguoiKy int NULL REFERENCES NhanVien(MaNV),
    NgayKy date NULL,
    CHECK ((MaNV IS NOT NULL AND MaPhongBan IS NULL) OR (MaNV IS NULL AND MaPhongBan IS NOT NULL)),
    CHECK ((CachThanhToan = N'Qua lương' AND KyApDung IS NOT NULL AND DAY(KyApDung) = 1) OR (CachThanhToan = N'Trực tiếp' AND KyApDung IS NULL)),
    CHECK ((TrangThai = N'Nháp' AND MaNguoiKy IS NULL AND NgayKy IS NULL) OR (TrangThai = N'Đã ký' AND MaNguoiKy IS NOT NULL AND NgayKy IS NOT NULL AND NgayKy >= NgayLap))
 );
GO

-- 16. ChiTietQuyetDinh
CREATE TABLE ChiTietQuyetDinh (
    MaQD int NOT NULL REFERENCES QuyetDinhThuongPhat(MaQD),
    MaNV int NOT NULL REFERENCES NhanVien(MaNV),
    SoTien decimal(18,2) NOT NULL CHECK (SoTien > 0),
    MaBangLuong int NULL,
    PRIMARY KEY (MaQD, MaNV),
    FOREIGN KEY (MaBangLuong, MaNV) REFERENCES ChiTietBangLuong(MaBangLuong, MaNV)
 );
GO

-- 17. PhieuThuChi
CREATE TABLE PhieuThuChi (
    MaPhieu int IDENTITY PRIMARY KEY,
    SoPhieu nvarchar(20) NOT NULL UNIQUE,
    MaQD int NOT NULL UNIQUE REFERENCES QuyetDinhThuongPhat(MaQD),
    NgayLap date NOT NULL,
    LoaiPhieu nvarchar(10) NOT NULL CHECK (LoaiPhieu IN (N'Thu',N'Chi')),
    SoTien decimal(18,2) NOT NULL CHECK (SoTien > 0)
 );
GO
