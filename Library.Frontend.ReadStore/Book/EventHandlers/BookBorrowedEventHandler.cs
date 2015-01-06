using Library.Events.v1;
using NServiceBus;

namespace Library.Frontend.ReadStore.Book.EventHandlers
{
    public class BookBorrowedEventHandler : IHandleMessages<BookBorrowedEvent>
    {
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

        private readonly IReadStoreDatabaseSettings _readStoreDatabaseSettings;
    }
}