using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Review entity that models the review table in db
    /// </summary>
    public class Review
    {
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public int RatingId { get; set; }
        public int EmployeeId { get; set; }
        public int CurrentSupervisorId { get; set; }

    }
}
