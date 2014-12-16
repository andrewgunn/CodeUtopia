
using Autofac;
using Library.Backend.Autofac;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;

namespace Library.Backend.Host
{
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.EnableOutbox();
            configuration.EndpointName("LibraryBackend");
            configuration.UsePersistence<InMemoryPersistence>();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new LibraryBackendModule());

            var container = builder.Build();

            ConfigureBus(configuration, container);

        }

        private void ConfigureBus(BusConfiguration busConfiguration, ILifetimeScope lifetimeScope)
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
        }
    }
}
