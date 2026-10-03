using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace eShopping.Data
{
    public static class Db
    {
        public static string ConnectionString
        {
            get
            {
                var setting =
                    ConfigurationManager.ConnectionStrings["eShoppingDB"];

                if (setting == null)
                {
                    throw new InvalidOperationException(
                        "Chưa cấu hình connectionStrings/eShoppingDB.");
                }

                return setting.ConnectionString;
            }
        }

        public static SqlConnection OpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);

            try
            {
                connection.Open();

                // Cần các SET options này khi thao tác bảng có
                // cột tính toán PERSISTED và filtered index.
                using (var command = new SqlCommand(@"
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;", connection))
                {
                    command.ExecuteNonQuery();
                }

                return connection;
            }
            catch
            {
                connection.Dispose();
                throw;
            }
        }

        public static SqlParameter P(
            string name,
            SqlDbType type,
            object value,
            int size = 0)
        {
            var parameter = new SqlParameter(name, type);

            if (size != 0)
                parameter.Size = size;

            if (type == SqlDbType.Decimal)
            {
                parameter.Precision = 18;
                parameter.Scale = 2;
            }

            parameter.Value = value ?? DBNull.Value;
            return parameter;
        }

        public static DataTable Query(
            string sql,
            params SqlParameter[] parameters)
        {
            using (var connection = OpenConnection())
            using (var command = new SqlCommand(sql, connection))
            using (var adapter = new SqlDataAdapter(command))
            {
                if (parameters != null && parameters.Length > 0)
                    command.Parameters.AddRange(parameters);

                var table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }
    }
}