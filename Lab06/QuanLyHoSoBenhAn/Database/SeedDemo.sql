-- Chỉ thêm dữ liệu minh họa có nhãn [DEMO]. Không xóa / ghi đè bản ghi đang có.
-- Chạy trong QuanLyHoSoBenhAn_Bai05. Có thể chạy lại mà không nhân đôi dữ liệu.
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;
SET XACT_ABORT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRANSACTION;

IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen=N'[DEMO] Bác sĩ Lan' AND VaiTro='BacSi')
    INSERT NhanVien(HoTen,VaiTro,DienThoai) VALUES(N'[DEMO] Bác sĩ Lan','BacSi','0900000011');
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen=N'[DEMO] Bác sĩ Nam' AND VaiTro='BacSi')
    INSERT NhanVien(HoTen,VaiTro,DienThoai) VALUES(N'[DEMO] Bác sĩ Nam','BacSi','0900000012');
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen=N'[DEMO] Tiếp nhận' AND VaiTro='TiepNhanCSKH')
    INSERT NhanVien(HoTen,VaiTro,DienThoai) VALUES(N'[DEMO] Tiếp nhận','TiepNhanCSKH',NULL);
IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE HoTen=N'[DEMO] Thu ngân' AND VaiTro='ThuNgan')
    INSERT NhanVien(HoTen,VaiTro,DienThoai) VALUES(N'[DEMO] Thu ngân','ThuNgan',NULL);
IF NOT EXISTS (SELECT 1 FROM KhoaDieuTri WHERE TenKhoa=N'Nội tổng quát')
    INSERT KhoaDieuTri(TenKhoa) VALUES(N'Nội tổng quát');
IF NOT EXISTS (SELECT 1 FROM KhoaDieuTri WHERE TenKhoa=N'Ngoại tổng quát')
    INSERT KhoaDieuTri(TenKhoa) VALUES(N'Ngoại tổng quát');
IF NOT EXISTS (SELECT 1 FROM LoaiXetNghiem WHERE TenLoaiXN=N'[DEMO] Xét nghiệm cơ bản')
    INSERT LoaiXetNghiem(TenLoaiXN,DonGia) VALUES(N'[DEMO] Xét nghiệm cơ bản',50000);

IF NOT EXISTS (SELECT 1 FROM Thuoc WHERE TenThuoc=N'[DEMO] Thuốc minh họa A')
    INSERT Thuoc(TenThuoc,DonViTinh,HamLuong,DonGia) VALUES(N'[DEMO] Thuốc minh họa A',N'Viên',N'Loại A',2000);
IF NOT EXISTS (SELECT 1 FROM Thuoc WHERE TenThuoc=N'[DEMO] Thuốc minh họa B')
    INSERT Thuoc(TenThuoc,DonViTinh,HamLuong,DonGia) VALUES(N'[DEMO] Thuốc minh họa B',N'Viên',N'Loại B',3000);
IF NOT EXISTS (SELECT 1 FROM Thuoc WHERE TenThuoc=N'[DEMO] Thuốc minh họa C')
    INSERT Thuoc(TenThuoc,DonViTinh,HamLuong,DonGia) VALUES(N'[DEMO] Thuốc minh họa C',N'Gói',N'Loại C',5000);
IF NOT EXISTS (SELECT 1 FROM Thuoc WHERE TenThuoc=N'[DEMO] Thuốc minh họa D')
    INSERT Thuoc(TenThuoc,DonViTinh,HamLuong,DonGia) VALUES(N'[DEMO] Thuốc minh họa D',N'Chai',N'Loại D',25000);

IF NOT EXISTS (SELECT 1 FROM BenhNhan WHERE HoTen=N'[DEMO] Nguyễn Minh An' AND DienThoai='0900000001')
    INSERT BenhNhan(HoTen,NgaySinh,GioiTinh,DiaChi,DienThoai,Anh3x4) VALUES(N'[DEMO] Nguyễn Minh An','2000-03-15',N'Nam',N'TP. Hồ Chí Minh','0900000001',N'Assets\demo.png');
IF NOT EXISTS (SELECT 1 FROM BenhNhan WHERE HoTen=N'[DEMO] Lê Thu Hà' AND DienThoai='0900000002')
    INSERT BenhNhan(HoTen,NgaySinh,GioiTinh,DiaChi,DienThoai,Anh3x4) VALUES(N'[DEMO] Lê Thu Hà','1995-07-20',N'Nữ',N'TP. Hồ Chí Minh','0900000002',N'Assets\demo.png');
IF NOT EXISTS (SELECT 1 FROM BenhNhan WHERE HoTen=N'[DEMO] Trần Gia Bảo' AND DienThoai='0900000003')
    INSERT BenhNhan(HoTen,NgaySinh,GioiTinh,DiaChi,DienThoai,Anh3x4) VALUES(N'[DEMO] Trần Gia Bảo','1988-11-05',N'Nam',N'Đồng Nai','0900000003',N'Assets\demo.png');

DECLARE @HomNay date=CAST(GETDATE() AS date);
INSERT HoSoBenhAn(MaBN,NgayCap,NgayDongPhi,HanPhi,PhiThuongNien)
SELECT b.MaBN,DATEADD(year,-2,@HomNay),
CASE WHEN b.DienThoai='0900000003' THEN DATEADD(year,-2,@HomNay) ELSE @HomNay END,
CASE WHEN b.DienThoai='0900000003' THEN DATEADD(year,1,DATEADD(year,-2,@HomNay)) ELSE DATEADD(year,1,@HomNay) END,200000
FROM BenhNhan b WHERE b.HoTen IN(N'[DEMO] Nguyễn Minh An',N'[DEMO] Lê Thu Hà',N'[DEMO] Trần Gia Bảo')
AND NOT EXISTS(SELECT 1 FROM HoSoBenhAn h WHERE h.MaBN=b.MaBN);

DECLARE @BS int=(SELECT MIN(MaNV) FROM NhanVien WHERE HoTen=N'[DEMO] Bác sĩ Lan' AND VaiTro='BacSi');
DECLARE @Khoa int=(SELECT MaKhoa FROM KhoaDieuTri WHERE TenKhoa=N'Nội tổng quát');
DECLARE @An int=(SELECT MIN(h.MaHSBA) FROM HoSoBenhAn h JOIN BenhNhan b ON b.MaBN=h.MaBN WHERE b.HoTen=N'[DEMO] Nguyễn Minh An' AND b.DienThoai='0900000001');
DECLARE @Ha int=(SELECT MIN(h.MaHSBA) FROM HoSoBenhAn h JOIN BenhNhan b ON b.MaBN=h.MaBN WHERE b.HoTen=N'[DEMO] Lê Thu Hà' AND b.DienThoai='0900000002');
DECLARE @Bao int=(SELECT MIN(h.MaHSBA) FROM HoSoBenhAn h JOIN BenhNhan b ON b.MaBN=h.MaBN WHERE b.HoTen=N'[DEMO] Trần Gia Bảo' AND b.DienThoai='0900000003');

IF NOT EXISTS(SELECT 1 FROM PhieuKhamBenh WHERE MaHSBA=@An AND TrieuChung=N'[DEMO] Phiếu chờ khám')
    INSERT PhieuKhamBenh(MaHSBA,MaBacSi,MaKhoa,NgayKham,LoaiKham,TrieuChung,TrangThai,TienKham)
    VALUES(@An,@BS,@Khoa,GETDATE(),N'Điều trị',N'[DEMO] Phiếu chờ khám',N'Chờ khám',100000);
IF NOT EXISTS(SELECT 1 FROM PhieuKhamBenh WHERE MaHSBA=@Ha AND TrieuChung=N'[DEMO] Phiếu có kết quả và toa thuốc')
BEGIN
    INSERT PhieuKhamBenh(MaHSBA,MaBacSi,MaKhoa,NgayKham,LoaiKham,TrieuChung,ThanNhiet,HuyetAp,NhipTim,ChanDoan,TrangThai,TienKham)
    VALUES(@Ha,@BS,@Khoa,GETDATE(),N'Điều trị',N'[DEMO] Phiếu có kết quả và toa thuốc',36.5,'120/80',75,N'Kết luận minh họa phục vụ kiểm thử',N'Hoàn tất khám',100000);
    DECLARE @Phieu int=CAST(SCOPE_IDENTITY() AS int);
    DECLARE @XN int=(SELECT MaLoaiXN FROM LoaiXetNghiem WHERE TenLoaiXN=N'[DEMO] Xét nghiệm cơ bản');
    INSERT ChiTietXetNghiem(MaPhieuKham,MaLoaiXN,KetQua,DonViDo,NgayKetQua,DonGia)
    SELECT @Phieu,@XN,N'Kết quả giả lập',N'Minh họa',GETDATE(),DonGia FROM LoaiXetNghiem WHERE MaLoaiXN=@XN;
    INSERT ToaThuoc(MaPhieuKham,NgayKe,LoiDan) VALUES(@Phieu,GETDATE(),N'Dữ liệu minh họa, không phải chỉ định điều trị.');
    DECLARE @Toa int=CAST(SCOPE_IDENTITY() AS int);
    INSERT ChiTietToaThuoc(MaToa,STT,MaThuoc,SoLuong,DonGia,LieuDung,CachDung)
    SELECT @Toa,ROW_NUMBER() OVER(ORDER BY MaThuoc),MaThuoc,5,DonGia,N'Liều dùng minh họa',N'Cách dùng minh họa'
    FROM Thuoc WHERE TenThuoc IN(N'[DEMO] Thuốc minh họa A',N'[DEMO] Thuốc minh họa B');
END;
IF NOT EXISTS(SELECT 1 FROM PhieuKhamBenh WHERE MaHSBA=@Bao AND TrieuChung=N'[DEMO] Phiếu đã thanh toán')
    INSERT PhieuKhamBenh(MaHSBA,MaBacSi,MaKhoa,NgayKham,LoaiKham,TrieuChung,ChanDoan,TrangThai,TienKham,TongVienPhi,NgayThanhToan)
    VALUES(@Bao,@BS,@Khoa,DATEADD(day,-2,GETDATE()),N'Điều trị',N'[DEMO] Phiếu đã thanh toán',N'Kết luận minh họa',N'Đã thanh toán',100000,100000,DATEADD(day,-2,GETDATE()));

COMMIT;
