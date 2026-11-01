using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using QuanLyVanPhong.Common;
using QuanLyVanPhong.Data;
namespace QuanLyVanPhong.Services
{
 public class GiaHanService
 {
  public DataTable History(int contract)
  { return Db.Query(@"SELECT g.SoPhieu,g.NgayLap,g.NgayDoiChieuPhi,g.SoThang,v.KyHieu,x.TuNgay AS HanCu,x.DenNgayMoi FROM PhieuGiaHan g JOIN ChiTietGiaHan x ON x.MaGiaHan=g.MaGiaHan JOIN ChiTietHopDong d ON d.MaChiTietHD=x.MaChiTietHD JOIN VanPhong v ON v.MaVanPhong=d.MaVanPhong WHERE g.MaHopDong=@h ORDER BY g.MaGiaHan DESC",Db.P("@h",contract)); }
  public DataTable Debts(int contract) { return new HoaDonService().List("",contract); }
  public int Extend(int contract,string number,int months,DateTime date,bool feesReconciled,IList<int> details)
  {
   Session.Require("Quản lý tòa nhà");number=Rules.Text(number,"Số phiếu",20);date=date.Date;
   Rules.Check(date==DateTime.Today,"Ngày lập và ngày đối chiếu phí phải là hôm nay.");
   Rules.Check(feesReconciled,"Cần xác nhận Kế toán đã chốt mọi phí phát sinh và thu đủ đến ngày đối chiếu.");
   Rules.Check(details!=null && details.Count>0 && details.Distinct().Count()==details.Count,"Chọn các phòng cần gia hạn, không trùng.");
   return Db.Transaction((c,tx)=> {
    // Khóa phòng theo mã tăng dần, giống thao tác lập hợp đồng.
    var all=Db.Query(c,tx,"SELECT MaVanPhong FROM ChiTietHopDong WHERE MaHopDong=@h ORDER BY MaVanPhong",Db.P("@h",contract));
    foreach(DataRow x in all.Rows) Db.Scalar(c,tx,"SELECT MaVanPhong FROM VanPhong WITH(UPDLOCK,HOLDLOCK) WHERE MaVanPhong=@p",Db.P("@p",x[0]));
    var h=Rules.Contract(c,tx,contract);int original=Rules.Months((DateTime)h["TuNgay"],(DateTime)h["DenNgay"]);
    Rules.Check(months>0 && months<=original/2,"Mỗi lần gia hạn tối đa "+(original/2)+" tháng (một nửa thời hạn gốc).");
    Rules.Check(Convert.ToInt32(Db.Scalar(c,tx,"SELECT COUNT(*) FROM HoaDon WHERE MaHopDong=@h AND NgayThanhToan IS NULL",Db.P("@h",contract)))==0,"Hợp đồng còn hóa đơn chưa thanh toán.");
    var selected=new List<DataRow>();
    foreach(int id in details.OrderBy(x=>x))
    {
     var d=Rules.Detail(c,tx,id);Rules.Check((int)d["MaHopDong"]==contract,"Phòng không thuộc hợp đồng.");
     DateTime old=(DateTime)d["DenNgayHienTai"],end=old.AddMonths(months);
     Rules.Check((DateTime)h["TuNgay"]<=date && date<old,"Chỉ gia hạn phòng đang trong hạn thuê.");
     int overlap=Convert.ToInt32(Db.Scalar(c,tx,@"SELECT COUNT(*) FROM ChiTietHopDong d JOIN HopDong h ON h.MaHopDong=d.MaHopDong WHERE d.MaVanPhong=@p AND d.MaChiTietHD<>@id AND h.TuNgay<@e AND @s<d.DenNgayHienTai",Db.P("@p",d["MaVanPhong"]),Db.P("@id",id),Db.P("@s",old),Db.P("@e",end)));
     Rules.Check(overlap==0,"Hạn gia hạn chồng lịch thuê tiếp theo.");
     // Kiểm tra cả các kỳ tiền thuê đã phát sinh nhưng chưa lập hóa đơn.
     var paid=Db.Query(c,tx,@"SELECT DISTINCT b.TuNgay,b.DenNgay FROM HoaDon b JOIN ChiTietHoaDon x ON x.MaHoaDon=b.MaHoaDon WHERE x.MaChiTietHD=@d AND b.Loai=N'Tiền thuê' AND b.NgayThanhToan IS NOT NULL ORDER BY b.TuNgay",Db.P("@d",id));
     DateTime covered=(DateTime)h["TuNgay"];
     foreach(DataRow b in paid.Rows) { if((DateTime)b["TuNgay"]>covered) break; if((DateTime)b["DenNgay"]>covered)covered=(DateTime)b["DenNgay"]; }
     Rules.Check(covered>date,"Chưa lập/thu đủ kỳ tiền thuê hiện tại. Cần lập bổ sung trước khi gia hạn.");
     selected.Add(d);
    }
    int g=Convert.ToInt32(Db.Scalar(c,tx,@"INSERT PhieuGiaHan(SoPhieu,MaHopDong,NgayLap,NgayDoiChieuPhi,SoThang) VALUES(@n,@h,@ng,@ng,@m); SELECT SCOPE_IDENTITY();",Db.P("@n",number),Db.P("@h",contract),Db.P("@ng",date),Db.P("@m",months)));
    foreach(var d in selected)
    {
     DateTime old=(DateTime)d["DenNgayHienTai"],end=old.AddMonths(months);
     Db.Execute(c,tx,"INSERT ChiTietGiaHan VALUES(@g,@d,@old,@new);UPDATE ChiTietHopDong SET DenNgayHienTai=@new WHERE MaChiTietHD=@d",Db.P("@g",g),Db.P("@d",d["MaChiTietHD"]),Db.P("@old",old),Db.P("@new",end));
    }
    return g;
   });
  }
 }
}
