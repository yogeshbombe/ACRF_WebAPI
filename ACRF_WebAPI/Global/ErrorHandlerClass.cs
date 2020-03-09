using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace ACRF_WebAPI.Global
{
    public class ErrorHandlerClass
    {
        public static void LogError(Exception ex)
        {

            ConnectionStringSettings connSettings = ConfigurationManager.ConnectionStrings["con1"];
            string connString = connSettings.ConnectionString;

            SqlConnection con = new SqlConnection(connString);
            try
            {
                con.Open();
                string sqlstr = "insert into ACRF_exception_error(stack_trace, exception_message , inner_exception , created_on , created_by)values(";
                sqlstr = sqlstr + "@StackTrace,@ExceptionMessage,@InnerException,@created_on,@created_by)";
                SqlCommand cmd = new SqlCommand(sqlstr, con);
                //cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StackTrace", ex.StackTrace);
                cmd.Parameters.AddWithValue("@ExceptionMessage", ex.Message);
                cmd.Parameters.AddWithValue("@InnerException", ex.InnerException != null
                    ? ex.InnerException.Message
                    : "");
                cmd.Parameters.AddWithValue("@created_on", DateTime.Now);
                cmd.Parameters.AddWithValue("@created_by", "");
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                con.Close();
            }
        }
    }

}