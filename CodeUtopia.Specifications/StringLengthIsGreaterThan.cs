namespace CodeUtopia.Specifications
{
    public class StringLengthIsGreaterThan : ISpecification<string>
    {
        public StringLengthIsGreaterThan(int minimumLength)
        {
            _minimumLength = minimumLength;
        }

        public bool IsSatisfiedBy(string candidate)
        {
            return candidate == null || candidate.Length > _minimumLength;
        }

        private readonly int _minimumLength;
    }
}