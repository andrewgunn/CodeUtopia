using System.Linq;
using Library.Events;
using Library.Frontend.ProjectionStore.Aggregate;
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
                var aggregate =
                    databaseContext.Aggregates.SingleOrDefault(x => x.AggregateId == bookReturnedEvent.AggregateId);

                if (aggregate == null)
                {
                    aggregate = new AggregateEntity
                                {
                                    AggregateId = bookReturnedEvent.AggregateId,
                                };
                    databaseContext.Aggregates.Add(aggregate);
                }
                else if (bookReturnedEvent.AggregateVersionNumber <= aggregate.AggregateVersionNumber)
                {
                    // We've seen this message, ignore it.
                    return;
                }

                aggregate.AggregateVersionNumber = bookReturnedEvent.AggregateVersionNumber;

                var book = databaseContext.Books.Find(bookReturnedEvent.AggregateId);
                book.IsBorrowed = false;

                databaseContext.SaveChanges();
            }
        }

        private readonly IProjectionStoreDatabaseSettings _projectionStoreDatabaseSettings;
    }
}