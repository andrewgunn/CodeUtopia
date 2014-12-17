using Library.Events;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.EventHandlers
{
    public class BookRegisteredEventHandler : IHandleMessages<BookRegisteredEvent>
    {
        public void Handle(BookRegisteredEvent bookRegisteredEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<LibraryHub>();
            context.Clients.All.BookRegistered(bookRegisteredEvent.AggregateId, bookRegisteredEvent.Title);
        }
    }
}