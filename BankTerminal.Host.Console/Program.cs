using System;
using Autofac;
using BankServer.Commands.v1;
using BankTerminal.Autofac;
using CodeUtopia.Hydrator;
using CodeUtopia.Messaging;
using ReportStolenBankCardCommand = BankServer.Commands.v2.ReportStolenBankCardCommand;

namespace BankTerminal.Host.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var random = RandomSingleton.Instance;

            var builder = new ContainerBuilder();
            builder.RegisterModule(new BankTerminalModule());

            var container = builder.Build();

            var bus = container.Resolve<IBus>();

            for (int i = 0; i < 100; i++)
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
                    bus.Send(new ReportStolenBankCardCommand(clientId, bankCardId, DateTime.UtcNow));
                }

                bus.Commit();
            }
        }
    }
}