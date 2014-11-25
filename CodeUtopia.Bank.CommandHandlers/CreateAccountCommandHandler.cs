using CodeUtopia.Bank.Commands.v1;
using CodeUtopia.Bank.Domain.Account;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.CommandHandlers
{
    public class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand>
    {
        public CreateAccountCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Execute(CreateAccountCommand createAccountCommand)
        {
            var account = Account.Create(createAccountCommand.AccountId,
                                         createAccountCommand.ClientId,
                                         createAccountCommand.AccountName);

            _aggregateRepository.Add(account);
            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}