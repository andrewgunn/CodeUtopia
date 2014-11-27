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
using CodeUtopia.Messaging.EasyNetQ;
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
            builder.RegisterType<Bus>()
                   .As<IBus>();

            // Command sender.
            builder.RegisterType<EasyNetQCommandSender>()
                   .Named<ICommandSender>("CommandSender");

            builder.RegisterDecorator<ICommandSender>((x, decorated) => new LoggingCommandSenderDecorator(decorated),
                                                      "CommandSender");

            // CommandHandler Resolver.
            builder.RegisterType<CommandHandlerResolver>()
                   .As<ICommandHandlerResolver>();

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
            builder.RegisterType<EasyNetQEventPublisher>()
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