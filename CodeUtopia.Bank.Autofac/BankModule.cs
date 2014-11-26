using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Autofac;
using Autofac.Core;
using CodeUtopia.Bank.CommandHandlers;
using CodeUtopia.Bank.ProjectionStore.EntityFramework.Client.EventHandlers;
using CodeUtopia.Bank.ProjectionStore.EntityFramework.QueryHandlers;
using CodeUtopia.Domain;
using CodeUtopia.EventStore;
using CodeUtopia.EventStore.EntityFramework;
using CodeUtopia.Messaging;
using CodeUtopia.Messaging.EasyNetQ;
using Module = Autofac.Module;

namespace CodeUtopia.Bank.Autofac
{
    public class BankModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            const string eventStoreNameOrConnectionString = "EventStore";
            const string projectionStoreNameOrConnectionString = "ProjectionStore";

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Bus.
            builder.RegisterType<InMemoryBus>()
                   .As<IBus>();

            // CommandHandler Resolver.
            builder.RegisterType<CommandHandlerResolver>()
                .As<ICommandHandlerResolver>();

            // Command sender.
            builder.RegisterType<InProcCommandSender>()
                   .Named<ICommandSender>("CommandSender");
            
            // Command sender. **EasyNetQ**
            /*builder.RegisterType<EasyNetQCommandSender>()
                   .Named<ICommandSender>("CommandSender");*/

            builder.RegisterDecorator<ICommandSender>((x, decorated) => new LoggingCommandSenderDecorator(decorated),
                                                      "CommandSender");

            // Command handlers.
            var commandHandlerAssembly = Assembly.GetAssembly(typeof(CreateClientCommandHandler));

            builder.RegisterAssemblyTypes(commandHandlerAssembly)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>)))
                                   .Select(interfaceType => new KeyedService("CommandHandler", interfaceType)));

            builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<>),
                                             typeof(ICommandHandler<>),
                                             "CommandHandler");

            builder.RegisterType<EventHandlerResolver>()
                .As<IEventHandlerResolver>();

            // Event publisher.
            builder.RegisterType<InProcEventPublisher>()
                   .Named<IEventPublisher>("EventPublisher");
            // Event publisher. **EasyNetQ**
            /*builder.RegisterType<EasyNetQEventPublisher>()
                   .Named<IEventPublisher>("EventPublisher");*/


            builder.RegisterDecorator<IEventPublisher>((x, decorated) => new LoggingEventPublisherDecorator(decorated),
                                                       "EventPublisher");

            // Event handlers.
            var eventHandlerAssembly = Assembly.GetAssembly(typeof(ClientCreatedEventHandler));

            // TODO Create IProjectionStoreConnectionString
            builder.RegisterAssemblyTypes(eventHandlerAssembly)
                   .WithParameter("nameOrConnectionString", projectionStoreNameOrConnectionString)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IEventHandler<>)))
                                   .Select(interfaceType => new KeyedService("EventHandler", interfaceType)));

            builder.RegisterGenericDecorator(typeof(LoggingEventHandlerDecorator<>),
                                             typeof(IEventHandler<>),
                                             "EventHandler");

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

            // Query executor.
            builder.RegisterType<QueryExecutor>()
                   .As<IQueryExecutor>();

            // Query handlers.
            var queryHandlerAssembly = Assembly.GetAssembly(typeof(ClientQueryHandler));

            // TODO Create IProjectionStoreConnectionString
            builder.RegisterAssemblyTypes(queryHandlerAssembly)
                   .WithParameter("nameOrConnectionString", projectionStoreNameOrConnectionString)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IQueryHandler<,>))));
        }
    }
}