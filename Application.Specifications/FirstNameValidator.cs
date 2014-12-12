using CodeUtopia.Specifications;

namespace Application.Specifications
{
    public class FirstNameValidator : ISpecification<string>
    {
        public bool IsSatisfiedBy(string candidate)
        {
            return new StringNotNullOrWhiteSpaceValidator().IsSatisfiedBy(candidate);
        }
    }
}