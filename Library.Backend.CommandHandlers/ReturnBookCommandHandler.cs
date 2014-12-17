using CodeUtopia.Domain;
using Library.Backed.Domain;
using Library.Commands.v1;
using NServiceBus;

namespace Library.Backend.CommandHandlers
{
    public class ReturnBookCommandHandler : IHandleMessages<ReturnBookCommand>
    {
        public ReturnBookCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(ReturnBookCommand returnBookCommand)
        {
            var book = _aggregateRepository.Get<Book>(returnBookCommand.BookId);
            book.Return(returnBookCommand.ReturnedAt);

            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}