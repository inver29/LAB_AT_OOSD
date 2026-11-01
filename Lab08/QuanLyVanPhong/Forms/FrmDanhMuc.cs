using System;
using System.Data;
using System.Windows.Forms;
using QuanLyVanPhong.Services;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmDanhMuc : Form
 {
  private readonly DanhMucService service=new DanhMucService();
  private DataGridView floors,companies,offices,fees;
  private NumericUpDown floorNumber,area,price,feePrice,minArea;
  private TextBox companyName,representative,phone,address,officeCode,direction,feeName,feeUnit;
  private ComboBox officeFloor,filterFloor;private DateTimePicker from,to;private CheckBox available;private bool loading;
  public FrmDanhMuc(){InitializeComponent();Shown+=(s,e)=>Ui.Run(this,LoadData);}
  private void LoadData()
  {
   loading=true;try {
    var f=service.Floors();Ui.Bind(floors,f,"SoTang","Số tầng");Ui.Bind(officeFloor,f.Copy(),"MaTang","SoTang");
    var ff=f.Copy();ff.Columns.Add("HienThi",typeof(string));foreach(DataRow row in ff.Rows)row["HienThi"]="Tầng "+row["SoTang"];var all=ff.NewRow();all["MaTang"]=0;all["SoTang"]=-1;all["HienThi"]="Tất cả tầng";ff.Rows.InsertAt(all,0);Ui.Bind(filterFloor,ff,"MaTang","HienThi");
    Ui.Bind(companies,service.Companies(),"TenCongTy","Tên công ty","DaiDien","Đại diện","DienThoai","Điện thoại","DiaChi","Địa chỉ");
    Ui.Bind(fees,service.Services(),"TenDichVu","Dịch vụ","DonViTinh","Đơn vị tính","DonGia","Đơn giá (đ)");
    LoadOffices();
   }finally{loading=false;} SelectedFloor();SelectedCompany();SelectedOffice();SelectedFee(); }
  private void LoadOffices(){Ui.Bind(offices,service.Offices(from.Value,to.Value,minArea.Value,Ui.Id(filterFloor),available.Checked),"KyHieu","Văn phòng","SoTang","Tầng","Huong","Hướng","DienTich","Diện tích (m²)","DonGiaThang","Giá tháng (đ)","TinhTrang","Tình trạng","LichThue","Lịch thuê");}
  private void SaveFloor(){service.SaveFloor(Ui.Id(floors,"MaTang"),(int)floorNumber.Value);LoadData();Ui.Done(this);}
  private void SaveCompany(){service.SaveCompany(Ui.Id(companies,"MaCongTy"),companyName.Text,representative.Text,phone.Text,address.Text);LoadData();Ui.Done(this);}
  private void SaveOffice(){service.SaveOffice(Ui.Id(offices,"MaVanPhong"),Ui.Id(officeFloor),officeCode.Text,direction.Text,area.Value,price.Value);LoadOffices();Ui.Done(this);}
  private void SaveFee(){service.SaveService(Ui.Id(fees,"MaDichVu"),feeName.Text,feeUnit.Text,feePrice.Value);LoadData();Ui.Done(this);}
  private void Delete(string type,DataGridView grid,string key){int id=Ui.Id(grid,key);if(id>0 && Ui.Confirm(this,"Xóa "+type.ToLower()+" đang chọn?")){service.Delete(type,id);LoadData();}}
  private void New(DataGridView grid,params Control[] fields){grid.ClearSelection();grid.CurrentCell=null;foreach(var c in fields){if(c is TextBox)c.Text="";else if(c is NumericUpDown)((NumericUpDown)c).Value=0;}}
  private void SelectedFloor(){if(loading)return;var r=Ui.Row(floors);if(r!=null)floorNumber.Value=(int)r["SoTang"];}
  private void SelectedCompany(){if(loading)return;var r=Ui.Row(companies);if(r==null)return;companyName.Text=(string)r["TenCongTy"];representative.Text=(string)r["DaiDien"];phone.Text=(string)r["DienThoai"];address.Text=Convert.ToString(r["DiaChi"]);}
  private void SelectedOffice(){if(loading)return;var r=Ui.Row(offices);if(r==null)return;officeFloor.SelectedValue=r["MaTang"];officeCode.Text=(string)r["KyHieu"];direction.Text=(string)r["Huong"];area.Value=(decimal)r["DienTich"];price.Value=(decimal)r["DonGiaThang"];}
  private void SelectedFee(){if(loading)return;var r=Ui.Row(fees);if(r==null)return;feeName.Text=(string)r["TenDichVu"];feeUnit.Text=(string)r["DonViTinh"];feePrice.Value=(decimal)r["DonGia"];}
 }
}
