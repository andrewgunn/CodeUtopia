using Library.Events.v1;
using Library.Frontend.Host.Hubs;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.EventHandlers
{
    public class BookRegisteredEventHandler : IHandleMessages<BookRegisteredEvent>
    {
        public void Handle(BookRegisteredEvent bookRegisteredEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<BookHub, IBookHub>();
            context.Clients.All.BookRegistered(bookRegisteredEvent.AggregateId, bookRegisteredEvent.Title);
        }
    }
}