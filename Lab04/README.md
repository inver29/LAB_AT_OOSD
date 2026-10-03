BÀI LAB 4: HỆ THỐNG e-SHOPPING

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

		Phân tích và thiết kế hệ thống e-SHOPPING với các biểu đồ use case, lớp, trạng thái, tuần tự và hoạt động.
		Thiết kế cơ sở dữ liệu SQL Server với các bảng: NhomSanPham, SanPham, KhachHang, PhiGiaoHang, DonDatHang, ChiTietDonDatHang, ThanhToan, EmailXacNhan.
		Thiết lập các ràng buộc toàn vẹn dữ liệu: Primary Key, Foreign Key, CHECK số lượng/đơn giá và UNIQUE cho các trường cần phân biệt.
		Xây dựng kiến trúc phân lớp: Presentation Layer (WinForms UI) -> Business Logic Layer (Services) -> Data Access Layer (Db.cs).
		Thiết kế giao diện cho 4 Form minh họa: FrmMain, FrmSanPham, FrmGioHang, FrmCheckout.
		Kết nối SQL Server để hiển thị sản phẩm, khách hàng, tính phí giao hàng và lưu thông tin đơn đặt hàng.

3. Kết quả đạt được

		FrmMain hiển thị trạng thái kết nối SQL Server, số lượng sản phẩm, khách hàng, đơn hàng và số sản phẩm trong giỏ.
		FrmSanPham hiển thị danh sách sản phẩm từ SQL Server. Người dùng có thể tìm kiếm theo từ khóa, lọc theo nhóm và chọn số lượng để thêm vào giỏ.
		FrmGioHang cho phép cập nhật số lượng, xóa dòng, làm rỗng giỏ và lấy lại giá sản phẩm từ SQL Server.
		FrmCheckout cho phép nhập thông tin người nhận, chọn khu vực, loại giao và loại thẻ để tính tổng tiền.
		Lưu được đơn chờ thanh toán và chi tiết đơn vào SQL Server. Danh sách đơn đã lưu và chi tiết đơn đang chọn được hiển thị trên giao diện.
		Luồng minh họa đã thực hiện: Xem sản phẩm -> Thêm vào giỏ -> Cập nhật giỏ hàng -> Nhập thông tin đặt hàng -> Tính tổng -> Lưu đơn chờ thanh toán.

4. Lỗi gặp phải

		Chưa ghi nhận lỗi cụ thể trong các thao tác minh họa.
		Bản minh họa mới thực hiện 4 Form. Các Form còn lại chưa được triển khai.
		Chưa tích hợp dịch vụ thanh toán trực tuyến và gửi email xác nhận.
		Giỏ hàng được giữ trong phiên chạy chương trình, chưa lưu vào cơ sở dữ liệu.

5. Hướng dẫn chạy chương trình

	Bước 1: Khởi tạo Cơ sở dữ liệu

		Mở SQL Server Management Studio và kết nối đến SQL Server.
		Mở file script SQL của bài e-SHOPPING hoặc sao chép script trong phần 4.4 của báo cáo vào cửa sổ New Query.
		Nhấn Execute để chạy script và tạo cơ sở dữ liệu eShopping.
		Bổ sung dữ liệu mẫu cho nhóm sản phẩm, sản phẩm, khách hàng và phí giao hàng nếu cơ sở dữ liệu chưa có dữ liệu.

	Bước 2: Cấu hình kết nối

		Mở Solution của bài e-SHOPPING bằng Visual Studio 2022.
		Mở file App.config.
		Trong thẻ connectionStrings, sửa giá trị Data Source=... sao cho khớp với tên SQL Server trên máy (Ví dụ: Data Source=.\SQLEXPRESS hoặc Data Source=localhost).
		Kiểm tra giá trị Initial Catalog=eShopping để kết nối đúng cơ sở dữ liệu.

	Bước 3: Biên dịch và Khởi chạy

		Trên thanh công cụ Visual Studio, chọn Build -> Clean Solution, sau đó Rebuild Solution để làm sạch cache.
		Nhấn Start (F5) để chạy ứng dụng. Màn hình đầu tiên xuất hiện sẽ là trang chủ (FrmMain).
		Nhấn Kiểm tra kết nối để kiểm tra kết nối SQL Server.
		Mở Sản phẩm, chọn sản phẩm và thêm vào giỏ. Mở Giỏ hàng để cập nhật số lượng hoặc xóa sản phẩm.
		Nhấn Đặt hàng, nhập thông tin người nhận và chọn thông tin giao hàng.
		Nhấn Tính tổng, sau đó nhấn Lưu đơn chờ thanh toán.
		Kiểm tra đơn vừa lưu trong danh sách đơn và xem chi tiết các sản phẩm đã đặt.