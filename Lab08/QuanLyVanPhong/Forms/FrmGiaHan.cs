using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyVanPhong.Services;
namespace QuanLyVanPhong.Forms
{
 public partial class FrmGiaHan : Form
 {
  private readonly GiaHanService service=new GiaHanService();private readonly HopDongService rentals=new HopDongService();
  private ComboBox contract;private TextBox number;private NumericUpDown months;private DateTimePicker date;private CheckBox reconciled;private DataGridView rooms,debts,history;private Label limit;private bool loading;
  public FrmGiaHan(){InitializeComponent();Shown+=(s,e)=>Ui.Run(this,()=>{loading=true;try{Ui.Bind(contract,rentals.List(),"MaHopDong","HienThi");}finally{loading=false;}LoadData();});}
  private void LoadData()
  {
   if(loading)return;reconciled.Checked=false;int id=Ui.Id(contract);var t=rentals.Details(id==0?-1:id);t.Columns.Add("Chon",typeof(bool));foreach(DataRow r in t.Rows)r["Chon"]=false;
   Ui.Bind(rooms,t,"Chon","Chọn","KyHieu","Văn phòng","TuNgay","Bắt đầu","DenNgayHienTai","Hạn hiện tại","DonGiaThang","Đơn giá tháng (đ)");foreach(DataGridViewColumn c in rooms.Columns)c.ReadOnly=c.Name!="Chon";
   Ui.Bind(debts,service.Debts(id==0?-1:id),"SoHoaDon","Hóa đơn","Loai","Loại","TuNgay","Từ ngày","DenNgay","Đến ngày","TongTien","Tổng tiền (đ)","TrangThai","Trạng thái");
   Ui.Bind(history,service.History(id),"SoPhieu","Số phiếu","NgayLap","Ngày lập","SoThang","Số tháng","KyHieu","Văn phòng","HanCu","Hạn cũ","DenNgayMoi","Hạn mới");
   var row=contract.SelectedItem as DataRowView;if(row!=null){DateTime s=(DateTime)row["TuNgay"],e=(DateTime)row["DenNgay"];int max=((e.Year-s.Year)*12+e.Month-s.Month)/2;limit.Text="Tối đa "+max+" tháng/lần";months.Maximum=max;}
  }
  private void Save()
  {
   rooms.EndEdit();var t=rooms.DataSource as DataTable;var selected=t==null?new System.Collections.Generic.List<int>():t.AsEnumerable().Where(x=>x["Chon"]!=DBNull.Value && (bool)x["Chon"]).Select(x=>(int)x["MaChiTietHD"]).ToList();
   if(!Ui.Confirm(this,"Lập phiếu gia hạn cho "+selected.Count+" phòng đã chọn?"))return;
   service.Extend(Ui.Id(contract),number.Text,(int)months.Value,date.Value,reconciled.Checked,selected);LoadData();number.Clear();Ui.Done(this);
  }
 }
}
