using System;

namespace Library.Frontend.Queries.Projections.Books
{
    public class BookProjection
    {
        public BookProjection(Guid bookId, string title)
        {
            _bookId = bookId;
            _title = title;
        }

        public Guid BookId
        {
            get
            {
                return _bookId;
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

        private readonly string _title;
    }
}