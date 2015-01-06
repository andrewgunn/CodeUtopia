using CodeUtopia.Domain;
using Library.Backend.Domain.Book;
using Library.Commands.v1;
using NServiceBus;

namespace Library.Backend.CommandHandlers
{
    public class BorrowBookCommandHandler : IHandleMessages<BorrowBookCommand>
    {
        public BorrowBookCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(BorrowBookCommand borrowBookCommand)
        {
            var book = _aggregateRepository.Get<Book>(borrowBookCommand.BookId);
            book.Borrow();

            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}