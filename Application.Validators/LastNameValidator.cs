using CodeUtopia.Specifications;
using CodeUtopia.Validators;

namespace Application.Validators
{
    public class LastNameValidator : IValidator<string>
    {
        public IValidationError Validate(string candidate)
        {
            var specification = new StringNotNullOrWhiteSpace();

            return specification.IsSatisfiedBy(candidate) ? null : new LastNameValidationError(candidate);
        }
    }
}