using System.Web.Mvc;
using Library.Commands.v1;
using NServiceBus;

namespace Library.Frontend.Host.Controllers
{
    public class CommandController : Controller
    {
        public CommandController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RepublishAllEvents(string returnUrl)
        {
            var command = new RepublishAllEventsCommand();

            _bus.Send(command);

            return Redirect(returnUrl);
        }

        private readonly IBus _bus;
    }
}