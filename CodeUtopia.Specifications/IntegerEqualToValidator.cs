namespace CodeUtopia.Specifications
{
    public class IntegerEqualToValidator : ISpecification<int>
    {
        public IntegerEqualToValidator(int equalValue)
        {
            _equalValue = equalValue;
        }

        public bool IsSatisfiedBy(int candidate)
        {
            return Equals(candidate, _equalValue);
        }

        private readonly int _equalValue;
    }
}