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
    public class DropDownController : ApiController
    {
        DropDownViewModel objDDVM = new DropDownViewModel();


        #region api/DropDown/ViewCountryList (Get)

        [Route("api/DropDown/ViewCountryList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewCountryList()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList = objDDVM.ListCountry();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion

        #region api/DropDown/ViewProfileList (Get)

        [Route("api/DropDown/ViewProfileList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewProfileList()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList = objDDVM.ListProfile();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion

        #region api/DropDown/ViewProjectList (Get)

        [Route("api/DropDown/ViewProjectList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewProjectList()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList = objDDVM.ListProject();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion

        #region api/DropDown/ViewEmployeeList (Get)

        [Route("api/DropDown/ViewEmployeeList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewEmployeeList()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList = objDDVM.ListEmployee();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion

        #region api/DropDown/ViewStatusList (Get)

        [Route("api/DropDown/ViewStatusList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewStatusList()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList = objDDVM.ListStatus();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion



        #region api/DropDown/ViewAirlineListMulti (Get)

        [Route("api/DropDown/ViewAirlineListMulti")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewAirlineListMulti()
        {
            List<MultiSelectListItem> objList = new List<MultiSelectListItem>();
            try
            {
                List<SelectListItem> objDList = new List<SelectListItem>();
                objDList = objDDVM.ListAirline();
                foreach(var data in objDList)
                {
                    objList.Add(new MultiSelectListItem { 
                    id=data.Value,
                    itemName=data.Text
                    });
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        #region api/DropDown/ViewCityListMulti (Get)

        [Route("api/DropDown/ViewCityListMulti")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewCityListMulti()
        {
            List<MultiSelectListItem> objList = new List<MultiSelectListItem>();
            try
            {
                List<SelectListItem> objDList = new List<SelectListItem>();
                objDList = objDDVM.ListCityFromDestination("");
                foreach (var data in objDList)
                {
                    objList.Add(new MultiSelectListItem
                    {
                        id = data.Value,
                        itemName = data.Text
                    });
                }
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion




        #region api/DropDown/ViewAirlineList (Get)

        [Route("api/DropDown/ViewAirlineList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewAirlineList()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList = objDDVM.ListAirline();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        #region api/DropDown/ViewTariffModeList (Get)

        [Route("api/DropDown/ViewTariffModeList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewTariffModeList()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList = objDDVM.ListTariffMode();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion

        
        #region api/DropDown/ViewCountryFromDestinationList (Get)

        [Route("api/DropDown/ViewCountryFromDestinationList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewCountryFromDestinationList()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList = objDDVM.ListCountryFromDestination();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        #region api/DropDown/ViewCityFromDestinationList (Get)

        [Route("api/DropDown/ViewCityFromDestinationList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewCityFromDestinationList(string Country)
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                if (Country == "undefined")
                {
                    Country = "";
                }
                if(Country == null)
                {
                    Country = "";
                }
                objList = objDDVM.ListCityFromDestination(Country);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        #region api/DropDown/ViewEmailList (Get)

        [Route("api/DropDown/ViewEmailList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewEmailList(int vendorId)
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList = objDDVM.ListEmail(vendorId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion



        #region api/DropDown/ViewQuotationStatusList (Get)

        [Route("api/DropDown/ViewQuotationStatusList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewQuotationStatusList()
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                objList = objDDVM.ListQuotationStatus();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion




        #region api/DropDown/ViewCityFromOriginList (Get)

        [Route("api/DropDown/ViewCityFromOriginList")]
        [HttpGet]
        [SessionAuthorizeFilter]
        public IHttpActionResult ViewCityFromOriginList(string Country)
        {
            List<SelectListItem> objList = new List<SelectListItem>();
            try
            {
                if (Country == "undefined")
                {
                    Country = "";
                }
                if (Country == null)
                {
                    Country = "";
                }
                objList = objDDVM.ListCityFromOrigin(Country);
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
