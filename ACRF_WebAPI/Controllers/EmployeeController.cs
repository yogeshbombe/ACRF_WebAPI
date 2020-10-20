using ACRF_WebAPI.App_Start;
using ACRF_WebAPI.Global;
using ACRF_WebAPI.Models;
using ACRF_WebAPI.ViewModel;
using System;
using System.Linq;
using System.Web.Http;

namespace ACRF_WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {

        EmployeeViewModel objEmployeeVM = new EmployeeViewModel();

        #region api/Employee/ViewEmployeeByPage (Get)

        [Route("api/Employee/ViewEmployeeByPage")]
        [HttpGet]
        public IHttpActionResult ViewEmployeeByPage(int max, int page, string sort_col, string sort_dir, string search = null)
        {
            Paged_EmployeeModel objList = new Paged_EmployeeModel();
            try
            {
                objList = objEmployeeVM.ListEmployeeDetailsByPagination(max, page, search, sort_col, sort_dir);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }
            return Ok(new { results = objList });
        }

        #endregion

        #region api/Employee/AddEmployee (Post)

        [Route("api/Employee/AddEmployee")]
        [HttpPost]
        //[SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult AddEmployee(Employee objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    // objModel.CreatedBy = "10002";//GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objEmployeeVM.CreateEmployee(objModel);
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


        #region api/Employee/ViewOneEmployee (Get)

        [Route("api/Employee/ViewOneEmployee")]
        [HttpGet]
       // [SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult ViewOneEmployee(int Id)
        {
            Employee objList = new Employee();
            try
            {
                objList = objEmployeeVM.GetOneEmployee(Id);
            }
            catch (Exception ex)
            {
                ErrorHandlerClass.LogError(ex);
            }

            return Ok(new { results = objList });
        }

        #endregion

        #region api/Employee/UpdateEmployee (Put)

        [Route("api/Employee/UpdateEmployee")]
        [HttpPut]
        //[SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult UpdateEmployee(Employee objModel)
        {
            string result = "";
            if (ModelState.IsValid)
            {
                try
                {
                    //objModel.UpdatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objEmployeeVM.UpdateEmployee(objModel);
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


        #region api/Employee/DeleteEmployee (Delete)

        [Route("api/Employee/DeleteEmployee")]
        [HttpDelete]
        //[SessionAuthorizeFilter(UserType.AdminUser)]
        public IHttpActionResult DeleteEmployee(int id)
        {
            string result = "";
            if (id != 0)
            {
                try
                {
                    string CreatedBy = GlobalFunction.getLoggedInUser(Request.Headers.GetValues("Token").First());
                    result = objEmployeeVM.DeleteEmplyee(id);
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
