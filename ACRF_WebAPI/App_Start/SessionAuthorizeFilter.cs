using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using EnCryptDecrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ACRF_WebAPI.App_Start
{
    public class SessionAuthorizeFilter : ActionFilterAttribute
    {
        GlobalFunction objGFunc = new GlobalFunction();

        string AllowedRoles = "";
        public SessionAuthorizeFilter(string roleList = "")
        {
            AllowedRoles = roleList;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string strTokenMessage = "";
            HttpContext.Current.Server.ScriptTimeout = 30000;
            try
            {
                var test = actionContext.Request.Headers.GetCookies();
                strTokenMessage = CheckToken(actionContext.Request.Headers.GetValues("Token").First());

                if (!String.IsNullOrEmpty(strTokenMessage))
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent(RESPONSE_STATUS.AUTH_TOKEN_EXPIRED)
                    };
                    return;

                }
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(RESPONSE_STATUS.AUTH_TOKEN_MISSING)
                };
                return;
            }
        }
        private string CheckToken(string strAuthorizationToken)
        {
            string strTokenMessage = RESPONSE_STATUS.AUTH_TOKEN_EXPIRED;
            try
            {
                String Token = CryptorEngine.Decrypt(strAuthorizationToken, true);
                string[] arrToken = Token.Split(' ');
                //IUserBusinessLogic ubl = new UserBusinessLogic();

                if (arrToken.Length > 1)
                {
                    string strUserId = arrToken[0];
                    GlobalVariables.gUser = strUserId;
                    //if (!ubl.GetSession(strUserId))
                    //    return strTokenMessage;

                    HttpCookie userTokenCookie = HttpContext.Current.Request.Cookies[strUserId];

                    if (false)//(userTokenCookie == null)
                    {
                        return strTokenMessage;
                    }
                    else
                    {
                        //if (DateTime.Now.AddDays(-1) > Convert.ToDateTime(arrToken[2]).AddHours(6))
                        //{
                        //    return strTokenMessage + "_" + DateTime.Now.AddDays(-1).ToString() + "_" + Convert.ToDateTime(arrToken[2]).AddHours(6).ToString() + "_";
                        //}
                        //else
                        //{
                        //    strTokenMessage = String.Empty;
                        //    return strTokenMessage;
                        //}


                        List<UserRoleModel> umr = objGFunc.GetUserRoles(strUserId);
                        bool roleFound = false;
                        string[] rolesList = AllowedRoles.Split(',');
                        foreach (UserRoleModel rm in umr)
                        {
                            foreach (string str in rolesList)
                            {
                                if (rm.Name == str.Trim())
                                {
                                    roleFound = true;
                                    break;
                                }
                            }
                        }

                        strTokenMessage = String.Empty;
                            return strTokenMessage;
                    }
                }
                else
                {
                    return strTokenMessage;
                }
            }
            catch (Exception ex)
            {

            }
            return strTokenMessage;
        }

    }
}