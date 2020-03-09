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
    public class QuotationController : ApiController
    {
        QuotationViewModel objQuotationVM = new QuotationViewModel();


        #region api/Quotation/ViewQuotationByPage (Get)

        [Route("api/Quotation/ViewQuotationByPage")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewQuotationByPage(int max, int page, string sort_col, string sort_dir, string search = null, int VendorId = 0)
        {
            Paged_ACRF_QuotationDetailsModel objList = new Paged_ACRF_QuotationDetailsModel();
            try
            {
                objList = objQuotationVM.ListQuotationByPagination(max, page, search, VendorId,sort_col,sort_dir);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Ok(new { results = objList });
        }

        #endregion




        #region api/Quotation/ViewOneQuotation (Get)

        [Route("api/Quotation/ViewOneQuotation")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult ViewOneQuotation(int QuotationId)
        {
            ACRF_QuotationModel objModel = new ACRF_QuotationModel();
            try
            {
                objModel = objQuotationVM.GetOneQuotation(QuotationId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objModel });
        }

        #endregion




        #region api/Quotation/UpdateQuotationStatus (Put)

        [Route("api/Quotation/UpdateQuotationStatus")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult UpdateQuotationStatus(QuotationStatusModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objQuotationVM.UpdateQuotationStatus(objModel);
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



    }
}
