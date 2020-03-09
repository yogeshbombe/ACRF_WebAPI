using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using ACRF_WebAPI.ViewModel;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ACRF_WebAPI.Controllers
{
    public class SendMailController : ApiController
    {

        SendGridEmailService objSGridVM = new SendGridEmailService();
        


        #region api/SendMail/SendMailForQuotation (Post)

        [Route("api/SendMail/SendMailForQuotation")]
        [HttpPost]
        //public async void SendMailForQuotation(EmailMessageInfo objMailModel, List<CargoRatesModel> objCRList)
        public IHttpActionResult SendMailForQuotation(sendMailModel objMail)
        {
            string result = "Error on sending mail!";
            if (ModelState.IsValid)
            {
                try
                {
                    int smail = 0;
                    foreach(var data in objMail.objCRList)
                    {
                        if(data.IsSelect==true)
                        {
                            smail = 1;
                        }
                    }

                    if (smail == 1)
                    {
                        // Make an API call, and save the response
                        result= objSGridVM.ExecuteMail(objMail.objMailModel, objMail.objCRList);
                    }
                    else
                    {
                        result = "Please select rate to send mail!";
                    }
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


        #region api/SendMail/SendMailTest (Post)

        [Route("api/SendMail/SendMailTest")]
        [HttpPost]
        //public async void SendMailForQuotation(EmailMessageInfo objMailModel, List<CargoRatesModel> objCRList)
        public IHttpActionResult SendMailTest()
        {
            string result = "Error on sending mail!";
            if (ModelState.IsValid)
            {
                try
                {
                    // Make an API call, and save the response
                    result = objSGridVM.ExecuteMailTest();

                    //int smail = 0;
                    //foreach (var data in objMail.objCRList)
                    //{
                    //    if (data.IsSelect == true)
                    //    {
                    //        smail = 1;
                    //    }
                    //}

                    //if (smail == 1)
                    //{
                    //    // Make an API call, and save the response
                    //    result = objSGridVM.ExecuteMail(objMail.objMailModel, objMail.objCRList);
                    //}
                    //else
                    //{
                    //    result = "Please select rate to send mail!";
                    //}
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

        private async Task SetResponseInfoContainers(Response apiResponse)
        {
            var Visible = true;
            //var InnerText = $"Statuscode {(int)apiResponse.StatusCode}: {apiResponse.StatusCode}";
            var InnerText1 = await apiResponse.Body.ReadAsStringAsync();
        }

    }
}
