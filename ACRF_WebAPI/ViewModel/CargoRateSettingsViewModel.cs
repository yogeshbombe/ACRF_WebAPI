using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class CargoRateSettingsViewModel
    {

        #region Create CargoRateSettings

        public string CreateCargoRateSettings(ACRF_CargoRateSettingsModel objModel)
        {
            string result = "Error on Saving Cargo Rates!";
            try
            {
                objModel = NullToBlank(objModel);
                result = CheckIfCargoRateSettingsExists(objModel);
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
                        sqlstr = "insert into ACRF_CargoRateSettings(Rate1,Rate2,Rate3,CreatedBy,CreatedOn,IsRate1,IsRate2,IsRate3,VendorId,DisplayRate1,"
                        +" DisplayRate2,DisplayRate3) values  (@Rate1,@Rate2,@Rate3,@CreatedBy,@CreatedOn,@IsRate1,@IsRate2,@IsRate3,@VendorId,@DisplayRate1,"
                        +" @DisplayRate2,@DisplayRate3)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Rate1", objModel.Rate1);
                        cmd.Parameters.AddWithValue("@Rate2", objModel.Rate2);
                        cmd.Parameters.AddWithValue("@Rate3", objModel.Rate3);
                        cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.Parameters.AddWithValue("@IsRate1", objModel.IsRate1);
                        cmd.Parameters.AddWithValue("@IsRate2", objModel.IsRate2);
                        cmd.Parameters.AddWithValue("@IsRate3", objModel.IsRate3);
                        cmd.Parameters.AddWithValue("@VendorId", objModel.VendorId);
                        cmd.Parameters.AddWithValue("@DisplayRate1", objModel.DisplayRate1);
                        cmd.Parameters.AddWithValue("@DisplayRate2", objModel.DisplayRate2);
                        cmd.Parameters.AddWithValue("@DisplayRate3", objModel.DisplayRate3);

                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "CargoRateSettings Added Successfully!";
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


        #region Update CargoRateSettings

        public string UpdateCargoRateSettings(ACRF_CargoRateSettingsModel objModel)
        {
            string result = "Error on Updating Airlines!";
            try
            {
                objModel = NullToBlank(objModel);
                result = CheckIfCargoRateSettingsExists(objModel);
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
                        sqlstr = "update ACRF_CargoRateSettings set Rate1=@Rate1,Rate2=@Rate2,Rate3=@Rate3,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn,IsRate1=@IsRate1,IsRate2=@IsRate2,IsRate3=@IsRate3,DisplayRate1=@DisplayRate1,DisplayRate2=@DisplayRate2,DisplayRate3=@DisplayRate3"
                        + " where VendorId=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Rate1", objModel.Rate1);
                        cmd.Parameters.AddWithValue("@Rate2", objModel.Rate2);
                        cmd.Parameters.AddWithValue("@Rate3", objModel.Rate3);
                        cmd.Parameters.AddWithValue("@Id", objModel.VendorId);
                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.Parameters.AddWithValue("@IsRate1", objModel.IsRate1);
                        cmd.Parameters.AddWithValue("@IsRate2", objModel.IsRate2);
                        cmd.Parameters.AddWithValue("@IsRate3", objModel.IsRate3);
                        cmd.Parameters.AddWithValue("@DisplayRate1", objModel.DisplayRate1);
                        cmd.Parameters.AddWithValue("@DisplayRate2", objModel.DisplayRate2);
                        cmd.Parameters.AddWithValue("@DisplayRate3", objModel.DisplayRate3);
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Cargoratesettings Updated Successfully!";
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

        #region Get One CargoRateSettings

        public ACRF_CargoRateSettingsModel GetOneCargoRateSettings(int id)
        {
            ACRF_CargoRateSettingsModel objModel = new ACRF_CargoRateSettingsModel();
            try
            {
                string sqlstr = "select Id, isnull(Rate1,0) as Rate1, isnull(Rate2,0) as Rate2,isnull(Rate3,'0') as Rate3,IsRate1,IsRate2,IsRate3,VendorId,CreatedBy,CreatedOn,DisplayRate1,DisplayRate2,DisplayRate3 " 
                    + " from ACRF_CargoRateSettings where VendorId=@Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    objModel.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objModel.Rate1 = Convert.ToInt32(sdr["Rate1"]);
                    objModel.Rate2 = Convert.ToInt32(sdr["Rate2"]);
                    objModel.Rate3 = Convert.ToInt32(sdr["Rate3"]);
                    objModel.IsRate1 = Convert.ToBoolean(sdr["IsRate1"].ToString());
                    objModel.IsRate2 = Convert.ToBoolean(sdr["IsRate2"].ToString());
                    objModel.IsRate3 = Convert.ToBoolean(sdr["IsRate3"].ToString());
                    objModel.VendorId = Convert.ToInt32(sdr["VendorId"].ToString());
                    objModel.CreatedBy = sdr["CreatedBy"].ToString();
                    objModel.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    objModel.DisplayRate1 = sdr["DisplayRate1"].ToString();
                    objModel.DisplayRate2 = sdr["DisplayRate2"].ToString();
                    objModel.DisplayRate3 = sdr["DisplayRate3"].ToString();
                
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

        #endregion


        private ACRF_CargoRateSettingsModel NullToBlank(ACRF_CargoRateSettingsModel objModel)
        {
           
            if (objModel.Rate1 == null)
            {
                objModel.Rate1 = 0;
            }
            if (objModel.Rate2 == null)
            {
                objModel.Rate2 = 0;
            }
            if (objModel.Rate3 == null)
            {
                objModel.Rate3 = 0;
            }
          
            if(objModel.DisplayRate1 == null)
            {
                objModel.DisplayRate1 = "";
            }
            if(objModel.DisplayRate2 == null)
            {
                objModel.DisplayRate2 = "";
            }
            if(objModel.DisplayRate3==null)
            {
                objModel.DisplayRate3 = "";
            }

            return objModel;
        }



        #region Check If Cargo Rates Already Exists

        public string CheckIfCargoRateSettingsExists(ACRF_CargoRateSettingsModel objModel)
        {
            string result = "";
            try
            {
                string sqlstr = "Select * from ACRF_CargoRateSettings Where ISNULL(Rate1,0)=@Rate1 and isnull(Rate2,0)=@Rate2 "
                + " and isnull(Rate3,'') =@Rate3";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Rate1", objModel.Rate1);
                cmd.Parameters.AddWithValue("@Rate2", objModel.Rate2);
                cmd.Parameters.AddWithValue("@Rate3", objModel.Rate3);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result = "CargoRateSettings already exists!";
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