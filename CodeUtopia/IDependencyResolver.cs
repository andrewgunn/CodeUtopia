using System;

namespace CodeUtopia
{
    public interface IDependencyResolver
    {
        TResult Resolve<TResult>();

        object Resolve(Type serviceType);
    }
}