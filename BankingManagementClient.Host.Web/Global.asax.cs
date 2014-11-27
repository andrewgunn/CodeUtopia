using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BankingBackend.Events.v1.Account;
using BankingBackend.Events.v1.Client;
using BankingManagementClient.Autofac;

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

            var bus = container.Resolve<CodeUtopia.Messaging.IBus>();
            bus.Subscribe<ClientCreatedEvent>();
            bus.Subscribe<AccountAssignedEvent>();
            bus.Subscribe<NewBankCardAssignedEvent>();
            bus.Subscribe<BankCardReportedStolenEvent>();
            bus.Subscribe<AccountCreatedEvent>();
            bus.Subscribe<AmountDepositedEvent>();
            bus.Subscribe<AmountWithdrawnEvent>();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}