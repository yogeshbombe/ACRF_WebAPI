using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using ACRF_WebAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ACRF_WebAPI.Controllers
{
    public class SendMessageController : ApiController
    {
        [HttpPost]
        [Route("api/SendMessage/sendSMSForQuotation")]
        public IHttpActionResult sendSMS(sendMessageQuotationModel objModel)
        {
            string result = "";
            string tariffMode = "";
            if (ModelState.IsValid)
            {
                try
                {
                    int smail = 0;
                    foreach (var data in objModel.objCRList)
                    {
                        if (data.IsSelect == true)
                        {
                            smail = 1;
                        }
                    }

                    if (smail == 1)
                    {
                        if (objModel.objMailModel.ToEmailAddress != "" || objModel.objMailModel.ToEmailAddress != null)
                        {
                            CustomerDetailsViewModel objCustVM = new CustomerDetailsViewModel();
                            objModel.objMailModel.ToMobileName = objCustVM.GetOneCustomerMobileNumbnerFromEmailId(objModel.objMailModel.ToEmailAddress);
                        }
                        if (objModel.objMailModel.TariffModeId > 0)
                        {
                            TariffModeViewModel objTariffVM = new TariffModeViewModel();
                            var tariffData = objTariffVM.GetOneTariffMode(objModel.objMailModel.TariffModeId);
                            tariffMode = tariffData.TariffMode;
                        }

                        String message = HttpUtility.UrlEncode(getData(objModel, tariffMode));
                        using (var wb = new WebClient())
                        {
                            byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                            {
                            {"apikey" , GlobalFunction.GetSendMessageApiKey()},
                            {"numbers" , objModel.objMailModel.ToMobileNumber},
                            {"message" , message},
                            {"sender" , "MYACRF"}
                            });
                            result = System.Text.Encoding.UTF8.GetString(response);


                            RootObject objrespon = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result);

                            if (objrespon.status == "failure")
                            {
                                result = objrespon.warnings[0].message.ToString();
                                result = result + " , " + objrespon.errors[0].message.ToString();
                            }
                            else
                            {
                                result = "Message sent successfully!";
                            }
                        }
                    }
                    else
                    {
                        result = "Please select rate to send message!";
                    }
                }
                catch (Exception ex)
                {
                    ErrorHandlerClass.LogError(ex);
                    result = "System Error on sending message!";
                }
            }
            else
            {
                result = "Enter Valid Credentials!";
            }
            return Ok(new { results = result });
        }




        private string getData(sendMessageQuotationModel objModel, string tariffMode)
        {

            string destination = DestinationMasterViewModel.GetCityNameFromCode(objModel.objMailModel.Destination);
            string origin = DestinationMasterViewModel.GetCityNameFromCode(objModel.objMailModel.Origin);

            string data = "";
            if (objModel.objMailModel.ToMobileName != "")
            {
                data = data + "Dear " + objModel.objMailModel.ToMobileName + ",";
            }
            else
            {
                data = data + "Dear sir,";
            }
            data = data + "Here is your quotation rate for " + tariffMode.ToUpper();
            if (origin != "")
            {
                data = data + " From " + origin.ToUpper();
            }
            else
            {
                data = data + " From " + objModel.objMailModel.Origin.ToUpper();
            }
            if (destination != "")
            {
                data = data + " To " + destination.ToUpper();
            }
            else
            {
                data = data + " To " + objModel.objMailModel.Destination.ToUpper();
            }

            data = data + " with " + objModel.objMailModel.GrossWeight + "KG. Airlines list with price are: ";

            foreach (var qdata in objModel.objCRList)
            {
                data = data + qdata.Airline.ToUpper() + "- " + qdata.TotalCost + ", ";
            }

            data = data + " Thanks.";
            return data;
        }

    }
}
