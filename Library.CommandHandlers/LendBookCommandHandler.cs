using CodeUtopia.Domain;
using Library.Commands;
using Library.Commands.v1;
using Library.Domain;
using NServiceBus;

namespace Library.CommandHandlers
{
    public class LendBookCommandHandler : IHandleMessages<BorrowBookCommand>
    {
        public LendBookCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(BorrowBookCommand borrowBookCommand)
        {
            var book = _aggregateRepository.Get<Book>(borrowBookCommand.BookId);
            book.Borrow(borrowBookCommand.BorrowedAt);

            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}