using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using CodeUtopia;
using CodeUtopia.ReadStore;
using Library.Commands.v1;
using Library.Frontend.Queries;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Log4Net;
using NServiceBus.Logging;

namespace Library.Frontend.Host
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = Container.Instance;

            AreaRegistration.RegisterAllAreas();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ConfigureBusLogging();

            var busConfiguration = new BusConfiguration();
            busConfiguration = ConfigureBus(busConfiguration, container);

            var bus = Bus.Create(busConfiguration)
                         .Start();

            var queryExecutor = container.Resolve<IQueryExecutor>();
            RepublishAllEventsIfNoneHaveBeenHandled(queryExecutor, bus);
        }

        private static BusConfiguration ConfigureBus(BusConfiguration busConfiguration, ILifetimeScope lifetimeScope)
        {
            var conventions = busConfiguration.Conventions();
            conventions.DefiningCommandsAs(x => x.Name.EndsWith("Command"));
            conventions.DefiningEventsAs(x => x.Name.EndsWith("Event"));

            busConfiguration.DisableFeature<Sagas>();
            busConfiguration.EndpointName("v1_LibraryFrontend");
            busConfiguration.LoadMessageHandlers<First<DomainEventHandler>>();
            busConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(lifetimeScope));
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.UseTransport<RabbitMQTransport>();

            return busConfiguration;
        }

        private static void ConfigureBusLogging()
        {
            var layout = new PatternLayout
                         {
                             ConversionPattern = "%d [%t] %-5p %c [%x] - %m%n"
                         };
            layout.ActivateOptions();
            var consoleAppender = new ColoredConsoleAppender
                                  {
                                      Threshold = Level.Debug,
                                      Layout = layout
                                  };
            consoleAppender.ActivateOptions();
            var fileAppender = new RollingFileAppender
                               {
                                   DatePattern = "yyyy-MM-dd'.log'",
                                   RollingStyle = RollingFileAppender.RollingMode.Composite,
                                   MaxFileSize = 10 * 1024 * 1024,
                                   MaxSizeRollBackups = 10,
                                   LockingModel = new FileAppender.MinimalLock(),
                                   StaticLogFileName = false,
                                   File = @"nsb_log_",
                                   Layout = layout,
                                   AppendToFile = true,
                                   Threshold = Level.Debug,
                               };
            fileAppender.ActivateOptions();

            BasicConfigurator.Configure(fileAppender, consoleAppender);

            LogManager.Use<Log4NetFactory>();
        }

        private void RepublishAllEventsIfNoneHaveBeenHandled(IQueryExecutor queryExecutor, IBus bus)
        {
            var query = new ReadStoreStatusQuery();
            var readStoreStatusProjection = queryExecutor.Execute(query);

            if (!readStoreStatusProjection.IsEmpty)
            {
                return;
            }

            var command = new RepublishAllEventsCommand();

            bus.Send(command);
        }
    }
}