using System;
using System.Threading;
using Autofac;
using CodeUtopia.Bank.Autofac;
using CodeUtopia.Bank.Commands.v1;
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

            var clientId = Guid.NewGuid();

            bus.Send(new CreateClientCommand(clientId, "Joe Bloggs"));

            var accountId = Guid.NewGuid();

            bus.Send(new OpenNewAccountCommand(accountId, clientId, "MyBank"));
            Thread.Sleep(1000);
            bus.Send(new DepositAmountCommand(accountId, 100));
            Thread.Sleep(1000);
            bus.Send(new WithdrawAmountCommand(accountId, 50));

            bus.Commit();

            System.Console.WriteLine("OMG, it worked (press any key)...");
            System.Console.ReadKey();
        }
    }
}