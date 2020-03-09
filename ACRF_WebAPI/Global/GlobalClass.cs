using ACRF_WebAPI.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Global
{
    public class gConnection
    {
        //public static SqlConnection Open()
        //{
        //    //ConnectionStringSettings connSettings = ConfigurationManager.ConnectionStrings["con1"];
        //    //string connString = connSettings.ConnectionString;

        //    //GlobalVariables.gConn = new SqlConnection(connString);
        //    string connString = HttpContext.Current.Session["DbDetails"].ToString();
        //    GlobalVariables.gConn = new SqlConnection(connString);
        //    GlobalVariables.gConn.Open();

        //    return GlobalVariables.gConn;
        //}


        //public static SqlConnection Close()
        //{
        //    GlobalVariables.gConn.Close();
        //    return GlobalVariables.gConn;
        //}




        public static SqlConnection Connection()
        {
            ConnectionStringSettings connSettings = ConfigurationManager.ConnectionStrings["con1"];
            string connString = connSettings.ConnectionString;

            GlobalVariables.gConn = new SqlConnection(connString);
            return GlobalVariables.gConn;
        }

    }




    public static class StandardDateTime
    {
        public static DateTime GetDateTime()
        {
            //DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
            //DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

            return localTime;
        }

    }


}