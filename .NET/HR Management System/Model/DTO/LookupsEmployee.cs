using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Lookups for Employee
    /// </summary>

    public class DepartmentLookupDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class EmployeeStatusLookupDTO
    {
        public int StatusId { get; set; }
        public string Status { get; set; }
    }

    public class JobAssignmentLookupDTO
    {
        public int JobAssignmentId { get; set; }
        public string AssignmentName { get; set; }
        public string JobTitle { get; set; }
    }

    public class CurrentSupervisorsLookupDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
