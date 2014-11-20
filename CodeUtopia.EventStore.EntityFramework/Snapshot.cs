using System;

namespace CodeUtopia.EventStore.EntityFramework
{
    internal class Snapshot
    {
        public Guid AggregateId { get; set; }

        public byte[] Data { get; set; }

        public Guid Id { get; set; }

        public int VersionNumber { get; set; }
    }
}