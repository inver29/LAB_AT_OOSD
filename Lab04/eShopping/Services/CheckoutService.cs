using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using eShopping.Data;
using eShopping.Models;

namespace eShopping.Services
{
    public class CheckoutService
    {
        private readonly GioHang _gio;
        private readonly GioHangService _gioService;
        private readonly Repository _repository = new Repository();

        public CheckoutService(GioHang gio)
        {
            if (gio == null)
                throw new ArgumentNullException(nameof(gio));

            _gio = gio;
            _gioService = new GioHangService(gio);
        }

        public DataTable LayKhachHang()
        {
            return _repository.LayKhachHang();
        }

        public DataTable LayKhuVuc()
        {
            return _repository.LayKhuVuc();
        }

        public DataTable LayDonHang()
        {
            return _repository.LayDonHang();
        }

        public DataTable LayChiTietDon(Guid maDon)
        {
            return _repository.LayChiTietDon(maDon);
        }

        private static decimal LayLePhiThe(string loaiThe)
        {
            if (!new[]
            {
                "VISA", "MasterCard", "Discover", "AmericanExpress"
            }.Contains(loaiThe))
            {
                throw new InvalidOperationException("Loại thẻ không hợp lệ.");
            }

            string value =
                ConfigurationManager.AppSettings["LePhiThe." + loaiThe];

            decimal phi;

            if (!decimal.TryParse(
                value,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out phi))
            {
                throw new InvalidOperationException(
                    "Chưa cấu hình lệ phí thẻ trong App.config.");
            }

            QuyTac.KiemTraTien(phi);
            return phi;
        }

        public BaoGia TinhTien(
            string khuVuc,
            string loaiGiao,
            string loaiThe)
        {
            if (_gio.ChiTiet.Count == 0)
                throw new InvalidOperationException("Giỏ hàng đang trống.");

            if (string.IsNullOrWhiteSpace(khuVuc))
                throw new InvalidOperationException("Hãy chọn khu vực.");

            if (!new[] { "Thuong", "Nhanh", "TrongNgay" }
                .Contains(loaiGiao))
            {
                throw new InvalidOperationException("Hãy chọn loại giao.");
            }

            _gioService.CapNhatGiaTuSql();

            var baoGia = new BaoGia
            {
                TienHang = _gio.TongTien,
                PhiGiaoHang = _repository.LayPhiGiaoHang(
                    khuVuc, loaiGiao, _gio.TongTien),
                LePhiThe = LayLePhiThe(loaiThe)
            };

            QuyTac.KiemTraTien(baoGia.TongTien);
            return baoGia;
        }

        public KetQuaDatHang LuuDon(
            YeuCauDatHang yeuCau,
            BaoGia baoGia)
        {
            if (yeuCau == null || baoGia == null)
            {
                throw new InvalidOperationException(
                    "Hãy tính tổng trước khi lưu đơn.");
            }

            if (yeuCau.MaKH == Guid.Empty)
                throw new InvalidOperationException("Hãy chọn khách hàng.");

            yeuCau.HoTenNguoiNhan =
                (yeuCau.HoTenNguoiNhan ?? "").Trim();

            yeuCau.DiaChiNguoiNhan =
                (yeuCau.DiaChiNguoiNhan ?? "").Trim();

            yeuCau.DienThoaiNguoiNhan =
                (yeuCau.DienThoaiNguoiNhan ?? "").Trim();

            KiemTraChuoi(yeuCau.HoTenNguoiNhan, 100, "Họ tên người nhận");
            KiemTraChuoi(yeuCau.DiaChiNguoiNhan, 250, "Địa chỉ người nhận");
            KiemTraChuoi(yeuCau.DienThoaiNguoiNhan, 20, "Điện thoại");
            KiemTraChuoi(yeuCau.KhuVuc, 100, "Khu vực");

            if (!new[] { "Thuong", "Nhanh", "TrongNgay" }
                .Contains(yeuCau.LoaiGiao))
            {
                throw new InvalidOperationException("Loại giao không hợp lệ.");
            }

            decimal phiThe = LayLePhiThe(yeuCau.LoaiThe);

            if (phiThe != baoGia.LePhiThe)
            {
                throw new InvalidOperationException(
                    "Lệ phí thẻ đã thay đổi. Hãy tính tổng lại.");
            }

            QuyTac.KiemTraTien(baoGia.TienHang);
            QuyTac.KiemTraTien(baoGia.PhiGiaoHang);
            QuyTac.KiemTraTien(baoGia.LePhiThe);
            QuyTac.KiemTraTien(baoGia.TongTien);

            var chiTiet = _gio.ChiTiet
                .Select(x => x.SaoChep())
                .ToList();

            // Chưa làm rỗng giỏ vì đơn mới đang chờ thanh toán.
            return _repository.LuuDon(yeuCau, chiTiet, baoGia);
        }

        private static void KiemTraChuoi(
            string value,
            int maxLength,
            string tenTruong)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException(
                    tenTruong + " không được để trống.");

            if (value.Length > maxLength)
                throw new InvalidOperationException(
                    tenTruong + " tối đa " + maxLength + " ký tự.");
        }
    }
}