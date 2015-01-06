using System.Data.Entity;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Autofac;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Domain;
using CodeUtopia.WriteStore;
using CodeUtopia.WriteStore.EntityFramework;

namespace Library.Backend.Autofac
{
    public class LibraryBackendModule : Module
    {
        private void InitializeDatabase(string writeStoreNameOrConnectionString)
        {
            using (var databaseContext = new WriteStoreContext(writeStoreNameOrConnectionString))
            {
                Database.SetInitializer(new CreateDatabaseIfNotExists<WriteStoreContext>());
                databaseContext.Database.Initialize(true);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            const string writeStoreNameOrConnectionString = "WriteStore";

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Aggregate repository.
            builder.RegisterType<WriteStoreAggregateRepository>()
                   .As<IAggregateRepository>();

            // Binary formatter.
            builder.RegisterType<BinaryFormatter>()
                   .As<IFormatter>();

            // TODO Create IWriteStoreDatabaseSettings
            // Entity Framework write store.
            builder.Register(
                             x =>
                             new EntityFrameworkWriteStore(writeStoreNameOrConnectionString, x.Resolve<IFormatter>()))
                   .As<IEventStorage>();

            InitializeDatabase(writeStoreNameOrConnectionString);
        }
    }
}