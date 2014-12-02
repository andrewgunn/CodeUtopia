using BankingBackend.Commands.v1;
using BankingBackend.Domain.Account;
using CodeUtopia.Domain;
using NServiceBus;

namespace BankingBackend.CommandHandlers
{
    public class WithdrawAmountCommandHandler : IHandleMessages<WithdrawAmountCommand>
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