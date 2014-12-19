using System;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class SnapshotEntity
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public byte[] Memento { get; set; }

        public string MementoType { get; set; }
    }
}