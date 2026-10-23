using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyNhanSu.Data
{
    public static class Db
    {
        public static string ConnectionString
        {
            get
            {
                var config = ConfigurationManager.ConnectionStrings["QuanLyNhanSuDB"];
                if (config != null && !string.IsNullOrWhiteSpace(config.ConnectionString))
                    return config.ConnectionString;

                // Dự phòng khi chạy trong môi trường test/PowerShell host: Đọc từ QuanLyNhanSu.exe.config
                string appDir = AppDomain.CurrentDomain.BaseDirectory;
                string exeConfig = System.IO.Path.Combine(appDir, "QuanLyNhanSu.exe.config");
                if (!System.IO.File.Exists(exeConfig))
                {
                    exeConfig = @"C:\Study\LAB_AT_OOSD\Lab07\QuanLyNhanSu\bin\Debug\QuanLyNhanSu.exe.config";
                }

                if (System.IO.File.Exists(exeConfig))
                {
                    var map = new ExeConfigurationFileMap { ExeConfigFilename = exeConfig };
                    var cfg = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                    if (cfg.ConnectionStrings.ConnectionStrings["QuanLyNhanSuDB"] != null)
                        return cfg.ConnectionStrings.ConnectionStrings["QuanLyNhanSuDB"].ConnectionString;
                }

                return @"Data Source=.\MSSQLSERVER08;Initial Catalog=QuanLyNhanSu_Bai07;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5";
            }
        }

        public static SqlConnection Open()
        {
            var connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                using (var cmd = new SqlCommand(@"SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON; SET ANSI_WARNINGS ON; SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON; SET NUMERIC_ROUNDABORT OFF; SET XACT_ABORT ON;", connection))
                {
                    cmd.ExecuteNonQuery();
                }
                return connection;
            }
            catch
            {
                connection.Dispose();
                throw;
            }
        }

        public static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        public static SqlCommand Command(SqlConnection c, SqlTransaction tx, string sql, params SqlParameter[] args)
        {
            var cmd = new SqlCommand(sql, c, tx) { CommandTimeout = 30 };
            if (args != null && args.Length > 0)
            {
                cmd.Parameters.AddRange(args);
            }
            return cmd;
        }

        public static object Scalar(SqlConnection c, SqlTransaction tx, string sql, params SqlParameter[] args)
        {
            using (var cmd = Command(c, tx, sql, args))
                return cmd.ExecuteScalar();
        }

        public static int Execute(SqlConnection c, SqlTransaction tx, string sql, params SqlParameter[] args)
        {
            using (var cmd = Command(c, tx, sql, args))
                return cmd.ExecuteNonQuery();
        }

        public static DataTable Query(string sql, params SqlParameter[] args)
        {
            using (var c = Open())
                return Query(c, null, sql, args);
        }

        public static DataTable Query(SqlConnection c, SqlTransaction tx, string sql, params SqlParameter[] args)
        {
            using (var cmd = Command(c, tx, sql, args))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public static T Transaction<T>(Func<SqlConnection, SqlTransaction, T> action)
        {
            using (var c = Open())
            using (var tx = c.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    T result = action(c, tx);
                    tx.Commit();
                    return result;
                }
                catch
                {
                    try { tx.Rollback(); } catch (InvalidOperationException) { }
                    throw;
                }
            }
        }

        public static void Transaction(Action<SqlConnection, SqlTransaction> action)
        {
            Transaction<bool>((c, tx) =>
            {
                action(c, tx);
                return true;
            });
        }

        /// <summary>
        /// Lấy khóa độc quyền theo kỳ lương dạng Payroll:yyyyMM (LockOwner=Transaction).
        /// Chặn các cập nhật đồng thời lên kỳ lương tương ứng.
        /// </summary>
        public static void AcquirePayrollLock(SqlConnection c, SqlTransaction tx, int year, int month, int timeoutSeconds = 5)
        {
            string resource = $"Payroll:{year:D4}{month:D2}";
            using (var cmd = new SqlCommand("sp_getapplock", c, tx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Resource", resource);
                cmd.Parameters.AddWithValue("@LockMode", "Exclusive");
                cmd.Parameters.AddWithValue("@LockOwner", "Transaction");
                cmd.Parameters.AddWithValue("@LockTimeout", timeoutSeconds * 1000);

                var returnParam = new SqlParameter("@ReturnVal", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(returnParam);

                cmd.ExecuteNonQuery();
                int retCode = (int)returnParam.Value;
                if (retCode < 0)
                {
                    throw new InvalidOperationException($"Không thể lấy khóa kỳ lương {month:D2}/{year} (mã lỗi: {retCode}). Kỳ này đang được thao tác bởi một phiên khác.");
                }
            }
        }

        /// <summary>
        /// Kiểm tra kỳ lương đã chốt chưa. Nếu đã chốt thì ném ngoại lệ chặn sửa đổi.
        /// </summary>
        public static void EnsurePayrollNotClosed(SqlConnection c, SqlTransaction tx, int year, int month)
        {
            string sql = "SELECT COUNT(*) FROM BangLuong WITH (UPDLOCK, HOLDLOCK) WHERE Thang = @Thang AND Nam = @Nam AND TrangThai = N'Đã chốt'";
            int count = Convert.ToInt32(Scalar(c, tx, sql, P("@Thang", month), P("@Nam", year)));
            if (count > 0)
            {
                throw new InvalidOperationException($"Kỳ lương {month:D2}/{year} đã chốt. Không được phép chỉnh sửa dữ liệu của kỳ này!");
            }
        }

        /// <summary>
        /// Kiểm tra kỳ theo một ngày cụ thể có bị chốt chưa.
        /// </summary>
        public static void EnsureDateNotInClosedPayroll(SqlConnection c, SqlTransaction tx, DateTime date)
        {
            EnsurePayrollNotClosed(c, tx, date.Year, date.Month);
        }
    }
}
