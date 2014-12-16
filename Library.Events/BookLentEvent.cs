using System;
using CodeUtopia.Events;

namespace Library.Events
{
    public class BookLentEvent : DomainEvent
    {
        public BookLentEvent(Guid bookId, int bookVersionNumber, DateTime lentAt)
            : base(bookId, bookVersionNumber)
        {
            _lentAt = lentAt;
        }

        public Guid BookId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        public DateTime LentAt
        {
            get
            {
                return _lentAt;
            }
        }

        private readonly DateTime _lentAt;
    }
}