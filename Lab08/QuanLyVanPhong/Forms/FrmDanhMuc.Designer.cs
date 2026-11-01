using System;
using System.Windows.Forms;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmDanhMuc
 {
  private void InitializeComponent()
  {
   var page=Ui.Page(this,"Danh mục và tra cứu văn phòng");
   TabPage tOffice=Ui.Tab("Văn phòng"),tCompany=Ui.Tab("Công ty"),tFloor=Ui.Tab("Tầng"),tFee=Ui.Tab("Dịch vụ");Ui.Tabs(page,tOffice,tCompany,tFloor,tFee);
   from=Ui.Date();to=Ui.Date(DateTime.Today.AddMonths(6));filterFloor=Ui.Combo();minArea=Ui.Number(0,100000,2);available=new CheckBox {Text="Chỉ phòng còn trống",AutoSize=true,Margin=new Padding(6,12,16,0)};
   officeFloor=Ui.Combo();officeCode=Ui.Text(20);direction=Ui.Text(30);area=Ui.Number(0,100000,2);price=Ui.Number();offices=Ui.Grid();
   var o=Ui.Stack(tOffice,0,0,-100,0,0);o.Controls.Add(Ui.Fields(5,"Từ ngày",from,"Đến ngày",to,"Tầng",filterFloor,"Diện tích tối thiểu (m²)",minArea,"Lọc lịch thuê",available),0,0);
   o.Controls.Add(Ui.Actions(Ui.Button("Tra cứu",LoadOffices),Ui.Button("Làm mới",LoadData,true)),0,1);o.Controls.Add(offices,0,2);
   o.Controls.Add(Ui.Fields(3,"Ký hiệu *",officeCode,"Tầng *",officeFloor,"Hướng *",direction,"Diện tích (m²) *",area,"Đơn giá tháng (đ) *",price),0,3);
   o.Controls.Add(Ui.Actions(Ui.Button("Nhập mới",()=>New(offices,officeCode,direction,area,price),true),Ui.Button("Lưu văn phòng",SaveOffice),Ui.Button("Xóa",()=>Delete("Văn phòng",offices,"MaVanPhong"),true)),0,4);
   companyName=Ui.Text(150);representative=Ui.Text(100);phone=Ui.Text(20);address=Ui.Text(200);companies=Ui.Grid();var c=Ui.Stack(tCompany,-100,0,0);c.Controls.Add(companies,0,0);c.Controls.Add(Ui.Fields(2,"Tên công ty *",companyName,"Người đại diện *",representative,"Điện thoại *",phone,"Địa chỉ",address),0,1);c.Controls.Add(Ui.Actions(Ui.Button("Nhập mới",()=>New(companies,companyName,representative,phone,address),true),Ui.Button("Lưu công ty",SaveCompany),Ui.Button("Xóa",()=>Delete("Công ty",companies,"MaCongTy"),true)),0,2);
   floorNumber=Ui.Number(0,200);floors=Ui.Grid();var f=Ui.Stack(tFloor,-100,0,0);f.Controls.Add(floors,0,0);f.Controls.Add(Ui.Fields(3,"Số tầng *",floorNumber),0,1);f.Controls.Add(Ui.Actions(Ui.Button("Nhập mới",()=>New(floors,floorNumber),true),Ui.Button("Lưu tầng",SaveFloor),Ui.Button("Xóa",()=>Delete("Tầng",floors,"MaTang"),true)),0,2);
   feeName=Ui.Text(100);feeUnit=Ui.Text(20);feePrice=Ui.Number();fees=Ui.Grid();var s=Ui.Stack(tFee,-100,0,0);s.Controls.Add(fees,0,0);s.Controls.Add(Ui.Fields(3,"Tên dịch vụ *",feeName,"Đơn vị tính *",feeUnit,"Đơn giá (đ) *",feePrice),0,1);s.Controls.Add(Ui.Actions(Ui.Button("Nhập mới",()=>New(fees,feeName,feeUnit,feePrice),true),Ui.Button("Lưu dịch vụ",SaveFee),Ui.Button("Xóa",()=>Delete("Dịch vụ",fees,"MaDichVu"),true)),0,2);
   floors.SelectionChanged+=(s1,e)=>SelectedFloor();companies.SelectionChanged+=(s1,e)=>SelectedCompany();offices.SelectionChanged+=(s1,e)=>SelectedOffice();fees.SelectionChanged+=(s1,e)=>SelectedFee();
  }
 }
}
