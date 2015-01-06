using Library.Events.v1;
using NServiceBus;

namespace Library.Frontend.ReadStore.Book.EventHandlers
{
    public class BookReturnedEventHandler : IHandleMessages<BookReturnedEvent>
    {
        public BookReturnedEventHandler(IReadStoreDatabaseSettings readStoreDatabaseSettings)
        {
            _readStoreDatabaseSettings = readStoreDatabaseSettings;
        }

        public void Handle(BookReturnedEvent bookReturnedEvent)
        {
            using (var databaseContext = new ReadStoreContext(_readStoreDatabaseSettings))
            {
                var book = databaseContext.Books.Find(bookReturnedEvent.AggregateId);
                book.IsBorrowed = false;

                databaseContext.SaveChanges();
            }
        }

        private readonly IReadStoreDatabaseSettings _readStoreDatabaseSettings;
    }
}