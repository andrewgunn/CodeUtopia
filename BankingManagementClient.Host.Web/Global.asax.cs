using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BankingBackend.Commands.v1;
using BankingManagementClient.Autofac;
using BankingManagementClient.ProjectionStore.Queries;
using CodeUtopia;
using CodeUtopia.Messaging;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;
using IBus = NServiceBus.IBus;

namespace BankingManagementClient.Host.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new BankingManagementClientModule());

            var container = builder.Build();

            var busConfiguration = new BusConfiguration();
            busConfiguration = ConfigureBus(busConfiguration, container);
            busConfiguration.EndpointName("BankingManagementClient");
            var startableBus = Bus.Create(busConfiguration);
            var bus = startableBus.Start();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var queryExecutor = container.Resolve<IQueryExecutor>();

            var projection = queryExecutor.Execute(new ClientsQuery());

            if (!projection.ClientProjections.Any())
            {
                bus.Send(new RepublishAllEventsCommand());
            }

        }

        private BusConfiguration ConfigureBus(BusConfiguration busConfiguration, ILifetimeScope lifetimeScope)
        {
            LogManager.Use<DefaultFactory>();

            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.DisableFeature<Sagas>();
            busConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(lifetimeScope));

            busConfiguration.UseTransport<RabbitMQTransport>();

            var conventions = busConfiguration.Conventions();
            conventions.DefiningCommandsAs(x => x.Name.EndsWith("Command"));
            conventions.DefiningEventsAs(x => x.Name.EndsWith("Event"));

            busConfiguration.UsePersistence<InMemoryPersistence>();

            return busConfiguration;
        }
    }
}