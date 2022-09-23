using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Database Entity for Departments
    /// </summary>
    public class Department : BaseEntity
    {
        public int DepartmentId { get; set; }

        [Required (ErrorMessage = "Department name is required.")]
        [StringLength(128, MinimumLength = 3, ErrorMessage = "Department name is mimimum of 3 and max 128 characters")]
        public string DepartmentName { get; set; }
     
        [Required(ErrorMessage = "Department description is required.")]
        [StringLength(512, ErrorMessage = "Department description must not exceed 512 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Invocation Date is required.")]
        public DateTime InvocationDate { get; set; }

        public bool Active { get; set; }



       // public int DepartmentInsertedId { get; set; }
        public byte[] RecordVersion { get; set; }

    }
}
