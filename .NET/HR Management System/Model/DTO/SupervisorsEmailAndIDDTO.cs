using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// DTO for storing the supervisors employee id's and email for when the email reminders are being sent.
    /// </summary>
    public class SupervisorsEmailAndIDDTO
    {
        public int EmployeeId { get; set; }
        public string Email { get; set; }
    }
}
