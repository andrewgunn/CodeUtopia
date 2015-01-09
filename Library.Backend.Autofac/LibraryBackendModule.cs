using System.Data.Entity;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Autofac;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Configuration;
using CodeUtopia.Domain;
using CodeUtopia.WriteStore;
using CodeUtopia.WriteStore.EntityFramework;

namespace Library.Backend.Autofac
{
    public class LibraryBackendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Binary formatter.
            builder.RegisterType<BinaryFormatter>()
                   .As<IFormatter>();

            // Settings provider
            builder.RegisterType<ConfigurationManagerSettingsProvider>()
                   .As<ISettingsProvider>();

            // Aggregate repository.
            builder.RegisterType<WriteStoreAggregateRepository>()
                   .As<IAggregateRepository>();

            // Read store database settings.
            builder.RegisterType<WriteStoreDatabaseSettings>()
                   .As<IWriteStoreDatabaseSettings>();

            // Entity Framework write store.
            builder.RegisterType<EntityFrameworkWriteStore>()
                   .As<IEventStorage>();

            Database.SetInitializer(new CreateDatabaseIfNotExists<WriteStoreContext>());
        }
    }
}