using System;
using Autofac;
using BankingBackend.Autofac;
using BankingBackend.Commands.v1;
using BankingBackend.Events.v1.Client;
using CodeUtopia.Messaging;
using EasyNetQ;
using IBus = EasyNetQ.IBus;

namespace BankingBackend.Host.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var builder = new ContainerBuilder();
            builder.RegisterModule(new BankingBackendModule());

            var easyNetQBus = RabbitHutch.CreateBus("host=localhost");

            builder.RegisterInstance(easyNetQBus)
                   .As<IBus>();

            var container = builder.Build();

            var commandHandlerResolver = container.Resolve<ICommandHandlerResolver>();

            easyNetQBus.Receive("BankingBackend",
                                registration =>
                                {
                                    registration.Add((CreateClientCommand x) =>
                                                     {
                                                         var commandHandler = commandHandlerResolver.Resolve<CreateClientCommand>();

                                                         commandHandler.Handle(x);
                                                     });

                                    registration.Add((OpenNewAccountCommand x) =>
                                                     {
                                                         var commandHandler = commandHandlerResolver.Resolve<OpenNewAccountCommand>();

                                                         commandHandler.Handle(x);
                                                     });

                                    registration.Add((DepositAmountCommand x) =>
                                                     {
                                                         var commandHandler = commandHandlerResolver.Resolve<DepositAmountCommand>();

                                                         commandHandler.Handle(x);
                                                     });

                                    registration.Add((WithdrawAmountCommand x) =>
                                                     {
                                                         var commandHandler = commandHandlerResolver.Resolve<WithdrawAmountCommand>();

                                                         commandHandler.Handle(x);
                                                     });

                                    registration.Add((AssignNewBankCardCommand x) =>
                                                     {
                                                         var commandHandler =
                                                             commandHandlerResolver.Resolve<AssignNewBankCardCommand>();

                                                         commandHandler.Handle(x);
                                                     });

                                    registration.Add((ReportStolenBankCardCommand x) =>
                                                     {
                                                         var commandHandler =
                                                             commandHandlerResolver.Resolve<ReportStolenBankCardCommand>();

                                                         commandHandler.Handle(x);
                                                     });
                                });

            

            System.Console.WriteLine();
            System.Console.WriteLine("Waiting for messages!");
            System.Console.ReadKey();
        }
    }
}