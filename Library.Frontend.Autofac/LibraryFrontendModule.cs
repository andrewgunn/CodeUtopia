using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using CodeUtopia;
using CodeUtopia.Autofac;
using CodeUtopia.Configuration;
using CodeUtopia.Messaging;
using CodeUtopia.Messaging.NServiceBus;
using Library.Frontend.ProjectionStore;
using Library.Frontend.ProjectionStore.Book.EventHandlers;
using Library.Frontend.ProjectionStore.QueryHandlers;
using Module = Autofac.Module;

namespace Library.Frontend.Autofac
{
    public class LibraryFrontendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Settings provider
            builder.RegisterType<ConfigurationManagerSettingsProvider>()
                   .As<ISettingsProvider>();

            // Bus.
            builder.RegisterType<NServiceBusBus>()
                   .WithParameter("endpointName", "LibraryFrontend")
                   .As<IBus>();

            // Command handler resolver.
            builder.RegisterType<CommandHandlerResolver>()
                   .As<ICommandHandlerResolver>();

            // Command sender.
            builder.RegisterType<NServiceBusCommandSender>()
                   .As<ICommandSender>();

            // Event coordinator.
            builder.RegisterType<EventCoordinator>()
                   .Named<IEventCoordinator>("EventCoordinator");

            builder.Register(
                             x =>
                             new IdempotentEventCoordinatorDecorator(
                                 x.ResolveNamed<IEventCoordinator>("EventCoordinator"),
                                 x.Resolve<IProjectionStoreDatabaseSettings>()))
                   .As<IEventCoordinator>();

            // Event handler resolver.
            builder.RegisterType<EventHandlerResolver>()
                   .As<IEventHandlerResolver>();

            // Event publisher.
            builder.RegisterType<NServiceBusEventPublisher>()
                   .As<IEventPublisher>();

            // Event handlers.
            var eventHandlerAssembly = Assembly.GetAssembly(typeof(BookRegisteredEventHandler));

            // TODO Create IProjectionStoreConnectionString
            builder.RegisterAssemblyTypes(eventHandlerAssembly)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IEventHandler<>)))
                                   .Select(interfaceType => new KeyedService("EventHandler", interfaceType)));

            builder.RegisterGenericDecorator(typeof(RetryEventHandlerDecorator<>),
                                             typeof(IEventHandler<>),
                                             "EventHandler",
                                             "RetryEventHandler");

            builder.RegisterGenericDecorator(typeof(LoggingEventHandlerDecorator<>),
                                             typeof(IEventHandler<>),
                                             "RetryEventHandler");

            // Query executor.
            builder.RegisterType<QueryExecutor>()
                   .As<IQueryExecutor>();

            // Query handlers.
            var queryHandlerAssembly = Assembly.GetAssembly(typeof(BooksQueryHandler));

            // TODO Create IProjectionStoreConnectionString
            builder.RegisterAssemblyTypes(queryHandlerAssembly)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(IQueryHandler<,>))));

            // Projection Store.
            builder.RegisterType<ProjectionStoreDatabaseSettings>()
                   .WithParameter("projectionStoreConnectionStringKey", "ProjectionStore")
                   .As<IProjectionStoreDatabaseSettings>();
        }
    }
}