using Library.Events.v1;
using Library.Frontend.Host.Hubs;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.EventHandlers
{
    public class BookBorrowedEventHandler : IHandleMessages<BookBorrowedEvent>,
                                            IHandleMessages<Events.v2.BookBorrowedEvent>
    {
        public BookBorrowedEventHandler(ILibrarySettings librarySettings)
        {
            _librarySettings = librarySettings;
        }

        public void Handle(BookBorrowedEvent bookBorrowedEvent)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<BookHub, IBookHub>();
            context.Clients.All.BookBorrowed(bookBorrowedEvent.AggregateId);
        }

        public void Handle(Events.v2.BookBorrowedEvent bookBorrowedEvent)
        {
            if (_librarySettings.VersionNumber < 2)
            {
                return;
            }

            var context = GlobalHost.ConnectionManager.GetHubContext<BookHub, IBookHub>();
            context.Clients.All.BookBorrowed(bookBorrowedEvent.AggregateId, bookBorrowedEvent.ReturnBy);
        }

        private readonly ILibrarySettings _librarySettings;
    }
}