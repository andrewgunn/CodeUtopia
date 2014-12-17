using System.Data.Entity;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Autofac;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Domain;
using CodeUtopia.EventStore;
using CodeUtopia.EventStore.EntityFramework;
using Library.CommandHandlers;
using Module = Autofac.Module;

namespace Library.Backend.Autofac
{
    public class LibraryBackendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            const string eventStoreNameOrConnectionString = "EventStore";

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();
            
            // Aggregate repository.
            builder.RegisterType<EventStoreAggregateRepository>()
                   .As<IAggregateRepository>();

            // Binary formatter.
            builder.RegisterType<BinaryFormatter>()
                   .As<IFormatter>();

            // TODO Create IEventStoreConnectionString
            // Entity Framework Event Store.
            builder.Register(
                             x =>
                             new EntityFrameworkEventStore(eventStoreNameOrConnectionString, x.Resolve<IFormatter>()))
                   .As<IEventStorage>();

            // Event Store database.
            using (var databaseContext = new EventStoreContext(eventStoreNameOrConnectionString))
            {
                Database.SetInitializer(new CreateDatabaseIfNotExists<EventStoreContext>());
                databaseContext.Database.Initialize(true);
            }
        }
    }
}