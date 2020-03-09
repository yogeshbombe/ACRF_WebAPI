using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class DestinationMasterViewModel
    {


        #region Create Destination Master

        public string CreateDestinationMaster(ACRF_DestinationMasterModel objModel)
        {
            string result = "Error on Saving Destination!";
            try
            {
                objModel = NullToBlank(objModel);
                result = CheckIfDestinationMasterExists(objModel);
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
                        sqlstr = "insert into ACRF_DestinationMaster(CityCode,CityName,CountryCode,CountryName,TimeDifference, "
                        + " CustomAirport,AirportName,ISDCode,Currency,State,IATAArea,CreatedBy,CreatedOn)"
                        + "  values (@CityCode,@CityName,@CountryCode,@CountryName,@TimeDifference, "
                        + " @CustomAirport,@AirportName,@ISDCode,@Currency,@State,@IATAArea,@CreatedBy,@CreatedOn)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CityCode", objModel.CityCode);
                        cmd.Parameters.AddWithValue("@CityName", objModel.CityName);
                        cmd.Parameters.AddWithValue("@CountryCode", objModel.CountryCode);
                        cmd.Parameters.AddWithValue("@CountryName", objModel.CountryName);
                        cmd.Parameters.AddWithValue("@TimeDifference", objModel.TimeDifference);
                        cmd.Parameters.AddWithValue("@CustomAirport", objModel.CustomAirport);
                        cmd.Parameters.AddWithValue("@AirportName", objModel.AirportName);
                        cmd.Parameters.AddWithValue("@ISDCode", objModel.ISDCode);
                        cmd.Parameters.AddWithValue("@Currency", objModel.Currency);
                        cmd.Parameters.AddWithValue("@State", objModel.State);
                        cmd.Parameters.AddWithValue("@IATAArea", objModel.IATAArea);
                        cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Destination Added Successfully!";
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




        #region List Destination Master

        public List<ACRF_DestinationMasterModel> ListDestinationMaster()
        {
            List<ACRF_DestinationMasterModel> objList = new List<ACRF_DestinationMasterModel>();
            try
            {
                string sqlstr = "Select ID,Isnull(CityCode,'') as CityCode,Isnull(CityName,'') as CityName, Isnull(CountryCode,'') as CountryCode,"
                + " Isnull(CountryName,'') as CountryName, Isnull(TimeDifference,'') as TimeDifference, Isnull(CustomAirport,'') as CustomAirport,"
                + " Isnull(AirportName,'') as AirportName, Isnull(ISDCode,'') as ISDCode, Isnull(Currency,'') as Currency, "
                + " Isnull(State,'') as State, Isnull(IATAArea,'') as IATAArea,CreatedBy,CreatedOn From ACRF_DestinationMaster order by Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_DestinationMasterModel tempobj = new ACRF_DestinationMasterModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.CityCode = sdr["CityCode"].ToString();
                    tempobj.CityName = sdr["CityName"].ToString();
                    tempobj.CountryCode = sdr["CountryCode"].ToString();
                    tempobj.CountryName = sdr["CountryName"].ToString();
                    tempobj.TimeDifference = sdr["TimeDifference"].ToString();
                    tempobj.CustomAirport = sdr["CustomAirport"].ToString();
                    tempobj.AirportName = sdr["AirportName"].ToString();
                    tempobj.ISDCode = sdr["ISDCode"].ToString();
                    tempobj.Currency = sdr["Currency"].ToString();
                    tempobj.State = sdr["State"].ToString();
                    tempobj.IATAArea = sdr["IATAArea"].ToString();
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

        


        #region Get One Destination Master

        public ACRF_DestinationMasterModel GetOneDestinationMaster(int Id)
        {
            ACRF_DestinationMasterModel objModel = new ACRF_DestinationMasterModel();
            try
            {
                string sqlstr = "Select ID,Isnull(CityCode,'') as CityCode,Isnull(CityName,'') as CityName, Isnull(CountryCode,'') as CountryCode,"
                + " Isnull(CountryName,'') as CountryName, Isnull(TimeDifference,'') as TimeDifference, Isnull(CustomAirport,'') as CustomAirport,"
                + " Isnull(AirportName,'') as AirportName, Isnull(ISDCode,'') as ISDCode, Isnull(Currency,'') as Currency, "
                + " Isnull(State,'') as State, Isnull(IATAArea,'') as IATAArea,CreatedBy,CreatedOn From ACRF_DestinationMaster where Id=@Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objModel.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objModel.CityCode = sdr["CityCode"].ToString();
                    objModel.CityName = sdr["CityName"].ToString();
                    objModel.CountryCode = sdr["CountryCode"].ToString();
                    objModel.CountryName = sdr["CountryName"].ToString();
                    objModel.TimeDifference = sdr["TimeDifference"].ToString();
                    objModel.CustomAirport = sdr["CustomAirport"].ToString();
                    objModel.AirportName = sdr["AirportName"].ToString();
                    objModel.ISDCode = sdr["ISDCode"].ToString();
                    objModel.Currency = sdr["Currency"].ToString();
                    objModel.State = sdr["State"].ToString();
                    objModel.IATAArea = sdr["IATAArea"].ToString();
                    objModel.CreatedBy = sdr["CreatedBy"].ToString();
                    objModel.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
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





        #region Update Destination Master

        public string UpdateDestinationMaster(ACRF_DestinationMasterModel objModel)
        {
            string result = "Error on Updating Destination Master!";
            try
            {
                objModel = NullToBlank(objModel);
                result = CheckIfDestinationMasterExists(objModel);
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
                        sqlstr = "update ACRF_DestinationMaster set CityCode=@CityCode,CityName=@CityName,CountryCode=@CountryCode,CountryName=@CountryName,TimeDifference=@TimeDifference, "
                         + " CustomAirport=@CustomAirport,AirportName=@AirportName,ISDCode=@ISDCode,Currency=@Currency,State=@State,IATAArea=@IATAArea,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn "
                         + " where Id=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CityCode", objModel.CityCode);
                        cmd.Parameters.AddWithValue("@CityName", objModel.CityName);
                        cmd.Parameters.AddWithValue("@CountryCode", objModel.CountryCode);
                        cmd.Parameters.AddWithValue("@CountryName", objModel.CountryName);
                        cmd.Parameters.AddWithValue("@TimeDifference", objModel.TimeDifference);
                        cmd.Parameters.AddWithValue("@CustomAirport", objModel.CustomAirport);
                        cmd.Parameters.AddWithValue("@AirportName", objModel.AirportName);
                        cmd.Parameters.AddWithValue("@ISDCode", objModel.ISDCode);
                        cmd.Parameters.AddWithValue("@Currency", objModel.Currency);
                        cmd.Parameters.AddWithValue("@State", objModel.State);
                        cmd.Parameters.AddWithValue("@IATAArea", objModel.IATAArea);
                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Destination Master Updated Successfully!";
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



        

        #region Delete Destination Master

        public string DeleteDestinationMaster(int Id, string CreatedBy)
        {
            string result = "Error on Deleting Destination Master!";
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
                    sqlstr = "insert into ACRF_DestinationMaster_Log(Id,CityCode,CityName,CountryCode,CountryName,TimeDifference, "
                        + " CustomAirport,AirportName,ISDCode,Currency,State,IATAArea,CreatedBy,CreatedOn)"
                        + " Select Id,CityCode,CityName,CountryCode,CountryName,TimeDifference, "
                        + " CustomAirport,AirportName,ISDCode,Currency,State,IATAArea,@CreatedBy,@CreatedOn from ACRF_DestinationMaster where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();


                    sqlstr = "delete from ACRF_DestinationMaster where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();



                    transaction.Commit();
                    connection.Close();
                    result = "Destination Master Deleted Successfully!";
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



        

        #region Check If Destination Master Already Exists

        public string CheckIfDestinationMasterExists(ACRF_DestinationMasterModel objModel)
        {
            string result = "";
            try
            {

                string sqlstr = "Select * from ACRF_DestinationMaster Where ISNULL(CityCode,'')=@CityCode and "
                + " Isnull(CountryCode,'') =@CountryCode and Isnull(Id,0)!=@Id ";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@CityCode", objModel.CityCode);
                cmd.Parameters.AddWithValue("@CountryCode", objModel.CountryCode);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (objModel.CityCode != "")
                {
                    while (sdr.Read())
                    {
                        result = "City Code with Country Code already exists!";
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



        private ACRF_DestinationMasterModel NullToBlank(ACRF_DestinationMasterModel objModel)
        {
            if (objModel.AirportName == null)
            {
                objModel.AirportName = "";
            }
            if (objModel.CityCode == null)
            {
                objModel.CityCode = "";
            }
            if (objModel.CityName == null)
            {
                objModel.CityName = "";
            }
            if (objModel.CountryCode == null)
            {
                objModel.CountryCode = "";
            }
            if (objModel.CountryName == null)
            {
                objModel.CountryName = "";
            }
            if (objModel.Currency == null)
            {
                objModel.Currency = "";
            }
            if (objModel.CustomAirport == null)
            {
                objModel.CustomAirport = "";
            }
            if (objModel.IATAArea == null)
            {
                objModel.IATAArea = "";
            }
            if (objModel.ISDCode == null)
            {
                objModel.ISDCode = "";
            }
            if (objModel.State == null)
            {
                objModel.State = "";
            }
            if (objModel.TimeDifference == null)
            {
                objModel.TimeDifference = "";
            }
            
            return objModel;
        }

        


        #region List Destination Master By Page

        public Paged_ACRF_DestinationMasterModel ListDestinationMasterByPage(int max, int page, string search, string sort_col, string sort_dir)
        {
            Paged_ACRF_DestinationMasterModel objPaged = new Paged_ACRF_DestinationMasterModel();
            List<ACRF_DestinationMasterModel> objList = new List<ACRF_DestinationMasterModel>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "ACRF_GetDestinationMasterByPage";

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
                    ACRF_DestinationMasterModel tempobj = new ACRF_DestinationMasterModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.CityCode = sdr["CityCode"].ToString();
                    tempobj.CityName = sdr["CityName"].ToString();
                    tempobj.CountryCode = sdr["CountryCode"].ToString();
                    tempobj.CountryName = sdr["CountryName"].ToString();
                    tempobj.TimeDifference = sdr["TimeDifference"].ToString();
                    tempobj.CustomAirport = sdr["CustomAirport"].ToString();
                    tempobj.AirportName = sdr["AirportName"].ToString();
                    tempobj.ISDCode = sdr["ISDCode"].ToString();
                    tempobj.Currency = sdr["Currency"].ToString();
                    tempobj.State = sdr["State"].ToString();
                    tempobj.IATAArea = sdr["IATAArea"].ToString();
                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    objList.Add(tempobj);
                }
                sdr.Close();
                objPaged.ACRF_DestinationMasterModelList = objList;


                sqlstr = "select count(*) as cnt from ACRF_DestinationMaster where CityCode like @search ";
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





        #region Get City Name From Code

        public static string GetCityNameFromCode(string code)
        {
            string result = "";
            try
            {
                string sqlstr = "Select Isnull(CityCode,'') as CityCode,Isnull(CityName,'') as CityName From ACRF_DestinationMaster where CityCode=@CityCode";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@CityCode", code);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result = sdr["CityName"].ToString();
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





        #region List Destination Master By Prefix

        public List<ACRF_DestinationSearchModel> ListDestinationMasterByPrefix(string prefix)
        {
            List<ACRF_DestinationSearchModel> objList = new List<ACRF_DestinationSearchModel>();
            try
            {
                string sqlstr = "Select ID,Isnull(CityCode,'') as CityCode,Isnull(CityName,'') as CityName From ACRF_DestinationMaster where "
                + " Isnull(CityCode,'') like @Prefix or Isnull(CityName,'') like @Prefix  order by Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Prefix", '%' + prefix + '%');
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_DestinationSearchModel tempobj = new ACRF_DestinationSearchModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.CityCode = sdr["CityCode"].ToString();
                    tempobj.CityName = sdr["CityName"].ToString();
                    tempobj.IsSelect = false;
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

    }
}