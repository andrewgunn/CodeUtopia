using Library.Events.v1;
using NServiceBus;

namespace Library.Frontend.ProjectionStore.Book.EventHandlers
{
    public class BookRegisteredEventHandler : IHandleMessages<BookRegisteredEvent>
    {
        public BookRegisteredEventHandler(IProjectionStoreDatabaseSettings projectionStoreDatabaseSettings)
        {
            _projectionStoreDatabaseSettings = projectionStoreDatabaseSettings;
        }

        public void Handle(BookRegisteredEvent bookRegisteredEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_projectionStoreDatabaseSettings))
            {
                var book = new BookEntity
                           {
                               BookId = bookRegisteredEvent.AggregateId,
                               Title = bookRegisteredEvent.Title
                           };

                databaseContext.Books.Add(book);

                databaseContext.SaveChanges();
            }
        }

        private readonly IProjectionStoreDatabaseSettings _projectionStoreDatabaseSettings;
    }
}