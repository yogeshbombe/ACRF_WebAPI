using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class TariffModeViewModel
    {

        #region Create Tariff Mode

        public string CreateTariffMode(ACRF_TariffModeModel objModel)
        {
            string result = "Error on Saving Tariff Mode!";
            try
            {
                result = CheckIfTariffModeExists(objModel);
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
                        sqlstr = "insert into ACRF_TariffMode(TariffMode,CreatedBy,CreatedOn) values (@TariffMode,@CreatedBy,@CreatedOn)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@TariffMode", objModel.TariffMode);
                        cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();
                        
                        transaction.Commit();
                        connection.Close();
                        result = "Tariff Mode Added Successfully!";
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

        

        #region Update Tariff Mode

        public string UpdateTariffMode(ACRF_TariffModeModel objModel)
        {
            string result = "Error on Updating Tariff Mode!";
            try
            {
                result = CheckIfTariffModeExists(objModel);
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
                        sqlstr = "update ACRF_TariffMode set TariffMode=@TariffMode, UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn where Id=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@TariffMode", objModel.TariffMode);
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);
                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Tariff Mode Updated Successfully!";
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


        
        #region Delete Tariff Mode

        public string DeleteTariffMode(int Id, string CreatedBy)
        {
            string result = "Error on Deleting Tariff Mode!";
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
                    sqlstr = "insert into ACRF_TariffMode_Log(Id,TariffMode,CreatedBy,CreatedOn) Select Id, " 
                     + " TariffMode,@CreatedBy,@CreatedOn from ACRF_TariffMode where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();


                    sqlstr = "delete from ACRF_TariffMode where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();



                    transaction.Commit();
                    connection.Close();
                    result = "Tariff Mode Deleted Successfully!";
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

        

        #region List Tariff Mode

        public List<ACRF_TariffModeModel> ListTariffMode()
        {
            List<ACRF_TariffModeModel> objList = new List<ACRF_TariffModeModel>();
            try
            {
                string sqlstr = "select Id, isnull(TariffMode,'') as TariffMode, isnull(CreatedBy,'') as CreatedBy, isnull(createdon,'') as CreatedOn,  "
                    + " isnull(updatedby,'') as updatedby, isnull(updatedon,'') as updatedon from ACRF_TariffMode order by Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_TariffModeModel tempobj = new ACRF_TariffModeModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.TariffMode = sdr["TariffMode"].ToString();
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


        
        #region Get One Tariff Mode

        public ACRF_TariffModeModel GetOneTariffMode(int Id)
        {
            ACRF_TariffModeModel objList = new ACRF_TariffModeModel();
            try
            {
                string sqlstr = "select Id, isnull(TariffMode,'') as TariffMode, isnull(CreatedBy,'') as CreatedBy, isnull(createdon,'') as CreatedOn,  "
                    + " isnull(updatedby,'') as updatedby, isnull(updatedon,'') as updatedon from ACRF_TariffMode where Id=@Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    objList.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objList.TariffMode = sdr["TariffMode"].ToString();
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




        #region List Tariff Mode With Pagination

        public Paged_ACRF_TariffModeModel ListTariffMode(int max, int page, string search, string sort_col, string sort_dir)
        {
            Paged_ACRF_TariffModeModel objPaged = new Paged_ACRF_TariffModeModel();
            List<ACRF_TariffModeModel> objList = new List<ACRF_TariffModeModel>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "ACRF_GetTariffModeByPage";

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
                    ACRF_TariffModeModel tempobj = new ACRF_TariffModeModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.TariffMode = sdr["TariffMode"].ToString();
                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    objList.Add(tempobj);
                }
                sdr.Close();
                objPaged.ACRF_TariffModeModelList = objList;


                sqlstr = "select count(*) as cnt from ACRF_TariffMode where TariffMode like @search ";
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




        #region Check If Tariff Mode Already Exists

        public string CheckIfTariffModeExists(ACRF_TariffModeModel objModel)
        {
            string result = "";
            try
            {

                string sqlstr = "Select * from ACRF_TariffMode Where ISNULL(TariffMode,'')=@TariffMode and Isnull(Id,0)!=@Id ";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@TariffMode", objModel.TariffMode);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (objModel.TariffMode != "")
                {
                    while (sdr.Read())
                    {
                        result = "Tariff Mode already exists!";
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


    }
}