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
    public class VendorDetailsController : ApiController
    {
        VendorDetailsViewModel objVendorDetailsVM = new VendorDetailsViewModel();


        #region api/VendorDetails/AddVendorDetails (Post)

        [Route("api/VendorDetails/AddVendorDetails")]
        [HttpPost]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddVendorDetails(ACRF_VendorDetailsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objVendorDetailsVM.CreateVendorDetails(objModel);
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


        #region api/VendorDetails/ViewAllVendorDetails (Get)

        [Route("api/VendorDetails/ViewAllVendorDetails")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewAllVendorDetails()
        {
            List<ACRF_VendorDetailsModel> objList = new List<ACRF_VendorDetailsModel>();
            try
            {
                objList = objVendorDetailsVM.ListVendorDetails();
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion


        #region api/VendorDetails/UpdateVendorDetails (Put)

        [Route("api/VendorDetails/UpdateVendorDetails")]
        [HttpPut]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateVendorDetails(ACRF_VendorDetailsModel objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objVendorDetailsVM.UpdateVendorDetails(objModel);
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


        #region api/VendorDetails/DeleteVendorDetails (Delete)

        [Route("api/VendorDetails/DeleteVendorDetails")]
        [HttpDelete]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult DeleteVendorDetails(int id)
        {
            string result = "";
            if (id != 0)
            {
                try
                {
                    string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objVendorDetailsVM.DeleteVendorDetails(id, CreatedBy);
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


        #region api/VendorDetails/ViewOneVendorDetails (Get)

        [Route("api/VendorDetails/ViewOneVendorDetails")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneVendorDetails(int VendorId)
        {
            ACRF_VendorDetailsModel objList = new ACRF_VendorDetailsModel();
            try
            {
                objList = objVendorDetailsVM.GetOneVendorDetails(VendorId);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion





        #region api/VendorDetails/ViewVendorDetailsByPage (Get)
        [Route("api/VendorDetails/ViewVendorDetailsByPage")]
        [HttpGet]
        [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewVendorDetailsByPage(int max, int page, string sort_col, string sort_dir, string search = null)
        {
            Paged_ACRF_VendorDetailsModel objList = new Paged_ACRF_VendorDetailsModel();
            try
            {
                objList = objVendorDetailsVM.ListVendorDetailsByPagination(max, page, search, sort_col,sort_dir);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Ok(new { results = objList });
        }

        #endregion




        #region api/Auth/UploadVendorImage

        [HttpPost]
        [Route("api/Auth/UploadVendorImage")]
        public HttpResponseMessage UploadVendorImage()
        {
            try
            {
                string fileName = null;
                var httpRequest = HttpContext.Current.Request;
                var postedfile = httpRequest.Files["Image"];

                int vendorId = Convert.ToInt32(httpRequest["VendorId"].ToString());

                fileName = new String(Path.GetFileNameWithoutExtension(postedfile.FileName).Take(10).ToArray()).Replace(" ", "-");
                fileName = vendorId + "_" + DateTime.Now.ToString("ddMMMyyyy") + Path.GetExtension(postedfile.FileName);
                string ext = Path.GetFileName(postedfile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/ProfileImage/Vendor/" + fileName);
                postedfile.SaveAs(filePath);

                string result = objVendorDetailsVM.UpdateVendorProfileImage(vendorId, "/ProfileImage/Vendor/" + fileName);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        #endregion



    }
}
