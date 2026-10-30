# BÀI LAB 8: HỆ THỐNG QUẢN LÝ CHO THUÊ VĂN PHÒNG Ở CAO ỐC (BÀI 4)

- **Trường:** Đại học Công nghệ TP.HCM (HUTECH)
- **Môn học:** Thực hành Phân tích thiết kế hướng đối tượng (OOSD)
- **Họ và tên sinh viên:** Nguyễn Hoài An
- **MSSV:** 1250080245
- **Lớp:** 12_ĐH_CNPM1
- **Nhóm thực hành:** 21
- **Ngày thực hiện:** 30/10/2026

---

## 1. Môi trường & Công cụ sử dụng

- **Hệ điều hành:** Microsoft Windows 11
- **Công cụ mô hình hóa:** Draw.io (diagrams.net) / StarUML
- **Soạn thảo báo cáo:** Microsoft Word (`Lab08.docx` và `Bai_4_Cho_thue_van_phong_cao_oc.docx`)
- **Hệ quản trị Cơ sở dữ liệu:** Microsoft SQL Server 2022
- **Nền tảng phát triển ứng dụng đề xuất:** C# Windows Forms App (.NET Framework 4.7.2), kiến trúc 3 tầng (Presentation Layer $\rightarrow$ Business Logic Layer $\rightarrow$ Data Access Layer).

---

## 2. Cấu trúc thư mục Lab08

```
C:\Study\LAB_AT_OOSD\Lab08\
│
├── Lab08.docx                          # Báo cáo Word hoàn chỉnh (Chuẩn form Lab07)
├── Bai_4_Cho_thue_van_phong_cao_oc.docx # Bản sao báo cáo theo tên đề bài gốc
├── README.md                           # Hướng dẫn chi tiết và tổng quan bài lab
│
├── SoDo/                               # Toàn bộ file sơ đồ định dạng gốc .drawio
│   ├── Hinh_2_1.drawio                 # Use Case Diagram tổng quát toàn hệ thống (Câu 3)
│   ├── Hinh_2_2.drawio                 # Phân rã Ký HĐ & Thanh toán tiền thuê, DV (Câu 4)
│   ├── Hinh_2_3.drawio                 # Phân rã Lập phiếu gia hạn thuê văn phòng
│   ├── Hinh_2_4.drawio                 # Phân rã Quản lý nhân viên văn phòng & thẻ xe
│   ├── Hinh_2_5.drawio                 # Phân rã Kiểm tra định kỳ 1 năm & xử phạt kết cấu
│   ├── Hinh_2_6.drawio                 # Phân rã Quản lý danh mục tòa nhà, tầng, phòng
│   ├── Hinh_2_7.drawio                 # Analysis Class Diagram (Câu 1 & Câu 2)
│   ├── Hinh_2_8.drawio                 # State Machine Diagram: Hợp đồng thuê
│   ├── Hinh_2_9.drawio                 # State Machine Diagram: Phòng văn phòng
│   ├── Hinh_2_10.drawio                # State Machine Diagram: Phiếu gia hạn
│   ├── Hinh_3_1.drawio                 # Sequence Diagram: Lập phiếu gia hạn (Câu 6)
│   ├── Hinh_3_2.drawio                 # Sequence Diagram: Ký HĐ & trả trước 6 tháng
│   ├── Hinh_3_3.drawio                 # Sequence Diagram: Thanh toán tháng 7+ & dịch vụ
│   ├── Hinh_3_4.drawio                 # Sequence Diagram: Tiếp nhận nhân viên & thẻ xe
│   ├── Hinh_3_5.drawio                 # Sequence Diagram: Kiểm tra & xử phạt kết cấu
│   ├── Hinh_3_6.drawio                 # Design Class Diagram chi tiết phân tầng
│   ├── Hinh_3_7.drawio                 # Thiết kế BCE: Hợp đồng & gia hạn
│   ├── Hinh_3_9.drawio                 # Activity Diagram: Ký HĐ đến thanh toán định kỳ (Câu 5)
│   ├── Hinh_4_1.drawio                 # Sơ đồ thực thể quan hệ (ERD) SQL Server
│   └── Hinh_5_1.drawio                 # Kiến trúc 3 tầng & Điều hướng Form
│
├── HinhAnh/                            # File ảnh PNG xuất ra từ Draw.io (nhúng vào Word)
│   ├── Hinh_2_1.png đến Hinh_2_10.png
│   ├── Hinh_3_1.png đến Hinh_3_9.png
│   ├── Hinh_4_1.png, Hinh_5_1.png
│   └── Hinh_6_1.png đến Hinh_6_7.png   # Thiết kế 7 giao diện WinForms
│
└── Database/                           # Kịch bản Cơ sở dữ liệu SQL Server
    ├── 01_TaoCSDL.sql                  # Tạo CSDL, 15 bảng, PK, FK, CHECK, Trigger
    └── 02_DuLieuMau.sql                # Nạp dữ liệu minh họa thực tế phong phú
```

---

## 3. Bảng đối chiếu giải quyết 6 câu hỏi của đề bài

| Câu hỏi | Nội dung yêu cầu | Vị trí giải quyết & Sơ đồ tương ứng |
| :--- | :--- | :--- |
| **Câu 1** | Xác định các Class cùng thuộc tính, phương thức tương ứng, chỉ định ID nhận dạng. | **Bảng 5 (Mục 2.2)** và **Mục 7 (Câu 1)**: Xác định 14 Class (`Tang`, `Phong`, `KhachHang`, `HopDongThue`, `ChiTietHopDongThue`, `PhieuThanhToanTienThue`, `DichVu`, `HoaDonDichVu`, `ChiTietDichVu`, `PhieuGiaHan`, `NhanVienVanPhong`, `TheGiuXe`, `BienBanKiemTra`, `QuyetDinhXuPhat`). |
| **Câu 2** | Tìm mối liên kết giữa các Class. Dùng StarUML / Draw.io vẽ Class Diagram có bản số. | **Bảng 6**, **Hình 2.7** (`SoDo/Hinh_2_7.drawio`) và **Mục 7 (Câu 2)**: Thiết lập quan hệ Aggregation (Tang - Phong), Composition (HĐ - ChiTiet, HĐDV - ChiTietDV) và Association với bản số chính xác. |
| **Câu 3** | Xác định Actor và Use Case tổng quát. Dùng StarUML / Draw.io vẽ Use Case Diagram. | **Mục 1.2.2**, **Hình 2.1** (`SoDo/Hinh_2_1.drawio`) và **Mục 7 (Câu 3)**: Gồm 6 tác nhân và 10 Use Case tổng quát bao quát toàn bộ hoạt động của cao ốc. |
| **Câu 4** | Vẽ Use Case Diagram phân rã chi tiết từ ký HĐ đến thanh toán tiền thuê tháng thứ 7 và dịch vụ. | **Hình 2.2** (`SoDo/Hinh_2_2.drawio`), **Mục 2.1 b)** và **Mục 7 (Câu 4)**: Phân rã luồng chọn phòng, ký HĐ $\ge$ 6 tháng, include trả trước 6 tháng đầu, nộp tiền thuê đầu tháng từ tháng 7, chốt chỉ số và trả tiền dịch vụ cuối tháng. |
| **Câu 5** | Vẽ Activity Diagram cho quy trình của Câu 4 ở trên. | **Hình 3.9** (`SoDo/Hinh_3_9.drawio`), **Mục 3.4** và **Mục 7 (Câu 5)**: Biểu đồ hoạt động 3 phân luồng (Khách hàng, Ban quản lý, Kế toán) với đầy đủ rẽ nhánh và chu kỳ lặp hàng tháng. |
| **Câu 6** | Vẽ Sequence Diagram cho nghiệp vụ “Lập phiếu gia hạn” (kiểm tra không nợ tiền phòng & dịch vụ). | **Hình 3.1** (`SoDo/Hinh_3_1.drawio`), **Mục 3.1 a)** và **Mục 7 (Câu 6)**: Biểu đồ tuần tự kiểm tra thời gian gia hạn $\le$ 1/2 HĐ gốc, kiểm tra nợ tiền thuê mặt bằng, kiểm tra nợ hóa đơn dịch vụ; rẽ nhánh alt thành công / từ chối. |

---

## 4. Hướng dẫn cài đặt CSDL & Kiểm thử

1. **Khởi tạo Cơ sở dữ liệu:**
   - Mở **SQL Server Management Studio (SSMS)**.
   - Mở và thực thi file `Database\01_TaoCSDL.sql` để tạo Database `QuanLyChoThueVanPhong_Bai08`, 15 bảng nghiệp vụ và Trigger `TR_KiemTraDieuKienGiaHan`.
   - Mở và thực thi file `Database\02_DuLieuMau.sql` để nạp dữ liệu minh họa.

2. **Mở và chỉnh sửa sơ đồ:**
   - Truy cập [draw.io](https://app.diagrams.net) hoặc mở phần mềm Draw.io Desktop trên máy.
   - Chọn **Open Existing Diagram** $\rightarrow$ Trỏ tới thư mục `C:\Study\LAB_AT_OOSD\Lab08\SoDo\` và chọn file `.drawio` cần xem hoặc chỉnh sửa.
