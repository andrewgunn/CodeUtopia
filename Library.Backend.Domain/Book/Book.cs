using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Domain;
using CodeUtopia.Validators;
using Library.Backend.Domain.Mementoes.v1;
using Library.Events.v1;
using Library.Validators;

namespace Library.Backend.Domain.Book
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
            EnsureIsInitialized();

            if (_borrowedAt != null)
            {
                throw new BookAlreadyBorrowedException(AggregateId);
            }

            var validationErrors = new List<IValidationError>();
            validationErrors.AddRange(new BorrowedAtValidator().Validate(borrowedAt));

            if (validationErrors.Any())
            {
                throw new AggregateValidationErrorException(validationErrors);
            }

            Apply(new BookBorrowedEvent
                  {
                      BorrowedAt = borrowedAt
                  });
        }

        public object CreateMemento()
        {
            return new BookMemento(AggregateId, _title, _borrowedAt);
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
            _borrowedAt = bookMemento.BorrowedAt;
        }

        private void OnBookBorrowedEvent(BookBorrowedEvent bookBorrowedAt)
        {
            _borrowedAt = bookBorrowedAt.BorrowedAt;
        }

        private void OnBookRegisteredEvent(BookRegisteredEvent bookRegisteredEvent)
        {
            _title = bookRegisteredEvent.Title;
        }

        private void OnBookReturnedEvent(BookReturnedEvent bookReturnedEvent)
        {
            _borrowedAt = null;
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
            EnsureIsInitialized();

            if (_borrowedAt == null)
            {
                throw new BookAlreadyReturnedException(AggregateId);
            }

            var validationErrors = new List<IValidationError>();
            validationErrors.AddRange(new ReturnedAtValidator(_borrowedAt.Value).Validate(returnedAt));

            if (validationErrors.Any())
            {
                throw new AggregateValidationErrorException(validationErrors);
            }

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

        private DateTime? _borrowedAt;

        private string _title;
    }
}