using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Services;

namespace QuanLyCongTyDuLich.Forms
{
    /// <summary>Tiện ích dùng chung cho các Form: Định dạng giao diện hiện đại & xử lý dữ liệu.</summary>
    internal static class FormHelper
    {
        public static void Nap(ComboBox cbo, DataTable dt, string display, string value)
        {
            cbo.DisplayMember = display;
            cbo.ValueMember = value;
            cbo.DataSource = dt;
        }

        public static string Gia(ComboBox cbo)
        {
            return cbo.SelectedValue == null ? "" : cbo.SelectedValue.ToString();
        }

        public static string O(DataGridView dgv, string cot)
        {
            return dgv.CurrentRow == null ? "" : Convert.ToString(dgv.CurrentRow.Cells[cot].Value);
        }

        /// <summary>Hiển thị kết quả; trả về true nếu thành công.</summary>
        public static bool Bao(KetQuaXuLy k)
        {
            MessageBox.Show(k.ThongBao, k.ThanhCong ? "Thông báo" : "Không thực hiện được",
                MessageBoxButtons.OK, k.ThanhCong ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            return k.ThanhCong;
        }

        /// <summary>
        /// Nâng cấp giao diện phẳng hiện đại: màu sắc chuyên nghiệp, nút bấm phẳng, DataGridView đẹp và tự động giãn cột thông minh.
        /// </summary>
        public static void ApDungGiaoDien(Form f)
        {
            f.BackColor = Color.FromArgb(248, 250, 252); // Slate 50
            f.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);

            DuyetControl(f, f is FrmMain);
        }

        private static void DuyetControl(Control parent, bool laFrmMain)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button btn)
                {
                    btn.Cursor = Cursors.Hand;
                    string name = btn.Name.ToLowerInvariant();

                    if (laFrmMain)
                    {
                        if (name == "btnthoat")
                        {
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.FlatAppearance.BorderColor = Color.FromArgb(252, 165, 165);
                            btn.FlatAppearance.BorderSize = 1;
                            btn.BackColor = Color.FromArgb(254, 226, 226);
                            btn.ForeColor = Color.FromArgb(153, 27, 27);
                            btn.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                        }
                        else
                        {
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                            btn.FlatAppearance.BorderSize = 1;
                            btn.BackColor = Color.White;
                            btn.ForeColor = Color.FromArgb(30, 41, 59);
                            btn.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                        }
                    }
                    else
                    {
                        if (name.Contains("dong") || name == "btnthoat")
                        {
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                            btn.FlatAppearance.BorderSize = 1;
                            btn.BackColor = Color.White;
                            btn.ForeColor = Color.FromArgb(71, 85, 105);
                            btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                        }
                        else if (name.Contains("huy") || name.Contains("dongdk"))
                        {
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.FlatAppearance.BorderSize = 0;
                            btn.BackColor = Color.FromArgb(220, 38, 38); // Đỏ nổi bật
                            btn.ForeColor = Color.White;
                            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        }
                        else
                        {
                            // Nút hành động chính (Thêm, Đăng ký, Phân công, Thanh toán...)
                            btn.FlatStyle = FlatStyle.Flat;
                            btn.FlatAppearance.BorderSize = 0;
                            btn.BackColor = Color.FromArgb(37, 99, 235); // Xanh dương hiện đại
                            btn.ForeColor = Color.White;
                            btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        }
                    }
                }
                else if (c is DataGridView dgv)
                {
                    DinhDangLuoi(dgv);
                }
                else if (c is GroupBox grp)
                {
                    grp.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    grp.ForeColor = Color.FromArgb(30, 41, 59);
                    DuyetControl(grp, false);
                }
                else if (c is TabControl tab)
                {
                    foreach (TabPage page in tab.TabPages)
                    {
                        page.BackColor = Color.White;
                        DuyetControl(page, false);
                    }
                }
                else if (c.HasChildren)
                {
                    DuyetControl(c, laFrmMain);
                }
            }
        }

        /// <summary>
        /// Định dạng DataGridView theo chuẩn phẳng: Header xanh navy, hàng so le, padding chuẩn và tự co giãn cột thông minh không bị cắt chữ.
        /// </summary>
        public static void DinhDangLuoi(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(226, 232, 240); // Slate 200
            dgv.BackgroundColor = Color.White;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.RowTemplate.Height = 30;

            // Header Style
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59); // Slate 800
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.25F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Row Style
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(15, 23, 42); // Slate 900
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(37, 99, 235); // Blue 600
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);

            // Alternating Row Style
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252); // Slate 50
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(37, 99, 235);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);

            // Tự động phân tích và căn chỉnh kích thước cột khi dữ liệu nạp xong
            dgv.DataBindingComplete -= Dgv_DataBindingComplete;
            dgv.DataBindingComplete += Dgv_DataBindingComplete;

            if (dgv.Columns.Count > 0)
            {
                ToiUuCot(dgv);
            }
        }

        private static void Dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (sender is DataGridView dgv)
            {
                ToiUuCot(dgv);
            }
        }

        /// <summary>
        /// Phân bổ không gian cột: cột số/mã/ngày/tiền tự tính theo nội dung (AllCells), các cột tên/mô tả tự giãn tối đa (Fill) tránh mất chữ.
        /// </summary>
        public static void ToiUuCot(DataGridView dgv)
        {
            if (dgv.Columns.Count == 0) return;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                string name = col.Name.ToUpperInvariant();

                // 1. Cột Tiền / Số lượng lớn -> Căn phải, định dạng phân cách hàng nghìn N0
                if (name.Contains("GIA") || name.Contains("TIEN") || name.Contains("LUONG") || name.Contains("CONLAI") || name.Contains("DATRA"))
                {
                    col.DefaultCellStyle.Format = "N0";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    col.MinimumWidth = 90;
                }
                // 2. Cột Ngày / Giờ -> Căn giữa, định dạng dd/MM/yyyy
                else if (name.StartsWith("NGAY") || name.Contains("SINH") || name.Contains("THOIGIAN"))
                {
                    if (col.ValueType == typeof(DateTime))
                    {
                        col.DefaultCellStyle.Format = (name.Contains("DANGKY") || name.Contains("THANHTOAN")) ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy";
                    }
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    col.MinimumWidth = 95;
                }
                // 3. Cột Mã, Thứ tự, Số nguyên nhỏ -> Căn giữa, co gọn vừa khít
                else if (name.StartsWith("MA") || name.StartsWith("SO") || name == "STT" || name == "THUTU" || name.Contains("CHANG") || name.Contains("SAO") || name.Contains("DIEMDANHGIA"))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    col.MinimumWidth = 65;
                }
                // 4. Cột Boolean (True/False) -> Căn giữa
                else if (col.ValueType == typeof(bool) || name.StartsWith("CO") || name.StartsWith("DANG") || name.StartsWith("DOI") || name.StartsWith("MUA") || name.StartsWith("DA"))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    col.MinimumWidth = 70;
                }
                // 5. Điện thoại, Trạng thái, Loại -> Căn giữa
                else if (name.Contains("DIENTHOAI") || name.Contains("TRANGTHAI") || name.Contains("LOAI") || name.Contains("GIAYTO"))
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    col.MinimumWidth = 100;
                }
                // 6. Cột văn bản dài (Tên tour, Mô tả, Địa chỉ, Ghi chú, Góp ý...) -> Fill tối đa
                else
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    if (name.Contains("TEN") || name.Contains("MOTA") || name.Contains("GOPY") || name.Contains("NOIDUNG") || name.Contains("DIA"))
                    {
                        col.FillWeight = 220;
                        col.MinimumWidth = 180;
                    }
                    else
                    {
                        col.FillWeight = 100;
                        col.MinimumWidth = 120;
                    }
                }
            }
        }
    }
}
