using Library.Events;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.EventHandlers
{
    public class BookReturnedEventHandler : IHandleMessages<BookReturnedEvent>
    {
        public void Handle(BookReturnedEvent bookReturnedEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<LibraryHub>();
            context.Clients.All.BookReturned(bookReturnedEvent.AggregateId);
        }
    }
}