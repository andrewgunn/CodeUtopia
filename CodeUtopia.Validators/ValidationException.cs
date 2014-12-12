using System;
using System.Collections.Generic;
using CodeUtopia.Validators.ValidationErrors;

namespace CodeUtopia.Validators
{
    public class ValidationException : Exception
    {
        public ValidationException(object value, params IValidationError[] validationErrors)
        {
            _value = value;
            _validationErrors = validationErrors;
        }

        public IReadOnlyCollection<IValidationError> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
        }

        private readonly IReadOnlyCollection<IValidationError> _validationErrors;

        private readonly object _value;
    }
}