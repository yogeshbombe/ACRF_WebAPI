using ACRF_WebAPI.App_Start;
using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using ACRF_WebAPI.ViewModel;
using EnCryptDecrypt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ACRF_WebAPI.Controllers
{
    public class AuthController : ApiController
    {

        AuthViewModel objAuthVM = new AuthViewModel();


        //[Route("api/Auth/Login")]
        //[HttpPost]
        //public IHttpActionResult Login(LoginModel _objLogin)
        //{
        //    string result = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            ACRF_UserDetailsModel ud = objAuthVM.UserAuth(_objLogin);
        //            if (ud != null)
        //            {
        //                ud.UserId = "Manish";
        //                ud.UserType = "Admin";
        //                if (ud.UserId != "")
        //                {
        //                    return Ok(new ResponseModel(RESPONSE_STATUS.SUCCESS, new LoginResponse(WriteCookie(ud.UserId, ud.UserType), ud.UserId), ud));
        //                }
        //                else
        //                {
        //                    return Ok(new ResponseModel(RESPONSE_STATUS.AUTH_UNAUTHORIZED, new LoginResponse(), new ACRF_UserDetailsModel()));
        //                }
        //            }
        //            else
        //            {
        //                return Ok(new ResponseModel(RESPONSE_STATUS.AUTH_UNAUTHORIZED, new LoginResponse(), new ACRF_UserDetailsModel()));
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            result = ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        result = "Enter Mandatory Fields";
        //    }
        //    return Ok(new { results = result });
        //}






        #region Login

        [Route("api/Auth/Login")]
        [HttpPost]
        public IHttpActionResult Login(LoginModel _objLogin)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    ACRF_UserDetailsModel ud = objAuthVM.UserAuth(_objLogin);

                    if (ud.email != "")
                    {
                        string token = WriteCookie(ud.email, ud.fullName, UserType.AdminUser, "");
                        string res = objAuthVM.UpdateLastLogin(UserType.AdminUser, ud.id);
                        ud.token = token;
                        return Ok(new { results = ud });
                    }
                    else
                    {
                        ud = objAuthVM.VendorAuth(_objLogin);
                        if (ud != null)
                        {
                            if (ud.email != "")
                            {
                                string token = WriteCookie(ud.email, ud.fullName, UserType.VendorUser, "");
                                string res = objAuthVM.UpdateLastLogin(UserType.VendorUser, ud.id);
                                ud.token = token;
                                //return Ok(new ResponseModel(RESPONSE_STATUS.SUCCESS, new LoginResponse(WriteCookie(ud.UserId, ud.UserType), ud.UserId), ud));
                                return Ok(new { results = ud });
                            }
                            else
                            {
                                ud.token = "";
                                //return Ok(new ResponseModel(RESPONSE_STATUS.AUTH_UNAUTHORIZED, new LoginResponse(), new ACRF_UserDetailsModel()));
                                return Ok(new { results = ud });
                            }
                        }
                        else
                        {
                            return Ok(new ResponseModel(RESPONSE_STATUS.AUTH_UNAUTHORIZED, new LoginResponse(), new ACRF_UserDetailsModel()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            else
            {
                result = "Enter Mandatory Fields";
            }
            return Ok(new { results = result });
        }

        #endregion



        #region Vendor Login

        [Route("api/Auth/VendorLogin")]
        [HttpPost]
        public IHttpActionResult VendorLogin(LoginModel _objLogin)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    ACRF_UserDetailsModel ud = objAuthVM.VendorAuth(_objLogin);
                    if (ud != null)
                    {
                        if (ud.email != "")
                        {
                            string token = WriteCookie(ud.email, ud.fullName, UserType.VendorUser, "");
                            string res = objAuthVM.UpdateLastLogin(UserType.VendorUser, ud.id);
                            ud.token = token;
                            //return Ok(new ResponseModel(RESPONSE_STATUS.SUCCESS, new LoginResponse(WriteCookie(ud.UserId, ud.UserType), ud.UserId), ud));
                            return Ok(new { results = ud });
                        }
                        else
                        {
                            ud.token = "";
                            //return Ok(new ResponseModel(RESPONSE_STATUS.AUTH_UNAUTHORIZED, new LoginResponse(), new ACRF_UserDetailsModel()));
                            return Ok(new { results = ud });
                        }
                    }
                    else
                    {
                        return Ok(new ResponseModel(RESPONSE_STATUS.AUTH_UNAUTHORIZED, new LoginResponse(), new ACRF_UserDetailsModel()));
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            else
            {
                result = "Enter Mandatory Fields";
            }
            return Ok(new { results = result });
        }

        #endregion



        #region Employee Login

        [Route("api/Auth/EmployeeLogin")]
        [HttpPost]
        public IHttpActionResult EmployeeLogin(LoginModel _objLogin)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    ACRF_UserDetailsModel ud = objAuthVM.EmployeeAuth(_objLogin);
                    if (ud != null)
                    {
                        if (ud.email != "")
                        {
                            string token = WriteCookie(ud.email, ud.fullName, UserType.EmployeeUser, "");
                            string res = objAuthVM.UpdateLastLogin(UserType.EmployeeUser, ud.id);
                            ud.token = token;
                            //return Ok(new ResponseModel(RESPONSE_STATUS.SUCCESS, new LoginResponse(WriteCookie(ud.UserId, ud.UserType), ud.UserId), ud));
                            return Ok(new { results = ud });
                        }
                        else
                        {
                            return Ok(new ResponseModel(RESPONSE_STATUS.AUTH_UNAUTHORIZED, new LoginResponse(), new ACRF_UserDetailsModel()));
                        }
                    }
                    else
                    {
                        return Ok(new ResponseModel(RESPONSE_STATUS.AUTH_UNAUTHORIZED, new LoginResponse(), new ACRF_UserDetailsModel()));
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            else
            {
                result = "Enter Mandatory Fields";
            }
            return Ok(new { results = result });
        }

        #endregion



        #region Verify Admin

        [Route("api/Auth/VerifyAdmin")]
        [HttpPost]
        public IHttpActionResult VerifyAdmin(AuthModel _objLogin)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    ACRF_UserDetailsModel ud = objAuthVM.AdminAuth(_objLogin);
                    if (ud != null)
                    {
                        if (ud.email != "")
                        {
                            string token = WriteCookie(ud.email, ud.fullName, UserType.AdminUser, ud.profileimage);
                            ud.token = token;
                            //return Ok(new ResponseModel(RESPONSE_STATUS.SUCCESS, new LoginResponse(WriteCookie(ud.UserId, ud.UserType), ud.UserId), ud));
                            return Ok(new { results = ud });
                        }
                        else
                        {
                            return Ok(new ResponseModel(RESPONSE_STATUS.AUTH_UNAUTHORIZED, new LoginResponse(), new ACRF_UserDetailsModel()));
                        }
                    }
                    else
                    {
                        return Ok(new ResponseModel(RESPONSE_STATUS.AUTH_UNAUTHORIZED, new LoginResponse(), new ACRF_UserDetailsModel()));
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }
            }
            else
            {
                result = "Enter Mandatory Fields";
            }
            return Ok(new { results = result });
        }

        #endregion



        #region api/Auth/ViewAdminProfile (Get)

        [Route("api/Auth/ViewAdminProfile")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewAdminProfile(int id)
        {
            ACRF_AdminDetailsModel objList = new ACRF_AdminDetailsModel();
            try
            {
                objList = objAuthVM.GetAdminProfile(id);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        #region api/Auth/UpdateAdminProfile (Put)

        [Route("api/Auth/UpdateAdminProfile")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateAdminProfile(ACRF_AdminDetailsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objAuthVM.UpdateAdminProfile(objModel);
                }
                catch (Exception ex)
                {
                    ErrorHandlerClass.LogError(ex);
                    result = ex.Message;
                }
            }
            else
            {
                result = "Enter Valid Mandatory Fields";
            }
            return Ok(new { results = result });
        }

        #endregion



        private string WriteCookie(string strId, string uName, string uType, string pimage)
        {
            //string Token = strId + " " + uType + " " + DateTime.Now.ToString();
            string Token = strId + " " + uType + " " + uName + " " + pimage;
            string strCookieValue = CryptorEngine.Encrypt(Token, true);// + " " + CryptorEngine.Encrypt(DateTime.Now.ToShortDateString(), true);
            //HttpCookie userTokenCookie = HttpContext.Current.Request.Cookies[strId];

            HttpCookie userTokenCookie = new HttpCookie(strId);
            userTokenCookie.Expires.AddDays(365);
            userTokenCookie.Value = strCookieValue;

            HttpContext.Current.Response.Cookies.Add(userTokenCookie);

            return strCookieValue;
        }




        #region api/Auth/UploadAdminImage

        [HttpPost]
        [Route("api/Auth/UploadAdminImage")]
        public HttpResponseMessage UploadAdminImage()
        {
            try
            {
                string fileName = null;
                var httpRequest = HttpContext.Current.Request;
                var postedfile = httpRequest.Files["Image"];

                int adminId = Convert.ToInt32(httpRequest["AdminId"].ToString());

                fileName = new String(Path.GetFileNameWithoutExtension(postedfile.FileName).Take(10).ToArray()).Replace(" ", "-");
                fileName = adminId + "_" + DateTime.Now.ToString("ddMMMyyyy") + Path.GetExtension(postedfile.FileName);
                string ext = Path.GetFileName(postedfile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/ProfileImage/Admin/" + fileName);
                postedfile.SaveAs(filePath);

                string result = objAuthVM.UpdateAdminProfileImage(adminId, "/ProfileImage/Admin/" + fileName);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        #endregion




        #region UpdateAdminLoginPassword (PUT)

        [HttpPut]
        [Route("api/Auth/UpdateAdminLoginPassword")]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateAdminLoginPassword(adminLoginPasswordModel objModel)
        {
            if (ModelState.IsValid)
            {
                string result = "";
                if (objModel.confirm_password == objModel.new_password)
                {
                    objModel.UpdatedBy = "";
                    result = objAuthVM.UpdateAdminLoginPassword(objModel);
                }
                else
                {
                    result = "New Password and Confirm Password should be same!";
                }

                return Ok(new { results = result });
            }
            else
            {
                return Ok(new { results = "Enter Mandatory Fields!" });
            }
        }

        #endregion



        #region UpdateVendorLoginPassword (PUT)

        [HttpPut]
        [Route("api/Auth/UpdateVendorLoginPassword")]
        [SessionAuthorizeFilter(UserType.VendorUser)]
        public IHttpActionResult UpdateVendorLoginPassword(adminLoginPasswordModel objModel)
        {
            if (ModelState.IsValid)
            {
                string result = "";
                if (objModel.confirm_password == objModel.new_password)
                {
                    objModel.UpdatedBy = "";
                    result = objAuthVM.UpdateVendorLoginPassword(objModel);
                }
                else
                {
                    result = "New Password and Confirm Password should be same!";
                }

                return Ok(new { results = result });
            }
            else
            {
                return Ok(new { results = "Enter Mandatory Fields!" });
            }
        }

        #endregion


    }
}
