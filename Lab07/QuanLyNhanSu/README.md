BÀI LAB 7: HỆ THỐNG QUẢN LÝ NHÂN SỰ CÔNG TY SƠN ĐÔNG Á

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

		Phân tích hệ thống quản lý nhân sự với 6 tác nhân và 9 use case. Hoàn thiện biểu đồ use case tổng quát, các use case phân rã, đặc tả use case, lớp phân tích, trạng thái, tuần tự, lớp thiết kế, hoạt động, ERD và sơ đồ điều hướng Form.
		Thiết kế cơ sở dữ liệu SQL Server gồm 17 bảng: PhongBan, NhanVien, QuyetDinhTuyenDung, HopDong, ChucVu, QuyetDinhChucVu, QuyetDinhLuong, HoSoBaoHiem, CaLamViec, ChamCong, PhieuPhuTroi, ChiTietPhuTroi, BangLuong, ChiTietBangLuong, QuyetDinhThuongPhat, ChiTietQuyetDinh và PhieuThuChi.
		Thiết lập Primary Key, Foreign Key, CHECK và UNIQUE để kiểm soát liên kết, số chứng từ, khoảng ngày, giờ làm, số tiền và trạng thái; bảo đảm mỗi kỳ chỉ có một bảng lương và mỗi quyết định trực tiếp chỉ có tối đa một phiếu thu hoặc chi.
		Xây dựng kiến trúc phân lớp: Presentation Layer (Forms) -> Business Logic Layer (Services) -> Data Access Layer (Data/Db.cs). Kết nối SQL qua App.config, sử dụng truy vấn có tham số và transaction cho các thao tác nhiều bảng.
		Xây dựng 8 Form: FrmMain, FrmHoSoHopDong, FrmQuyetDinhNhanSu, FrmChamCongPhuTroi, FrmThuongPhat, FrmThuChi, FrmBangLuong và FrmDanhMuc. Các nhóm nghiệp vụ liên quan được bố trí thành tab trong cùng Form.
		Hoàn thiện báo cáo Lab07.docx đến phần 6, chèn đủ 8 ảnh giao diện. Sơ đồ chỉnh sửa được lưu trong SoDo, ảnh giao diện trong GiaoDien; script tạo cơ sở dữ liệu và dữ liệu mẫu nằm trong Database.

3. Kết quả đạt được

		FrmMain điều hướng đến 7 màn hình nghiệp vụ và có nút Thoát. Giao diện dùng tiếng Việt, nền sáng và các nút màu xanh; danh sách dữ liệu hiển thị bằng DataGridView.
		FrmHoSoHopDong quản lý hồ sơ, tiếp nhận thử việc, ghi kết quả và ký hợp đồng. Hợp đồng có loại ngắn hạn hoặc dài hạn; loại dài hạn cần thông tin BHXH và BHYT. Có kiểm tra kết quả thử việc, ngày ký và khoảng hiệu lực hợp đồng.
		FrmQuyetDinhNhanSu lập quyết định lương, chức vụ và tra cứu lịch sử theo nhân viên. Khi có quyết định mới, hệ thống kết thúc hiệu lực bản trước để lưu các lần thay đổi theo thời gian.
		FrmChamCongPhuTroi hỗ trợ phân ca theo ngày, nhập giờ làm thực tế, lập và xác nhận phiếu phụ trội. Giờ công để trống là chưa chấm; 0 giờ là vắng mặt. Phụ trội chỉ được tính lương khi đã xác nhận và không cộng lại vào giờ thường.
		FrmThuongPhat lập quyết định cho cá nhân hoặc phòng ban, phân biệt Trực tiếp và Qua lương. Quyết định qua lương có phân bổ cho từng người; quyết định Nháp được ký để chuyển sang Đã ký và làm căn cứ thực hiện.
		FrmThuChi lấy quyết định Trực tiếp đã ký để lập phiếu: thưởng dùng phiếu Chi, phạt dùng phiếu Thu. Khoản đã thu hoặc chi trực tiếp không được cộng/trừ lại trong bảng lương.
		FrmBangLuong hỗ trợ tính Nháp, kiểm tra điều kiện, chốt kỳ và xem phiếu lương. Tiền phụ trội dùng hệ số 1,5 theo đề; thực lĩnh gồm công, phụ trội, phụ cấp, thưởng, trừ phạt và bảo hiểm. Số giờ chuẩn tháng được nhập theo kỳ. Kỳ đã chốt không được tính lại hoặc sửa dữ liệu nguồn thuộc kỳ.
		FrmDanhMuc quản lý phòng ban, chức vụ và ca làm việc; chặn xóa danh mục đang được sử dụng. Ca làm việc và phụ trội được giới hạn trong cùng ngày, chưa hỗ trợ ca qua đêm.
		Phạm vi hiện tại là ứng dụng thực hành với dữ liệu mẫu và ngữ cảnh vai trò Demo, chưa có đăng nhập và phân quyền bằng tài khoản riêng. Công thức phân bổ lương, phụ cấp và bảo hiểm là giả định của bài; chưa triển khai thuế thu nhập, tạm ứng, điều chỉnh lương hồi tố hoặc tích hợp máy chấm công, ngân hàng, cổng bảo hiểm.

4. Lỗi gặp phải

		Khi chuyển project sang máy khác, chương trình có thể không kết nối được nếu tên SQL Server hoặc database trong App.config không khớp. Cần sửa đúng Data Source và kiểm tra tài khoản Windows có quyền truy cập database.
		Một số bảng có nhiều cột và khung lịch sử còn hẹp nên nội dung dài có thể hiện dấu "..."; cần kéo thanh cuộn ngang hoặc mở rộng cửa sổ để xem đủ dữ liệu.
		Không chốt được lương nếu tháng chưa kết thúc, còn ca chưa chấm công, phiếu phụ trội Kế hoạch hoặc quyết định Nháp Qua lương của kỳ. Cần xử lý các dữ liệu này rồi kiểm tra lại điều kiện chốt.

5. Hướng dẫn chạy chương trình

	Bước 1: Khởi tạo Cơ sở dữ liệu

		Các đường dẫn bên dưới được tính từ thư mục Lab07.
		Mở SQL Server Management Studio, kết nối đến SQL Server trên máy. Cấu hình hiện tại sử dụng instance .\MSSQLSERVER08.
		Mở Database\01_TaoCSDL.sql, nhấn Execute (F5) để tạo database QuanLyNhanSu_Bai07 và 17 bảng nghiệp vụ.
		Nếu đã tạo database bằng script trong báo cáo thì bỏ qua bước tạo lại bảng. Không chạy lại các lệnh CREATE TABLE trên database đã có đủ bảng.
		Mở Database\02_DuLieuMau.sql và nhấn Execute để nạp dữ liệu minh họa. Script có kiểm tra tồn tại để tránh nhân đôi các bản ghi mẫu; không dùng script này để đặt lại dữ liệu đã thao tác.
		Dữ liệu mẫu có nhân viên thử việc, nhân viên chính thức, hợp đồng, lương, bảo hiểm, công và phụ trội tháng 09/2026. Tháng 10/2026 có công chưa chấm, phụ trội Kế hoạch và quyết định Nháp để minh họa điều kiện chặn chốt lương.

	Bước 2: Cấu hình kết nối

		Mở QuanLyNhanSu\QuanLyNhanSu.sln bằng Visual Studio 2022. Máy cần có thành phần phát triển Windows Forms và .NET Framework 4.7.2 Targeting Pack.
		Mở QuanLyNhanSu\App.config, tìm connectionStrings có tên QuanLyNhanSuDB.
		Chuỗi kết nối hiện tại: Data Source=.\MSSQLSERVER08;Initial Catalog=QuanLyNhanSu_Bai07;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5.
		Sửa Data Source cho khớp tên SQL Server trên máy; giữ Initial Catalog=QuanLyNhanSu_Bai07 nếu dùng script đi kèm. Integrated Security=True sử dụng tài khoản Windows để kết nối.
		Lưu App.config sau khi chỉnh sửa. Project đã tham chiếu System.Configuration để đọc chuỗi kết nối.

	Bước 3: Biên dịch và Khởi chạy

		Chọn Build -> Clean Solution, sau đó Rebuild Solution. Nhấn Start (F5) để chạy FrmMain.
		Vào Hồ sơ và Hợp đồng để tra cứu hoặc tiếp nhận nhân viên mới; ghi kết quả tại tab Đánh giá Thử việc. Khi nhân viên đạt yêu cầu và hết thử việc, chuyển sang tab Ký Hợp đồng và Bảo hiểm.
		Vào Quyết định Lương và Chức vụ, chọn nhân viên, nhập số quyết định, ngày hiệu lực, lương hoặc phụ cấp rồi lưu; chọn nhân viên trong danh sách để xem lịch sử.
		Vào Phân ca, Chấm công và Phụ trội: phân ca trước, ghi giờ thực tế tại tab chấm công, sau đó lập và xác nhận các đợt phụ trội đã thực hiện.
		Vào Khen thưởng và Kỷ luật: chọn cá nhân hoặc tập thể, nhập số tiền và hình thức thanh toán, lập quyết định Nháp rồi ký duyệt. Với Qua lương, chọn đúng kỳ và kiểm tra danh sách phân bổ.
		Vào Thu chi để chọn quyết định Trực tiếp đã ký, kiểm tra loại phiếu và số tiền, nhập số phiếu cùng ngày lập rồi xác nhận. Xem lại tại tab Danh sách Phiếu đã lập.
		Vào Bảng lương, chọn tháng/năm và giờ chuẩn H -> Tính Nháp bảng lương -> Kiểm tra điều kiện chốt -> Chốt bảng lương. Có thể dùng tháng 09/2026, H = 176 để xem dữ liệu mẫu; nếu kỳ này đã chốt thì chỉ tra cứu, không tạo lại hoặc sửa trực tiếp SQL để mở khóa.
		Vào Quản lý Danh mục để thêm hoặc cập nhật phòng ban, chức vụ và ca. Không xóa danh mục đang có hồ sơ hay chứng từ liên kết.
