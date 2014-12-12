namespace CodeUtopia.Specifications
{
    public class IntegerNotEqualTo : ISpecification<int>
    {
        public IntegerNotEqualTo(int notEqualValue)
        {
            _notEqualValue = notEqualValue;
        }

        public bool IsSatisfiedBy(int candidate)
        {
            return candidate == _notEqualValue;
        }

        private readonly int _notEqualValue;
    }
}