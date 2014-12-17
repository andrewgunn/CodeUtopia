using System;
using CodeUtopia.Events;

namespace Library.Events
{
    [Serializable]
    public class BookBorrowedEvent : IEditableDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public DateTime BorrowedAt { get; set; }
    }
}