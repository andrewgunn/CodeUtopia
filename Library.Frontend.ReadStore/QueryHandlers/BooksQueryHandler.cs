using System.Linq;
using CodeUtopia;
using Library.Frontend.Queries;
using Library.Frontend.Queries.Projections.Books;

namespace Library.Frontend.ReadStore.QueryHandlers
{
    public class BooksQueryHandler : IQueryHandler<BooksQuery, BooksProjection>
    {
        public BooksQueryHandler(IReadStoreDatabaseSettings readStoreDatabaseSettings)
        {
            _readStoreDatabaseSettings = readStoreDatabaseSettings;
        }

        public BooksProjection Handle(BooksQuery query)
        {
            using (var databaseContext = new ReadStoreContext(_readStoreDatabaseSettings))
            {
                var books = databaseContext.Books.ToList();

                var bookProjections = books.Select(x => new BookProjection(x.BookId, x.Title, x.IsBorrowed))
                                           .ToList();

                return new BooksProjection(bookProjections);
            }
        }

        private readonly IReadStoreDatabaseSettings _readStoreDatabaseSettings;
    }
}