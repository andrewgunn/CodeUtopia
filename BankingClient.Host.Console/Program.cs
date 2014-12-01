using System;
using System.Threading;
using Autofac;
using BankingClient.Autofac;
using CodeUtopia.Hydrator;
using CodeUtopia.Messaging;

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

            var bus = container.Resolve<IBus>();

            for (int i = 0; i < 100; i++)
            {
                // Client.
                var clientId = Guid.NewGuid();

                bus.Send(new BankingBackend.Commands.v1.CreateClientCommand(clientId,
                    string.Format("{0} {1}",
                        FirstNameGenerator.RandomFirstName(),
                        LastNameGenerator.RandomLastName())));

                // Account.
                var accountId = Guid.NewGuid();

                bus.Send(new BankingBackend.Commands.v1.OpenNewAccountCommand(clientId, accountId, "MyAccount"));
                bus.Send(new BankingBackend.Commands.v1.DepositAmountCommand(accountId, 100));
                bus.Send(new BankingBackend.Commands.v1.WithdrawAmountCommand(accountId, 50));

                // Bank card.
                var bankCardId = Guid.NewGuid();

                bus.Send(new BankingBackend.Commands.v1.AssignNewBankCardCommand(clientId, bankCardId, accountId));

                if (random.Next(0, 2) == 0)
                {
                    bus.Send(new BankingBackend.Commands.v2.ReportStolenBankCardCommand(clientId, bankCardId, DateTime.UtcNow));
                }

                bus.Commit();
            }
        }
    }
}