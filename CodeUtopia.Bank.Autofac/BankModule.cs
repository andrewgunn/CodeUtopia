using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Autofac;
using CodeUtopia.Bank.CommandHandlers;
using CodeUtopia.Bank.ProjectionStore.EntityFramework;
using CodeUtopia.Bank.ProjectionStore.EntityFramework.AccountDetail.EventHandlers;
using CodeUtopia.Domain;
using CodeUtopia.EventStore;
using CodeUtopia.EventStore.EntityFramework;
using CodeUtopia.Messaging;
using Module = Autofac.Module;

namespace CodeUtopia.Bank.Autofac
{
    public class BankModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            const string eventStoreNameOrConnectionString = "EventStore";

            using (var databaseContext = new EventStoreContext(eventStoreNameOrConnectionString))
            {
                if (databaseContext.Database.Exists())
                {
                    databaseContext.Database.Delete();
                }

                databaseContext.Database.Initialize(true);
            }

            const string projectionStoreNameOrConnectionString = "ProjectionStore";

            using (var databaseContext = new ProjectionStoreContext(projectionStoreNameOrConnectionString))
            {
                if (databaseContext.Database.Exists())
                {
                    databaseContext.Database.Delete();
                }

                databaseContext.Database.Initialize(true);
            }

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Bus.
            builder.RegisterType<InMemoryBus>()
                   .As<IBus>();

            // Command dispatcher.
            builder.RegisterType<CommandDispatcher>()
                   .As<ICommandDispatcher>();

            // Command handlers.
            var commandHandlerAssembly = Assembly.GetAssembly(typeof(CreateAccountCommandHandler));

            builder.RegisterAssemblyTypes(commandHandlerAssembly)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))));

            // Event dispatcher.
            builder.RegisterType<EventDispatcher>()
                   .As<IEventDispatcher>();

            // Event handlers.
            var eventHandlerAssembly = Assembly.GetAssembly(typeof(AccountCreatedEventHandler));

            // TODO Create IProjectionStoreConnectionString
            builder.RegisterAssemblyTypes(eventHandlerAssembly)
                   .WithParameter("nameOrConnectionString", projectionStoreNameOrConnectionString)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IEventHandler<>))));

            // Aggregate repository.
            builder.RegisterType<AggregateRepository>()
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
        }
    }
}