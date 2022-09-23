using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Stores the data for the supervisors department that will be used for an update - web
    /// </summary>
   public class SupervisorDepartmentUpdateDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public byte[] RecordVersion { get; set; }
    }
}
