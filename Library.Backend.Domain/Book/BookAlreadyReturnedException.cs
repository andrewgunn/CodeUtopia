using System;

namespace Library.Backend.Domain.Book
{
    public class BookAlreadyReturnedException : Exception
    {
        public BookAlreadyReturnedException(Guid bookId)
            : base("The book has already been returned.")
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