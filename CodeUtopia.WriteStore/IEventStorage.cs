using System;
using System.Collections.Generic;
using CodeUtopia.Events;

namespace CodeUtopia.WriteStore
{
    /// <summary>
    ///     Stores domain events.
    /// </summary>
    public interface IEventStorage : ISnapshotStorage, IUnitOfWork
    {
        IReadOnlyCollection<IDomainEvent> GetEvents(int skip, int take);

        IReadOnlyCollection<IDomainEvent> GetEventsForAggregate(Guid aggregateId);

        IReadOnlyCollection<IDomainEvent> GetEventsForAggregateSinceLastSnapshot(Guid aggregateId);

        void SaveEvents(IReadOnlyCollection<IDomainEvent> domainEvents);
    }
}