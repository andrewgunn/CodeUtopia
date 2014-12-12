using CodeUtopia.Specifications;
using CodeUtopia.Validators;

namespace Application.Validators
{
    public class LoanTermValidator : IValidator<int>
    {
        public IValidationError Validate(int candidate)
        {
            var specification = new IntegerEqualTo(12).Or(new IntegerEqualTo(18))
                                                      .Or(new IntegerEqualTo(24))
                                                      .Or(new IntegerEqualTo(36));

            return specification.IsSatisfiedBy(candidate) ? null : new LoanTermValidationError(candidate);
        }
    }
}