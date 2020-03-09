using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Mail;
using ACRF_WebAPI.Models;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using ACRF_WebAPI.Global;

namespace ACRF_WebAPI.ViewModel
{
    public class SendGridEmailService
    {
        QuotationViewModel objQuotVM = new QuotationViewModel();
        VendorDetailsViewModel objVendorVM = new VendorDetailsViewModel();
        
        public string ExecuteMail(EmailMessageInfo objMailModel, List<CargoRatesModel> objCRList)
        {
            string result = "Error on Sending Mail!";
            try
            {
                var htmlData = getDataFromHtml(objCRList, objMailModel);
                string Key = ConfigurationManager.AppSettings["SendGridApiKey"];
                var apiKey = Environment.GetEnvironmentVariable(Key);
                var client = new SendGridClient(Key);
                var from = new EmailAddress(objMailModel.FromEmailAddress, objMailModel.FromName);
                var subject = objMailModel.EmailSubject;
                var to = new EmailAddress(objMailModel.ToEmailAddress, objMailModel.ToName);
                var plainTextContent = "";
                var htmlContent = htmlData;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = client.SendEmailAsync(msg);

                InsertQuotationData(objMailModel, objCRList);

                result = "Mail Sent Successfully!";
            }
            catch(Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return result;
        }

        //Added For Test
        public string ExecuteMailTest()
        {
            string result = "Error on Sending Mail!";
            try
            {
                var htmlData = "Test Email";//getDataFromHtml(objCRList, objMailModel);
                string Key = ConfigurationManager.AppSettings["SendGridApiKey"];
                var apiKey = Environment.GetEnvironmentVariable(Key);
                var client = new SendGridClient(Key);
                var from = new EmailAddress("ybombe26@gmail.com", "Yogesh");
                var subject = "Test Mail Subject Line";
                var to = new EmailAddress("yogeshbombe@gmail.com", "YogeshBombe");
                var plainTextContent = "";
                var htmlContent = htmlData;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = client.SendEmailAsync(msg);

                //InsertQuotationData(objMailModel, objCRList);

                result = "Mail Sent Successfully!";
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return result;
        }




        private string getDataFromHtml(List<CargoRatesModel> objCRList, EmailMessageInfo objMailModel)
        {
            string oCity = "";
            string dCity = "";
            string cargotariff = "";

            var fileContents = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/MailFormat/emailer.html"));
            string sirreplace = "";
            if(objMailModel.ToName != "" || objMailModel.ToName != null)
            {
                sirreplace = objMailModel.ToName;
            }
            else
            {
                sirreplace = "Sir";
            }
            

            string tabledata = "<table width='100%'> "
                       + " <thead style='background-color: #ddd;'> "
                + " <tr>"
                + " <th style='padding: 10px 0px 10px 0px;'>Airline</th>"
                + " <th style='padding: 10px 0px 10px 0px;'>Total</th>"
                + " <th style='padding: 10px 0px 10px 0px;'>Slab1</th>"
                + " <th style='padding: 10px 0px 10px 0px;'>Rate</th>"
                + " <th style='padding: 10px 0px 10px 0px;'>Freight</th>"
                + " <th style='padding: 10px 0px 10px 0px;'>FSC</th>"
                + " <th style='padding: 10px 0px 10px 0px;'>WSC</th>"
                + " <th style='padding: 10px 0px 10px 0px;'>X Ray</th>"
                + " <th style='padding: 10px 0px 10px 0px;'>MCC</th>"
                + " <th style='padding: 10px 0px 10px 0px;'>CTG</th>";
            if (objMailModel.IsRate1 == true)
            {
                tabledata = tabledata + " <th style='padding: 10px 0px 10px 0px;'>" + objMailModel.DisplayRateName1 + "</th>";
            }
            if (objMailModel.IsRate2 == true)
            {
                tabledata = tabledata + " <th style='padding: 10px 0px 10px 0px;'>" + objMailModel.DisplayRateName2 + "</th>";
            }
            if (objMailModel.IsRate3 == true)
            {
                tabledata = tabledata + " <th style='padding: 10px 0px 10px 0px;'>" + objMailModel.DisplayRateName3 + "</th>";
            }
            tabledata = tabledata + " "
                + " </tr>"
                + " </thead>"
                + " <tbody  style='background-color: #ece9e6;'>";
            foreach (var data in objCRList)
            {
                if (data.IsSelect == true)
                {
                    oCity = data.OCityCode;
                    dCity = data.DCityCode;
                    cargotariff = data.TariffMode;


                    tabledata = tabledata + " <tr>";
                    tabledata = tabledata + " <td  style='padding: 10px 0px 10px 0px;text-align:  center;'>";
                    tabledata = tabledata + data.Airline;
                    tabledata = tabledata + " </td>";

                    tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>";
                    tabledata = tabledata + data.TotalCost;
                    tabledata = tabledata + " </td>";

                    tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>";
                    tabledata = tabledata + data.Slab1;
                    tabledata = tabledata + " </td>";

                    tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>";
                    tabledata = tabledata + data.Rate;
                    tabledata = tabledata + " </td>";

                    tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>";
                    tabledata = tabledata + data.Freight;
                    tabledata = tabledata + " </td>";

                    tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>";
                    tabledata = tabledata + data.FSC;
                    tabledata = tabledata + " </td>";

                    tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>";
                    tabledata = tabledata + data.WSC;
                    tabledata = tabledata + " </td>";

                    tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>";
                    tabledata = tabledata + data.Xray;
                    tabledata = tabledata + " </td>";

                    tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>";
                    tabledata = tabledata + data.Mcc;
                    tabledata = tabledata + " </td>";

                    tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>";
                    tabledata = tabledata + data.Ctc;
                    tabledata = tabledata + " </td>";

                    if (objMailModel.IsRate1 == true)
                    {
                        tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>" + data.Rate1 + "</td>";
                    }
                    if (objMailModel.IsRate2 == true)
                    {
                        tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>" + data.Rate2 + "</td>";
                    }
                    if (objMailModel.IsRate3 == true)
                    {
                        tabledata = tabledata + " <td style='padding: 10px 0px 10px 0px;text-align:  center;'>" + data.Rate3 + "</td>";
                    }
                    
                    tabledata = tabledata + " <tr>";
                }
            }

            tabledata = tabledata + " </tbody></table>";

            string cargodetails = "Ex- " + oCity + " Airport to " + dCity + " Airport, for " + cargotariff + " cargo";
            string cargodetails2 = cargotariff + " Cargo Rates Ex- "+ oCity +" Airport to " + dCity + " Airport Currency: INR";
            
            fileContents = fileContents.Replace("dynamic_tabledata", tabledata);

            fileContents = fileContents.Replace("{{Sir}}", sirreplace);

            fileContents = fileContents.Replace("{{CargoDetails}}", cargodetails);
            fileContents = fileContents.Replace("{{CargoDetails2}}", cargodetails2);


            var vendordata = objVendorVM.GetOneVendorDetails(objMailModel.VendorId);

            fileContents = fileContents.Replace("{{VendorAddress}}", vendordata.Address);
            //fileContents = fileContents.Replace("{{VendorMobile}}", vendordata.Mobile);
            //fileContents = fileContents.Replace("{{VendorEmail}}", vendordata.Email);
            
            fileContents = fileContents.Replace("{{VendorName}}", vendordata.VendorName);


            return fileContents;
        }




        private string InsertQuotationData(EmailMessageInfo objMailModel, List<CargoRatesModel> objCRList)
        {
            string result = "";
            ACRF_QuotationModel objQuotModel = new ACRF_QuotationModel();
            List<ACRF_QuotationDetailsModel> objQuotDetList = new List<ACRF_QuotationDetailsModel>();

            objQuotModel.ClientMail = objMailModel.ToEmailAddress;
            objQuotModel.FromMail = objMailModel.FromEmailAddress;
            objQuotModel.MailSubject = objMailModel.EmailSubject;
            objQuotModel.QuotationStatus = QuotationType.InProgress;
            objQuotModel.VendorId = objMailModel.VendorId;
            objQuotModel.CreatedBy = objMailModel.FromEmailAddress;

            foreach (var data in objCRList)
            {
                if (data.IsSelect == true)
                {
                    ACRF_QuotationDetailsModel tempobj = new ACRF_QuotationDetailsModel();
                    tempobj.AirlineId = data.AirlineId;
                    tempobj.AMS = data.AMS;
                    tempobj.Ctc = data.Ctc;
                    tempobj.DAirportName = data.DAirportName;
                    tempobj.DCityCode = data.DCityCode;
                    tempobj.DCountryCode = data.DCountryCode;
                    tempobj.DisplayRateName1 = objMailModel.DisplayRateName1;
                    tempobj.DisplayRateName2 = objMailModel.DisplayRateName2;
                    tempobj.DisplayRateName3 = objMailModel.DisplayRateName3;
                    tempobj.Freight = data.Freight;
                    tempobj.FSC = data.FSC;
                    tempobj.Id = data.Id;
                    tempobj.IsRate1 = objMailModel.IsRate1;
                    tempobj.IsRate2 = objMailModel.IsRate2;
                    tempobj.IsRate3 = objMailModel.IsRate3;
                    tempobj.Mcc = data.Mcc;
                    tempobj.OAirportName = data.OAirportName;
                    tempobj.OCityCode = data.OCityCode;
                    tempobj.OCountryCode = data.OCountryCode;
                    tempobj.Rate = data.Rate;
                    tempobj.Rate1 = data.Rate1;
                    tempobj.Rate2 = data.Rate2;
                    tempobj.Rate3 = data.Rate3;
                    tempobj.Slab1 = data.Slab1;
                    tempobj.TariffModeId = data.TariffModeId;
                    tempobj.TotalCost = data.TotalCost;
                    tempobj.WSC = data.WSC;
                    tempobj.Xray = data.Xray;

                    objQuotDetList.Add(tempobj);
                }
            }
            objQuotModel.ACRF_QuotationDetailsModelList = objQuotDetList;

            result = objQuotVM.CreateQuotation(objQuotModel);
            return result;
        }





//        public void Execute(string frommail, string fromname, string rptomail, string rptoname, string toemail, string toname, string subject)
//        {
            
//            using (var client = new HttpClient())
//            {
//                EmailMessageInfo objMail = new EmailMessageInfo();
                

//                string data=@"{
//  'personalizations': [
//    {
//      'to': [
//        {
//          'email': 'manish.bhanumca@gmail.com',
//          'name': 'Manish Kumar'
//        }
//      ],
//      'subject': 'Hello, World!'
//    }
//  ],
//  'from': {
//    'email': 'estartdev@gmail.com',
//    'name': 'Estar'
//  },
//  'reply_to': {
//    'email': 'estartdev@gmail.com',
//    'name': 'Estar'
//  },
//  'content': [
//    {
//      'type': 'text/plain',
//      'value': 'Hi Manish'
//    }
//  ]
//}";

//                Object json = JsonConvert.DeserializeObject<Object>(data);

//                data = json.ToString();
                
//                client.BaseAddress = new Uri("https://api.sendgrid.com/");
//                client.DefaultRequestHeaders.Add("authorization", "Bearer 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");
//                //client..Add("content-type", "application/json");
//                var response = client.PostAsJsonAsync("v3/mail/send", json).Result;
//                if (response.IsSuccessStatusCode)
//                {
//                    //result = "Success";
//                }
//                else
//                {
//                    //result = "Error";
//                }
//            }

//        }


//        private from GetFromMail(string email, string name)
//        {
//            from objfrom = new from();
//            objfrom.email = email;
//            objfrom.name = name;
//            return objfrom;
//        }

//        private List<to> GetToMail(string email, string name)
//        {
//            List<to> objtlist = new List<to>();
//            to objto = new to();
//            objto.email = email;
//            objto.name = name;
//            objtlist.Add(objto);
//            return objtlist;
//        }

//        private reply_to GetReplyToMail(string email, string name)
//        {
//            reply_to objrpto = new reply_to();
//            objrpto.email = email;
//            objrpto.name = name;
//            return objrpto;
//        }

//        private personalizations GetPerso(string temail, string tname, string subj)
//        {
//            personalizations objPer = new personalizations();

//            objPer.to = GetToMail(temail, tname);
//            objPer.subject = subj;
//            return objPer;
//        }

//        private List<content> GetContent(string type, string value)
//        {
//            List<content> objList = new List<content>();
//            content objcon = new content();
//            objcon.type = type;
//            objcon.value = value;
//            objList.Add(objcon);
//            return objList;
//        }



//        //public SendGridEmailService()
//        //{
//        //    _client = new SendGridClient(apiKey);
//        //}

//        //public EmailResponse Send(EmailContract contract)
//        //{

//        //    var emailMessage = new SendGridMessage()
//        //    {
//        //        From = new EmailAddress(contract.FromEmailAddress, contract.Alias),
//        //        Subject = contract.Subject,
//        //        HtmlContent = contract.Body
//        //    };

//        //    emailMessage.AddTo(new EmailAddress(contract.ToEmailAddress));
//        //    if (!string.IsNullOrWhiteSpace(contract.BccEmailAddress))
//        //    {
//        //        emailMessage.AddBcc(new EmailAddress(contract.BccEmailAddress));
//        //    }

//        //    if (!string.IsNullOrWhiteSpace(contract.CcEmailAddress))
//        //    {
//        //        emailMessage.AddBcc(new EmailAddress(contract.CcEmailAddress));
//        //    }

//        //    return ProcessResponse(_client.SendEmailAsync(emailMessage).Result);
//        //}

//        //private EmailResponse ProcessResponse(Response response)
//        //{
//        //    if (response.StatusCode.Equals(System.Net.HttpStatusCode.Accepted)
//        //        || response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
//        //    {
//        //        return ToMailResponse(response);
//        //    }

//        //    //TODO check for null
//        //    var errorResponse = response.Body.ReadAsStringAsync().Result;

//        //    throw new EmailServiceException(response.StatusCode.ToString(), errorResponse);
//        //}

//        //private static EmailResponse ToMailResponse(Response response)
//        //{
//        //    if (response == null)
//        //        return null;

//        //    var headers = (HttpHeaders)response.Headers;
//        //    var messageId = headers.GetValues(MessageId).FirstOrDefault();
//        //    return new EmailResponse()
//        //    {
//        //        UniqueMessageId = messageId,
//        //        DateSent = DateTime.UtcNow,
//        //    };
//        //}

    }
}