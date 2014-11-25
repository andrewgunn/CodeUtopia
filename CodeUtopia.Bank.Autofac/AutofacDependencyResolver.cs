using System;
using Autofac;

namespace CodeUtopia.Bank.Autofac
{
    // TODO Move into a Module (CodeUtopia.Autofac).
    public class AutofacDependencyResolver : IDependencyResolver
    {
        public AutofacDependencyResolver(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public TResult Resolve<TResult>()
        {
            return _lifetimeScope.Resolve<TResult>();
        }

        public object Resolve(Type serviceType)
        {
            return _lifetimeScope.Resolve(serviceType);
        }

        private readonly ILifetimeScope _lifetimeScope;
    }
}