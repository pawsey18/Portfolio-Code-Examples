using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Service
{
    public class DepartmentService
    {
        private DepartmentRepo repo = new DepartmentRepo();


        /// <summary>
        /// Add a department 
        /// </summary>
        /// <param name="department"></param>
        /// <returns>Department</returns>
        public Department AddDepartment(Department department)
        {
            DepartmentNameIsUnique(department);
            if (Validate(department))
                return repo.AddDepartment(department);

            return department;
        }

        /// <summary>
        /// Update department - Desktop
        /// </summary>
        /// <param name="department"></param>
        /// <returns>Department</returns>
        public Department UpdateDepartmentDesktop(Department department)
        {
            if (Validate(department))
                return repo.UpdateDepartmentDesktop(department);

            return department;
        }

        /// <summary>
        ///  Update the supervisors department for web
        /// </summary>
        /// <param name="department"></param>
        /// <returns>SupervisorDepartmentUpdateDTO</returns>
        public SupervisorDepartmentUpdateDTO UpdateDepartmentWeb(SupervisorDepartmentUpdateDTO department)
        {
            return repo.UpdateDepartmentWeb(department);
        }

        /// <summary>
        /// Get a list of departments
        /// </summary>
        /// <returns>List of department objects </returns>
        public List<Department> GetDepartments()
        {
            return repo.GetDepartments();
        }

        /// <summary>
        ///  Delete department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>boolean value based on if it has or has not been deleted</returns>
        public bool DeleteDepartment(int id)
        {
            return repo.DeleteDepartment(id);
        }

        /// <summary>
        /// Get department by id for updates.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The department</returns>
        public Department GetDepartmentById(int id)
        {
            return repo.GetDepartmentById(id);
        }


        /// <summary>
        /// Check if department has no employees
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>boolean value based on condition</returns>
        public bool DepartmentIsEmpty(int departmentId)
        {
            if (repo.IsDepartmentEmpty(departmentId))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        ///  Get Details for department by id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>A list of details for specific department by id</returns>
        public List<SupervisorModifyDepartmentDTO> GetSupervisorDepartmentDetailsDTO(int employeeId)
        {
            return repo.GetSupervisorsDepartmentInformation(employeeId);
        }



        /// <summary>
        /// Check if the department name being added or updated is unique 
        /// </summary>
        /// <param name="department"></param>
        public bool DepartmentNameIsUnique(Department department)
        {
            if (repo.IsDepartmentNameUnique( department.DepartmentName))
            {
                department.Errors.Add(new ValidationError("Department name must be unique\n" +
                    " Please try another name and try again."));
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Check to see if the date is before the creation date
        /// </summary>
        /// <param name="department"></param>
        public bool IsInovationDateBeforeInitialCreationDate(Department department, DateTime dateToCpompare)
        {

            if (repo.IsInvocationDateBeforeCreationDate(department.DepartmentId, department.InvocationDate))
            {
                if (dateToCpompare < department.InvocationDate)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// /checks to see if the invocation date is in the past
        /// </summary>
        /// <param name="department"></param>
        public bool IsInvocationDatePast(Department department)
        {
            DateTime today = DateTime.Today;
            if (department.InvocationDate.Date < today)
            {

                department.Errors.Add(new ValidationError("Inovation date can be set to the current date or in the future only."));
                return true;
            }
            return false;
        }


        /// <summary>
        ///  Validates the department model
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        private bool Validate(Department department)
        {
            

            ValidationContext context = new ValidationContext(department);
            List<ValidationResult> results = new List<ValidationResult>();

            Validator.TryValidateObject(department,
                context, results, true);

            foreach (ValidationResult e in results)
            {
                department.AddError(new ValidationError(e.ErrorMessage));
            }
            return department.Errors.Count == 0;
        }
    }
}
