using System;
using System.Collections.Generic;
using CodeUtopia.Events;

namespace CodeUtopia.Domain
{
    public interface IEntity
    {
        void ClearChanges();

        IEnumerable<IEntityEvent> GetChanges();

        void LoadFromHistory(IReadOnlyCollection<IEntityEvent> domainEvents);

        Guid AggregateId { get; }

        Guid EntityId { get; }
    }
}