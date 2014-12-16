using System.Linq;
using CodeUtopia;
using Library.Frontend.Queries;
using Library.Frontend.Queries.Projections.Books;

namespace Library.Frontend.ProjectionStore.QueryHandlers
{
    public class BooksQueryHandler : IQueryHandler<BooksQuery, BooksProjection>
    {
        public BooksQueryHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public BooksProjection Handle(BooksQuery query)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var books = databaseContext.Books.ToList();

                var bookProjections = books.Select(x => new BookProjection(x.BookId, x.Title))
                                           .ToList();

                return new BooksProjection(bookProjections);
            }
        }

        private readonly string _nameOrConnectionString;
    }
}