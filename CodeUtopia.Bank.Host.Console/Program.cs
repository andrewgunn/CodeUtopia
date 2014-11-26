using System;
using System.Data.Entity.Core.Metadata.Edm;
using Autofac;
using CodeUtopia.Bank.Autofac;
using CodeUtopia.Bank.Commands.v1;
using CodeUtopia.Bank.Events.v1.Account;
using CodeUtopia.Bank.Events.v1.Client;
using CodeUtopia.Bank.ProjectionStore.Queries;
using CodeUtopia.Messaging;
using EasyNetQ;
using IBus = CodeUtopia.Messaging.IBus;

namespace CodeUtopia.Bank.Host.Console
{
    public class MyMessage
    {
        public string Name { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var easyNetQBus = RabbitHutch.CreateBus("host=localhost");

            var builder = new ContainerBuilder();
            builder.RegisterModule(new BankModule());
            builder.RegisterInstance(easyNetQBus)
                .As<global::EasyNetQ.IBus>();

            var container = builder.Build();

            var commandResolver = container.Resolve<ICommandHandlerResolver>();
            var eventResolver = container.Resolve<IEventHandlerResolver>();

            easyNetQBus.Receive("MagicQueue", registration =>
            {
                registration.Add((CreateClientCommand x) =>
                {
                    var handler = commandResolver.Resolve<CreateClientCommand>();

                    handler.Handle(x);
                });

                registration.Add((OpenNewAccountCommand x) =>
                {
                    var handler = commandResolver.Resolve<OpenNewAccountCommand>();

                    handler.Handle(x);
                });

                registration.Add((DepositAmountCommand x) =>
                {
                    var handler = commandResolver.Resolve<DepositAmountCommand>();

                    handler.Handle(x);
                });

                registration.Add((WithdrawAmountCommand x) =>
                {
                    var handler = commandResolver.Resolve<WithdrawAmountCommand>();

                    handler.Handle(x);
                });

                registration.Add((AssignNewBankCardCommand x) =>
                {
                    var handler = commandResolver.Resolve<AssignNewBankCardCommand>();

                    handler.Handle(x);
                });

                registration.Add((ReportStolenBankCardCommand x) =>
                {
                    var handler = commandResolver.Resolve<ReportStolenBankCardCommand>();

                    handler.Handle(x);
                });
            });

            easyNetQBus.Subscribe("MagicQueue-ClientCreatedEventSubscription", (ClientCreatedEvent x) =>
            {
                var handlers = eventResolver.Resolve<ClientCreatedEvent>();

                foreach (var eventHandler in handlers)
                {
                    eventHandler.Handle(x);
                }
            });
            
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