using System;
using CodeUtopia.Events;

namespace Library.Events
{
    [Serializable]
    public class BookLentEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public DateTime LentAt { get; set; }
    }
}