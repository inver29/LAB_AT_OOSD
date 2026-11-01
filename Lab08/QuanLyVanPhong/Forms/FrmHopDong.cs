using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyVanPhong.Services;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmHopDong : Form
 {
  private readonly HopDongService service=new HopDongService();private readonly DanhMucService catalog=new DanhMucService();
  private DataGridView list,details,available,cart;private ComboBox company;private TextBox keyword,number,content;private DateTimePicker signed,start;private NumericUpDown months,agreedPrice;private Label end;
  private DataTable chosen;private bool ready;
  public FrmHopDong(){InitializeComponent();Shown+=(s,e)=>Ui.Run(this,()=>{ready=true;Ui.Bind(company,catalog.Companies(),"MaCongTy","TenCongTy");LoadList();FindOffices();});}
  private void LoadList(){Ui.Bind(list,service.List(keyword.Text),"SoHopDong","Số hợp đồng","TenCongTy","Công ty","NgayKy","Ngày ký","TuNgay","Bắt đầu","DenNgay","Hạn ban đầu","NoiDung","Nội dung");LoadDetails();}
  private void LoadDetails(){Ui.Bind(details,service.Details(Ui.Id(list,"MaHopDong")==0?-1:Ui.Id(list,"MaHopDong")),"KyHieu","Văn phòng","DonGiaThang","Đơn giá tháng (đ)","DenNgayHienTai","Hạn hiện tại");}
  private DateTime EndDate {get {return start.Value.Date.AddMonths((int)months.Value);} }
  private void DatesChanged(){end.Text=EndDate.ToString("dd/MM/yyyy");chosen.Clear();if(ready)Ui.Run(this,FindOffices);}
  private void FindOffices(){Ui.Bind(available,catalog.Offices(start.Value.Date,EndDate,0,0,true),"KyHieu","Phòng trống","SoTang","Tầng","Huong","Hướng","DienTich","Diện tích (m²)","DonGiaThang","Giá tham khảo (đ)");}
  private void Add()
  {
   var r=Ui.Row(available);if(r==null)throw new InvalidOperationException("Chọn phòng trống.");int id=(int)r["MaVanPhong"];
   if(chosen.AsEnumerable().Any(x=>(int)x["MaVanPhong"]==id))throw new InvalidOperationException("Phòng đã có trong hợp đồng.");
   if(agreedPrice.Value<=0)throw new InvalidOperationException("Đơn giá phải dương.");chosen.Rows.Add(id,r["KyHieu"],agreedPrice.Value);
  }
  private void Remove(){var r=Ui.Row(cart);if(r!=null)chosen.Rows.Remove(r);}
  private void Create()
  {
   var lines=chosen.AsEnumerable().Select(r=>new RentalLine {OfficeId=(int)r["MaVanPhong"],Price=(decimal)r["DonGiaThang"]}).ToList();
   if(!Ui.Confirm(this,"Lưu hợp đồng và tạo hóa đơn tiền thuê sáu tháng đầu?"))return;
   int id=service.Create(number.Text,Ui.Id(company),signed.Value,start.Value,EndDate,content.Text,lines);LoadList();Ui.Select(list,"MaHopDong",id);chosen.Clear();number.Clear();FindOffices();Ui.Done(this,"Đã lập hợp đồng và hóa đơn tiền thuê sáu tháng đầu.");
  }
 }
}
