using System;
using System.Windows.Forms;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmKiemTra
 {
  private void InitializeComponent()
  {
   var page=Ui.Page(this,"Kiểm tra phòng và ghi nhận vi phạm");TabPage check=Ui.Tab("Kiểm tra phòng"),annual=Ui.Tab("Phòng cần kiểm tra"),violate=Ui.Tab("Biên bản vi phạm");Ui.Tabs(page,check,annual,violate);
   office=Ui.Combo();result=Ui.Choices("Đạt","Cần sửa");date=Ui.Date();note=Ui.Text(500);year=Ui.Number(DateTime.Today.Year,2100);year.Minimum=2000;year.ThousandsSeparator=false;inspections=Ui.Grid();due=Ui.Grid();violations=Ui.Grid();
   var a=Ui.Stack(check,0,-100,0,0);a.Controls.Add(Ui.Fields(3,"Văn phòng *",office,"Ngày kiểm tra *",date,"Kết quả *",result),0,0);a.Controls.Add(inspections,0,1);a.Controls.Add(Ui.Fields(1,"Ghi chú",note),0,2);a.Controls.Add(Ui.Actions(Ui.Button("Lưu lần kiểm tra",SaveInspection),Ui.Button("Làm mới",LoadInspections,true)),0,3);
   var b=Ui.Stack(annual,0,0,-100);b.Controls.Add(Ui.Fields(4,"Năm kiểm tra",year),0,0);b.Controls.Add(Ui.Actions(Ui.Button("Xem phòng chưa kiểm tra",LoadDue)),0,1);b.Controls.Add(due,0,2);
   violationRoom=Ui.Combo();inspection=Ui.Combo();violationDate=Ui.Date();fine=Ui.Number(500000);content=Ui.Text(500);var c=Ui.Stack(violate,-100,0,0,0);c.Controls.Add(violations,0,0);c.Controls.Add(Ui.Fields(2,"Phòng / Công ty chịu trách nhiệm *",violationRoom,"Lần kiểm tra liên quan",inspection,"Ngày lập *",violationDate,"Tiền phạt (đ) *",fine),0,1);c.Controls.Add(Ui.Fields(1,"Nội dung vi phạm *",content),0,2);c.Controls.Add(Ui.Actions(Ui.Button("Lập biên bản vi phạm",SaveViolation),Ui.Button("Làm mới",LoadViolations,true)),0,3);
   office.SelectedIndexChanged+=(s,e)=>{if(!loading)Ui.Run(this,LoadInspections);};violationRoom.SelectedIndexChanged+=(s,e)=>{if(!loading)Ui.Run(this,RoomChanged);};
  }
 }
}
