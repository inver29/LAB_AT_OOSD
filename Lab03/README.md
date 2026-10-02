BÀI LAB 3: HỆ THỐNG QUẢN LÝ KHÁCH SẠN

		Họ và tên: Nguyễn Hoài An
		Lớp: 12_ĐH_CNPM1
		MSSV: 1250080245
		Trường: Đại học Tài Nguyên và Môi trường TP.HCM
		Môn học: Phương pháp phát triển phần mềm hướng đối tượng

1. Môi trường phát triển

		IDE: Visual Studio 2022
		Nền tảng: Windows Forms App (.NET Framework 4.7.2)
		Cơ sở dữ liệu: Microsoft SQL Server	
		Ngôn ngữ: C#

2. Nội dung đã thực hiện

		Thiết kế và khởi tạo cơ sở dữ liệu SQL Server với đầy đủ các bảng nghiệp vụ: KhuVuc, Phong, TienNghi, PhieuDatPhong, ChiTietDatPhong, DichVu, HoaDon, ThanhToan...
		Thiết lập chặt chẽ các ràng buộc toàn vẹn dữ liệu (Primary Key, Foreign Key, CHECK sức chứa/đơn giá, UNIQUE tránh trùng lặp thiết bị trong ngày).
		Xây dựng kiến trúc phân lớp rõ ràng: Presentation Layer (WinForms UI) -> Business Logic Layer (Services) -> Data Access Layer (Db.cs).
		Thiết kế giao diện (UI)  cho 7 Form chức năng: FrmMain, FrmDanhMuc, FrmPhongTienNghi, FrmDatPhong, FrmDichVu, FrmTraPhong, FrmThongKe.

3. Kết quả đạt được
	
		Giao diện hoạt động mượt mà hiển thị tốt.
		Luồng nghiệp vụ đa phần đều hoạt động được: Nhập danh mục -> Lập phiếu đặt phòng -> Ghi nhận người lưu trú -> Gọi dịch vụ -> Thanh toán -> Trả phòng.
		Các quy tắc nghiệp vụ được đảm bảo . Các thao tác sai logic (vượt quá sức chứa, trùng thời gian, thanh toán vượt mức) đều bị hệ thống chặn lại và cảnh báo rõ ràng.

4. Lỗi gặp phải

		Lỗi scale giao diện chưa được tối ưu và còn bị che nội dung ở một số chỗ.

5. Hướng dẫn chạy chương trình

	Bước 1: Khởi tạo Cơ sở dữ liệu

		Mở SQL Server.
		Mở file script QuanLyKhachSan.sql được đính kèm trong folder nộp bài.
		Nhấn Execute để chạy file.

	Bước 2: Cấu hình kết nối

		Mở Solution QuanLyKhachSan.sln bằng Visual Studio 2022.
		Mở file App.config.
		Trong thẻ connectionStrings, sửa giá trị Data Source=... sao cho khớp với tên SQL Server trên máy của (Ví dụ: Data Source=.\SQLEXPRESS hoặc Data Source=localhost).

	Bước 3: Biên dịch và Khởi chạy

		Trên thanh công cụ Visual Studio, chọn Build -> Clean Solution, sau đó Rebuild Solution để làm sạch cache.		
		Nhấn Start (F5) để chạy ứng dụng. Màn hình đầu tiên xuất hiện sẽ là hệ thống Menu chính (FrmMain).
