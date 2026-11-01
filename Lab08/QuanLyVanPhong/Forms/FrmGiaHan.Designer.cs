using System;
using System.Windows.Forms;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmGiaHan
 {
  private void InitializeComponent()
  {
   var page=Ui.Page(this,"Gia hạn hợp đồng");TabPage create=Ui.Tab("Lập phiếu gia hạn"),old=Ui.Tab("Lịch sử gia hạn");Ui.Tabs(page,create,old);
   contract=Ui.Combo();number=Ui.Text(20);months=Ui.Number(1,60);months.Minimum=1;date=Ui.Date();date.Enabled=false;limit=new Label {AutoSize=true,Margin=new Padding(0,12,12,0)};reconciled=new CheckBox {Text="Kế toán đã chốt phí phát sinh và thu đủ đến hôm nay",AutoSize=true,Margin=new Padding(0,10,0,8)};
   rooms=Ui.Grid(true);debts=Ui.Grid();history=Ui.Grid();var a=Ui.Stack(create,0,0,-50,-50,0,0);a.Controls.Add(Ui.Fields(4,"Hợp đồng *",contract,"Số phiếu *",number,"Số tháng *",months,"Ngày lập / Đối chiếu phí",date),0,0);a.Controls.Add(Ui.Actions(Ui.Button("Đối chiếu công nợ",LoadData,true),limit),0,1);a.Controls.Add(rooms,0,2);a.Controls.Add(debts,0,3);a.Controls.Add(reconciled,0,4);a.Controls.Add(Ui.Actions(Ui.Button("Lập phiếu gia hạn",Save)),0,5);old.Controls.Add(history);
   contract.SelectedIndexChanged+=(s,e)=>Ui.Run(this,LoadData);rooms.CurrentCellDirtyStateChanged+=(s,e)=>{if(rooms.IsCurrentCellDirty)rooms.CommitEdit(DataGridViewDataErrorContexts.Commit);};
  }
 }
}
