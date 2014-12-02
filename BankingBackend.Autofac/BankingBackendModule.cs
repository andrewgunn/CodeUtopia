using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Autofac;
using Autofac.Core;
using BankingBackend.CommandHandlers;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Domain;
using CodeUtopia.EventStore;
using CodeUtopia.EventStore.EntityFramework;
using CodeUtopia.Messaging;
using CodeUtopia.Messaging.NServiceBus;
using IBus = CodeUtopia.Messaging.IBus;
using Module = Autofac.Module;

namespace BankingBackend.Autofac
{
    public class BankingBackendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            const string eventStoreNameOrConnectionString = "EventStore";

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Bus.
            builder.RegisterType<NServiceBusBus>()
                .WithParameter("endpointName", "BankingBackend")
                   .As<IBus>();

            // Command handler resolver.
            builder.RegisterType<CommandHandlerResolver>()
                   .As<ICommandHandlerResolver>();

            // Command sender.
            builder.RegisterType<NServiceBusCommandSender>()
                   .Named<ICommandSender>("CommandSender");

            builder.RegisterDecorator<ICommandSender>((x, decorated) => new LoggingCommandSenderDecorator(decorated),
                                                      "CommandSender");

            // Command handlers.
            var commandHandlerAssembly = Assembly.GetAssembly(typeof(CreateClientCommandHandler));

            builder.RegisterAssemblyTypes(commandHandlerAssembly)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>)))
                                   .Select(interfaceType => new KeyedService("CommandHandler", interfaceType)));

            builder.RegisterGenericDecorator(typeof(RetryCommandHandlerDecorator<>),
                                             typeof(ICommandHandler<>),
                                             "CommandHandler",
                                             "RetryCommandHandler");

            builder.RegisterGenericDecorator(typeof(LoggingCommandHandlerDecorator<>),
                                             typeof(ICommandHandler<>),
                                             "RetryCommandHandler");

            // Event coordinator.
            builder.RegisterType<EventCoordinator>()
                   .As<IEventCoordinator>();

            // Event handler resolver.
            builder.RegisterType<EventHandlerResolver>()
                   .As<IEventHandlerResolver>();

            // Event publisher.
            builder.RegisterType<NServiceBusEventPublisher>()
                   .Named<IEventPublisher>("EventPublisher");

            builder.RegisterDecorator<IEventPublisher>((x, decorated) => new LoggingEventPublisherDecorator(decorated),
                                                       "EventPublisher");

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
        }
    }
}