using System;

namespace CodeUtopia.Events
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }

        int AggregateVersionNumber { get; }
    }
}