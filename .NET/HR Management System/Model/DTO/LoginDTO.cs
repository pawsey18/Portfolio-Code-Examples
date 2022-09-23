using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    ///  Login DTO 
    /// </summary>
    public class LoginDTO
    {
        public int UserId { get; set; }
        public int JobAssignment { get; set; }
        public string Password { get; set; }
        public string EmployeeName { get; set; }
        public string Role { get; set; }

        public bool isSupervisor()
        {
            return Role.Contains("Supervisor") || Role.Equals("CEO");
        }
    }
}
