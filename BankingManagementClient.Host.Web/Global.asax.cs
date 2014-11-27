using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BankingBackend.Events.v1.Client;
using BankingManagementClient.Autofac;
using CodeUtopia.Messaging;
using EasyNetQ;
using IBus = EasyNetQ.IBus;

namespace BankingManagementClient.Host.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new BankingManagementClientModule());

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            var easyNetQBus = RabbitHutch.CreateBus("host=localhost");

            builder.RegisterInstance(easyNetQBus)
                   .As<IBus>();

            var container = builder.Build();

            var eventHandlerResolver = container.Resolve<IEventHandlerResolver>();

            easyNetQBus.Subscribe("BankingManagementClient-ClientCreatedEventSubscription",
                                  (ClientCreatedEvent x) =>
                                  {
                                      var eventHandlers = eventHandlerResolver.Resolve<ClientCreatedEvent>();

                                      foreach (var eventHandler in eventHandlers)
                                      {
                                          eventHandler.Handle(x);
                                      }
                                  });

            easyNetQBus.Subscribe("BankingManagementClient-AccountAssignedEventSubscription",
                                  (AccountAssignedEvent x) =>
                                  {
                                      var eventHandlers = eventHandlerResolver.Resolve<AccountAssignedEvent>();

                                      foreach (var eventHandler in eventHandlers)
                                      {
                                          eventHandler.Handle(x);
                                      }
                                  });

            easyNetQBus.Subscribe("BankingManagementClient-NewBankCardAssignedEventSubscription",
                                  (NewBankCardAssignedEvent x) =>
                                  {
                                      var eventHandlers = eventHandlerResolver.Resolve<NewBankCardAssignedEvent>();

                                      foreach (var eventHandler in eventHandlers)
                                      {
                                          eventHandler.Handle(x);
                                      }
                                  });

            easyNetQBus.Subscribe("BankingManagementClient-BankCardReportedStolenEventSubscription",
                                  (BankCardReportedStolenEvent x) =>
                                  {
                                      var eventHandlers = eventHandlerResolver.Resolve<BankCardReportedStolenEvent>();

                                      foreach (var eventHandler in eventHandlers)
                                      {
                                          eventHandler.Handle(x);
                                      }
                                  });

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}