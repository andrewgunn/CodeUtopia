namespace CodeUtopia.Specifications
{
    public class StringNotNullOrWhiteSpace : ISpecification<string>
    {
        public bool IsSatisfiedBy(string candidate)
        {
            return !string.IsNullOrWhiteSpace(candidate);
        }
    }
}