using System;
using System.Threading;
using Autofac;
using BankingBackend.Commands.v1;
using BankingClient.Autofac;
using CodeUtopia.Hydrator;
using CodeUtopia.Messaging;

namespace BankingClient.Host.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new BankingClientModule());

            var container = builder.Build();

            var bus = container.Resolve<IBus>();

            do
            {
                while (!System.Console.KeyAvailable)
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
                    bus.Send(new ReportStolenBankCardCommand(clientId, bankCardId));

                    bus.Commit();

                    Thread.Sleep(1000);
                }
            }
            while (System.Console.ReadKey(true)
                         .Key != ConsoleKey.Escape);
        }
    }
}