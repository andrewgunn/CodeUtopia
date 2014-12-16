using System.Linq;
using Library.Events;
using Library.Frontend.ProjectionStore.Aggregate;
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
                var aggregate = databaseContext.Aggregates.SingleOrDefault(x => x.AggregateId == bookRegisteredEvent.AggregateId);

                if (aggregate == null)
                {
                    aggregate = new AggregateEntity
                    {
                        AggregateId = bookRegisteredEvent.AggregateId,
                    };
                    databaseContext.Aggregates.Add(aggregate);
                }
                else if (bookRegisteredEvent.AggregateVersionNumber <= aggregate.AggregateVersionNumber)
                {
                    // We've seen this message, ignore it.
                    return;
                }

                aggregate.AggregateVersionNumber = bookRegisteredEvent.AggregateVersionNumber;

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