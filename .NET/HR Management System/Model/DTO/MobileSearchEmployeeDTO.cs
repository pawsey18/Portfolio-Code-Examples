using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Employee DTO for API
    /// </summary>
    public class MobileSearchEmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }
        public string WorkPhone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string StreetAddress { get; set; }
        public string JobTitle { get; set; }
        public bool Active { get; set; }
        public int DepartmentId { get; set; }

    }
}
