namespace CodeUtopia.Specifications
{
    public class StringNotNullOrWhiteSpaceValidator : ISpecification<string>
    {
        public bool IsSatisfiedBy(string candidate)
        {
            return !string.IsNullOrWhiteSpace(candidate);
        }
    }
}