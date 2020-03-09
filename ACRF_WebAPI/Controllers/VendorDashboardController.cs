using ACRF_WebAPI.App_Start;
using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using ACRF_WebAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ACRF_WebAPI.Controllers
{
    public class VendorDashboardController : ApiController
    {
        VendorDashboardViewModel objVDVM = new VendorDashboardViewModel();

        [Route("api/VendorDashboard/GetQuotationStatus")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult GetQuotationStatus(int VendorId)
        {
            QuotationStatusCount objModel = new QuotationStatusCount();

            objModel = objVDVM.GetQuotationStatus(VendorId);

            return Ok(new { results = objModel });
        }




        [Route("api/VendorDashboard/GetQuotationAmountForLastTweleveMonths")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult GetQuotationAmountForLastTweleveMonths(int VendorId)
        {
            DisplayChart objDispChart = new DisplayChart();
            try
            {
                objDispChart = objVDVM.GetQuotationAmountForLastTweleveMonths(VendorId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Ok(new { results = objDispChart });
        }





        [Route("api/VendorDashboard/GetQuotationAmountForLastTweleveMonthsCompleted")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult GetQuotationAmountForLastTweleveMonthsCompleted(int VendorId)
        {
            DisplayChart objDispChart = new DisplayChart();
            try
            {
                objDispChart = objVDVM.GetQuotationAmountForLastTweleveMonthsCompleted(VendorId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Ok(new { results = objDispChart });
        }


        


        [Route("api/VendorDashboard/GetQuotationSentCompletedCount")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult GetQuotationSentCompletedCount(int VendorId)
        {
            QuotationSentCompletedCount objDModel = new QuotationSentCompletedCount();
            try
            {
                objDModel = objVDVM.GetQuotationSentCompletedCount(VendorId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Ok(new { results = objDModel });
        }






        [Route("api/VendorDashboard/GetMonthlySale")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult GetMonthlySale(int VendorId)
        {
            List<MonthlySale> objDModel = new List<MonthlySale>();
            try
            {
                objDModel = objVDVM.GetMonthlySale(VendorId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Ok(new { results = objDModel });
        }






        [Route("api/VendorDashboard/GetQuotationCountForLastTweleveMonthsCompleted")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult GetQuotationCountForLastTweleveMonthsCompleted(int VendorId)
        {
            DisplayMultiChart objDispChart = new DisplayMultiChart();
            try
            {
                objDispChart = objVDVM.GetQuotationCountForLastTweleveMonthsCompleted(VendorId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Ok(new { results = objDispChart });
        }



    }
}
