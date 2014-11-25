using System;
using CodeUtopia.Bank.ProjectionStore.Projections.AccountDetail;

namespace CodeUtopia.Bank.ProjectionStore.Queries
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