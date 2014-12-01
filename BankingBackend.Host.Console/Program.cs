using Autofac;
using BankingBackend.Autofac;
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
            bus.Listen<BankingBackend.Commands.v1.CreateClientCommand>();
            bus.Listen<BankingBackend.Commands.v1.OpenNewAccountCommand>();
            bus.Listen<BankingBackend.Commands.v1.AssignNewBankCardCommand>();
            bus.Listen<BankingBackend.Commands.v2.ReportStolenBankCardCommand>();
            bus.Listen<BankingBackend.Commands.v1.DepositAmountCommand>();
            bus.Listen<BankingBackend.Commands.v1.WithdrawAmountCommand>();
            bus.Listen<BankingBackend.Commands.v1.RepublishAllEventsCommand>();

            System.Console.ReadKey();
        }
    }
}