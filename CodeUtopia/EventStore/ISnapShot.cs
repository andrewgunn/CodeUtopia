using System;

namespace CodeUtopia.EventStore
{
    public interface ISnapshot
    {
        Guid AggregateId { get; }

        IMemento Memento { get; }

        int VersionNumber { get; }
    }
}