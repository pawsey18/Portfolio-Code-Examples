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
    public class ReviewRepo
    {
        DataAccess db = new DataAccess();

        /// <summary>
        /// Adding a new review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public Review AddReview(Review review)
        {
            List<ParmStruct> parms = new List<ParmStruct>()
            {
               // new ParmStruct("@DepartmentInsertedId",department.DepartmentInsertedId,SqlDbType.Int,0, ParameterDirection.Output),
                new ParmStruct("@Comment ", review.Comment, SqlDbType.VarChar),
                new ParmStruct("@ReviewDate ", review.ReviewDate, SqlDbType.DateTime),
                new ParmStruct("@RatingId ", review.RatingId, SqlDbType.Int),
                new ParmStruct("@EmployeeId ", review.EmployeeId, SqlDbType.Int),
                new ParmStruct("@CurrentSupervisorId",review.CurrentSupervisorId, SqlDbType.Int)
            };


            if (db.ExecuteNonQuery("spAddEmployeeReview", parms, CommandType.StoredProcedure) > 0)
            {
                //  department.DepartmentInsertedId = (int)parms.Where(p => p.Name == "@DepartmentInsertedId").FirstOrDefault().Value;
                // department.RecordVersion = (byte[])parms.Where(p => p.Name == "@RecordVersion").FirstOrDefault().Value;
                return review;
            }

            return review;
        }
        /// <summary>
        /// Get list for employees to view performance reviews
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>

        public List<ShowEmployeePerformanceReviewsDTO> GetEmployeePerformanceReviews(int employeeId)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                 new ParmStruct("@EmployeeId", employeeId, SqlDbType.Int, 0),
            };

            DataTable dt = db.Execute("spShowEmployeeReviews", parms, CommandType.StoredProcedure);

            List<ShowEmployeePerformanceReviewsDTO> employees = new List<ShowEmployeePerformanceReviewsDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(
                    new ShowEmployeePerformanceReviewsDTO
                    {
                        ReviewId = Convert.ToInt32(row["ReviewId"]),
                        Year = Convert.ToInt32(row["Year"]),
                        Quarter = Convert.ToInt32(row["Quarter"]),
                        FullName = row["FullName"].ToString(),
                        ReviewDate = row["ReviewDate"].ToString(),
                        Comment = row["Comment"].ToString(),
                        Rating = row["Rating"].ToString(),
                    }
                );
            }
            return employees;
        }

        /// <summary>
        /// Get email list for supervisor
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<MailListDTO> GetEmailListForSupervisor(int employeeId)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                 new ParmStruct("@EmployeeId", employeeId, SqlDbType.Int, 0),
            };

            DataTable dt = db.Execute("spGetSupervisorListForEmail", parms, CommandType.StoredProcedure);

            List<MailListDTO> employees = new List<MailListDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(
                    new MailListDTO
                    {
                        FullName = row["FullName"].ToString(),
                    }
                );
            }
            return employees;
        }

        /// <summary>
        /// Get the list of supervisors emails and id's
        /// </summary>
        /// <returns></returns>
        public List<SupervisorsEmailAndIDDTO> GetSupervisorsEmailAndID()
        {
            DataTable dt = db.Execute("spGetSupervisorsEmailAndID", null, CommandType.StoredProcedure);

            List<SupervisorsEmailAndIDDTO> employees = new List<SupervisorsEmailAndIDDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(
                    new SupervisorsEmailAndIDDTO
                    {
                        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                        Email = row["Email"].ToString()
                    }
                );
            }
            return employees;
        }

        /// <summary>
        /// Insert the date into the Email Reminder table to be compared
        /// </summary>
        /// <returns></returns>
        public bool AddEmailReminder()
        {
            if (db.ExecuteNonQuery("spInsertEmailReminder", null, CommandType.StoredProcedure) > 0)
            {
          
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if there is a date inserted for today
        /// </summary>
        /// <returns>False if there are no dates inserted already for todays date</returns>
        public bool CheckDate()
        {
            DataTable dt = db.Execute("spCheckDate", null, CommandType.StoredProcedure);

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }



        /// <summary>
        ///  Get a list of employees that are up for this quarter review or any employee that 
        ///  hasn't been reviewed from the previous quarter 
        /// </summary>
        /// <param name="supervisorId"></param>
        /// <returns></returns>

        public List<ReviewListDTO> GetEmployeesDueForReview(int supervisorId)
        {
            List<ParmStruct> parms = new List<ParmStruct>
            {
                 new ParmStruct("@EmployeeId", supervisorId, SqlDbType.Int, 0),
            };

            DataTable dt = db.Execute("spGetEmployeesDueForReview", parms, CommandType.StoredProcedure);

            List<ReviewListDTO> employees = new List<ReviewListDTO>();

            foreach (DataRow row in dt.Rows)
            {
                employees.Add(
                    new ReviewListDTO
                    {
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        EmployeeId = Convert.ToInt32(row["EmployeeId"]),
                        CurrentSupervisorId = Convert.ToInt32(row["CurrentSupervisorId"]),
                        ReviewDate = row["ReviewDate"].ToString(),
                        HasQuarterReview = row["HasQuarterReview"].ToString()
                    }
                );
            }
            return employees;
        }
    }
}
