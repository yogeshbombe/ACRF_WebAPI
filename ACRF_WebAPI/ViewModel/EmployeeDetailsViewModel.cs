using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class EmployeeDetailsViewModel
    {

        #region Create Employee Details

        public string CreateEmployeeDetails(ACRF_EmployeeDetailsModel objModel)
        {
            string result = "Error on Saving Employee Details!";
            try
            {
                objModel = NullToBlank(objModel);
                if (objModel.Password != "")
                {
                    result = CheckIfEmployeeDetailsExists(objModel);
                    if (result == "")
                    {
                        objModel.Password = EnCryptDecrypt.Encryption.encrypt(objModel.Password);

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
                            sqlstr = "insert into ACRF_EmployeeDetails(VendorId,EmployeeNo,EmployeeName,Address,ContactName,Mobile,Email,FAX,SkypeId,MiscInfo "
                             + " ,Password,CountryId,PostalCode,CreatedBy,CreatedOn) "
                             + " values (@VendorId,@EmployeeNo,@EmployeeName,@Address,@ContactName,@Mobile,@Email,@FAX,@SkypeId,@MiscInfo "
                             + " ,@Password,@CountryId,@PostalCode,@CreatedBy,@CreatedOn)";
                            cmd.CommandText = sqlstr;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@VendorId", objModel.VendorId);
                            cmd.Parameters.AddWithValue("@EmployeeNo", objModel.EmployeeNo);
                            cmd.Parameters.AddWithValue("@EmployeeName", objModel.EmployeeName);
                            cmd.Parameters.AddWithValue("@Address", objModel.Address);
                            cmd.Parameters.AddWithValue("@ContactName", objModel.ContactName);
                            cmd.Parameters.AddWithValue("@Mobile", objModel.Mobile);
                            cmd.Parameters.AddWithValue("@Email", objModel.Email);
                            cmd.Parameters.AddWithValue("@FAX", objModel.FAX);
                            cmd.Parameters.AddWithValue("@SkypeId", objModel.SkypeId);
                            cmd.Parameters.AddWithValue("@MiscInfo", objModel.MiscInfo);
                            cmd.Parameters.AddWithValue("@Password", objModel.Password);
                            cmd.Parameters.AddWithValue("@CountryId", objModel.CountryId);
                            cmd.Parameters.AddWithValue("@PostalCode", objModel.PostalCode);
                            cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                            cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                            cmd.ExecuteNonQuery();


                            transaction.Commit();
                            connection.Close();
                            result = "Employee Details Added Successfully!";
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
                else
                {
                    result = "Password can't be blank!";
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion



        #region List Employee Details

        public List<ACRF_EmployeeDetailsModel> ListEmployeeDetails(int VendorId)
        {
            List<ACRF_EmployeeDetailsModel> objList = new List<ACRF_EmployeeDetailsModel>();
            try
            {
                string sqlstr = "select Id,EmployeeName, VendorId, EmployeeNo, isnull(Address,'') as Address, isnull(Mobile,'') as Mobile,"
                + " isnull(Email,'') as Email, isnull(CountryId,0) as CountryId, "
                + " isnull(LastLogin,'') as LastLogin, isnull(CreatedBy,'') as CreatedBy, isnull(CreatedOn,'')  "
                + " as CreatedOn, isnull(ContactName,'') as ContactName From ACRF_EmployeeDetails ";
                if (VendorId != 0)
                {
                    sqlstr = sqlstr + " where VendorId=@VendorId ";
                }
                sqlstr = sqlstr + " order by Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                if (VendorId != 0)
                {
                    cmd.Parameters.AddWithValue("@VendorId", VendorId);
                }
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_EmployeeDetailsModel tempobj = new ACRF_EmployeeDetailsModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.EmployeeName = sdr["EmployeeName"].ToString();
                    tempobj.VendorId = Convert.ToInt32(sdr["VendorId"].ToString());
                    tempobj.EmployeeNo = sdr["EmployeeNo"].ToString();
                    tempobj.Address = sdr["Address"].ToString();
                    tempobj.Mobile = sdr["Mobile"].ToString();
                    tempobj.Email = sdr["Email"].ToString();
                    tempobj.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
                    tempobj.LastLogin = Convert.ToDateTime(sdr["LastLogin"].ToString());
                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    tempobj.ContactName = sdr["ContactName"].ToString();
                    tempobj.Password = EnCryptDecrypt.Encryption.decrypt(tempobj.Password);
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



        #region Get One Employee Details

        public ACRF_EmployeeDetailsModel GetOneEmployeeDetails(int EmployeeId)
        {
            ACRF_EmployeeDetailsModel objList = new ACRF_EmployeeDetailsModel();
            try
            {
                string sqlstr = "select Id,EmployeeName, VendorId, EmployeeNo, isnull(Address,'') as Address, isnull(ContactName,'') as ContactName, "
                + " isnull(Mobile,'') as Mobile, isnull(FAX,'') as FAX, isnull(SkypeId,'') as SkypeId, "
                + " isnull(Email,'') as Email, isnull(Password,'') as Password, isnull(CountryId,0) as CountryId, isnull(MiscInfo,'') as MiscInfo,"
                + " isnull(LastLogin,'') as LastLogin, isnull(CreatedBy,'') as CreatedBy, isnull(CreatedOn,'')  "
                + " as CreatedOn, isnull(PostalCode,'') as PostalCode From ACRF_EmployeeDetails where Id=@Id";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", EmployeeId);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    objList.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objList.VendorId = Convert.ToInt32(sdr["VendorId"].ToString());
                    objList.EmployeeNo = sdr["EmployeeNo"].ToString();
                    objList.EmployeeName = sdr["EmployeeName"].ToString();
                    objList.Address = sdr["Address"].ToString();
                    objList.ContactName = sdr["ContactName"].ToString();
                    objList.Mobile = sdr["Mobile"].ToString();
                    objList.Email = sdr["Email"].ToString();
                    //objList.Password = sdr["Password"].ToString();
                    objList.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
                    objList.LastLogin = Convert.ToDateTime(sdr["LastLogin"].ToString());
                    objList.CreatedBy = sdr["CreatedBy"].ToString();
                    objList.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    objList.PostalCode = sdr["PostalCode"].ToString();
                    objList.FAX = sdr["FAX"].ToString();
                    objList.SkypeId = sdr["SkypeId"].ToString();
                    objList.MiscInfo = sdr["MiscInfo"].ToString();
                    objList.Password = EnCryptDecrypt.Encryption.decrypt(objList.Password);
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



        #region Update Employee Details

        public string UpdateEmployeeDetails(ACRF_EmployeeDetailsModel objModel)
        {
            string result = "Error on Updating Employee Details!";
            try
            {
                objModel = NullToBlank(objModel);
                //result = CheckIfEmployeeDetailsExists(objModel);
                //if (result == "")
                //{
                objModel.Password = EnCryptDecrypt.Encryption.encrypt(objModel.Password);
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
                    sqlstr = "update ACRF_EmployeeDetails set VendorId=@VendorId, EmployeeName=@EmployeeName,Address=@Address,ContactName=@ContactName,"
                     + " Mobile=@Mobile,Email=@Email,FAX=@FAX,SkypeId=@SkypeId,MiscInfo=@MiscInfo "
                     + " ,CountryId=@CountryId,PostalCode=@PostalCode,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn "
                     + " where Id=@Id ";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", objModel.Id);
                    cmd.Parameters.AddWithValue("@VendorId", objModel.VendorId);
                    cmd.Parameters.AddWithValue("@EmployeeName", objModel.EmployeeName);
                    cmd.Parameters.AddWithValue("@Address", objModel.Address);
                    cmd.Parameters.AddWithValue("@ContactName", objModel.ContactName);
                    cmd.Parameters.AddWithValue("@Mobile", objModel.Mobile);
                    cmd.Parameters.AddWithValue("@Email", objModel.Email);
                    cmd.Parameters.AddWithValue("@FAX", objModel.FAX);
                    cmd.Parameters.AddWithValue("@SkypeId", objModel.SkypeId);
                    cmd.Parameters.AddWithValue("@MiscInfo", objModel.MiscInfo);
                    cmd.Parameters.AddWithValue("@CountryId", objModel.CountryId);
                    cmd.Parameters.AddWithValue("@PostalCode", objModel.PostalCode);
                    cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                    cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();


                    transaction.Commit();
                    connection.Close();
                    result = "Employee Details Updated Successfully!";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    connection.Close();
                    Global.ErrorHandlerClass.LogError(ex);
                    result = ex.Message;
                }
                //}
                //else
                //{
                //    return result;
                //}
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion



        #region Delete Employee Details

        public string DeleteEmployeeDetails(int id, string created_by)
        {
            string result = "Error on Deleting Employee Details!";
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
                    sqlstr = "insert into ACRF_EmployeeDetails_Log(Id, VendorId,EmployeeNo, EmployeeName,Address,ContactName,"
                         + " Mobile,Email,FAX,SkypeId,MiscInfo ,Password,CountryId,PostalCode,CreatedBy,CreatedOn) "
                     + " select Id, VendorId, EmployeeNo, EmployeeName,Address,ContactName,Mobile,Email,FAX,SkypeId,MiscInfo "
                         + " ,Password,CountryId,PostalCode,@CreatedBy,@CreatedOn from ACRF_EmployeeDetails where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@CreatedBy", created_by);
                    cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();


                    sqlstr = "delete from ACRF_EmployeeDetails where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();


                    transaction.Commit();
                    connection.Close();
                    result = "Employee Details Deleted Successfully!";
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



        #region Check If Employee Details Already Exists

        public string CheckIfEmployeeDetailsExists(ACRF_EmployeeDetailsModel objModel)
        {
            string result = "";
            try
            {

                string sqlstr = "Select * from ACRF_EmployeeDetails Where ISNULL(Mobile,'')=@Mobile and Isnull(VendorId,0)!=@VendorId ";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Mobile", objModel.Mobile);
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


                sqlstr = "Select * from ACRF_EmployeeDetails Where Isnull(VendorId,0)!=@VendorId "
                 + " and Email=@Email ";
                cmd.Parameters.Clear();
                cmd.Connection = connection;
                cmd.CommandText = sqlstr;
                cmd.Parameters.AddWithValue("@Email", objModel.Email);
                cmd.Parameters.AddWithValue("@VendorId", objModel.Id);
                sdr = cmd.ExecuteReader();

                if (objModel.Email != "")
                {
                    while (sdr.Read())
                    {
                        result = "Email already exists!";
                    }
                }
                sdr.Close();


                sqlstr = "Select * from ACRF_EmployeeDetails Where Isnull(VendorId,0)!=@VendorId "
                 + " and EmployeeNo=@EmployeeNo ";
                cmd.Parameters.Clear();
                cmd.Connection = connection;
                cmd.CommandText = sqlstr;
                cmd.Parameters.AddWithValue("@EmployeeNo", objModel.EmployeeNo);
                cmd.Parameters.AddWithValue("@VendorId", objModel.Id);
                sdr = cmd.ExecuteReader();

                if (objModel.Email != "")
                {
                    while (sdr.Read())
                    {
                        result = "Employee No already exists!";
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


        private ACRF_EmployeeDetailsModel NullToBlank(ACRF_EmployeeDetailsModel objModel)
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
            if (objModel.Password == null)
            {
                objModel.Password = "";
            }
            if (objModel.PostalCode == null)
            {
                objModel.PostalCode = "";
            }
            if (objModel.SkypeId == null)
            {
                objModel.SkypeId = "";
            }
            if (objModel.EmployeeName == null)
            {
                objModel.EmployeeName = "";
            }

            return objModel;
        }




        #region List Employee Details By Pagination

        public Paged_ACRF_EmployeeDetailsModel ListEmployeeDetailsByPagination(int max, int page, string search, string sort_col, string sort_dir)
        {
            Paged_ACRF_EmployeeDetailsModel objPaged = new Paged_ACRF_EmployeeDetailsModel();
            List<ACRF_EmployeeDetailsModel> objList = new List<ACRF_EmployeeDetailsModel>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "ACRF_GetEmployeeByPage";

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
                    ACRF_EmployeeDetailsModel tempobj = new ACRF_EmployeeDetailsModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.VendorId = Convert.ToInt32(sdr["VendorId"].ToString());
                    tempobj.EmployeeNo = sdr["EmployeeNo"].ToString();
                    tempobj.EmployeeName = sdr["EmployeeName"].ToString();
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
                objPaged.ACRF_EmployeeDetailsModelList = objList;


                sqlstr = "select count(*) as cnt from ACRF_EmployeeDetails where EmployeeName like @search ";
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