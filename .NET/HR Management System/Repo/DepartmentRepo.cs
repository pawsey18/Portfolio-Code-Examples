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
    public class DepartmentRepo
    {
        DataAccess db = new DataAccess();


        /// <summary>
        /// Add Department
        /// </summary>
        /// <param name="department"></param>
        /// <returns>Department</returns>
        public Department AddDepartment(Department department)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
               // new ParmStruct("@DepartmentInsertedId",department.DepartmentInsertedId,SqlDbType.Int,0, ParameterDirection.Output),
                new ParmStruct("@DepartmentName", department.DepartmentName, SqlDbType.VarChar, 20),
                new ParmStruct("@Description ", department.Description, SqlDbType.VarChar, 30),
                new ParmStruct("@InovationDate ", department.InvocationDate, SqlDbType.Date),
                new ParmStruct("@Active ", department.Active, SqlDbType.Bit),
                new ParmStruct("@RecordVersion",DBNull.Value, SqlDbType.Timestamp,0, ParameterDirection.Input)
                // To insert row version - use DbNull.Value and set sql db type type to timestamp with parameter direction as input
                // instead of using output 
            };



            if (db.ExecuteNonQuery("spCreateDepartment", parms, CommandType.StoredProcedure) > 0)
            {
                return department;
            }
            return department;
        }



        /// <summary>
        /// Delete the department 
        /// </summary>
        /// <param name="e"></param>
        /// <returns>boolean value based on if the department was deleted</returns>
        public bool DeleteDepartment(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@DepartmentId", id, SqlDbType.Int, 0),
            };

            //e.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
            if (db.ExecuteNonQuery("spDeleteDepartmentById", parms, CommandType.StoredProcedure) > 1)
            {
                return true;
            }
            return false;
        }



        /// <summary>
        ///  Get the department details for the department that the supervisor works in
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<SupervisorModifyDepartmentDTO> GetSupervisorsDepartmentInformation(int employeeId)
        {

            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@EmployeeId", employeeId, SqlDbType.Int)

            };


            DataTable dt = db.Execute("spGetEmployeeWithDepartmentDetailsWeb", parms, CommandType.StoredProcedure);

            List<SupervisorModifyDepartmentDTO> departments = new List<SupervisorModifyDepartmentDTO>();

            foreach (DataRow row in dt.Rows)
            {
                departments.Add(
                    new SupervisorModifyDepartmentDTO
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                        DepartmentId = Convert.ToInt32(row["DepartmentId"].ToString()),
                        DepartmentName = row["DepartmentName"].ToString(),
                        Description = row["Description"].ToString(),
                        InvocationDate = Convert.ToDateTime(row["InvocationDate"]),
                        RecordVersion = (byte[])row["RecordVersion"]
                    }
                );
            }
            return departments;
        }


        /// <summary>
        /// Update the department record for Desktop 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public Department UpdateDepartmentDesktop(Department d)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@RecordVersion",d.RecordVersion, SqlDbType.Timestamp,0, ParameterDirection.InputOutput),
                new ParmStruct("@DepartmentId", d.DepartmentId, SqlDbType.Int),
                new ParmStruct("@DepartmentName", d.DepartmentName, SqlDbType.VarChar, 20),
                new ParmStruct("@Description", d.Description, SqlDbType.VarChar, 30),
                new ParmStruct("@InovationDate", d.InvocationDate, SqlDbType.Date),
                new ParmStruct("@Active", d.Active, SqlDbType.Bit),

            };

            db.ExecuteNonQuery("spUpdateDepartmentDesktop", parms, CommandType.StoredProcedure);

            d.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;

            return d;
        }

        public SupervisorDepartmentUpdateDTO UpdateDepartmentWeb(SupervisorDepartmentUpdateDTO d)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
                new ParmStruct("@DepartmentId", d.DepartmentId, SqlDbType.Int),
                new ParmStruct("@DepartmentName", d.DepartmentName, SqlDbType.VarChar, 20),
                new ParmStruct("@Description", d.Description, SqlDbType.VarChar, 30),
                new ParmStruct("@RecordVersion",d.RecordVersion, SqlDbType.Timestamp,0, ParameterDirection.InputOutput),
            };

            db.ExecuteNonQuery("spSupervisorDepartmentUpdateWeb", parms, CommandType.StoredProcedure);

            d.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;

            return d;
        }


        /// <summary>
        ///  Get the department by ID
        /// </summary>
        /// <param name="id">Using the department ID to filter to get specific department details </param>
        /// <returns>A populated Employee</returns>
        public Department GetDepartmentById(int id)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                 new ParmStruct("@DepartmentId", id, SqlDbType.Int)
            };

            DataTable dt = db.Execute("spDepartmentEmployeeById", parms, CommandType.StoredProcedure);
            return PopulateDepartmentRecords(dt.Rows[0]);
        }


        /// <summary>
        /// Get a list of departments
        /// </summary>
        /// <returns>Returns a list of departments </returns>
        public List<Department> GetDepartments()
        {
            DataTable dt = db.Execute("spGetDepartments", null, CommandType.StoredProcedure);
            List<Department> departments = new List<Department>();

            foreach (DataRow row in dt.Rows)
            {
                departments.Add(
                    new Department
                    {
                        DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                        DepartmentName = row["DepartmentName"].ToString(),
                        Description = row["Description"].ToString(),
                        InvocationDate = Convert.ToDateTime(row["InvocationDate"]),
                        Active = Convert.ToBoolean(row["Active"]),

                    }
                );
            }
            return departments;
        }


        /// <summary>
        /// Checks to see if the department name being inserted is unique or not
        /// </summary>
        /// <param name="departmentName">Using the department name to query the database to check condition</param>
        /// <returns>A true or false value based on user input entered into the department name text field</returns>
        public bool IsDepartmentNameUnique(string departmentName)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
               // new ParmStruct("@DepartmentId ", departmentId, SqlDbType.Int),
                new ParmStruct("@DepartmentName", departmentName, SqlDbType.VarChar, 20)

            };

            return Convert.ToInt32(db.ExecuteScaler("spIsDepartmentUnique", parms, CommandType.StoredProcedure)) > 0;
        }

        /// <summary>
        ///  Checking to see if the department to be edited has no employees.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public bool IsDepartmentEmpty(int departmentId)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@DepartmentId", departmentId, SqlDbType.Int),

            };

            return Convert.ToInt32(db.ExecuteScaler("spIsDepartmentEmpty", parms, CommandType.StoredProcedure)) > 0;
        }

        /// <summary>
        ///  Checking to see if an attempt has been made to set invocation date before the creation date
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public bool IsInvocationDateBeforeCreationDate(int departmentId, DateTime invocationDate)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                new ParmStruct("@DepartmentId", departmentId, SqlDbType.Int),
                new ParmStruct("@InvocationDate", invocationDate, SqlDbType.Date),


            };

            return Convert.ToInt32(db.ExecuteScaler("spIsInvocationDateBeforeCreationDate", parms, CommandType.StoredProcedure)) > 0;
        }



        /// <summary>
        ///  Populate the department record
        /// </summary>
        /// <param name="row"></param>
        /// <returns>Department</returns>
        private Department PopulateDepartmentRecords(DataRow row)
        {
            return new Department()
            {
                DepartmentName = (row["DepartmentName"]).ToString(),
                Description = row["Description"].ToString(),
                InvocationDate = Convert.ToDateTime(row["InvocationDate"]),
                Active = Convert.ToBoolean(row["Active"]),
                RecordVersion = (byte[])row["RecordVersion"]
            };
        }
    }
}
