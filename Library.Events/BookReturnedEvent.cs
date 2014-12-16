using System;
using CodeUtopia.Events;

namespace Library.Events
{
    [Serializable]
    public class BookReturnedEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public DateTime ReturnedAt { get; set; }
    }
}