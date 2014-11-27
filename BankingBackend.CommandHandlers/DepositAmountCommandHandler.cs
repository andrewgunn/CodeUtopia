using BankingBackend.Commands.v1;
using BankingBackend.Domain.Account;
using CodeUtopia;
using CodeUtopia.Domain;

namespace BankingBackend.CommandHandlers
{
    public class DepositAmountCommandHandler : ICommandHandler<DepositAmountCommand>
    {
        public DepositAmountCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(DepositAmountCommand depositAmountCommand)
        {
            var account = _aggregateRepository.Get<Account>(depositAmountCommand.AccountId);
            account.Deposit(depositAmountCommand.Amount);

            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}