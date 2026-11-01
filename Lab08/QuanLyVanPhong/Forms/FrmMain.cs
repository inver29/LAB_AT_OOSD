using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyVanPhong.Common;
using QuanLyVanPhong.Data;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmMain : Form
 {
  private ComboBox roles;private Label connection;
  public FrmMain(){InitializeComponent();Shown+=(s,e)=>TestConnection();}
  private void Open(Func<Form> factory){Ui.Run(this,()=>{using(var f=factory())f.ShowDialog(this);});}
  private void TestConnection()
  {
   try {using(var c=Db.Open())connection.Text="SQL Server • "+c.Database;connection.ForeColor=Ui.Blue;}
   catch(System.Data.SqlClient.SqlException){connection.Text="Chưa kết nối SQL Server";connection.ForeColor=Color.Firebrick;}
  }
 }
}
