using System;
using CodeUtopia.Bank.Domain.Mementos.v1;
using CodeUtopia.Bank.Events.v1.Account;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Domain.Account
{
    public class Account : Aggregate, IOriginator
    {
        public Account()
        {
            _accountName = new AccountName("");
            _balance = new Balance();

            RegisterEventHandlers();
        }

        private Account(Guid clientId, string accountName)
        {
            Apply(new AccountCreated(Guid.NewGuid(), this, clientId, accountName));
        }

        public static Account Create(Guid clientId, string accountName)
        {
            return new Account(clientId, accountName);
        }

        public IMemento CreateMemento()
        {
            return new AccountMemento(_accountName, _balance);
        }

        public void Deposit(Amount amount)
        {
            EnsureAccountIsInitialized();

            var balance = _balance.Deposit(amount);

            Apply(new AmountDeposited(AggregateId, this, balance, amount));
        }

        protected void EnsureAccountIsInitialized()
        {
            EnsureIsInitialized();
        }

        protected void EnsureBalanceHasSufficientFundsForWithdrawl(Amount amount)
        {
            if (_balance.HasSufficientFundsForWithdrawl(amount))
            {
                throw new BalanceHasInsufficientFundsForWithdrawlException(_balance, amount);
            }
        }

        public void LoadFromMemento(Guid aggregateId, int versionNumber, IMemento memento)
        {
            var accountMemento = memento as AccountMemento;

            if (accountMemento == null)
            {
                throw new UnexpectedMementoException(memento);
            }

            AggregateId = aggregateId;
            VersionNumber = versionNumber;

            _accountName = accountMemento.AccountName;
            _balance = accountMemento.Balanace;
        }

        private void OnAccountCreated(AccountCreated accountCreated)
        {
            AggregateId = accountCreated.AggregateId;
            _accountName = accountCreated.AccountName;
            _clientId = accountCreated.ClientId;
        }

        private void OnAmountDeposited(AmountDeposited amountDeposited)
        {
            _balance = amountDeposited.Balance;
        }

        private void OnAmountWithdrawn(AmountWithdrawn amountWithdrawn)
        {
            _balance = amountWithdrawn.Balance;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<AccountCreated>(OnAccountCreated);
            RegisterEventHandler<AmountDeposited>(OnAmountDeposited);
            RegisterEventHandler<AmountWithdrawn>(OnAmountWithdrawn);
        }

        public void Withdraw(Amount amount)
        {
            EnsureAccountIsInitialized();
            EnsureBalanceHasSufficientFundsForWithdrawl(amount);

            var balance = _balance.Withdraw(amount);

            Apply(new AmountWithdrawn(AggregateId, this, balance, amount));
        }

        private AccountName _accountName;

        private Balance _balance;

        private Guid _clientId;
    }
}