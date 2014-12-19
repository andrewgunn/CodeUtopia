namespace CodeUtopia.Specifications
{
    public class StringLengthIsThan : ISpecification<string>
    {
        public StringLengthIsThan(int length)
        {
            _length = length;
        }

        public bool IsSatisfiedBy(string candidate)
        {
            return candidate == null || candidate.Length == _length;
        }

        private readonly int _length;
    }
}