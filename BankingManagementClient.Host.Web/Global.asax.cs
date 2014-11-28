using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BankingManagementClient.Autofac;
using CodeUtopia.Messaging;

namespace BankingManagementClient.Host.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new BankingManagementClientModule());

            var container = builder.Build();

            var bus = container.Resolve<IBus>();

            bus.Subscribe<BankingBackend.Events.v1.Client.ClientCreatedEvent>();
            bus.Subscribe<BankingBackend.Events.v1.Client.AccountAssignedEvent>();
            bus.Subscribe<BankingBackend.Events.v1.Client.NewBankCardAssignedEvent>();
            bus.Subscribe<BankingBackend.Events.v1.Client.BankCardReportedStolenEvent>();

            bus.Subscribe<BankingBackend.Events.v1.Account.AccountCreatedEvent>();
            bus.Subscribe<BankingBackend.Events.v1.Account.AmountDepositedEvent>();
            bus.Subscribe<BankingBackend.Events.v1.Account.AmountWithdrawnEvent>();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}