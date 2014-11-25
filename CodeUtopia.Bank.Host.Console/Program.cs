using System;
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

            var accountId = new Guid("6D335963-4C72-4250-AD4A-1BA66D8B688C");
            var clientId = new Guid("0DA242D6-40D4-45F5-B12C-3A7DEF357E26");

            bus.Send(new CreateAccountCommand(accountId, clientId, "HSBC"));
            bus.Send(new DepositAmountCommand(accountId, 100));
            bus.Send(new WithdrawAmountCommand(accountId, 50));

            bus.Commit();

            System.Console.WriteLine("OMG, it worked (press any key)...");
            System.Console.ReadKey();
        }
    }
}