using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using QuanLyHoSoBenhAn.Data;
using QuanLyHoSoBenhAn.Services;

class IntegrationTests
{
    private static int passed;
    private static void Check(bool condition, string label)
    { if (!condition) throw new Exception("FAIL: " + label); Console.WriteLine("PASS: " + label); passed++; }
    private static void Reject(Action action, string label)
    { bool rejected = false; try { action(); } catch (InvalidOperationException) { rejected = true; } Check(rejected, label); }
    private static void Execute(string sql)
    { using (var c = Db.Open()) Db.Execute(c, null, sql); }
    private static int Count(string sql)
    { using (var c = Db.Open()) return Convert.ToInt32(Db.Scalar(c, null, sql)); }
    private static DongThuoc Line(int id)
    { return new DongThuoc { MaThuoc = id, SoLuong = 2, LieuDung = "Minh họa", CachDung = "Minh họa" }; }
    static int Main()
    {
        try
        {
            Check(Count("SELECT COUNT(*) FROM sys.tables") == 12, "Schema giữ đúng 12 bảng");
            Check(Count("SELECT COUNT(*) FROM BenhNhan") == 3, "Chạy seed hai lần vẫn có 3 bệnh nhân");
            var hs = new HoSoService(); var kham = new KhamBenhService(); var toa = new ToaThuocService(); var thu = new ThanhToanService();
            var bn = new BenhNhanInput { HoTen = "BN kiểm thử", NgaySinh = new DateTime(2000, 1, 1), GioiTinh = "Nam", DiaChi = "Địa chỉ thử", DienThoai = "0909990000", Anh3x4 = "Assets/demo.png", PhiThuongNien = 200000 };
            int hoSo = hs.DangKy(bn);
            Check(hs.DanhSach("0909990000").Rows.Count == 1, "Đăng ký và tra cứu hồ sơ");
            Reject(() => hs.DangKy(bn), "Chặn đăng ký trùng");
            Reject(() => hs.GiaHan(hoSo, 200000), "Chặn gia hạn khi còn hạn");
            Execute("UPDATE HoSoBenhAn SET NgayCap='2020-01-01',NgayDongPhi='2020-01-01',HanPhi='2021-01-01' WHERE MaHSBA=" + hoSo);
            hs.GiaHan(hoSo, 250000);
            Check(Convert.ToDateTime(hs.DanhSach("0909990000").Rows[0]["HanPhi"]).Date == DateTime.Today.AddYears(1), "Gia hạn đúng một năm");
            int bs = Count("SELECT MIN(MaNV) FROM NhanVien WHERE VaiTro='BacSi'");
            int kh = Count("SELECT MIN(MaKhoa) FROM KhoaDieuTri");
            int nv = Count("SELECT MIN(MaNV) FROM NhanVien WHERE VaiTro='ThuNgan'");
            Reject(() => kham.TaoPhieu(hoSo, nv, kh, 100000), "Không cho thu ngân làm bác sĩ");
            int phieu = kham.TaoPhieu(hoSo, bs, kh, 100000);
            Reject(() => thu.ThanhToan(phieu, 100000), "Không thu tiền phiếu chưa hoàn tất");
            Reject(() => kham.LuuKham(phieu, new KhamInput { TrieuChung = "Thử", HoanTat = true }), "Hoàn tất phải có chẩn đoán");
            int xn = Count("SELECT MIN(MaLoaiXN) FROM LoaiXetNghiem");
            Execute("INSERT ChiTietXetNghiem(MaPhieuKham,MaLoaiXN,DonGia) VALUES(" + phieu + "," + xn + ",50000)");
            var clinical = new KhamInput { TrieuChung = "Thông tin minh họa", ChanDoan = "Chẩn đoán minh họa", HoanTat = true, ThanNhiet = 36.5m, NhipTim = 75 };
            Reject(() => kham.LuuKham(phieu, clinical), "Chặn hoàn tất nếu xét nghiệm chưa có kết quả");
            Execute("UPDATE ChiTietXetNghiem SET KetQua=N'Minh họa',NgayKetQua=GETDATE() WHERE MaPhieuKham=" + phieu);
            kham.LuuKham(phieu, clinical);
            Check(kham.DanhSach(phieu.ToString()).Rows[0]["TrangThai"].ToString() == "Hoàn tất khám", "Lưu và hoàn tất khám");
            int drug1 = Count("SELECT MIN(MaThuoc) FROM Thuoc"), drug2 = Count("SELECT MAX(MaThuoc) FROM Thuoc");
            int oldHeaders = Count("SELECT COUNT(*) FROM ToaThuoc"), oldLines = Count("SELECT COUNT(*) FROM ChiTietToaThuoc");
            Reject(() => toa.LapToa(phieu, "", new List<DongThuoc> { Line(drug1), Line(int.MaxValue) }), "Thuốc thứ hai lỗi khiến giao dịch bị hủy");
            Check(Count("SELECT COUNT(*) FROM ToaThuoc") == oldHeaders && Count("SELECT COUNT(*) FROM ChiTietToaThuoc") == oldLines, "Rollback cả đầu toa và dòng thuốc đầu tiên");
            toa.LapToa(phieu, "Lời dặn thử", new List<DongThuoc> { Line(drug1), Line(drug2) });
            Check(toa.ChiTiet(phieu).Rows.Count == 2, "Lưu toa nhiều dòng");
            Reject(() => toa.LapToa(phieu, "", new List<DongThuoc> { Line(drug1) }), "Chặn toa thứ hai");
            decimal snapshot = Convert.ToDecimal(toa.ChiTiet(phieu).Rows[0]["DonGia"]);
            Execute("UPDATE Thuoc SET DonGia=DonGia+999 WHERE MaThuoc=" + drug1);
            Check(Convert.ToDecimal(toa.ChiTiet(phieu).Rows[0]["DonGia"]) == snapshot, "Giá lịch sử không đổi khi danh mục đổi giá");
            decimal total = thu.ChiPhi(phieu).AsEnumerable().Sum(r => r.Field<decimal>("ThanhTien"));
            Reject(() => thu.ThanhToan(phieu, total - 1), "Chặn thu nếu tổng tiền đã xem không khớp");
            Check(thu.ThanhToan(phieu, total) == total, "Thanh toán đúng tổng khám + xét nghiệm + thuốc");
            Reject(() => thu.ThanhToan(phieu, total), "Chặn thanh toán hai lần");
            Reject(() => kham.LuuKham(phieu, clinical), "Chặn sửa phiếu đã thanh toán");
            Console.WriteLine(passed + " integration checks passed. Live application database was not used.");
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
    }
}
