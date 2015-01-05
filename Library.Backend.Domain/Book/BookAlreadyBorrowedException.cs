using System;

namespace Library.Backend.Domain.Book
{
    public class BookAlreadyBorrowedException : Exception
    {
        public BookAlreadyBorrowedException(Guid bookId)
            : base("The book has already been borrowed.")
        {
            _bookId = bookId;
        }

        public Guid BookId
        {
            get
            {
                return _bookId;
            }
        }

        private readonly Guid _bookId;
    }
}