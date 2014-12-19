using System;
using System.Linq;
using System.Threading.Tasks;
using CodeUtopia;
using Library.Commands.v1;
using Library.Frontend.Host.Models;
using Library.Frontend.Queries;
using Microsoft.AspNet.SignalR;
using NServiceBus;

namespace Library.Frontend.Host
{
    public class BookHub : Hub
    {
        public BookHub()
        {
            _bus = GlobalHost.DependencyResolver.Resolve<IBus>();
            _queryExecutor = GlobalHost.DependencyResolver.Resolve<IQueryExecutor>();
        }

        public void BorrowBook(Guid bookId)
        {
            var command = new BorrowBookCommand
                          {
                              BookId = bookId,
                          };

            _bus.Send(command);
        }

        public override Task OnConnected()
        {
            var booksProjection = _queryExecutor.Execute(new BooksQuery());
            var bookModels = booksProjection.Books.Select(x => new BookModel(x.BookId, x.Title, x.IsBorrowed))
                                            .ToList();

            Clients.Caller.loadBooks(bookModels);

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
                              BookId = bookId,
                          };

            _bus.Send(command);
        }

        private readonly IBus _bus;

        private readonly IQueryExecutor _queryExecutor;
    }
}