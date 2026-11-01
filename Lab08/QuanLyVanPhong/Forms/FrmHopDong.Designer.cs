using System;
using System.Data;
using System.Windows.Forms;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmHopDong
 {
  private void InitializeComponent()
  {
   var page=Ui.Page(this,"Hợp đồng thuê văn phòng");TabPage existing=Ui.Tab("Tra cứu hợp đồng"),create=Ui.Tab("Lập hợp đồng");Ui.Tabs(page,existing,create);
   keyword=Ui.Text();keyword.Width=300;list=Ui.Grid();details=Ui.Grid();var a=Ui.Stack(existing,0,-60,-40);a.Controls.Add(Ui.Actions(keyword,Ui.Button("Tìm kiếm",LoadList),Ui.Button("Làm mới",LoadList,true)),0,0);a.Controls.Add(list,0,1);a.Controls.Add(details,0,2);
   company=Ui.Combo();number=Ui.Text(16);content=Ui.Text(1000);signed=Ui.Date();start=Ui.Date();months=Ui.Number(6,120);months.Minimum=6;end=new Label {Text=start.Value.AddMonths(6).ToString("dd/MM/yyyy"),AutoSize=true,Padding=new Padding(0,4,0,0)};
   agreedPrice=Ui.Number();agreedPrice.Width=170;available=Ui.Grid();cart=Ui.Grid();chosen=new DataTable();chosen.Columns.Add("MaVanPhong",typeof(int));chosen.Columns.Add("KyHieu");chosen.Columns.Add("DonGiaThang",typeof(decimal));Ui.Bind(cart,chosen,"KyHieu","Phòng đã chọn","DonGiaThang","Đơn giá thỏa thuận/tháng (đ)");
   var b=Ui.Stack(create,0,0,-45,-55,0);b.Controls.Add(Ui.Fields(4,"Số hợp đồng *",number,"Công ty *",company,"Ngày ký *",signed,"Từ ngày *",start,"Số tháng thuê *",months,"Đến ngày (không gồm)",end,"Điều khoản *",content),0,0);
   b.Controls.Add(Ui.Actions(Ui.Button("Tìm phòng trống",FindOffices),new Label {Text="Giá thỏa thuận (đ)",AutoSize=true,Margin=new Padding(0,12,8,0)},agreedPrice,Ui.Button("Thêm phòng",Add)),0,1);
   b.Controls.Add(available,0,2);b.Controls.Add(cart,0,3);b.Controls.Add(Ui.Actions(Ui.Button("Bỏ phòng chọn",Remove,true),Ui.Button("Lập hợp đồng",Create)),0,4);
   list.SelectionChanged+=(s,e)=>{if(ready)Ui.Run(this,LoadDetails);};available.SelectionChanged+=(s,e)=>{var r=Ui.Row(available);if(r!=null)agreedPrice.Value=(decimal)r["DonGiaThang"];};start.ValueChanged+=(s,e)=>DatesChanged();months.ValueChanged+=(s,e)=>DatesChanged();
  }
 }
}
