using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyKhachSan.Data;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Services
{
    public class PhongTienNghiService
    {
        public DataTable LayPhong()
        {
            return Db.Query("SELECT p.*, k.TenKhuVuc FROM Phong p JOIN KhuVuc k ON p.MaKhuVuc = k.MaKhuVuc ORDER BY p.SoPhong");
        }

        public DataTable LayTienNghi()
        {
            return Db.Query("SELECT t.*, l.TenLoaiTN FROM TienNghi t JOIN LoaiTienNghi l ON t.MaLoaiTN = l.MaLoaiTN ORDER BY t.MaTienNghi");
        }

        public DataTable LayLapDat()
        {
            string sql = @"SELECT p.*, l.TenLoaiTN 
                           FROM PhieuLapDat p 
                           JOIN TienNghi t ON p.MaTienNghi = t.MaTienNghi 
                           JOIN LoaiTienNghi l ON t.MaLoaiTN = l.MaLoaiTN 
                           ORDER BY p.NgayLap DESC";
            return Db.Query(sql);
        }

        public KetQuaXuLy ThemPhong(string so, string khu, int max, decimal gia)
        {
            if (string.IsNullOrWhiteSpace(so) || string.IsNullOrWhiteSpace(khu) || max <= 0 || gia < 0)
                return KetQuaXuLy.Fail("Thông tin phòng không hợp lệ (Sức chứa và đơn giá phải lớn hơn 0).");

            try
            {
                string sql = "INSERT INTO Phong(SoPhong, MaKhuVuc, SoNguoiToiDa, DonGiaNgay, TrangThai) VALUES(@s, @k, @m, @g, N'Trống')";
                Db.Execute(sql,
                    new SqlParameter("@s", so),
                    new SqlParameter("@k", khu),
                    new SqlParameter("@m", max),
                    new SqlParameter("@g", gia));
                return KetQuaXuLy.Ok("Đã thêm phòng thành công.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy ThemTienNghi(string ma, string loai, int stt, string tt)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(loai) || stt <= 0)
                return KetQuaXuLy.Fail("Thông tin tiện nghi không hợp lệ.");

            try
            {
                Db.Execute("INSERT INTO TienNghi(MaTienNghi, MaLoaiTN, SoThuTu, TinhTrangHienTai) VALUES(@m, @l, @s, @t)",
                    new SqlParameter("@m", ma),
                    new SqlParameter("@l", loai),
                    new SqlParameter("@s", stt),
                    new SqlParameter("@t", tt));
                return KetQuaXuLy.Ok("Đã thêm tiện nghi.");
            }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }

        public KetQuaXuLy LapDat(string soPhieu, string maTN, string soPhong, DateTime ngay, string tinhTrang, string maNV, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(soPhieu) || string.IsNullOrWhiteSpace(maTN) || string.IsNullOrWhiteSpace(soPhong) || string.IsNullOrWhiteSpace(maNV))
                return KetQuaXuLy.Fail("Phiếu lắp đặt chưa đủ thông tin bắt buộc.");

            using (var cn = Db.OpenConnection())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    // 1. Thêm phiếu lắp đặt
                    var cmdInsert = new SqlCommand("INSERT INTO PhieuLapDat(SoPhieuLapDat, MaTienNghi, SoPhong, NgayLap, TinhTrang, MaNV, GhiChu) VALUES(@p, @tn, @ph, @n, @tt, @nv, @g)", cn, tx);
                    cmdInsert.Parameters.AddWithValue("@p", soPhieu);
                    cmdInsert.Parameters.AddWithValue("@tn", maTN);
                    cmdInsert.Parameters.AddWithValue("@ph", soPhong);
                    cmdInsert.Parameters.AddWithValue("@n", ngay.Date);
                    cmdInsert.Parameters.AddWithValue("@tt", tinhTrang);
                    cmdInsert.Parameters.AddWithValue("@nv", maNV);
                    cmdInsert.Parameters.AddWithValue("@g", string.IsNullOrWhiteSpace(ghiChu) ? (object)DBNull.Value : ghiChu);
                    cmdInsert.ExecuteNonQuery();

                    // 2. Cập nhật tình trạng thiết bị hiện tại
                    var cmdUpdate = new SqlCommand("UPDATE TienNghi SET TinhTrangHienTai = @tt WHERE MaTienNghi = @m", cn, tx);
                    cmdUpdate.Parameters.AddWithValue("@tt", tinhTrang);
                    cmdUpdate.Parameters.AddWithValue("@m", maTN);
                    cmdUpdate.ExecuteNonQuery();

                    tx.Commit();
                    return KetQuaXuLy.Ok("Đã lập phiếu lắp đặt thiết bị.");
                }
                catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                {
                    tx.Rollback();
                    return KetQuaXuLy.Fail("Thiết bị này đã được lắp cho một phòng khác trong ngày đã chọn.");
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