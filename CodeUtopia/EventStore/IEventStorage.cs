using System;
using System.Collections.Generic;
using CodeUtopia.Domain;

namespace CodeUtopia.EventStore
{
    /// <summary>
    ///     Stores domain events.
    /// </summary>
    public interface IEventStorage : ISnapshotStorage, IUnitOfWork
    {
        IReadOnlyCollection<IDomainEvent> GetAll(Guid aggregateId);

        IReadOnlyCollection<IDomainEvent> GetAllSinceLastSnapshot(Guid aggregateId);

        void SaveChanges(IAggregate aggregate);
    }
}