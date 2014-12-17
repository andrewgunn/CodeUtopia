using Library.Commands.v1;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host
{
    public class LibraryHub : Hub
    {
        public LibraryHub()
        {
            _bus = GlobalHost.DependencyResolver.Resolve<IBus>();
        }

        public void RepublishAllEvents()
        {
            var command = new RepublishAllEventsCommand();

            _bus.Send(command);
        }

        private readonly IBus _bus;
    }
}