using System;
using Autofac;
using CodeUtopia.Bank.Autofac;
using CodeUtopia.Bank.Commands.v1;
using CodeUtopia.Bank.ProjectionStore.Queries;
using CodeUtopia.Messaging;

namespace CodeUtopia.Bank.Host.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new BankModule());

            var container = builder.Build();

            var bus = container.Resolve<IBus>();

            // Client.
            var clientId = Guid.NewGuid();

            bus.Send(new CreateClientCommand(clientId, "Joe Bloggs"));

            // Account.
            var accountId = Guid.NewGuid();

            bus.Send(new OpenNewAccountCommand(clientId, accountId, "MyBank"));
            bus.Send(new DepositAmountCommand(accountId, 100));
            bus.Send(new WithdrawAmountCommand(accountId, 50));

            // Bank card.
            var bankCardId = Guid.NewGuid();

            bus.Send(new AssignNewBankCardCommand(clientId, bankCardId, accountId));
            bus.Send(new ReportStolenBankCardCommand(clientId, bankCardId));

            bus.Commit();

            System.Console.WriteLine();
            System.Console.WriteLine("Fin!");
            System.Console.ReadKey();
        }
    }
}