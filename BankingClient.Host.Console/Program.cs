using System;
using System.Threading;
using Autofac;
using BankingBackend.Commands.v1;
using BankingClient.Autofac;
using CodeUtopia.Hydrator;
using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Logging;

namespace BankingClient.Host.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var random = RandomSingleton.Instance;

            var builder = new ContainerBuilder();
            builder.RegisterModule(new BankingClientModule());

            var container = builder.Build();
            var busConfiguration = new BusConfiguration();
            busConfiguration = ConfigureBus(busConfiguration, container);
            
            var startableBus = Bus.Create(busConfiguration);
            var bus = startableBus.Start();

            for (int i = 0; i < 1; i++)
            {
                // Client.
                var clientId = Guid.NewGuid();

                bus.Send(new CreateClientCommand(clientId,
                    string.Format("{0} {1}",
                        FirstNameGenerator.RandomFirstName(),
                        LastNameGenerator.RandomLastName())));

                // Account.
                var accountId = Guid.NewGuid();

                bus.Send(new OpenNewAccountCommand(clientId, accountId, "MyAccount"));
                bus.Send(new DepositAmountCommand(accountId, 100));
                bus.Send(new WithdrawAmountCommand(accountId, 50));

                // Bank card.
                var bankCardId = Guid.NewGuid();

                bus.Send(new AssignNewBankCardCommand(clientId, bankCardId, accountId));

                if (random.Next(0, 2) == 0)
                {
                    bus.Send(new ReportStolenBankCardCommand(clientId, bankCardId));
                }
            }
        }

        private static BusConfiguration ConfigureBus(BusConfiguration busConfiguration, ILifetimeScope lifetimeScope)
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