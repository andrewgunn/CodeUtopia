namespace CodeUtopia.Specifications
{
    public class StringLengthIsLessThan : ISpecification<string>
    {
        public StringLengthIsLessThan(int maximumLength)
        {
            _maximumLength = maximumLength;
        }

        public bool IsSatisfiedBy(string candidate)
        {
            return candidate == null || candidate.Length < _maximumLength;
        }

        private readonly int _maximumLength;
    }
}