using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuanLyHoSoBenhAn.Forms;
class UiCheck {
 static IEnumerable<Control> All(Control c) { foreach(Control x in c.Controls){yield return x; foreach(var y in All(x)) yield return y;} }
 [STAThread] static int Main(string[] args) {
  Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false);
  Directory.CreateDirectory(args[0]);
  foreach(Form f in new Form[]{new FrmMain(),new FrmHoSo(),new FrmKhamBenh(),new FrmToaThuoc(),new FrmThanhToan()}) {
   using(f){ f.Show(); Application.DoEvents(); f.PerformLayout();
    var controls=All(f).ToList();
    if(f is FrmMain && (controls.OfType<Button>().Count()!=5 || controls.OfType<Label>().Count()!=1)) throw new Exception("Main must have exactly 5 buttons and 1 label");
    if(f is FrmMain) {
     foreach(var button in controls.OfType<Button>().Where(b=>b.Name!="btnExit")) {
      string expected=button.Name=="btnHoSo"?"FrmHoSo":button.Name=="btnKham"?"FrmKhamBenh":button.Name=="btnToa"?"FrmToaThuoc":"FrmThanhToan";
      bool opened=false; var before=button.BackColor;
      using(var timer=new System.Windows.Forms.Timer()){timer.Interval=100; timer.Tick+=(s,e)=>{var child=Application.OpenForms.Cast<Form>().FirstOrDefault(x=>x!=f);if(child!=null){opened=child.GetType().Name==expected;child.Close();timer.Stop();}};timer.Start();button.PerformClick();}
      if(!opened || button.BackColor!=before)throw new Exception("Navigation or palette reset: "+button.Name);
     }
     Console.WriteLine("PASS all four main buttons open the correct form and retain their palette");
    }
    foreach(var size in new[]{f.Size,f.MinimumSize}) {
     f.Size=size; Application.DoEvents(); f.PerformLayout();
     using(var b=new Bitmap(f.Width,f.Height)){f.DrawToBitmap(b,new Rectangle(Point.Empty,f.Size));b.Save(Path.Combine(args[0],f.GetType().Name+"_"+size.Width+".png"));}
     foreach(var button in controls.OfType<Button>()) if(button.Visible && (button.Width<90 || button.Height<28)) throw new Exception("Button clipped: "+f.GetType().Name+"/"+button.Name);
    }
    Console.WriteLine("PASS "+f.GetType().Name+": rendered default and minimum size; "+controls.OfType<DataGridView>().Sum(g=>g.Rows.Count)+" bound rows");
    f.Close();
   }
  }
  return 0;
 }
}

