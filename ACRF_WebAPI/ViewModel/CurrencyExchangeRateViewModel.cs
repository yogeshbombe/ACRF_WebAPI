using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class CurrencyExchangeRateViewModel
    {
        #region Create Currency Exchange Rate

        public string CreateCurrencyExchangeRate(ACRF_CurrencyExchangeRateModel objModel)
        {
            string result = "Error on Saving Currency Exchange Rate!";
            try
            {
                result = CheckIfCurrencyExchangeRateExists(objModel);
                if (result == "")
                {

                    var connection = gConnection.Connection();
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = connection;
                    try
                    {
                        string sqlstr = "";
                        sqlstr = "insert into ACRF_CurrencyExchangeRate(FCountryId,FCurrency,FCurrencyCode,Unit,ImportRate,ExportRate,CreatedBy,CreatedOn)"
                            + " values (@FCountryId,@FCurrency,@FCurrencyCode,@Unit,@ImportRate,@ExportRate,@CreatedBy,@CreatedOn)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@FCountryId", objModel.FCountryId);
                        cmd.Parameters.AddWithValue("@FCurrency", objModel.FCurrency);
                        cmd.Parameters.AddWithValue("@FCurrencyCode", objModel.FCurrencyCode);
                        cmd.Parameters.AddWithValue("@Unit", objModel.Unit);
                        cmd.Parameters.AddWithValue("@ImportRate", objModel.ImportRate);
                        cmd.Parameters.AddWithValue("@ExportRate", objModel.ExportRate);
                        cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Currency Exchange Rate Added Successfully!";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        connection.Close();
                        Global.ErrorHandlerClass.LogError(ex);
                        result = ex.Message;
                    }
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion



        #region Update Currency Exchange Rate

        public string UpdateCurrencyExchangeRate(ACRF_CurrencyExchangeRateModel objModel)
        {
            string result = "Error on Updating Currency Exchange Rate!";
            try
            {
                result = CheckIfCurrencyExchangeRateExists(objModel);
                if (result == "")
                {

                    var connection = gConnection.Connection();
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.Connection = connection;
                    try
                    {
                        string sqlstr = "";
                        sqlstr = "update ACRF_CurrencyExchangeRate set FCountryId=@FCountryId,FCurrency=@FCurrency"
                        + ", FCurrencyCode=@FCurrencyCode, Unit=@Unit, ImportRate=@ImportRate, ExportRate=@ExportRate,"
                        + " UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn where Id=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@FCountryId", objModel.FCountryId);
                        cmd.Parameters.AddWithValue("@FCurrency", objModel.FCurrency);
                        cmd.Parameters.AddWithValue("@FCurrencyCode", objModel.FCurrencyCode);
                        cmd.Parameters.AddWithValue("@Unit", objModel.Unit);
                        cmd.Parameters.AddWithValue("@ImportRate", objModel.ImportRate);
                        cmd.Parameters.AddWithValue("@ExportRate", objModel.ExportRate);
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);
                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Currency Exchange Rate Updated Successfully!";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        connection.Close();
                        Global.ErrorHandlerClass.LogError(ex);
                        result = ex.Message;
                    }
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion



        #region Delete Currency Exchange Rate

        public string DeleteCurrencyExchangeRate(int Id, string CreatedBy)
        {
            string result = "Error on Deleting Currency Exchange Rate!";
            try
            {
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction();
                cmd.Transaction = transaction;
                cmd.Connection = connection;
                try
                {
                    string sqlstr = "";
                    sqlstr = "insert into ACRF_CurrencyExchangeRate_Log(Id,FCountryId,FCurrency,FCurrencyCode,Unit,ImportRate,ExportRate,CreatedBy,CreatedOn)"
                        + " select Id,FCountryId,FCurrency,FCurrencyCode,Unit,ImportRate,ExportRate,@CreatedBy,@CreatedOn from ACRF_CurrencyExchangeRate where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();


                    sqlstr = "delete from ACRF_CurrencyExchangeRate where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();


                    transaction.Commit();
                    connection.Close();
                    result = "Currency Exchange Rate Deleted Successfully!";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    connection.Close();
                    Global.ErrorHandlerClass.LogError(ex);
                    result = ex.Message;
                }

            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion



        #region List Currency Exchange Rate

        public List<ACRF_CurrencyExchangeRateModel> ListCurrencyExchangeRate()
        {
            List<ACRF_CurrencyExchangeRateModel> objList = new List<ACRF_CurrencyExchangeRateModel>();
            try
            {
                string sqlstr = "Select R.Id, R.FCountryId, R.FCurrency, R.FCurrencyCode, R.Unit,  R.ImportRate, R.ExportRate, R.CreatedBy, R.CreatedOn, "
                + " C.Country From ACRF_CurrencyExchangeRate R, ACRF_Country C where R.FCountryId=C.Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_CurrencyExchangeRateModel tempobj = new ACRF_CurrencyExchangeRateModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.FCountryId = Convert.ToInt32(sdr["FCountryId"].ToString());
                    tempobj.FCurrency = sdr["FCurrency"].ToString();
                    tempobj.FCurrencyCode = sdr["FCurrencyCode"].ToString();
                    tempobj.Unit = Convert.ToInt32(sdr["Unit"].ToString());
                    tempobj.ImportRate = Convert.ToDecimal(sdr["ImportRate"].ToString());
                    tempobj.ExportRate = Convert.ToDecimal(sdr["ExportRate"].ToString());
                    tempobj.Country = sdr["Country"].ToString();
                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
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



        #region Get One Currency Exchange Rate

        public ACRF_CurrencyExchangeRateModel OneCurrencyExchangeRate(int Id)
        {
            ACRF_CurrencyExchangeRateModel objList = new ACRF_CurrencyExchangeRateModel();
            try
            {
                string sqlstr = "Select R.Id, R.FCountryId, R.FCurrency, R.FCurrencyCode, R.Unit,  R.ImportRate, R.ExportRate, R.CreatedBy, R.CreatedOn, "
                + " C.Country From ACRF_CurrencyExchangeRate R, ACRF_Country C where R.FCountryId=C.Id and R.Id=@Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    objList.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objList.FCountryId = Convert.ToInt32(sdr["FCountryId"].ToString());
                    objList.FCurrency = sdr["FCurrency"].ToString();
                    objList.FCurrencyCode = sdr["FCurrencyCode"].ToString();
                    objList.Unit = Convert.ToInt32(sdr["Unit"].ToString());
                    objList.ImportRate = Convert.ToDecimal(sdr["ImportRate"].ToString());
                    objList.ExportRate = Convert.ToDecimal(sdr["ExportRate"].ToString());
                    objList.Country = sdr["Country"].ToString();
                    objList.CreatedBy = sdr["CreatedBy"].ToString();
                    objList.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
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



        #region Check If CurrencyExchangeRate Already Exists

        public string CheckIfCurrencyExchangeRateExists(ACRF_CurrencyExchangeRateModel objModel)
        {
            string result = "";
            try
            {

                string sqlstr = "Select * from ACRF_CurrencyExchangeRate Where ISNULL(FCountryId,0)=@FCountryId and "
                + " ISNULL(FCurrency,'')=@FCurrency and Isnull(FCurrencyCode,'') as FCurrencyCode and Isnull(Id,0)!=@Id ";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@FCountryId", objModel.FCountryId);
                cmd.Parameters.AddWithValue("@FCurrency", objModel.FCurrency);
                cmd.Parameters.AddWithValue("@FCurrencyCode", objModel.FCurrencyCode);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result = "Currency Exchange Rate already exists!";
                }
                sdr.Close();

                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return result;
        }

        #endregion




        #region List Currency Exchange Rate With Pagination

        public Paged_ACRF_CurrencyExchangeRateModel ListCurrencyExchangeRateWithPagination(int max, int page, string search, string sort_col, string sort_dir)
        {
            Paged_ACRF_CurrencyExchangeRateModel objPaged = new Paged_ACRF_CurrencyExchangeRateModel();
            List<ACRF_CurrencyExchangeRateModel> objList = new List<ACRF_CurrencyExchangeRateModel>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "ACRF_GetCurrencyExchangeRateByPage";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startRowIndex", startIndex);
                cmd.Parameters.AddWithValue("@pageSize", max);
                cmd.Parameters.AddWithValue("@search", search);
                cmd.Parameters.AddWithValue("@sort_col", sort_col);
                cmd.Parameters.AddWithValue("@sort_dir", sort_dir);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_CurrencyExchangeRateModel tempobj = new ACRF_CurrencyExchangeRateModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.FCountryId = Convert.ToInt32(sdr["FCountryId"].ToString());
                    tempobj.FCurrency = sdr["FCurrency"].ToString();
                    tempobj.FCurrencyCode = sdr["FCurrencyCode"].ToString();
                    tempobj.Unit = Convert.ToInt32(sdr["Unit"].ToString());
                    tempobj.ImportRate = Convert.ToDecimal(sdr["ImportRate"].ToString());
                    tempobj.ExportRate = Convert.ToDecimal(sdr["ExportRate"].ToString());
                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    objList.Add(tempobj);
                }
                sdr.Close();
                objPaged.ACRF_CurrencyExchangeRateModelList = objList;


                sqlstr = "select count(*) as cnt from ACRF_CurrencyExchangeRate where FCurrency like @search ";
                cmd.Parameters.Clear();
                cmd.CommandText = sqlstr;
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@search", '%' + @search + '%');
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objPaged.PageCount = Convert.ToInt32(sdr["cnt"].ToString());
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objPaged;
        }

        #endregion




    }
}