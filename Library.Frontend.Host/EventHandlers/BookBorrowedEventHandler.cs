using Library.Events;
using Library.Events.v1;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.EventHandlers
{
    public class BookBorrowedEventHandler : IHandleMessages<BookBorrowedEvent>
    {
        public void Handle(BookBorrowedEvent bookBorrowedEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<BookHub>();
            context.Clients.All.BookBorrowed(bookBorrowedEvent.AggregateId);
        }
    }
}