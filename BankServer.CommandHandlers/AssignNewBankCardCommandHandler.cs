using BankServer.Commands.v1;
using BankServer.Domain.Client;
using CodeUtopia;
using CodeUtopia.Domain;

namespace BankServer.CommandHandlers
{
    public class AssignNewBankCardCommandHandler : ICommandHandler<AssignNewBankCardCommand>
    {
        public AssignNewBankCardCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(AssignNewBankCardCommand assignNewBankCardCommand)
        {
            var client = _aggregateRepository.Get<Client>(assignNewBankCardCommand.ClientId);
            client.AssignNewBankCard(assignNewBankCardCommand.BankCardId, assignNewBankCardCommand.AccountId);

            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}