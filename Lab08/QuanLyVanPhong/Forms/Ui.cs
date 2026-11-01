using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
namespace QuanLyVanPhong.Forms
{
 internal static class Ui
 {
  internal static readonly Color Ink=Color.FromArgb(37,55,74), Blue=Color.FromArgb(55,103,145), Background=Color.FromArgb(244,247,250);
  internal static TableLayoutPanel Page(Form f,string title)
  {
   f.Text=title;f.Font=new Font("Segoe UI",10F);f.AutoScaleMode=AutoScaleMode.Dpi;
   f.BackColor=Background;f.ForeColor=Ink;f.StartPosition=FormStartPosition.CenterScreen;
   f.ClientSize=new Size(1120,740);f.MinimumSize=new Size(1000,680);
   var root=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=2,Padding=new Padding(20,12,20,16)};
   root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,62));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));
   root.Controls.Add(new Label {Text=title,Font=new Font("Segoe UI",20F,FontStyle.Bold),Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleLeft,AutoEllipsis=false,UseMnemonic=false},0,0);
   f.Controls.Add(root);return root;
  }
  internal static TabControl Tabs(TableLayoutPanel page,params TabPage[] tabs)
  { var t=new TabControl {Dock=DockStyle.Fill};t.TabPages.AddRange(tabs);page.Controls.Add(t,0,1);return t; }
  internal static TabPage Tab(string name) { return new TabPage(name){BackColor=Background,Padding=new Padding(12)}; }
  internal static TableLayoutPanel Stack(Control parent,params float[] rows)
  {
   var t=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=rows.Length,Margin=new Padding(0)};t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
   foreach(float r in rows) t.RowStyles.Add(r<0?new RowStyle(SizeType.Percent,-r):r==0?new RowStyle(SizeType.AutoSize):new RowStyle(SizeType.Absolute,r));
   parent.Controls.Add(t);return t;
  }
  internal static TableLayoutPanel Fields(int columns,params object[] pairs)
  {
   int count=pairs.Length/2;var t=new TableLayoutPanel {Dock=DockStyle.Fill,AutoSize=true,ColumnCount=columns,RowCount=(count+columns-1)/columns,Margin=new Padding(0,4,0,4)};
   for(int i=0;i<columns;i++)t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100F/columns));
   for(int i=0;i<t.RowCount;i++)t.RowStyles.Add(new RowStyle(SizeType.Absolute,72));
   for(int i=0;i<count;i++)
   {
    var panel=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=2,Margin=new Padding(0,0,12,2)};
    panel.RowStyles.Add(new RowStyle(SizeType.Absolute,28));panel.RowStyles.Add(new RowStyle(SizeType.Percent,100));panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));
    panel.Controls.Add(new Label {Text=(string)pairs[i*2],Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleLeft,UseMnemonic=false},0,0);
    var c=(Control)pairs[i*2+1];c.Dock=DockStyle.Top;c.Margin=new Padding(0,2,0,0);panel.Controls.Add(c,0,1);t.Controls.Add(panel,i%columns,i/columns);
   }
   return t;
  }
  internal static Button Button(string text,Action action=null,bool secondary=false)
  {
   var b=new Button {Text=text,AutoSize=true,MinimumSize=new Size(110,38),Padding=new Padding(14,6,14,6),FlatStyle=FlatStyle.Flat,BackColor=secondary?Color.FromArgb(229,235,241):Blue,ForeColor=secondary?Ink:Color.White,Font=new Font("Segoe UI",10F,FontStyle.Bold),Margin=new Padding(0,4,10,4),UseMnemonic=false};
   b.FlatAppearance.BorderSize=0;if(action!=null)b.Click+=(s,e)=>Run(b,action);return b;
  }
  internal static FlowLayoutPanel Actions(params Control[] controls)
  { var p=new FlowLayoutPanel {Dock=DockStyle.Fill,AutoSize=true,WrapContents=true,Margin=new Padding(0)};p.Controls.AddRange(controls);return p; }
  internal static TextBox Text(int max=200) { return new TextBox {MaxLength=max,Height=30}; }
  internal static ComboBox Combo()
  {
   var c=new ComboBox {DropDownStyle=ComboBoxStyle.DropDownList,IntegralHeight=false,DropDownHeight=240};
   c.DropDown+=(sender,e)=>{int width=c.Width;foreach(var item in c.Items)width=Math.Max(width,TextRenderer.MeasureText(c.GetItemText(item),c.Font).Width+36);c.DropDownWidth=Math.Min(width,Screen.FromControl(c).WorkingArea.Width-60);};
   return c;
  }
  internal static ComboBox Choices(params string[] values) {var c=Combo();c.Items.AddRange(values);if(values.Length>0)c.SelectedIndex=0;return c;}
  internal static DateTimePicker Date(DateTime? value=null) {return new DateTimePicker {Format=DateTimePickerFormat.Custom,CustomFormat="dd/MM/yyyy",Value=value??DateTime.Today};}
  internal static NumericUpDown Number(decimal value=0,decimal max=1000000000000M,int decimals=0)
  {return new NumericUpDown {Minimum=0,Maximum=max,Value=value,DecimalPlaces=decimals,ThousandsSeparator=true};}
  internal static DataGridView Grid(bool editable=false)
  {
   var g=new DataGridView {Dock=DockStyle.Fill,ReadOnly=!editable,AllowUserToAddRows=false,AllowUserToDeleteRows=false,AllowUserToResizeRows=true,MultiSelect=false,SelectionMode=DataGridViewSelectionMode.FullRowSelect,AutoGenerateColumns=true,AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill,AutoSizeRowsMode=DataGridViewAutoSizeRowsMode.AllCells,BackgroundColor=Color.White,BorderStyle=BorderStyle.FixedSingle,RowHeadersVisible=false,EnableHeadersVisualStyles=false,ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.AutoSize,ScrollBars=ScrollBars.Both,Margin=new Padding(0,4,0,8)};
   g.ColumnHeadersDefaultCellStyle=new DataGridViewCellStyle {BackColor=Color.FromArgb(227,236,244),ForeColor=Ink,Font=new Font("Segoe UI",10F,FontStyle.Bold),Padding=new Padding(5,8,5,8),WrapMode=DataGridViewTriState.True};
   g.DefaultCellStyle=new DataGridViewCellStyle {BackColor=Color.White,ForeColor=Ink,SelectionBackColor=Color.FromArgb(215,232,247),SelectionForeColor=Ink,Padding=new Padding(5),WrapMode=DataGridViewTriState.True};
   g.AlternatingRowsDefaultCellStyle.BackColor=Color.FromArgb(246,248,251);g.GridColor=Color.FromArgb(225,233,240);return g;
  }
  internal static void Bind(DataGridView g,DataTable t,params string[] pairs)
  {
   // Tạo cột tường minh trước khi gắn nguồn dữ liệu, kể cả tab chưa được mở.
   g.DataSource=null;g.AutoGenerateColumns=false;g.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.None;g.Columns.Clear();
   for(int i=0;i<pairs.Length;i+=2)
   {
    var field=t.Columns[pairs[i]];if(field==null)continue;
    DataGridViewColumn c=field.DataType==typeof(bool)?(DataGridViewColumn)new DataGridViewCheckBoxColumn():new DataGridViewTextBoxColumn();
    c.Name=pairs[i];c.DataPropertyName=pairs[i];c.HeaderText=pairs[i+1];c.ValueType=field.DataType;c.MinimumWidth=field.DataType==typeof(DateTime)?110:115;c.Width=c.MinimumWidth;c.FillWeight=115;
    if(field.DataType==typeof(DateTime))c.DefaultCellStyle.Format="dd/MM/yyyy";
    if(field.DataType==typeof(decimal)){c.DefaultCellStyle.Format="N2";c.DefaultCellStyle.Alignment=DataGridViewContentAlignment.MiddleRight;}
    if(c.Name.Contains("Gia")||c.Name.Contains("Tien")||c.Name=="ThanhTien"){c.MinimumWidth=135;c.Width=135;c.DefaultCellStyle.Format="N0";}
    if(c.Name.Contains("Ten")||c.Name=="NoiDung"||c.Name=="HienThi"){c.MinimumWidth=180;c.Width=180;c.FillWeight=180;}
    c.MinimumWidth=Math.Max(c.MinimumWidth,TextRenderer.MeasureText(c.HeaderText,g.ColumnHeadersDefaultCellStyle.Font).Width+24);
    if(c.Name.StartsWith("SoHoaDon") || c.Name=="SoHopDong" || c.Name=="SoPhieu" || c.Name=="TrangThai")c.MinimumWidth=Math.Max(165,c.MinimumWidth);
    c.Width=c.MinimumWidth;g.Columns.Add(c);
   }
   g.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;g.DataSource=t;
  }

  internal static void Bind(ComboBox c,DataTable t,string key,string label)
  {c.DataSource=null;c.DisplayMember=label;c.ValueMember=key;c.DataSource=t;}
  internal static DataRow Row(DataGridView g) {var v=g.CurrentRow==null?null:g.CurrentRow.DataBoundItem as DataRowView;return v==null?null:v.Row;}
  internal static int Id(DataGridView g,string field){var r=Row(g);return r==null?0:Convert.ToInt32(r[field]);}
  internal static int Id(ComboBox c){return c.SelectedValue is int?(int)c.SelectedValue:0;}
  internal static void Select(DataGridView g,string key,int id)
  {foreach(DataGridViewRow r in g.Rows){var v=r.DataBoundItem as DataRowView;if(v!=null && Convert.ToInt32(v[key])==id){foreach(DataGridViewCell cell in r.Cells)if(cell.Visible){g.CurrentCell=cell;break;}r.Selected=true;return;}}}
  internal static void Run(Control owner,Action action)
  {
   try {owner.Cursor=Cursors.WaitCursor;action();}
   catch(SqlException e)
   {
    string m=e.Number==2601||e.Number==2627?"Mã, số chứng từ hoặc biển số đang sử dụng đã tồn tại.":e.Number==547?"Dữ liệu đang được tham chiếu hoặc không còn tồn tại; không thể thay đổi.":e.Number==1205||e.Number==1222?"Dữ liệu đang được xử lý. Vui lòng thử lại.":"Không thực hiện được thao tác SQL.\n"+e.Message;
    MessageBox.Show(owner,m,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
   }
   catch(Exception e) when(e is InvalidOperationException || e is ArgumentException || e is FormatException || e is System.IO.IOException)
   {MessageBox.Show(owner,e.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);}
   finally {owner.Cursor=Cursors.Default;}
  }
  internal static void Done(Control c,string text="Đã lưu thành công.") {MessageBox.Show(c,text,"Hoàn tất",MessageBoxButtons.OK,MessageBoxIcon.Information);}
  internal static bool Confirm(Control c,string text) {return MessageBox.Show(c,text,"Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes;}
  internal static string Money(decimal value){return value.ToString("N0",CultureInfo.GetCultureInfo("vi-VN"))+" đ";}
 }
}
