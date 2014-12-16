using System.Web.Mvc;
using CodeUtopia.Messaging;
using Library.Commands;

namespace Library.Frontend.Host.Controllers
{
    public class EventController : Controller
    {
        public EventController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectToRouteResult RepublishAll()
        {
            var command = new RepublishAllEventsCommand();

            _bus.Send(command);
            _bus.Commit();

            return RedirectToAction("List", "Book");
        }

        private readonly IBus _bus;
    }
}