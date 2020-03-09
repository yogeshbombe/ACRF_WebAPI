using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class AirlinesViewModel
    {

        #region Create Airlines

        public string CreateAirlines(ACRF_AirlinesModel objModel)
        {
            string result = "Error on Saving Airlines!";
            try
            {
                objModel = NullToBlank(objModel);
                result = CheckIfAirlinesExists(objModel);
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
                        sqlstr = "insert into ACRF_Airlines(AirlineCode,SubCode,AirlineName,HQAddress,HQPhone,HQFAX,HQEmail,HQIATAMember,"
                        + " SalGsaCsaName,SalAddress,SalPhone,SalFAX,SalEmail,OprAddress,OprPhone,OprFAX,OprEmail,CreatedBy,CreatedOn)"
                        + " values (@AirlineCode,@SubCode,@AirlineName,@HQAddress,@HQPhone,@HQFAX,@HQEmail,@HQIATAMember,"
                        + " @SalGsaCsaName,@SalAddress,@SalPhone,@SalFAX,@SalEmail,@OprAddress,@OprPhone,@OprFAX,@OprEmail,@CreatedBy,@CreatedOn)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@AirlineCode", objModel.AirlineCode);
                        cmd.Parameters.AddWithValue("@SubCode", objModel.SubCode);
                        cmd.Parameters.AddWithValue("@AirlineName", objModel.AirlineName);
                        cmd.Parameters.AddWithValue("@HQAddress", objModel.HQAddress);
                        cmd.Parameters.AddWithValue("@HQPhone", objModel.HQPhone);
                        cmd.Parameters.AddWithValue("@HQFAX", objModel.HQFAX);
                        cmd.Parameters.AddWithValue("@HQEmail", objModel.HQEmail);
                        cmd.Parameters.AddWithValue("@HQIATAMember", objModel.HQIATAMember);
                        cmd.Parameters.AddWithValue("@SalGsaCsaName", objModel.SalGsaCsaName);
                        cmd.Parameters.AddWithValue("@SalAddress", objModel.SalAddress);
                        cmd.Parameters.AddWithValue("@SalPhone", objModel.SalPhone);
                        cmd.Parameters.AddWithValue("@SalFAX", objModel.SalFAX);
                        cmd.Parameters.AddWithValue("@SalEmail", objModel.SalEmail);
                        cmd.Parameters.AddWithValue("@OprAddress", objModel.OprAddress);
                        cmd.Parameters.AddWithValue("@OprPhone", objModel.OprPhone);
                        cmd.Parameters.AddWithValue("@OprFAX", objModel.OprFAX);
                        cmd.Parameters.AddWithValue("@OprEmail", objModel.OprEmail);

                        cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Airlines Added Successfully!";
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



        #region List Airlines With Pagination

        public Paged_ACRF_AirlinesModel ListAirlines(int max, int page, string search, string sort_col, string sort_dir)
        {
            Paged_ACRF_AirlinesModel objPaged = new Paged_ACRF_AirlinesModel();
            List<ACRF_AirlinesModel> objList = new List<ACRF_AirlinesModel>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "ACRF_GetAirlinesByPage";


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
                    ACRF_AirlinesModel tempobj = new ACRF_AirlinesModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.AirlineCode = sdr["AirlineCode"].ToString();
                    tempobj.SubCode = sdr["SubCode"].ToString();
                    tempobj.AirlineName = sdr["AirlineName"].ToString();
                    tempobj.HQAddress = sdr["HQAddress"].ToString();
                    tempobj.HQPhone = sdr["HQPhone"].ToString();
                    tempobj.HQFAX = sdr["HQFAX"].ToString();
                    tempobj.HQEmail = sdr["HQEmail"].ToString();
                    tempobj.HQIATAMember = sdr["HQIATAMember"].ToString();
                    tempobj.SalGsaCsaName = sdr["SalGsaCsaName"].ToString();
                    tempobj.SalAddress = sdr["SalAddress"].ToString();
                    tempobj.SalPhone = sdr["SalPhone"].ToString();
                    tempobj.SalFAX = sdr["SalFAX"].ToString();
                    tempobj.SalEmail = sdr["SalEmail"].ToString();
                    tempobj.OprAddress = sdr["OprAddress"].ToString();
                    tempobj.OprPhone = sdr["OprPhone"].ToString();
                    tempobj.OprFAX = sdr["OprFAX"].ToString();
                    tempobj.OprEmail = sdr["OprEmail"].ToString();

                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());

                    if (sdr["AirlinePhoto"].ToString() == "")
                    {
                        tempobj.AirlinePhoto = "";
                        tempobj.AirlineDemoPhoto = tempobj.AirlineName[0].ToString();
                    }
                    else
                    {
                        tempobj.AirlinePhoto = GlobalFunction.GetAPIUrl() + sdr["AirlinePhoto"].ToString();
                    }

                    objList.Add(tempobj);
                }
                sdr.Close();
                objPaged.ACRF_AirlinesModelList = objList;


                sqlstr = "select count(*) as cnt from ACRF_Airlines where AirlineName like  @search ";
                cmd.Parameters.Clear();
                cmd.CommandText = sqlstr;
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@search", '%' + search + '%');
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



        #region Check If Airlines Already Exists

        public string CheckIfAirlinesExists(ACRF_AirlinesModel objModel)
        {
            string result = "";
            try
            {
                string sqlstr = "Select * from ACRF_Airlines Where ISNULL(AirlineCode,'')=@AirlineCode and isnull(AirlineName,'')=@AirlineName "
                + " and isnull(SubCode,'') =@SubCode and Isnull(Id,0)!=@Id ";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@AirlineCode", objModel.AirlineCode);
                cmd.Parameters.AddWithValue("@AirlineName", objModel.AirlineName);
                cmd.Parameters.AddWithValue("@SubCode", objModel.SubCode);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result = "Airlines already exists!";
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




        #region List Airlines

        public List<ACRF_AirlinesModel> ListAirlines()
        {
            List<ACRF_AirlinesModel> objList = new List<ACRF_AirlinesModel>();
            try
            {
                string sqlstr = "select Id, isnull(AirlineCode,'') as AirlineCode, isnull(SubCode,'') as SubCode, isnull(AirlineName,'') as AirlineName, "
                + " isnull(HQAddress,'') as HQAddress, isnull(HQPhone,'') as HQPhone, isnull(HQFAX,'') as HQFAX, isnull(HQEmail,'') as HQEmail, "
                + " isnull(HQIATAMember,'') as HQIATAMember,isnull(SalGsaCsaName,'') as SalGsaCsaName, isnull(SalAddress,'') as SalAddress, "
                + " isnull(SalPhone,'') as SalPhone, isnull(SalFAX,'') as SalFAX, isnull(SalEmail,'') as SalEmail, isnull(OprAddress,'')  "
                + " as OprAddress, isnull(OprPhone,'') as OprPhone, isnull(OprFAX,'') as OprFAX, isnull(OprEmail,'') as OprEmail, "
                + " isnull(CreatedBy,'') as CreatedBy, isnull(CreatedOn,'') as CreatedOn, isnull(AirlinePhoto,'') as AirlinePhoto from ACRF_Airlines order by Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_AirlinesModel tempobj = new ACRF_AirlinesModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.AirlineCode = sdr["AirlineCode"].ToString();
                    tempobj.SubCode = sdr["SubCode"].ToString();
                    tempobj.AirlineName = sdr["AirlineName"].ToString();
                    tempobj.HQAddress = sdr["HQAddress"].ToString();
                    tempobj.HQPhone = sdr["HQPhone"].ToString();
                    tempobj.HQFAX = sdr["HQFAX"].ToString();
                    tempobj.HQEmail = sdr["HQEmail"].ToString();
                    tempobj.HQIATAMember = sdr["HQIATAMember"].ToString();
                    tempobj.SalGsaCsaName = sdr["SalGsaCsaName"].ToString();
                    tempobj.SalAddress = sdr["SalAddress"].ToString();
                    tempobj.SalPhone = sdr["SalPhone"].ToString();
                    tempobj.SalFAX = sdr["SalFAX"].ToString();
                    tempobj.SalEmail = sdr["SalEmail"].ToString();
                    tempobj.OprAddress = sdr["OprAddress"].ToString();
                    tempobj.OprPhone = sdr["OprPhone"].ToString();
                    tempobj.OprFAX = sdr["OprFAX"].ToString();
                    tempobj.OprEmail = sdr["OprEmail"].ToString();
                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());

                    if (sdr["AirlinePhoto"].ToString() == "")
                    {
                        tempobj.AirlinePhoto = "";
                        tempobj.AirlineDemoPhoto = tempobj.AirlineName[0].ToString();
                    }
                    else
                    {
                        tempobj.AirlinePhoto = GlobalFunction.GetAPIUrl() + sdr["AirlinePhoto"].ToString();
                    }

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



        #region Get One Airlines

        public ACRF_AirlinesModel GetOneAirlines(int Id)
        {
            ACRF_AirlinesModel objModel = new ACRF_AirlinesModel();
            try
            {
                string sqlstr = "select Id, isnull(AirlineCode,'') as AirlineCode, isnull(SubCode,'') as SubCode, isnull(AirlineName,'') as AirlineName, "
                 + " isnull(HQAddress,'') as HQAddress, isnull(HQPhone,'') as HQPhone, isnull(HQFAX,'') as HQFAX, isnull(HQEmail,'') as HQEmail, "
                 + " isnull(HQIATAMember,'') as HQIATAMember,isnull(SalGsaCsaName,'') as SalGsaCsaName, isnull(SalAddress,'') as SalAddress, "
                 + " isnull(SalPhone,'') as SalPhone, isnull(SalFAX,'') as SalFAX, isnull(SalEmail,'') as SalEmail, isnull(OprAddress,'')  "
                 + " as OprAddress, isnull(OprPhone,'') as OprPhone, isnull(OprFAX,'') as OprFAX, isnull(OprEmail,'') as OprEmail, "
                 + " isnull(CreatedBy,'') as CreatedBy, isnull(CreatedOn,'') as CreatedOn from ACRF_Airlines where Id=@Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objModel.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objModel.AirlineCode = sdr["AirlineCode"].ToString();
                    objModel.SubCode = sdr["SubCode"].ToString();
                    objModel.AirlineName = sdr["AirlineName"].ToString();
                    objModel.HQAddress = sdr["HQAddress"].ToString();
                    objModel.HQPhone = sdr["HQPhone"].ToString();
                    objModel.HQFAX = sdr["HQFAX"].ToString();
                    objModel.HQEmail = sdr["HQEmail"].ToString();
                    objModel.HQIATAMember = sdr["HQIATAMember"].ToString();
                    objModel.SalGsaCsaName = sdr["SalGsaCsaName"].ToString();
                    objModel.SalAddress = sdr["SalAddress"].ToString();
                    objModel.SalPhone = sdr["SalPhone"].ToString();
                    objModel.SalFAX = sdr["SalFAX"].ToString();
                    objModel.SalEmail = sdr["SalEmail"].ToString();
                    objModel.OprAddress = sdr["OprAddress"].ToString();
                    objModel.OprPhone = sdr["OprPhone"].ToString();
                    objModel.OprFAX = sdr["OprFAX"].ToString();
                    objModel.OprEmail = sdr["OprEmail"].ToString();
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



        #region Update Airlines

        public string UpdateAirlines(ACRF_AirlinesModel objModel)
        {
            string result = "Error on Updating Airlines!";
            try
            {
                objModel = NullToBlank(objModel);
                result = CheckIfAirlinesExists(objModel);
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
                        string sqlstr = "update ACRF_Airlines set AirlineCode=@AirlineCode,SubCode=@SubCode,AirlineName=@AirlineName,HQAddress=@HQAddress,"
                        + " HQPhone=@HQPhone,HQFAX=@HQFAX,HQEmail=@HQEmail,HQIATAMember=@HQIATAMember,SalGsaCsaName=@SalGsaCsaName,SalAddress=@SalAddress,"
                        + " SalPhone=@SalPhone,SalFAX=@SalFAX,SalEmail=@SalEmail,OprAddress=@OprAddress,OprPhone=@OprPhone,OprFAX=@OprFAX,OprEmail=@OprEmail,"
                        + " UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn where Id=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@AirlineCode", objModel.AirlineCode);
                        cmd.Parameters.AddWithValue("@SubCode", objModel.SubCode);
                        cmd.Parameters.AddWithValue("@AirlineName", objModel.AirlineName);
                        cmd.Parameters.AddWithValue("@HQAddress", objModel.HQAddress);
                        cmd.Parameters.AddWithValue("@HQPhone", objModel.HQPhone);
                        cmd.Parameters.AddWithValue("@HQFAX", objModel.HQFAX);
                        cmd.Parameters.AddWithValue("@HQEmail", objModel.HQEmail);
                        cmd.Parameters.AddWithValue("@HQIATAMember", objModel.HQIATAMember);
                        cmd.Parameters.AddWithValue("@SalGsaCsaName", objModel.SalGsaCsaName);
                        cmd.Parameters.AddWithValue("@SalAddress", objModel.SalAddress);
                        cmd.Parameters.AddWithValue("@SalPhone", objModel.SalPhone);
                        cmd.Parameters.AddWithValue("@SalFAX", objModel.SalFAX);
                        cmd.Parameters.AddWithValue("@SalEmail", objModel.SalEmail);
                        cmd.Parameters.AddWithValue("@OprAddress", objModel.OprAddress);
                        cmd.Parameters.AddWithValue("@OprPhone", objModel.OprPhone);
                        cmd.Parameters.AddWithValue("@OprFAX", objModel.OprFAX);
                        cmd.Parameters.AddWithValue("@OprEmail", objModel.OprEmail);
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);

                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Airlines Updated Successfully!";
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



        #region Delete Airlines

        public string DeleteAirlines(int Id, string CreatedBy)
        {
            string result = "Error on Deleting Airlines!";
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
                    sqlstr = "insert into ACRF_Airlines_Log(Id,AirlineCode,SubCode,AirlineName,HQAddress,HQPhone,HQFAX,HQEmail,HQIATAMember,"
                        + " SalGsaCsaName,SalAddress,SalPhone,SalFAX,SalEmail,OprAddress,OprPhone,OprFAX,OprEmail,CreatedBy,CreatedOn)"
                        + " Select Id,AirlineCode,SubCode,AirlineName,HQAddress,HQPhone,HQFAX,HQEmail,HQIATAMember,"
                        + " SalGsaCsaName,SalAddress,SalPhone,SalFAX,SalEmail,OprAddress,OprPhone,OprFAX,OprEmail, "
                        + " @CreatedBy,@CreatedOn from ACRF_Airlines where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();


                    sqlstr = "delete from ACRF_Airlines where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.ExecuteNonQuery();



                    transaction.Commit();
                    connection.Close();
                    result = "Airlines Deleted Successfully!";
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




        public ACRF_AirlinesModel NullToBlank(ACRF_AirlinesModel objModel)
        {
            if (objModel.AirlineCode == null)
            {
                objModel.AirlineCode = "";
            }
            if (objModel.AirlineName == null)
            {
                objModel.AirlineName = "";
            }
            if (objModel.HQAddress == null)
            {
                objModel.HQAddress = "";
            }
            if (objModel.HQEmail == null)
            {
                objModel.HQEmail = "";
            }
            if (objModel.HQFAX == null)
            {
                objModel.HQFAX = "";
            }
            if (objModel.HQIATAMember == null)
            {
                objModel.HQIATAMember = "";
            }
            if (objModel.HQPhone == null)
            {
                objModel.HQPhone = "";
            }
            if (objModel.OprAddress == null)
            {
                objModel.OprAddress = "";
            }
            if (objModel.OprEmail == null)
            {
                objModel.OprEmail = "";
            }
            if (objModel.OprFAX == null)
            {
                objModel.OprFAX = "";
            }
            if (objModel.OprPhone == null)
            {
                objModel.OprPhone = "";
            }
            if (objModel.SalAddress == null)
            {
                objModel.SalAddress = "";
            }
            if (objModel.SalEmail == null)
            {
                objModel.SalEmail = "";
            }
            if (objModel.SalFAX == null)
            {
                objModel.SalFAX = "";
            }
            if (objModel.SalGsaCsaName == null)
            {
                objModel.SalGsaCsaName = "";
            }
            if (objModel.SalPhone == null)
            {
                objModel.SalPhone = "";
            }

            return objModel;
        }




        #region List Airlines By Prefix

        public List<ACRF_AirlinesSearchModel> ListAirlinesByPrefix(string prefix)
        {
            List<ACRF_AirlinesSearchModel> objList = new List<ACRF_AirlinesSearchModel>();
            try
            {
                string sqlstr = "select Id, isnull(AirlineCode,'') as AirlineCode, isnull(SubCode,'') as SubCode, isnull(AirlineName,'') as AirlineName "
                + " from ACRF_Airlines where isnull(AirlineName,'') like @AirlineName order by Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@AirlineName", '%' + prefix + '%');
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_AirlinesSearchModel tempobj = new ACRF_AirlinesSearchModel();
                    //tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    //tempobj.AirlineCode = sdr["AirlineCode"].ToString();
                    //tempobj.SubCode = sdr["SubCode"].ToString();
                    //tempobj.AirlineName = sdr["AirlineName"].ToString();
                    //tempobj.IsSelect = false;
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




        #region Update Airlines Photo

        public string UpdateAirlinePhoto(ACRF_AirlinesModel objModel)
        {
            string result = "Error on Updating Airline Photo!";
            try
            {
                objModel = NullToBlank(objModel);
                result = CheckIfAirlinesExists(objModel);
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
                        string sqlstr = "update ACRF_Airlines set AirlinePhoto=@AirlinePhoto,"
                        + " UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn where Id=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@AirlinePhoto", objModel.AirlinePhoto);
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);

                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Airlines Photo Updated Successfully!";
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


    }
}