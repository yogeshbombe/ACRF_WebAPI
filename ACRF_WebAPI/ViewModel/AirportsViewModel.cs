using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class AirportsViewModel
    {

        #region Create Airports

        public string CreateAirports(ACRF_AirportsModel objModel)
        {
            string result = "Error on Saving Airports!";
            try
            {
                result = CheckIfAirportsExists(objModel);
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
                        sqlstr = "insert into ACRF_Airports(ShortCode,Airports,CountryId,CreatedBy,CreatedOn) "
                         + " values (@ShortCode,@Airports,@CountryId,@CreatedBy,@CreatedOn)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@ShortCode", objModel.ShortCode);
                        cmd.Parameters.AddWithValue("@Airports", objModel.Airports);
                        cmd.Parameters.AddWithValue("@CountryId", objModel.CountryId);
                        cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Airports Added Successfully!";
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



        #region List Airports

        public List<ACRF_AirportsModel> ListAirports()
        {
            List<ACRF_AirportsModel> objList = new List<ACRF_AirportsModel>();
            try
            {
                string sqlstr = "select Id, isnull(ShortCode,'') as ShortCode, isnull(Airports,'') as Airports, isnull(CountryId,0) as CountryId, "
                + " isnull(CreatedBy,'') as CreatedBy, isnull(createdon,'') as CreatedOn,  "
                    + " isnull(updatedby,'') as updatedby, isnull(updatedon,'') as updatedon from ACRF_Airports order by Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_AirportsModel tempobj = new ACRF_AirportsModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.Airports = sdr["Airports"].ToString();
                    tempobj.ShortCode = sdr["ShortCode"].ToString();
                    tempobj.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
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

        

        #region Get One Airports

        public ACRF_AirportsModel GetOneAirports(int Id)
        {
            ACRF_AirportsModel objList = new ACRF_AirportsModel();
            try
            {
                string sqlstr = "select Id, isnull(ShortCode,'') as ShortCode, isnull(Airports,'') as Airports, isnull(CountryId,0) as CountryId, "
                + " isnull(CreatedBy,'') as CreatedBy, isnull(createdon,'') as CreatedOn,  "
                    + " isnull(updatedby,'') as updatedby, isnull(updatedon,'') as updatedon from ACRF_Airports where Id=@Id";
                
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    objList.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objList.Airports = sdr["Airports"].ToString();
                    objList.ShortCode = sdr["ShortCode"].ToString();
                    objList.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
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


        
        #region Update Airports

        public string UpdateAirports(ACRF_AirportsModel objModel)
        {
            string result = "Error on Updating Airports!";
            try
            {
                result = CheckIfAirportsExists(objModel);
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
                        sqlstr = "update ACRF_Airports set Airports=@Airports, ShortCode=@ShortCode, CountryId=@CountryId,"
                        + " UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn where Id=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Airports", objModel.Airports);
                        cmd.Parameters.AddWithValue("@ShortCode", objModel.ShortCode);
                        cmd.Parameters.AddWithValue("@CountryId", objModel.CountryId);
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);
                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Airports Updated Successfully!";
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


        
        #region Delete Airports

        public string DeleteAirports(int Id, string CreatedBy)
        {
            string result = "Error on Deleting Airports!";
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
                    sqlstr = "insert into ACRF_Airports_Log(Id,ShortCode,Airports,CountryId,CreatedBy,CreatedOn) Select Id,"
                     + " ShortCode,Airports,CountryId,@CreatedBy,@CreatedOn from ACRF_Airports where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();


                    sqlstr = "delete from ACRF_Airports where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();



                    transaction.Commit();
                    connection.Close();
                    result = "Airports Deleted Successfully!";
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




        #region Check If Airports Already Exists

        public string CheckIfAirportsExists(ACRF_AirportsModel objModel)
        {
            string result = "";
            try
            {

                string sqlstr = "Select * from ACRF_Airports Where ISNULL(Airports,'')=@Airports and Isnull(Id,0)!=@Id "
                 + " and CountryId=@CountryId and ShortCode=@ShortCode ";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Airports", objModel.Airports);
                cmd.Parameters.AddWithValue("@CountryId", objModel.CountryId);
                cmd.Parameters.AddWithValue("@ShortCode", objModel.ShortCode);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    result = "Airports already exists!";
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