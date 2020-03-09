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
    public class CargoRateSettingsettingsController : ApiController
    {
        CargoRateSettingsViewModel objCrRtVM = new CargoRateSettingsViewModel();

        #region api/CargoRateSettings/AddCargoRateSettings (Post)

        [Route("api/CargoRateSettings/AddCargoRateSettings")]
        [HttpPost]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddCargoRateSettings(ACRF_CargoRateSettingsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCrRtVM.CreateCargoRateSettings(objModel);
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


        #region api/CargoRateSettings/UpdateCargoRateSettings (Put)

        [Route("api/CargoRateSettings/UpdateCargoRateSettings")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateCargoRateSettings(ACRF_CargoRateSettingsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCrRtVM.UpdateCargoRateSettings(objModel);
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

        #region api/CargoRateSettings/ViewOneCargoRateSettings (Get)

        [Route("api/CargoRateSettings/ViewOneCargoRateSettings")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneCargoRateSettings(int Id)
        {
            ACRF_CargoRateSettingsModel objList = new ACRF_CargoRateSettingsModel();

            try
            {
                objList = objCrRtVM.GetOneCargoRateSettings(Id);
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
