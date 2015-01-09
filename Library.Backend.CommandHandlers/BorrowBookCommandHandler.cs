using CodeUtopia.Domain;
using Library.Backend.Domain.Book;
using Library.Commands.v1;
using NServiceBus;

namespace Library.Backend.CommandHandlers
{
    public class BorrowBookCommandHandler : IHandleMessages<BorrowBookCommand>,
                                            IHandleMessages<Commands.v2.BorrowBookCommand>
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

        public void Handle(Commands.v2.BorrowBookCommand borrowBookCommand)
        {
            var book = _aggregateRepository.Get<Book>(borrowBookCommand.BookId);
            book.Borrow(borrowBookCommand.ReturnBy);

            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}