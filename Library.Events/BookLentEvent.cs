using System;
using CodeUtopia.Events;

namespace Library.Events
{
    [Serializable]
    public class BookBorrowedEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public DateTime BorrowedAt { get; set; }
    }
}