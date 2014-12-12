namespace CodeUtopia.Specifications
{
    public class IntegerEqualTo : ISpecification<int>
    {
        public IntegerEqualTo(int equalValue)
        {
            _equalValue = equalValue;
        }

        public bool IsSatisfiedBy(int candidate)
        {
            return candidate == _equalValue;
        }

        private readonly int _equalValue;
    }
}