using Library.Events;
using NServiceBus;

namespace Library.Frontend.ProjectionStore.Book.EventHandlers
{
    public class BookBorrowedEventHandler : IHandleMessages<BookBorrowedEvent>
    {
        public BookBorrowedEventHandler(IProjectionStoreDatabaseSettings projectionStoreDatabaseSettings)
        {
            _projectionStoreDatabaseSettings = projectionStoreDatabaseSettings;
        }

        public void Handle(BookBorrowedEvent bookBorrowedEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_projectionStoreDatabaseSettings))
            {
                var book = databaseContext.Books.Find(bookBorrowedEvent.AggregateId);
                book.IsBorrowed = true;

                databaseContext.SaveChanges();
            }
        }

        private readonly IProjectionStoreDatabaseSettings _projectionStoreDatabaseSettings;
    }
}