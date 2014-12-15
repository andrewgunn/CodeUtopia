namespace CodeUtopia.Specifications
{
    public class DecimalInRange : ISpecification<decimal>
    {
        public DecimalInRange(decimal minimumValue, decimal maximumValue)
        {
            _minimumValue = minimumValue;
            _maximumValue = maximumValue;
        }

        public bool IsSatisfiedBy(decimal candidate)
        {
            return new DecimalNotLessThan(_minimumValue).And(new DecimalNotGreaterThan(_maximumValue))
                                                        .IsSatisfiedBy(candidate);
        }

        private readonly decimal _maximumValue;

        private readonly decimal _minimumValue;
    }
}