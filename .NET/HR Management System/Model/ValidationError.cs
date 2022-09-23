using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ValidationError
    {
      
        private string errorMessage;
        private object model;
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ValidationError()
        {

        }

        public ValidationError(string desc)
        {
            Description = desc;
        }




        public ValidationError(string errorMessage, object model)
        {
            this.errorMessage = errorMessage;
            this.model = model;
        }

        /// <summary>
        /// Paremeterized Constructor
        /// </summary>
        /// <param name="message">The erro message</param>
        /// <param name="type">The error type</param>
        public ValidationError(string message, ErrorType type)
        {
            Message = message;
            Type = type;
        }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="field">The invalid field</param>
        /// <param name="message">The message</param>
        /// <param name="type">The error type</param>
        public ValidationError(string field, string message, ErrorType type)
        {
            Field = field;
            Message = message;
            Type = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The Field the error exists on
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// The error Message to associate with the field
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The type of the error
        /// </summary>
        public ErrorType Type { get; set; }

        public string Description { get; set; }

        #endregion
    }
}
