using System;

namespace Library.Frontend.Queries.Projections.Books
{
    public class BookProjection
    {
        public BookProjection(Guid bookId, string title, bool isBorrowed, DateTime? returnBy)
        {
            _bookId = bookId;
            _title = title;
            _isBorrowed = isBorrowed;
            _returnBy = returnBy;
        }

        public Guid BookId
        {
            get
            {
                return _bookId;
            }
        }

        public bool IsBorrowed
        {
            get
            {
                return _isBorrowed;
            }
        }

        public DateTime? ReturnBy
        {
            get
            {
                return _returnBy;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        private readonly Guid _bookId;

        private readonly bool _isBorrowed;

        private readonly DateTime? _returnBy;

        private readonly string _title;
    }
}