using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// A DTO used for the web - Getting supervisors list of reviews, then inserting a new review
    /// </summary>
   public class ReviewListDTO
    {
        public int ReviewId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long EmployeeId { get; set; }
        public long CurrentSupervisorId { get; set; }
        public string ReviewDate { get; set; }
        public string HasQuarterReview { get; set; }

    }
}
