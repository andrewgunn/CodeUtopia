using Autofac;
using Library.Backend.Autofac;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;

namespace Library.Backend.Host
{
    public class EndpointConfig : IConfigureThisEndpoint
    {
        private static void ConfigureBus(BusConfiguration busConfiguration, ILifetimeScope lifetimeScope)
        {
            LogManager.Use<DefaultFactory>();

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

        public void Customize(BusConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new LibraryBackendModule());

            var container = builder.Build();

            ConfigureBus(configuration, container);
        }
    }
}