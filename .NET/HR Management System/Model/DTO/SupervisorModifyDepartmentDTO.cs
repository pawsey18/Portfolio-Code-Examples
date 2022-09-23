using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// DTO for storing supervisors department details
    /// </summary>
    public class SupervisorModifyDepartmentDTO
    {
        public int EmployeeId { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public DateTime InvocationDate { get; set; }
        public byte[] RecordVersion { get; set; }

    }
}
