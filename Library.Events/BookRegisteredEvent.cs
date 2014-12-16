using System;
using CodeUtopia.Events;

namespace Library.Events
{
    public class BookRegisteredEvent : DomainEvent
    {
        public BookRegisteredEvent(Guid bookId, int bookVersionNumber, string title)
            : base(bookId, bookVersionNumber)
        {
            _title = title;
        }

        public Guid BookId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        private readonly string _title;
    }
}