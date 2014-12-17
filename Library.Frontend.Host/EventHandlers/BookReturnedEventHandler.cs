using Library.Events;
using Library.Events.v1;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.EventHandlers
{
    public class BookReturnedEventHandler : IHandleMessages<BookReturnedEvent>
    {
        public void Handle(BookReturnedEvent bookReturnedEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<BookHub>();
            context.Clients.All.BookReturned(bookReturnedEvent.AggregateId);
        }
    }
}