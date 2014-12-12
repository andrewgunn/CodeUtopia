using System;
using CodeUtopia.Domain;

namespace CodeUtopia.EventStore
{
    public interface ISnapshot
    {
        Guid AggregateId { get; }

        int AggregateVersionNumber { get; }

        object Memento { get; }
    }
}