using Autofac;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Messaging;
using CodeUtopia.Messaging.NServiceBus;
using IBus = CodeUtopia.Messaging.IBus;

namespace BankingClient.Autofac
{
    public class BankingClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Bus.
            builder.RegisterType<NServiceBusBus>()
                    .WithParameter("endpointName", "BankingClient")
                    .As<IBus>();

            // Command handler resolver.
            builder.RegisterType<CommandHandlerResolver>()
                   .As<ICommandHandlerResolver>();

            // Command sender.
            builder.RegisterType<NServiceBusCommandSender>()
                   .Named<ICommandSender>("CommandSender");

            builder.RegisterDecorator<ICommandSender>((x, decorated) => new LoggingCommandSenderDecorator(decorated),
                                                      "CommandSender");

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
        }
    }
}