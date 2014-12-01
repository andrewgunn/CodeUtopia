using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using BankReporting.ProjectionStore.EntityFramework;
using BankReporting.ProjectionStore.EntityFramework.Client.EventHandlers;
using BankReporting.ProjectionStore.EntityFramework.QueryHandlers;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Messaging;
using CodeUtopia.Messaging.EasyNetQ;
using EasyNetQ;
using IBus = CodeUtopia.Messaging.IBus;
using Module = Autofac.Module;

namespace BankReporting.Autofac
{
    public class BankingManagementClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            const string projectionStoreNameOrConnectionString = "ProjectionStore";

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                .As<IDependencyResolver>();

            // Bus.
            builder.RegisterType<EasyNetQBus>()
                .WithParameter("endpointName", "BankingManagementClient")
                .As<IBus>();

            builder.RegisterInstance(RabbitHutch.CreateBus("host=localhost"))
                .As<EasyNetQ.IBus>();

            // Command handler resolver.
            builder.RegisterType<CommandHandlerResolver>()
                .As<ICommandHandlerResolver>();

            // Command sender.
            builder.RegisterType<EasyNetQCommandSender>()
                .As<ICommandSender>();

            // Event coordinator.
            builder.RegisterType<EventCoordinator>()
                .Named<IEventCoordinator>("EventCoordinator");

            builder.Register(
                x =>
                    new IdempotentEventCoordinatorDecorator(x.ResolveNamed<IEventCoordinator>("EventCoordinator"), projectionStoreNameOrConnectionString))
                .As<IEventCoordinator>();

            // Event handler resolver.
            builder.RegisterType<EventHandlerResolver>()
                .As<IEventHandlerResolver>();

            // Event publisher.
            builder.RegisterType<EasyNetQEventPublisher>()
                .As<IEventPublisher>();

            // Event handlers.
            var eventHandlerAssembly = Assembly.GetAssembly(typeof (ClientCreatedEventHandler));

            // TODO Create IProjectionStoreConnectionString
            builder.RegisterAssemblyTypes(eventHandlerAssembly)
                .WithParameter("nameOrConnectionString", projectionStoreNameOrConnectionString)
                .As(type => type.GetInterfaces()
                    .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof (IEventHandler<>)))
                    .Select(interfaceType => new KeyedService("EventHandler", interfaceType)));

            builder.RegisterGenericDecorator(typeof (RetryEventHandlerDecorator<>),
                typeof (IEventHandler<>),
                "EventHandler",
                "RetryEventHandler");

            builder.RegisterGenericDecorator(typeof (LoggingEventHandlerDecorator<>),
                typeof (IEventHandler<>),
                "RetryEventHandler");

            // Query dispatcher.
            builder.RegisterType<QueryDispatcher>()
                .As<IQueryDispatcher>();

            // Query handlers.
            var queryHandlerAssembly = Assembly.GetAssembly(typeof (ClientQueryHandler));

            // TODO Create IProjectionStoreConnectionString
            builder.RegisterAssemblyTypes(queryHandlerAssembly)
                .WithParameter("nameOrConnectionString", projectionStoreNameOrConnectionString)
                .As(type => type.GetInterfaces()
                    .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof (IQueryHandler<,>))));
        }
    }
}