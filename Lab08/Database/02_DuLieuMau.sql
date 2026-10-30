-- ==============================================================================
-- HỆ THỐNG QUẢN LÝ CHO THUÊ VĂN PHÒNG CAO ỐC - BÀI LAB 08
-- Sinh viên thực hiện: Nguyễn Hoài An - MSSV: 1250080245 - Lớp: 12_ĐH_CNPM1
-- Script 02: Nạp dữ liệu mẫu thực tế và phong phú
-- ==============================================================================

USE QuanLyChoThueVanPhong_Bai08;
GO

-- 1. NẠP DỮ LIỆU TẦNG
INSERT INTO dbo.Tang (MaTang, TenTang, ViTriDoCao, HeSoGiaTang, MoTa) VALUES
('T01', N'Tầng 1 (Trệt)', 1, 1.30, N'Sảnh lễ tân, ngân hàng, showroom, mặt tiền tòa nhà'),
('T02', N'Tầng 2', 2, 1.15, N'Khu vực văn phòng giao dịch thương mại'),
('T03', N'Tầng 3', 3, 1.00, N'Khu văn phòng tiêu chuẩn'),
('T05', N'Tầng 5', 5, 1.05, N'Khu văn phòng công nghệ và truyền thông'),
('T08', N'Tầng 8', 8, 1.10, N'Khu văn phòng view thoáng trung tâm'),
('T10', N'Tầng 10', 10, 1.18, N'Khu văn phòng cao cấp tập đoàn'),
('T12', N'Tầng 12', 12, 1.25, N'Khu penthouse văn phòng cao cấp nhất');
GO

-- 2. NẠP DỮ LIỆU PHÒNG VĂN PHÒNG
INSERT INTO dbo.Phong (MaPhong, MaTang, TenPhong, DienTich, HuongPhong, DonGiaTheoM2, TinhTrang, MoTaKetCau) VALUES
('P101', 'T01', N'Văn phòng P.101', 180.0, N'Đông Nam', 650000, N'Đang cho thuê', N'Tường kính cường lực, sàn đá hoa cương, cửa cuốn tự động'),
('P102', 'T01', N'Văn phòng P.102', 150.0, N'Nam', 620000, N'Trống', N'Kết cấu mở, phù hợp phòng giao dịch ngân hàng'),
('P201', 'T02', N'Văn phòng P.201', 120.0, N'Đông', 450000, N'Trống', N'Sàn gỗ công nghiệp, trần thạch cao, 2 cửa kính cách âm'),
('P301', 'T03', N'Văn phòng P.301', 140.0, N'Nam', 380000, N'Đang cho thuê', N'Tường bê tông nhẹ, hệ thống PCCC tiêu chuẩn'),
('P501', 'T05', N'Văn phòng P.501', 100.0, N'Bắc', 390000, N'Trống', N'Nguyên trạng bàn giao chuẩn của tòa nhà'),
('P502', 'T05', N'Văn phòng P.502', 160.0, N'Tây Nam', 400000, N'Đang vi phạm', N'Phòng bị tự ý đập tường ngăn, đang chờ khôi phục nguyên trạng'),
('P801', 'T08', N'Văn phòng P.801', 120.0, N'Đông Nam', 420000, N'Đang cho thuê', N'View công viên, hệ thống chiếu sáng LED âm trần cao cấp'),
('P802', 'T08', N'Văn phòng P.802', 150.0, N'Đông', 420000, N'Đang cho thuê', N'Phòng góc 2 mặt thoáng, trần thạch cao khung xương chìm'),
('P1001', 'T10', N'Văn phòng P.1001', 220.0, N'Nam', 480000, N'Trống', N'Văn phòng hạng A diện tích lớn'),
('P1201', 'T12', N'Văn phòng P.1201', 250.0, N'Đông Nam', 550000, N'Trống', N'Penthouse văn phòng tầng thượng');
GO

-- 3. NẠP DỮ LIỆU KHÁCH HÀNG / DOANH NGHIỆP
INSERT INTO dbo.KhachHang (MaKH, TenCongTy, NguoiDaiDien, ChucVuDaiDien, MaSoThue, SoDienThoai, Email, DiaChiTruSo) VALUES
('KH01', N'Công ty Cổ phần Công nghệ Đông Á', N'Trần Minh Đức', N'Tổng Giám đốc', '0312456789', '0903123456', 'duc.tran@donga-tech.vn', N'Phòng 801-802 Tòa nhà HUTECH Tower'),
('KH02', N'Công ty TNHH Phần mềm Alpha Global', N'Lê Hoàng Nam', N'Giám đốc', '0314567890', '0918765432', 'contact@alphaglobal.com', N'Phòng 301 Tòa nhà HUTECH Tower'),
('KH03', N'Công ty TNHH Truyền thông Sao Mai', N'Vũ Thị Bích Ngọc', N'Giám đốc điều hành', '0316789012', '0988654321', 'ngoc.vu@saomaimedia.vn', N'Phòng 502 Tòa nhà HUTECH Tower'),
('KH04', N'Ngân hàng TMCP Ngoại Thương Chi nhánh Cao Ốc', N'Phạm Quang Tuấn', N'Giám đốc Chi nhánh', '0100112437', '0283899999', 'tuanpq@vietcombank.com.vn', N'Tầng 1 Tòa nhà HUTECH Tower');
GO

-- 4. NẠP DỮ LIỆU HỢP ĐỒNG THUÊ VĂN PHÒNG
-- KH01 thuê cùng lúc 2 văn phòng (P801 và P802), thời hạn 12 tháng (01/01/2026 - 31/12/2026)
INSERT INTO dbo.HopDongThue (MaHD, SoHopDong, MaKH, NgayKy, NgayBatDau, NgayHetHan, ThoiGianThue, TienCoc, TongTienThueThang, TienTraTruoc6Thang, TrangThai) VALUES
('HD01', 'HD-2026-001', 'KH01', '2026-01-01', '2026-01-01', '2026-12-31', 12, 189000000, 94500000, 567000000, N'Đang hiệu lực'),
('HD02', 'HD-2026-002', 'KH02', '2026-03-01', '2026-03-01', '2026-08-31', 6, 106400000, 53200000, 319200000, N'Đang hiệu lực'),
('HD03', 'HD-2026-003', 'KH03', '2026-02-01', '2026-02-01', '2027-01-31', 12, 128000000, 64000000, 384000000, N'Tạm dừng vi phạm'),
('HD04', 'HD-2026-004', 'KH04', '2026-01-01', '2026-01-01', '2028-12-31', 36, 351000000, 117000000, 702000000, N'Đang hiệu lực');
GO

-- 5. NẠP DỮ LIỆU CHI TIẾT HỢP ĐỒNG THUÊ (Thuê cùng lúc nhiều phòng)
INSERT INTO dbo.ChiTietHopDongThue (MaHD, MaPhong, DienTichThue, DonGiaThueThang, NgayBanGiao) VALUES
('HD01', 'P801', 120.0, 42000000, '2026-01-01'),
('HD01', 'P802', 150.0, 52500000, '2026-01-01'),
('HD02', 'P301', 140.0, 53200000, '2026-03-01'),
('HD03', 'P502', 160.0, 64000000, '2026-02-01'),
('HD04', 'P101', 180.0, 117000000, '2026-01-01');
GO

-- 6. NẠP DỮ LIỆU PHIẾU THANH TOÁN TIỀN THUÊ
-- HD01: Đã thanh toán 6 tháng đầu (tháng 1 đến 6) và các tháng 7, 8, 9, 10 nộp đầu mỗi tháng
INSERT INTO dbo.PhieuThanhToanTienThue (MaPhieuThu, MaHD, DotThanhToan, KyThang, KyNam, SoTien, NgayThu, HinhThucThanhToan, NguoiThu, TrangThai) VALUES
('PT01', 'HD01', N'Thanh toán trả trước 6 tháng đầu (Tháng 1-6/2026)', 1, 2026, 567000000, '2026-01-02', N'Chuyển khoản', N'Kế toán Nguyễn Hoài An', N'Đã thu'),
('PT02', 'HD01', N'Tiền thuê tháng 07/2026 (Nộp đầu tháng)', 7, 2026, 94500000, '2026-07-03', N'Chuyển khoản', N'Kế toán Nguyễn Hoài An', N'Đã thu'),
('PT03', 'HD01', N'Tiền thuê tháng 08/2026 (Nộp đầu tháng)', 8, 2026, 94500000, '2026-08-02', N'Chuyển khoản', N'Kế toán Nguyễn Hoài An', N'Đã thu'),
('PT04', 'HD01', N'Tiền thuê tháng 09/2026 (Nộp đầu tháng)', 9, 2026, 94500000, '2026-09-03', N'Chuyển khoản', N'Kế toán Nguyễn Hoài An', N'Đã thu'),
('PT05', 'HD01', N'Tiền thuê tháng 10/2026 (Nộp đầu tháng)', 10, 2026, 94500000, '2026-10-02', N'Chuyển khoản', N'Kế toán Nguyễn Hoài An', N'Đã thu'),
('PT06', 'HD02', N'Thanh toán trả trước 6 tháng đầu (Tháng 3-8/2026)', 3, 2026, 319200000, '2026-03-02', N'Chuyển khoản', N'Kế toán Nguyễn Hoài An', N'Đã thu'),
('PT07', 'HD03', N'Thanh toán trả trước 6 tháng đầu (Tháng 2-7/2026)', 2, 2026, 384000000, '2026-02-03', N'Chuyển khoản', N'Kế toán Nguyễn Hoài An', N'Đã thu');
GO

-- 7. NẠP DỮ LIỆU DANH MỤC DỊCH VỤ
INSERT INTO dbo.DichVu (MaDV, TenDV, DonGia, DonViTinh, MoTa) VALUES
('DV01', N'Điện năng tiêu thụ', 3500, N'kWh', N'Điện 3 pha sản xuất và chiếu sáng văn phòng'),
('DV02', N'Nước sinh hoạt', 18000, N'm3', N'Nước sạch phục vụ khối văn phòng'),
('DV03', N'Internet cáp quang chuyên dụng', 4500000, N'Gói/tháng', N'Băng thông quốc tế cam kết 200 Mbps'),
('DV04', N'Phí vệ sinh môi trường & rác thải', 1500000, N'Tháng', N'Thu gom rác và khử khuẩn định kỳ'),
('DV05', N'Gửi xe máy ngoài định mức', 120000, N'Xe/tháng', N'Phí giữ xe máy từ xe thứ 21 trở đi'),
('DV06', N'Gửi ô tô', 1800000, N'Xe/tháng', N'Phí đỗ xe ô tô tại tầng hầm B1-B2');
GO

-- 8. NẠP DỮ LIỆU HÓA ĐƠN DỊCH VỤ HÀNG THÁNG (Thanh toán cuối tháng)
INSERT INTO dbo.HoaDonDichVu (MaHDDV, MaHD, KyThang, KyNam, NgayLap, NgayHetHanThanhToan, TongTienDichVu, TrangThaiThanhToan, NgayThanhToan) VALUES
('HDDV-01', 'HD01', 7, 2026, '2026-07-28', '2026-07-31', 15235000, N'Đã thanh toán', '2026-07-30'),
('HDDV-02', 'HD01', 8, 2026, '2026-08-28', '2026-08-31', 16100000, N'Đã thanh toán', '2026-08-30'),
('HDDV-03', 'HD01', 9, 2026, '2026-09-28', '2026-09-30', 14850000, N'Đã thanh toán', '2026-09-29'),
('HDDV-04', 'HD02', 7, 2026, '2026-07-28', '2026-07-31', 9400000, N'Đã thanh toán', '2026-07-31'),
('HDDV-05', 'HD03', 7, 2026, '2026-07-28', '2026-07-31', 12500000, N'Chưa thanh toán', NULL);
GO

-- 9. NẠP DỮ LIỆU CHI TIẾT HÓA ĐƠN DỊCH VỤ
INSERT INTO dbo.ChiTietDichVu (MaHDDV, MaDV, ChiSoDau, ChiSoCuoi, SoLuongTieuThu, DonGia, ThanhTien) VALUES
('HDDV-01', 'DV01', 10500, 12950, 2450, 3500, 8575000),
('HDDV-01', 'DV02', 320, 440, 120, 18000, 2160000),
('HDDV-01', 'DV03', NULL, NULL, 1, 4500000, 4500000),
('HDDV-02', 'DV01', 12950, 15600, 2650, 3500, 9275000),
('HDDV-02', 'DV02', 440, 565, 125, 18000, 2250000),
('HDDV-02', 'DV03', NULL, NULL, 1, 4500000, 4500000),
('HDDV-02', 'DV05', NULL, NULL, 1, 120000, 120000);
GO

-- 10. NẠP DỮ LIỆU PHIẾU GIA HẠN THUÊ VĂN PHÒNG
-- HD01 (Đông Á) thuê 12 tháng, gia hạn 6 tháng cho P801 (thỏa mãn <= 1/2 HĐ gốc, không nợ tiền phòng & dịch vụ)
INSERT INTO dbo.PhieuGiaHan (MaPGH, SoPGH, MaHD, MaPhong, NgayLap, ThoiGianGiaHan, NgayBatDauGiaHan, NgayKetThucGiaHan, DonGiaGiaHanThang, NguoiDuyet, TrangThai, GhiChu) VALUES
('PGH01', 'PGH-2026-001', 'HD01', 'P801', '2026-10-15', 6, '2027-01-01', '2027-06-30', 44000000, N'Trưởng BQL Trần Văn An', N'Đã phê duyệt', N'Gia hạn hợp lệ: Không nợ tiền thuê và dịch vụ, thời gian 6 tháng <= 12/2');
GO

-- 11. NẠP DỮ LIỆU NHÂN VIÊN VĂN PHÒNG (Báo cáo an ninh cho BQL)
INSERT INTO dbo.NhanVienVanPhong (MaNV, HoTen, HinhAnh, MaKH, ChucVu, MaPhong, MaTang, SoCCCD, SoDienThoai, NgayTiepNhan, TrangThaiLamViec) VALUES
('NV01', N'Nguyễn Văn Hùng', 'nv01_hung.jpg', 'KH01', N'Kỹ sư phần mềm Lead', 'P801', 'T08', '079201001234', '0908123456', '2026-01-05', N'Đang làm việc'),
('NV02', N'Trần Thị Thu Hà', 'nv02_ha.jpg', 'KH01', N'Kế toán trưởng', 'P801', 'T08', '079198005678', '0912345678', '2026-01-05', N'Đang làm việc'),
('NV03', N'Lê Minh Tuấn', 'nv03_tuan.jpg', 'KH01', N'Trưởng phòng Kinh doanh', 'P802', 'T08', '079195009012', '0933456789', '2026-01-10', N'Đang làm việc'),
('NV04', N'Đặng Thanh Phong', 'nv04_phong.jpg', 'KH02', N'Lập trình viên', 'P301', 'T03', '079203003456', '0977889900', '2026-03-05', N'Đang làm việc'),
('NV05', N'Võ Hoàng Yến', 'nv05_yen.jpg', 'KH03', N'Biên tập viên', 'P502', 'T05', '079199007890', '0966554433', '2026-02-10', N'Đang làm việc');
GO

-- 12. NẠP DỮ LIỆU THẺ GIỮ XE
INSERT INTO dbo.TheGiuXe (MaTheXe, MaNV, BienSoXe, LoaiXe, NhanHieu, MauXe, NgayCap, TrangThai) VALUES
('TX01', 'NV01', '59-P1 987.65', N'Xe máy', N'Honda SH 150i', N'Trắng', '2026-01-06', N'Đang hoạt động'),
('TX02', 'NV02', '59-F2 345.67', N'Xe máy', N'Yamaha Grande', N'Đỏ đô', '2026-01-06', N'Đang hoạt động'),
('TX03', 'NV03', '51H-123.45', N'Ô tô', N'Mazda CX-5', N'Xám titan', '2026-01-11', N'Đang hoạt động'),
('TX04', 'NV04', '59-S3 678.90', N'Xe máy', N'Honda AirBlade', N'Đen mờ', '2026-03-06', N'Đang hoạt động'),
('TX05', 'NV05', '59-T1 112.23', N'Xe máy', N'Honda Vision', N'Xanh dương', '2026-02-11', N'Đang hoạt động');
GO

-- 13. NẠP DỮ LIỆU BIÊN BẢN KIỂM TRA ĐỊNH KỲ 1 NĂM
INSERT INTO dbo.BienBanKiemTra (MaBBKT, MaPhong, NgayKiemTra, NguoiKiemTra, TinhTrangHienTai, CoViPhamKetCau, KeHoachTuSua, KetLuan) VALUES
('BBKT01', 'P801', '2026-10-20', N'Kỹ sư Nguyễn Thành Long', N'Phòng sử dụng đúng công năng, hệ thống điện nước ổn định, kết cấu nguyên vẹn', 0, N'Vệ sinh lưới lọc điều hòa định kỳ', N'Đạt tiêu chuẩn an toàn'),
('BBKT02', 'P802', '2026-10-20', N'Kỹ sư Nguyễn Thành Long', N'Phòng sạch sẽ, kết cấu vách ngăn đúng bản vẽ ban đầu', 0, N'Sơn dặm chân tường khu vực pantry', N'Đạt tiêu chuẩn an toàn'),
('BBKT03', 'P502', '2026-10-22', N'Kỹ sư Lê Quốc Bảo', N'Phát hiện tự ý đập phá tường ngăn phòng để mở rộng không gian, tháo gỡ đầu phun PCCC trái phép', 1, N'Yêu cầu khôi phục tường gạch và đầu phun PCCC theo bản vẽ kỹ thuật tòa nhà', N'Vi phạm nghiêm trọng quy định kết cấu');
GO

-- 14. NẠP DỮ LIỆU QUYẾT ĐỊNH XỬ PHẠT VI PHẠM KẾT CẤU
INSERT INTO dbo.QuyetDinhXuPhat (MaQDXP, MaBBKT, MaPhong, MaKH, NgayLap, HanhViViPham, MucPhat, BienPhapKhacPhuc, HanNopPhat, TrangThaiNop, NgayNopPhat) VALUES
('QDXP01', 'BBKT03', 'P502', 'KH03', '2026-10-23', N'Tự ý đập bỏ tường ngăn phòng và di dời hệ thống chữa cháy tự động khi chưa được BQL cao ốc chấp thuận bằng văn bản', 35000000, N'Buộc khôi phục nguyên trạng tường gạch và kiểm định lại an toàn PCCC trong vòng 10 ngày', '2026-11-05', N'Chưa nộp', NULL);
GO

-- 15. NẠP DỮ LIỆU KẾ HOẠCH TU SỬA BẢO TRÌ TÒA NHÀ
INSERT INTO dbo.KeHoachTuSua (MaKHTS, MaBBKT, MaPhong, NoiDungTuSua, ChiPhiDuKien, NgayBatDau, NgayHoanTatDuKien, DonViThiCong, TrangThai) VALUES
('KHTS01', 'BBKT01', 'P801', N'Bảo dưỡng hệ thống điều hòa trung tâm VRV và kiểm định van áp suất', 4500000, '2026-11-01', '2026-11-02', N'Công ty Cơ điện Lạnh Đông Nam', N'Lên kế hoạch'),
('KHTS02', 'BBKT03', 'P502', N'Giám sát đơn vị thi công của bên thuê tái lập tường ngăn và thẩm duyệt lại hệ thống PCCC', 8000000, '2026-10-28', '2026-11-05', N'Đội Kỹ thuật Tòa nhà HUTECH', N'Đang thực hiện');
GO
