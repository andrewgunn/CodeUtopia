using Autofac;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Messaging;
using CodeUtopia.Messaging.EasyNetQ;

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
            builder.RegisterType<Bus>()
                   .As<IBus>();

            // Command sender.
            builder.RegisterType<EasyNetQCommandSender>()
                   .Named<ICommandSender>("CommandSender");

            builder.RegisterDecorator<ICommandSender>((x, decorated) => new LoggingCommandSenderDecorator(decorated),
                                                      "CommandSender");

            // Event publisher.
            builder.RegisterType<EasyNetQEventPublisher>()
                   .Named<IEventPublisher>("EventPublisher");

            builder.RegisterDecorator<IEventPublisher>((x, decorated) => new LoggingEventPublisherDecorator(decorated),
                                                       "EventPublisher");
        }
    }
}