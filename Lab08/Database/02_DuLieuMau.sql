-- Lab08: dữ liệu minh họa, không xóa hoặc đặt lại dữ liệu hiện có.
USE QuanLyVanPhong_Lab08;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;
SET XACT_ABORT ON;
GO
BEGIN TRANSACTION;
-- Chỉ nạp bộ ví dụ khi database chưa có hợp đồng; chạy lại sẽ không nhân đôi.
IF NOT EXISTS (SELECT 1 FROM HopDong)
BEGIN
 DECLARE @thang date=DATEFROMPARTS(YEAR(GETDATE()),MONTH(GETDATE()),1);
 DECLARE @a date=DATEADD(month,-8,@thang), @b date=DATEADD(month,-2,@thang);
 IF NOT EXISTS(SELECT 1 FROM Tang WHERE SoTang=1) INSERT Tang(SoTang) VALUES(1);
 IF NOT EXISTS(SELECT 1 FROM Tang WHERE SoTang=2) INSERT Tang(SoTang) VALUES(2);
 IF NOT EXISTS(SELECT 1 FROM Tang WHERE SoTang=3) INSERT Tang(SoTang) VALUES(3);
 DECLARE @t1 int=(SELECT MaTang FROM Tang WHERE SoTang=1),@t2 int=(SELECT MaTang FROM Tang WHERE SoTang=2),@t3 int=(SELECT MaTang FROM Tang WHERE SoTang=3);
 INSERT CongTy(TenCongTy,DaiDien,DienThoai,DiaChi) VALUES
 (N'Công ty Minh An',N'Nguyễn Minh An','0900000001',N'TP. Hồ Chí Minh'),
 (N'Công ty Ánh Dương',N'Lê Thu Hà','0900000002',N'TP. Hồ Chí Minh'),
 (N'Công ty Nam Việt',N'Trần Gia Bảo','0900000003',N'Đồng Nai');
 DECLARE @c1 int=(SELECT TOP 1 MaCongTy FROM CongTy WHERE TenCongTy=N'Công ty Minh An' ORDER BY MaCongTy DESC),@c2 int=(SELECT TOP 1 MaCongTy FROM CongTy WHERE TenCongTy=N'Công ty Ánh Dương' ORDER BY MaCongTy DESC);
 INSERT VanPhong(MaTang,KyHieu,Huong,DienTich,DonGiaThang,TinhTrang)
 SELECT v.Tang,v.KyHieu,v.Huong,v.DienTich,v.Gia,v.TinhTrang FROM (VALUES
 (@t1,N'VP101',N'Đông',60,12000000,N'Đạt'),(@t1,N'VP102',N'Tây',80,15000000,N'Đạt'),
 (@t2,N'VP201',N'Nam',100,20000000,N'Đạt'),(@t2,N'VP202',N'Bắc',75,16000000,N'Đạt'),
 (@t3,N'VP301',N'Đông Nam',120,25000000,N'Đạt'),(@t3,N'VP302',N'Tây Nam',90,18000000,N'Cần sửa')) v(Tang,KyHieu,Huong,DienTich,Gia,TinhTrang)
 WHERE NOT EXISTS(SELECT 1 FROM VanPhong p WHERE p.KyHieu=v.KyHieu);
 INSERT DichVu(TenDichVu,DonViTinh,DonGia) SELECT n,u,p FROM (VALUES(N'Điện',N'kWh',3500),(N'Nước',N'm³',18000),(N'Internet',N'Tháng',300000),(N'Vệ sinh',N'Lần',100000)) v(n,u,p) WHERE NOT EXISTS(SELECT 1 FROM DichVu d WHERE d.TenDichVu=v.n);
 INSERT HopDong(SoHopDong,MaCongTy,NgayKy,TuNgay,DenNgay,NoiDung) VALUES(N'HD-MAU-01',@c1,@a,@a,DATEADD(month,12,@a),N'Thuê hai văn phòng; giá cố định theo hợp đồng.'),(N'HD-MAU-02',@c2,@b,@b,DATEADD(month,6,@b),N'Thuê sáu tháng; thu trước sáu tháng đầu.');
 DECLARE @h1 int=(SELECT MaHopDong FROM HopDong WHERE SoHopDong=N'HD-MAU-01'),@h2 int=(SELECT MaHopDong FROM HopDong WHERE SoHopDong=N'HD-MAU-02');
 INSERT ChiTietHopDong(MaHopDong,MaVanPhong,DonGiaThang,DenNgayHienTai) VALUES
 (@h1,(SELECT MaVanPhong FROM VanPhong WHERE KyHieu=N'VP101'),12000000,DATEADD(month,12,@a)),
 (@h1,(SELECT MaVanPhong FROM VanPhong WHERE KyHieu=N'VP201'),20000000,DATEADD(month,12,@a)),
 (@h2,(SELECT MaVanPhong FROM VanPhong WHERE KyHieu=N'VP102'),15000000,DATEADD(month,6,@b));
 DECLARE @d1 int=(SELECT MaChiTietHD FROM ChiTietHopDong WHERE MaHopDong=@h1 AND MaVanPhong=(SELECT MaVanPhong FROM VanPhong WHERE KyHieu=N'VP101')),
 @d2 int=(SELECT MaChiTietHD FROM ChiTietHopDong WHERE MaHopDong=@h1 AND MaVanPhong=(SELECT MaVanPhong FROM VanPhong WHERE KyHieu=N'VP201')),
 @d3 int=(SELECT MaChiTietHD FROM ChiTietHopDong WHERE MaHopDong=@h2);
 INSERT HoaDon(SoHoaDon,MaHopDong,Loai,TuNgay,DenNgay,NgayLap,NgayThanhToan) VALUES
 (N'HD-MAU-01-T01',@h1,N'Tiền thuê',@a,DATEADD(month,6,@a),@a,@a),
 (N'HD-MAU-01-T07',@h1,N'Tiền thuê',DATEADD(month,6,@a),DATEADD(month,7,@a),DATEADD(month,6,@a),DATEADD(month,6,@a)),
 (N'HD-MAU-01-T08',@h1,N'Tiền thuê',DATEADD(month,7,@a),DATEADD(month,8,@a),DATEADD(month,7,@a),DATEADD(month,7,@a)),
 (N'HD-MAU-01-T09',@h1,N'Tiền thuê',@thang,DATEADD(month,1,@thang),@thang,@thang),
 (N'HD-MAU-02-T01',@h2,N'Tiền thuê',@b,DATEADD(month,6,@b),@b,@b),
 (N'HD-MAU-02-DV01',@h2,N'Dịch vụ',DATEADD(month,-1,@thang),@thang,@thang,NULL);
 INSERT ChiTietHoaDon(MaHoaDon,STT,MaChiTietHD,MaDichVu,SoLuong,DonGia)
 SELECT hd.MaHoaDon,ROW_NUMBER() OVER(PARTITION BY hd.MaHoaDon ORDER BY d.MaChiTietHD),d.MaChiTietHD,NULL,CASE WHEN hd.SoHoaDon LIKE N'%-T01' THEN 6 ELSE 1 END,d.DonGiaThang FROM HoaDon hd JOIN ChiTietHopDong d ON d.MaHopDong=hd.MaHopDong WHERE hd.Loai=N'Tiền thuê';
 INSERT ChiTietHoaDon VALUES((SELECT MaHoaDon FROM HoaDon WHERE SoHoaDon=N'HD-MAU-02-DV01'),1,@d3,(SELECT MaDichVu FROM DichVu WHERE TenDichVu=N'Điện'),150,3500);
 INSERT NhanVien(MaChiTietHD,HoTen,ChucVu,Anh) VALUES
 (@d1,N'Nguyễn Văn An',N'Nhân viên kinh doanh',N'C:\Study\LAB_AT_OOSD\Lab08\QuanLyVanPhong\Assets\nhan_vien_mau.png'),
 (@d2,N'Lê Thu Hà',N'Trưởng bộ phận',N'C:\Study\LAB_AT_OOSD\Lab08\QuanLyVanPhong\Assets\nhan_vien_mau.png'),
 (@d3,N'Trần Gia Bảo',N'Kỹ thuật viên',N'C:\Study\LAB_AT_OOSD\Lab08\QuanLyVanPhong\Assets\nhan_vien_mau.png');
 INSERT TheGiuXe(MaNhanVien,LoaiXe,BienSo,NgayCap) VALUES((SELECT MaNhanVien FROM NhanVien WHERE HoTen=N'Nguyễn Văn An'),N'Xe máy','59-A1-12345',@thang);
 INSERT KiemTra(MaVanPhong,NgayKiemTra,KetQua,GhiChu) VALUES((SELECT MaVanPhong FROM VanPhong WHERE KyHieu=N'VP101'),DATEADD(day,-10,@thang),N'Đạt',N'Kiểm tra định kỳ'),((SELECT MaVanPhong FROM VanPhong WHERE KyHieu=N'VP302'),DATEADD(day,-5,@thang),N'Cần sửa',N'Cần sửa hệ thống chiếu sáng');
 INSERT ViPham(MaChiTietHD,NgayLap,NoiDung,TienPhat) VALUES(@d2,DATEADD(day,-1,@thang),N'Tự ý thay đổi vách ngăn; biên bản minh họa.',1000000);
END;
COMMIT;
GO
