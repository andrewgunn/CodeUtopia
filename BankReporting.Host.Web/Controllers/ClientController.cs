using System;
using System.Web.Mvc;
using BankReporting.ProjectionStore.Queries;
using CodeUtopia;

namespace BankReporting.Host.Web.Controllers
{
    public class ClientController : Controller
    {
        public ClientController(IQueryDispatcher queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public ViewResult Details([Bind(Prefix = "id")] Guid clientId)
        {
            var clientDetailProjection = _queryExecutor.Execute(new ClientDetailQuery(clientId));

            return View(clientDetailProjection);
        }

        public ViewResult List()
        {
            var clientsProjection = _queryExecutor.Execute(new ClientsQuery());

            return View(clientsProjection.ClientProjections);
        }

        private readonly IQueryDispatcher _queryExecutor;
    }
}