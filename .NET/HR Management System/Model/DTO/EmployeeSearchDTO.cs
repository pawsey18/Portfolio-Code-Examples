using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Used for searching on the web and desktop
    /// </summary>
    public class EmployeeSearchDTO
    {
        public long EmployeeId { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
      
    }
}
