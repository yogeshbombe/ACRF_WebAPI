using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class CountryViewModel
    {

        #region Create Country

        public string CreateCountry(ACRF_CountryModel objModel)
        {
            string result = "Error on Saving Country!";
            try
            {
                result = CheckIfCountryExists(objModel);
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
                        sqlstr = "insert into ACRF_Country(Country,CreatedBy,CreatedOn) values (@Country,@CreatedBy,@CreatedOn)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Country", objModel.Country);
                        cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Country Added Successfully!";
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



        #region List Country With Pagination

        public Paged_CountryModel ListCountry(int max, int page, string search, string sort_col, string sort_dir)
        {
            Paged_CountryModel objPaged = new Paged_CountryModel();
            List<ACRF_CountryModel> objList = new List<ACRF_CountryModel>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "ACRF_GetCountryByPage";


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
                    ACRF_CountryModel tempobj = new ACRF_CountryModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.Country = sdr["Country"].ToString();
                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    objList.Add(tempobj);
                }
                sdr.Close();
                objPaged.ACRF_CountryModelList = objList;


                sqlstr = "select count(*) as cnt from ACRF_Country where country like @search ";
                cmd.Parameters.Clear();
                cmd.CommandText = sqlstr;
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@search" , '%'+@search+'%');
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



        #region Check If Country Already Exists

        public string CheckIfCountryExists(ACRF_CountryModel objModel)
        {
            string result = "";
            try
            {

                string sqlstr = "Select * from ACRF_Country Where ISNULL(Country,'')=@Country and Isnull(Id,0)!=@Id ";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Country", objModel.Country);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (objModel.Country != "")
                {
                    while (sdr.Read())
                    {
                        result = "Country already exists!";
                    }
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




        #region List Country

        public List<ACRF_CountryModel> ListCountry()
        {
            List<ACRF_CountryModel> objList = new List<ACRF_CountryModel>();
            try
            {
                string sqlstr = "select Id, isnull(country,'') as Country, isnull(CreatedBy,'') as CreatedBy, isnull(createdon,'') as CreatedOn,  "
                    + " isnull(updatedby,'') as updatedby, isnull(updatedon,'') as updatedon from ACRF_Country order by Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_CountryModel tempobj = new ACRF_CountryModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
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



        #region Get One Country

        public ACRF_CountryModel GetOneCountry(int Id)
        {
            ACRF_CountryModel objList = new ACRF_CountryModel();
            try
            {
                string sqlstr = "select Id, isnull(country,'') as Country, isnull(CreatedBy,'') as CreatedBy, isnull(createdon,'') as CreatedOn,  "
                    + " isnull(updatedby,'') as updatedby, isnull(updatedon,'') as updatedon from ACRF_Country where Id=@Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    objList.Id = Convert.ToInt32(sdr["Id"].ToString());
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



        #region Update Country

        public string UpdateCountry(ACRF_CountryModel objModel)
        {
            string result = "Error on Updating Country!";
            try
            {
                result = CheckIfCountryExists(objModel);
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
                        sqlstr = "update ACRF_Country set Country=@Country, UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn where Id=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Country", objModel.Country);
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);
                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Country Updated Successfully!";
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



        #region Delete Country

        public string DeleteCountry(int Id, string CreatedBy)
        {
            string result = "Error on Deleting Country!";
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
                    sqlstr = "insert into ACRF_Country_Log(Id,Country,CreatedBy,CreatedOn) Select Id,Country,@CreatedBy,@CreatedOn from ACRF_Country where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();
                    

                    sqlstr = "delete from ACRF_Country where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();



                    transaction.Commit();
                    connection.Close();
                    result = "Country Deleted Successfully!";
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

    }
}