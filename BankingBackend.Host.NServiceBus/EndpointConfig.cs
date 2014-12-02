
using Autofac;
using BankingBackend.Autofac;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;

namespace BankingBackend.Host.NServiceBus
{
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new BankingBackendModule());

            var container = builder.Build();

            ConfigureBus(configuration, container);
            
            configuration.EndpointName("BankingBackend");
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
