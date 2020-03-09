using ACRF_WebAPI.Models;
using EnCryptDecrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ACRF_WebAPI.Global
{
    public class GlobalFunction
    {
        public static string getLoggedInUser(string AuthorizationToken)
        {
            string strUserId = "";
            String Token = CryptorEngine.Decrypt(AuthorizationToken, true);
            string[] arrToken = Token.Split(' ');

            if (arrToken.Length > 1)
            {
                strUserId = arrToken[0];
            }
            return strUserId;
        }


        public List<UserRoleModel> GetUserRoles(string userId)
        {
            List<UserRoleModel> objList = new List<UserRoleModel>();

            UserRoleModel tempobj = new UserRoleModel();
            tempobj.RoleId  = UserType.AdminUser;
            objList.Add(tempobj);


            tempobj = new UserRoleModel();
            tempobj.RoleId = UserType.VendorUser;
            objList.Add(tempobj);


            tempobj = new UserRoleModel();
            tempobj.RoleId = UserType.EmployeeUser;
            objList.Add(tempobj);

            return objList;
        }



        public static string GetAPIUrl()
        {
            string APIUrl = WebConfigurationManager.AppSettings["WebAPIUrl"];

            return APIUrl;
        }



        public static string GetSendMessageApiKey()
        {
            string APIKey = WebConfigurationManager.AppSettings["MessageAPIKey"];

            return APIKey;
        }



    }
}