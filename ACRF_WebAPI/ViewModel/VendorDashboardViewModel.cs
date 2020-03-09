using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class VendorDashboardViewModel
    {
        public QuotationStatusCount GetQuotationStatus(int VendorId)
        {
            QuotationStatusCount objModel = new QuotationStatusCount();
            try
            {
                string sqlstr = "Select Count(*) As Cnt, QuotationStatus From ACRF_Quotation where VendorId=@VendorId Group By QuotationStatus";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if (sdr["QuotationStatus"].ToString() == QuotationType.Cancelled)
                    {
                        objModel.Cancelled = Convert.ToInt32(sdr["Cnt"].ToString());
                    }
                    if (sdr["QuotationStatus"].ToString() == QuotationType.Completed)
                    {
                        objModel.Completed = Convert.ToInt32(sdr["Cnt"].ToString());
                    }
                    if (sdr["QuotationStatus"].ToString() == QuotationType.InProgress)
                    {
                        objModel.InProgress = Convert.ToInt32(sdr["Cnt"].ToString());
                    }
                    if (sdr["QuotationStatus"].ToString() == QuotationType.OnHold)
                    {
                        objModel.OnHold = Convert.ToInt32(sdr["Cnt"].ToString());
                    }
                }
                sdr.Close();

                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return objModel;
        }



        public DisplayChart GetQuotationAmountForLastTweleveMonths(int VendorId)
        {
            DisplayChart objModel = new DisplayChart();
            try
            {
                List<string> CountList = new List<string>();
                List<string> MonList = new List<string>();
                string sqlstr = "Select sum(count) as count, ''''+mon+'''' as mon from ACRFVW_GetLastTweleveMonthQuotationAmount "
                + " where VendorId in (0," + VendorId + ") group by yyyy,mon,mon_number order by mon_number";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    CountList.Add(sdr["count"].ToString());
                    MonList.Add(sdr["mon"].ToString());
                }
                connection.Close();
                objModel.Count = CountList;
                objModel.Text = MonList;
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objModel;
        }



        public DisplayChart GetQuotationAmountForLastTweleveMonthsCompleted(int VendorId)
        {
            DisplayChart objModel = new DisplayChart();
            try
            {
                List<string> CountList = new List<string>();
                List<string> MonList = new List<string>();
                string sqlstr = "Select sum(count) as count, ''''+mon+'''' as mon from ACRFVW_GetLastTweleveMonthQuotationAmountWithStatus "
                + " where VendorId in (0," + VendorId + ") and QuotationStatus in ('','" + QuotationType.Completed + "') group by yyyy,mon,mon_number order by mon_number";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    CountList.Add(sdr["count"].ToString());
                    MonList.Add(sdr["mon"].ToString());
                }
                connection.Close();
                objModel.Count = CountList;
                objModel.Text = MonList;
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objModel;
        }



        public QuotationSentCompletedCount GetQuotationSentCompletedCount(int VendorId)
        {
            QuotationSentCompletedCount objModel = new QuotationSentCompletedCount();
            try
            {
                string sqlstr = "Select Count(*) As Cnt From ACRF_Quotation where VendorId=@VendorId ";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objModel.Sent = Convert.ToInt32(sdr["Cnt"].ToString());
                }
                sdr.Close();


                sqlstr = "Select Count(*) As Cnt From ACRF_Quotation where VendorId=@VendorId and QuotationStatus=@QuotationStatus";
                cmd.Parameters.Clear();
                cmd.Connection = connection;
                cmd.CommandText = sqlstr;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@QuotationStatus", QuotationType.Completed);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objModel.Completed = Convert.ToInt32(sdr["Cnt"].ToString());
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return objModel;
        }



        public List<MonthlySale> GetMonthlySale(int VendorId)
        {
            List<MonthlySale> objList = new List<MonthlySale>();
            try
            {
                string sqlstr = "Select sum(count) as count, mon + '-' + Convert(varchar(10),yyyy) as mon from "
                + " ACRFVW_GetLastTweleveMonthQuotationAmountWithStatus where VendorId in (0,@VendorId)  "
                + " group by yyyy,mon,mon_number order by yyyy desc, mon_number desc";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    MonthlySale tempobj = new MonthlySale();
                    tempobj.MonthName = sdr["mon"].ToString();
                    tempobj.SaleAmount = Convert.ToDecimal(sdr["count"].ToString());

                    objList.Add(tempobj);
                }                
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return objList;
        }



        public DisplayMultiChart GetQuotationCountForLastTweleveMonthsCompleted(int VendorId)
        {
            DisplayMultiChart objModel = new DisplayMultiChart();
            try
            {
                List<string> CountList = new List<string>();
                List<string> MonList = new List<string>();
                List<string> MonList1 = new List<string>();
                string sqlstr = "Select sum(count) as count, ''''+mon+'''' as mon from ACRFVW_GetLastTweleveMonthQuotationCountWithStatus "
                + " where VendorId in (0," + VendorId + ") and QuotationStatus in ('','" + QuotationType.Completed + "') group by yyyy,mon,mon_number order by mon_number";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    CountList.Add(sdr["count"].ToString());
                    MonList.Add(sdr["mon"].ToString());
                }
                sdr.Close();
                objModel.Count1 = CountList;
                objModel.Text = MonList;


                sqlstr = "Select sum(count) as count, ''''+mon+'''' as mon from ACRFVW_GetLastTweleveMonthQuotationCountWithStatus "
                + " where VendorId in (0," + VendorId + ") group by yyyy,mon,mon_number order by mon_number";
                cmd.Parameters.Clear();
                cmd.Connection = connection;
                cmd.CommandText = sqlstr;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    MonList1.Add(sdr["count"].ToString());
                }
                sdr.Close();

                objModel.Count2 = MonList1;
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objModel;
        }



    }
}