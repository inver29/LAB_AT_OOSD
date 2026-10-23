using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyNhanSu.Forms
{
    internal static class FormHelper
    {
        public static readonly Color Primary = Color.FromArgb(55, 103, 145);
        public static readonly Color Ink = Color.FromArgb(37, 55, 74);

        public static void Style(Form form)
        {
            form.Font = new Font("Segoe UI", 10F);
            form.BackColor = Color.FromArgb(244, 247, 250);
            form.ForeColor = Ink;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ClientSize = new Size(1200, 790);
            form.MinimumSize = new Size(1050, 680);
            StyleChildren(form);
        }

        private static void StyleChildren(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                var label = control as Label;
                if (label != null)
                {
                    label.UseMnemonic = false;
                }

                var button = control as Button;
                if (button != null)
                {
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderSize = 0;
                    button.BackColor = Primary;
                    button.ForeColor = Color.White;
                    button.Cursor = Cursors.Hand;
                    button.UseVisualStyleBackColor = false;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 86, 124);
                    button.FlatAppearance.MouseDownBackColor = Color.FromArgb(33, 69, 101);
                    button.Height = 38;
                    button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

                    Color normalBackground = button.BackColor, normalForeground = button.ForeColor;
                    button.EnabledChanged += (s, e) =>
                    {
                        if (!button.Enabled)
                        {
                            normalBackground = button.BackColor;
                            normalForeground = button.ForeColor;
                            button.BackColor = Color.FromArgb(229, 234, 240);
                            button.ForeColor = Color.FromArgb(105, 119, 134);
                        }
                        else
                        {
                            button.BackColor = normalBackground;
                            button.ForeColor = normalForeground;
                        }
                    };
                }

                var textBox = control as TextBox;
                if (textBox != null)
                {
                    textBox.BackColor = textBox.ReadOnly ? Color.FromArgb(237, 241, 246) : Color.White;
                    textBox.ForeColor = Ink;
                    textBox.ReadOnlyChanged += (s, e) => textBox.BackColor = textBox.ReadOnly ? Color.FromArgb(237, 241, 246) : Color.White;
                }

                var grid = control as DataGridView;
                if (grid != null)
                {
                    grid.BackgroundColor = Color.White;
                    grid.BorderStyle = BorderStyle.None;
                    grid.RowHeadersVisible = false;
                    grid.ReadOnly = true;
                    grid.AllowUserToAddRows = false;
                    grid.AllowUserToDeleteRows = false;
                    grid.MultiSelect = false;
                    grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    grid.ScrollBars = ScrollBars.Both;
                    grid.EnableHeadersVisualStyles = false;
                    grid.ColumnHeadersHeight = 38;
                    grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(228, 236, 245);
                    grid.ColumnHeadersDefaultCellStyle.ForeColor = Ink;
                    grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    grid.DefaultCellStyle.BackColor = Color.White;
                    grid.DefaultCellStyle.ForeColor = Ink;
                    grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(216, 232, 247);
                    grid.DefaultCellStyle.SelectionForeColor = Ink;
                    grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 248, 252);
                    grid.DefaultCellStyle.Padding = new Padding(4);
                    grid.RowTemplate.Height = 33;
                    grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    grid.GridColor = Color.FromArgb(231, 237, 241);
                    grid.DataBindingComplete += (s, e) => FormatColumns(grid);
                }

                StyleChildren(control);
            }
        }

        private static void FormatColumns(DataGridView grid)
        {
            string[] dates = { "NgaySinh", "NgayQD", "NgayKy", "TuNgay", "DenNgay", "NgayLap", "NgayPhuTroi", "Ngay", "HDTuNgay", "HDDenNgay", "ThuViecTuNgay", "ThuViecDenNgay", "KyApDung" };
            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.MinimumWidth = 75;
                if (Array.IndexOf(dates, col.Name) >= 0)
                {
                    if (col.Name == "KyApDung")
                        col.DefaultCellStyle.Format = "MM/yyyy";
                    else
                        col.DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                else if (col.ValueType == typeof(DateTime))
                {
                    col.DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                if (col.Name.Contains("Tien") || col.Name.Contains("Luong") || col.Name.Contains("PhuCap") || col.Name == "ThucLinh" || (col.Name.Contains("Tru") && col.ValueType == typeof(decimal)))
                {
                    col.DefaultCellStyle.Format = "N0";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        public static void Columns(DataGridView grid, params string[] fields)
        {
            foreach (DataGridViewColumn col in grid.Columns) col.Visible = false;
            for (int i = 0; i < fields.Length; i += 2)
            {
                var column = grid.Columns[fields[i]];
                if (column == null) continue;
                column.Visible = true;
                column.HeaderText = fields[i + 1];
                column.DisplayIndex = i / 2;

                // Thiết lập kích thước tối thiểu và trọng số để không bị co chữ thành "..." khi ở chế độ cửa sổ
                if (column.Name.Contains("Ma") || column.Name == "STT" || column.Name == "Phai" || column.Name == "GioiTinh")
                {
                    column.MinimumWidth = 65;
                    column.FillWeight = 60;
                }
                else if (column.Name == "HoTen" || column.Name == "TenPhongBan" || column.Name == "NoiDung" || column.Name == "TenCa" || column.Name == "TenChucVu")
                {
                    column.MinimumWidth = 140;
                    column.FillWeight = 160;
                }
                else if (column.Name.Contains("Ngay") || column.Name == "KyApDung" || column.Name.Contains("Han"))
                {
                    column.MinimumWidth = 95;
                    column.FillWeight = 95;
                }
                else if (column.Name.Contains("Luong") || column.Name.Contains("Tien") || column.Name.Contains("PhuCap") || column.Name == "ThucLinh" || column.Name.Contains("ThuNhap") || column.Name.Contains("KhauTru"))
                {
                    column.MinimumWidth = 100;
                    column.FillWeight = 100;
                }
                else
                {
                    column.MinimumWidth = 85;
                    column.FillWeight = 90;
                }
            }
            if (grid.Rows.Count > 0 && grid.CurrentCell == null && fields.Length > 0 && grid.Columns[fields[0]] != null)
                grid.CurrentCell = grid.Rows[0].Cells[fields[0]];
        }

        public static Panel CreateField(string labelText, Control control, int labelHeight = 22)
        {
            var pnl = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0, 2, 12, 4) };
            var lbl = new Label {
                Text = labelText,
                Dock = DockStyle.Top,
                Height = labelHeight,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Ink,
                UseMnemonic = false
            };
            control.Dock = DockStyle.Fill;
            pnl.Controls.Add(control);
            pnl.Controls.Add(lbl);
            return pnl;
        }

        public static DataRow Row(DataGridView grid)
        {
            return (grid.CurrentRow == null ? null : grid.CurrentRow.DataBoundItem as DataRowView)?.Row;
        }

        public static int Id(ComboBox box)
        {
            return box.SelectedValue is int ? (int)box.SelectedValue : 0;
        }

        public static void Bind(ComboBox box, DataTable table, string value, string display)
        {
            box.DataSource = null;
            box.ValueMember = value;
            box.DisplayMember = display;
            box.DataSource = table;
        }

        public static string Money(decimal value)
        {
            return value.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + " đ";
        }

        public static void Run(Control owner, Action action)
        {
            try
            {
                owner.Cursor = Cursors.WaitCursor;
                action();
            }
            catch (SqlException ex)
            {
                string message = ex.Number == 2601 || ex.Number == 2627
                    ? "Dữ liệu đã tồn tại (trùng khóa hoặc số chứng từ). Vui lòng kiểm tra lại."
                    : ex.Number == -2
                    ? "Kết nối cơ sở dữ liệu hết thời gian chờ. Vui lòng thử lại."
                    : ex.Number == 1205
                    ? "Dữ liệu đang được xử lý ở giao dịch khác (Deadlock/Lock timeout). Vui lòng thử lại."
                    : "Không thực hiện được thao tác SQL:\n" + ex.Message;
                MessageBox.Show(owner, message, "Thông báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException || ex is FormatException)
            {
                MessageBox.Show(owner, ex.Message, "Thông báo nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                owner.Cursor = Cursors.Default;
            }
        }

        public static void Success(Control owner, string message)
        {
            MessageBox.Show(owner, message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SelectId(DataGridView grid, string column, int id)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (Convert.ToInt32(row.Cells[column].Value) == id)
                {
                    grid.CurrentCell = row.Cells[column];
                    row.Selected = true;
                    return;
                }
            }
        }
    }
}
