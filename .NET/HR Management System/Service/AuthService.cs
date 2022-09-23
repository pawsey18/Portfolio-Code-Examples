
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthService
    {

        private AuthRepo repo = new AuthRepo();


        /// <summary>
        /// Check if employee id and password match
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="password"></param>
        /// <returns>LoginDTO</returns>
        public LoginDTO Login(int employeeId, string password)
        {
         
            return repo.Login(employeeId,password );
        }

        /// <summary>
        /// Check to see if employee is a supervisor
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>boolean based on condition</returns>
        public bool IsSupervisor(int employeeId)
        {
            return repo.IsSupervisor(employeeId);
        }
    }
}
