using BankingBackend.Commands.v1;
using BankingBackend.Domain.Client;
using CodeUtopia.Domain;
using NServiceBus;

namespace BankingBackend.CommandHandlers
{
    public class AssignNewBankCardCommandHandler : IHandleMessages<AssignNewBankCardCommand>
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