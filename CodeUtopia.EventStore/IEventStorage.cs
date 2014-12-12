using System;
using System.Collections.Generic;
using CodeUtopia.Events;

namespace CodeUtopia.EventStore
{
    /// <summary>
    ///     Stores domain events.
    /// </summary>
    public interface IEventStorage : ISnapshotStorage, IUnitOfWork
    {
        IReadOnlyCollection<IDomainEvent> GetAll(int skip, int take);

        IReadOnlyCollection<IDomainEvent> GetAll(Guid aggregateId);

        IReadOnlyCollection<IDomainEvent> GetAllSinceLastSnapshot(Guid aggregateId);

        void SaveChanges(Guid aggregateId, IReadOnlyCollection<IDomainEvent> domainEvents);
    }
}