using Autofac;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Messaging;
using CodeUtopia.Messaging.EasyNetQ;
using EasyNetQ;
using IBus = CodeUtopia.Messaging.IBus;

namespace BankTerminal.Autofac
{
    public class BankTerminalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Bus.
            builder.RegisterType<EasyNetQBus>()
                    .WithParameter("endpointName", "BankingClient")
                    .As<IBus>();

            builder.RegisterInstance(RabbitHutch.CreateBus("host=localhost"))
                   .As<EasyNetQ.IBus>();

            // Command handler resolver.
            builder.RegisterType<CommandHandlerResolver>()
                   .As<ICommandHandlerResolver>();

            // Command sender.
            builder.RegisterType<EasyNetQCommandSender>()
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
            builder.RegisterType<EasyNetQEventPublisher>()
                   .Named<IEventPublisher>("EventPublisher");

            builder.RegisterDecorator<IEventPublisher>((x, decorated) => new LoggingEventPublisherDecorator(decorated),
                                                       "EventPublisher");
        }
    }
}