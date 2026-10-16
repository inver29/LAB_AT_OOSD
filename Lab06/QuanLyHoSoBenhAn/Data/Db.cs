using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyHoSoBenhAn.Data
{
    public static class Db
    {
        public static string ConnectionString
        {
            get
            {
                var config = ConfigurationManager.ConnectionStrings["QuanLyHoSoBenhAnDB"];
                if (config == null) throw new InvalidOperationException("Thiếu kết nối QuanLyHoSoBenhAnDB trong App.config.");
                return config.ConnectionString;
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
                    cmd.ExecuteNonQuery();
                return connection;
            }
            catch { connection.Dispose(); throw; }
        }

        public static SqlParameter P(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        public static SqlCommand Command(SqlConnection c, SqlTransaction tx, string sql, params SqlParameter[] args)
        {
            var cmd = new SqlCommand(sql, c, tx) { CommandTimeout = 15 };
            cmd.Parameters.AddRange(args);
            return cmd;
        }

        public static object Scalar(SqlConnection c, SqlTransaction tx, string sql, params SqlParameter[] args)
        { using (var cmd = Command(c, tx, sql, args)) return cmd.ExecuteScalar(); }

        public static int Execute(SqlConnection c, SqlTransaction tx, string sql, params SqlParameter[] args)
        { using (var cmd = Command(c, tx, sql, args)) return cmd.ExecuteNonQuery(); }

        public static DataTable Query(string sql, params SqlParameter[] args)
        {
            using (var c = Open()) return Query(c, null, sql, args);
        }

        public static DataTable Query(SqlConnection c, SqlTransaction tx, string sql, params SqlParameter[] args)
        {
            using (var cmd = Command(c, tx, sql, args))
            using (var adapter = new SqlDataAdapter(cmd))
            {
                var table = new DataTable(); adapter.Fill(table); return table;
            }
        }

        // Mọi thay đổi trong action thành công cùng nhau hoặc hủy toàn bộ.
        public static T Transaction<T>(Func<SqlConnection, SqlTransaction, T> action)
        {
            using (var c = Open())
            using (var tx = c.BeginTransaction(IsolationLevel.Serializable))
            {
                try { T result = action(c, tx); tx.Commit(); return result; }
                catch { try { tx.Rollback(); } catch (InvalidOperationException) { } throw; }
            }
        }
    }
}
