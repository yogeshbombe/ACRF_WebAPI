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
    public class EmployeeDetailsController : ApiController
    {
        EmployeeDetailsViewModel objEmployeeDetailsVM = new EmployeeDetailsViewModel();


        #region api/EmployeeDetails/AddEmployeeDetails (Post)

        [Route("api/EmployeeDetails/AddEmployeeDetails")]
        [HttpPost]
        //[SessionAuthorizeFilter(UserType.AdminVendor)]
            [SessionAuthorizeFilter]
        public IHttpActionResult AddEmployeeDetails(ACRF_EmployeeDetailsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objEmployeeDetailsVM.CreateEmployeeDetails(objModel);
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


        #region api/EmployeeDetails/ViewAllEmployeeDetails (Get)

        [Route("api/EmployeeDetails/ViewAllEmployeeDetails")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult ViewAllEmployeeDetails(int VendorId)
        {
            List<ACRF_EmployeeDetailsModel> objList = new List<ACRF_EmployeeDetailsModel>();
            try
            {
                objList = objEmployeeDetailsVM.ListEmployeeDetails(VendorId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        #region api/EmployeeDetails/UpdateEmployeeDetails (Put)

        [Route("api/EmployeeDetails/UpdateEmployeeDetails")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult UpdateEmployeeDetails(ACRF_EmployeeDetailsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objEmployeeDetailsVM.UpdateEmployeeDetails(objModel);
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


        #region api/EmployeeDetails/DeleteEmployeeDetails (Delete)

        [Route("api/EmployeeDetails/DeleteEmployeeDetails")]
        [HttpDelete]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult DeleteEmployeeDetails(int id)
        {
            string result = "";
            if (id != 0)
            {
                try
                {
                    string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objEmployeeDetailsVM.DeleteEmployeeDetails(id, CreatedBy);
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


        #region api/EmployeeDetails/ViewOneEmployeeDetails (Get)

        [Route("api/EmployeeDetails/ViewOneEmployeeDetails")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult ViewOneEmployeeDetails(int EmployeeId)
        {
            ACRF_EmployeeDetailsModel objList = new ACRF_EmployeeDetailsModel();
            try
            {
                objList = objEmployeeDetailsVM.GetOneEmployeeDetails(EmployeeId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion

        
        #region api/EmployeeDetails/ViewEmployeeDetailsByPage (Get)

        [Route("api/EmployeeDetails/ViewEmployeeDetailsByPage")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewEmployeeDetailsByPage(int max, int page, string sort_col, string sort_dir, string search = null)
        {
            Paged_ACRF_EmployeeDetailsModel objList = new Paged_ACRF_EmployeeDetailsModel();
            try
            {
                objList = objEmployeeDetailsVM.ListEmployeeDetailsByPagination(max, page, search,sort_col,sort_dir);
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
