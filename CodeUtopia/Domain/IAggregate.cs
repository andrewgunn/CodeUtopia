using System;
using System.Collections.Generic;

namespace CodeUtopia.Domain
{
    // TODO Split into separate interfaces (e.g. IAggregate, IAggregateTracker, IRegisterEntity)
    public interface IAggregate
    {
        void ClearChanges();

        IReadOnlyCollection<IDomainEvent> GetChanges();

        void LoadFromHistory(IReadOnlyCollection<IDomainEvent> domainEvents);

        void RegisterEntity(IEntity entity);

        Guid AggregateId { get; }

        int VersionNumber { get; }
    }
}