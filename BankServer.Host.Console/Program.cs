using Autofac;
using BankServer.Autofac;
using BankServer.Commands.v1;
using CodeUtopia.Messaging;

namespace BankServer.Host.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new BankServerModuke());

            var container = builder.Build();

            var bus = container.Resolve<IBus>();

            // Client.
            bus.Listen<CreateClientCommand>();
            bus.Listen<OpenNewAccountCommand>();
            bus.Listen<AssignNewBankCardCommand>();
            bus.Listen<ReportStolenBankCardCommand>();

            // Account.
            bus.Listen<DepositAmountCommand>();
            bus.Listen<WithdrawAmountCommand>();
            bus.Listen<RepublishAllEventsCommand>();

            System.Console.ReadKey();
        }
    }
}