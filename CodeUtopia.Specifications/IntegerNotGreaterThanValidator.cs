namespace CodeUtopia.Specifications
{
    public class IntegerNotGreaterThanValidator : ISpecification<int>
    {
        public IntegerNotGreaterThanValidator(int maximumValue)
        {
            _maximumValue = maximumValue;
        }

        public bool IsSatisfiedBy(int candidate)
        {
            return candidate < _maximumValue;
        }

        private readonly int _maximumValue;
    }
}