using CodeUtopia.Specifications;
using CodeUtopia.Validators;

namespace Application.Validators
{
    public class FirstNameValidator : IValidator<string>
    {
        public IValidationError Validate(string candidate)
        {
            var specification = new StringNotNullOrWhiteSpace();

            return specification.IsSatisfiedBy(candidate) ? null : new FirstNameValidationError(candidate);
        }
    }
}