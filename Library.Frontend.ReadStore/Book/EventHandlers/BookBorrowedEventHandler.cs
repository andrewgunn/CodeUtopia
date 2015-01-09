using System;
using Library.Events.v1;
using NServiceBus;

namespace Library.Frontend.ReadStore.Book.EventHandlers
{
    public class BookBorrowedEventHandler : IHandleMessages<BookBorrowedEvent>,
                                            IHandleMessages<Events.v2.BookBorrowedEvent>
    {
        public BookBorrowedEventHandler(ILibrarySettings librarySettings)
        {
            _librarySettings = librarySettings;
        }

        public BookBorrowedEventHandler(IReadStoreDatabaseSettings readStoreDatabaseSettings)
        {
            _readStoreDatabaseSettings = readStoreDatabaseSettings;
        }

        public void Handle(BookBorrowedEvent bookBorrowedEvent)
        {
            using (var databaseContext = new ReadStoreContext(_readStoreDatabaseSettings))
            {
                var book = databaseContext.Books.Find(bookBorrowedEvent.AggregateId);
                book.IsBorrowed = true;

                databaseContext.SaveChanges();
            }
        }

        public void Handle(Events.v2.BookBorrowedEvent bookBorrowedEvent)
        {
            if (_librarySettings.VersionNumber < 2)
            {
                return;
            }

            using (var databaseContext = new ReadStoreContext(_readStoreDatabaseSettings))
            {
                var book = databaseContext.Books.Find(bookBorrowedEvent.AggregateId);
                book.IsBorrowed = true;
                book.ReturnBy = bookBorrowedEvent.ReturnBy;

                databaseContext.SaveChanges();
            }
        }

        private readonly ILibrarySettings _librarySettings;

        private readonly IReadStoreDatabaseSettings _readStoreDatabaseSettings;
    }
}