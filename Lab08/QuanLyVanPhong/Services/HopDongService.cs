using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using QuanLyVanPhong.Data;
using QuanLyVanPhong.Common;
namespace QuanLyVanPhong.Services
{
 public class RentalLine { public int OfficeId {get;set;} public decimal Price {get;set;} }
 public class HopDongService
 {
  public DataTable List(string keyword="")
  { return Db.Query(@"SELECT h.*,c.TenCongTy,CONCAT(h.SoHopDong,N' — ',c.TenCongTy) AS HienThi FROM HopDong h JOIN CongTy c ON c.MaCongTy=h.MaCongTy WHERE h.SoHopDong LIKE @q OR c.TenCongTy LIKE @q ORDER BY h.MaHopDong DESC",Db.P("@q","%"+keyword+"%")); }
  public DataTable Details(int id)
  { return Db.Query(@"SELECT d.*,v.KyHieu,h.TuNgay,h.DenNgay,c.TenCongTy,CONCAT(v.KyHieu,N' — ',c.TenCongTy,N' (',h.SoHopDong,N')') AS HienThi FROM ChiTietHopDong d JOIN HopDong h ON h.MaHopDong=d.MaHopDong JOIN CongTy c ON c.MaCongTy=h.MaCongTy JOIN VanPhong v ON v.MaVanPhong=d.MaVanPhong WHERE (@id=0 OR d.MaHopDong=@id) ORDER BY v.KyHieu",Db.P("@id",id)); }
  public int Create(string number,int company,DateTime signed,DateTime start,DateTime end,string content,List<RentalLine> lines)
  {
   Session.Require("Quản lý tòa nhà");number=Rules.Text(number,"Số hợp đồng",20);content=Rules.Text(content,"Nội dung",1000);
   signed=signed.Date;start=start.Date;end=end.Date;Rules.Check(signed<=start && signed<=DateTime.Today,"Ngày ký không sau ngày bắt đầu hoặc ngày hiện tại.");
   Rules.Check(Rules.Months(start,end)>=6,"Thời hạn thuê tối thiểu sáu tháng.");
   Rules.Check(company>0 && lines!=null && lines.Count>0,"Chọn công ty và ít nhất một văn phòng.");
   Rules.Check(lines.Select(x=>x.OfficeId).Distinct().Count()==lines.Count && lines.All(x=>x.Price>0),"Phòng không được trùng; đơn giá phải dương.");
   Rules.Check((number+"-T01").Length<=20,"Số hợp đồng tối đa 16 ký tự để tạo số hóa đơn đầu kỳ.");
   return Db.Transaction((c,tx)=> {
    foreach(var line in lines.OrderBy(x=>x.OfficeId))
    {
     var p=Db.Query(c,tx,"SELECT TinhTrang FROM VanPhong WITH(UPDLOCK,HOLDLOCK) WHERE MaVanPhong=@p",Db.P("@p",line.OfficeId));
     Rules.Check(p.Rows.Count==1 && (string)p.Rows[0][0]=="Đạt","Phòng không tồn tại hoặc cần sửa.");
     var n=Convert.ToInt32(Db.Scalar(c,tx,@"SELECT COUNT(*) FROM ChiTietHopDong d JOIN HopDong h ON h.MaHopDong=d.MaHopDong WHERE d.MaVanPhong=@p AND h.TuNgay<@e AND @s<d.DenNgayHienTai",Db.P("@p",line.OfficeId),Db.P("@s",start),Db.P("@e",end)));
     Rules.Check(n==0,"Phòng đã có lịch thuê trong khoảng đã chọn.");
    }
    int id=Convert.ToInt32(Db.Scalar(c,tx,@"INSERT HopDong(SoHopDong,MaCongTy,NgayKy,TuNgay,DenNgay,NoiDung) VALUES(@n,@c,@k,@s,@e,@t); SELECT SCOPE_IDENTITY();",Db.P("@n",number),Db.P("@c",company),Db.P("@k",signed),Db.P("@s",start),Db.P("@e",end),Db.P("@t",content)));
    int bill=Convert.ToInt32(Db.Scalar(c,tx,@"INSERT HoaDon(SoHoaDon,MaHopDong,Loai,TuNgay,DenNgay,NgayLap) VALUES(@n,@h,N'Tiền thuê',@s,@e,@k);SELECT SCOPE_IDENTITY();",Db.P("@n",number+"-T01"),Db.P("@h",id),Db.P("@s",start),Db.P("@e",start.AddMonths(6)),Db.P("@k",signed)));
    int stt=0;
    foreach(var line in lines)
    {
     int d=Convert.ToInt32(Db.Scalar(c,tx,@"INSERT ChiTietHopDong(MaHopDong,MaVanPhong,DonGiaThang,DenNgayHienTai) VALUES(@h,@p,@g,@e);SELECT SCOPE_IDENTITY();",Db.P("@h",id),Db.P("@p",line.OfficeId),Db.P("@g",line.Price),Db.P("@e",end)));
     Db.Execute(c,tx,"INSERT ChiTietHoaDon VALUES(@b,@i,@d,NULL,6,@g)",Db.P("@b",bill),Db.P("@i",++stt),Db.P("@d",d),Db.P("@g",line.Price));
    }
    return id;
   });
  }
 }
}
