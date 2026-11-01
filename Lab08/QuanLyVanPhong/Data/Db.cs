using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace QuanLyVanPhong.Data
{
 public static class Db
 {
  public static string ConnectionString { get { return ConfigurationManager.ConnectionStrings["QuanLyVanPhongDB"].ConnectionString; } }
  public static SqlConnection Open()
  {
   var c = new SqlConnection(ConnectionString);
   try
   {
    c.Open();
    using(var cmd = new SqlCommand(@"SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON; SET ANSI_WARNINGS ON; SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON; SET NUMERIC_ROUNDABORT OFF; SET XACT_ABORT ON;",c)) cmd.ExecuteNonQuery();
    return c;
   }
   catch { c.Dispose(); throw; }
  }
  public static SqlParameter P(string name, object value) { return new SqlParameter(name,value ?? DBNull.Value); }
  public static SqlCommand Command(SqlConnection c, SqlTransaction tx, string sql, params SqlParameter[] args)
  {
   var cmd = new SqlCommand(sql,c,tx) { CommandTimeout=30 };
   if(args != null && args.Length>0) cmd.Parameters.AddRange(args);
   return cmd;
  }
  public static DataTable Query(string sql, params SqlParameter[] args)
  { using(var c=Open()) return Query(c,null,sql,args); }
  public static DataTable Query(SqlConnection c,SqlTransaction tx,string sql,params SqlParameter[] args)
  {
   using(var cmd=Command(c,tx,sql,args)) using(var a=new SqlDataAdapter(cmd))
   { var t=new DataTable(); a.Fill(t); return t; }
  }
  public static object Scalar(SqlConnection c,SqlTransaction tx,string sql,params SqlParameter[] args)
  { using(var cmd=Command(c,tx,sql,args)) return cmd.ExecuteScalar(); }
  public static int Execute(SqlConnection c,SqlTransaction tx,string sql,params SqlParameter[] args)
  { using(var cmd=Command(c,tx,sql,args)) return cmd.ExecuteNonQuery(); }
  public static T Transaction<T>(Func<SqlConnection,SqlTransaction,T> action)
  {
   using(var c=Open()) using(var tx=c.BeginTransaction(IsolationLevel.Serializable))
   {
    try { var result=action(c,tx); tx.Commit(); return result; }
    catch { try { tx.Rollback(); } catch(InvalidOperationException) {} throw; }
   }
  }
  public static void Transaction(Action<SqlConnection,SqlTransaction> action)
  { Transaction<bool>((c,tx)=> { action(c,tx); return true; }); }
 }
}
