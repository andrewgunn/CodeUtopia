using System;
using CodeUtopia.Domain;
using Library.Backend.Domain.Mementoes.v1;
using Library.Events.v1;

namespace Library.Backend.Domain
{
    public class Book : Aggregate, IOriginator
    {
        public Book()
        {
            RegisterEventHandlers();
        }

        private Book(Guid bookId, string title)
            : base(bookId)
        {
            RegisterEventHandlers();

            Apply(new BookRegisteredEvent
                  {
                      Title = title
                  });
        }

        public void Borrow(DateTime borrowedAt)
        {
            Apply(new BookBorrowedEvent
                  {
                      BorrowedAt = borrowedAt
                  });
        }

        public object CreateMemento()
        {
            return new BookMemento(AggregateId, _title, _isBorrowed);
        }

        public void LoadFromMemento(Guid aggregateId, int aggregateVersionNumber, object memento)
        {
            var bookMemento = memento as BookMemento;

            if (bookMemento == null)
            {
                return;
            }

            LoadFromMemento(aggregateId, aggregateVersionNumber);
            _title = bookMemento.Title;
            _isBorrowed = bookMemento.IsBorrowed;
        }

        private void OnBookBorrowedEvent(BookBorrowedEvent bookBorrowedAt)
        {
            _isBorrowed = true;
        }

        private void OnBookRegisteredEvent(BookRegisteredEvent bookRegisteredEvent)
        {
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