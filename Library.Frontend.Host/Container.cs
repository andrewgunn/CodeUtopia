using Autofac;
using Autofac.Integration.Mvc;
using Library.Frontend.Autofac;

namespace Library.Frontend.Host
{
    public static class Container
    {
        static Container()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new LibraryFrontendModule());

            _container = builder.Build();
        }

        public static IContainer Instance
        {
            get
            {
                return _container;
            }
        }

        private static readonly IContainer _container;
    }
}