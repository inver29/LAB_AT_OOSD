BÀI LAB 8: HỆ THỐNG QUẢN LÝ CHO THUÊ VĂN PHÒNG Ở CAO ỐC

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

		Phân tích nghiệp vụ cho thuê văn phòng với 4 tác nhân: Đại diện công ty thuê, Quản lý tòa nhà, Kế toán và Bảo vệ. Hệ thống gồm 7 use case, bao quát danh mục, hợp đồng, tiền thuê, dịch vụ, gia hạn, nhân viên – thẻ xe và kiểm tra – vi phạm.
		Thiết kế cơ sở dữ liệu SQL Server gồm 14 bảng: Tang, CongTy, VanPhong, HopDong, ChiTietHopDong, PhieuGiaHan, ChiTietGiaHan, DichVu, HoaDon, ChiTietHoaDon, NhanVien, TheGiuXe, KiemTra và ViPham. Ứng dụng sử dụng các bảng đã thiết kế trong báo cáo Lab08.
		Thiết lập Primary Key, Foreign Key, CHECK và UNIQUE để kiểm soát dữ liệu liên kết, số chứng từ, thời hạn và số tiền. Chỉ mục lọc bảo đảm một biển số chỉ có tối đa một thẻ còn hiệu lực.
		Xây dựng kiến trúc phân lớp: Forms -> Services -> Data/Db.cs -> SQL Server. App.config quản lý chuỗi kết nối; các truy vấn nhận tham số. Thao tác hợp đồng, hóa đơn, gia hạn và ngừng nhân viên sử dụng transaction để lưu đầy đủ hoặc rollback khi có lỗi.
		Xây dựng 7 Form: FrmMain, FrmDanhMuc, FrmHopDong, FrmHoaDon, FrmGiaHan, FrmNhanVienTheXe và FrmKiemTra. Các chức năng liên quan được nhóm thành tab, thống nhất màu sắc và bố cục theo các lab trước.
		Báo cáo nằm trong Lab08.docx; sơ đồ chỉnh sửa được nằm trong SoDo; script tạo cơ sở dữ liệu và dữ liệu mẫu nằm trong Database. Ảnh giao diện và bản sao sơ đồ draw.io được tập hợp trong IMAGES/LAB08 của repository, theo cách bố trí chung của các lab.

3. Kết quả đạt được

		FrmMain điều hướng đến 6 nhóm nghiệp vụ, chọn vai trò mô phỏng, hiển thị trạng thái kết nối và có nút Thoát. Giao diện dùng tiếng Việt, nền sáng, nút xanh; các cửa sổ và bảng dữ liệu co giãn theo kích thước hiển thị.
		FrmDanhMuc quản lý tầng, công ty, văn phòng và dịch vụ. Tra cứu phòng theo tầng, diện tích và khoảng thuê; phòng cần sửa hoặc đã có lịch giao nhau không được trả về trong danh sách phòng còn trống. Đơn giá danh mục thay đổi không làm thay đổi đơn giá hợp đồng cũ. Xóa danh mục đang được tham chiếu bị chặn.
		FrmHopDong lập hợp đồng cho một hoặc nhiều văn phòng, thời hạn tối thiểu 6 tháng. Hệ thống kiểm tra lại lịch và tình trạng phòng, lưu giá thỏa thuận từng phòng, đồng thời tạo hóa đơn tiền thuê 6 tháng đầu trong cùng transaction. Các phòng được khóa theo thứ tự mã để kiểm soát thao tác đồng thời.
		FrmHoaDon tra cứu hóa đơn và chi tiết, lập tiền thuê từng tháng từ tháng thứ 7 hoặc lập phí dịch vụ riêng. Tiền thuê theo ngày bắt đầu hợp đồng; kỳ dịch vụ theo tháng lịch hoặc phần kỳ cần chốt sớm. Đơn giá dịch vụ được lưu tại lúc lập, không lấy lại giá danh mục để tính hóa đơn cũ. Hệ thống chặn khoảng phí giao nhau; chỉ ghi nhận thanh toán một lần khi số tiền bằng tổng hóa đơn.
		FrmGiaHan chọn các phòng của cùng hợp đồng, đối chiếu hóa đơn và xác nhận Kế toán đã chốt phí phát sinh đến ngày lập. Chỉ gia hạn khi phòng còn trong hạn, không còn hóa đơn chưa thu, đã thu kỳ thuê hiện tại và không chồng lịch tiếp theo. Mỗi lần gia hạn tối đa một nửa thời hạn gốc, lấy phần nguyên; chỉ cập nhật hạn của các phòng được chọn, giữ nguyên thời hạn ban đầu của hợp đồng.
		Xác nhận chốt phí trên FrmGiaHan là thông tin do người phụ trách đối chiếu và xác nhận; chương trình không tự suy ra điện, nước hoặc dịch vụ chưa được nhập từ thực tế. Khoản phát sinh phải được lập bổ sung và thu đủ trước khi xác nhận.
		FrmNhanVienTheXe lưu hồ sơ nhân viên, ảnh, chức vụ và phòng làm việc; công ty được suy ra từ hợp đồng. Cấp thẻ cho nhân viên còn làm việc và phòng thuê đã được thanh toán cho ngày hiện tại. Có tra cứu theo mã thẻ, biển số hoặc nhân viên; giữ lịch sử thẻ đã thu hồi. Ngừng nhân viên đồng thời thu hồi toàn bộ thẻ đang sử dụng.
		FrmKiemTra ghi kết quả kiểm tra và cập nhật tình trạng vật lý của phòng, lọc các phòng chưa kiểm tra trong năm. Sau sửa chữa, phải ghi lần kiểm tra Đạt trước khi cho thuê mới. Biên bản vi phạm xác định đúng bên thuê tại ngày lập; lần kiểm tra liên quan là tùy chọn và phải thuộc đúng phòng.
		Đã build thành công cấu hình Debug và Release; kiểm tra 37 tình huống nghiệp vụ trên database thử riêng, gồm chống trùng lịch khi thao tác đồng thời và rollback khi lỗi. Đã mở đủ 17 màn hình/tab để rà bố cục, đồng thời kiểm tra ở kích thước cửa sổ nhỏ hơn. Bảng nhiều cột có thanh cuộn ngang, dòng văn bản dài tự xuống dòng và danh sách chọn mở rộng theo nội dung.
		Phạm vi là ứng dụng thực hành có dữ liệu mẫu và vai trò Demo, chưa có đăng nhập bằng tài khoản riêng. Chưa triển khai thu tiền nhiều lần, đổi phòng, đổi giá giữa kỳ, thu tiền phạt hoặc tích hợp ngân hàng và thiết bị kiểm soát xe. Đại diện công ty làm việc thông qua nhân viên phụ trách, chưa có cổng tự phục vụ.

4. Lỗi gặp phải

		Kết nối SQL có thể thất bại khi Data Source hoặc Initial Catalog không khớp máy đang chạy. Cần kiểm tra SQL Server đang hoạt động và tài khoản Windows có quyền truy cập database.
		Khi tạo chỉ mục lọc cho biển số, SQL Server yêu cầu các SET option phù hợp, đặc biệt QUOTED_IDENTIFIER. Script tạo database và Db.Open đã bổ sung các thiết lập này.
		DataGridView đặt trong tab chưa mở có thể lỗi khi tự sinh cột và chỉnh độ rộng. Đã chuyển sang tạo cột tường minh trước khi gắn DataSource để các tab tải ổn định.
		Ảnh nhân viên được lưu bằng đường dẫn tệp. Khi chuyển project sang vị trí hoặc máy khác, cần chọn lại ảnh tại màn hình hồ sơ nếu đường dẫn cũ không còn tồn tại. Ảnh mẫu nằm trong QuanLyVanPhong/Assets/nhan_vien_mau.png.

5. Hướng dẫn chạy chương trình

	Bước 1: Khởi tạo Cơ sở dữ liệu

		Các đường dẫn bên dưới được tính từ thư mục Lab08.
		Mở SQL Server Management Studio và kết nối SQL Server. Cấu hình hiện tại sử dụng instance .\MSSQLSERVER08.
		Nếu máy chưa có database, mở Database/QuanLyVanPhong.sql rồi Execute (F5) để tạo QuanLyVanPhong_Lab08 và 14 bảng. Nếu đã tạo đầy đủ bằng script trong báo cáo thì bỏ qua bước tạo lại; không chạy lại CREATE DATABASE hoặc CREATE TABLE trên database đang có dữ liệu.
		Mở Database/02_DuLieuMau.sql và Execute để nạp ví dụ. Script chỉ nạp khi chưa có hợp đồng, không xóa dữ liệu hiện có và không nhân đôi bộ mẫu khi chạy lại.
		Ngày mẫu được tính theo đầu tháng tại lần nạp đầu tiên. Có 3 công ty, 6 văn phòng, 4 dịch vụ, 2 hợp đồng, hóa đơn, nhân viên, thẻ xe, kiểm tra và vi phạm. HD-MAU-01 có tiền thuê hiện tại đã thu; HD-MAU-02 có một hóa đơn dịch vụ chưa thanh toán để minh họa công nợ. VP202 và VP301 còn trống; VP302 cần sửa.

	Bước 2: Cấu hình kết nối

		Mở QuanLyVanPhong/QuanLyVanPhong.sln trong Visual Studio 2022. Máy cần có workload .NET desktop development và .NET Framework 4.7.2 Targeting Pack.
		Mở QuanLyVanPhong/App.config, tìm connectionStrings có tên QuanLyVanPhongDB.
		Chuỗi kết nối hiện tại: Data Source=.\MSSQLSERVER08;Initial Catalog=QuanLyVanPhong_Lab08;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5.
		Sửa Data Source cho đúng SQL Server trên máy; giữ Initial Catalog nếu dùng script đi kèm. Integrated Security=True dùng tài khoản Windows. Project đã tham chiếu System.Configuration và dùng System.Data.SqlClient tương tự Lab06, Lab07.

	Bước 3: Biên dịch và Khởi chạy

		Chọn Build -> Rebuild Solution, sau đó Start (F5) để mở FrmMain. Có thể dùng vai trò Toàn quyền (demo) khi trình bày toàn bộ chức năng; các vai trò riêng chỉ cho phép ghi dữ liệu theo nhiệm vụ tương ứng.
		Vào Danh mục: chọn khoảng thuê và điều kiện lọc, nhấn Tra cứu. Chọn dòng để sửa hoặc Nhập mới để thêm. Tình trạng phòng được cập nhật qua lần kiểm tra, không sửa trực tiếp trong danh mục.
		Vào Hợp đồng -> Lập hợp đồng: nhập số hợp đồng, công ty, ngày ký, ngày bắt đầu, số tháng và điều khoản; chọn phòng trống, nhập giá thỏa thuận rồi Thêm phòng. Kiểm tra danh sách trước khi Lập hợp đồng. Hóa đơn 6 tháng đầu được tạo tự động.
		Vào Hóa đơn / Thu tiền: chọn hóa đơn, kiểm tra chi tiết và số tiền, nhập ngày thu rồi Xác nhận thu đủ. Khi tới kỳ tiếp theo từ tháng thứ 7, dùng tab Lập tiền thuê; nhập đúng ngày theo kỳ hợp đồng. Không lập lại phần đã có hóa đơn.
		Vào Lập phí dịch vụ: chọn hợp đồng, khoảng phí, phòng và dịch vụ; nhập số lượng thực tế, thêm các dòng rồi lập hóa đơn. Đến ngày không gồm trong khoảng tính phí. Không có phát sinh thì không tạo hóa đơn rỗng.
		Vào Gia hạn: chọn hợp đồng, đánh dấu từng phòng, nhập số phiếu và số tháng. Đối chiếu công nợ; nếu có khoản chưa lập hoặc chưa thu thì xử lý ở Hóa đơn trước. Sau khi Kế toán đã chốt và thu đủ mọi phí phát sinh đến hôm nay, chọn xác nhận rồi Lập phiếu gia hạn. Tra cứu hạn cũ/hạn mới tại tab Lịch sử gia hạn.
		Vào Nhân viên và thẻ giữ xe: chọn Nhập mới, nhập thông tin, chọn phòng còn thuê và ảnh rồi lưu. Chuyển sang tab Thẻ giữ xe để cấp hoặc thu hồi. Khi nhân viên nghỉ, dùng Ngừng làm việc để thu hồi các thẻ liên quan.
		Vào Kiểm tra: chọn văn phòng, ngày, kết quả và ghi chú rồi lưu. Xem Phòng cần kiểm tra theo năm. Tại Biên bản vi phạm, chọn phòng/công ty chịu trách nhiệm, lần kiểm tra nếu có, nội dung và tiền phạt rồi lập biên bản.

Ảnh giao diện minh họa

[Trang chính](../IMAGES/LAB08/FrmMain.png) · [Danh mục](../IMAGES/LAB08/FrmDanhMuc.png) · [Hợp đồng](../IMAGES/LAB08/FrmHopDong.png) · [Hóa đơn](../IMAGES/LAB08/FrmHoaDon.png) · [Gia hạn](../IMAGES/LAB08/FrmGiaHan.png) · [Nhân viên – Thẻ xe](../IMAGES/LAB08/FrmNhanVienTheXe.png) · [Kiểm tra](../IMAGES/LAB08/FrmKiemTra.png)
