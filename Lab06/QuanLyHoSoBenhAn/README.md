BÀI LAB 6: HỆ THỐNG QUẢN LÝ HỒ SƠ BỆNH ÁN

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

		Thiết kế cơ sở dữ liệu SQL Server gồm 12 bảng nghiệp vụ: BenhNhan, HoSoBenhAn, NhanVien, KhoaDieuTri, LichHenKham, PhieuKhamBenh, LoaiXetNghiem, ChiTietXetNghiem, Thuoc, ToaThuoc, ChiTietToaThuoc và TheoDoiDieuTri.
		Thiết lập các ràng buộc Primary Key, Foreign Key, CHECK và UNIQUE để kiểm soát liên kết, ngày hạn phí, đơn giá, số lượng thuốc; bảo đảm mỗi bệnh nhân có tối đa một hồ sơ và mỗi phiếu khám có tối đa một toa thuốc.
		Xây dựng kiến trúc phân lớp: Presentation Layer (WinForms UI) -> Business Logic Layer (Services) -> Data Access Layer (Db.cs).
		Hoàn thiện 5 Form: FrmMain, FrmHoSo, FrmKhamBenh, FrmToaThuoc và FrmThanhToan.
		Kết nối database qua App.config, sử dụng truy vấn có tham số và transaction khi đăng ký hồ sơ, lưu toa thuốc, thanh toán. Bổ sung dữ liệu mẫu có nhãn [DEMO] để chạy thử.

3. Kết quả đạt được

		FrmMain điều hướng đến 4 trang chức năng. Các trang hiển thị dữ liệu từ SQL Server, hỗ trợ tìm kiếm và làm mới danh sách.
		Thực hiện được luồng: Đăng ký và cấp hồ sơ -> Lập phiếu khám điều trị -> Ghi nhận triệu chứng, sinh hiệu, chẩn đoán -> Hoàn tất khám -> Kê toa nếu cần -> Thanh toán viện phí.
		Tra cứu hồ sơ theo mã, họ tên hoặc điện thoại; đăng ký kèm ảnh 3x4 và gia hạn phí cho hồ sơ hết hạn. Hạn phí được tính bằng ngày đóng phí cộng một năm.
		Hệ thống chặn đăng ký trùng thông tin, lập toa thứ hai, số lượng thuốc không hợp lệ, thanh toán trùng và sửa phiếu đã thanh toán. Hoàn tất khám yêu cầu có chẩn đoán và đủ kết quả xét nghiệm nếu đã chỉ định.
		Đã build thành công, kiểm tra 4 nút điều hướng, bố cục 5 Form ở hai kích thước cửa sổ và 20 tình huống nghiệp vụ trên database kiểm thử riêng.
		Phạm vi giao diện hiện tại gồm 4 trang tiêu biểu. Chưa triển khai đăng nhập, giao diện quản lý danh mục, đặt lịch khám định kỳ, nhập kết quả xét nghiệm và theo dõi sau điều trị. Danh mục và kết quả xét nghiệm minh họa được nạp bằng script dữ liệu mẫu.

4. Lỗi gặp phải

		Lần đầu nạp danh sách chưa tự chọn dòng nên phần chi tiết có thể trống; đã chỉnh lại việc chọn ô hiện hành sau khi cấu hình các cột.
		Màu nút có thể thay đổi khi đóng Form con; đã chỉnh để giữ lại màu giao diện. Các nút bị khóa được hiển thị bằng màu xám để dễ nhận biết.

5. Hướng dẫn chạy chương trình

	Bước 1: Khởi tạo Cơ sở dữ liệu

		Mở SQL Server Management Studio và kết nối SQL Server trên máy.
		Mở file Database\Schema.sql trong project QuanLyHoSoBenhAn, nhấn Execute để tạo database QuanLyHoSoBenhAn_Bai05 và các bảng.
		Nếu đã tạo database bằng script trong file Word thì bỏ qua bước tạo lại bảng.
		Chọn đúng database QuanLyHoSoBenhAn_Bai05, mở Database\SeedDemo.sql và nhấn Execute để thêm dữ liệu mẫu. Script không xóa dữ liệu đang có và có thể chạy lại mà không nhân đôi dữ liệu mẫu.

	Bước 2: Cấu hình kết nối

		Mở Solution QuanLyHoSoBenhAn.sln trong thư mục project bằng Visual Studio 2022.
		Mở App.config. Trong thẻ connectionStrings, sửa Data Source=... cho khớp tên SQL Server trên máy. Cấu hình hiện tại: Data Source=.\MSSQLSERVER08.
		Giữ Initial Catalog=QuanLyHoSoBenhAn_Bai05. Kết nối sử dụng Integrated Security=True để xác thực bằng tài khoản Windows.

	Bước 3: Biên dịch và Khởi chạy

		Chọn Build -> Clean Solution, sau đó Rebuild Solution.
		Nhấn Start (F5). Màn hình đầu tiên là FrmMain, gồm 4 nút chức năng và nút Thoát.
		Vào Hồ sơ bệnh nhân để tra cứu hoặc đăng ký mới, chọn ảnh JPG/PNG và cấp hồ sơ. Ảnh tự chọn được sao chép vào %LOCALAPPDATA%\QuanLyHoSoBenhAn\Anh.
		Vào Khám bệnh -> Phiếu mới -> chọn hồ sơ, bác sĩ, khoa và tiền khám -> Tạo phiếu. Nhập thông tin khám rồi chọn Lưu khám hoặc Hoàn tất khám.
		Vào Toa thuốc, chọn phiếu đã hoàn tất khám và chưa thanh toán; nhập thuốc, số lượng, liều dùng, cách dùng -> Thêm thuốc -> Lưu toa thuốc. Toa đã lưu chỉ được tra cứu.
		Vào Thanh toán viện phí, chọn phiếu và kiểm tra các khoản thu trước khi xác nhận. Tổng viện phí gồm tiền khám, xét nghiệm và thuốc theo đơn giá đã lưu.
