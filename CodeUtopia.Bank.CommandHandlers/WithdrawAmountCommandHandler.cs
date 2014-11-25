using CodeUtopia.Bank.Commands.v1;
using CodeUtopia.Bank.Domain.Account;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.CommandHandlers
{
    public class WithdrawAmountCommandHandler : ICommandHandler<WithdrawAmountCommand>
    {
        public WithdrawAmountCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Execute(WithdrawAmountCommand withdrawAmountCommand)
        {
            var account = _aggregateRepository.Get<Account>(withdrawAmountCommand.AccountId);
            account.Withdraw(withdrawAmountCommand.Amount);

            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}