using Model;
using Model.DTO;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmployeeService
    {
        private EmployeeRepo repo = new EmployeeRepo();

        
        public static string formPw;


        /// <summary>
        /// Add employee record to database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee</returns>
        public Employee AddEmployee(Employee employee)
        {
            IsMaxEmployeesPerSupervisor(employee, 1);
            if (Validate(employee))
                return repo.AddEmployee(employee);

            return employee;

        }

        /// <summary>
        ///  Update employee for desktop
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>Employee</returns>

        public Employee UpdateEmployeeDesktop(Employee employee)
        {
        
            IsMaxEmployeesPerSupervisor(employee, 0);
            if (Validate(employee))
                return repo.UpdateEmployeeDesktop(employee);

            return employee;
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>EmployeeInformationDTO</returns>
        public EmployeeInformationDTO UpdatePersonalEmployeeRecord(EmployeeInformationDTO employee)
        {
                if (employee != null)
                return repo.UpdateEmployeePersonalRecord(employee);

            return null;
        }



        /// <summary>
        /// Get a particular employee by their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee</returns>
        public Employee GetEmployeeById(int id)
        {
            return repo.GetEmployeeById(id);
        }


        #region Desktop Search Employee

        /// <summary>
        /// Get all the employees
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>EmployeeSearchDTO</returns>
        public List<EmployeeSearchDTO> GetAllEmps(EmployeeSearchDTO employee)
        {
            return repo.GetAllEmps(employee);
        }

        /// <summary>
        /// Get all the employees with no parameters - being used when search fields are empty to get all employees.
        /// </summary>
        /// <returns>EmployeeSearchDTO</returns>
        public List<EmployeeSearchDTO> GetAllEmployeesNoParams()
        {
            return repo.GetAllEmpWithNoParams();
        }
        #endregion

        #region Android

        /// <summary>
        /// Get the list of employees for obtaining their information
        /// </summary>
        /// <returns>List of EmployeeInformationDTO </returns>
        public List<EmployeeInformationDTO> GetEmployeeInformationDTO()
        {
            return repo.GetEmployeeInformation();
        }

        /// <summary>
        /// Get list of employees for api
        /// </summary>
        /// <returns></returns>
        public List<MobileSearchEmployeeDTO> GetAllEmpsForAPI()
        {
            return repo.GetAllEmpWithJoinsForAPI();
        }

        #endregion



        /// <summary>
        /// A method to verifiy an employees age is 16 or greater
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private void IsLegalAge(Employee employee)
        {
            DateTime now = DateTime.Now;
            int nowYear = now.Year;
            DateTime birthDate = employee.DOB;
            int birthYear = birthDate.Year;
            int age = Convert.ToInt32(nowYear - birthYear);
            if (age < 16)
            {
                employee.Errors.Add(new ValidationError("Employee must be 16 years of age or greater."));
            }
        }

        /// <summary>
        /// Check if employee is reitred, if so then disable status combo box
        /// </summary>
        /// <param name="statusId"></param>
        /// <returns>true or false if condition has been met</returns>
        public bool IfEmployeeIsRetiredDisableStatus(int statusId)
        {
            if (statusId == 2)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Check to see if the employee is at the age of retirement.
        /// </summary>
        /// <param name="dob"></param>
        /// <param name="statusId"></param>
        /// <returns>a boolean value if employee is not age of retirement</returns>
        public bool IsAgeOfRetirement(DateTime dob, int statusId)
        {
            DateTime now = DateTime.Now;
            int nowYear = now.Year;
            DateTime birthDate = dob;
            int birthYear = birthDate.Year;
            int age = Convert.ToInt32(nowYear - birthYear);
            if (age < 65 && statusId == 2)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        ///  Generating a random Employee ID
        /// </summary>
        /// <returns></returns>
        public int GenerateUnique8Digits()
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 8; i++)
            {
                int number = random.Next(0, 8);
                sb.Append(number);
            }
            return Convert.ToInt32(sb.ToString());
        }

        /// <summary>
        ///  Check to see if the supervisors have 10 employees already
        /// </summary>
        /// <param name="employee"></param>
        public void IsMaxEmployeesPerSupervisor(Employee employee, int insertOrUpdate)
        {
            if (repo.IsMaxEmployeesPerSupervisor(employee.DepartmentId, employee.CurrentSupervisorId, employee.EmployeeId, insertOrUpdate))
            {
                employee.Errors.Add(new ValidationError("A maximum of 10 employees and 1 supervisor can be assigned.\n" +
                    " Additional employees to a department will require additional supervisors assigned."));
            }
        }

        /// <summary>
        /// Check employee's SIN is unique 
        /// </summary>
        /// <param name="employee"></param>
        public void IsSINUnique(Employee employee, long id)
        {
            if (repo.IsSINUnique(employee.SIN, employee.EmployeeId))
            {
                employee.Errors.Add(new ValidationError("SIN must be unique\n" +
                    " Please re-enter your SIN and try again."));
            }
        }


        /// <summary>
        /// Check to see if the start date is valid
        /// </summary>
        /// <param name="employee"></param>
        private void IsValidStartDate(Employee employee)
        {
           
            if (employee.JobStartDate < employee.SeniorityDate)
            {
                employee.Errors.Add(new ValidationError("Job start date cannot be prior to seniority date."));
            }
        }

        /// <summary>
        ///  Gets the current CEO employee ID
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public CEODTO GetCEOIDForSupervisor()
        {
            return repo.GetCurrentCEOIDForNewSupervisor();
        }

        /// <summary>
        ///  Saving the form's password as a by to bypass the validation wit the data annotations for password on the Search Employee form.
        /// </summary>
        /// <param name="formPassword"></param>
        /// <returns></returns>
        public  string OldFormPassword(string formPassword)
        {
            return formPw = formPassword;
        }


        /// <summary>
        /// Validate the employee model and business rules
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private bool Validate(Employee employee)
        {
          
            IsLegalAge(employee);
            IsValidStartDate(employee);
            IsSINUnique(employee, employee.EmployeeId);

            ValidationContext context = new ValidationContext(employee);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(employee,
                context, results, true);

            foreach (ValidationResult e in results)
            {
                employee.AddError(new ValidationError(e.ErrorMessage));
            }
            return employee.Errors.Count == 0;
        }
    }
}
