using System;
using CodeUtopia.Messages;

namespace Library.Events.v1
{
    [Serializable]
    public class BookBorrowedEvent : IEditableDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }
    }
}