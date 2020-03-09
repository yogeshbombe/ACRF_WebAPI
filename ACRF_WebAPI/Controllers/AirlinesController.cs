using ACRF_WebAPI.App_Start;
using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using ACRF_WebAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ACRF_WebAPI.Controllers
{
    public class AirlinesController : ApiController
    {
        AirlinesViewModel objAirlinesVM = new AirlinesViewModel();


        #region api/Airlines/AddAirlines (Post)

        [Route("api/Airlines/AddAirlines")]
        [HttpPost]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddAirlines(ACRF_AirlinesModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objAirlinesVM.CreateAirlines(objModel);
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




        #region api/Airlines/ViewAirlinesByPage (Get)

        [Route("api/Airlines/ViewAirlinesByPage")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewAirlinesByPage(int max, int page, string sort_col, string sort_dir, string search = null)
        {
            Paged_ACRF_AirlinesModel objList = new Paged_ACRF_AirlinesModel();

            try
            {
                objList = objAirlinesVM.ListAirlines(max,page,search, sort_col, sort_dir);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion





        #region api/Airlines/ViewOneAirlines (Get)

        [Route("api/Airlines/ViewOneAirlines")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneAirlines(int Id)
        {
            ACRF_AirlinesModel objList = new ACRF_AirlinesModel();

            try
            {
                objList = objAirlinesVM.GetOneAirlines(Id);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion






        #region api/Airlines/ViewAllAirlines (Get)

        [Route("api/Airlines/ViewAllAirlines")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewAllAirlines()
        {
            List<ACRF_AirlinesModel> objList = new List<ACRF_AirlinesModel>();
            try
            {
                objList = objAirlinesVM.ListAirlines();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion




        #region api/Airlines/UpdateAirlines (Put)

        [Route("api/Airlines/UpdateAirlines")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateAirlines(ACRF_AirlinesModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objAirlinesVM.UpdateAirlines(objModel);
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




        #region api/Airlines/DeleteAirlines (Delete)

        [Route("api/Airlines/DeleteAirlines")]
        [HttpDelete]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult DeleteAirlines(int Id)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objAirlinesVM.DeleteAirlines(Id, CreatedBy);
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




        #region api/Airlines/ViewAllAirlinesSearch (Get)

        [Route("api/Airlines/ViewAllAirlinesSearch")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminVendor)]
        public IHttpActionResult ViewAllAirlinesSearch(string prefix)
        {
            List<ACRF_AirlinesSearchModel> objList = new List<ACRF_AirlinesSearchModel>();
            try
            {
                objList = objAirlinesVM.ListAirlinesByPrefix(prefix);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion





        #region api/Airlines/UploadAirlineImage

        [HttpPut]
        [Route("api/Airlines/UploadAirlineImage")]
        public IHttpActionResult UploadAirlineImage()
        {
            string result = "Error in uploading airline image.";
            try
            {
                ACRF_AirlinesModel objAirModel = new ACRF_AirlinesModel();
                string fileName = null;
                var httpRequest = HttpContext.Current.Request;
                var postedfile = httpRequest.Files["Image"];

                int Id = Convert.ToInt32(httpRequest["AirlineId"].ToString());

                fileName = new String(Path.GetFileNameWithoutExtension(postedfile.FileName).Take(10).ToArray()).Replace(" ", "-");
                fileName =Id + "_" + DateTime.Now.ToString("ddMMMyyyy") + Path.GetExtension(postedfile.FileName);
                string ext = Path.GetFileName(postedfile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/ProfileImage/Airline/" + fileName);
                postedfile.SaveAs(filePath);

                objAirModel.Id = Id;
                objAirModel.AirlinePhoto = "/ProfileImage/Airline/" + fileName;
                objAirModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());

                result = objAirlinesVM.UpdateAirlinePhoto(objAirModel);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Ok(new { results = result });
        }

        #endregion


    }
}
