using CodeUtopia.Bank.Commands.v1;
using CodeUtopia.Bank.Domain.Account;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.CommandHandlers
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