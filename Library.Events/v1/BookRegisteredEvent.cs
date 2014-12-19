using System;
using CodeUtopia.Events;

namespace Library.Events.v1
{
    [Serializable]
    public class BookRegisteredEvent : IEditableDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public string Title { get; set; }
    }
}