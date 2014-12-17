using System;
using CodeUtopia.Messaging;
using Library.Commands;
using Microsoft.AspNet.SignalR;

namespace Library.Frontend.Host
{
    public class LibraryHub : Hub
    {
        public LibraryHub()
        {
            _bus = GlobalHost.DependencyResolver.Resolve<IBus>();
        }

        public void BorrowBook(Guid bookId)
        {
            var command = new BorrowBookCommand
                          {
                              BookId = bookId,
                          };

            _bus.Send(command);
            _bus.Commit();
        }

        public void RegisterBook(string title)
        {
            var command = new RegisterBookCommand
                          {
                              BookId = Guid.NewGuid(),
                              Title = title
                          };

            _bus.Send(command);
            _bus.Commit();
        }

        public void ReturnBook(Guid bookId)
        {
            var command = new ReturnBookCommand
                          {
                              BookId = bookId,
                          };

            _bus.Send(command);
            _bus.Commit();
        }

        private readonly IBus _bus;
    }
}