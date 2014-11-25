namespace CodeUtopia
{
    public interface IDependencyResolver
    {
        TResult Resolve<TResult>();
    }
}