using Autofac;
using Library.Backend.Autofac;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Log4Net;
using NServiceBus.Logging;

namespace Library.Backend.Host
{
    public class EndpointConfig : IConfigureThisEndpoint
    {
        private static void ConfigureBus(BusConfiguration busConfiguration, ILifetimeScope lifetimeScope)
        {
            var conventions = busConfiguration.Conventions();
            conventions.DefiningCommandsAs(x => x.Name.EndsWith("Command"));
            conventions.DefiningEventsAs(x => x.Name.EndsWith("Event"));

            busConfiguration.DisableFeature<Sagas>();
            busConfiguration.EnableOutbox();
            busConfiguration.EndpointName("LibraryBackend");
            busConfiguration.UseContainer<AutofacBuilder>(c => c.ExistingLifetimeScope(lifetimeScope));
            busConfiguration.UsePersistence<InMemoryPersistence>();
            busConfiguration.UseSerialization<JsonSerializer>();
            busConfiguration.UseTransport<RabbitMQTransport>();
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

        public void Customize(BusConfiguration configuration)
        {

            var builder = new ContainerBuilder();
            builder.RegisterModule(new LibraryBackendModule());

            var container = builder.Build();

            ConfigureBusLogging();

            ConfigureBus(configuration, container);
        }
    }
}