using System;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class DomainEventEntity
    {
        public Guid AggregateId { get; set; }

        public string AggregateType { get; set; }

        public byte[] Data { get; set; }

        public int VersionNumber { get; set; }
    }
}