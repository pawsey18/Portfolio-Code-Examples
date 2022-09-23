using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Lookups
{
    public class LookupsEmployeeService
    {
        public EmployeeLookupsRepo repo;

        public LookupsEmployeeService()
        {
            repo = new EmployeeLookupsRepo();
        }

        /// <summary>
        /// Get list of departments
        /// </summary>
        /// <returns></returns>
        public List<DepartmentLookupDTO> GetDepartments()
        {
            return repo.RetrieveDepartmentList();
        }

        /// <summary>
        /// Get list of statuses
        /// </summary>
        /// <returns></returns>
        public List<EmployeeStatusLookupDTO> GetEmployeeStatus()
        {
            return repo.RetrieveStatusList();
        }

        /// <summary>
        /// Get list of job assignments
        /// </summary>
        /// <returns></returns>
        public List<JobAssignmentLookupDTO> GetJobAssignments()
        {
            return repo.RetrieveJobAssignmentList();
        }

        /// <summary>
        /// Get the current supervisor in a department
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public List<CurrentSupervisorsLookupDTO> GetCurrentSupervisorsInDepartment(int departmentId)
        {
            return repo.RetrieveCurrentSupervisorsByDepartmentId(departmentId);
        }

        /// <summary>
        /// Get all the supervisors
        /// </summary>
        /// <returns></returns>
        public List<CurrentSupervisorsLookupDTO> GetAllSupervisors()
        {
            return repo.GetAllSupervisors();
        }

        /// <summary>
        /// Get an Employee's Email
        /// </summary>
        /// <param name="empId">The employee's user id</param>
        /// <returns>The employee's email or empty string</returns>
        public string GetEmail(int empId)
        {
            return repo.GetEmail(empId);
        }
    }
}
