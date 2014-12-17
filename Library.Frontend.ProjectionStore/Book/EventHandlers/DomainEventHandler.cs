using System.Linq;
using CodeUtopia.Events;
using Library.Frontend.ProjectionStore.Aggregate;
using NServiceBus;

namespace Library.Frontend.ProjectionStore.Book.EventHandlers
{
    public class DomainEventHandler : IHandleMessages<IDomainEvent>
    {
        public DomainEventHandler(IProjectionStoreDatabaseSettings projectionStoreDatabaseSettings, IBus bus)
        {
            _projectionStoreDatabaseSettings = projectionStoreDatabaseSettings;
            _bus = bus;
        }

        public void Handle(IDomainEvent domainEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_projectionStoreDatabaseSettings))
            {
                var aggregate = databaseContext.Aggregates.SingleOrDefault(x => x.AggregateId == domainEvent.AggregateId);

                if (aggregate == null)
                {
                    aggregate = new AggregateEntity
                                {
                                    AggregateId = domainEvent.AggregateId
                                };
                    databaseContext.Aggregates.Add(aggregate);
                }
                else if (domainEvent.AggregateVersionNumber <= aggregate.AggregateVersionNumber)
                {
                    // We've seen this message, ignore it.
                    _bus.DoNotContinueDispatchingCurrentMessageToHandlers();

                    return;
                }
                else if (domainEvent.AggregateVersionNumber != aggregate.AggregateVersionNumber + 1)
                {
                    throw new UnexpectedDomainEventException(domainEvent.AggregateId,
                                                             domainEvent.AggregateVersionNumber,
                                                             aggregate.AggregateVersionNumber);
                }

                aggregate.AggregateVersionNumber = domainEvent.AggregateVersionNumber;

                databaseContext.SaveChanges();
            }
        }

        private readonly IBus _bus;

        private readonly IProjectionStoreDatabaseSettings _projectionStoreDatabaseSettings;
    }
}