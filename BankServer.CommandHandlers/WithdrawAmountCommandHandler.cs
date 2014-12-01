using BankServer.Commands.v1;
using BankServer.Domain.Account;
using CodeUtopia;
using CodeUtopia.Domain;

namespace BankServer.CommandHandlers
{
    public class WithdrawAmountCommandHandler : ICommandHandler<WithdrawAmountCommand>
    {
        public WithdrawAmountCommandHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public void Handle(WithdrawAmountCommand withdrawAmountCommand)
        {
            var account = _aggregateRepository.Get<Account>(withdrawAmountCommand.AccountId);
            account.Withdraw(withdrawAmountCommand.Amount);

            _aggregateRepository.Commit();
        }

        private readonly IAggregateRepository _aggregateRepository;
    }
}