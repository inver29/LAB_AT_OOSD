using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyVanPhong.Data;
namespace QuanLyVanPhong.Services
{
 internal static class Rules
 {
  internal static void Check(bool ok,string message) { if(!ok) throw new InvalidOperationException(message); }
  internal static string Text(string value,string label,int max)
  {
   value=(value??"").Trim(); Check(value.Length>0,label+" không được để trống.");
   Check(value.Length<=max,label+" tối đa "+max+" ký tự."); return value;
  }
  internal static DataRow Contract(SqlConnection c,SqlTransaction tx,int id)
  {
   var t=Db.Query(c,tx,"SELECT * FROM HopDong WITH(UPDLOCK,HOLDLOCK) WHERE MaHopDong=@id",Db.P("@id",id));
   Check(t.Rows.Count==1,"Không tìm thấy hợp đồng."); return t.Rows[0];
  }
  internal static DataRow Detail(SqlConnection c,SqlTransaction tx,int id)
  {
   var t=Db.Query(c,tx,@"SELECT d.*,h.TuNgay,h.DenNgay FROM ChiTietHopDong d JOIN HopDong h ON h.MaHopDong=d.MaHopDong WHERE d.MaChiTietHD=@id",Db.P("@id",id));
   Check(t.Rows.Count==1,"Không tìm thấy phòng thuê."); return t.Rows[0];
  }
  internal static void Active(SqlConnection c,SqlTransaction tx,int detail,DateTime day)
  {
   var d=Detail(c,tx,detail);
   Contract(c,tx,(int)d["MaHopDong"]);
   Check((DateTime)d["TuNgay"]<=day && day<(DateTime)d["DenNgayHienTai"],"Phòng không trong thời hạn thuê vào ngày đã chọn.");
   var paid=Convert.ToInt32(Db.Scalar(c,tx,@"SELECT COUNT(*) FROM HoaDon b JOIN ChiTietHoaDon x ON x.MaHoaDon=b.MaHoaDon
WHERE b.Loai=N'Tiền thuê' AND x.MaChiTietHD=@d AND b.TuNgay<=@ng AND @ng<b.DenNgay AND b.NgayThanhToan IS NOT NULL AND b.NgayThanhToan<=@ng",Db.P("@d",detail),Db.P("@ng",day)));
   Check(paid>0,"Chưa thu đủ tiền thuê cho ngày làm việc/cấp thẻ. Hãy thanh toán trước.");
  }
  internal static int Months(DateTime start,DateTime end)
  { int n=(end.Year-start.Year)*12+end.Month-start.Month; Check(n>0 && start.AddMonths(n)==end,"Thời hạn phải là số tháng nguyên tính từ ngày bắt đầu."); return n; }
 }
}
