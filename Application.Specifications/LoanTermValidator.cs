using CodeUtopia.Specifications;

namespace Application.Specifications
{
    public class LoanTermValidator : ISpecification<int>
    {
        public bool IsSatisfiedBy(int candidate)
        {
            return new IntegerEqualToValidator(12).Or(new IntegerEqualToValidator(18))
                                                  .Or(new IntegerEqualToValidator(24))
                                                  .Or(new IntegerEqualToValidator(36))
                                                  .IsSatisfiedBy(candidate);
        }
    }
}