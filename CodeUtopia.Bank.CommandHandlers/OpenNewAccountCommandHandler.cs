using CodeUtopia.Bank.Commands.v1;
using CodeUtopia.Bank.Domain.Account;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.CommandHandlers
{
    public class OpenNewAccountCommandHandler : ICommandHandler<OpenNewAccountCommand>
    {
        public OpenNewAccountCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(OpenNewAccountCommand openNewAccountCommand)
        {
            var account = Account.Create(openNewAccountCommand.AccountId,
                                         openNewAccountCommand.ClientId,
                                         openNewAccountCommand.AccountName);

            _aggregateRepository.Add(account);
            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}