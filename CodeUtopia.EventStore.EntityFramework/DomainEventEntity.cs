using System;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class DomainEventEntity
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public byte[] Data { get; set; }

        public string DomainEventType { get; set; }
    }
}