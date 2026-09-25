using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Services
{
    public class DichVuService
    {
        public DataTable LayPhieuDangO()
        {
            string sql = @"SELECT d.SoPhieuDat, k.HoTen, c.SoPhong 
                           FROM PhieuDatPhong d 
                           JOIN KhachHang k ON d.MaKhach = k.MaKhach 
                           JOIN ChiTietDatPhong c ON d.SoPhieuDat = c.SoPhieuDat 
                           WHERE d.TrangThai = N'Đang ở' 
                           ORDER BY d.SoPhieuDat, c.SoPhong";
            return Db.Query(sql);
        }

        public DataTable LayDichVu() => Db.Query("SELECT * FROM DichVu ORDER BY MaDV");

        public DataTable LayLichSu(string soPhieu)
        {
            string sql = @"SELECT p.SoPhieuSDDV, p.SoPhong, p.NgaySuDung, d.TenDV, c.SoLuong, c.DonGia, c.ThanhTien 
                           FROM PhieuSuDungDV p 
                           JOIN ChiTietPhieuSuDungDV c ON p.SoPhieuSDDV = c.SoPhieuSDDV 
                           JOIN DichVu d ON c.MaDV = d.MaDV 
                           WHERE p.SoPhieuDat = @s 
                           ORDER BY p.NgaySuDung DESC, p.SoPhong, d.TenDV";
            return Db.Query(sql, new SqlParameter("@s", soPhieu));
        }

        public KetQuaXuLy GhiNhan(string soPhieuDat, string soPhong, DateTime ngay, string maNV, string maDV, int soLuong)
        {
            if (string.IsNullOrWhiteSpace(soPhieuDat) || string.IsNullOrWhiteSpace(soPhong) || string.IsNullOrWhiteSpace(maNV) || string.IsNullOrWhiteSpace(maDV) || soLuong <= 0)
                return KetQuaXuLy.Fail("Dữ liệu ghi nhận dịch vụ không hợp lệ.");

            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    // 1. Kiểm tra trạng thái phiếu
                    using (var st = new SqlCommand("SELECT TrangThai FROM PhieuDatPhong WHERE SoPhieuDat = @s", cn, tx))
                    {
                        st.Parameters.AddWithValue("@s", soPhieuDat);
                        if (Convert.ToString(st.ExecuteScalar()) != "Đang ở")
                            return KetQuaXuLy.Fail("Chỉ được ghi nhận dịch vụ phát sinh cho các phiếu đang lưu trú.");
                    }

                    // 2. Lấy đơn giá hiện hành của dịch vụ
                    decimal gia;
                    using (var g = new SqlCommand("SELECT DonGia FROM DichVu WHERE MaDV = @d", cn, tx))
                    {
                        g.Parameters.AddWithValue("@d", maDV);
                        object og = g.ExecuteScalar();
                        if (og == null) return KetQuaXuLy.Fail("Không tìm thấy thông tin dịch vụ trong danh mục.");
                        gia = Convert.ToDecimal(og);
                    }

                    // 3. Tìm phiếu dịch vụ đã có của phòng trong ngày hôm đó
                    string soPhieuDichVu;
                    using (var f = new SqlCommand("SELECT SoPhieuSDDV FROM PhieuSuDungDV WHERE SoPhieuDat = @s AND SoPhong = @p AND NgaySuDung = @n", cn, tx))
                    {
                        f.Parameters.AddWithValue("@s", soPhieuDat);
                        f.Parameters.AddWithValue("@p", soPhong);
                        f.Parameters.AddWithValue("@n", ngay.Date);
                        soPhieuDichVu = Convert.ToString(f.ExecuteScalar());
                    }

                    // Nếu chưa có phiếu trong ngày, tiến hành tạo mới
                    if (string.IsNullOrWhiteSpace(soPhieuDichVu))
                    {
                        soPhieuDichVu = "SD" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        using (var h = new SqlCommand("INSERT INTO PhieuSuDungDV(SoPhieuSDDV, SoPhieuDat, SoPhong, NgaySuDung, MaNV) VALUES(@so, @s, @p, @n, @nv)", cn, tx))
                        {
                            h.Parameters.AddWithValue("@so", soPhieuDichVu);
                            h.Parameters.AddWithValue("@s", soPhieuDat);
                            h.Parameters.AddWithValue("@p", soPhong);
                            h.Parameters.AddWithValue("@n", ngay.Date);
                            h.Parameters.AddWithValue("@nv", maNV);
                            h.ExecuteNonQuery();
                        }
                    }

                    // 4. Kiểm tra dịch vụ này đã tồn tại trong phiếu ngày hôm nay chưa
                    bool isExisting = false;
                    using (var chk = new SqlCommand("SELECT COUNT(*) FROM ChiTietPhieuSuDungDV WHERE SoPhieuSDDV = @so AND MaDV = @d", cn, tx))
                    {
                        chk.Parameters.AddWithValue("@so", soPhieuDichVu);
                        chk.Parameters.AddWithValue("@d", maDV);
                        isExisting = Convert.ToInt32(chk.ExecuteScalar()) > 0;
                    }

                    // Cộng dồn hoặc Thêm mới chi tiết
                    if (isExisting)
                    {
                        using (var u = new SqlCommand("UPDATE ChiTietPhieuSuDungDV SET SoLuong = SoLuong + @sl, DonGia = @g WHERE SoPhieuSDDV = @so AND MaDV = @d", cn, tx))
                        {
                            u.Parameters.AddWithValue("@sl", soLuong);
                            u.Parameters.AddWithValue("@g", gia);
                            u.Parameters.AddWithValue("@so", soPhieuDichVu);
                            u.Parameters.AddWithValue("@d", maDV);
                            u.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (var i = new SqlCommand("INSERT INTO ChiTietPhieuSuDungDV(SoPhieuSDDV, MaDV, SoLuong, DonGia) VALUES(@so, @d, @sl, @g)", cn, tx))
                        {
                            i.Parameters.AddWithValue("@so", soPhieuDichVu);
                            i.Parameters.AddWithValue("@d", maDV);
                            i.Parameters.AddWithValue("@sl", soLuong);
                            i.Parameters.AddWithValue("@g", gia);
                            i.ExecuteNonQuery();
                        }
                    }

                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã ghi nhận sử dụng dịch vụ. Hệ thống tự động cộng dồn số lượng nếu trùng dịch vụ trong ngày.");
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