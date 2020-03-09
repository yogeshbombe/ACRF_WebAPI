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
    public class CustomerController : ApiController
    {
        CustomerDetailsViewModel objCustomerDetailsVM = new CustomerDetailsViewModel();


        #region api/CustomerDetails/AddCustomerDetails (Post)

        [Route("api/CustomerDetails/AddCustomerDetails")]
        [HttpPost]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddCustomerDetails(ACRF_CustomerDetailsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCustomerDetailsVM.CreateCustomerDetails(objModel);
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

        
        #region api/CustomerDetails/ViewAllCustomerDetails (Get)

        [Route("api/CustomerDetails/ViewAllCustomerDetails")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewAllCustomerDetails(int Id)
        {
            List<ACRF_CustomerDetailsModel> objList = new List<ACRF_CustomerDetailsModel>();
            try
            {
                objList = objCustomerDetailsVM.ListCustomerDetails(Id);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        #region api/CustomerDetails/UpdateCustomerDetails (Put)

        [Route("api/CustomerDetails/UpdateCustomerDetails")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateCustomerDetails(ACRF_CustomerDetailsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCustomerDetailsVM.UpdateCustomerDetails(objModel);
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

        
        #region api/CustomerDetails/DeleteCustomerDetails (Delete)

        [Route("api/CustomerDetails/DeleteCustomerDetails")]
        [HttpDelete]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult DeleteCustomerDetails(int id)
        {
            string result = "";
            if (id != 0)
            {
                try
                {
                    string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCustomerDetailsVM.DeleteCustomerDetails(id, CreatedBy);
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

        
        #region api/CustomerDetails/ViewOneCustomerDetails (Get)

        [Route("api/CustomerDetails/ViewOneCustomerDetails")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneCustomerDetails(int CustomerId)
        {
            ACRF_CustomerDetailsModel objList = new ACRF_CustomerDetailsModel();
            try
            {
                objList = objCustomerDetailsVM.GetOneCustomerDetails(CustomerId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion




        #region api/CustomerDetails/ViewCustomerDetailsByPage (Get)

        [Route("api/CustomerDetails/ViewCustomerDetailsByPage")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewCustomerDetailsByPage(int max, int page, string sort_col, string sort_dir, string search = null)
        {
            Paged_ACRF_CustomerDetailsModel objList = new Paged_ACRF_CustomerDetailsModel();
            try
            {
                objList = objCustomerDetailsVM.ListCustomerDetailsByPagination(max, page, search, sort_col,sort_dir);
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
