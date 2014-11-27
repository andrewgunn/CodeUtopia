using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankingManagementClient.ProjectionStore.Queries;
using CodeUtopia;

namespace BankingManagementClient.Host.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueryExecutor _queryExecutor;

        public HomeController(IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        // GET: Home
        public ActionResult Index()
        {
            var clientsProjection = _queryExecutor.Execute(new ClientsQuery());

            return View(clientsProjection.ClientProjections);
        }
    }
}