using System;
using System.ComponentModel;
using System.Linq;

namespace eShopping.Models
{
    public class SanPham
    {
        public Guid MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal GiaHienHanh { get; set; }
        public bool TinhTrang { get; set; }
    }

    public class GioHangItem
    {
        public Guid MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }

        public decimal ThanhTien
        {
            get { return SoLuong * DonGia; }
        }

        public GioHangItem SaoChep()
        {
            return new GioHangItem
            {
                MaSP = MaSP,
                TenSP = TenSP,
                SoLuong = SoLuong,
                DonGia = DonGia
            };
        }
    }

    public class GioHang
    {
        public BindingList<GioHangItem> ChiTiet { get; private set; }

        public GioHang()
        {
            ChiTiet = new BindingList<GioHangItem>();
        }

        public decimal TongTien
        {
            get { return ChiTiet.Sum(x => x.ThanhTien); }
        }

        public int TongSoLuong
        {
            get { return ChiTiet.Sum(x => x.SoLuong); }
        }
    }

    public class LuaChon
    {
        public string Ma { get; set; }
        public string Ten { get; set; }
    }

    public class BaoGia
    {
        public decimal TienHang { get; set; }
        public decimal PhiGiaoHang { get; set; }
        public decimal LePhiThe { get; set; }

        public decimal TongTien
        {
            get { return TienHang + PhiGiaoHang + LePhiThe; }
        }
    }

    public class YeuCauDatHang
    {
        public Guid MaKH { get; set; }
        public string HoTenNguoiNhan { get; set; }
        public string DiaChiNguoiNhan { get; set; }
        public string DienThoaiNguoiNhan { get; set; }
        public string KhuVuc { get; set; }
        public string LoaiGiao { get; set; }
        public string LoaiThe { get; set; }
    }

    public class KetQuaDatHang
    {
        public Guid MaDon { get; set; }
        public string MaThamChieu { get; set; }
        public decimal TongTien { get; set; }
    }

    public static class QuyTac
    {
        public const int SoLuongToiDa = 10000;

        // Giới hạn của decimal(18,2) trong SQL Server.
        public const decimal TienToiDa = 9999999999999999.99m;

        public static void KiemTraTien(decimal tien)
        {
            if (tien < 0 || tien > TienToiDa ||
                decimal.Round(tien, 2) != tien)
            {
                throw new InvalidOperationException(
                    "Số tiền không hợp lệ hoặc vượt giới hạn lưu trữ.");
            }
        }

        public static void KiemTraSoLuong(int soLuong)
        {
            if (soLuong < 1 || soLuong > SoLuongToiDa)
            {
                throw new InvalidOperationException(
                    "Số lượng phải từ 1 đến " + SoLuongToiDa + ".");
            }
        }
    }
}