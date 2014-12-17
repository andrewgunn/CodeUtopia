using Library.Events;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.EventHandlers
{
    public class BookBorrowedEventHandler : IHandleMessages<BookBorrowedEvent>
    {
        public void Handle(BookBorrowedEvent bookBorrowedEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<LibraryHub>();
            context.Clients.All.BookBorrowed(bookBorrowedEvent.AggregateId);
        }
    }
}