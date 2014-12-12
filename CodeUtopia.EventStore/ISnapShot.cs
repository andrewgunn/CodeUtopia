using System;

namespace CodeUtopia.EventStore
{
    public interface ISnapshot
    {
        Guid AggregateId { get; }

        int AggregateVersionNumber { get; }

        object Memento { get; }
    }
}