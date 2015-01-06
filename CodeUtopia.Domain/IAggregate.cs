using System;
using System.Collections.Generic;
using CodeUtopia.Messages;

namespace CodeUtopia.Domain
{
    public interface IAggregate
    {
        void ClearChanges();

        IReadOnlyCollection<IDomainEvent> GetChanges();

        void LoadFromHistory(IReadOnlyCollection<IDomainEvent> domainEvents);

        Guid AggregateId { get; }

        int AggregateVersionNumber { get; }
    }
}