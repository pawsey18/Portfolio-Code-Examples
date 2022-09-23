using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types;

namespace Repository
{
    /// <summary>
    /// Lookups for Employee to retrieve departments, status and job assignments.
    /// </summary>
    public class EmployeeLookupsRepo
    {
        DataAccess db = new DataAccess();

        /// <summary>
        ///  Retrieve a list of Departments
        /// </summary>
        /// <returns>Returns a  list of departments </returns>
        public List<DepartmentLookupDTO> RetrieveDepartmentList()
        {
            DataTable dt = db.Execute("spGetDepartments");

            List<DepartmentLookupDTO> departments = new List<DepartmentLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                departments.Add(
                    new DepartmentLookupDTO
                    {
                        DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                        DepartmentName = row["DepartmentName"].ToString()
                    }
                );
            }
            return departments;
        }

        /// <summary>
        /// Get the list of statuses
        /// </summary>
        /// <returns></returns>
        public List<EmployeeStatusLookupDTO> RetrieveStatusList()
        {
            DataTable dt = db.Execute("spGetEmployeeStatuses");

            List<EmployeeStatusLookupDTO> status = new List<EmployeeStatusLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                status.Add(
                    new EmployeeStatusLookupDTO
                    {
                        StatusId = Convert.ToInt32(row["StatusId"]),
                        Status = row["Status"].ToString()
                    }
                );
            }
            return status;
        }

        /// <summary>
        /// Get the job assignments list
        /// </summary>
        /// <returns></returns>
        public List<JobAssignmentLookupDTO> RetrieveJobAssignmentList()
        {
            DataTable dt = db.Execute("spGetJobAssignments");

            List<JobAssignmentLookupDTO> jobAssignments = new List<JobAssignmentLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                jobAssignments.Add(
                    new JobAssignmentLookupDTO
                    {
                        JobAssignmentId = Convert.ToInt32(row["JobAssignmentId"]),
                        AssignmentName = row["AssignmentName"].ToString(),
                        JobTitle = row["JobTitle"].ToString()
                    }
                );
            }
            return jobAssignments;
        }

        /// <summary>
        ///  Get current supervisor in department by id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
       public List<CurrentSupervisorsLookupDTO> RetrieveCurrentSupervisorsByDepartmentId(int departmentId)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
             new ParmStruct("@DepartmentId", departmentId, SqlDbType.Int, 0)
            };
            DataTable dt = db.Execute("spGetCurrentSupervisors", parms);

            List<CurrentSupervisorsLookupDTO> supervisor = new List<CurrentSupervisorsLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                supervisor.Add(
                    new CurrentSupervisorsLookupDTO
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                        FirstName = row["FullName"].ToString(),
                    }
                );
            }
            return supervisor;
        }

        /// <summary>
        ///  Get all the supervisors
        /// </summary>
        /// <returns></returns>
        public List<CurrentSupervisorsLookupDTO> GetAllSupervisors()
        {
          
            DataTable dt = db.Execute("spGetAllSupervisors", null);

            List<CurrentSupervisorsLookupDTO> supervisor = new List<CurrentSupervisorsLookupDTO>();

            foreach (DataRow row in dt.Rows)
            {
                supervisor.Add(
                    new CurrentSupervisorsLookupDTO
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                        FirstName = row["FullName"].ToString(),
                    }
                );
            }
            return supervisor;
        }

        /// <summary>
        /// Get an employee's email address
        /// </summary>
        /// <param name="empId">The employee's ID</param>
        /// <returns>The email address or an empty string</returns>
        public string GetEmail(int empId)
        {
            var result = db.ExecuteScaler("spGetEmployeeEmail", new List<ParmStruct> { new ParmStruct("@EmpId", empId, SqlDbType.Int) } );

            return result.ToString();
        }
    }
}
