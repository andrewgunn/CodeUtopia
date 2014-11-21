using System;

namespace CodeUtopia.Event
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }

        int VersionNumber { get; }
    }
}