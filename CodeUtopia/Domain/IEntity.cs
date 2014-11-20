using System;
using System.Collections.Generic;

namespace CodeUtopia.Domain
{
    // TODO Split into separate interfaces (e.g. IEntity, IEntityTracker)
    public interface IEntity
    {
        void ClearChanges();

        IEnumerable<IEntityEvent> GetChanges();

        void LoadFromHistory(IReadOnlyCollection<IEntityEvent> domainEvents);

        Guid AggregateId { get; }

        Guid EntityId { get; }
    }
}