namespace CodeUtopia.Specifications
{
    public interface ISpecification<in T>
    {
        bool IsSatisfiedBy(T candidate);
    }
}