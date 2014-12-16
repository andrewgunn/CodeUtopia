using System;
using CodeUtopia.Events;
using CodeUtopia.Messaging;
using Library.Frontend.ProjectionStore.Aggregate;

namespace Library.Frontend.ProjectionStore
{
    public class IdempotentEventCoordinatorDecorator : IEventCoordinator
    {
        public IdempotentEventCoordinatorDecorator(IEventCoordinator decorated, IProjectionStoreDatabaseSettings projectionStoreDatabaseSettings)
        {
            _decorated = decorated;
            _projectionStoreDatabaseSettings = projectionStoreDatabaseSettings;
        }

        public void Coordinate<TEvent>(TEvent @event, IBus bus) where TEvent : class
        {
            // TODO Fix this code smell.
            var domainEvent = @event as IDomainEvent;

            if (domainEvent == null)
            {
                return;
            }

            using (var databaseContext = new ProjectionStoreContext(_projectionStoreDatabaseSettings))
            {
                var aggregate = databaseContext.Aggregates.Find(domainEvent.AggregateId);

                if (aggregate == null)
                {
                    if (domainEvent.AggregateVersionNumber != 1)
                    {
                        bus.Defer(@event, TimeSpan.FromSeconds(1));

                        return;
                    }

                    aggregate = new AggregateEntity
                                {
                                    AggregateId = domainEvent.AggregateId,
                                    VersionNumber = domainEvent.AggregateVersionNumber
                                };

                    databaseContext.Aggregates.Add(aggregate);
                    databaseContext.SaveChanges();
                }
                else
                {
                    if (aggregate.VersionNumber >= domainEvent.AggregateVersionNumber)
                    {
                        return;
                    }

                    if (aggregate.VersionNumber != domainEvent.AggregateVersionNumber - 1)
                    {
                        bus.Defer(@event, TimeSpan.FromSeconds(1));

                        return;
                    }

                    aggregate.VersionNumber = domainEvent.AggregateVersionNumber;
                    databaseContext.SaveChanges();
                }
            }

            _decorated.Coordinate(@event, bus);
        }

        private readonly IEventCoordinator _decorated;

        private readonly IProjectionStoreDatabaseSettings _projectionStoreDatabaseSettings;
    }
}