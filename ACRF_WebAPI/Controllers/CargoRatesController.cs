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
    public class CargoRatesController : ApiController
    {
        CargoRatesViewModel objCrRtVM = new CargoRatesViewModel();
        CargoRateSettingsViewModel objCrRtStVM = new CargoRateSettingsViewModel();


        #region api/CargoRates/AddCargoRates (Post)

        [Route("api/Settings/AddProjectSettings")]
        [HttpPost]
        //[SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddCargoRates(ACRF_CargoRatesModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                   // objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCrRtVM.CreateCargoRates(objModel);
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


        #region api/CargoRates/UpdateCargoRates (Put)

        [Route("api/Settings/UpdateProjectSettings")]
        [HttpPut]
        //[SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateCargoRates(ACRF_CargoRatesModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    //objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCrRtVM.UpdateCargoRates(objModel);
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


        #region api/CargoRates/DeleteCargoRates (Delete)

        [Route("api/CargoRates/DeleteCargoRates")]
        [HttpDelete]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult DeleteCargoRates(int Id)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    //string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objCrRtVM.DeleteCargoRates(Id);
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






        #region api/Settings/ViewOneProjectSettings (Get)

        [Route("api/Settings/ViewOneProjectSettings")]
        [HttpGet]
        //[SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneCargoRates(int Id)
        {
            ACRF_CargoRatesModel objList = new ACRF_CargoRatesModel();

            try
            {
                objList = objCrRtVM.GetOneCargoRates(Id);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion

        
        #region api/CargoRates/ViewAllCargoRates (Get)

        //[Route("api/CargoRates/ViewAllCargoRates")]
        //[HttpGet]
        //[SessionAuthorizeFilter(UserType.AdminUser)]
        //public IHttpActionResult ViewAllCargoRates()
        //{
        //    List<ACRF_CargoRatesModel> objList = new List<ACRF_CargoRatesModel>();
        //    try
        //    {
        //        objList = objCrRtVM.ListCargoRates();
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandlerClass.LogError(ex);
        //    }

        //    return Ok(new { results = objList });
        //}

        #endregion



        #region api/CargoRates/ViewCargoRatesByPage (Get)

        [Route("api/Settings/ViewProjectsSettingByPage")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewCargoRatesByPage(int max, int page, string sort_col, string sort_dir, string search = null)
        {
            Paged_ACRF_CargoRatesModel objList = new Paged_ACRF_CargoRatesModel();
            try
            {
                objList = objCrRtVM.ListCargoRatesByPagination(max, page, search,sort_col,sort_dir);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Ok(new { results = objList });
        }

        #endregion






        #region api/CargoRates/ViewCargoRatesForParameters (Get)

        //[Route("api/CargoRates/ViewCargoRatesForParameters")]
        //[HttpGet]
        //[SessionAuthorizeFilter()]
        //public IHttpActionResult ViewCargoRatesForParameters(string Origin, string Destination, int TariffMode, int GWeight, int Airlines,int VendorId)
        //{           
        //    List<ACRF_CargoRatesModel> objList = new List<ACRF_CargoRatesModel>();
        //    ACRF_CargoRateSettingsModel objStList = new ACRF_CargoRateSettingsModel();
        //    List<CargoRatesModel> objCRList = new List<CargoRatesModel>();
        //    try
        //    {
        //        objList = objCrRtVM.ListCargoRatesFromODTGA(Origin,Destination,TariffMode,GWeight,Airlines);
        //        objStList = objCrRtStVM.GetOneCargoRateSettings(VendorId);

        

        //        foreach (var data in objList)
        //        {
        //            CargoRatesModel tempobj = new CargoRatesModel();

        //            tempobj.Airline = data.Airline;
        //            tempobj.AirlineId = data.AirlineId;
        //            tempobj.DAirportName = data.DAirportName;
        //            tempobj.DCity = data.DCity;
        //            tempobj.DCityCode = data.DCityCode;
        //            tempobj.DCountry = data.DCountry;
        //            tempobj.DCountryCode = data.DCountryCode;
        //            tempobj.OAirportName = data.OAirportName;
        //            tempobj.OCity = data.OCity;
        //            tempobj.OCityCode = data.OCityCode;
        //            tempobj.OCountry = data.OCountry;
        //            tempobj.OCountryCode = data.OCountryCode;
        //            tempobj.TariffMode = data.TariffMode;
        //            tempobj.TariffModeId = data.TariffModeId;

        //            tempobj.Slab1 = GWeight;
        //            tempobj.Rate = data.MinPrice;
        //            if(GWeight>data.MinWeight)
        //            {
        //                tempobj.Rate = data.MinPrice;
        //            }
        //            if(GWeight>45)
        //            {
        //                tempobj.Rate = data.plus45;
        //            }
        //            if(GWeight>100)
        //            {
        //                tempobj.Rate = data.plus100;
        //            }
        //            if(GWeight>250)
        //            {
        //                tempobj.Rate = data.plus250;
        //            }
        //            if(GWeight>300)
        //            {
        //                tempobj.Rate = data.plus300;
        //            }
        //            if(GWeight>500)
        //            {
        //                tempobj.Rate = data.plus500;
        //            }
        //            if(GWeight>1000)
        //            {
        //                tempobj.Rate = data.plus1000;
        //            }


        //            if(data.CtcMin<GWeight)
        //            {
        //                tempobj.Ctc = data.CtcKg * GWeight;
        //            }
        //            else
        //            {
        //                tempobj.Ctc = data.CtcKg;
        //            }

        //            if (data.FSCMin < GWeight)
        //            {
        //                tempobj.FSC = data.FSCKg * GWeight;
        //            }
        //            else
        //            {
        //                tempobj.FSC = data.FSCKg;
        //            }

        //            if (data.MccMin < GWeight)
        //            {
        //                tempobj.Mcc = data.MccKg * GWeight;
        //            }
        //            else
        //            {
        //                tempobj.Mcc = data.MccKg;
        //            }

        //            if (data.WSCMin < GWeight)
        //            {
        //                tempobj.WSC = data.WSCKg * GWeight;
        //            }
        //            else
        //            {
        //                tempobj.WSC = data.WSCKg;
        //            }

        //            if (data.XrayMin < GWeight)
        //            {
        //                tempobj.Xray = data.XrayKg * GWeight;
        //            }
        //            else
        //            {
        //                tempobj.Xray = data.XrayKg;
        //            }
                    
        //            tempobj.IsRate1 = objStList.IsRate1;
        //            tempobj.IsRate2 = objStList.IsRate2;
        //            tempobj.IsRate3 = objStList.IsRate3;
        //            tempobj.Rate1 = objStList.Rate1;
        //            tempobj.Rate2 = objStList.Rate2;
        //            tempobj.Rate3 = objStList.Rate3;
        //            tempobj.Freight = (tempobj.Rate * GWeight);
        //            tempobj.TotalCost = tempobj.Freight + tempobj.TotalCost + tempobj.Ctc + tempobj.FSC + tempobj.Mcc + tempobj.WSC + tempobj.Xray + tempobj.Rate1 + tempobj.Rate2 + tempobj.Rate3;

        //            tempobj.AirlineDemoPhoto = data.AirlineDemoPhoto;
        //            tempobj.AirlinePhoto = data.AirlinePhoto;

        //            objCRList.Add(tempobj);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandlerClass.LogError(ex);
        //    }

        //    return Ok(new { results = objCRList });
        //}

        #endregion






        #region api/CargoRates/ViewCargoRatesForMultiSelect (Get)

        //[Route("api/CargoRates/ViewCargoRatesForMultiSelect")]
        //[HttpPost]
        //[SessionAuthorizeFilter()]
        //public IHttpActionResult ViewCargoRatesForMultiSelect(CargoRatesForMulitSelectModel objModel)
        //{
        //    List<ACRF_CargoRatesModel> objList = new List<ACRF_CargoRatesModel>();
        //    ACRF_CargoRateSettingsModel objStList = new ACRF_CargoRateSettingsModel();
        //    List<CargoRatesModel> objCRList = new List<CargoRatesModel>();
        //    try
        //    {
        //        objList = objCrRtVM.ListCargoRatesFromODTGA(objModel);
        //        objStList = objCrRtStVM.GetOneCargoRateSettings(objModel.VendorId);



        //        foreach (var data in objList)
        //        {
        //            CargoRatesModel tempobj = new CargoRatesModel();

        //            tempobj.Airline = data.Airline;
        //            tempobj.AirlineId = data.AirlineId;
        //            tempobj.DAirportName = data.DAirportName;
        //            tempobj.DCity = data.DCity;
        //            tempobj.DCityCode = data.DCityCode;
        //            tempobj.DCountry = data.DCountry;
        //            tempobj.DCountryCode = data.DCountryCode;
        //            tempobj.OAirportName = data.OAirportName;
        //            tempobj.OCity = data.OCity;
        //            tempobj.OCityCode = data.OCityCode;
        //            tempobj.OCountry = data.OCountry;
        //            tempobj.OCountryCode = data.OCountryCode;
        //            tempobj.TariffMode = data.TariffMode;
        //            tempobj.TariffModeId = data.TariffModeId;

        //            tempobj.Slab1 = objModel.GWeight;
        //            tempobj.Rate = data.MinPrice;
        //            if (objModel.GWeight > data.MinWeight)
        //            {
        //                tempobj.Rate = data.MinPrice;
        //            }
        //            if (objModel.GWeight > 45)
        //            {
        //                tempobj.Rate = data.plus45;
        //            }
        //            if (objModel.GWeight > 100)
        //            {
        //                tempobj.Rate = data.plus100;
        //            }
        //            if (objModel.GWeight > 250)
        //            {
        //                tempobj.Rate = data.plus250;
        //            }
        //            if (objModel.GWeight > 300)
        //            {
        //                tempobj.Rate = data.plus300;
        //            }
        //            if (objModel.GWeight > 500)
        //            {
        //                tempobj.Rate = data.plus500;
        //            }
        //            if (objModel.GWeight > 1000)
        //            {
        //                tempobj.Rate = data.plus1000;
        //            }


        //            if (data.CtcMin < objModel.GWeight)
        //            {
        //                tempobj.Ctc = data.CtcKg * objModel.GWeight;
        //            }
        //            else
        //            {
        //                tempobj.Ctc = data.CtcKg;
        //            }

        //            if (data.FSCMin < objModel.GWeight)
        //            {
        //                tempobj.FSC = data.FSCKg * objModel.GWeight;
        //            }
        //            else
        //            {
        //                tempobj.FSC = data.FSCKg;
        //            }

        //            if (data.MccMin < objModel.GWeight)
        //            {
        //                tempobj.Mcc = data.MccKg * objModel.GWeight;
        //            }
        //            else
        //            {
        //                tempobj.Mcc = data.MccKg;
        //            }

        //            if (data.WSCMin < objModel.GWeight)
        //            {
        //                tempobj.WSC = data.WSCKg * objModel.GWeight;
        //            }
        //            else
        //            {
        //                tempobj.WSC = data.WSCKg;
        //            }

        //            if (data.XrayMin < objModel.GWeight)
        //            {
        //                tempobj.Xray = data.XrayKg * objModel.GWeight;
        //            }
        //            else
        //            {
        //                tempobj.Xray = data.XrayKg;
        //            }

        //            tempobj.IsRate1 = objStList.IsRate1;
        //            tempobj.IsRate2 = objStList.IsRate2;
        //            tempobj.IsRate3 = objStList.IsRate3;
        //            tempobj.Rate1 = objStList.Rate1;
        //            tempobj.Rate2 = objStList.Rate2;
        //            tempobj.Rate3 = objStList.Rate3;
        //            tempobj.Freight = (tempobj.Rate * objModel.GWeight);
        //            tempobj.TotalCost = tempobj.Freight + tempobj.TotalCost + tempobj.Ctc + tempobj.FSC + tempobj.Mcc + tempobj.WSC + tempobj.Xray + tempobj.Rate1 + tempobj.Rate2 + tempobj.Rate3;

        //            tempobj.AirlineDemoPhoto = data.AirlineDemoPhoto;
        //            tempobj.AirlinePhoto = data.AirlinePhoto;

        //            objCRList.Add(tempobj);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandlerClass.LogError(ex);
        //    }

        //    return Ok(new { results = objCRList });
        //}

        #endregion






        #region api/CargoRates/ViewCargoRatesForMultiAirlines (Get)

        //[Route("api/CargoRates/ViewCargoRatesForMultiAirlines")]
        //[HttpPost]
        //[SessionAuthorizeFilter()]
        //public IHttpActionResult ViewCargoRatesForMultiAirlines(CargoRatesForMulitSelectModel objModel)
        //{
        //    List<ACRF_CargoRatesModel> objList = new List<ACRF_CargoRatesModel>();
        //    ACRF_CargoRateSettingsModel objStList = new ACRF_CargoRateSettingsModel();
        //    List<CargoRatesModel> objCRList = new List<CargoRatesModel>();
        //    try
        //    {
        //        objList = objCrRtVM.ListCargoRatesFromODTGA(objModel);
        //        objStList = objCrRtStVM.GetOneCargoRateSettings(objModel.VendorId);



        //        foreach (var data in objList)
        //        {
        //            CargoRatesModel tempobj = new CargoRatesModel();

        //            tempobj.Airline = data.Airline;
        //            tempobj.AirlineId = data.AirlineId;
        //            tempobj.DAirportName = data.DAirportName;
        //            tempobj.DCity = data.DCity;
        //            tempobj.DCityCode = data.DCityCode;
        //            tempobj.DCountry = data.DCountry;
        //            tempobj.DCountryCode = data.DCountryCode;
        //            tempobj.OAirportName = data.OAirportName;
        //            tempobj.OCity = data.OCity;
        //            tempobj.OCityCode = data.OCityCode;
        //            tempobj.OCountry = data.OCountry;
        //            tempobj.OCountryCode = data.OCountryCode;
        //            tempobj.TariffMode = data.TariffMode;
        //            tempobj.TariffModeId = data.TariffModeId;

        //            tempobj.Slab1 = objModel.GWeight;
        //            tempobj.Rate = data.MinPrice;
        //            if (objModel.GWeight > data.MinWeight)
        //            {
        //                tempobj.Rate = data.MinPrice;
        //            }
        //            if (objModel.GWeight > 45)
        //            {
        //                tempobj.Rate = data.plus45;
        //            }
        //            if (objModel.GWeight > 100)
        //            {
        //                tempobj.Rate = data.plus100;
        //            }
        //            if (objModel.GWeight > 250)
        //            {
        //                tempobj.Rate = data.plus250;
        //            }
        //            if (objModel.GWeight > 300)
        //            {
        //                tempobj.Rate = data.plus300;
        //            }
        //            if (objModel.GWeight > 500)
        //            {
        //                tempobj.Rate = data.plus500;
        //            }
        //            if (objModel.GWeight > 1000)
        //            {
        //                tempobj.Rate = data.plus1000;
        //            }


        //            if (data.CtcMin < objModel.GWeight)
        //            {
        //                tempobj.Ctc = data.CtcKg * objModel.GWeight;
        //            }
        //            else
        //            {
        //                tempobj.Ctc = data.CtcKg;
        //            }

        //            if (data.FSCMin < objModel.GWeight)
        //            {
        //                tempobj.FSC = data.FSCKg * objModel.GWeight;
        //            }
        //            else
        //            {
        //                tempobj.FSC = data.FSCKg;
        //            }

        //            if (data.MccMin < objModel.GWeight)
        //            {
        //                tempobj.Mcc = data.MccKg * objModel.GWeight;
        //            }
        //            else
        //            {
        //                tempobj.Mcc = data.MccKg;
        //            }

        //            if (data.WSCMin < objModel.GWeight)
        //            {
        //                tempobj.WSC = data.WSCKg * objModel.GWeight;
        //            }
        //            else
        //            {
        //                tempobj.WSC = data.WSCKg;
        //            }

        //            if (data.XrayMin < objModel.GWeight)
        //            {
        //                tempobj.Xray = data.XrayKg * objModel.GWeight;
        //            }
        //            else
        //            {
        //                tempobj.Xray = data.XrayKg;
        //            }

        //            tempobj.IsRate1 = objStList.IsRate1;
        //            tempobj.IsRate2 = objStList.IsRate2;
        //            tempobj.IsRate3 = objStList.IsRate3;
        //            tempobj.Rate1 = objStList.Rate1;
        //            tempobj.Rate2 = objStList.Rate2;
        //            tempobj.Rate3 = objStList.Rate3;
        //            tempobj.Freight = (tempobj.Rate * objModel.GWeight);
        //            tempobj.TotalCost = tempobj.Freight + tempobj.TotalCost + tempobj.Ctc + tempobj.FSC + tempobj.Mcc + tempobj.WSC + tempobj.Xray + tempobj.Rate1 + tempobj.Rate2 + tempobj.Rate3;

        //            tempobj.AirlineDemoPhoto = data.AirlineDemoPhoto;
        //            tempobj.AirlinePhoto = data.AirlinePhoto;

        //            objCRList.Add(tempobj);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorHandlerClass.LogError(ex);
        //    }

        //    return Ok(new { results = objCRList });
        //}

        #endregion



    }
}
