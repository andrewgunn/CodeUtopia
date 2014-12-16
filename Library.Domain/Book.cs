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

        public void Borrow(DateTime borrowedAt)
        {
            Apply(new BookBorrowedEvent
                  {
                      AggregateId = AggregateId,
                      AggregateVersionNumber = GetNextVersionNumber(),
                      BorrowedAt = borrowedAt
                  });
        }

        private void OnBookBorrowedEvent(BookBorrowedEvent bookBorrowedAt)
        {
            _isBorrowed = true;
        }

        private void OnBookRegisteredEvent(BookRegisteredEvent bookRegisteredEvent)
        {
            AggregateId = bookRegisteredEvent.AggregateId;
            _title = bookRegisteredEvent.Title;
        }

        private void OnBookReturnedEvent(BookReturnedEvent bookReturnedEvent)
        {
            _isBorrowed = false;
        }

        public static Book Register(Guid bookId, string title)
        {
            return new Book(bookId, title);
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<BookRegisteredEvent>(OnBookRegisteredEvent);
            RegisterEventHandler<BookBorrowedEvent>(OnBookBorrowedEvent);
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

        private bool _isBorrowed;

        private string _title;
    }
}