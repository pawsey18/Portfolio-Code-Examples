using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// DTO for holding a joined query to display data to the employee
    /// </summary>
    public class ShowEmployeePerformanceReviewsDTO
    {
        public int ReviewId { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public string FullName { get; set; }
        public string ReviewDate { get; set; }
        public string Comment { get; set; }
        public string Rating { get; set; }
    }
}
