using System.Collections.Generic;

namespace Library.Frontend.Queries.Projections.Books
{
    public class BooksProjection
    {
        public BooksProjection(IReadOnlyCollection<BookProjection> books)
        {
            _books = books;
        }

        public IReadOnlyCollection<BookProjection> Books
        {
            get
            {
                return _books;
            }
        }

        private readonly IReadOnlyCollection<BookProjection> _books;
    }
}