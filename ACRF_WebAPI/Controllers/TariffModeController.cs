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
    public class TariffModeController : ApiController
    {
        TariffModeViewModel objTariffModeVM = new TariffModeViewModel();


        #region api/TariffMode/AddTariffMode (Post)

        [Route("api/TariffMode/AddTariffMode")]
        [HttpPost]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddTariffMode(ACRF_TariffModeModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objTariffModeVM.CreateTariffMode(objModel);
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



        #region api/TariffMode/ViewTariffModeByPage (Get)

        [Route("api/TariffMode/ViewTariffModeByPage")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewTariffModeByPage(int max, int page, string sort_col, string sort_dir, string search = null)
        {
            Paged_ACRF_TariffModeModel objList = new Paged_ACRF_TariffModeModel();

            try
            {
                objList = objTariffModeVM.ListTariffMode(max, page, search,sort_col,sort_dir);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        
        #region api/TariffMode/ViewOneTariffMode (Get)

        [Route("api/TariffMode/ViewOneTariffMode")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneTariffMode(int Id)
        {
            ACRF_TariffModeModel objList = new ACRF_TariffModeModel();

            try
            {
                objList = objTariffModeVM.GetOneTariffMode(Id);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion

        

        #region api/TariffMode/ViewAllTariffMode (Get)

        [Route("api/TariffMode/ViewAllTariffMode")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewAllTariffMode()
        {
            List<ACRF_TariffModeModel> objList = new List<ACRF_TariffModeModel>();
            try
            {
                objList = objTariffModeVM.ListTariffMode();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion

        

        #region api/TariffMode/UpdateTariffMode (Put)

        [Route("api/TariffMode/UpdateTariffMode")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateTariffMode(ACRF_TariffModeModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objTariffModeVM.UpdateTariffMode(objModel);
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

        

        #region api/TariffMode/DeleteTariffMode (Delete)

        [Route("api/TariffMode/DeleteTariffMode")]
        [HttpDelete]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult DeleteTariffMode(int Id)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objTariffModeVM.DeleteTariffMode(Id, CreatedBy);
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
