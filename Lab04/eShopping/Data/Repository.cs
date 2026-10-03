using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using eShopping.Models;

namespace eShopping.Data
{
    public class Repository
    {
        public DataTable LayThongKe()
        {
            return Db.Query(@"
SELECT
    (SELECT COUNT(*) FROM dbo.SanPham) AS SoSanPham,
    (SELECT COUNT(*) FROM dbo.KhachHang) AS SoKhachHang,
    (SELECT COUNT(*) FROM dbo.DonDatHang) AS SoDonHang;");
        }

        public DataTable LayNhomSanPham()
        {
            return Db.Query(@"
SELECT MaNhom, TenNhom
FROM dbo.NhomSanPham
ORDER BY TenNhom;");
        }

        public DataTable LayDanhSachSanPham(
            string tuKhoa,
            Guid? maNhom)
        {
            return Db.Query(@"
SELECT
    p.MaSP,
    p.TenSP,
    n.TenNhom,
    p.NhaSanXuat,
    p.GiaHienHanh,
    p.TinhTrang
FROM dbo.SanPham p
JOIN dbo.NhomSanPham n ON n.MaNhom = p.MaNhom
WHERE
    (@MaNhom IS NULL OR p.MaNhom = @MaNhom)
    AND (
        @TuKhoa = N''
        OR CHARINDEX(@TuKhoa, p.TenSP) > 0
        OR CHARINDEX(@TuKhoa, p.NhaSanXuat) > 0
    )
ORDER BY p.TenSP;",
                Db.P("@MaNhom", SqlDbType.UniqueIdentifier,
                    maNhom.HasValue ? (object)maNhom.Value : null),
                Db.P("@TuKhoa", SqlDbType.NVarChar,
                    (tuKhoa ?? "").Trim(), 200));
        }

        public SanPham LaySanPham(Guid maSP)
        {
            using (var connection = Db.OpenConnection())
            {
                return DocSanPham(connection, null, maSP);
            }
        }

        private static SanPham DocSanPham(
            SqlConnection connection,
            SqlTransaction transaction,
            Guid maSP)
        {
            using (var command = new SqlCommand(@"
SELECT MaSP, TenSP, GiaHienHanh, TinhTrang
FROM dbo.SanPham WITH (HOLDLOCK)
WHERE MaSP = @MaSP;", connection, transaction))
            {
                command.Parameters.Add(
                    Db.P("@MaSP", SqlDbType.UniqueIdentifier, maSP));

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return new SanPham
                    {
                        MaSP = reader.GetGuid(0),
                        TenSP = reader.GetString(1),
                        GiaHienHanh = reader.GetDecimal(2),
                        TinhTrang = reader.GetBoolean(3)
                    };
                }
            }
        }

        public DataTable LayKhachHang()
        {
            return Db.Query(@"
SELECT MaKH, HoTen, DiaChi, DienThoai
FROM dbo.KhachHang
ORDER BY HoTen;");
        }

        public DataTable LayKhuVuc()
        {
            return Db.Query(@"
SELECT DISTINCT KhuVuc
FROM dbo.PhiGiaoHang
ORDER BY KhuVuc;");
        }

        public decimal LayPhiGiaoHang(
            string khuVuc,
            string loaiGiao,
            decimal tienHang)
        {
            using (var connection = Db.OpenConnection())
            {
                return DocPhiGiaoHang(
                    connection, null, khuVuc, loaiGiao, tienHang);
            }
        }

        private static decimal DocPhiGiaoHang(
            SqlConnection connection,
            SqlTransaction transaction,
            string khuVuc,
            string loaiGiao,
            decimal tienHang)
        {
            object value = Scalar(connection, transaction, @"
SELECT PhiCoBan
FROM dbo.PhiGiaoHang WITH (HOLDLOCK)
WHERE KhuVuc = @KhuVuc AND LoaiGiao = @LoaiGiao;",
                Db.P("@KhuVuc", SqlDbType.NVarChar, khuVuc, 100),
                Db.P("@LoaiGiao", SqlDbType.VarChar, loaiGiao, 10));

            if (value == null || value == DBNull.Value)
            {
                throw new InvalidOperationException(
                    "Chưa cấu hình phí cho khu vực và loại giao này.");
            }

            decimal phi = Convert.ToDecimal(value);

            if (loaiGiao == "Nhanh" && tienHang >= 1000000m)
                return 0;

            if (loaiGiao == "TrongNgay" && tienHang >= 5000000m)
                return 0;

            return phi;
        }

        public DataTable LayDonHang()
        {
            return Db.Query(@"
SELECT
    d.MaDon,
    k.HoTen AS KhachHang,
    d.ThoiDiem,
    d.TongTien,
    d.TrangThai
FROM dbo.DonDatHang d
JOIN dbo.KhachHang k ON k.MaKH = d.MaKH
ORDER BY d.ThoiDiem DESC, d.MaDon;");
        }

        public DataTable LayChiTietDon(Guid maDon)
        {
            return Db.Query(@"
SELECT
    TenSPLucDat,
    SoLuong,
    DonGiaLucDat,
    ThanhTien
FROM dbo.ChiTietDonDatHang
WHERE MaDon = @MaDon
ORDER BY TenSPLucDat;",
                Db.P("@MaDon", SqlDbType.UniqueIdentifier, maDon));
        }

        public KetQuaDatHang LuuDon(
            YeuCauDatHang yeuCau,
            List<GioHangItem> chiTiet,
            BaoGia baoGia)
        {
            if (chiTiet == null || chiTiet.Count == 0)
                throw new InvalidOperationException("Giỏ hàng đang trống.");

            Guid maDon = Guid.NewGuid();
            string maThamChieu = "ESHOP-" + Guid.NewGuid().ToString("N");

            using (var connection = Db.OpenConnection())
            using (var transaction =
                connection.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    int soKhach = Convert.ToInt32(
                        Scalar(connection, transaction, @"
SELECT COUNT(*)
FROM dbo.KhachHang
WHERE MaKH = @MaKH;",
                            Db.P("@MaKH", SqlDbType.UniqueIdentifier,
                                yeuCau.MaKH)));

                    if (soKhach == 0)
                    {
                        throw new InvalidOperationException(
                            "Khách hàng không còn tồn tại trong SQL.");
                    }

                    decimal tienHang = 0;

                    // Thứ tự đọc ổn định giữa các giao dịch.
                    foreach (var item in chiTiet.OrderBy(x => x.MaSP))
                    {
                        QuyTac.KiemTraSoLuong(item.SoLuong);

                        var sanPham =
                            DocSanPham(connection, transaction, item.MaSP);

                        if (sanPham == null || !sanPham.TinhTrang)
                        {
                            throw new InvalidOperationException(
                                "Sản phẩm không còn bán: " + item.TenSP);
                        }

                        if (sanPham.GiaHienHanh != item.DonGia)
                        {
                            throw new InvalidOperationException(
                                "Giá sản phẩm đã thay đổi. " +
                                "Hãy bấm Tính tổng lại trước khi lưu đơn.");
                        }

                        // Tên sản phẩm được chụp từ SQL tại lúc đặt.
                        item.TenSP = sanPham.TenSP;
                        tienHang += item.ThanhTien;
                    }

                    QuyTac.KiemTraTien(tienHang);

                    decimal phiGiao = DocPhiGiaoHang(
                        connection,
                        transaction,
                        yeuCau.KhuVuc,
                        yeuCau.LoaiGiao,
                        tienHang);

                    decimal tongTien =
                        tienHang + phiGiao + baoGia.LePhiThe;

                    QuyTac.KiemTraTien(tongTien);

                    if (tienHang != baoGia.TienHang ||
                        phiGiao != baoGia.PhiGiaoHang ||
                        tongTien != baoGia.TongTien)
                    {
                        throw new InvalidOperationException(
                            "Tổng tiền đã thay đổi. Hãy bấm Tính tổng lại.");
                    }

                    Execute(connection, transaction, @"
INSERT INTO dbo.DonDatHang
(
    MaDon, MaKH,
    HoTenNguoiNhan, DiaChiNguoiNhan, DienThoaiNguoiNhan,
    KhuVuc, LoaiGiao,
    TienHang, PhiGiaoHang, LePhiThe, TongTien, TrangThai
)
VALUES
(
    @MaDon, @MaKH,
    @HoTen, @DiaChi, @DienThoai,
    @KhuVuc, @LoaiGiao,
    @TienHang, @PhiGiao, @LePhiThe, @TongTien, 'ChoThanhToan'
);",
                        Db.P("@MaDon", SqlDbType.UniqueIdentifier, maDon),
                        Db.P("@MaKH", SqlDbType.UniqueIdentifier,
                            yeuCau.MaKH),
                        Db.P("@HoTen", SqlDbType.NVarChar,
                            yeuCau.HoTenNguoiNhan, 100),
                        Db.P("@DiaChi", SqlDbType.NVarChar,
                            yeuCau.DiaChiNguoiNhan, 250),
                        Db.P("@DienThoai", SqlDbType.VarChar,
                            yeuCau.DienThoaiNguoiNhan, 20),
                        Db.P("@KhuVuc", SqlDbType.NVarChar,
                            yeuCau.KhuVuc, 100),
                        Db.P("@LoaiGiao", SqlDbType.VarChar,
                            yeuCau.LoaiGiao, 10),
                        Db.P("@TienHang", SqlDbType.Decimal, tienHang),
                        Db.P("@PhiGiao", SqlDbType.Decimal, phiGiao),
                        Db.P("@LePhiThe", SqlDbType.Decimal,
                            baoGia.LePhiThe),
                        Db.P("@TongTien", SqlDbType.Decimal, tongTien));

                    foreach (var item in chiTiet)
                    {
                        Execute(connection, transaction, @"
INSERT INTO dbo.ChiTietDonDatHang
    (MaDon, MaSP, TenSPLucDat, SoLuong, DonGiaLucDat)
VALUES
    (@MaDon, @MaSP, @TenSP, @SoLuong, @DonGia);",
                            Db.P("@MaDon", SqlDbType.UniqueIdentifier,
                                maDon),
                            Db.P("@MaSP", SqlDbType.UniqueIdentifier,
                                item.MaSP),
                            Db.P("@TenSP", SqlDbType.NVarChar,
                                item.TenSP, 200),
                            Db.P("@SoLuong", SqlDbType.Int,
                                item.SoLuong),
                            Db.P("@DonGia", SqlDbType.Decimal,
                                item.DonGia));
                    }

                    Execute(connection, transaction, @"
INSERT INTO dbo.ThanhToan
    (MaDon, MaThamChieu, LoaiThe, SoTien, TrangThai)
VALUES
    (@MaDon, @MaThamChieu, @LoaiThe, @SoTien, 'ChoXuLy');",
                        Db.P("@MaDon", SqlDbType.UniqueIdentifier,
                            maDon),
                        Db.P("@MaThamChieu", SqlDbType.VarChar,
                            maThamChieu, 100),
                        Db.P("@LoaiThe", SqlDbType.VarChar,
                            yeuCau.LoaiThe, 20),
                        Db.P("@SoTien", SqlDbType.Decimal,
                            tongTien));

                    transaction.Commit();

                    return new KetQuaDatHang
                    {
                        MaDon = maDon,
                        MaThamChieu = maThamChieu,
                        TongTien = tongTien
                    };
                }
                catch
                {
                    try { transaction.Rollback(); }
                    catch { /* Giữ lại lỗi ban đầu. */ }

                    throw;
                }
            }
        }

        private static object Scalar(
            SqlConnection connection,
            SqlTransaction transaction,
            string sql,
            params SqlParameter[] parameters)
        {
            using (var command =
                new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.AddRange(parameters);
                return command.ExecuteScalar();
            }
        }

        private static void Execute(
            SqlConnection connection,
            SqlTransaction transaction,
            string sql,
            params SqlParameter[] parameters)
        {
            using (var command =
                new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }
    }
}