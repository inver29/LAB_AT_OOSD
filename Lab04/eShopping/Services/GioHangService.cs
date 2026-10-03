using System;
using System.Collections.Generic;
using System.Linq;
using eShopping.Data;
using eShopping.Models;

namespace eShopping.Services
{
    public class GioHangService
    {
        private readonly GioHang _gio;
        private readonly Repository _repository = new Repository();

        public GioHangService(GioHang gio)
        {
            if (gio == null)
                throw new ArgumentNullException(nameof(gio));

            _gio = gio;
        }

        private SanPham LaySanPhamHopLe(Guid maSP)
        {
            var sanPham = _repository.LaySanPham(maSP);

            if (sanPham == null)
                throw new InvalidOperationException("Không tìm thấy sản phẩm.");

            if (!sanPham.TinhTrang)
            {
                throw new InvalidOperationException(
                    "Sản phẩm hiện đã hết hàng.");
            }

            return sanPham;
        }

        public void Them(Guid maSP, int soLuong)
        {
            QuyTac.KiemTraSoLuong(soLuong);

            var sanPham = LaySanPhamHopLe(maSP);
            var item = _gio.ChiTiet.FirstOrDefault(x => x.MaSP == maSP);

            int soLuongMoi =
                soLuong + (item == null ? 0 : item.SoLuong);

            QuyTac.KiemTraSoLuong(soLuongMoi);

            decimal tongMoi =
                _gio.TongTien
                - (item == null ? 0 : item.ThanhTien)
                + soLuongMoi * sanPham.GiaHienHanh;

            QuyTac.KiemTraTien(tongMoi);

            if (item == null)
            {
                _gio.ChiTiet.Add(new GioHangItem
                {
                    MaSP = sanPham.MaSP,
                    TenSP = sanPham.TenSP,
                    DonGia = sanPham.GiaHienHanh,
                    SoLuong = soLuongMoi
                });
            }
            else
            {
                item.TenSP = sanPham.TenSP;
                item.DonGia = sanPham.GiaHienHanh;
                item.SoLuong = soLuongMoi;
            }

            _gio.ChiTiet.ResetBindings();
        }

        public void CapNhat(Guid maSP, int soLuong)
        {
            QuyTac.KiemTraSoLuong(soLuong);

            var item = _gio.ChiTiet.FirstOrDefault(x => x.MaSP == maSP);

            if (item == null)
                throw new InvalidOperationException("Dòng giỏ không tồn tại.");

            var sanPham = LaySanPhamHopLe(maSP);

            decimal tongMoi =
                _gio.TongTien - item.ThanhTien
                + soLuong * sanPham.GiaHienHanh;

            QuyTac.KiemTraTien(tongMoi);

            item.TenSP = sanPham.TenSP;
            item.DonGia = sanPham.GiaHienHanh;
            item.SoLuong = soLuong;

            _gio.ChiTiet.ResetBindings();
        }

        public void Xoa(Guid maSP)
        {
            var item = _gio.ChiTiet.FirstOrDefault(x => x.MaSP == maSP);

            if (item != null)
                _gio.ChiTiet.Remove(item);
        }

        public void LamRong()
        {
            _gio.ChiTiet.Clear();
        }

        public void CapNhatGiaTuSql()
        {
            var sanPhamMoi = new Dictionary<Guid, SanPham>();
            decimal tongMoi = 0;

            // Kiểm tra toàn bộ trước khi thay đổi giỏ.
            foreach (var item in _gio.ChiTiet)
            {
                QuyTac.KiemTraSoLuong(item.SoLuong);

                var sanPham = LaySanPhamHopLe(item.MaSP);
                sanPhamMoi.Add(item.MaSP, sanPham);
                tongMoi += item.SoLuong * sanPham.GiaHienHanh;
            }

            QuyTac.KiemTraTien(tongMoi);

            foreach (var item in _gio.ChiTiet)
            {
                var sanPham = sanPhamMoi[item.MaSP];
                item.TenSP = sanPham.TenSP;
                item.DonGia = sanPham.GiaHienHanh;
            }

            _gio.ChiTiet.ResetBindings();
        }
    }
}