using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Parent class for all database entities to facilitate field validation
    /// </summary>
    public class BaseEntity
    {
        #region Properties

        /// <summary>
        /// A list of ValidationErrors
        /// </summary>
        public List<ValidationError> Errors { get; set; } = new List<ValidationError>();

        #endregion

        #region Methods

        /// <summary>
        /// Add an error to the list of errors
        /// </summary>
        /// <param name="error">A validation to add to the error list</param>
        public void AddError(ValidationError error)
        {
            Errors.Add(error);
        }

        /// <summary>
        /// Add an error to the List of Errors
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="type">the error type</param>
        public void AddError(string message, ErrorType type)
        {
            Errors.Add(new ValidationError(message, type));
        }

        /// <summary>
        /// Add an error to the list of errors
        /// </summary>
        /// <param name="field">The field for the specified Error. Pass an empty string if it is a general error.</param>
        /// <param name="message">The error Message</param>
        /// <param name="type">The type of error</param>
        public void AddError(string field, string message, ErrorType type)
        {
            Errors.Add(new ValidationError(field, message, type));
        }

        /// <summary>
        /// Add a list of errors to the existing list.
        /// </summary>
        /// <param name="errors">The collection to append to the errors</param>
        public void AddErrors(List<ValidationError> errors)
        {
            Errors.AddRange(errors);
        }

        /// <summary>
        /// Remove all errors from the lsit.
        /// </summary>
        public void ClearErrors()
        {
            Errors.Clear();
        }

        #endregion
    }
}
