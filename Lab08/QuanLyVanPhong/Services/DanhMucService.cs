using System;
using System.Data;
using QuanLyVanPhong.Common;
using QuanLyVanPhong.Data;
namespace QuanLyVanPhong.Services
{
 public class DanhMucService
 {
  public DataTable Floors() { return Db.Query("SELECT MaTang,SoTang FROM Tang ORDER BY SoTang"); }
  public DataTable Companies() { return Db.Query("SELECT * FROM CongTy ORDER BY TenCongTy"); }
  public DataTable Services() { return Db.Query("SELECT * FROM DichVu ORDER BY TenDichVu"); }
  public DataTable Offices(DateTime from,DateTime to,decimal minArea=0,int floor=0,bool available=false)
  {
   Rules.Check(to>from,"Đến ngày phải sau từ ngày.");
   return Db.Query(@"SELECT v.*,t.SoTang,CASE WHEN v.TinhTrang<>N'Đạt' THEN N'Cần sửa' WHEN EXISTS
(SELECT 1 FROM ChiTietHopDong d JOIN HopDong h ON h.MaHopDong=d.MaHopDong WHERE d.MaVanPhong=v.MaVanPhong AND h.TuNgay<@to AND @from<d.DenNgayHienTai)
THEN N'Đã có lịch' ELSE N'Còn trống' END AS LichThue FROM VanPhong v JOIN Tang t ON t.MaTang=v.MaTang
WHERE v.DienTich>=@area AND (@floor=0 OR v.MaTang=@floor) AND (@available=0 OR (v.TinhTrang=N'Đạt' AND NOT EXISTS
(SELECT 1 FROM ChiTietHopDong d JOIN HopDong h ON h.MaHopDong=d.MaHopDong WHERE d.MaVanPhong=v.MaVanPhong AND h.TuNgay<@to AND @from<d.DenNgayHienTai))) ORDER BY t.SoTang,v.KyHieu",
    Db.P("@from",from.Date),Db.P("@to",to.Date),Db.P("@area",minArea),Db.P("@floor",floor),Db.P("@available",available));
  }
  public void SaveFloor(int id,int number)
  { Session.Require("Quản lý tòa nhà"); Rules.Check(number>=0,"Số tầng không được âm."); Save(id,"Tang","MaTang","SoTang=@n","SoTang","@n",Db.P("@n",number)); }
  public void SaveCompany(int id,string name,string representative,string phone,string address)
  {
   Session.Require("Quản lý tòa nhà"); name=Rules.Text(name,"Tên công ty",150); representative=Rules.Text(representative,"Đại diện",100);phone=Rules.Text(phone,"Điện thoại",20);Rules.Check((address??"").Length<=200,"Địa chỉ tối đa 200 ký tự.");
   Save(id,"CongTy","MaCongTy","TenCongTy=@n,DaiDien=@r,DienThoai=@p,DiaChi=@a","TenCongTy,DaiDien,DienThoai,DiaChi","@n,@r,@p,@a",Db.P("@n",name),Db.P("@r",representative),Db.P("@p",phone),Db.P("@a",address));
  }
  public void SaveService(int id,string name,string unit,decimal price)
  {
   Session.Require("Quản lý tòa nhà"); name=Rules.Text(name,"Tên dịch vụ",100);unit=Rules.Text(unit,"Đơn vị tính",20);Rules.Check(price>=0,"Đơn giá không âm.");
   Save(id,"DichVu","MaDichVu","TenDichVu=@n,DonViTinh=@u,DonGia=@p","TenDichVu,DonViTinh,DonGia","@n,@u,@p",Db.P("@n",name),Db.P("@u",unit),Db.P("@p",price));
  }
  public void SaveOffice(int id,int floor,string code,string direction,decimal area,decimal price)
  {
   Session.Require("Quản lý tòa nhà");code=Rules.Text(code,"Ký hiệu",20);direction=Rules.Text(direction,"Hướng",30);Rules.Check(floor>0 && area>0 && price>0,"Chọn tầng; diện tích và đơn giá phải dương.");
   // Tình trạng chỉ thay đổi qua kiểm tra để giữ lịch sử.
   Save(id,"VanPhong","MaVanPhong","MaTang=@f,KyHieu=@n,Huong=@h,DienTich=@a,DonGiaThang=@p","MaTang,KyHieu,Huong,DienTich,DonGiaThang,TinhTrang","@f,@n,@h,@a,@p,N'Đạt'",Db.P("@f",floor),Db.P("@n",code),Db.P("@h",direction),Db.P("@a",area),Db.P("@p",price));
  }
  // Tên bảng/cột bên dưới chỉ nhận hằng số nội bộ, không nhận chuỗi nhập từ giao diện.
  private void Save(int id,string table,string key,string set,string cols,string vals,params System.Data.SqlClient.SqlParameter[] args)
  {
   Db.Transaction((c,tx)=> {
    if(id==0) Db.Execute(c,tx,"INSERT INTO "+table+"("+cols+") VALUES("+vals+")",args);
    else { using(var cmd=Db.Command(c,tx,"UPDATE "+table+" SET "+set+" WHERE "+key+"=@id",args)) {cmd.Parameters.AddWithValue("@id",id);Rules.Check(cmd.ExecuteNonQuery()==1,"Dòng đã bị xóa.");} }
   });
  }
  public void Delete(string kind,int id)
  {
   Session.Require("Quản lý tòa nhà");string table,key;
   switch(kind) { case "Tầng":table="Tang";key="MaTang";break;case "Công ty":table="CongTy";key="MaCongTy";break;case "Dịch vụ":table="DichVu";key="MaDichVu";break;case "Văn phòng":table="VanPhong";key="MaVanPhong";break;default:throw new InvalidOperationException("Danh mục không hợp lệ."); }
   Db.Transaction((c,tx)=> {Rules.Check(Db.Execute(c,tx,"DELETE FROM "+table+" WHERE "+key+"=@id",Db.P("@id",id))==1,"Chọn dòng cần xóa.");});
  }
 }
}
