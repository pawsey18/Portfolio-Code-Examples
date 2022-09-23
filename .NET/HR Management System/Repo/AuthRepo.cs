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
    /// AuthRepo 
    /// </summary>
    public class AuthRepo
    {
        DataAccess db;

        public AuthRepo()
        {
            db = new DataAccess();
        }

        #region Public Methods

        /// <summary>
        /// Verifying an Employees user name and password
        /// </summary>
        /// <param name="employeeId">Employee ID is the Employees username</param>
        /// <param name="password">Password to be checked</param>
        /// <returns>The login record if the credentials match/returns>
        public LoginDTO Login(int employeeId, string password)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@EmployeeID", employeeId, SqlDbType.Int, 8, ParameterDirection.Input),
                new ParmStruct("@Password", password, SqlDbType.VarChar),
            };

            DataTable dt = db.Execute("spLoginEmployee", parms, CommandType.StoredProcedure);

            if (dt.Rows.Count == 0)
                return null;

            string firstName = dt.Rows[0]["FirstName"].ToString();
            string lastName = dt.Rows[0]["LastName"].ToString();

            return new LoginDTO
            {
                UserId = Convert.ToInt32(dt.Rows[0]["EmployeeId"]),
                EmployeeName = $"{lastName}, {firstName}",
                JobAssignment = Convert.ToInt32(dt.Rows[0]["JobAssignmentId"]),
                Role = dt.Rows[0]["Role"].ToString()
            };
        }

        /// <summary>
        /// Check if an employee with the provided ID is a supervisor
        /// </summary>
        /// <param name="employeeId">The ID of the required employee</param>
        /// <returns>Boolean</returns>
        public bool IsSupervisor(int employeeId)
        {
            List<ParmStruct> parms = new List<ParmStruct> { new ParmStruct("@EmployeeId", employeeId, SqlDbType.Int) };

            return Convert.ToBoolean(db.ExecuteScaler("spSupervisorCheck", parms));
        }

        #endregion region

    }
}
