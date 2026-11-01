using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmHoaDon
 {
  private void InitializeComponent()
  {
   var page=Ui.Page(this,"Hóa đơn và thanh toán");TabPage view=Ui.Tab("Hóa đơn / Thu tiền"),rent=Ui.Tab("Lập tiền thuê"),fee=Ui.Tab("Lập phí dịch vụ");Ui.Tabs(page,view,rent,fee);
   keyword=Ui.Text();keyword.Width=300;bills=Ui.Grid();details=Ui.Grid();received=Ui.Number();payDate=Ui.Date();total=new Label {Text="Tổng hóa đơn: 0 đ",AutoSize=true,Font=new Font("Segoe UI",13F,FontStyle.Bold),Margin=new Padding(0,8,0,8)};
   var a=Ui.Stack(view,0,-55,-45,0,0,0);a.Controls.Add(Ui.Actions(keyword,Ui.Button("Tìm kiếm",LoadBills),Ui.Button("Làm mới",LoadBills,true)),0,0);a.Controls.Add(bills,0,1);a.Controls.Add(details,0,2);a.Controls.Add(total,0,3);a.Controls.Add(Ui.Fields(3,"Số tiền đã nhận (đ) *",received,"Ngày thanh toán *",payDate),0,4);a.Controls.Add(Ui.Actions(Ui.Button("Xác nhận thu đủ",Pay)),0,5);
   rentContract=Ui.Combo();rentNumber=Ui.Text(20);rentFrom=Ui.Date();rentTo=new Label {AutoSize=true,Padding=new Padding(0,4,0,0)};
   var b=Ui.Stack(rent,0,0,-100);b.Controls.Add(Ui.Fields(2,"Hợp đồng *",rentContract,"Số hóa đơn *",rentNumber,"Kỳ từ ngày *",rentFrom,"Kỳ đến ngày (không gồm)",rentTo),0,0);b.Controls.Add(Ui.Actions(Ui.Button("Lập tiền thuê tháng",CreateRent)),0,1);
   feeContract=Ui.Combo();feeNumber=Ui.Text(20);feeOffice=Ui.Combo();feeService=Ui.Combo();quantity=Ui.Number(1,1000000000,2);unitPrice=new Label {AutoSize=true,Padding=new Padding(0,4,0,0)};feeFrom=Ui.Date(new DateTime(DateTime.Today.Year,DateTime.Today.Month,1).AddMonths(-1));feeTo=Ui.Date(new DateTime(DateTime.Today.Year,DateTime.Today.Month,1));feeTotal=new Label {AutoSize=true,Font=new Font("Segoe UI",12F,FontStyle.Bold),Text="Tổng dịch vụ: 0 đ",Margin=new Padding(0,12,10,0)};
   lines=new DataTable();lines.Columns.Add("MaChiTietHD",typeof(int));lines.Columns.Add("MaDichVu",typeof(int));lines.Columns.Add("KyHieu");lines.Columns.Add("TenDichVu");lines.Columns.Add("SoLuong",typeof(decimal));lines.Columns.Add("DonGia",typeof(decimal));lines.Columns.Add("ThanhTien",typeof(decimal));feeCart=Ui.Grid();Ui.Bind(feeCart,lines,"KyHieu","Văn phòng","TenDichVu","Dịch vụ","SoLuong","Số lượng","DonGia","Đơn giá (đ)","ThanhTien","Thành tiền (đ)");
   var c=Ui.Stack(fee,0,0,0,-100,0);c.Controls.Add(Ui.Fields(4,"Hợp đồng *",feeContract,"Số hóa đơn *",feeNumber,"Từ ngày *",feeFrom,"Đến ngày (không gồm) *",feeTo),0,0);c.Controls.Add(Ui.Fields(4,"Văn phòng *",feeOffice,"Dịch vụ *",feeService,"Số lượng *",quantity,"Đơn giá danh mục",unitPrice),0,1);c.Controls.Add(Ui.Actions(Ui.Button("Thêm dịch vụ",AddFee),Ui.Button("Bỏ dòng chọn",RemoveFee,true)),0,2);c.Controls.Add(feeCart,0,3);c.Controls.Add(Ui.Actions(Ui.Button("Lập hóa đơn dịch vụ",CreateFees),feeTotal),0,4);
   bills.SelectionChanged+=(s,e)=>{if(!loading)Ui.Run(this,LoadDetails);};rentContract.SelectedIndexChanged+=(s,e)=>ContractChanged();feeContract.SelectedIndexChanged+=(s,e)=>Ui.Run(this,FeeContractChanged);feeService.SelectedIndexChanged+=(s,e)=>PriceChanged();rentFrom.ValueChanged+=(s,e)=>rentTo.Text=rentFrom.Value.AddMonths(1).ToString("dd/MM/yyyy");
  }
 }
}
