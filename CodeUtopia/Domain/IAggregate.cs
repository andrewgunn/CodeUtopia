using System;
using System.Collections.Generic;
using CodeUtopia.Event;

namespace CodeUtopia.Domain
{
    // TODO Split into separate interfaces (e.g. IAggregate, IAggregateTracker, IRegisterEntity)
    public interface IAggregate : IVersionNumberProvider
    {
        void ClearChanges();

        IReadOnlyCollection<IDomainEvent> GetChanges();

        void LoadFromHistory(IReadOnlyCollection<IDomainEvent> domainEvents);

        void RegisterEntityForTracking(IEntity entity);

        Guid AggregateId { get; }

        int VersionNumber { get; }
    }
}