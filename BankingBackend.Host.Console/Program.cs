using Autofac;
using BankingBackend.Autofac;
using BankingBackend.Commands.v1;
using CodeUtopia.Messaging;

namespace BankingBackend.Host.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new BankingBackendModule());

            var container = builder.Build();

            var bus = container.Resolve<IBus>();
            bus.Listen<CreateClientCommand>();
            bus.Listen<OpenNewAccountCommand>();
            bus.Listen<AssignNewBankCardCommand>();
            bus.Listen<ReportStolenBankCardCommand>();
            bus.Listen<DepositAmountCommand>();
            bus.Listen<WithdrawAmountCommand>();
            bus.Listen<RepublishAllEventsCommand>();

            System.Console.ReadKey();
        }
    }
}