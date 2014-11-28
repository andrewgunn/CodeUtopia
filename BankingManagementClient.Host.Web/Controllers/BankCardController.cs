using System;
using System.Web.Mvc;
using BankingBackend.Commands.v1;
using CodeUtopia;
using CodeUtopia.Messaging;

namespace BankingManagementClient.Host.Web.Controllers
{
    public class BankCardController : Controller
    {
        private readonly ICommandSender _commandSender;

        public BankCardController(ICommandSender commandSender)
        {
            _commandSender = commandSender;
        }

        [HttpPost]
        public RedirectToRouteResult ReportAsStolen([Bind(Prefix = "id")] Guid bankdCardId, Guid clientId)
        {
            var reportStolenBankCardCommand = new ReportStolenBankCardCommand(clientId, bankdCardId);

            _commandSender.Send(reportStolenBankCardCommand);

            return RedirectToAction("Details", "Client", new { id = clientId });
        }

        private readonly IQueryExecutor _queryExecutor;
    }
}