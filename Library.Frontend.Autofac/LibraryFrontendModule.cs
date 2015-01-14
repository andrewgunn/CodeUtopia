using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Autofac;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Configuration;
using CodeUtopia.ReadStore;
using Library.Frontend.ReadStore;
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

            // Date/time provider.
            builder.RegisterType<UtcNowDateTimeProvider>()
                   .As<IDateTimeProvider>();

            // Settings provider.
            builder.RegisterType<ConfigurationManagerSettingsProvider>()
                   .As<ISettingsProvider>();

            // Library settings.
            builder.RegisterType<LibrarySettings>()
                   .As<ILibrarySettings>();

            // Query executor.
            builder.RegisterType<QueryExecutor>()
                   .As<IQueryExecutor>();

            // Read store database settings.
            builder.RegisterType<ReadStoreDatabaseSettings>()
                   .As<IReadStoreDatabaseSettings>();

            // Query handlers.
            var queryHandlerAssembly = Assembly.GetAssembly(typeof(IReadStoreDatabaseSettings));

            builder.RegisterAssemblyTypes(queryHandlerAssembly)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IQueryHandler<,>))));

            // Read store repository.
            builder.RegisterType<ReadStoreRepository>()
                   .As<IReadStoreRepository>();

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ReadStoreContext>());
        }
    }
}