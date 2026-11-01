using System;
using System.Data;
using System.Windows.Forms;
using QuanLyVanPhong.Services;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmKiemTra : Form
 {
  private readonly KiemTraService service=new KiemTraService();private readonly HopDongService rentals=new HopDongService();private readonly DanhMucService catalog=new DanhMucService();
  private ComboBox office,result,violationRoom,inspection;private DateTimePicker date,violationDate;private NumericUpDown fine,year;private TextBox note,content;private DataGridView inspections,due,violations;private bool loading;
  public FrmKiemTra(){InitializeComponent();Shown+=(s,e)=>Ui.Run(this,LoadData);}
  private void LoadData()
  {
   loading=true;try {
    Ui.Bind(office,catalog.Offices(DateTime.Today,DateTime.Today.AddDays(1)),"MaVanPhong","KyHieu");Ui.Bind(violationRoom,rentals.Details(0),"MaChiTietHD","HienThi");LoadInspections();LoadDue();LoadViolations();
   }finally{loading=false;}RoomChanged();
  }
  private void LoadInspections(){Ui.Bind(inspections,service.Inspections(Ui.Id(office)),"MaKiemTra","Mã kiểm tra","KyHieu","Văn phòng","NgayKiemTra","Ngày kiểm tra","KetQua","Kết quả","GhiChu","Ghi chú");}
  private void LoadDue(){Ui.Bind(due,service.Due((int)year.Value),"KyHieu","Văn phòng chưa kiểm tra","SoTang","Tầng","TinhTrang","Tình trạng","LanCuoi","Kiểm tra gần nhất");}
  private void LoadViolations(){Ui.Bind(violations,service.Violations(),"MaViPham","Mã biên bản","KyHieu","Văn phòng","TenCongTy","Công ty","SoHopDong","Hợp đồng","NgayLap","Ngày lập","NoiDung","Nội dung","TienPhat","Tiền phạt (đ)");}
  private void RoomChanged()
  {
   if(loading)return;var r=violationRoom.SelectedItem as DataRowView;var t=service.Inspections(r==null?-1:(int)r["MaVanPhong"]);t.Columns.Add("HienThi",typeof(string));foreach(DataRow x in t.Rows)x["HienThi"]="KT "+x["MaKiemTra"]+" — "+((DateTime)x["NgayKiemTra"]).ToString("dd/MM/yyyy")+" — "+x["KetQua"];
   var no=t.NewRow();no["MaKiemTra"]=0;no["HienThi"]="Không gắn lần kiểm tra";t.Rows.InsertAt(no,0);Ui.Bind(inspection,t,"MaKiemTra","HienThi");
  }
  private void SaveInspection(){service.Inspect(Ui.Id(office),date.Value,result.Text,note.Text);LoadInspections();LoadDue();RoomChanged();Ui.Done(this);}
  private void SaveViolation(){int k=Ui.Id(inspection);service.Violate(Ui.Id(violationRoom),k==0?(int?)null:k,violationDate.Value,content.Text,fine.Value);LoadViolations();content.Clear();Ui.Done(this);}
 }
}
