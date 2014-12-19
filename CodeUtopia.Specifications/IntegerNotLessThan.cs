namespace CodeUtopia.Specifications
{
    public class IntegerNotLessThan : ISpecification<int>
    {
        public IntegerNotLessThan(int minimumValue)
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