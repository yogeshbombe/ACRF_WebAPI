using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class CustomerDetailsViewModel
    {

        #region Create Customer Details

        public string CreateCustomerDetails(ACRF_CustomerDetailsModel objModel)
        {
            string result = "Error on Saving Customer Details!";
            try
            {
                objModel = NullToBlank(objModel);
                result = CheckIfCustomerDetailsExists(objModel);
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
                        sqlstr = "insert into ACRF_CustomerDetails(VendorId,CustomerName,Address,ContactName,Mobile,Email,FAX,SkypeId,Website,MiscInfo "
                         + " ,CountryId,PostalCode,CreatedBy,CreatedOn) "
                         + " values (@VendorId,@CustomerName,@Address,@ContactName,@Mobile,@Email,@FAX,@SkypeId,@Website,@MiscInfo "
                         + " ,@CountryId,@PostalCode,@CreatedBy,@CreatedOn)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@VendorId", objModel.VendorId);
                        cmd.Parameters.AddWithValue("@CustomerName", objModel.CustomerName);
                        cmd.Parameters.AddWithValue("@Address", objModel.Address);
                        cmd.Parameters.AddWithValue("@ContactName", objModel.ContactName);
                        cmd.Parameters.AddWithValue("@Mobile", objModel.Mobile);
                        cmd.Parameters.AddWithValue("@Email", objModel.Email);
                        cmd.Parameters.AddWithValue("@FAX", objModel.FAX);
                        cmd.Parameters.AddWithValue("@SkypeId", objModel.SkypeId);
                        cmd.Parameters.AddWithValue("@Website", objModel.Website);
                        cmd.Parameters.AddWithValue("@MiscInfo", objModel.MiscInfo);
                        cmd.Parameters.AddWithValue("@CountryId", objModel.CountryId);
                        cmd.Parameters.AddWithValue("@PostalCode", objModel.PostalCode);
                        cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Customer Details Added Successfully!";
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

        
        #region List Customer Details

        public List<ACRF_CustomerDetailsModel> ListCustomerDetails(int VendorId)
        {
            List<ACRF_CustomerDetailsModel> objList = new List<ACRF_CustomerDetailsModel>();
            try
            {
                string sqlstr = "select Id,CustomerName, VendorId, isnull(Address,'') as Address, isnull(Mobile,'') as Mobile,"
                + " isnull(Email,'') as Email, isnull(Password,'') as Password, isnull(CountryId,0) as CountryId, "
                + " isnull(CreatedBy,'') as CreatedBy, isnull(CreatedOn,'')  "
                + " as CreatedOn, isnull(ContactName,'') as ContactName From ACRF_CustomerDetails ";
                if(VendorId>0)
                {
                    sqlstr = sqlstr + " where VendorId=@VendorId";
                }

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                if(VendorId>0)
                {
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                }
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_CustomerDetailsModel tempobj = new ACRF_CustomerDetailsModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.CustomerName = sdr["CustomerName"].ToString();
                    tempobj.Address = sdr["Address"].ToString();
                    tempobj.Mobile = sdr["Mobile"].ToString();
                    tempobj.Email = sdr["Email"].ToString();
                    tempobj.VendorId = Convert.ToInt32(sdr["VendorId"].ToString());
                    tempobj.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    tempobj.ContactName = sdr["ContactName"].ToString();
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


        #region Update Customer Details

        public string UpdateCustomerDetails(ACRF_CustomerDetailsModel objModel)
        {
            string result = "Error on Updating Customer Details!";
            try
            {
                objModel = NullToBlank(objModel);
                result = CheckIfCustomerDetailsExists(objModel);
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
                        sqlstr = "update ACRF_CustomerDetails set CustomerName=@CustomerName,Address=@Address,ContactName=@ContactName,"
                         + " Mobile=@Mobile,Email=@Email,FAX=@FAX,SkypeId=@SkypeId,Website=@Website,MiscInfo=@MiscInfo "
                         + " ,CountryId=@CountryId,PostalCode=@PostalCode,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn "
                         + " where Id=@Id ";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);
                        cmd.Parameters.AddWithValue("@CustomerName", objModel.CustomerName);
                        cmd.Parameters.AddWithValue("@Address", objModel.Address);
                        cmd.Parameters.AddWithValue("@ContactName", objModel.ContactName);
                        cmd.Parameters.AddWithValue("@Mobile", objModel.Mobile);
                        cmd.Parameters.AddWithValue("@Email", objModel.Email);
                        cmd.Parameters.AddWithValue("@FAX", objModel.FAX);
                        cmd.Parameters.AddWithValue("@SkypeId", objModel.SkypeId);
                        cmd.Parameters.AddWithValue("@Website", objModel.Website);
                        cmd.Parameters.AddWithValue("@MiscInfo", objModel.MiscInfo);
                        cmd.Parameters.AddWithValue("@CountryId", objModel.CountryId);
                        cmd.Parameters.AddWithValue("@PostalCode", objModel.PostalCode);
                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Customer Details Updated Successfully!";
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


        #region Get One Customer Details

        public ACRF_CustomerDetailsModel GetOneCustomerDetails(int VendorId)
        {
            ACRF_CustomerDetailsModel objList = new ACRF_CustomerDetailsModel();
            try
            {
                string sqlstr = "select Id,CustomerName, VendorId, isnull(Address,'') as Address, isnull(ContactName,'') as ContactName, "
                + " isnull(Mobile,'') as Mobile, isnull(FAX,'') as FAX, isnull(SkypeId,'') as SkypeId, isnull(Website,'') as Website,"
                + " isnull(Email,'') as Email, isnull(Password,'') as Password, isnull(CountryId,0) as CountryId, isnull(MiscInfo,'') as MiscInfo,"
                + " isnull(CreatedBy,'') as CreatedBy, isnull(CreatedOn,'')  "
                + " as CreatedOn, isnull(PostalCode,'') as PostalCode From ACRF_CustomerDetails where Id=@Id";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", VendorId);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    objList.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objList.CustomerName = sdr["CustomerName"].ToString();
                    objList.Address = sdr["Address"].ToString();
                    objList.ContactName = sdr["ContactName"].ToString();
                    objList.Mobile = sdr["Mobile"].ToString();
                    objList.Email = sdr["Email"].ToString();
                    objList.VendorId = Convert.ToInt32(sdr["VendorId"].ToString());
                    objList.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
                    objList.CreatedBy = sdr["CreatedBy"].ToString();
                    objList.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    objList.PostalCode = sdr["PostalCode"].ToString();
                    objList.FAX = sdr["FAX"].ToString();
                    objList.SkypeId = sdr["SkypeId"].ToString();
                    objList.Website = sdr["Website"].ToString();
                    objList.MiscInfo = sdr["MiscInfo"].ToString();
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


        #region Delete Customer Details

        public string DeleteCustomerDetails(int id, string created_by)
        {
            string result = "Error on Deleting Customer Details!";
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
                    sqlstr = "insert into ACRF_CustomerDetails_Log(Id, CustomerName, VendorId,Address,ContactName,Mobile,Email,FAX,SkypeId,Website,MiscInfo "
                         + " ,Password,CountryId,PostalCode,CreatedBy,CreatedOn) "
                     + " select Id, CustomerName, VendorId,Address,ContactName,Mobile,Email,FAX,SkypeId,Website,MiscInfo "
                         + " ,Password,CountryId,PostalCode,@CreatedBy,@CreatedOn from ACRF_CustomerDetails where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@CreatedBy", created_by);
                    cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();


                    sqlstr = "delete from ACRF_CustomerDetails where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();


                    transaction.Commit();
                    connection.Close();
                    result = "Customer Details Deleted Successfully!";
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



        #region Check If Customer Details Already Exists

        public string CheckIfCustomerDetailsExists(ACRF_CustomerDetailsModel objModel)
        {
            string result = "";
            try
            {

                string sqlstr = "Select * from ACRF_CustomerDetails Where ISNULL(Mobile,'')=@Mobile and Isnull(Id,0)!=@Id and Isnull(VendorId,0) =@VendorId";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Mobile", objModel.Mobile);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                cmd.Parameters.AddWithValue("@VendorId", objModel.VendorId);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (objModel.Mobile != "")
                {
                    while (sdr.Read())
                    {
                        result = "Mobile already exists!";
                    }
                }
                sdr.Close();




                sqlstr = "Select * from ACRF_CustomerDetails Where Isnull(Id,0)!=@Id and Isnull(VendorId,0) =@VendorId"
                 + " and Email=@Email ";
                cmd.Parameters.Clear();
                cmd.Connection = connection;
                cmd.CommandText = sqlstr;
                cmd.Parameters.AddWithValue("@Email", objModel.Email);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                cmd.Parameters.AddWithValue("@VendorId", objModel.VendorId);
                sdr = cmd.ExecuteReader();

                if (objModel.Email != "")
                {
                    while (sdr.Read())
                    {
                        result = "Email already exists!";
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



        private ACRF_CustomerDetailsModel NullToBlank(ACRF_CustomerDetailsModel objModel)
        {
            if (objModel.Address == null)
            {
                objModel.Address = "";
            }
            if (objModel.ContactName == null)
            {
                objModel.ContactName = "";
            }
            if (objModel.Email == null)
            {
                objModel.Email = "";
            }
            if (objModel.FAX == null)
            {
                objModel.FAX = "";
            }
            if (objModel.MiscInfo == null)
            {
                objModel.MiscInfo = "";
            }
            if (objModel.Mobile == null)
            {
                objModel.Mobile = "";
            }
            if (objModel.PostalCode == null)
            {
                objModel.PostalCode = "";
            }
            if (objModel.SkypeId == null)
            {
                objModel.SkypeId = "";
            }
            if (objModel.CustomerName == null)
            {
                objModel.CustomerName = "";
            }
            if (objModel.Website == null)
            {
                objModel.Website = "";
            }

            return objModel;
        }




        #region List Customer Details By Pagination

        public Paged_ACRF_CustomerDetailsModel ListCustomerDetailsByPagination(int max, int page, string search, string sort_col, string sort_dir)
        {
            Paged_ACRF_CustomerDetailsModel objPaged = new Paged_ACRF_CustomerDetailsModel();
            List<ACRF_CustomerDetailsModel> objList = new List<ACRF_CustomerDetailsModel>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "ACRF_GetCustomerByPage";

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
                    ACRF_CustomerDetailsModel tempobj = new ACRF_CustomerDetailsModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.VendorId = Convert.ToInt32(sdr["VendorId"].ToString());
                    tempobj.CustomerName = sdr["CustomerName"].ToString();
                    tempobj.Address = sdr["Address"].ToString();
                    tempobj.ContactName = sdr["ContactName"].ToString();
                    tempobj.Mobile = sdr["Mobile"].ToString();
                    tempobj.Email = sdr["Email"].ToString();
                    tempobj.FAX = sdr["FAX"].ToString();
                    tempobj.SkypeId = sdr["SkypeId"].ToString();
                    tempobj.MiscInfo = sdr["MiscInfo"].ToString();
                    tempobj.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
                    tempobj.PostalCode = sdr["PostalCode"].ToString();
                    //tempobj.LastLogin = Convert.ToDateTime(sdr["LastLogin"].ToString());

                    //tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    //tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    tempobj.Country = sdr["Country"].ToString();
                    tempobj.VendorName = sdr["VendorName"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();
                objPaged.ACRF_CustomerDetailsModelList = objList;


                sqlstr = "select count(*) as cnt from ACRF_CustomerDetails where CustomerName like @search ";
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




        #region Get One Customer Details

        public string GetOneCustomerMobileNumbnerFromEmailId(string Email)
        {
            string Mobile = "";
            try
            {
                string sqlstr = "select isnull(Mobile,'') as Mobile From ACRF_CustomerDetails where Email=@Email";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Mobile = sdr["Mobile"].ToString();
                }
                sdr.Close();

                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Mobile;
        }

        #endregion


    }
}