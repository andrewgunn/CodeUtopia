namespace CodeUtopia.Specifications
{
    public class DecimalNotGreaterThan : ISpecification<decimal>
    {
        public DecimalNotGreaterThan(decimal maximumValue)
        {
            _maximumValue = maximumValue;
        }

        public bool IsSatisfiedBy(decimal candidate)
        {
            return candidate < _maximumValue;
        }

        private readonly decimal _maximumValue;
    }
}