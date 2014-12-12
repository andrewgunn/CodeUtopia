using System;
using System.Collections.Generic;

namespace CodeUtopia.Validators
{
    public class AggregateValidationException : Exception
    {
        public AggregateValidationException(params ValidationException[] validationExceptions)
        {
            _validationExceptions = validationExceptions;
        }

        public IReadOnlyCollection<ValidationException> ValidationExceptions
        {
            get
            {
                return _validationExceptions;
            }
        }

        private readonly IReadOnlyCollection<ValidationException> _validationExceptions;
    }
}