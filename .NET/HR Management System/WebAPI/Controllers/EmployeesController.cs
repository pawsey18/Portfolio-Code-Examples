using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Model;
using Service;

namespace CapstoneAlpha.WebAPI.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeesController : ApiController
    {
        private AuthService service = new AuthService();
        private EmployeeService empService = new EmployeeService();
        

        [Route("auth/{id}")]
        [HttpPost]
        public IHttpActionResult Login(int id, [FromBody] LoginDTO body)
        {
            try
            {
                LoginDTO login = service.Login(id, body.Password);

                if (login == null)
                {
                    return Content(HttpStatusCode.NotFound, "Invalid Login");
                }

                return Content(HttpStatusCode.OK, login);
                
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured. Please contact the system admin.");
            }
        }

       
        /// <summary>
        /// Get employee information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("information/{id}")]
        public IHttpActionResult GetEmployeeInformationToBeUpdated(int? id)
        {
            try
            {
                List<EmployeeInformationDTO> employees = empService.GetEmployeeInformationDTO();

                if (id != 0)
                    employees = employees.Where(a => a.EmployeeId == id).ToList();

                return Ok(employees);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured.  Please" +
                   "contact the system administrator.");
            }
        }


        /// <summary>
        /// Get employee information for android
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("information")]
        public IHttpActionResult GetEmployeeInformationAndroid(int? id)
        {
            try
            {
                List<EmployeeInformationDTO> employees = empService.GetEmployeeInformationDTO();

                if (id != 0)
                    employees = employees.Where(a => a.EmployeeId == id).ToList();

             

                return Ok(employees);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured.  Please" +
                   "contact the system administrator.");
            }
        }


        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("record/{id}")]
        public IHttpActionResult UpdateEmpoyeePersonalRecord(int id, [FromBody] EmployeeInformationDTO employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                    return BadRequest();

                employee = empService.UpdatePersonalEmployeeRecord(employee);

                if (employee == null)
                {
                    return Content(HttpStatusCode.BadRequest, employee);
                }

                return Ok();
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured.  Please " +
                   "contact the system administrator.");
            }
        }




        /// <summary>
        ///  Get Employees for the Web searching of employees
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="active"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetEmployees(string lastName = null, bool active = true, int? employeeId = 0 )
        {
            try
            {
                List<MobileSearchEmployeeDTO> employees = empService.GetAllEmpsForAPI();


                if (!string.IsNullOrEmpty(lastName))
                    employees = employees.Where(a => a.LastName.ToLower().Contains(lastName.ToLower())).ToList();

                if (active != true)
                    employees = employees.Where(a => a.Active == false).ToList();

                if (employeeId != 0)
                    employees = employees.Where(a => a.EmployeeId == employeeId).ToList();

                if (employeeId != 0 && !string.IsNullOrEmpty(lastName))
                    employees = employees.Where(a => a.LastName.ToLower().Contains(lastName.ToLower()) || a.EmployeeId == employeeId).ToList();


                return Ok(employees);
            }
            catch (Exception)
            {
                return Content(HttpStatusCode.InternalServerError, "An internal error has occured.  Please" +
                   "contact the system administrator.");
            }
        }

    }
}
