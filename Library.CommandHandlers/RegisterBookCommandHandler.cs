using CodeUtopia.Domain;
using Library.Commands;
using Library.Domain;
using NServiceBus;

namespace Library.CommandHandlers
{
    public class RegisterBookCommandHandler : IHandleMessages<RegisterBookCommand>
    {
        public RegisterBookCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(RegisterBookCommand registerBookCommand)
        {
            var book = Book.Register(registerBookCommand.BookId, registerBookCommand.Title);

            _aggregateRepository.Add(book);
            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}