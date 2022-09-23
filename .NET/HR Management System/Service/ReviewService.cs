using Model;
using Model.DTO;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
  
    public class ReviewService
    {
        private ReviewRepo repo = new ReviewRepo();

        /// <summary>
        /// Get the list of all the employees that have a quarter review
        /// </summary>
        /// <param name="supervisorId"></param>
        /// <returns></returns>
        public List<ReviewListDTO> GetEmployeesDueForReviewForSupervisor(int supervisorId)
        {
            return repo.GetEmployeesDueForReview(supervisorId);
        }

        /// <summary>
        /// Insert the date when email was sent
        /// </summary>
        /// <returns></returns>
        public bool InsertEmailReminderDate()
        {
            return repo.AddEmailReminder();
        }

        /// <summary>
        ///  Check if there is email sent date for today
        /// </summary>
        /// <returns></returns>
        public bool CheckDate()
        {
            if (repo.CheckDate())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get's the list of pending reviews for the supervisor's email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<MailListDTO> GetEmailListForSupervisor(int id)
        {
            return repo.GetEmailListForSupervisor(id);
        }

        /// <summary>
        /// Gte supervisors emails and id's
        /// </summary>
        /// <returns></returns>
        public List<SupervisorsEmailAndIDDTO> GetSupervisorsEmailsAndID()
        {
            return repo.GetSupervisorsEmailAndID();
        }


        /// <summary>
        /// Get the list of reviews for a specific employee by employee id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<ShowEmployeePerformanceReviewsDTO> ShowEmployeePerformanceReviews(int employeeId)
        {
            return repo.GetEmployeePerformanceReviews(employeeId);
        }


        /// <summary>
        /// Supervisors to add a new employee review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public Review AddReview(Review review)
        {
           return repo.AddReview(review);
        }
    }
}
