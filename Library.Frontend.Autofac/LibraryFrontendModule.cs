using System.Data.Entity;
using System.Linq;
using System.Reflection;
using Autofac;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Configuration;
using CodeUtopia.ReadStore;
using Library.Frontend.ReadStore;
using NServiceBus;
using Module = Autofac.Module;

namespace Library.Frontend.Autofac
{
    public class LibraryFrontendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            const string readStoreConnectionStringKey = "ReadStore";

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Date/time provider
            builder.RegisterType<UtcNowDateTimeProvider>()
                   .As<IDateTimeProvider>();

            // Settings provider
            builder.RegisterType<ConfigurationManagerSettingsProvider>()
                   .As<ISettingsProvider>();

            // Query executor.
            builder.RegisterType<QueryExecutor>()
                   .As<IQueryExecutor>();

            // Query handlers.
            var queryHandlerAssembly = Assembly.GetAssembly(typeof(IReadStoreDatabaseSettings));

            builder.RegisterAssemblyTypes(queryHandlerAssembly)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IQueryHandler<,>))));

            // Read store database settings.
            builder.RegisterType<ReadStoreDatabaseSettings>()
                   .WithParameter("readStoreConnectionStringKey", readStoreConnectionStringKey)
                   .As<IReadStoreDatabaseSettings>();

            // Read store repository.
            builder.RegisterType<ReadStoreRepository>()
                   .As<IReadStoreRepository>();

            InitializeDatabase(readStoreConnectionStringKey);
        }

        private void InitializeDatabase(string readStoreNameOrConnectionString)
        {
            using (var databaseContext = new ReadStoreContext(readStoreNameOrConnectionString))
            {
                Database.SetInitializer(new CreateDatabaseIfNotExists<ReadStoreContext>());
                databaseContext.Database.Initialize(true);
            }
        }
    }
}