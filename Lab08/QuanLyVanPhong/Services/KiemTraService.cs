using System;
using System.Data;
using QuanLyVanPhong.Data;
using QuanLyVanPhong.Common;
namespace QuanLyVanPhong.Services
{
 public class KiemTraService
 {
  public DataTable Inspections(int office=0)
  { return Db.Query(@"SELECT k.*,v.KyHieu FROM KiemTra k JOIN VanPhong v ON v.MaVanPhong=k.MaVanPhong WHERE (@p=0 OR k.MaVanPhong=@p) ORDER BY k.NgayKiemTra DESC,k.MaKiemTra DESC",Db.P("@p",office)); }
  public DataTable Due(int year)
  { return Db.Query(@"SELECT v.MaVanPhong,v.KyHieu,t.SoTang,v.TinhTrang,MAX(k.NgayKiemTra) AS LanCuoi FROM VanPhong v JOIN Tang t ON t.MaTang=v.MaTang LEFT JOIN KiemTra k ON k.MaVanPhong=v.MaVanPhong WHERE NOT EXISTS(SELECT 1 FROM KiemTra k2 WHERE k2.MaVanPhong=v.MaVanPhong AND YEAR(k2.NgayKiemTra)=@y) GROUP BY v.MaVanPhong,v.KyHieu,t.SoTang,v.TinhTrang ORDER BY t.SoTang,v.KyHieu",Db.P("@y",year)); }
  public DataTable Violations()
  { return Db.Query(@"SELECT p.*,v.KyHieu,h.SoHopDong,c.TenCongTy FROM ViPham p JOIN ChiTietHopDong d ON d.MaChiTietHD=p.MaChiTietHD JOIN VanPhong v ON v.MaVanPhong=d.MaVanPhong JOIN HopDong h ON h.MaHopDong=d.MaHopDong JOIN CongTy c ON c.MaCongTy=h.MaCongTy ORDER BY p.MaViPham DESC"); }
  public void Inspect(int office,DateTime day,string result,string note)
  {
   Session.Require("Quản lý tòa nhà");day=day.Date;Rules.Check(day<=DateTime.Today,"Ngày kiểm tra không sau hôm nay.");Rules.Check(result=="Đạt" || result=="Cần sửa","Chọn kết quả kiểm tra.");Rules.Check((note??"").Length<=500,"Ghi chú tối đa 500 ký tự.");
   Db.Transaction((c,tx)=> {
    Rules.Check(Db.Scalar(c,tx,"SELECT MaVanPhong FROM VanPhong WITH(UPDLOCK,HOLDLOCK) WHERE MaVanPhong=@p",Db.P("@p",office))!=null,"Chọn văn phòng.");
    var latest=Db.Scalar(c,tx,"SELECT MAX(NgayKiemTra) FROM KiemTra WHERE MaVanPhong=@p",Db.P("@p",office));
    Rules.Check(latest==DBNull.Value || day>=Convert.ToDateTime(latest),"Ngày kiểm tra mới không trước lần gần nhất.");
    Db.Execute(c,tx,"INSERT KiemTra(MaVanPhong,NgayKiemTra,KetQua,GhiChu) VALUES(@p,@ng,@k,@t);UPDATE VanPhong SET TinhTrang=@k WHERE MaVanPhong=@p",Db.P("@p",office),Db.P("@ng",day),Db.P("@k",result),Db.P("@t",note));
   });
  }
  public void Violate(int detail,int? inspection,DateTime day,string content,decimal fine)
  {
   Session.Require("Quản lý tòa nhà");day=day.Date;content=Rules.Text(content,"Nội dung vi phạm",500);Rules.Check(day<=DateTime.Today && fine>0,"Ngày lập không sau hôm nay; tiền phạt phải dương.");
   Db.Transaction((c,tx)=> {
    var d=Rules.Detail(c,tx,detail);Rules.Contract(c,tx,(int)d["MaHopDong"]);
    Rules.Check((DateTime)d["TuNgay"]<=day && day<(DateTime)d["DenNgayHienTai"],"Phòng không thuộc bên thuê tại ngày vi phạm.");
    if(inspection.HasValue) Rules.Check(Convert.ToInt32(Db.Scalar(c,tx,"SELECT COUNT(*) FROM KiemTra WHERE MaKiemTra=@k AND MaVanPhong=@p AND NgayKiemTra<=@ng",Db.P("@k",inspection.Value),Db.P("@p",d["MaVanPhong"]),Db.P("@ng",day)))==1,"Lần kiểm tra không đúng phòng hoặc sau ngày lập biên bản.");
    Db.Execute(c,tx,"INSERT ViPham(MaChiTietHD,MaKiemTra,NgayLap,NoiDung,TienPhat) VALUES(@d,@k,@ng,@t,@p)",Db.P("@d",detail),Db.P("@k",inspection),Db.P("@ng",day),Db.P("@t",content),Db.P("@p",fine));
   });
  }
 }
}
