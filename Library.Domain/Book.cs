using System;
using CodeUtopia.Domain;
using Library.Events;

namespace Library.Domain
{
    public class Book : Aggregate
    {
        public Book()
        {
            RegisterEventHandlers();
        }

        private Book(Guid bookId, string title)
            : this()
        {
            Apply(new BookRegisteredEvent
                  {
                      AggregateId = bookId,
                      AggregateVersionNumber = GetNextVersionNumber(),
                      Title = title
                  });
        }

        public void Lend(DateTime lentAt)
        {
            Apply(new BookLentEvent
                  {
                      AggregateId = AggregateId,
                      AggregateVersionNumber = GetNextVersionNumber(),
                      LentAt = lentAt
                  });
        }

        private void OnBookLentEvent(BookLentEvent bookLentAt)
        {
            _lentAt = bookLentAt.LentAt;
        }

        private void OnBookRegisteredEvent(BookRegisteredEvent bookRegisteredEvent)
        {
            AggregateId = bookRegisteredEvent.AggregateId;
            _title = bookRegisteredEvent.Title;
        }

        private void OnBookReturnedEvent(BookReturnedEvent bookReturnedEvent)
        {
            _lentAt = null;
        }

        public static Book Register(Guid bookId, string title)
        {
            return new Book(bookId, title);
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<BookRegisteredEvent>(OnBookRegisteredEvent);
            RegisterEventHandler<BookLentEvent>(OnBookLentEvent);
            RegisterEventHandler<BookReturnedEvent>(OnBookReturnedEvent);
        }

        public void Return(DateTime returnedAt)
        {
            Apply(new BookReturnedEvent
                  {
                      AggregateId = AggregateId,
                      AggregateVersionNumber = GetNextVersionNumber(),
                      ReturnedAt = returnedAt
                  });
        }

        protected Guid BookId
        {
            get
            {
                return AggregateId;
            }
        }

        private DateTime? _lentAt;

        private string _title;
    }
}