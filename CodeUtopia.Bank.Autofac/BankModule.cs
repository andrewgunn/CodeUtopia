﻿using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Autofac;
using CodeUtopia.Bank.CommandHandlers;
using CodeUtopia.Bank.ProjectionStore.EntityFramework.Client.EventHandlers;
using CodeUtopia.Bank.ProjectionStore.EntityFramework.QueryHandlers;
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
            const string projectionStoreNameOrConnectionString = "ProjectionStore";

            // Dependency resolver.
            builder.RegisterType<AutofacDependencyResolver>()
                   .As<IDependencyResolver>();

            // Bus.
            builder.RegisterType<InMemoryBus>()
                   .As<IBus>();

            // Command dispatcher.
            builder.RegisterType<CommandSender>()
                   .As<ICommandSender>();

            // Command handlers.
            var commandHandlerAssembly = Assembly.GetAssembly(typeof(CreateClientCommandHandler));

            builder.RegisterAssemblyTypes(commandHandlerAssembly)
                   .As(type => type.GetInterfaces()
                                   .Where(interfaceType => interfaceType.IsClosedTypeOf(typeof(ICommandHandler<>))));

            // Event dispatcher.
            builder.RegisterType<EventPublisher>()
                   .As<IEventPublisher>();

            // Event handlers.
            var eventHandlerAssembly = Assembly.GetAssembly(typeof(ClientCreatedEventHandler));

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