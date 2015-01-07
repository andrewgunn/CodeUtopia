using CodeUtopia.Domain;
using Library.Backend.Domain.Book;
using Library.Commands.v1;
using Library.Commands.v1.Replies;
using Library.Validators.Book;
using NServiceBus;
using BookValidationErrorCodes = Library.Commands.v1.Replies.BookValidationErrorCodes;

namespace Library.Backend.CommandHandlers
{
    public class RegisterBookCommandHandler : IHandleMessages<RegisterBookCommand>
    {
        public RegisterBookCommandHandler(IAggregateRepository aggregateRepository, IBus bus)
        {
            _aggregateRepository = aggregateRepository;
            _bus = bus;
        }

        public void Handle(RegisterBookCommand registerBookCommand)
        {
            try
            {
                var book = Book.Register(registerBookCommand.BookId, registerBookCommand.Title);

                _aggregateRepository.Add(book);
                _aggregateRepository.Commit();
            }
            catch (BookValidationFailedException bookValidationException)
            {
                _bus.Reply(new BookValidationFailedReply
                           {
                               ErrorCodes = (BookValidationErrorCodes)(int)bookValidationException.ErrorCodes // TODO Fix this.
                           });
            }
        }

        private readonly IAggregateRepository _aggregateRepository;

        private readonly IBus _bus;
    }
}