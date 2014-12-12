using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Validators.ValidationErrors;

namespace CodeUtopia.Validators
{
    public abstract class Validator : IValidator
    {
        protected Validator()
        {
            _validationErrors = new List<IValidationError>();
        }

        protected void AddValidationError(IValidationError validationError)
        {
            _validationErrors.Add(validationError);
        }

        protected void AddValidationErrors(IReadOnlyCollection<IValidationError> validationErrors)
        {
            _validationErrors.AddRange(validationErrors);
        }

        public bool IsValid
        {
            get
            {
                return !_validationErrors.Any();
            }
        }

        public IReadOnlyCollection<IValidationError> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        private readonly List<IValidationError> _validationErrors;
    }
}