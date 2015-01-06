using System;

namespace CodeUtopia.Messages
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }

        int AggregateVersionNumber { get; }
    }
}