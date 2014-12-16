using System;
using CodeUtopia.Events;

namespace Library.Events
{
    [Serializable]
    public class BookRegisteredEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public string Title { get; set; }
    }
}