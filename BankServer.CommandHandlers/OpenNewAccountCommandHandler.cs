using BankServer.Commands.v1;
using BankServer.Domain.Client;
using CodeUtopia;
using CodeUtopia.Domain;

namespace BankServer.CommandHandlers
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