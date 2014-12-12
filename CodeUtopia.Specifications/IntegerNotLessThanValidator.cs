namespace CodeUtopia.Specifications
{
    public class IntegerNotLessThanValidator : ISpecification<int>
    {
        public IntegerNotLessThanValidator(int minimumValue)
        {
            _minimumValue = minimumValue;
        }

        public bool IsSatisfiedBy(int candidate)
        {
            return candidate >= _minimumValue;
        }

        private readonly int _minimumValue;
    }
}