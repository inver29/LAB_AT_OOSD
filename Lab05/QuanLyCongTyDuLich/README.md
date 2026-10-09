BÀI LAB 5: HỆ THỐNG QUẢN LÝ CÔNG TY DU LỊCH VĂN HÓA VIỆT

		Họ và tên: Nguyễn Hoài An
		Lớp: 12_ĐH_CNPM1
		MSSV: 1250080245
		Trường: Đại học Tài Nguyên và Môi trường TP.HCM
		Môn học: Phương pháp phát triển phần mềm hướng đối tượng

1. Môi trường phát triển

		IDE: Visual Studio 2022
		Nền tảng: Windows Forms App (.NET Framework 4.7.2)
		Cơ sở dữ liệu: Microsoft SQL Server (LocalDB / SQL Server Express)
		Ngôn ngữ: C#

2. Nội dung đã thực hiện

		Phân tích và thiết kế hệ thống Quản lý công ty du lịch Văn Hóa Việt với bộ mô hình UML hoàn chỉnh: Use Case tổng quát, Use Case phân rã (đăng ký -> kết thúc tour, quản lý tour), Class Diagram phân tích và thiết kế chi tiết, Biểu đồ trạng thái (Phiếu đoàn, Chuyến lẻ, Phiếu khảo sát), Biểu đồ tuần tự (Lập phiếu đoàn, Đăng ký khách lẻ, Phân công HDV, Thanh toán sau tour, Khảo sát, Tính lương) và Biểu đồ hoạt động.
		Thiết kế cơ sở dữ liệu SQL Server với 16 bảng chuẩn hóa: Tour, PhuongTien, DiemThamQuan, DiemBanVe, HuongDanVien, TourDiemDung, TourPhuongTien, TourDiemThamQuan, ChuyenLe, DoanKhach, DangKyDoan, ThanhVienDoan, DangKyLe, PhanCongHDV, ThanhToanDoan, KhaoSat.
		Thiết lập đầy đủ các ràng buộc toàn vẹn dữ liệu: Primary Key, Foreign Key, CHECK ràng buộc hạng sao khách sạn (2 - 5 sao), số lượng người đi tour lẻ (< 12) và tour đoàn (> 12), tiền cọc > 0, ngày kết thúc >= ngày đi; UNIQUE INDEX có điều kiện đảm bảo 1 HDV / chuyến lẻ và 1 khảo sát / đăng ký.
		Xây dựng kiến trúc phân tầng 3 lớp chuẩn mực: Presentation Layer (WinForms UI) -> Business Logic Layer (Services) -> Data Access Layer (Data/Db.cs).
		Hiện thực đầy đủ 9 Form (bao gồm cả mã nguồn logic .cs và thiết kế .Designer.cs): FrmMain (trang chủ điều hướng), FrmDanhMuc, FrmTour, FrmChuyenLe, FrmDangKyLe, FrmDangKyDoan, FrmPhanCongHDV, FrmKetThucKhaoSat, FrmLuongThongKe.
		Kết nối SQL Server thông qua Db.cs dùng ConfigurationManager, xử lý giao dịch an toàn với SqlTransaction (đăng ký đoàn kèm danh sách bảo hiểm, hủy phiếu đoàn mất cọc và gỡ phân công).
		Nâng cấp giao diện phẳng hiện đại (Modern Flat UI), thiết lập DpiAwareness PerMonitorV2, giải quyết triệt để lỗi scale co giãn và mất chữ trên các bảng DataGridView.

3. Kết quả đạt được

		FrmMain đóng vai trò dashboard điều hướng trực quan, mở các màn hình nghiệp vụ qua ShowDialog() và đảm bảo tính đóng gói độc lập giữa các Form.
		FrmDanhMuc quản lý danh mục dùng chung gồm 4 tab: Phương tiện, Điểm bán vé, Hướng dẫn viên (lương cơ bản), Điểm tham quan.
		FrmTour quản lý chi tiết hành trình: điểm dừng theo thứ tự (kiểm tra chuẩn sao khách sạn), phương tiện theo từng chặng và các điểm tham quan gắn liền với tour.
		FrmChuyenLe lập lịch chuyến cho khách lẻ, tự động tính ngày về theo số ngày của tour (NgayVe = NgayDi + SoNgay - 1), hỗ trợ đóng đăng ký khi chuyến đủ chỗ hoặc hết hạn.
		FrmDangKyLe tiếp nhận đăng ký khách lẻ (< 12 người) tại các điểm bán vé, tự động tính tiền vé theo đơn giá tour và xác nhận thanh toán vé ngay.
		FrmDangKyDoan tiếp nhận đoàn (> 12 người), tự động tính ngày kết thúc dự kiến và tổng tiền; quản lý danh sách người cùng đi khi đoàn mua bảo hiểm; lưu toàn bộ thông tin đoàn, phiếu đăng ký và danh sách người đi trong 1 Transaction an toàn; hỗ trợ hủy đăng ký (mất tiền cọc và tự động gỡ phân công HDV).
		FrmPhanCongHDV quản lý phân công HDV cho chuyến lẻ (đúng 1 HDV) hoặc tour đoàn (có thể nhiều HDV), tự động kiểm tra và ngăn chặn trùng chéo lịch công tác của HDV.
		FrmKetThucKhaoSat quản lý thanh toán sau tour của đoàn (chỉ thanh toán sau ngày kết thúc chuyến, kiểm tra không vượt số tiền còn lại); quản lý gửi phiếu khảo sát cho các tour đã hoàn tất và ghi nhận điểm đánh giá (1 - 5), ý kiến góp ý của khách hàng.
		FrmLuongThongKe tự động tính lương HDV trong tháng theo công thức: Lương = Lương căn bản + Tổng thù lao các tour kết thúc trong tháng; cung cấp bảng thống kê 5 chỉ số kinh doanh tổng hợp trong khoảng thời gian tùy chọn.
		Đạt kết quả kiểm thử chính xác 24/24 Test Case nghiệp vụ theo kịch bản của đề tài.

4. Lỗi gặp phải

		Ban đầu nhãn tiêu đề "Tour đang chọn (cho các tab hành trình):" bị ComboBox che lấp ở các màn hình có tỉ lệ hiển thị (DPI scaling) cao. Đã khắc phục bằng cách điều chỉnh tọa độ và bổ sung căn lề động (cboTour.Left = lblTourChon.Right + 12) cùng cấu hình DpiAwareness PerMonitorV2 trong App.config.
		Các cột bảng DataGridView ở chế độ AutoSizeColumnsMode.Fill mặc định bị chia đều tỷ lệ, khiến các cột mã/số chiếm quá nhiều diện tích và cột tên tour/mô tả bị cắt ngắn hiển thị dấu "...". Đã xử lý bằng hàm ToiUuCot trong FormHelper.cs, phân loại thông minh để co gọn cột số/ngày/tiền và giãn tối đa cột văn bản dài.
		Trường hợp nhóm đúng 12 người: Đề bài chỉ quy định khách lẻ dưới 12 người và khách đoàn trên 12 người; hệ thống xử lý bằng cách từ chối ở cả hai luồng và hiển thị thông báo hướng dẫn đưa ra quyết định nghiệp vụ (theo quyết định QD01).
		Chưa tích hợp cổng thanh toán trực tuyến qua ngân hàng/thẻ tín dụng (hệ thống hiện tại ghi nhận thanh toán tiền mặt/chuyển khoản nội bộ).

5. Hướng dẫn chạy chương trình

	Bước 1: Khởi tạo Cơ sở dữ liệu

		Mở SQL Server Management Studio (SSMS) hoặc cửa sổ SQL Server trong Visual Studio và kết nối đến SQL Server (Ví dụ: (localdb)\MSSQLLocalDB hoặc .\SQLEXPRESS).
		Mở file Database/QuanLyCongTyDuLich.sql trong thư mục project.
		Nhấn Execute (F5) để thực thi script: tự động tạo CSDL QuanLyCongTyDuLich, 16 bảng, các ràng buộc toàn vẹn và nạp sẵn dữ liệu mẫu.

	Bước 2: Cấu hình kết nối

		Mở Solution QuanLyCongTyDuLich.sln bằng Visual Studio 2022.
		Mở file App.config trong project.
		Trong thẻ connectionStrings, kiểm tra giá trị Data Source=... trong chuỗi kết nối QuanLyCongTyDuLichDB sao cho phù hợp với SQL Server trên máy (Mặc định: Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyCongTyDuLich;Integrated Security=True).
		Lưu file App.config nếu có chỉnh sửa.

	Bước 3: Biên dịch và Khởi chạy

		Trên thanh menu Visual Studio, chọn Build -> Rebuild Solution (Ctrl + Shift + B) để biên dịch toàn bộ mã nguồn.
		Nhấn Start (F5) để khởi chạy ứng dụng. Màn hình đầu tiên xuất hiện là trang chủ điều hướng (FrmMain).
		Nhấn chọn các nút chức năng trên màn hình chính để mở từng phân hệ:
		- Danh mục: xem và thêm mới phương tiện, điểm bán vé, hướng dẫn viên, điểm tham quan.
		- Tour - hành trình: chọn tour xem các điểm dừng, chặng phương tiện, điểm tham quan; thêm tour mới.
		- Lịch chuyến khách lẻ: tạo chuyến lẻ, tự động hiển thị ngày về, đóng đăng ký chuyến.
		- Đăng ký khách lẻ: chọn chuyến mở đăng ký, chọn điểm bán, nhập số người (< 12) để xem thành tiền và bấm Đăng ký và thanh toán vé.
		- Đăng ký theo đoàn: nhập thông tin đoàn, số người (> 12), tiền cọc, danh sách người đi khi mua bảo hiểm; kiểm tra tính năng hủy phiếu mất cọc.
		- Phân công hướng dẫn viên: phân công HDV cho chuyến hoặc đoàn, kiểm tra hệ thống chặn trùng lịch.
		- Kết thúc tour - khảo sát: ghi nhận thanh toán sau tour cho đoàn và gửi khảo sát, ghi nhận điểm/góp ý cho các tour đã kết thúc.
		- Lương - thống kê: chọn tháng/năm để xem bảng lương HDV và thống kê 5 chỉ số kinh doanh theo khoảng thời gian.
