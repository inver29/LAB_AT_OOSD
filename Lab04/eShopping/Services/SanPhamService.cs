using System;
using System.Data;
using eShopping.Data;

namespace eShopping.Services
{
    public class SanPhamService
    {
        private readonly Repository _repository = new Repository();

        public DataTable LayNhom()
        {
            var table = _repository.LayNhomSanPham();

            var row = table.NewRow();
            row["MaNhom"] = Guid.Empty;
            row["TenNhom"] = "Tất cả nhóm";
            table.Rows.InsertAt(row, 0);

            return table;
        }

        public DataTable TimKiem(string tuKhoa, Guid? maNhom)
        {
            if ((tuKhoa ?? "").Trim().Length > 200)
                throw new InvalidOperationException("Từ khóa quá dài.");

            return _repository.LayDanhSachSanPham(tuKhoa, maNhom);
        }
    }
}