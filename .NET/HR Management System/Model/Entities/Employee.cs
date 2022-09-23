using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Model
{
    /// <summary>
    /// Model for Database Entity: Employee
    /// </summary>
    public class Employee : BaseEntity
    {

        #region Properties
       
        public long EmployeeId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name has to be between 2 and 50 characters.")]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name has to be between 3 and 50 characters.")]
        public string LastName { get; set; }

        public string MiddleInit { get; set; }

        [Required(ErrorMessage = "Street address is required")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[A-Za-z]{1}\d{1}[A-Za-z]{1}\d{1}$")]
        public string PostalCode { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "SIN in the wrong format.")]
        [RegularExpression(@"^\d{3}-?\d{3}-?\d{3}$")]
        public string SIN { get; set; }

        [Required]
        public DateTime SeniorityDate { get; set; }
     
        [Required]
        public DateTime JobStartDate { get; set; }

        public bool Supervisor { get; set; }

        public int SupervisorId { get; set; }

        [Required(ErrorMessage = "Must select another department")]
        public int CurrentSupervisorId { get; set; }

        [Required]
        public string WorkPhone { get; set; }

        public string CellPhone { get; set; }
        
       
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$")]
        public string Email { get; set; }


       
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long. contain 1 upper case and 1 special character", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public int JobAssignmentId { get; set; }

        public int DepartmentId { get; set; }

        [Required]
        public int StatusId { get; set; }

        public string OfficeLocation { get; set; }

        public byte[] RecordVersion { get; set; }


        // testing
        public int ReturnedInsertedId { get; set; }

        #endregion

        #region Public Methods

        public void DisplayEmployee()
        {

        }

        public void UpdateEmployee()
        {

        }

        public void InsertEmployee()
        {

        }

        public void SearchEmployee()
        {

        }

        public void ShowErrorMessage()
        {

        }

        public void ShowSuccessMessage()
        {

        }

        #endregion
    }
}
