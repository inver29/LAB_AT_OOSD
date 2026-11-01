using System;
using System.Drawing;
using System.Windows.Forms;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmNhanVienTheXe
 {
  private void InitializeComponent()
  {
   var page=Ui.Page(this,"Nhân viên và thẻ giữ xe");TabPage people=Ui.Tab("Hồ sơ nhân viên"),parking=Ui.Tab("Thẻ giữ xe");Ui.Tabs(page,people,parking);
   employees=Ui.Grid();cards=Ui.Grid();keyword=Ui.Text();keyword.Width=320;cardKeyword=Ui.Text();cardKeyword.Width=320;room=Ui.Combo();employee=Ui.Combo();name=Ui.Text(100);position=Ui.Text(100);vehicle=Ui.Choices("Xe máy","Ô tô","Xe đạp");plate=Ui.Text(20);
   picture=new PictureBox {Dock=DockStyle.Fill,SizeMode=PictureBoxSizeMode.Zoom,BackColor=Color.White,BorderStyle=BorderStyle.FixedSingle};photoName=new Label {AutoSize=true,MaximumSize=new Size(180,0),TextAlign=ContentAlignment.MiddleCenter};
   var a=Ui.Stack(people,0,-100,170,0);a.Controls.Add(Ui.Actions(keyword,Ui.Button("Tìm kiếm",LoadData),Ui.Button("Làm mới",LoadData,true)),0,0);a.Controls.Add(employees,0,1);
   var editor=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=2,RowCount=1};editor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,170));editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));var ph=Ui.Stack(editor,-100,0);ph.Controls.Add(picture,0,0);ph.Controls.Add(photoName,0,1);editor.Controls.Add(Ui.Fields(2,"Họ tên *",name,"Chức vụ *",position,"Văn phòng làm việc *",room,"Ảnh *",Ui.Button("Chọn ảnh",PickPhoto,true)),1,0);a.Controls.Add(editor,0,2);a.Controls.Add(Ui.Actions(Ui.Button("Nhập mới",NewEmployee,true),Ui.Button("Lưu nhân viên",Save),Ui.Button("Ngừng làm việc",Stop,true)),0,3);
   var b=Ui.Stack(parking,0,-100,0,0);b.Controls.Add(Ui.Actions(cardKeyword,Ui.Button("Tra cứu thẻ",LoadCards),Ui.Button("Thu hồi thẻ",Revoke,true)),0,0);b.Controls.Add(cards,0,1);b.Controls.Add(Ui.Fields(3,"Nhân viên *",employee,"Loại xe *",vehicle,"Biển số *",plate),0,2);b.Controls.Add(Ui.Actions(Ui.Button("Cấp thẻ giữ xe",Issue)),0,3);
   employees.SelectionChanged+=(s,e)=>{if(!loading)Ui.Run(this,Selected);};
  }
 }
}
