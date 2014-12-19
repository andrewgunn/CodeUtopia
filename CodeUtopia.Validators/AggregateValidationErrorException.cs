using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeUtopia.Validators
{
    public class AggregateValidationErrorException : Exception
    {
        public AggregateValidationErrorException(IEnumerable<IValidationError> validationErrors)
        {
            _validationErrors = validationErrors.ToList();
        }

        public IReadOnlyCollection<IValidationError> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        private readonly IReadOnlyCollection<IValidationError> _validationErrors;
    }
}