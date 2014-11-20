using System;

namespace CodeUtopia.Domain
{
    public interface IDomainEvent
    {
        Guid AggregateId { get; }

        int VersionNumber { get; }
    }
}