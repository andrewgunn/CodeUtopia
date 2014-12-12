namespace CodeUtopia.Specifications
{
    public class IntegerRangeValidator : ISpecification<int>
    {
        public IntegerRangeValidator(int minimumValue, int maximumValue)
        {
            _minimumValue = minimumValue;
            _maximumValue = maximumValue;
        }

        public bool IsSatisfiedBy(int candidate)
        {
            return new IntegerNotLessThanValidator(_minimumValue).And(new IntegerNotGreaterThanValidator(_maximumValue))
                                                                 .IsSatisfiedBy(candidate);
        }

        private readonly int _maximumValue;

        private readonly int _minimumValue;
    }
}