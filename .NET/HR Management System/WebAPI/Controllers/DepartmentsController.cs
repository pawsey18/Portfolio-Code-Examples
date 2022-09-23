using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CapstoneAlpha.WebAPI.Controllers
{
    [RoutePrefix("api/departments")]
    public class DepartmentsController : ApiController
    {
        DepartmentService service = new DepartmentService();

        /// <summary>
        /// Get department list
        /// </summary>
        /// <param name="departmentName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetDepartments(string departmentName = null)
        {
            try
            {
                List<Department> departments = service.GetDepartments();

                if (!string.IsNullOrEmpty(departmentName))
                    departments = departments.Where(a => a.DepartmentName.ToLower().Contains(departmentName.ToLower())).ToList();
                return Ok(departments);
            }
            catch (Exception)
            {
                //return InternalServerError(ex);
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured.  Please" +
                   "contact the system administrator.");
            }
        }

        /// <summary>
        /// Get the department details 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("department/{id}")]
        public IHttpActionResult GetSupervisorsDepartmentToBeUpdated(int id)
        {
            try
            {
                List<SupervisorModifyDepartmentDTO> employees = service.GetSupervisorDepartmentDetailsDTO(id);

                if (id != 0)
                    employees.ToList();

                return Ok(employees);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured.  Please" +
                   "contact the system administrator.");
            }
        }


 
        /// <summary>
        /// Update supervisors department
        /// </summary>
        /// <param name="id"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update/{id}")]
        public IHttpActionResult UpdateSupervisorDepartment(int id, [FromBody] SupervisorDepartmentUpdateDTO d)
        {
            try
            {
                DepartmentService newService = new DepartmentService();
                if (id != d.DepartmentId)
                    return BadRequest();

                d = newService.UpdateDepartmentWeb(d);

                if (d == null)
                {
                    return Content(HttpStatusCode.BadRequest, d);
                }

                return Ok();
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured.  Please " +
                   "contact the system administrator.");
            }
        }

    }
}
