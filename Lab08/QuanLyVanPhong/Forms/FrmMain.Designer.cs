using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyVanPhong.Common;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmMain
 {
  private void InitializeComponent()
  {
   SuspendLayout();
   var page=Ui.Page(this,"Quản lý cho thuê văn phòng");
   var title=page.GetControlFromPosition(0,0) as Label;title.TextAlign=ContentAlignment.MiddleCenter;
   var root=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=3,Padding=new Padding(18,0,18,0)};
   root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.AutoSize));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,54));page.Controls.Add(root,0,1);
   roles=Ui.Choices("Toàn quyền (demo)","Quản lý tòa nhà","Kế toán","Bảo vệ");roles.Width=220;roles.SelectedIndexChanged+=(s,e)=>Session.Role=roles.Text;
   connection=new Label {Text="Đang kết nối...",AutoSize=true,Margin=new Padding(8,12,20,0)};
   root.Controls.Add(Ui.Actions(connection,new Label {Text="Vai trò",AutoSize=true,Margin=new Padding(8,12,8,0)},roles,Ui.Button("Kết nối lại",TestConnection,true)),0,0);
   var cards=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=3,RowCount=2,Margin=new Padding(0,18,0,12)};
   for(int i=0;i<3;i++)cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100F/3));for(int i=0;i<2;i++)cards.RowStyles.Add(new RowStyle(SizeType.Percent,50));
   string[] names={"Danh mục\nvà tra cứu văn phòng","Hợp đồng\nthuê văn phòng","Hóa đơn\nvà thanh toán","Gia hạn\nhợp đồng","Nhân viên\nvà thẻ giữ xe","Kiểm tra\nvà vi phạm"};
   Func<Form>[] factories={()=>new FrmDanhMuc(),()=>new FrmHopDong(),()=>new FrmHoaDon(),()=>new FrmGiaHan(),()=>new FrmNhanVienTheXe(),()=>new FrmKiemTra()};
   for(int i=0;i<6;i++)
   {
    var factory=factories[i];var b=Ui.Button(names[i],()=>Open(factory));b.AutoSize=false;b.Dock=DockStyle.Fill;b.Margin=new Padding(8);b.Font=new Font("Segoe UI",13F,FontStyle.Bold);b.BackColor=Color.FromArgb(232,240,248);b.ForeColor=Color.FromArgb(35,77,115);b.FlatAppearance.BorderSize=1;b.FlatAppearance.BorderColor=Color.FromArgb(197,213,229);cards.Controls.Add(b,i%3,i/3);
   }
   root.Controls.Add(cards,0,1);var exit=Ui.Button("Thoát",()=>Close(),true);exit.Anchor=AnchorStyles.None;exit.Width=150;root.Controls.Add(exit,0,2);
   ResumeLayout(true);
  }
 }
}
