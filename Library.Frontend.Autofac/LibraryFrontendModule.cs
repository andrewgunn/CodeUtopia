using System.Linq;
using System.Reflection;
using Autofac;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Configuration;
using Library.Frontend.ProjectionStore;
using Library.Frontend.ProjectionStore.QueryHandlers;
using Module = Autofac.Module;

namespace Library.Frontend.Autofac
{
    public class LibraryFrontendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Settings provider
            builder.RegisterType<ConfigurationManagerSettingsProvider>()
                   .As<ISettingsProvider>();

            // Query executor.
            builder.RegisterType<QueryExecutor>()
                   .As<IQueryExecutor>();

            // Query handlers.
            var queryHandlerAssembly = Assembly.GetAssembly(typeof(BooksQueryHandler));

            // TODO Create IProjectionStoreConnectionString
            builder.RegisterAssemblyTypes(queryHandlerAssembly)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IQueryHandler<,>))));

            // Projection Store.
            builder.RegisterType<ProjectionStoreDatabaseSettings>()
                   .WithParameter("projectionStoreConnectionStringKey", "ProjectionStore")
                   .As<IProjectionStoreDatabaseSettings>();
        }
    }
}