using System;
using BankReporting.Queries.Projections.AccountDetail;
using CodeUtopia;

namespace BankReporting.Queries
{
    public class AccountDetailQuery : IQuery<AccountDetailProjection>
    {
        public AccountDetailQuery(Guid accountId)
        {
            _accountId = accountId;
        }

        public Guid AccountId
        {
            get
            {
                return _accountId;
            }
        }

        private readonly Guid _accountId;
    }
}