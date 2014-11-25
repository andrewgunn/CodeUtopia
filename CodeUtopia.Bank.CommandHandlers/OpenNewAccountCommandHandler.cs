using CodeUtopia.Bank.Commands.v1;
using CodeUtopia.Bank.Domain.Client;
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
            var client = _aggregateRepository.Get<Client>(openNewAccountCommand.ClientId);

            var account = client.OpenNewAccount(openNewAccountCommand.AccountId, openNewAccountCommand.AccountName);

            _aggregateRepository.Add(account);
            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}