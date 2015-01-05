using System;
using System.Collections.Generic;

namespace CodeUtopia.Validators
{
    public class AggregateValidationErrorException : Exception
    {
        public AggregateValidationErrorException(List<IValidationError> validationErrors)
            : base("One or more validation errors occurred.")
        {
            _validationErrors = validationErrors;
        }

        public List<IValidationError> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        private readonly List<IValidationError> _validationErrors;
    }
}