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
    public class CurrencyExchangeRateController : ApiController
    {
        CurrencyExchangeRateViewModel objCERVM = new CurrencyExchangeRateViewModel();


        #region api/CurrencyExchangeRate/AddCurrencyExchangeRate (Post)

        [Route("api/CurrencyExchangeRate/AddCurrencyExchangeRate")]
        [HttpPost]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddCurrencyExchangeRate(ACRF_CurrencyExchangeRateModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCERVM.CreateCurrencyExchangeRate(objModel);
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


        #region api/CurrencyExchangeRate/UpdateCurrencyExchangeRate (Put)

        [Route("api/CurrencyExchangeRate/UpdateCurrencyExchangeRate")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateCurrencyExchangeRate(ACRF_CurrencyExchangeRateModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCERVM.UpdateCurrencyExchangeRate(objModel);
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


        #region api/CurrencyExchangeRate/DeleteCurrencyExchangeRate (Delete)

        [Route("api/CurrencyExchangeRate/DeleteCurrencyExchangeRate")]
        [HttpDelete]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult DeleteCurrencyExchangeRate(int CurrId)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCERVM.DeleteCurrencyExchangeRate(CurrId, CreatedBy);
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


        #region api/CurrencyExchangeRate/ViewAllCurrencyExchangeRate (Get)

        [Route("api/CurrencyExchangeRate/ViewAllCurrencyExchangeRate")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewAllCurrencyExchangeRate()
        {
            List<ACRF_CurrencyExchangeRateModel> objList = new List<ACRF_CurrencyExchangeRateModel>();
            try
            {
                objList = objCERVM.ListCurrencyExchangeRate();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        #region api/CurrencyExchangeRate/ViewOneCurrencyExchangeRate (Get)

        [Route("api/CurrencyExchangeRate/ViewOneCurrencyExchangeRate")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneCurrencyExchangeRate(int CurrId)
        {
            ACRF_CurrencyExchangeRateModel objList = new ACRF_CurrencyExchangeRateModel();
            try
            {
                objList = objCERVM.OneCurrencyExchangeRate(CurrId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion






        #region api/CurrencyExchangeRate/ViewCurrencyExchangeRateByPage (Get)

        [Route("api/CurrencyExchangeRate/ViewCurrencyExchangeRateByPage")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewCurrencyExchangeRateByPage(int max, int page, string sort_col, string sort_dir, string search = null)
        {
            Paged_ACRF_CurrencyExchangeRateModel objList = new Paged_ACRF_CurrencyExchangeRateModel();
            try
            {
                objList = objCERVM.ListCurrencyExchangeRateWithPagination(max, page, search,sort_col,sort_dir);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion



    }
}
