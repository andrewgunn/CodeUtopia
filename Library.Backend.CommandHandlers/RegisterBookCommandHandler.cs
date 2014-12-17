using CodeUtopia.Domain;
using Library.Backed.Domain;
using Library.Commands.v1;
using NServiceBus;

namespace Library.Backend.CommandHandlers
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