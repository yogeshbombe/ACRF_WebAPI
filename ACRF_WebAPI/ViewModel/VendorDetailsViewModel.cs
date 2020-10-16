using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class VendorDetailsViewModel
    {
        #region Create Vendor Details

        public string CreateVendorDetails(ACRF_VendorDetailsModel objModel)
        {
            string result = "Error on Saving Project Details!";
            try
            {
                objModel = NullToBlank(objModel);
                if (objModel.Password != "")
                {
                    result = CheckIfVendorDetailsExists(objModel);
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
                            sqlstr = "insert into ProjectDetails(ProjectName,ManagerName,Mobile,Email,SkypeId"
                             + " ,Password,CreatedBy,CreatedOn,SprintStartDate,SprintEndDate,CurrentSprintName,Devhours,Testhours) "
                             + " values (@ProjectName,@ManagerName,@Mobile,@Email,@SkypeId"
                             + " ,@Password,@CreatedBy,@CreatedOn,@SprintStartDate,@SprintEndDate,@CurrentSprintName,@Devhours,@Testhours)";
                            cmd.CommandText = sqlstr;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ProjectName", objModel.ProjectName);
                            cmd.Parameters.AddWithValue("@ManagerName", objModel.ManagerName);
                            cmd.Parameters.AddWithValue("@Mobile", objModel.Mobile);
                            cmd.Parameters.AddWithValue("@Email", objModel.Email);
                            cmd.Parameters.AddWithValue("@SkypeId", objModel.SkypeId);
                            cmd.Parameters.AddWithValue("@Password", objModel.Password);
                            cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                            cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                            cmd.Parameters.AddWithValue("@SprintStartDate", objModel.SprintStartDate);
                            cmd.Parameters.AddWithValue("@SprintEndDate", objModel.SprintEndDate);
                            cmd.Parameters.AddWithValue("@CurrentSprintName", objModel.CurrentSprintName);
                            cmd.Parameters.AddWithValue("@Devhours", objModel.Devhours);
                            cmd.Parameters.AddWithValue("@Testhours", objModel.Testhours);
                            cmd.ExecuteNonQuery();


                            transaction.Commit();
                            connection.Close();
                            result = "Project Added Successfully!";
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
                    result = "Paasword can't be blank!";
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return result;
        }

        #endregion



        #region List Vendor Details

        public List<ACRF_VendorDetailsModel> ListVendorDetails()
        {
            List<ACRF_VendorDetailsModel> objList = new List<ACRF_VendorDetailsModel>();
            try
            {
                string sqlstr = "select Id,isnull(ProjectName,'''') as ProjectName, isnull(ManagerName,'''') as ManagerName,"
                +"isnull(Mobile,'''') as Mobile, isnull(Email,'''') as Email,isnull(SkypeId,'''') as SkypeId,isnull(LastLogin,'''') as LastLogin,  "
                +"isnull(CreatedBy,'''') as CreatedBy,isnull(createdon,'''') as CreatedOn," +
                " isnull(updatedby,'''') as updatedby, isnull(updatedon,'''') as updatedon,"
                +"SprintStartDate,SprintEndDate,isnull(CurrentSprintName,'''') as CurrentSprintName," +
                "isnull(Devhours,0) as Devhours," +
                "isnull(Testhours,0) as Testhours From ProjectDetails order by Id";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    ACRF_VendorDetailsModel tempobj = new ACRF_VendorDetailsModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.ProjectName = sdr["ProjectName"].ToString();
                    tempobj.Mobile = sdr["Mobile"].ToString();
                    tempobj.Email = sdr["Email"].ToString();
                    tempobj.Password = sdr["Password"].ToString();
                    tempobj.LastLogin = Convert.ToDateTime(sdr["LastLogin"].ToString());
                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    tempobj.ManagerName = sdr["ManagerName"].ToString();
                    //tempobj.SprintStartDate = Convert.ToDateTime(sdr["SprintStartDate"].ToString());
                    tempobj.SprintStartDate = sdr["SprintStartDate"].ToString();
                    //tempobj.SprintEndDate = Convert.ToDateTime(sdr["SprintEndDate"].ToString());
                    tempobj.SprintEndDate = sdr["SprintEndDate"].ToString();
                    tempobj.CurrentSprintName = sdr["CurrentSprintName"].ToString();
                    tempobj.Devhours = Convert.ToInt32(sdr["Devhours"].ToString());
                    tempobj.Testhours = Convert.ToInt32(sdr["Testhours"].ToString());
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



        #region Update Vendor Details

        public string UpdateVendorDetails(ACRF_VendorDetailsModel objModel)
        {
            string result = "Error on Updating Project Details!";
            try
            {
                objModel = NullToBlank(objModel);
                result = CheckIfVendorDetailsExists(objModel);
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
                        sqlstr = "update ProjectDetails set ProjectName=@ProjectName,ManagerName=@ManagerName,"
                         + " Mobile=@Mobile,Email=@Email,SkypeId=@SkypeId"
                         + " ,Password=@Password,UpdatedBy=@UpdatedBy,UpdatedOn=@UpdatedOn,SprintStartDate=@SprintStartDate"
                         + " ,SprintEndDate=@SprintEndDate, CurrentSprintName=@CurrentSprintName,Devhours=@Devhours,Testhours=@Testhours"
                         + " where Id=@Id ";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Id", objModel.Id);
                        cmd.Parameters.AddWithValue("@ProjectName", objModel.ProjectName);
                        cmd.Parameters.AddWithValue("@ManagerName", objModel.ManagerName);
                        cmd.Parameters.AddWithValue("@Mobile", objModel.Mobile);
                        cmd.Parameters.AddWithValue("@Email", objModel.Email);
                        cmd.Parameters.AddWithValue("@SkypeId", objModel.SkypeId);
                        cmd.Parameters.AddWithValue("@Password", objModel.Password);
                        cmd.Parameters.AddWithValue("@UpdatedBy", objModel.UpdatedBy);
                        cmd.Parameters.AddWithValue("@UpdatedOn", StandardDateTime.GetDateTime());
                        cmd.Parameters.AddWithValue("@SprintStartDate", objModel.SprintStartDate);
                        cmd.Parameters.AddWithValue("@SprintEndDate", objModel.SprintEndDate);
                        cmd.Parameters.AddWithValue("@CurrentSprintName", objModel.CurrentSprintName);
                        cmd.Parameters.AddWithValue("@Devhours", objModel.Devhours);
                        cmd.Parameters.AddWithValue("@Testhours", objModel.Testhours);
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Project Details Updated Successfully!";
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



        #region Delete Vendor Details

        public string DeleteVendorDetails(int id, string created_by)
        {
            string result = "Error on Deleting Project Details!";
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
                    sqlstr = "insert into ProjectDetails_Log(ProjectName,ManagerName,Mobile,Email,SkypeId"
                         + " ,Password,CreatedBy,CreatedOn,SprintStartDate,SprintEndDate,CurrentSprintName,Devhours,Testhours) "
                     + " select ProjectName,ManagerName,Mobile,Email,SkypeId"
                         + " ,Password,CreatedBy,CreatedOn,SprintStartDate,SprintEndDate,CurrentSprintName,Devhours,Testhours from ProjectDetails where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@CreatedBy", created_by);
                    cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    cmd.ExecuteNonQuery();


                    sqlstr = "delete from ProjectDetails where Id=@Id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();

                    
                    transaction.Commit();
                    connection.Close();
                    result = "Project Deleted Successfully!";
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



        #region Get One Vendor Details

        public ACRF_VendorDetailsModel GetOneVendorDetails(int VendorId)
        {
            ACRF_VendorDetailsModel objList = new ACRF_VendorDetailsModel();
            try
            {
                string sqlstr = "select Id,ProjectName,isnull(ManagerName,'') as ManagerName, "
                + " isnull(Mobile,'') as Mobile,isnull(SkypeId,'') as SkypeId,"
                + " isnull(Email,'') as Email, isnull(Password,'') as Password,"
                + " isnull(LastLogin,'') as LastLogin, isnull(CreatedBy,'') as CreatedBy, isnull(CreatedOn,'')  "
                + " as CreatedOn,SprintStartDate,SprintEndDate,isnull(CurrentSprintName,'''') as CurrentSprintName,isnull(Devhours,0) as Devhours,isnull(Testhours,0) as Testhours,isnull(profilepicture,'') as profilepicture From ProjectDetails where Id=@Id";
                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", VendorId);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    objList.Id = Convert.ToInt32(sdr["Id"].ToString());
                    objList.ProjectName = sdr["ProjectName"].ToString();
                    //objList.Address = sdr["Address"].ToString();
                    objList.ManagerName = sdr["ManagerName"].ToString();
                    objList.Mobile = sdr["Mobile"].ToString();
                    objList.Email = sdr["Email"].ToString();
                    objList.Password = sdr["Password"].ToString();
                    //objList.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
                    objList.LastLogin = Convert.ToDateTime(sdr["LastLogin"].ToString());
                    objList.CreatedBy = sdr["CreatedBy"].ToString();
                    objList.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    //objList.PostalCode = sdr["PostalCode"].ToString();
                    //objList.FAX = sdr["FAX"].ToString();
                    objList.SkypeId = sdr["SkypeId"].ToString();
                    //objList.Website = sdr["Website"].ToString();
                    // objList.MiscInfo = sdr["MiscInfo"].ToString();
                    //objList.SprintStartDate = Convert.ToDateTime(sdr["SprintStartDate"].ToString());
                    //objList.SprintEndDate = Convert.ToDateTime(sdr["SprintEndDate"].ToString());
                    objList.SprintStartDate = sdr["SprintStartDate"].ToString();
                    objList.SprintEndDate = sdr["SprintEndDate"].ToString();
                    objList.CurrentSprintName = sdr["CurrentSprintName"].ToString();
                    objList.Devhours = Convert.ToInt32(sdr["Devhours"].ToString());
                    objList.Testhours = Convert.ToInt32(sdr["Testhours"].ToString());
                    objList.Password = EnCryptDecrypt.Encryption.decrypt(objList.Password);
                    if(sdr["profilepicture"].ToString() == "")
                    {
                        objList.ProfilePicture = "";
                    }
                    else
                    {
                        objList.ProfilePicture = GlobalFunction.GetAPIUrl() + sdr["profilepicture"].ToString();
                    }
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



        #region Check If Vendor Details Already Exists

        public string CheckIfVendorDetailsExists(ACRF_VendorDetailsModel objModel)
        {
            string result = "";
            try
            {

                string sqlstr = "Select * from ProjectDetails Where ISNULL(Mobile,'')=@Mobile and Isnull(Id,0)!=@Id ";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Mobile", objModel.Mobile);
                cmd.Parameters.AddWithValue("@Email", objModel.Email);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (objModel.Mobile != "")
                {
                    while (sdr.Read())
                    {
                        result = "Mobile already exists!";
                    }
                }
                sdr.Close();




                sqlstr = "Select * from ProjectDetails Where Isnull(Id,0)!=@Id "
                 + " and Email=@Email ";
                cmd.Parameters.Clear();
                cmd.Connection = connection;
                cmd.CommandText = sqlstr;
                cmd.Parameters.AddWithValue("@Email", objModel.Email);
                cmd.Parameters.AddWithValue("@Id", objModel.Id);
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



        private ACRF_VendorDetailsModel NullToBlank(ACRF_VendorDetailsModel objModel)
        {
            //if(objModel.Address==null)
            //{
            //    objModel.Address = "";
            //}
            if(objModel.ManagerName == null)
            {
                objModel.ManagerName = "";
            }
            if(objModel.Email == null)
            {
                objModel.Email = "";
            }
            //if(objModel.FAX == null)
            //{
            //    objModel.FAX = "";
            //}
            //if(objModel.MiscInfo == null)
            //{
            //    objModel.MiscInfo = "";
            //}
            if(objModel.Mobile==null)
            {
                objModel.Mobile = "";
            }
            if(objModel.Password == null)
            {
                objModel.Password = "";
            }
            //if(objModel.PostalCode==null)
            //{
            //    objModel.PostalCode = "";
            //}
            if(objModel.SkypeId==null)
            {
                objModel.SkypeId = "";
            }
            if(objModel.ProjectName==null)
            {
                objModel.ProjectName = "";
            }
            //if(objModel.Website==null)
            //{
            //    objModel.Website = "";
            //}

            return objModel;
        }





        #region List Vendor Details By Pagination

        public Paged_ACRF_VendorDetailsModel ListVendorDetailsByPagination(int max, int page, string search, string sort_col, string sort_dir)
        {
            Paged_ACRF_VendorDetailsModel objPaged = new Paged_ACRF_VendorDetailsModel();
            List<ACRF_VendorDetailsModel> objList = new List<ACRF_VendorDetailsModel>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "[ACRF_GetProjectDetailsByPage]";

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
                    ACRF_VendorDetailsModel tempobj = new ACRF_VendorDetailsModel();
                    tempobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                    tempobj.ProjectName = sdr["ProjectName"].ToString();
                    //tempobj.Address = sdr["Address"].ToString();
                    tempobj.ManagerName = sdr["ManagerName"].ToString();
                    tempobj.Mobile = sdr["Mobile"].ToString();
                    tempobj.Email = sdr["Email"].ToString();
                   // tempobj.FAX = sdr["FAX"].ToString();
                    tempobj.SkypeId = sdr["SkypeId"].ToString();
                    //tempobj.Website = sdr["Website"].ToString();
                    //tempobj.MiscInfo = sdr["MiscInfo"].ToString();
                    //tempobj.CountryId = Convert.ToInt32(sdr["CountryId"].ToString());
                    //tempobj.PostalCode = sdr["PostalCode"].ToString();
                    tempobj.LastLogin = Convert.ToDateTime(sdr["LastLogin"].ToString());

                    tempobj.CreatedBy = sdr["CreatedBy"].ToString();
                    tempobj.CreatedOn = Convert.ToDateTime(sdr["CreatedOn"].ToString());
                    var usCulture = new System.Globalization.CultureInfo("en-US");
                   // tempobj.SprintStartDate= Convert.ToDateTime(sdr["SprintStartDate"].ToString()).ToShortDateString();
                    //tempobj.SprintEndDate = Convert.ToDateTime(sdr["SprintEndDate"].ToString());
                    tempobj.SprintStartDate = sdr["SprintStartDate"].ToString();
                    tempobj.SprintEndDate = sdr["SprintEndDate"].ToString();
                    tempobj.CurrentSprintName = sdr["CurrentSprintName"].ToString();
                    tempobj.Devhours = Convert.ToInt32(sdr["Devhours"].ToString());
                    tempobj.Testhours = Convert.ToInt32(sdr["Testhours"].ToString());
                    objList.Add(tempobj);
                }
                sdr.Close();
                objPaged.ACRF_VendorDetailsModelList = objList;


                sqlstr = "select count(*) as cnt from ProjectDetails where ProjectName like @search ";
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




        #region UpdateVendorProfileImage

        public string UpdateVendorProfileImage(int vendorId, string attachment_path)
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
                    sqlstr = "update ACRF_VendorDetails set  ProfilePicture=@ProfilePicture, updatedon=@updatedon "
                    + " where id=@id";
                    cmd.CommandText = sqlstr;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", vendorId);
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



    }
}