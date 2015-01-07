using Library.Events.v1;
using Library.Frontend.Host.Hubs;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.EventHandlers
{
    public class BookReturnedEventHandler : IHandleMessages<BookReturnedEvent>
    {
        public void Handle(BookReturnedEvent bookReturnedEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<BookHub, IBookHub>();
            context.Clients.All.BookReturned(bookReturnedEvent.AggregateId);
        }
    }
}