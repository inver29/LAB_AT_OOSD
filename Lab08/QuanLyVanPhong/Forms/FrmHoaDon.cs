using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using QuanLyVanPhong.Services;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmHoaDon : Form
 {
  private readonly HoaDonService service=new HoaDonService();private readonly HopDongService rentals=new HopDongService();private readonly DanhMucService catalog=new DanhMucService();
  private DataGridView bills,details,feeCart;private TextBox keyword,rentNumber,feeNumber;private ComboBox rentContract,feeContract,feeOffice,feeService;private DateTimePicker rentFrom,feeFrom,feeTo,payDate;private Label rentTo,total,unitPrice,feeTotal;private NumericUpDown received,quantity;private DataTable lines;private bool loading;
  public FrmHoaDon(){InitializeComponent();Shown+=(s,e)=>Ui.Run(this,LoadData);}
  private void LoadData()
  {
   loading=true;try {var h=rentals.List();Ui.Bind(rentContract,h.Copy(),"MaHopDong","HienThi");Ui.Bind(feeContract,h.Copy(),"MaHopDong","HienThi");Ui.Bind(feeService,catalog.Services(),"MaDichVu","TenDichVu");LoadBills();}finally{loading=false;}
   ContractChanged();FeeContractChanged();PriceChanged();
  }
  private void LoadBills(){Ui.Bind(bills,service.List(keyword.Text),"SoHoaDon","Số hóa đơn","SoHopDong","Hợp đồng","TenCongTy","Công ty","Loai","Loại","TuNgay","Từ ngày","DenNgay","Đến ngày","TongTien","Tổng tiền (đ)","TrangThai","Trạng thái","NgayThanhToan","Ngày thu");LoadDetails();}
  private void LoadDetails()
  {
   var r=Ui.Row(bills);Ui.Bind(details,service.Details(r==null?0:(int)r["MaHoaDon"]),"KyHieu","Văn phòng","NoiDung","Nội dung","SoLuong","Số lượng","DonGia","Đơn giá (đ)","ThanhTien","Thành tiền (đ)");
   decimal money=r==null?0:(decimal)r["TongTien"];total.Text="Tổng hóa đơn: "+Ui.Money(money);received.Value=money;
  }
  private void ContractChanged()
  {
   if(loading)return;var r=rentContract.SelectedItem as DataRowView;if(r!=null){rentFrom.Value=((DateTime)r["TuNgay"]).AddMonths(6);rentTo.Text=rentFrom.Value.AddMonths(1).ToString("dd/MM/yyyy");}
  }
  private void FeeContractChanged(){if(loading)return;lines.Clear();Ui.Bind(feeOffice,rentals.Details(Ui.Id(feeContract)==0?-1:Ui.Id(feeContract)),"MaChiTietHD","KyHieu");}
  private void PriceChanged(){var r=feeService.SelectedItem as DataRowView;unitPrice.Text=r==null?"0 đ":Ui.Money((decimal)r["DonGia"]);}
  private void AddFee()
  {
   var d=feeOffice.SelectedItem as DataRowView;var s=feeService.SelectedItem as DataRowView;if(d==null||s==null||quantity.Value<=0)throw new InvalidOperationException("Chọn phòng, dịch vụ và số lượng dương.");
   int did=(int)d["MaChiTietHD"],sid=(int)s["MaDichVu"];if(lines.AsEnumerable().Any(x=>(int)x["MaChiTietHD"]==did && (int)x["MaDichVu"]==sid))throw new InvalidOperationException("Phòng và dịch vụ đã có trong danh sách.");
   decimal price=(decimal)s["DonGia"];lines.Rows.Add(did,sid,d["KyHieu"],s["TenDichVu"],quantity.Value,price,quantity.Value*price);ShowFeeTotal();
  }
  private void RemoveFee(){var r=Ui.Row(feeCart);if(r!=null)lines.Rows.Remove(r);ShowFeeTotal();}
  private void ShowFeeTotal(){feeTotal.Text="Tổng dịch vụ: "+Ui.Money(lines.AsEnumerable().Sum(x=>(decimal)x["ThanhTien"]));}
  private void CreateRent(){int id=service.CreateRent(Ui.Id(rentContract),rentNumber.Text,rentFrom.Value);LoadBills();Ui.Select(bills,"MaHoaDon",id);rentNumber.Clear();Ui.Done(this);}
  private void CreateFees()
  {
   var items=lines.AsEnumerable().Select(r=>new FeeLine {DetailId=(int)r["MaChiTietHD"],ServiceId=(int)r["MaDichVu"],Quantity=(decimal)r["SoLuong"]}).ToList();
   int id=service.CreateFees(Ui.Id(feeContract),feeNumber.Text,feeFrom.Value,feeTo.Value,items);LoadBills();Ui.Select(bills,"MaHoaDon",id);lines.Clear();ShowFeeTotal();feeNumber.Clear();Ui.Done(this);
  }
  private void Pay(){var r=Ui.Row(bills);if(r==null)throw new InvalidOperationException("Chọn hóa đơn cần thu.");if(!Ui.Confirm(this,"Xác nhận đã thu đủ "+Ui.Money(received.Value)+" cho "+r["SoHoaDon"]+"?"))return;service.Pay((int)r["MaHoaDon"],received.Value,payDate.Value);LoadBills();Ui.Done(this,"Đã ghi nhận thanh toán.");}
 }
}
