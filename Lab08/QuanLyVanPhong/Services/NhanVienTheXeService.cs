using System;
using System.Data;
using System.IO;
using QuanLyVanPhong.Data;
using QuanLyVanPhong.Common;
namespace QuanLyVanPhong.Services
{
 public class NhanVienTheXeService
 {
  public DataTable Employees(string keyword="")
  { return Db.Query(@"SELECT n.*,v.KyHieu,c.TenCongTy,CONCAT(n.HoTen,N' — ',v.KyHieu) AS HienThi FROM NhanVien n JOIN ChiTietHopDong d ON d.MaChiTietHD=n.MaChiTietHD JOIN HopDong h ON h.MaHopDong=d.MaHopDong JOIN CongTy c ON c.MaCongTy=h.MaCongTy JOIN VanPhong v ON v.MaVanPhong=d.MaVanPhong WHERE n.HoTen LIKE @q OR c.TenCongTy LIKE @q OR v.KyHieu LIKE @q ORDER BY n.MaNhanVien DESC",Db.P("@q","%"+keyword+"%")); }
  public DataTable Cards(string keyword="")
  { return Db.Query(@"SELECT t.*,n.HoTen,v.KyHieu,c.TenCongTy,CASE WHEN t.ConHieuLuc=0 THEN N'Đã thu hồi' WHEN n.DangLamViec=0 OR h.TuNgay>@ng OR d.DenNgayHienTai<=@ng OR NOT EXISTS (SELECT 1 FROM HoaDon b JOIN ChiTietHoaDon x ON x.MaHoaDon=b.MaHoaDon WHERE x.MaChiTietHD=d.MaChiTietHD AND b.Loai=N'Tiền thuê' AND b.NgayThanhToan<=@ng AND b.TuNgay<=@ng AND @ng<b.DenNgay) THEN N'Không đủ điều kiện' ELSE N'Còn hiệu lực' END AS TrangThai FROM TheGiuXe t JOIN NhanVien n ON n.MaNhanVien=t.MaNhanVien JOIN ChiTietHopDong d ON d.MaChiTietHD=n.MaChiTietHD JOIN HopDong h ON h.MaHopDong=d.MaHopDong JOIN CongTy c ON c.MaCongTy=h.MaCongTy JOIN VanPhong v ON v.MaVanPhong=d.MaVanPhong WHERE t.BienSo LIKE @q OR n.HoTen LIKE @q OR CONVERT(varchar(12),t.MaThe)=@raw ORDER BY t.MaThe DESC",Db.P("@q","%"+keyword+"%"),Db.P("@raw",keyword),Db.P("@ng",DateTime.Today)); }
  public void Save(int id,int detail,string name,string position,string photo)
  {
   Session.Require("Quản lý tòa nhà");name=Rules.Text(name,"Họ tên",100);position=Rules.Text(position,"Chức vụ",100);photo=Rules.Text(photo,"Ảnh",255);Rules.Check(File.Exists(photo),"Chọn tệp ảnh nhân viên đang tồn tại.");
   Db.Transaction((c,tx)=> {
    var d=Rules.Detail(c,tx,detail);Rules.Contract(c,tx,(int)d["MaHopDong"]);Rules.Active(c,tx,detail,DateTime.Today);
    if(id==0) Db.Execute(c,tx,"INSERT NhanVien(MaChiTietHD,HoTen,ChucVu,Anh) VALUES(@d,@n,@p,@a)",Db.P("@d",detail),Db.P("@n",name),Db.P("@p",position),Db.P("@a",photo));
    else {
     var old=Db.Query(c,tx,"SELECT * FROM NhanVien WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@id",Db.P("@id",id));
     Rules.Check(old.Rows.Count==1 && (bool)old.Rows[0]["DangLamViec"],"Nhân viên không còn làm việc; không sửa lịch sử.");
     Rules.Check((int)old.Rows[0]["MaChiTietHD"]==detail,"Phạm vi bài chưa xử lý chuyển phòng làm việc.");
     Db.Execute(c,tx,"UPDATE NhanVien SET HoTen=@n,ChucVu=@p,Anh=@a WHERE MaNhanVien=@id",Db.P("@id",id),Db.P("@n",name),Db.P("@p",position),Db.P("@a",photo));
    }
   });
  }
  public void Stop(int id)
  {
   Session.Require("Quản lý tòa nhà");Db.Transaction((c,tx)=> {
    Rules.Check(Db.Execute(c,tx,"UPDATE NhanVien SET DangLamViec=0 WHERE MaNhanVien=@id AND DangLamViec=1;",Db.P("@id",id))==1,"Chọn nhân viên còn làm việc.");
    Db.Execute(c,tx,"UPDATE TheGiuXe SET ConHieuLuc=0 WHERE MaNhanVien=@id AND ConHieuLuc=1",Db.P("@id",id));
   });
  }
  public void Issue(int employee,string type,string plate)
  {
   Session.Require("Bảo vệ");type=Rules.Text(type,"Loại xe",30);plate=Rules.Text(plate,"Biển số",20).ToUpperInvariant().Replace(" ","");
   Rules.Check(System.Text.RegularExpressions.Regex.IsMatch(plate,@"^[A-Z0-9.\-]+$"),"Biển số chỉ gồm chữ, số, dấu chấm hoặc gạch ngang.");
   Db.Transaction((c,tx)=> {
    var n=Db.Query(c,tx,"SELECT * FROM NhanVien WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@id",Db.P("@id",employee));
    Rules.Check(n.Rows.Count==1 && (bool)n.Rows[0]["DangLamViec"],"Chọn nhân viên còn làm việc.");Rules.Active(c,tx,(int)n.Rows[0]["MaChiTietHD"],DateTime.Today);
    Db.Execute(c,tx,"INSERT TheGiuXe(MaNhanVien,LoaiXe,BienSo,NgayCap) VALUES(@id,@t,@p,@ng)",Db.P("@id",employee),Db.P("@t",type),Db.P("@p",plate),Db.P("@ng",DateTime.Today));
   });
  }
  public void Revoke(int id)
  {
   Session.Require("Bảo vệ");Db.Transaction((c,tx)=> {Rules.Check(Db.Execute(c,tx,"UPDATE TheGiuXe SET ConHieuLuc=0 WHERE MaThe=@id AND ConHieuLuc=1",Db.P("@id",id))==1,"Chọn thẻ chưa thu hồi.");});
  }
 }
}
