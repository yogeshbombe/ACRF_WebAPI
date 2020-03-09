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
    public class AirportsController : ApiController
    {

        AirportsViewModel objAirportsVM = new AirportsViewModel();


        #region api/Airports/AddAirports (Post)

        [Route("api/Airports/AddAirports")]
        [HttpPost]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddAirports(ACRF_AirportsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objAirportsVM.CreateAirports(objModel);
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




        #region api/Airports/ViewOneAirports (Get)

        [Route("api/Airports/ViewOneAirports")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneAirports(int Id)
        {
            ACRF_AirportsModel objList = new ACRF_AirportsModel();

            try
            {
                objList = objAirportsVM.GetOneAirports(Id);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        

        #region api/Airports/ViewAllAirports (Get)

        [Route("api/Airports/ViewAllAirports")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewAllAirports()
        {
            List<ACRF_AirportsModel> objList = new List<ACRF_AirportsModel>();
            try
            {
                objList = objAirportsVM.ListAirports();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion




        #region api/Airports/UpdateAirports (Put)

        [Route("api/Airports/UpdateAirports")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateAirports(ACRF_AirportsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objAirportsVM.UpdateAirports(objModel);
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




        #region api/Airports/DeleteAirports (Delete)

        [Route("api/Airports/DeleteAirports")]
        [HttpDelete]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult DeleteAirports(int Id)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objAirportsVM.DeleteAirports(Id, CreatedBy);
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
