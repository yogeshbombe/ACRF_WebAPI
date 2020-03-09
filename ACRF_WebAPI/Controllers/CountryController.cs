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
    public class CountryController : ApiController
    {
        CountryViewModel objCountryVM = new CountryViewModel();


        #region api/Country/AddCountry (Post)

        [Route("api/Country/AddCountry")]
        [HttpPost]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddCountry(ACRF_CountryModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCountryVM.CreateCountry(objModel);
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




        #region api/Country/ViewCountryByPage (Get)

        [Route("api/Country/ViewCountryByPage")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewCountryByPage(int max, int page, string sort_col, string sort_dir, string search = null)
        {
            Paged_CountryModel objList = new Paged_CountryModel();
            try
            {
                objList = objCountryVM.ListCountry(max,page,search, sort_col, sort_dir);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion





        #region api/Country/ViewOneCountry (Get)

        [Route("api/Country/ViewOneCountry")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneCountry(int Id)
        {
            ACRF_CountryModel objList = new ACRF_CountryModel();

            try
            {
                objList = objCountryVM.GetOneCountry(Id);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion






        #region api/Country/ViewAllCountry (Get)

        [Route("api/Country/ViewAllCountry")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewAllCountry()
        {
            List<ACRF_CountryModel> objList = new List<ACRF_CountryModel>();
            try
            {
                objList = objCountryVM.ListCountry();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion




        #region api/Country/UpdateCountry (Put)

        [Route("api/Country/UpdateCountry")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateCountry(ACRF_CountryModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCountryVM.UpdateCountry(objModel);
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




        #region api/Country/DeleteCountry (Delete)

        [Route("api/Country/DeleteCountry")]
        [HttpDelete]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult DeleteCountry(int Id)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCountryVM.DeleteCountry(Id,CreatedBy);
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
