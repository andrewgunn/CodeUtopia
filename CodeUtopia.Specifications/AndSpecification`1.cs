namespace CodeUtopia.Specifications
{
    public class AndSpecification<T> : ISpecification<T>
    {
        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public bool IsSatisfiedBy(T candidate)
        {
            return _left.IsSatisfiedBy(candidate) && _right.IsSatisfiedBy(candidate);
        }

        private readonly ISpecification<T> _left;

        private readonly ISpecification<T> _right;
    }
}