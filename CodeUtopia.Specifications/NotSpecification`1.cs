namespace CodeUtopia.Specifications
{
    public class NotSpecification<T> : ISpecification<T>
    {
        public NotSpecification(ISpecification<T> other)
        {
            _other = other;
        }

        public bool IsSatisfiedBy(T candidate)
        {
            return !_other.IsSatisfiedBy(candidate);
        }

        private readonly ISpecification<T> _other;
    }
}