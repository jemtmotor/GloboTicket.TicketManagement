using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Exceptions
{
    public class ValidatorException : Exception
    {
        public List<string> ValidationErrors { get; set; }

        public ValidatorException(ValidationResult validationResult) 
        {
            ValidationErrors = new List<string>();
            foreach (var validationError in validationResult.Errors) 
            {
                ValidationErrors.Add(validationError.ErrorMessage);
            }
        }
    }
}
