using System;

namespace BankReporting.ProjectionStore.Projections.ClientDetail
{
    public class AccountProjection
    {
        public AccountProjection(Guid accountId, string accountName)
        {
            _accountId = accountId;
            _accountName = accountName;
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

        private readonly Guid _accountId;

        private readonly string _accountName;
    }
}