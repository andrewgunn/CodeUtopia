namespace CodeUtopia.Specifications
{
    public class IntegerNotGreaterThan : ISpecification<int>
    {
        public IntegerNotGreaterThan(int maximumValue)
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