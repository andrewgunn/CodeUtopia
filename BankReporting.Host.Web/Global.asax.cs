using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BankReporting.Autofac;
using BankReporting.Queries;
using BankServer.Commands.v1;
using BankServer.Events.v1.Account;
using BankServer.Events.v1.Client;
using CodeUtopia;
using CodeUtopia.Messaging;

namespace BankReporting.Host.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new BankReportingModule());

            var container = builder.Build();

            var bus = container.Resolve<IBus>();

            // Client.
            bus.Subscribe<ClientCreatedEvent>();
            bus.Subscribe<AccountAssignedEvent>();
            bus.Subscribe<NewBankCardAssignedEvent>();
            bus.Subscribe<BankCardReportedStolenEvent>();

            // Account.
            bus.Subscribe<AccountCreatedEvent>();
            bus.Subscribe<AmountDepositedEvent>();
            bus.Subscribe<AmountWithdrawnEvent>();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var queryExecutor = container.Resolve<IQueryDispatcher>();

            var projection = queryExecutor.Execute(new ClientsQuery());

            if (!projection.ClientProjections.Any())
            {
                bus.Send(new RepublishAllEventsCommand());
                bus.Commit();
            }
        }
    }
}