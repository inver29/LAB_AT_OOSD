USE eShopping;
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;

DECLARE @NhomDienTu uniqueidentifier =
    '11111111-1111-1111-1111-111111111111';

DECLARE @NhomGiaDung uniqueidentifier =
    '22222222-2222-2222-2222-222222222222';

IF NOT EXISTS (
    SELECT 1 FROM dbo.NhomSanPham
    WHERE MaNhom = @NhomDienTu
)
    INSERT INTO dbo.NhomSanPham (MaNhom, TenNhom)
    VALUES (@NhomDienTu, N'Điện tử');

IF NOT EXISTS (
    SELECT 1 FROM dbo.NhomSanPham
    WHERE MaNhom = @NhomGiaDung
)
    INSERT INTO dbo.NhomSanPham (MaNhom, TenNhom)
    VALUES (@NhomGiaDung, N'Gia dụng');

INSERT INTO dbo.SanPham
    (MaSP, MaNhom, TenSP, NhaSanXuat, GiaHienHanh, TinhTrang)
SELECT
    v.MaSP, v.MaNhom, v.TenSP,
    v.NhaSanXuat, v.GiaHienHanh, v.TinhTrang
FROM
(
    VALUES
    (
        CAST('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1' AS uniqueidentifier),
        @NhomDienTu, N'Tai nghe Bluetooth', N'ABC',
        CAST(450000 AS decimal(18,2)), CAST(1 AS bit)
    ),
    (
        CAST('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2' AS uniqueidentifier),
        @NhomDienTu, N'Màn hình 24 inch', N'ABC',
        CAST(2500000 AS decimal(18,2)), CAST(1 AS bit)
    ),
    (
        CAST('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3' AS uniqueidentifier),
        @NhomGiaDung, N'Nồi cơm điện', N'XYZ',
        CAST(1200000 AS decimal(18,2)), CAST(1 AS bit)
    ),
    (
        CAST('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4' AS uniqueidentifier),
        @NhomGiaDung, N'Quạt điện', N'XYZ',
        CAST(650000 AS decimal(18,2)), CAST(0 AS bit)
    )
) v(MaSP, MaNhom, TenSP, NhaSanXuat, GiaHienHanh, TinhTrang)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.SanPham p WHERE p.MaSP = v.MaSP
);

IF NOT EXISTS (
    SELECT 1 FROM dbo.KhachHang
    WHERE TenDangNhap = N'khachmau'
)
BEGIN
    INSERT INTO dbo.KhachHang
    (
        HoTen, NgaySinh, GiayTo, DiaChi, DienThoai,
        TenDangNhap, MatKhauBam, Email
    )
    VALUES
    (
        N'Nguyễn Văn An',
        '20000115',
        'DEMO001',
        N'123 Nguyễn Trãi, TP. Hồ Chí Minh',
        '0901234567',
        N'khachmau',
        'TAI_KHOAN_MAU_KHONG_SU_DUNG_DANG_NHAP',
        NULL
    );
END;

-- Mức phí minh họa để thử giao diện.
INSERT INTO dbo.PhiGiaoHang (KhuVuc, LoaiGiao, PhiCoBan)
SELECT v.KhuVuc, v.LoaiGiao, v.PhiCoBan
FROM
(
    VALUES
    (N'Nội thành', 'Thuong',   CAST(20000 AS decimal(18,2))),
    (N'Nội thành', 'Nhanh',    CAST(35000 AS decimal(18,2))),
    (N'Nội thành', 'TrongNgay',CAST(60000 AS decimal(18,2))),
    (N'Ngoại thành', 'Thuong', CAST(30000 AS decimal(18,2))),
    (N'Ngoại thành', 'Nhanh',  CAST(50000 AS decimal(18,2))),
    (N'Ngoại thành', 'TrongNgay',CAST(90000 AS decimal(18,2)))
) v(KhuVuc, LoaiGiao, PhiCoBan)
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.PhiGiaoHang p
    WHERE p.KhuVuc = v.KhuVuc AND p.LoaiGiao = v.LoaiGiao
);

COMMIT TRANSACTION;
GO