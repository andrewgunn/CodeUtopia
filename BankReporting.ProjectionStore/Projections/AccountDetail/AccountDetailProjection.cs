using System;

namespace BankReporting.ProjectionStore.Projections.AccountDetail
{
    public class AccountDetailProjection
    {
        public AccountDetailProjection(Guid accountId, Guid clientId, string accountName, decimal balance)
        {
            _accountId = accountId;
            _clientId = clientId;
            _accountName = accountName;
            _balance = balance;
        }

        public Guid AccountId
        {
            get
            {
                return _accountId;
            }
        }

        public string AccountName
        {
            get
            {
                return _accountName;
            }
        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
        }

        public Guid ClientId
        {
            get { return _clientId; }
        }

        private readonly Guid _accountId;
        private readonly Guid _clientId;

        private readonly string _accountName;

        private readonly decimal _balance;
    }
}