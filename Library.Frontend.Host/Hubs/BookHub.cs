using System;
using System.Linq;
using System.Threading.Tasks;
using CodeUtopia;
using Library.Commands.v1;
using Library.Frontend.Host.Models;
using Library.Frontend.Queries;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host.Hubs
{
    public class BookHub : Hub<IBookHub>
    {
        public BookHub()
        {
            _bus = GlobalHost.DependencyResolver.Resolve<IBus>();
            _librarySettings = GlobalHost.DependencyResolver.Resolve<ILibrarySettings>();
            _queryExecutor = GlobalHost.DependencyResolver.Resolve<IQueryExecutor>();
        }

        [Obsolete]
        public void BorrowBook(Guid bookId)
        {
            if (_librarySettings.VersionNumber == 1)
            {
                var command = new BorrowBookCommand
                {
                    BookId = bookId
                };

                _bus.Send(command);
            }
            else
            {
                var command = new Commands.v2.BorrowBookCommand
                {
                    BookId = bookId,
                    ReturnBy = DateTime.UtcNow.AddDays(7)
                };

                _bus.Send(command);
            }
        }

        public override Task OnConnected()
        {
            var booksQuery = new BooksQuery();
            var booksProjection = _queryExecutor.Execute(booksQuery);
            var bookModels = booksProjection.Books.Select(x => new BookModel(x.BookId, x.Title, x.IsBorrowed, x.ReturnBy))
                                            .ToList();

            Clients.Caller.LoadBooks(bookModels);

            return base.OnConnected();
        }

        public void RegisterBook(string title)
        {
            var command = new RegisterBookCommand
                          {
                              BookId = Guid.NewGuid(),
                              Title = title
                          };

            _bus.Send(command);
        }

        public void ReturnBook(Guid bookId)
        {
            var command = new ReturnBookCommand
                          {
                              BookId = bookId
                          };

            _bus.Send(command);
        }

        private readonly IBus _bus;

        private ILibrarySettings _librarySettings;

        private readonly IQueryExecutor _queryExecutor;
    }
}