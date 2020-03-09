using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class DropDownViewModel
    {
        #region List Country

        public List<SelectListItem> ListCountry()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                string sqlstr = "select Id, isnull(Country,'') as Country from ACRF_Country order by Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SelectListItem tempobj = new SelectListItem();
                    tempobj.Text = sdr["Country"].ToString();
                    tempobj.Value = sdr["Id"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();


                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion

        #region List Profile

        public List<SelectListItem> ListProfile()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                string sqlstr = "select ID as Profile, isnull(ProfileName,'') as ProfileName from tbl_Profiles order by ID";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SelectListItem tempobj = new SelectListItem();
                    tempobj.Text = sdr["ProfileName"].ToString();
                    tempobj.Value = sdr["Profile"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();


                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion

        #region List Project

        public List<SelectListItem> ListProject()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                string sqlstr = "select ID as ProjectID, isnull(Project,'') as ProjectName from tbl_Projects order by ID";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SelectListItem tempobj = new SelectListItem();
                    tempobj.Text = sdr["ProjectName"].ToString();
                    tempobj.Value = sdr["ProjectID"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();


                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion

        #region List Employee

        public List<SelectListItem> ListEmployee()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                string sqlstr = "select EmpID,EmpName as Name from [tbl_DCTEmployee] order by ID";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SelectListItem tempobj = new SelectListItem();
                    tempobj.Text = sdr["Name"].ToString();
                    tempobj.Value =sdr["EmpID"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();


                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion


        #region List Status

        public List<SelectListItem> ListStatus()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                string sqlstr = "select ID,Status from [tbl_Status] order by ID";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SelectListItem tempobj = new SelectListItem();
                    tempobj.Text = sdr["Status"].ToString();
                    tempobj.Value = sdr["ID"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();


                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion
        #region List Airline

        public List<SelectListItem> ListAirline()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                string sqlstr = "Select Id, AirlineName From ACRF_Airlines order by AirlineName";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SelectListItem tempobj = new SelectListItem();
                    tempobj.Text = sdr["AirlineName"].ToString();
                    tempobj.Value = sdr["Id"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();


                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion



        #region List Tariff Mode

        public List<SelectListItem> ListTariffMode()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                string sqlstr = "Select Id, TariffMode From ACRF_TariffMode order by TariffMode";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SelectListItem tempobj = new SelectListItem();
                    tempobj.Text = sdr["TariffMode"].ToString();
                    tempobj.Value = sdr["Id"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();

                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion



        #region List Country From Destination

        public List<SelectListItem> ListCountryFromDestination()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                string sqlstr = "Select distinct CountryCode, CountryName From ACRF_DestinationMaster order by CountryName";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SelectListItem tempobj = new SelectListItem();
                    tempobj.Text = sdr["CountryName"].ToString();
                    tempobj.Value = sdr["CountryCode"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();

                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion



        #region List City From Destination

        public List<SelectListItem> ListCityFromDestination(string CountryCode)
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                string sqlstr = "Select CityCode, CityName From ACRF_DestinationMaster ";
                if (CountryCode != "")
                {
                    sqlstr = sqlstr + " where CountryCode=@CountryCode ";
                }
                sqlstr = sqlstr + " order by CityName";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                if (CountryCode != "")
                {
                    cmd.Parameters.AddWithValue("@CountryCode", CountryCode);
                }
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SelectListItem tempobj = new SelectListItem();
                    tempobj.Text = sdr["CityName"].ToString();
                    tempobj.Value = sdr["CityCode"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();

                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion



        #region List City From Origin

        public List<SelectListItem> ListCityFromOrigin(string CountryCode)
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                //string sqlstr = "Select CityCode, CityName From ACRF_DestinationMaster ";
                //if (CountryCode != "")
                //{
                //    sqlstr = sqlstr + " where CountryCode=@CountryCode ";
                //}
                //sqlstr = sqlstr + " order by CityName";

                //var connection = gConnection.Connection();
                //connection.Open();
                //SqlCommand cmd = new SqlCommand(sqlstr, connection);
                //cmd.CommandType = System.Data.CommandType.Text;
                //SqlDataReader sdr = cmd.ExecuteReader();

                //while (sdr.Read())
                //{
                //    SelectListItem tempobj = new SelectListItem();
                //    tempobj.Text = sdr["CityName"].ToString();
                //    tempobj.Value = sdr["CityCode"].ToString();
                //    objList.Add(tempobj);
                //}
                //sdr.Close();

                //connection.Close();

                SelectListItem tempobj = new SelectListItem();
                tempobj.Text = "Mumbai";
                tempobj.Value = "BOM";
                objList.Add(tempobj);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion


        #region List Email

        public List<SelectListItem> ListEmail(int vendorId)
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                string sqlstr = "select Email, isnull(CustomerName,'') as CustomerName from ACRF_CustomerDetails where VendorId=@vendorId order by Email";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@vendorId", vendorId);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    SelectListItem tempobj = new SelectListItem();
                    tempobj.Text = sdr["CustomerName"].ToString();
                    tempobj.Value = sdr["Email"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();


                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion





        #region List Quotation Status

        public List<SelectListItem> ListQuotationStatus()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList.Add(new SelectListItem
                {
                    Text = QuotationType.InProgress,
                    Value = QuotationType.InProgress
                });
                objList.Add(new SelectListItem
                {
                    Text = QuotationType.OnHold,
                    Value = QuotationType.OnHold
                });
                objList.Add(new SelectListItem
                {
                    Text = QuotationType.Completed,
                    Value = QuotationType.Completed
                });
                objList.Add(new SelectListItem
                {
                    Text = QuotationType.Cancelled,
                    Value = QuotationType.Cancelled
                });
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objList;
        }

        #endregion


    }
}