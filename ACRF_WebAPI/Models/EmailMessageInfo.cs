using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ACRF_WebAPI.Models
{
    public class EmailMessageInfo
    {
        public int VendorId { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Enter valid from email address!")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string FromEmailAddress { get; set; }
        public string FromName { get; set; }

        //[DataType(DataType.EmailAddress, ErrorMessage = "Enter valid to email address!")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ToEmailAddress { get; set; }
        public string ToName { get; set; }

        public string CcEmailAddress { get; set; }

        public string BccEmailAddress { get; set; }

        public string EmailSubject { get; set; }

        public string EmailBody { get; set; }

        //public personalizations personalizations { get; set; }
        //public from from { get; set; }
        //public reply_to reply_to { get; set; }
        //public List<content> content { get; set; }

        public string DisplayRateName1 { get; set; }
        public string DisplayRateName2 { get; set; }
        public string DisplayRateName3 { get; set; }

        public bool IsRate1 { get; set; }
        public bool IsRate2 { get; set; }
        public bool IsRate3 { get; set; }
    }


    public class personalizations
    {
        public List<to> to { get; set; }
        public string subject { get; set; }
    }

    public class to
    {
        public string email { get; set; }
        public string name { get; set; }
    }
    public class from
    {
        public string email { get; set; }
        public string name { get; set; }
    }

    public class reply_to
    {
        public string email { get; set; }
        public string name { get; set; }
    }

    public class content
    {
        public string type { get; set; }
        public string value { get; set; }
    }


    public class sendMailModel
    {
        public EmailMessageInfo objMailModel { get; set; }

        public List<CargoRatesModel> objCRList { get; set; }
    }


    public class QuotationMessageInfo
    {
        public string ToEmailAddress { get; set; }

        public string ToMobileNumber { get; set; }
        public string ToName { get; set; }

        public string ToMobileName { get; set; }
        public string MessageBody { get; set; }


        public string DisplayRateName1 { get; set; }
        public string DisplayRateName2 { get; set; }
        public string DisplayRateName3 { get; set; }

        public bool IsRate1 { get; set; }
        public bool IsRate2 { get; set; }
        public bool IsRate3 { get; set; }


        public string Origin { get; set; }
        public string Destination { get; set; }
        public string GrossWeight { get; set; }
        public int TariffModeId { get; set; }
    }



    public class sendMessageQuotationModel
    {
        public QuotationMessageInfo objMailModel { get; set; }

        public List<CargoRatesModel> objCRList { get; set; }
    }
}