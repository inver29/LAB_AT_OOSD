# Bài 5 – Quản lý hồ sơ bệnh án

Visual Studio 2022 · WinForms · .NET Framework 4.7.2 · SQL Server.

Mở `QuanLyHoSoBenhAn.sln`, chọn **Start (F5)**. Cấu hình kết nối trong `App.config`, mục `QuanLyHoSoBenhAnDB`:

```text
Data Source=.\MSSQLSERVER08;Initial Catalog=QuanLyHoSoBenhAn_Bai05;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5
```

Database dùng đúng 12 bảng từ script Word. `Database/SeedDemo.sql` bổ sung dữ liệu có nhãn `[DEMO]`, không tạo bảng, không xóa hoặc cập nhật dữ liệu có sẵn; chạy lại không thêm trùng. Đã chạy trên database hiện tại. Ảnh mẫu là `Assets/demo.png`.

## Năm form đã triển khai

- **FrmMain:** tiêu đề, bốn nút mở các form nghiệp vụ và nút Thoát.
- **FrmHoSo:** tìm theo mã hồ sơ / tên / điện thoại; đăng ký bệnh nhân kèm ảnh và cấp hồ sơ; gia hạn hồ sơ hết hạn. Ngày hết hạn bằng ngày đóng phí cộng một năm. Ảnh tự chọn được sao chép vào `%LOCALAPPDATA%\QuanLyHoSoBenhAn\Anh`.
- **FrmKhamBenh:** tạo phiếu Điều trị, chọn bác sĩ / khoa / tiền khám; lưu triệu chứng, thân nhiệt, huyết áp, nhịp tim và chẩn đoán; hoàn tất khám. Chỉ hoàn tất khi đủ chẩn đoán và tất cả xét nghiệm đã có kết quả. Phiếu đã thanh toán chỉ được xem.
- **FrmToaThuoc:** chọn phiếu đã hoàn tất, thêm / bỏ dòng thuốc, nhập liều và cách dùng; lưu toa cùng toàn bộ chi tiết trong một giao dịch. Toa đã lưu chỉ tra cứu; mỗi phiếu tối đa một toa. Đơn giá được lấy lại từ danh mục lúc lưu và giữ nguyên trong chi tiết.
- **FrmThanhToan:** xem chi phí khám + xét nghiệm + thuốc; xác nhận thu tiền một lần. Nếu chi phí vừa thay đổi, phải làm mới trước khi thu.

## Luồng chạy thử

1. Hồ sơ: xem 3 bệnh nhân mẫu; chọn hồ sơ hết hạn để thử gia hạn, hoặc Nhập mới và chọn ảnh để đăng ký.
2. Khám bệnh: Phiếu mới → chọn hồ sơ / bác sĩ / khoa → Tạo phiếu → nhập triệu chứng và chẩn đoán → Hoàn tất khám.
3. Toa thuốc: chọn phiếu vừa khám → chọn thuốc, số lượng, liều dùng và cách dùng → Thêm thuốc → Lưu toa thuốc.
4. Thanh toán: chọn phiếu → xem từng khoản → Xác nhận thanh toán. Có thể thanh toán phiếu không kê thuốc nếu không cần toa.

Giai đoạn này chưa làm giao diện đặt lịch / khám định kỳ miễn phí, chỉ định và nhập kết quả xét nghiệm, theo dõi sau điều trị, quản lý danh mục hoặc đăng nhập. Danh mục và một phiếu có kết quả xét nghiệm được nạp bằng dữ liệu mẫu. Việc chọn bác sĩ là gán người phụ trách, không phải xác thực tài khoản. Database và nghiệp vụ còn lại của bài được giữ để phát triển tiếp.

Code theo lớp: `Forms` → `Services` → `Data/Db.cs`. Các file `.Designer.cs` chứa control chuẩn để mở trong Visual Studio Designer; phần xử lý sự kiện nằm trong `.cs` cùng tên.

Chạy kiểm thử tích hợp bằng `Tests/RunTests.ps1`. Script tạo database kiểm thử tên ngẫu nhiên, chạy các service thật và tự xóa đúng database kiểm thử đó khi xong; không ghi test vào database đang dùng.
