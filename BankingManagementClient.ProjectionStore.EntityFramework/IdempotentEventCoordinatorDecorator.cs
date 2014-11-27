using CodeUtopia.Events;
using CodeUtopia.Messaging;

namespace BankingManagementClient.ProjectionStore.EntityFramework
{
    public class IdempotentEventCoordinatorDecorator : IEventCoordinator
    {
        public IdempotentEventCoordinatorDecorator(string nameOrConnectionString, IEventCoordinator decorated)
        {
            _nameOrConnectionString = nameOrConnectionString;
            _decorated = decorated;
        }

        public void Coordinate<TEvent>(TEvent @event) where TEvent : class
        {
            // TODO Fix this code smell.
            var domainEvent = @event as IDomainEvent;

            if (domainEvent == null)
            {
                return;
            }

            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var aggregate = databaseContext.Aggregates.Find(domainEvent.AggregateId);

                if (aggregate == null)
                {
                    if (domainEvent.VersionNumber != 1)
                    {
                        // TODO Defer the event.
                        return;
                    }

                    aggregate = new AggregateEntity
                                {
                                    AggregateId = domainEvent.AggregateId,
                                    VersionNumber = domainEvent.VersionNumber
                                };

                    databaseContext.Aggregates.Add(aggregate);
                    databaseContext.SaveChanges();
                }
                else
                {
                    if (aggregate.VersionNumber >= domainEvent.VersionNumber)
                    {
                        return;
                    }

                    if (aggregate.VersionNumber != domainEvent.VersionNumber - 1)
                    {
                        // TODO Defer the event.
                        return;
                    }

                    aggregate.VersionNumber = domainEvent.VersionNumber;
                    databaseContext.SaveChanges();
                }
            }

            _decorated.Coordinate(@event);
        }

        private readonly IEventCoordinator _decorated;

        private readonly string _nameOrConnectionString;
    }
}