using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using QuanLyVanPhong.Data;
using QuanLyVanPhong.Common;
namespace QuanLyVanPhong.Services
{
 public class FeeLine { public int DetailId {get;set;} public int ServiceId {get;set;} public decimal Quantity {get;set;} }
 public class HoaDonService
 {
  public DataTable List(string keyword="",int contract=0)
  { return Db.Query(@"SELECT b.*,h.SoHopDong,c.TenCongTy,SUM(x.SoLuong*x.DonGia) AS TongTien,CASE WHEN b.NgayThanhToan IS NULL THEN N'Chưa thanh toán' ELSE N'Đã thanh toán' END AS TrangThai FROM HoaDon b JOIN HopDong h ON h.MaHopDong=b.MaHopDong JOIN CongTy c ON c.MaCongTy=h.MaCongTy JOIN ChiTietHoaDon x ON x.MaHoaDon=b.MaHoaDon WHERE (@h=0 OR b.MaHopDong=@h) AND (b.SoHoaDon LIKE @q OR h.SoHopDong LIKE @q OR c.TenCongTy LIKE @q) GROUP BY b.MaHoaDon,b.SoHoaDon,b.MaHopDong,b.Loai,b.TuNgay,b.DenNgay,b.NgayLap,b.NgayThanhToan,h.SoHopDong,c.TenCongTy ORDER BY b.MaHoaDon DESC",Db.P("@q","%"+keyword+"%"),Db.P("@h",contract)); }
  public DataTable Details(int id)
  { return Db.Query(@"SELECT x.*,v.KyHieu,ISNULL(s.TenDichVu,N'Tiền thuê') AS NoiDung,x.SoLuong*x.DonGia AS ThanhTien FROM ChiTietHoaDon x JOIN ChiTietHopDong d ON d.MaChiTietHD=x.MaChiTietHD JOIN VanPhong v ON v.MaVanPhong=d.MaVanPhong LEFT JOIN DichVu s ON s.MaDichVu=x.MaDichVu WHERE x.MaHoaDon=@id ORDER BY x.STT",Db.P("@id",id)); }
  public int CreateRent(int contract,string number,DateTime from)
  {
   Session.Require("Kế toán");number=Rules.Text(number,"Số hóa đơn",20);from=from.Date;
   return Db.Transaction((c,tx)=> {
    var h=Rules.Contract(c,tx,contract);DateTime start=(DateTime)h["TuNgay"],to=from.AddMonths(1);
    int months=(from.Year-start.Year)*12+from.Month-start.Month;
    Rules.Check(months>=6 && start.AddMonths(months)==from,"Kỳ từ tháng thứ bảy phải theo ngày bắt đầu hợp đồng; sáu tháng đầu đã có hóa đơn.");
    var ds=Db.Query(c,tx,@"SELECT * FROM ChiTietHopDong WHERE MaHopDong=@h AND DenNgayHienTai>=@e",Db.P("@h",contract),Db.P("@e",to));
    Rules.Check(ds.Rows.Count>0,"Không còn phòng thuê cho kỳ này.");
    foreach(DataRow d in ds.Rows) CheckOverlap(c,tx,(int)d["MaChiTietHD"],null,from,to);
    int id=Insert(c,tx,number,contract,"Tiền thuê",from,to);
    int i=0;foreach(DataRow d in ds.Rows) Db.Execute(c,tx,"INSERT ChiTietHoaDon VALUES(@b,@i,@d,NULL,1,@p)",Db.P("@b",id),Db.P("@i",++i),Db.P("@d",d["MaChiTietHD"]),Db.P("@p",d["DonGiaThang"]));
    return id;
   });
  }
  public int CreateFees(int contract,string number,DateTime from,DateTime to,List<FeeLine> lines)
  {
   Session.Require("Kế toán");number=Rules.Text(number,"Số hóa đơn",20);from=from.Date;to=to.Date;
   Rules.Check(to>from && to<=DateTime.Today,"Khoảng phí phải hợp lệ và đã phát sinh (đến ngày không sau hôm nay).");
   Rules.Check(to<=new DateTime(from.Year,from.Month,1).AddMonths(1),"Mỗi kỳ dịch vụ nằm trong một tháng lịch; có thể chốt sớm để gia hạn.");
   Rules.Check(lines!=null && lines.Count>0 && lines.All(x=>x.Quantity>0),"Thêm ít nhất một dòng dịch vụ, số lượng phải dương.");
   Rules.Check(lines.Select(x=>x.DetailId+":"+x.ServiceId).Distinct().Count()==lines.Count,"Không lặp cùng phòng và dịch vụ trong hóa đơn.");
   return Db.Transaction((c,tx)=> {
    Rules.Contract(c,tx,contract);
    foreach(var x in lines)
    {
     var d=Rules.Detail(c,tx,x.DetailId);
     Rules.Check((int)d["MaHopDong"]==contract && (DateTime)d["TuNgay"]<=from && to<=(DateTime)d["DenNgayHienTai"],"Phòng không thuộc hợp đồng hoặc ngoài kỳ thuê.");
     CheckOverlap(c,tx,x.DetailId,x.ServiceId,from,to);
    }
    int id=Insert(c,tx,number,contract,"Dịch vụ",from,to),i=0;
    foreach(var x in lines)
    {
     var price=Db.Scalar(c,tx,"SELECT DonGia FROM DichVu WHERE MaDichVu=@s",Db.P("@s",x.ServiceId));Rules.Check(price!=null,"Dịch vụ không tồn tại.");
     Db.Execute(c,tx,"INSERT ChiTietHoaDon VALUES(@b,@i,@d,@s,@q,@p)",Db.P("@b",id),Db.P("@i",++i),Db.P("@d",x.DetailId),Db.P("@s",x.ServiceId),Db.P("@q",x.Quantity),Db.P("@p",price));
    }
    return id;
   });
  }
  private static int Insert(System.Data.SqlClient.SqlConnection c,System.Data.SqlClient.SqlTransaction tx,string number,int contract,string type,DateTime from,DateTime to)
  { return Convert.ToInt32(Db.Scalar(c,tx,"INSERT HoaDon(SoHoaDon,MaHopDong,Loai,TuNgay,DenNgay,NgayLap) VALUES(@n,@h,@t,@s,@e,@ng);SELECT SCOPE_IDENTITY();",Db.P("@n",number),Db.P("@h",contract),Db.P("@t",type),Db.P("@s",from),Db.P("@e",to),Db.P("@ng",DateTime.Today))); }
  private static void CheckOverlap(System.Data.SqlClient.SqlConnection c,System.Data.SqlClient.SqlTransaction tx,int detail,int? service,DateTime from,DateTime to)
  {
   int n=Convert.ToInt32(Db.Scalar(c,tx,@"SELECT COUNT(*) FROM HoaDon b JOIN ChiTietHoaDon x ON x.MaHoaDon=b.MaHoaDon WHERE x.MaChiTietHD=@d AND ((@v IS NULL AND x.MaDichVu IS NULL) OR x.MaDichVu=@v) AND b.TuNgay<@e AND @s<b.DenNgay",Db.P("@d",detail),Db.P("@v",service),Db.P("@s",from),Db.P("@e",to)));
   Rules.Check(n==0,"Khoảng tính phí đã có hóa đơn; không được thu trùng.");
  }
  public void Pay(int bill,decimal received,DateTime day)
  {
   Session.Require("Kế toán");day=day.Date;Rules.Check(day<=DateTime.Today,"Ngày thu không sau hôm nay.");
   Db.Transaction((c,tx)=> {
    int h=Convert.ToInt32(Db.Scalar(c,tx,"SELECT MaHopDong FROM HoaDon WHERE MaHoaDon=@b",Db.P("@b",bill)));Rules.Contract(c,tx,h);
    var t=Db.Query(c,tx,"SELECT * FROM HoaDon WITH(UPDLOCK,HOLDLOCK) WHERE MaHoaDon=@b",Db.P("@b",bill));Rules.Check(t.Rows.Count==1,"Chọn hóa đơn.");
    Rules.Check(t.Rows[0]["NgayThanhToan"]==DBNull.Value,"Hóa đơn đã thu đủ; không được thu lại.");Rules.Check(day>=(DateTime)t.Rows[0]["NgayLap"],"Ngày thu không trước ngày lập.");
    decimal total=Convert.ToDecimal(Db.Scalar(c,tx,"SELECT SUM(SoLuong*DonGia) FROM ChiTietHoaDon WHERE MaHoaDon=@b",Db.P("@b",bill)));
    Rules.Check(received==total,"Số tiền xác nhận phải bằng tổng hóa đơn. Bài chưa xử lý thu nhiều lần hoặc tiền thừa.");
    Db.Execute(c,tx,"UPDATE HoaDon SET NgayThanhToan=@ng WHERE MaHoaDon=@b AND NgayThanhToan IS NULL",Db.P("@ng",day),Db.P("@b",bill));
   });
  }
 }
}
