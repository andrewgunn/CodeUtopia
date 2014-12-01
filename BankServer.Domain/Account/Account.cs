using System;
using BankServer.Events.v1.Account;
using BankServer.Mementoes.v1;
using CodeUtopia.Domain;

namespace BankServer.Domain.Account
{
    public class Account : Aggregate, IOriginator
    {
        public Account()
        {
            _accountName = new AccountName("");
            _balance = new Balance();

            RegisterEventHandlers();
        }

        private Account(Guid accountId, Guid clientId, AccountName accountName)
            : this()
        {
            Apply(new AccountCreatedEvent(accountId, GetNextVersionNumber(), clientId, accountName));
        }

        public static Account Create(Guid accountId, Guid clientId, AccountName accountName)
        {
            return new Account(accountId, clientId, accountName);
        }

        public IMemento CreateMemento()
        {
            return new AccountMemento(_accountName, _balance);
        }

        public void Deposit(Amount amount)
        {
            EnsureAccountIsInitialized();

            var balance = _balance.Deposit(amount);

            Apply(new AmountDepositedEvent(AggregateId, GetNextVersionNumber(), balance, amount));
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

        private void OnAccountCreated(AccountCreatedEvent accountCreatedEvent)
        {
            AggregateId = accountCreatedEvent.AccountId;
            _accountName = accountCreatedEvent.AccountName;
            _clientId = accountCreatedEvent.ClientId;
        }

        private void OnAmountDeposited(AmountDepositedEvent amountDepositedEvent)
        {
            _balance = amountDepositedEvent.Balance;
        }

        private void OnAmountWithdrawn(AmountWithdrawnEvent amountWithdrawnEvent)
        {
            _balance = amountWithdrawnEvent.Balance;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<AccountCreatedEvent>(OnAccountCreated);
            RegisterEventHandler<AmountDepositedEvent>(OnAmountDeposited);
            RegisterEventHandler<AmountWithdrawnEvent>(OnAmountWithdrawn);
        }

        public void Withdraw(Amount amount)
        {
            EnsureAccountIsInitialized();
            EnsureBalanceHasSufficientFundsForWithdrawl(amount);

            var balance = _balance.Withdraw(amount);

            Apply(new AmountWithdrawnEvent(AggregateId, GetNextVersionNumber(), balance, amount));
        }

        private AccountName _accountName;

        private Balance _balance;

        private Guid _clientId;
    }
}