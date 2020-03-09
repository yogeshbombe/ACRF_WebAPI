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
    public class DestinationMasterController : ApiController
    {
        DestinationMasterViewModel objDestVM = new DestinationMasterViewModel();


        #region api/DestinationMaster/AddDestinationMaster (Post)

        [Route("api/DestinationMaster/AddDestinationMaster")]
        [HttpPost]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddDestinationMaster(ACRF_DestinationMasterModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objDestVM.CreateDestinationMaster(objModel);
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




        #region api/DestinationMaster/ViewOneDestinationMaster (Get)

        [Route("api/DestinationMaster/ViewOneDestinationMaster")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneDestinationMaster(int Id)
        {
            ACRF_DestinationMasterModel objList = new ACRF_DestinationMasterModel();

            try
            {
                objList = objDestVM.GetOneDestinationMaster(Id);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion
        



        #region api/DestinationMaster/ViewAllDestinationMaster (Get)

        [Route("api/DestinationMaster/ViewAllDestinationMaster")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewAllDestinationMaster()
        {
            List<ACRF_DestinationMasterModel> objList = new List<ACRF_DestinationMasterModel>();
            try
            {
                objList = objDestVM.ListDestinationMaster();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion




        #region api/DestinationMaster/UpdateDestinationMaster (Put)

        [Route("api/DestinationMaster/UpdateDestinationMaster")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateDestinationMaster(ACRF_DestinationMasterModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objDestVM.UpdateDestinationMaster(objModel);
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




        #region api/DestinationMaster/DeleteDestinationMaster (Delete)

        [Route("api/DestinationMaster/DeleteDestinationMaster")]
        [HttpDelete]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult DeleteDestinationMaster(int Id)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objDestVM.DeleteDestinationMaster(Id, CreatedBy);
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




        #region api/DestinationMaster/ViewDestinationMasterByPage (Get)

        [Route("api/DestinationMaster/ViewDestinationMasterByPage")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewDestinationMasterByPage(int max, int page, string sort_col, string sort_dir, string search = null)
        {
            Paged_ACRF_DestinationMasterModel objList = new Paged_ACRF_DestinationMasterModel();

            try
            {
                objList = objDestVM.ListDestinationMasterByPage(max, page, search, sort_col, sort_dir);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion



        #region api/DestinationMaster/ListDestinationMasterSearch (Get)

        [Route("api/DestinationMaster/ListDestinationMasterSearch")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult ListDestinationMasterSearch(string _prefix)
        {
            List<ACRF_DestinationSearchModel> objList = new List<ACRF_DestinationSearchModel>();
            try
            {
                objList = objDestVM.ListDestinationMasterByPrefix(_prefix);
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
