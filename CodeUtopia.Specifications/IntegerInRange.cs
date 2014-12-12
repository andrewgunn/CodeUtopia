namespace CodeUtopia.Specifications
{
    public class IntegerInRange : ISpecification<int>
    {
        public IntegerInRange(int minimumValue, int maximumValue)
        {
            _minimumValue = minimumValue;
            _maximumValue = maximumValue;
        }

        public bool IsSatisfiedBy(int candidate)
        {
            return new IntegerNotLessThan(_minimumValue).And(new IntegerNotGreaterThan(_maximumValue))
                                                        .IsSatisfiedBy(candidate);
        }

        private readonly int _maximumValue;

        private readonly int _minimumValue;
    }
}