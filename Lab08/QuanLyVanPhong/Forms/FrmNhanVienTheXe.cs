using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using QuanLyVanPhong.Services;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmNhanVienTheXe : Form
 {
  private readonly NhanVienTheXeService service=new NhanVienTheXeService();private readonly HopDongService rentals=new HopDongService();
  private DataGridView employees,cards;private ComboBox room,employee,vehicle;private TextBox keyword,cardKeyword,name,position,plate;private PictureBox picture;private Label photoName;private string photo="";private bool loading;
  public FrmNhanVienTheXe(){InitializeComponent();Shown+=(s,e)=>Ui.Run(this,LoadData);FormClosed+=(s,e)=>{if(picture.Image!=null)picture.Image.Dispose();};}
  private void LoadData()
  {
   loading=true;try {
    Ui.Bind(room,rentals.Details(0),"MaChiTietHD","HienThi");var t=service.Employees(keyword.Text);Ui.Bind(employees,t,"HoTen","Họ tên","ChucVu","Chức vụ","TenCongTy","Công ty","KyHieu","Văn phòng","DangLamViec","Còn làm việc");
    var all=service.Employees();var active=all.Clone();foreach(DataRow r in all.Rows)if((bool)r["DangLamViec"])active.ImportRow(r);Ui.Bind(employee,active,"MaNhanVien","HienThi");LoadCards();
   }finally{loading=false;}Selected();
  }
  private void LoadCards(){Ui.Bind(cards,service.Cards(cardKeyword.Text),"MaThe","Mã thẻ","HoTen","Nhân viên","TenCongTy","Công ty","KyHieu","Văn phòng","LoaiXe","Loại xe","BienSo","Biển số","NgayCap","Ngày cấp","TrangThai","Trạng thái");}
  private void Selected()
  {
   if(loading)return;var r=Ui.Row(employees);if(r==null)return;name.Text=(string)r["HoTen"];position.Text=(string)r["ChucVu"];room.SelectedValue=r["MaChiTietHD"];room.Enabled=false;photo=(string)r["Anh"];ShowPhoto();
  }
  private void ShowPhoto()
  {
   var old=picture.Image;picture.Image=null;if(old!=null)old.Dispose();photoName.Text=Path.GetFileName(photo);
   if(File.Exists(photo)){using(var stream=File.OpenRead(photo))using(var img=Image.FromStream(stream))picture.Image=new Bitmap(img);}
  }
  private void PickPhoto()
  {
   using(var dialog=new OpenFileDialog {Filter="Ảnh nhân viên|*.png;*.jpg;*.jpeg;*.bmp",Title="Chọn ảnh nhân viên"})
   {if(dialog.ShowDialog(this)==DialogResult.OK){using(var stream=File.OpenRead(dialog.FileName))using(var img=Image.FromStream(stream)){}photo=dialog.FileName;ShowPhoto();}}
  }
  private void NewEmployee(){employees.CurrentCell=null;employees.ClearSelection();name.Clear();position.Clear();photo="";room.Enabled=true;ShowPhoto();}
  private void Save(){service.Save(Ui.Id(employees,"MaNhanVien"),Ui.Id(room),name.Text,position.Text,photo);LoadData();Ui.Done(this);}
  private void Stop(){int id=Ui.Id(employees,"MaNhanVien");if(id>0 && Ui.Confirm(this,"Ngừng nhân viên và thu hồi toàn bộ thẻ đang sử dụng?")){service.Stop(id);LoadData();Ui.Done(this);}}
  private void Issue(){service.Issue(Ui.Id(employee),vehicle.Text,plate.Text);LoadCards();plate.Clear();Ui.Done(this);}
  private void Revoke(){int id=Ui.Id(cards,"MaThe");if(id>0 && Ui.Confirm(this,"Thu hồi thẻ xe đang chọn?")){service.Revoke(id);LoadCards();Ui.Done(this);}}
 }
}
