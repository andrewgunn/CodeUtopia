using System;
using System.Web.Mvc;
using BankReporting.ProjectionStore.Queries;
using CodeUtopia;

namespace BankReporting.Host.Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController(IQueryDispatcher queryExecutor)
        {
            _queryExecutor = queryExecutor;
        }

        public ViewResult Details([Bind(Prefix = "id")] Guid accountId)
        {
            var accountDetailProjection = _queryExecutor.Execute(new AccountDetailQuery(accountId));

            return View(accountDetailProjection);
        }

        private readonly IQueryDispatcher _queryExecutor;
    }
}