using System;
using System.Data;
using QuanLyCongTyDuLich.Data;

namespace QuanLyCongTyDuLich.Services
{
    /// <summary>Tour, điểm dừng, phương tiện theo chặng và điểm tham quan của tour.</summary>
    public class TourService
    {
        public DataTable LayTour() { return Db.Query("SELECT MaTour, TenTour, SoNgay, SoDem, DonGiaKhach, MoTa, DangMoBan FROM Tour ORDER BY MaTour"); }
        public DataTable LayTourMoBan() { return Db.Query("SELECT MaTour, MaTour + ' - ' + TenTour AS HienThi, SoNgay, DonGiaKhach FROM Tour WHERE DangMoBan = 1 ORDER BY MaTour"); }
        public DataTable LayDiemDung(string maTour) { return Db.Query("SELECT ThuTu, TenDiemDung, DoiPhuongTien, CoNoiAn, CoKhachSan, HangSaoKhachSan, GhiChu FROM TourDiemDung WHERE MaTour = @m ORDER BY ThuTu", Db.P("@m", maTour)); }
        public DataTable LayChang(string maTour) { return Db.Query("SELECT t.ThuTuChang, p.TenPT, t.MaPT, t.GhiChu FROM TourPhuongTien t JOIN PhuongTien p ON t.MaPT = p.MaPT WHERE t.MaTour = @m ORDER BY t.ThuTuChang", Db.P("@m", maTour)); }
        public DataTable LayDiemTQTour(string maTour) { return Db.Query("SELECT t.ThuTu, d.MaDiemTQ, d.TenDiemTQ, d.DiaDiem FROM TourDiemThamQuan t JOIN DiemThamQuan d ON t.MaDiemTQ = d.MaDiemTQ WHERE t.MaTour = @m ORDER BY t.ThuTu", Db.P("@m", maTour)); }

        public KetQuaXuLy ThemTour(string ma, string ten, int soNgay, int soDem, decimal donGia, string moTa)
        {
            if (string.IsNullOrWhiteSpace(ma) || string.IsNullOrWhiteSpace(ten)) return KetQuaXuLy.Fail("Mã tour và tên tour không được để trống.");
            if (soNgay <= 0 || soDem < 0 || soDem > soNgay || donGia < 0) return KetQuaXuLy.Fail("Số ngày, số đêm hoặc đơn giá không hợp lệ.");
            return Chay("INSERT INTO Tour(MaTour, TenTour, SoNgay, SoDem, DonGiaKhach, MoTa, DangMoBan) VALUES(@m, @t, @n, @d, @g, @mt, 1)", "Đã thêm tour.",
                Db.P("@m", ma), Db.P("@t", ten), Db.P("@n", soNgay), Db.P("@d", soDem), Db.P("@g", donGia), Db.P("@mt", moTa));
        }

        public KetQuaXuLy ThemDiemDung(string maTour, int thuTu, string ten, bool doiPT, bool coAn, bool coKS, int hangSao, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(maTour) || thuTu <= 0 || string.IsNullOrWhiteSpace(ten)) return KetQuaXuLy.Fail("Thông tin điểm dừng chưa đầy đủ.");
            if (coKS && (hangSao < 2 || hangSao > 5)) return KetQuaXuLy.Fail("Khách sạn phải có hạng từ 2 đến 5 sao.");
            return Chay("INSERT INTO TourDiemDung(MaTour, ThuTu, TenDiemDung, DoiPhuongTien, CoNoiAn, CoKhachSan, HangSaoKhachSan, GhiChu) VALUES(@t, @st, @ten, @doi, @an, @ks, @sao, @g)",
                "Đã thêm điểm dừng.", Db.P("@t", maTour), Db.P("@st", thuTu), Db.P("@ten", ten), Db.P("@doi", doiPT), Db.P("@an", coAn), Db.P("@ks", coKS),
                Db.P("@sao", coKS ? (object)hangSao : null), Db.P("@g", ghiChu));
        }

        public KetQuaXuLy ThemChang(string maTour, int thuTuChang, string maPT, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(maTour) || thuTuChang <= 0 || string.IsNullOrWhiteSpace(maPT)) return KetQuaXuLy.Fail("Thông tin chặng / phương tiện chưa đầy đủ.");
            return Chay("INSERT INTO TourPhuongTien(MaTour, ThuTuChang, MaPT, GhiChu) VALUES(@t, @c, @p, @g)", "Đã gắn phương tiện cho chặng.",
                Db.P("@t", maTour), Db.P("@c", thuTuChang), Db.P("@p", maPT), Db.P("@g", ghiChu));
        }

        public KetQuaXuLy ThemDiemTQTour(string maTour, string maDiemTQ, int thuTu)
        {
            if (string.IsNullOrWhiteSpace(maTour) || string.IsNullOrWhiteSpace(maDiemTQ) || thuTu <= 0) return KetQuaXuLy.Fail("Thông tin điểm tham quan của tour chưa đầy đủ.");
            return Chay("INSERT INTO TourDiemThamQuan(MaTour, MaDiemTQ, ThuTu) VALUES(@t, @d, @st)", "Đã gắn điểm tham quan cho tour.",
                Db.P("@t", maTour), Db.P("@d", maDiemTQ), Db.P("@st", thuTu));
        }

        private static KetQuaXuLy Chay(string sql, string thongBao, params System.Data.SqlClient.SqlParameter[] ps)
        {
            try { Db.Execute(sql, ps); return KetQuaXuLy.Ok(thongBao); }
            catch (Exception ex) { return KetQuaXuLy.Fail(ex.Message); }
        }
    }
}
