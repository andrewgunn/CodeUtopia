using CodeUtopia.Bank.Commands.v1;
using CodeUtopia.Bank.Domain.Client;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.CommandHandlers
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