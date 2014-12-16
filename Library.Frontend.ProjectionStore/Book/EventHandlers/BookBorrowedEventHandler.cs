using System.Linq;
using Library.Events;
using Library.Frontend.ProjectionStore.Aggregate;
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
                var aggregate = databaseContext.Aggregates.SingleOrDefault(x => x.AggregateId == bookBorrowedEvent.AggregateId);

                if (aggregate == null)
                {
                    aggregate = new AggregateEntity
                                {
                                    AggregateId = bookBorrowedEvent.AggregateId,
                                };
                    databaseContext.Aggregates.Add(aggregate);
                }
                else if (bookBorrowedEvent.AggregateVersionNumber <= aggregate.AggregateVersionNumber)
                {
                    // We've seen this message, ignore it.
                    return;
                }

                aggregate.AggregateVersionNumber = bookBorrowedEvent.AggregateVersionNumber;

                var book = databaseContext.Books.Find(bookBorrowedEvent.AggregateId);
                book.IsBorrowed = true;

                databaseContext.SaveChanges();
            }
        }

        private readonly IProjectionStoreDatabaseSettings _projectionStoreDatabaseSettings;
    }
}