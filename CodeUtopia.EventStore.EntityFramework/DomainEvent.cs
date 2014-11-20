using System;

namespace CodeUtopia.EventStore.EntityFramework
{
    internal class DomainEvent
    {
        public Guid AggregateId { get; set; }

        public byte[] Data { get; set; }

        public Guid Id { get; set; }

        public int VersionNumber { get; set; }
    }
}