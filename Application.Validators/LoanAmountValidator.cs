using CodeUtopia.Specifications;
using CodeUtopia.Validators;

namespace Application.Validators
{
    public class LoanAmountValidator : IValidator<decimal>
    {
        public IValidationError Validate(decimal candidate)
        {
            var specification = new DecimalInRange(1000, 3000);

            return specification.IsSatisfiedBy(candidate) ? null : new LoanAmountValidationError(candidate);
        }
    }
}