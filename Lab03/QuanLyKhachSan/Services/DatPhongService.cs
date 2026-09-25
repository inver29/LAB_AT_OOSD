using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Services
{
    public class DatPhongService
    {
        public DataTable LayKhach() => Db.Query("SELECT * FROM KhachHang ORDER BY HoTen");

        public DataTable LayPhong() => Db.Query("SELECT p.*, k.TenKhuVuc FROM Phong p JOIN KhuVuc k ON p.MaKhuVuc = k.MaKhuVuc ORDER BY p.SoPhong");

        public DataTable LayPhieuDat() => Db.Query("SELECT d.*, k.HoTen FROM PhieuDatPhong d JOIN KhachHang k ON d.MaKhach = k.MaKhach ORDER BY d.NgayLap DESC");

        public DataTable LayChiTiet(string soPhieu)
        {
            return Db.Query("SELECT c.*, p.SoNguoiToiDa, p.DonGiaNgay FROM ChiTietDatPhong c JOIN Phong p ON c.SoPhong = p.SoPhong WHERE c.SoPhieuDat = @s",
                new SqlParameter("@s", soPhieu));
        }

        public DataTable LayNguoiLuuTru(string soPhieu)
        {
            return Db.Query("SELECT * FROM NguoiLuuTru WHERE SoPhieuDat = @s ORDER BY SoPhong, MaNguoiLT",
                new SqlParameter("@s", soPhieu));
        }

        public KetQuaXuLy ThemKhach(string ma, string ten, string cmnd, string qt, string sdt)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(cmnd) || string.IsNullOrWhiteSpace(qt))
                return KetQuaXuLy.Fail("Thông tin khách chưa đầy đủ.");

            try
            {
                Db.Execute("INSERT INTO KhachHang(MaKhach, HoTen, SoCMND, QuocTich, SoDienThoai) VALUES(@m, @t, @c, @q, @s)",
                    new SqlParameter("@m", ma),
                    new SqlParameter("@t", ten),
                    new SqlParameter("@c", cmnd),
                    new SqlParameter("@q", qt),
                    new SqlParameter("@s", string.IsNullOrWhiteSpace(sdt) ? (object)DBNull.Value : sdt));
                return KetQuaXuLy.Ok("Đã lưu thông tin khách hàng.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        private bool PhongTrungLich(SqlConnection cn, SqlTransaction tx, string phong, DateTime nhan, DateTime tra)
        {
            string sql = @"SELECT COUNT(*) 
                           FROM ChiTietDatPhong c 
                           JOIN PhieuDatPhong d ON c.SoPhieuDat = d.SoPhieuDat 
                           WHERE c.SoPhong = @p 
                             AND d.TrangThai IN (N'Đã đặt', N'Đang ở') 
                             AND @nhan <= d.NgayTraDuKien 
                             AND @tra >= d.NgayNhan";
            using (var cmd = new SqlCommand(sql, cn, tx))
            {
                cmd.Parameters.AddWithValue("@p", phong);
                cmd.Parameters.AddWithValue("@nhan", nhan.Date);
                cmd.Parameters.AddWithValue("@tra", tra.Date);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        public KetQuaXuLy TaoDatPhong(string so, string maKhach, string maNV, DateTime ngayLap, DateTime nhan, DateTime tra, decimal coc, string kenh, List<PhongDatItem> ds)
        {
            if (string.IsNullOrWhiteSpace(so) || string.IsNullOrWhiteSpace(maKhach) || string.IsNullOrWhiteSpace(maNV) || ds == null || ds.Count == 0)
                return KetQuaXuLy.Fail("Phiếu đặt phòng thiếu thông tin hoặc chưa chọn phòng.");

            if (tra.Date < nhan.Date)
                return KetQuaXuLy.Fail("Ngày trả dự kiến không được xảy ra trước ngày nhận.");

            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    // Kiểm tra sức chứa và lịch chồng chéo cho từng phòng
                    foreach (var x in ds)
                    {
                        using (var q = new SqlCommand("SELECT SoNguoiToiDa FROM Phong WHERE SoPhong = @p", cn, tx))
                        {
                            q.Parameters.AddWithValue("@p", x.SoPhong);
                            object capacity = q.ExecuteScalar();
                            if (capacity == null) return KetQuaXuLy.Fail($"Không tìm thấy phòng {x.SoPhong}");

                            if (x.SoNguoi <= 0 || x.SoNguoi > Convert.ToInt32(capacity))
                                return KetQuaXuLy.Fail($"Số người của phòng {x.SoPhong} vượt quá sức chứa quy định.");
                        }

                        if (PhongTrungLich(cn, tx, x.SoPhong, nhan, tra))
                            return KetQuaXuLy.Fail($"Phòng {x.SoPhong} đã có khách đặt trong khoảng thời gian này.");
                    }

                    // Lưu phiếu đặt phòng
                    string sqlHeader = "INSERT INTO PhieuDatPhong(SoPhieuDat, MaKhach, MaNVLeTan, NgayLap, NgayNhan, NgayTraDuKien, TienCoc, KenhDat, TrangThai) VALUES(@s, @k, @nv, @lap, @nhan, @tra, @c, @kenh, N'Đã đặt')";
                    using (var h = new SqlCommand(sqlHeader, cn, tx))
                    {
                        h.Parameters.AddWithValue("@s", so);
                        h.Parameters.AddWithValue("@k", maKhach);
                        h.Parameters.AddWithValue("@nv", maNV);
                        h.Parameters.AddWithValue("@lap", ngayLap);
                        h.Parameters.AddWithValue("@nhan", nhan.Date);
                        h.Parameters.AddWithValue("@tra", tra.Date);
                        h.Parameters.AddWithValue("@c", coc);
                        h.Parameters.AddWithValue("@kenh", kenh);
                        h.ExecuteNonQuery();
                    }

                    // Lưu chi tiết từng phòng và cập nhật trạng thái
                    foreach (var x in ds)
                    {
                        using (var c = new SqlCommand("INSERT INTO ChiTietDatPhong(SoPhieuDat, SoPhong, SoNguoi) VALUES(@s, @p, @n)", cn, tx))
                        {
                            c.Parameters.AddWithValue("@s", so);
                            c.Parameters.AddWithValue("@p", x.SoPhong);
                            c.Parameters.AddWithValue("@n", x.SoNguoi);
                            c.ExecuteNonQuery();
                        }

                        using (var u = new SqlCommand("UPDATE Phong SET TrangThai = N'Đã đặt' WHERE SoPhong = @p AND TrangThai = N'Trống'", cn, tx))
                        {
                            u.Parameters.AddWithValue("@p", x.SoPhong);
                            u.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã thiết lập phiếu đặt phòng thành công.");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }

        public KetQuaXuLy ThemNguoiLuuTru(string so, string phong, string ten, string cmnd, string qt)
        {
            if (string.IsNullOrWhiteSpace(so) || string.IsNullOrWhiteSpace(phong) || string.IsNullOrWhiteSpace(ten) || string.IsNullOrWhiteSpace(cmnd))
                return KetQuaXuLy.Fail("Thông tin người lưu trú chưa đầy đủ.");

            try
            {
                int maxAllowed = Convert.ToInt32(Db.Scalar("SELECT SoNguoi FROM ChiTietDatPhong WHERE SoPhieuDat = @s AND SoPhong = @p",
                    new SqlParameter("@s", so), new SqlParameter("@p", phong)));

                int currentCount = Convert.ToInt32(Db.Scalar("SELECT COUNT(*) FROM NguoiLuuTru WHERE SoPhieuDat = @s AND SoPhong = @p",
                    new SqlParameter("@s", so), new SqlParameter("@p", phong)));

                if (currentCount >= maxAllowed)
                    return KetQuaXuLy.Fail("Phòng này đã khai báo đủ số người lưu trú được phép.");

                Db.Execute("INSERT INTO NguoiLuuTru(SoPhieuDat, SoPhong, HoTen, SoCMND, QuocTich) VALUES(@s, @p, @t, @c, @q)",
                    new SqlParameter("@s", so),
                    new SqlParameter("@p", phong),
                    new SqlParameter("@t", ten),
                    new SqlParameter("@c", cmnd),
                    new SqlParameter("@q", qt));

                return KetQuaXuLy.Ok("Đã ghi nhận người lưu trú.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy NhanPhong(string soPhieu, DateTime thucTe)
        {
            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    var cmdUpdatePhieu = new SqlCommand("UPDATE PhieuDatPhong SET TrangThai = N'Đang ở', NgayNhanThucTe = @n WHERE SoPhieuDat = @s AND TrangThai = N'Đã đặt'", cn, tx);
                    cmdUpdatePhieu.Parameters.AddWithValue("@n", thucTe);
                    cmdUpdatePhieu.Parameters.AddWithValue("@s", soPhieu);
                    if (cmdUpdatePhieu.ExecuteNonQuery() == 0)
                    {
                        tx.Rollback();
                        return KetQuaXuLy.Fail("Trạng thái phiếu không hợp lệ để nhận phòng (chỉ áp dụng phiếu 'Đã đặt').");
                    }

                    var cmdUpdatePhong = new SqlCommand("UPDATE Phong SET TrangThai = N'Đang ở' WHERE SoPhong IN(SELECT SoPhong FROM ChiTietDatPhong WHERE SoPhieuDat = @s)", cn, tx);
                    cmdUpdatePhong.Parameters.AddWithValue("@s", soPhieu);
                    cmdUpdatePhong.ExecuteNonQuery();

                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã xác nhận nhận phòng thành công.");
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    return KetQuaXuLy.Fail(ex.Message);
                }
            }
        }

        public KetQuaXuLy DanhDauNoShow(string soPhieu)
        {
            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    var cmdPhieu = new SqlCommand("UPDATE PhieuDatPhong SET TrangThai = N'No-show' WHERE SoPhieuDat = @s AND TrangThai = N'Đã đặt'", cn, tx);
                    cmdPhieu.Parameters.AddWithValue("@s", soPhieu);
                    if (cmdPhieu.ExecuteNonQuery() == 0)
                    {
                        tx.Rollback();
                        return KetQuaXuLy.Fail("Chỉ có thể đánh dấu No-show cho phiếu đang ở trạng thái 'Đã đặt'.");
                    }

                    var cmdPhong = new SqlCommand("UPDATE Phong SET TrangThai = N'Trống' WHERE SoPhong IN (SELECT SoPhong FROM ChiTietDatPhong WHERE SoPhieuDat = @s)", cn, tx);
                    cmdPhong.Parameters.AddWithValue("@s", soPhieu);
                    cmdPhong.ExecuteNonQuery();

                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã đánh dấu khách vắng mặt (No-show). Các phòng liên quan đã được giải phóng.");
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