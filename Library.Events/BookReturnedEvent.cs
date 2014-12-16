using System;
using CodeUtopia.Events;

namespace Library.Events
{
    public class BookReturnedEvent : DomainEvent
    {
        public BookReturnedEvent(Guid bookId, int bookVersionNumber, DateTime returnedAt)
            : base(bookId, bookVersionNumber)
        {
            _returnedAt = returnedAt;
        }

        public Guid BookId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        public DateTime ReturnedAt
        {
            get
            {
                return _returnedAt;
            }
        }

        private readonly DateTime _returnedAt;
    }
}