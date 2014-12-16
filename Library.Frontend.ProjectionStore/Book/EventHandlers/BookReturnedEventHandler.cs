using Library.Events;
using NServiceBus;

namespace Library.Frontend.ProjectionStore.Book.EventHandlers
{
    public class BookReturnedEventHandler : IHandleMessages<BookReturnedEvent>
    {
        public BookReturnedEventHandler(IProjectionStoreDatabaseSettings projectionStoreDatabaseSettings)
        {
            _projectionStoreDatabaseSettings = projectionStoreDatabaseSettings;
        }

        public void Handle(BookReturnedEvent bookReturnedEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_projectionStoreDatabaseSettings))
            {
                var book = databaseContext.Books.Find(bookReturnedEvent.AggregateId);
                book.IsBorrowed = false;

                databaseContext.SaveChanges();
            }
        }

        private readonly IProjectionStoreDatabaseSettings _projectionStoreDatabaseSettings;
    }
}