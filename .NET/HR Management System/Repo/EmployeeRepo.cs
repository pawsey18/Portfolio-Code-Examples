using DAL;
using Model;
using Model.DTO;
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
    /// Employee Repo
    /// </summary>
    public class EmployeeRepo
    {

        DataAccess db = new DataAccess();

        /// <summary>
        /// Add an employee to the database
        /// </summary>
        /// <param name="e">The Employee object to be populated with data</param>
        /// <returns>The employee object if there are records, else it returns a null object</returns>
        public Employee AddEmployee(Employee e)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                
                new ParmStruct("@EmployeeId", e.EmployeeId, SqlDbType.Int, 0),
                new ParmStruct("@FirstName", e.FirstName, SqlDbType.VarChar, 20),
                new ParmStruct("@MiddleInit", e.MiddleInit, SqlDbType.Char, 1),
                new ParmStruct("@LastName", e.LastName, SqlDbType.VarChar, 50),
                new ParmStruct("@StreetAddress", e.StreetAddress, SqlDbType.VarChar, 50),
                new ParmStruct("@City", e.City, SqlDbType.VarChar, 30),
                new ParmStruct("@PostalCode", e.PostalCode, SqlDbType.VarChar, 7),
                new ParmStruct("@DOB", e.DOB, SqlDbType.Date),
                new ParmStruct("@SIN", e.SIN, SqlDbType.VarChar, 11),
                new ParmStruct("@SeniorityDate", e.SeniorityDate, SqlDbType.Date),
                new ParmStruct("@JobStartDate", e.JobStartDate, SqlDbType.Date),
                new ParmStruct("@Supervisor", e.Supervisor, SqlDbType.Bit),
                new ParmStruct("@CurrentSupervisorId", e.CurrentSupervisorId, SqlDbType.Int, 0),
                new ParmStruct("@WorkPhone", e.WorkPhone, SqlDbType.VarChar, 12),
                new ParmStruct("@CellPhone", e.CellPhone, SqlDbType.VarChar, 12),
                new ParmStruct("@Email", e.Email, SqlDbType.VarChar, 40),
                new ParmStruct("@Password", e.Password, SqlDbType.VarChar, 30),
                new ParmStruct("@DepartmentId", e.DepartmentId, SqlDbType.Int, 0),
                new ParmStruct("@StatusId ", e.StatusId, SqlDbType.Int, 0),
                new ParmStruct("@JobAssignmentId", e.JobAssignmentId, SqlDbType.Int, 0),
                new ParmStruct("@OfficeLocation", e.JobAssignmentId, SqlDbType.VarChar),
                new ParmStruct("@RecordVersion", DBNull.Value,SqlDbType.Timestamp ,0, ParameterDirection.InputOutput)
            };

            //if (db.ExecuteNonQuery("spCreateEmployee", parms) > 0)
            //{
            //    return e;
            //}
            //return e;
            db.ExecuteNonQuery("spCreateEmployee", parms, CommandType.StoredProcedure);

            e.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;

            return e;

        }

        /// <summary>
        /// Update the employee record for Desktop 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Employee UpdateEmployeeDesktop(Employee e)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@RecordVersion", e.RecordVersion, SqlDbType.Timestamp,0, ParameterDirection.InputOutput),
                new ParmStruct("@EmployeeId", e.EmployeeId, SqlDbType.Int, 0),
                new ParmStruct("@FirstName", e.FirstName, SqlDbType.VarChar, 20),
                new ParmStruct("@MiddleInit", e.MiddleInit, SqlDbType.Char, 1),
                new ParmStruct("@LastName", e.LastName, SqlDbType.VarChar, 50),
                new ParmStruct("@StreetAddress", e.StreetAddress, SqlDbType.VarChar, 50),
                new ParmStruct("@City", e.City, SqlDbType.VarChar, 30),
                new ParmStruct("@PostalCode", e.PostalCode, SqlDbType.VarChar, 7),
                new ParmStruct("@SIN", e.SIN, SqlDbType.VarChar, 11),
                new ParmStruct("@SeniorityDate", e.SeniorityDate, SqlDbType.Date),
                new ParmStruct("@JobStartDate", e.JobStartDate, SqlDbType.Date),
                new ParmStruct("@Supervisor", e.Supervisor, SqlDbType.Bit),
                new ParmStruct("@CurrentSupervisorId", e.CurrentSupervisorId, SqlDbType.Int, 0),
                new ParmStruct("@WorkPhone", e.WorkPhone, SqlDbType.VarChar, 12),
                new ParmStruct("@CellPhone", e.CellPhone, SqlDbType.VarChar, 12),
                new ParmStruct("@DepartmentId", e.DepartmentId, SqlDbType.Int, 0),
                new ParmStruct("@StatusId ", e.StatusId, SqlDbType.Int, 0),
                new ParmStruct("@JobAssignmentId", e.JobAssignmentId, SqlDbType.Int, 0),
                new ParmStruct("@OfficeLocation", e.OfficeLocation, SqlDbType.VarChar),
                     new ParmStruct("@Email", e.Email, SqlDbType.VarChar, 40)
            };

            db.ExecuteNonQuery("spUpdateEmployeeDesktop", parms, CommandType.StoredProcedure);

            e.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;

            return e;
        }



        /// <summary>
        /// Update the employee's personal record
        /// </summary>
        /// <param name="emp"></param>
        /// <returns>EmployeeInformationDTO</returns>
        public EmployeeInformationDTO UpdateEmployeePersonalRecord(EmployeeInformationDTO emp)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@RecordVersion", emp.RecordVersion, SqlDbType.Timestamp,0,ParameterDirection.InputOutput),
                new ParmStruct("@EmployeeId", emp.EmployeeId, SqlDbType.Int),
                new ParmStruct("@FirstName", emp.FirstName, SqlDbType.NVarChar, 30),
                new ParmStruct("@MiddleName", emp.MiddleName, SqlDbType.Char, 1),
                new ParmStruct("@LastName", emp.LastName, SqlDbType.NVarChar, 50),
                new ParmStruct("@Password ", emp.Password, SqlDbType.VarChar),
                new ParmStruct("@StreetAddress", emp.StreetAddress, SqlDbType.VarChar, 50),
                new ParmStruct("@City", emp.City, SqlDbType.VarChar, 30),
                new ParmStruct("@PostalCode", emp.PostalCode, SqlDbType.VarChar, 7)
            };

            db.ExecuteNonQuery("spEmployeePersonalRecordUpdate", parms);

            emp.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;

            return emp;
        }


        /// <summary>
        ///  Get all of the Employees for desktop
        /// </summary>
        /// <param name="employee">Using the Employee Id and LastName to filter the employee in the stored procedure</param>
        /// <returns>A list of employees</returns>
        public List<EmployeeSearchDTO> GetAllEmps(EmployeeSearchDTO employee)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                 new ParmStruct("@EmployeeId", employee.EmployeeId, SqlDbType.Int, 0),
                 new ParmStruct("@LastName", employee.LastName, SqlDbType.VarChar, 20),

            };

            DataTable dt = db.Execute("spGetEmployeeList", parms, CommandType.StoredProcedure);

            List<EmployeeSearchDTO> employees = new List<EmployeeSearchDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(
                    new EmployeeSearchDTO
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                        FullName = row["FullName"].ToString(),
                        LastName = row["LastName"].ToString(),
                    }
                );
            }
            return employees;
        }

        /// <summary>
        ///  Get the employee details for Web.
        /// </summary>
        /// <returns>A list of employees</returns>
        public List<EmployeeInformationDTO> GetEmployeeInformation()
        {
            DataTable dt = db.Execute("spGetEmployeeInformation", null, CommandType.StoredProcedure);

            List<EmployeeInformationDTO> employees = new List<EmployeeInformationDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(
                    new EmployeeInformationDTO
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                        Password = row["Password"].ToString(),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        MiddleName = row["MiddleInit"].ToString(),
                        StreetAddress = row["StreetAddress"].ToString(),
                        City = row["City"].ToString(),
                        PostalCode = row["PostalCode"].ToString(),
                        RecordVersion = (byte[])row["RecordVersion"]
                    }
                );
            }
            return employees;
        }

        /// <summary>
        ///  Using the Mobile Search Employee DTO for API calls
        /// </summary>
        /// <returns>Returns a list of DTO objects if there are records found</returns>
        public List<MobileSearchEmployeeDTO> GetAllEmpWithJoinsForAPI()
        {
            DataTable dt = db.Execute("spGetEmployeesListFullDetails", null, CommandType.StoredProcedure);

            List<MobileSearchEmployeeDTO> employees = new List<MobileSearchEmployeeDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(
                    new MobileSearchEmployeeDTO
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                        LastName = row["LastName"].ToString(),
                        MiddleName = row["MiddleInit"].ToString(),
                        FirstName = row["FirstName"].ToString(),
                        WorkPhone = row["WorkPhone"].ToString(),
                        CellPhone = row["CellPhone"].ToString(),
                        Email = row["Email"].ToString(),
                        StreetAddress = row["StreetAddress"].ToString(),
                        JobTitle = row["JobTitle"].ToString(),
                        Active = Convert.ToBoolean(row["Active"]),
                        DepartmentId = Convert.ToInt32(row["DepartmentId"])

                    }
                );
            }
            return employees;
        }

        /// <summary>
        ///  Get's all of the Employees without parameters for getting a list of employees on form load in the Employee Search Module.
        /// </summary>
        /// <returns>A List of Employee Search DTO objects if there are records</returns>
        public List<EmployeeSearchDTO> GetAllEmpWithNoParams()
        {
            DataTable dt = db.Execute("spGetEmployeeListWithoutParams", null, CommandType.StoredProcedure);
            List<EmployeeSearchDTO> employees = new List<EmployeeSearchDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(
                    new EmployeeSearchDTO
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                        FullName = row["FullName"].ToString(),
                       

                    }
                );
            }
            return employees;
        }




        /// <summary>
        ///    Checks if the Employee being inserted has at least 1 Supervisor and 10 Employees per department.
        ///    Get all employees in a department where there is a supervisor and the current supervisor supervises 10 or less employees.
        /// </summary>
        /// <param name="departmentId">Uses the DepartmentId to filter the where clause and uses passed in value to filter</param>
        /// <param name="currentSupervisorId">Using the current supervisor id to filter</param>
        /// <returns>A bool returning whether the condition has been met.</returns>
        public bool IsMaxEmployeesPerSupervisor(int departmentId, int currentSupervisorId, long employeeId, int insertOrUpdate)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@DepartmentId", departmentId, SqlDbType.Int, 0),
                new ParmStruct("@CurrentSupervisorId", currentSupervisorId, SqlDbType.Int),
                new ParmStruct("@EmployeeId", employeeId, SqlDbType.Int),
                new ParmStruct("@InsertOrUpdate ", insertOrUpdate, SqlDbType.Int),
            };
            return Convert.ToInt32(db.ExecuteScaler("spIsSupervising10Employees", parms, CommandType.StoredProcedure)) > 0;
        }


        /// <summary>
        ///  Checking that the Employees Social Insurance Number is unique.
        /// </summary>
        /// <param name="sin"></param>
        /// <returns></returns>
        public bool IsSINUnique(string sin, long employeeId)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@SIN", sin, SqlDbType.VarChar, 11),
                new ParmStruct("@EmployeeId", employeeId, SqlDbType.Int),

            };

            return Convert.ToInt32(db.ExecuteScaler("spIsSINUnique", parms, CommandType.StoredProcedure)) > 0;
        }

        /// <summary>
        /// Get's the current CEO ID for when Employees add supervisors when there are no supervisor in a department
        /// </summary>
        /// <returns>A CEO DTO Object that is populated with the data retrieved </returns>
        public CEODTO GetCurrentCEOIDForNewSupervisor()
        {
            DataTable dt = db.Execute("spGetCEOIDForSupevisor", null, CommandType.StoredProcedure);
            return PopulateCEO(dt.Rows[0]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public CEODTO PopulateCEO(DataRow row)
        {
            return new CEODTO()
            {
                CEOID = Convert.ToInt32(row["EmployeeId"]),
            };
        }

        /// <summary>
        ///  Get the employee by ID
        /// </summary>
        /// <param name="id">Using the Employee ID to filter to get specific employee </param>
        /// <returns>A populated Employee</returns>
        public Employee GetEmployeeById(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                 new ParmStruct("@EmployeeId", id, SqlDbType.Int)

            };

            DataTable dt = db.Execute("spGetSingleEmployeeById", parms, CommandType.StoredProcedure);
            return PopulateEmployeeRecords(dt.Rows[0]);
        }

        /// <summary>
        /// Populate an employee object
        /// </summary>
        /// <param name="row"></param>
        /// <returns>A new employee</returns>
        private Employee PopulateEmployeeRecords(DataRow row)
        {
            return new Employee()
            {
                FirstName = row["FirstName"].ToString(),
                MiddleInit = row["MiddleInit"].ToString(),
                LastName = row["LastName"].ToString(),
                StreetAddress = row["StreetAddress"].ToString(),
                WorkPhone = row["WorkPhone"].ToString(),
                CellPhone = row["CellPhone"].ToString(),
                City = row["City"].ToString(),
                Email = row["Email"].ToString(),
                PostalCode = row["PostalCode"].ToString(),
                SIN = row["SIN"].ToString(),
                Supervisor = Convert.ToBoolean(row["Supervisor"]),
                SeniorityDate = Convert.ToDateTime(row["SeniorityDate"]),
                StatusId = Convert.ToInt32(row["StatusId"]),
                DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                JobAssignmentId = Convert.ToInt32(row["JobAssignmentId"]),
                OfficeLocation = row["OfficeLocation"].ToString(),
                JobStartDate = Convert.ToDateTime(row["JobStartDate"]),
                CurrentSupervisorId = Convert.ToInt32(row["CurrentSupervisorId"]),
                DOB = Convert.ToDateTime(row["DOB"]),
                RecordVersion = (byte[])row["RecordVersion"]
                
            };
        }
    }
}
