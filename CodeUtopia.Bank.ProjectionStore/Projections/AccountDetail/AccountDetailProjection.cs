using System;

namespace CodeUtopia.Bank.ProjectionStore.Projections.AccountDetail
{
    public class AccountDetailProjection
    {
        public AccountDetailProjection(Guid accountId, string accountName, decimal balance)
        {
            _accountId = accountId;
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

        private readonly Guid _accountId;

        private readonly string _accountName;

        private readonly decimal _balance;
    }
}