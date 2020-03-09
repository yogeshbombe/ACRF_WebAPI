using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public ResponseObject ResponseObject { get; set; }

        public ACRF_UserDetailsModel UserDetailModel { get; set; }

        public ResponseModel(string StatusCode, ResponseObject objResponse, ACRF_UserDetailsModel objUserDetail)
        {
            Status = StatusCode;
            ResponseObject = objResponse;
            UserDetailModel = objUserDetail;
        }
    }


    public class ResponseObject
    {
        public string ReturnVal { get; set; }
        public ResponseObject() {

        }
        public ResponseObject(string returnValue) {
            ReturnVal = returnValue;
        }
    }




    public class ResponseModel_
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }



    public class LoginResponse : ResponseObject
    {
        #region Properties
        public string Token { get; set; }
        public string Id { get; set; }

        public LoginResponse()
        {
            Token = String.Empty;
            Id = String.Empty;
        }
        public LoginResponse(string token, string id)
        {
            Token = token;
            Id = id;
        }
        #endregion
    }

}