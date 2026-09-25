using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Services
{
    public class TraPhongService
    {
        public DataTable LayPhieuDangO()
        {
            return Db.Query(@"SELECT d.SoPhieuDat, k.HoTen, d.NgayNhanThucTe, d.NgayTraDuKien 
                              FROM PhieuDatPhong d 
                              JOIN KhachHang k ON d.MaKhach = k.MaKhach 
                              WHERE d.TrangThai = N'Đang ở' 
                              ORDER BY d.SoPhieuDat");
        }

        public DataTable LayPhongTheoPhieu(string soPhieu)
        {
            return Db.Query("SELECT c.SoPhong, p.DonGiaNgay FROM ChiTietDatPhong c JOIN Phong p ON c.SoPhong = p.SoPhong WHERE c.SoPhieuDat = @s",
                new SqlParameter("@s", soPhieu));
        }

        public DataTable LayTienNghiPhong(string phong)
        {
            return Db.Query(@"SELECT p.MaTienNghi, l.TenLoaiTN, t.TinhTrangHienTai 
                              FROM PhieuLapDat p 
                              JOIN TienNghi t ON p.MaTienNghi = t.MaTienNghi 
                              JOIN LoaiTienNghi l ON t.MaLoaiTN = l.MaLoaiTN 
                              WHERE p.SoPhong = @p 
                              ORDER BY p.NgayLap DESC",
                              new SqlParameter("@p", phong));
        }

        public DataTable LayQuyDinh()
        {
            return Db.Query("SELECT q.*, l.TenLoaiTN FROM QuyDinhDenBu q JOIN LoaiTienNghi l ON q.MaLoaiTN = l.MaLoaiTN ORDER BY l.TenLoaiTN, q.MucDoThietHai");
        }

        public DataTable LayHoaDon()
        {
            return Db.Query(@"SELECT h.*, k.HoTen 
                              FROM HoaDon h 
                              JOIN PhieuDatPhong d ON h.SoPhieuDat = d.SoPhieuDat 
                              JOIN KhachHang k ON d.MaKhach = k.MaKhach 
                              ORDER BY h.NgayLap DESC");
        }

        public KetQuaXuLy LapPhieuDenBu(string soDB, string soDat, string phong, DateTime ngay, string maNV, List<DenBuItem> ds)
        {
            if (string.IsNullOrWhiteSpace(soDB) || string.IsNullOrWhiteSpace(soDat) || string.IsNullOrWhiteSpace(phong) || ds == null || ds.Count == 0)
                return KetQuaXuLy.Fail("Dữ liệu phiếu đền bù chưa hợp lệ hoặc chưa thêm tài sản hỏng.");

            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    decimal tong = 0;
                    foreach (var item in ds)
                    {
                        if (item.SoTien < 0) return KetQuaXuLy.Fail("Mức đền bù không được là số âm.");
                        tong += item.SoTien;
                    }

                    using (var h = new SqlCommand("INSERT INTO PhieuDenBu(SoPhieuDenBu, SoPhieuDat, SoPhong, NgayLap, MaNV, TongTien) VALUES(@so, @d, @p, @n, @nv, @t)", cn, tx))
                    {
                        h.Parameters.AddWithValue("@so", soDB);
                        h.Parameters.AddWithValue("@d", soDat);
                        h.Parameters.AddWithValue("@p", phong);
                        h.Parameters.AddWithValue("@n", ngay);
                        h.Parameters.AddWithValue("@nv", maNV);
                        h.Parameters.AddWithValue("@t", tong);
                        h.ExecuteNonQuery();
                    }

                    foreach (var item in ds)
                    {
                        using (var c = new SqlCommand("INSERT INTO ChiTietPhieuDenBu(SoPhieuDenBu, MaTienNghi, MucDoThietHai, SoTien) VALUES(@so, @tn, @m, @t)", cn, tx))
                        {
                            c.Parameters.AddWithValue("@so", soDB);
                            c.Parameters.AddWithValue("@tn", item.MaTienNghi);
                            c.Parameters.AddWithValue("@m", item.MucDoThietHai);
                            c.Parameters.AddWithValue("@t", item.SoTien);
                            c.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    return KetQuaXuLy.Ok($"Đã lập phiếu đền bù thành công. Tổng tiền: {tong:N0}");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }

        public KetQuaXuLy LapHoaDon(string soHD, string soDat, DateTime ngay, string maNV, int soNgay)
        {
            if (string.IsNullOrWhiteSpace(soHD) || string.IsNullOrWhiteSpace(soDat) || soNgay <= 0)
                return KetQuaXuLy.Fail("Thông tin lập hóa đơn không hợp lệ (số ngày phải lớn hơn 0).");

            try
            {
                // Tổng hợp tiền phòng
                decimal tienPhong = Convert.ToDecimal(Db.Scalar(
                    "SELECT ISNULL(SUM(p.DonGiaNgay),0) FROM ChiTietDatPhong c JOIN Phong p ON c.SoPhong = p.SoPhong WHERE c.SoPhieuDat = @s",
                    new SqlParameter("@s", soDat))) * soNgay;

                // Tổng hợp tiền dịch vụ
                decimal tienDichVu = Convert.ToDecimal(Db.Scalar(
                    "SELECT ISNULL(SUM(c.ThanhTien),0) FROM PhieuSuDungDV h JOIN ChiTietPhieuSuDungDV c ON h.SoPhieuSDDV = c.SoPhieuSDDV WHERE h.SoPhieuDat = @s",
                    new SqlParameter("@s", soDat)));

                string sqlInsert = @"INSERT INTO HoaDon(SoHoaDon, SoPhieuDat, NgayLap, MaNV, SoNgayTinhTien, TienPhong, TienDichVu, TrangThai) 
                                     VALUES(@h, @s, @n, @nv, @ng, @p, @d, N'Chưa thanh toán')";

                Db.Execute(sqlInsert,
                    new SqlParameter("@h", soHD),
                    new SqlParameter("@s", soDat),
                    new SqlParameter("@n", ngay),
                    new SqlParameter("@nv", maNV),
                    new SqlParameter("@ng", soNgay),
                    new SqlParameter("@p", tienPhong),
                    new SqlParameter("@d", tienDichVu));

                return KetQuaXuLy.Ok("Đã tổng hợp số liệu và lập hóa đơn thành công.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy ThanhToan(string maTT, string soHD, DateTime ngay, string hinhThuc, decimal tien)
        {
            if (string.IsNullOrWhiteSpace(maTT) || string.IsNullOrWhiteSpace(soHD) || string.IsNullOrWhiteSpace(hinhThuc) || tien <= 0)
                return KetQuaXuLy.Fail("Thông tin giao dịch không hợp lệ.");

            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    // Lấy tổng tiền yêu cầu của Hóa Đơn
                    object objTong = new SqlCommand("SELECT TongTien FROM HoaDon WHERE SoHoaDon = @h", cn, tx) { Parameters = { new SqlParameter("@h", soHD) } }.ExecuteScalar();
                    if (objTong == null) return KetQuaXuLy.Fail("Không tìm thấy hóa đơn cần thanh toán.");
                    decimal tongTien = Convert.ToDecimal(objTong);

                    // Lấy số tiền đã trả trước đó
                    decimal daTra = Convert.ToDecimal(new SqlCommand("SELECT ISNULL(SUM(SoTien),0) FROM ThanhToan WHERE SoHoaDon = @h", cn, tx) { Parameters = { new SqlParameter("@h", soHD) } }.ExecuteScalar());

                    if (daTra + tien > tongTien)
                        return KetQuaXuLy.Fail($"Khách hàng thanh toán dư. Số tiền cần thu thêm tối đa là: {(tongTien - daTra):N0}");

                    // Ghi nhận giao dịch
                    using (var i = new SqlCommand("INSERT INTO ThanhToan(MaThanhToan, SoHoaDon, NgayThanhToan, HinhThuc, SoTien) VALUES(@m, @h, @n, @ht, @t)", cn, tx))
                    {
                        i.Parameters.AddWithValue("@m", maTT);
                        i.Parameters.AddWithValue("@h", soHD);
                        i.Parameters.AddWithValue("@n", ngay);
                        i.Parameters.AddWithValue("@ht", hinhThuc);
                        i.Parameters.AddWithValue("@t", tien);
                        i.ExecuteNonQuery();
                    }

                    // Đổi trạng thái HĐ nếu thanh toán đủ
                    if (daTra + tien == tongTien)
                    {
                        using (var u = new SqlCommand("UPDATE HoaDon SET TrangThai = N'Đã thanh toán' WHERE SoHoaDon = @h", cn, tx))
                        {
                            u.Parameters.AddWithValue("@h", soHD);
                            u.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    return KetQuaXuLy.Ok($"Ghi nhận thanh toán {hinhThuc} thành công.");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }

        public KetQuaXuLy TraPhong(string soDat, DateTime ngayTra)
        {
            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    // Validation an toàn bắt buộc: Kiểm tra hóa đơn
                    string tt = "";
                    using (var q = new SqlCommand("SELECT TrangThai FROM HoaDon WHERE SoPhieuDat = @s", cn, tx))
                    {
                        q.Parameters.AddWithValue("@s", soDat);
                        object result = q.ExecuteScalar();
                        if (result == null) return KetQuaXuLy.Fail("Không thể trả phòng: Chưa xuất hóa đơn tổng hợp.");
                        tt = Convert.ToString(result);
                    }

                    if (tt != "Đã thanh toán")
                        return KetQuaXuLy.Fail("Không thể trả phòng: Hóa đơn chưa được thanh toán đầy đủ.");

                    // Cập nhật trạng thái phiếu đặt -> Đã trả
                    using (var u1 = new SqlCommand("UPDATE PhieuDatPhong SET TrangThai = N'Đã trả', NgayTraThucTe = @n WHERE SoPhieuDat = @s", cn, tx))
                    {
                        u1.Parameters.AddWithValue("@n", ngayTra);
                        u1.Parameters.AddWithValue("@s", soDat);
                        u1.ExecuteNonQuery();
                    }

                    // Giải phóng toàn bộ phòng đang giữ trong phiếu -> Trống
                    using (var u2 = new SqlCommand("UPDATE Phong SET TrangThai = N'Trống' WHERE SoPhong IN (SELECT SoPhong FROM ChiTietDatPhong WHERE SoPhieuDat = @s)", cn, tx))
                    {
                        u2.Parameters.AddWithValue("@s", soDat);
                        u2.ExecuteNonQuery();
                    }

                    tx.Commit();
                    return KetQuaXuLy.Ok("Thủ tục trả phòng hoàn tất. Các phòng đã được giải phóng.");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }
    }
}