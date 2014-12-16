using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using CodeUtopia;
using Library.Commands;
using Library.Frontend.Autofac;
using Library.Frontend.ProjectionStore;
using Library.Frontend.Queries;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;

namespace Library.Frontend.Host
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new LibraryFrontendModule());

            var container = builder.Build();

            using (
                var databaseContext = new ProjectionStoreContext(container.Resolve<IProjectionStoreDatabaseSettings>()))
            {
                Database.SetInitializer(new CreateDatabaseIfNotExists<ProjectionStoreContext>());
                databaseContext.Database.Initialize(true);
            }

            AreaRegistration.RegisterAllAreas();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var busConfiguration = new BusConfiguration();
            busConfiguration = ConfigureBus(busConfiguration, container);

            var startableBus = Bus.Create(busConfiguration);
            var bus = startableBus.Start();

            var queryExecutor = container.Resolve<IQueryExecutor>();
            var projection = queryExecutor.Execute(new BooksQuery());

            if (projection.Books.Any())
            {
                return;
            }

            bus.Send(new RepublishAllEventsCommand());
        }

        private static BusConfiguration ConfigureBus(BusConfiguration busConfiguration, ILifetimeScope lifetimeScope)
        {
            LogManager.Use<DefaultFactory>();

            var conventions = busConfiguration.Conventions();
            conventions.DefiningCommandsAs(x => x.Name.EndsWith("Command"));
            conventions.DefiningEventsAs(x => x.Name.EndsWith("Event"));

            busConfiguration.DisableFeature<Sagas>();
            busConfiguration.EndpointName("LibraryFrontend");
            busConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(lifetimeScope));
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.UseTransport<RabbitMQTransport>();

            return busConfiguration;
        }
    }
}