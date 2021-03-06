﻿using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.ViewModel
{
    public class EmployeeViewModel
    {
        #region List Employee Details By Pagination

        public Paged_EmployeeModel ListEmployeeDetailsByPagination(int max, int page, string search, string sort_col, string sort_dir)
        {
            Paged_EmployeeModel objPaged = new Paged_EmployeeModel();
            List<Employee> objList = new List<Employee>();
            try
            {
                if (search == null)
                {
                    search = "";
                }
                int startIndex = max * (page - 1);

                string sqlstr = "USP_GetEmployeeByPage";

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
                    Employee tempobj = new Employee();
                    tempobj.ID = Convert.ToInt32(sdr["ID"].ToString());
                    tempobj.EmpID = Convert.ToInt32(sdr["EmpID"].ToString());
                    tempobj.Email = sdr["Email"].ToString();
                    tempobj.Name = sdr["Name"].ToString();
                    tempobj.ManagerName = sdr["ManagerName"].ToString();
                    tempobj.Profile = Convert.ToInt32(sdr["ProfileID"].ToString());
                    tempobj.ProjectID = Convert.ToInt32(sdr["ProjectID"].ToString());

                    tempobj.ProfilelName = sdr["Profile"].ToString();
                    tempobj.ProjectName = sdr["ProjectName"].ToString();
                    tempobj.Status = Convert.ToInt32(sdr["Status"]);
                    tempobj.StatusName = sdr["StatusName"].ToString();
                    tempobj.Expertise= sdr["Experties"].ToString().Split(',').Select(x => int.Parse(x)).ToArray();
                    tempobj.stream = sdr["Stream"].ToString().Split(',').Select(x => int.Parse(x)).ToArray();
                    tempobj.ExpertiseDisplay = sdr["ExpertiseDisplay"].ToString();
                    tempobj.streamDisplay = sdr["streamDisplay"].ToString();
                    objList.Add(tempobj);
                }
                sdr.Close();
                objPaged.EmployeeModelList = objList;


                sqlstr = "select count(*) as cnt from tbl_Employee inner join tbl_DCTEmployee on tbl_Employee.EmpID=tbl_DCTEmployee.EmpID where tbl_DCTEmployee.EmpName like @search ";
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

        #region Create Country

        public string CreateEmployee(Employee objModel)
        {
            string result = "Error on Saving Country!";
            try
            {
                result = CheckIfEmployeeExists(objModel);
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
                        //sqlstr = "insert into tbl_Employee(EmpID,ManagerEmpID,Profile,ProjectID,Password,CreatedBy,CreatedOn) values (@EmpID,@CreatedBy,@CreatedOn)";
                        sqlstr = "insert into tbl_Employee(EmpID,Email,ManagerEmpID,Profile,ProjectID,Password,Status,Experties,Stream) values (@EmpID,@Email,@ManagerEmpID,@Profile,@ProjectID,@Password,@Status,@Experties,@Stream)";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@EmpID", objModel.EmpID);
                        cmd.Parameters.AddWithValue("@Email", objModel.Email);
                        cmd.Parameters.AddWithValue("@ManagerEmpID", objModel.ManagerEmpID);
                        cmd.Parameters.AddWithValue("@Profile", objModel.Profile);
                        cmd.Parameters.AddWithValue("@ProjectID", objModel.ProjectID);
                        cmd.Parameters.AddWithValue("@Password", EnCryptDecrypt.Encryption.encrypt(objModel.Password.Trim()));
                        cmd.Parameters.AddWithValue("@Status", objModel.Status);
                        cmd.Parameters.AddWithValue("@Experties", string.Join(",", objModel.Expertise));
                        cmd.Parameters.AddWithValue("@Stream", string.Join(",", objModel.stream));
                        //cmd.Parameters.AddWithValue("@CreatedBy", objModel.CreatedBy);
                        //cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                        cmd.ExecuteNonQuery();


                        transaction.Commit();
                        connection.Close();
                        result = "Employee Added Successfully!";
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

        #region Check If Employee Details Already Exists

        public string CheckIfEmployeeExists(Employee objModel)
        {
            string result = "";
            try
            {

                string sqlstr = "Select * from tbl_Employee Where ISNULL(EmpID,'')=@EmpID";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@EmpID", objModel.EmpID);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (objModel.EmpID > 0)
                {
                    while (sdr.Read())
                    {
                        result = "Employee already exists!";
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

        #region Get One Employee Details

        public Employee GetOneEmployee(int ID)
        {
            Employee objList = new Employee();
            try
            {
                
                string sqlstr = "select E.ID as ID,E.EmpID as EmpID,E.Email as Email, D.EmpName Name, E.ManagerEmpID ManagerEmpID, E.Profile as Profile,E.ProjectID as ProjectID, "
                + " E.Status as Status,S.Status as StatusName,E.Password as Password,P.ProfileName as ProfileName,PR.Project as ProjectName,E.Experties,E.Stream "
                + " from tbl_Employee E inner "
                + " join tbl_DCTEmployee D on D.EmpID = E.EmpID "
                + " inner join tbl_DCTEmployee F on F.EmpID = E.ManagerEmpID "
                + " inner join tbl_Profiles P on P.ID = E.Profile "
                + " inner join tbl_Status S on S.ID = E.Status "
                + " inner join tbl_Projects PR on PR.ID = E.ProjectID "
                + " where E.ID = @Id ";

                var connection = gConnection.Connection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlstr, connection);
                cmd.Parameters.AddWithValue("@Id", ID);
                cmd.CommandType = System.Data.CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    objList.ID = Convert.ToInt32(sdr["ID"]);
                    objList.EmpID = Convert.ToInt32(sdr["EmpID"].ToString());
                    objList.Email = sdr["Email"].ToString();
                   // objList.Name = sdr["Name"].ToString();
                    objList.ManagerEmpID = Convert.ToInt32(sdr["ManagerEmpID"]);
                    //objList.ManagerName = sdr["ManagerName"].ToString();
                    objList.Profile = Convert.ToInt32(sdr["Profile"].ToString());
                    objList.ProjectID = Convert.ToInt32(sdr["ProjectID"]);
                    objList.Status = Convert.ToInt32(sdr["Status"]);
                    objList.StatusName = sdr["StatusName"].ToString();
                    objList.Password = EnCryptDecrypt.Encryption.decrypt(sdr["Password"].ToString());
                    objList.ProfilelName = (sdr["ProfileName"].ToString());
                    objList.ProjectName = sdr["ProjectName"].ToString();
                    objList.Expertise = sdr["Experties"].ToString().Split(',').Select(x => int.Parse(x)).ToArray();
                    objList.stream = sdr["Stream"].ToString().Split(',').Select(x => int.Parse(x)).ToArray();
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

        public string UpdateEmployee(Employee objModel)
        {
            string result = "Error on Updating Employee Details!";
            try
            {
                //objModel = NullToBlank(objModel);
                result = CheckIfEmployeeExists(objModel);
                if (result == "Employee already exists!")
                {
                    objModel.Password = EnCryptDecrypt.Encryption.encrypt(objModel.Password.Trim());
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
                        sqlstr = "update tbl_Employee set EmpID=@EmpID,ManagerEmpID=@ManagerEmpID,Profile=@Profile,ProjectID=@ProjectID,Password=@Password,Status=@Status,Email=@Email,Experties=@Experties,Stream=@Stream where ID=@Id";
                        cmd.CommandText = sqlstr;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Id", objModel.ID);
                        cmd.Parameters.AddWithValue("@EmpID", objModel.EmpID);
                        cmd.Parameters.AddWithValue("@Email", objModel.Email);
                        //cmd.Parameters.AddWithValue("@ManagerName", objModel.ManagerName);
                        cmd.Parameters.AddWithValue("@ManagerEmpID", objModel.ManagerEmpID);
                        cmd.Parameters.AddWithValue("@Profile", objModel.Profile);
                        cmd.Parameters.AddWithValue("@ProjectID", objModel.ProjectID);
                        cmd.Parameters.AddWithValue("@Password", objModel.Password);
                        cmd.Parameters.AddWithValue("@Status", Convert.ToInt32(objModel.Status));
                        cmd.Parameters.AddWithValue("@Experties", string.Join(",", objModel.Expertise));
                        cmd.Parameters.AddWithValue("@Stream", string.Join(",", objModel.stream));
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

        #region Delete Employee

        public string DeleteEmplyee(int id)
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
                    //sqlstr = "insert into ACRF_VendorDetails_Log(Id, VendorName,Address,ContactName,Mobile,Email,FAX,SkypeId,Website,MiscInfo "
                    //     + " ,Password,CountryId,PostalCode,CreatedBy,CreatedOn) "
                    // + " select Id, VendorName,Address,ContactName,Mobile,Email,FAX,SkypeId,Website,MiscInfo "
                    //     + " ,Password,CountryId,PostalCode,@CreatedBy,@CreatedOn from ACRF_VendorDetails where Id=@Id";
                    //cmd.CommandText = sqlstr;
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@Id", id);
                    //cmd.Parameters.AddWithValue("@CreatedBy", created_by);
                    //cmd.Parameters.AddWithValue("@CreatedOn", StandardDateTime.GetDateTime());
                    //cmd.ExecuteNonQuery();


                    sqlstr = "delete from tbl_Employee where ID=@Id";
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


    }
}