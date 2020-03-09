using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using EnCryptDecrypt;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class AuthViewModel
    {
        public ACRF_UserDetailsModel UserAuth(LoginModel objLogModel)
        {
            //string m = Encryption.encrypt(objLogModel.Password);
            ACRF_UserDetailsModel objModel = new ACRF_UserDetailsModel();
            objModel.email = "";
            objModel.fullName = "";
            try
            {

                string sqlstr = "select Email, AdminName, Id, isnull(ProfilePicture,'') as ProfilePicture from ACRF_AdminDetails "
                   + " where Email=@Email and Password=@Password";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Email", objLogModel.UserName);
                cmd.Parameters.AddWithValue("@Password", Encryption.encrypt(objLogModel.Password));
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objModel.email = sdr["Email"].ToString();
                    objModel.fullName = sdr["AdminName"].ToString();
                    objModel.id = Convert.ToInt32(sdr["Id"].ToString());
                    objModel.userType = UserType.AdminUserShort;
                    objModel.profileimage = sdr["ProfilePicture"].ToString();
                    if (sdr["ProfilePicture"] == "")
                    {
                        objModel.profileimage = "";
                    }
                    else
                    {
                        objModel.profileimage = GlobalFunction.GetAPIUrl() + objModel.profileimage;
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objModel;
        }


        public ACRF_UserDetailsModel VendorAuth(LoginModel objLogModel)
        {
            ACRF_UserDetailsModel objModel = new ACRF_UserDetailsModel();
            objModel.email = "";
            objModel.fullName = "";

            try
            {
                string sqlstr = "select Id, VendorName, Email, isnull(ProfilePicture,'') as ProfilePicture from ACRF_VendorDetails "
                    + " where Email=@Email and Password=@Password";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Email", objLogModel.UserName);
                cmd.Parameters.AddWithValue("@Password", Encryption.encrypt(objLogModel.Password));
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objModel.email = sdr["Email"].ToString();
                    objModel.fullName = sdr["VendorName"].ToString();
                    objModel.id = Convert.ToInt32(sdr["Id"].ToString());
                    objModel.userType = UserType.VendorUserShort;
                    objModel.profileimage = sdr["ProfilePicture"].ToString();
                    if (sdr["ProfilePicture"].ToString() == "")
                    {
                        objModel.profileimage = "";
                    }
                    else
                    {
                        objModel.profileimage = GlobalFunction.GetAPIUrl() + objModel.profileimage;
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objModel;
        }



        public ACRF_UserDetailsModel EmployeeAuth(LoginModel objLogModel)
        {
            ACRF_UserDetailsModel objModel = new ACRF_UserDetailsModel();
            try
            {
                string sqlstr = "select Id, EmployeeName, Email from ACRF_EmployeeDetails "
                    + " where Email=@Email and Password=@Password";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Email", objLogModel.UserName);
                cmd.Parameters.AddWithValue("@Password", Encryption.encrypt(objLogModel.Password));
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objModel.email = sdr["Email"].ToString();
                    objModel.fullName = sdr["EmployeeName"].ToString();
                    objModel.id = Convert.ToInt32(sdr["Id"].ToString());
                    objModel.userType = UserType.VendorUser;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objModel;
        }


        public ACRF_UserDetailsModel AdminAuth(AuthModel objAuthModel)
        {
            ACRF_UserDetailsModel objModel = new ACRF_UserDetailsModel();

            try
            {
                string sqlstr = "select Email, AdminName, Id, isnull(ProfilePicture,'') as ProfilePicture from ACRF_AdminDetails "
                    + " where Email=@Email ";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Email", objAuthModel.UserName);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objModel.email = sdr["Email"].ToString();
                    objModel.fullName = sdr["AdminName"].ToString();
                    objModel.id = Convert.ToInt32(sdr["Id"].ToString());
                    objModel.userType = UserType.AdminUser;
                    objModel.profileimage = sdr["ProfilePicture"].ToString();
                    if (sdr["ProfilePicture"] == "")
                    {
                        objModel.profileimage = "";
                    }
                    else
                    {
                        objModel.profileimage = GlobalFunction.GetAPIUrl() + objModel.profileimage;
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objModel;
        }




        public string UpdateLastLogin(string UserType, int id)
        {
            string result = "";

            try
            {
                string sqlstr = " ";

                if (UserType == ACRF_WebAPI.Global.UserType.AdminUser)
                {
                    sqlstr = "update ACRF_AdminDetails set LastLogin=@LastLogin where ID=@Id ";
                }
                else if (UserType == ACRF_WebAPI.Global.UserType.VendorUser)
                {
                    sqlstr = "update ACRF_VendorDetails set LastLogin=@LastLogin where ID=@Id ";
                }
                else if (UserType == ACRF_WebAPI.Global.UserType.EmployeeUser)
                {
                    sqlstr = "update ACRF_EmployeeDetails set LastLogin=@LastLogin where ID=@Id ";
                }

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@LastLogin", StandardDateTime.GetDateTime());
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }


        public string UpdateAdminProfile(ACRF_AdminDetailsModel objModel)
        {
            string result = "Error! Profile not updated;";

            try
            {
                string sqlstr = " update ACRF_AdminDetails set AdminName=@AdminName,Address=@Address,ContactName=@ContactName,Mobile=@Mobile,"
                + " Email=@Email,FAX=@FAX,SkypeId=@SkypeId,MiscInfo=@MiscInfo,Password=@Password,CountryId=@CountryId, "
                + " PostalCode=@PostalCode,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn where Id=@Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@AdminName", objModel.AdminName);
                cmd.Parameters.AddWithValue("@Address", objModel.Address);
                cmd.Parameters.AddWithValue("@ContactName", objModel.ContactName);
                cmd.Parameters.AddWithValue("@Mobile", objModel.Mobile);
                cmd.Parameters.AddWithValue("@Email", objModel.Email);
                cmd.Parameters.AddWithValue("@FAX", objModel.FAX);
                cmd.Parameters.AddWithValue("@SkypeId", objModel.SkypeId);
                cmd.Parameters.AddWithValue("@MiscInfo", objModel.MiscInfo);
                cmd.Parameters.AddWithValue("@Password", Encryption.encrypt(objModel.Password));
                cmd.Parameters.AddWithValue("@CountryId", objModel.CountryId);
                cmd.Parameters.AddWithValue("@PostalCode", objModel.PostalCode);
                cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                cmd.ExecuteNonQuery();
                connection.Close();


                result = "Profile Updated Successfully!";
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }


        public ACRF_AdminDetailsModel GetAdminProfile(int id)
        {
            ACRF_AdminDetailsModel objModel = new ACRF_AdminDetailsModel();
            try
            {
                string sqlstr = "select Id, isnull(AdminName,'') as AdminName, isnull(Address,'') as Address, isnull(ContactName,'') as ContactName, "
                + " isnull(Mobile,'') as Mobile, isnull(Email,'') as Email, isnull(FAX,'') as FAX, isnull(SkypeId,'') as SkypeId, "
                + " isnull(MiscInfo,'') as MiscInfo, isnull(Password,'') as Password, isnull(CountryId,0) as CountryId, "
                + " isnull(PostalCode,'') as PostalCode, isnull(CreatedBy,'') as CreatedBy, isnull(CreatedOn,'') as CreatedOn,  "
                + " isnull(ProfilePicture,'') as ProfilePicture from ACRF_AdminDetails where Id=@Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    objModel.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objModel.AdminName = sdr["AdminName"].ToString();
                    objModel.Address = sdr["Address"].ToString();
                    objModel.ContactName = sdr["ContactName"].ToString();
                    objModel.Mobile = sdr["Mobile"].ToString();
                    objModel.Email = sdr["Email"].ToString();
                    objModel.FAX = sdr["FAX"].ToString();
                    objModel.SkypeId = sdr["SkypeId"].ToString();
                    objModel.MiscInfo = sdr["MiscInfo"].ToString();
                    objModel.Password = sdr["Password"].ToString();
                    objModel.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
                    objModel.PostalCode = sdr["PostalCode"].ToString();
                    objModel.CreatedBy = sdr["CreatedBy"].ToString();
                    objModel.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    objModel.Password = Encryption.decrypt(objModel.Password);
                    if (sdr["ProfilePicture"].ToString() == "")
                    {
                        objModel.ProfilePicture = "";
                    }
                    else
                    {
                        objModel.ProfilePicture = GlobalFunction.GetAPIUrl() + sdr["ProfilePicture"].ToString();
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return objModel;
        }






        #region UpdateAdminProfileImage

        public string UpdateAdminProfileImage(int adminId, string attachment_path)
        {
            string result = "Error on Uploading Profile Image";
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
                    sqlstr = "update ACRF_AdminDetails set  ProfilePicture=@ProfilePicture, updatedon=@updatedon "
                    + " where id=@id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", adminId);
                    cmd.Parameters.AddWithValue("@ProfilePicture", attachment_path);

                    cmd.Parameters.AddWithValue("@updatedon", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();


                    transaction.Commit();
                    connection.Close();
                    result = "Profile Image Updated Successfully!";
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



        #region UpdateAdminLoginPassword

        public string UpdateAdminLoginPassword(adminLoginPasswordModel objModel)
        {
            string result = "Error on updating admin login password!";
            try
            {
                bool chkres = CheckIfAdminExists(objModel);
                if (chkres == true)
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
                        sqlstr = "update ACRF_AdminDetails set Password=@Password, "
                        + " UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn where Id=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);
                        cmd.Parameters.AddWithValue("@Password", Encryption.encrypt(objModel.new_password));
                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Admin Login Password Updated Successfully!";
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
                    result = "Enter Valid Old Password";
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion


        public bool CheckIfAdminExists(adminLoginPasswordModel objModel)
        {
            bool result = false;

            try
            {
                string sqlstr = "select * from ACRF_AdminDetails where Id=@Id and Password=@Password";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                cmd.Parameters.AddWithValue("@Password", Encryption.encrypt(objModel.old_password));
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result = true;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }




        #region UpdateVendorLoginPassword

        public string UpdateVendorLoginPassword(adminLoginPasswordModel objModel)
        {
            string result = "Error on updating vendor login password!";
            try
            {
                bool chkres = CheckIfVendorExists(objModel);
                if (chkres == true)
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
                        sqlstr = "update ACRF_VendorDetails set Password=@Password, "
                        + " UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn where Id=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);
                        cmd.Parameters.AddWithValue("@Password", Encryption.encrypt(objModel.new_password));
                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Vendor Login Password Updated Successfully!";
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
                    result = "Enter Valid Old Password";
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion


        public bool CheckIfVendorExists(adminLoginPasswordModel objModel)
        {
            bool result = false;

            try
            {
                string sqlstr = "select * from ACRF_VendorDetails where Id=@Id and Password=@Password";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                cmd.Parameters.AddWithValue("@Password", Encryption.encrypt(objModel.old_password));
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result = true;
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }


    }
}