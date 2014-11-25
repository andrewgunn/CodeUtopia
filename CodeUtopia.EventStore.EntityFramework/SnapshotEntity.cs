using System;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class SnapshotEntity
    {
        public Guid AggregateId { get; set; }

        public byte[] Data { get; set; }

        public int VersionNumber { get; set; }
    }
}