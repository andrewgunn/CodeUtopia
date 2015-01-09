using System;
using CodeUtopia.Messages;

namespace Library.Events.v2
{
    [Serializable]
    public class BookBorrowedEvent : IEditableDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public DateTime ReturnBy { get; set; }
    }
}