namespace CodeUtopia.Specifications
{
    public class IntegerNotEqualToValidator : ISpecification<int>
    {
        public IntegerNotEqualToValidator(int notEqualValue)
        {
            _notEqualValue = notEqualValue;
        }

        public bool IsSatisfiedBy(int candidate)
        {
            return !Equals(candidate, _notEqualValue);
        }

        private readonly int _notEqualValue;
    }
}