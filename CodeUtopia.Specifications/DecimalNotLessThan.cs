namespace CodeUtopia.Specifications
{
    public class DecimalNotLessThan : ISpecification<decimal>
    {
        public DecimalNotLessThan(decimal minimumValue)
        {
            _minimumValue = minimumValue;
        }

        public bool IsSatisfiedBy(decimal candidate)
        {
            return candidate >= _minimumValue;
        }

        private readonly decimal _minimumValue;
    }
}