using System;
using CodeUtopia.Domain;
using Library.Backend.Domain.Mementoes.v1;
using Library.Events.v1;
using Library.Validators.Book;

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
            var errorCodes = new TitleValidator().Validate(title);

            if (errorCodes != BookErrorCodes.None)
            {
                throw new BookErrorException(errorCodes);
            }

            RegisterEventHandlers();

            Apply(new BookRegisteredEvent
                  {
                      Title = title
                  });
        }

        [Obsolete]
        public void Borrow()
        {
            EnsureIsInitialized();

            if (_isBorrowed)
            {
                throw new BookAlreadyBorrowedException(AggregateId);
            }

            Apply(new BookBorrowedEvent());
        }

        public void Borrow(DateTime returnBy)
        {
            EnsureIsInitialized();

            if (_isBorrowed)
            {
                throw new BookAlreadyBorrowedException(AggregateId);
            }

            Apply(new Events.v2.BookBorrowedEvent
                  {
                      ReturnBy = returnBy
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

        private void OnBookBorrowedEvent(BookBorrowedEvent bookBorrowedEvent)
        {
            _isBorrowed = true;
        }

        private void OnBookBorrowedEvent(Events.v2.BookBorrowedEvent bookBorrowedEvent)
        {
            _returnBy = bookBorrowedEvent.ReturnBy;
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
            RegisterEventHandler<Events.v2.BookBorrowedEvent>(OnBookBorrowedEvent);
            RegisterEventHandler<BookReturnedEvent>(OnBookReturnedEvent);
        }

        public void Return()
        {
            EnsureIsInitialized();

            if (!_isBorrowed)
            {
                throw new BookAlreadyReturnedException(AggregateId);
            }

            Apply(new BookReturnedEvent());
        }

        protected Guid BookId
        {
            get
            {
                return AggregateId;
            }
        }

        private bool _isBorrowed;

        private DateTime? _returnBy;

        private string _title;
    }
}