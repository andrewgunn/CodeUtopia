using System.Linq;
using BankReporting.Queries;
using BankReporting.Queries.Projections.AccountDetail;
using CodeUtopia;

namespace BankReporting.ProjectionStore.QueryHandlers
{
    public class AccountDetailQueryHandler : IQueryHandler<AccountDetailQuery, AccountDetailProjection>
    {
        public AccountDetailQueryHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public AccountDetailProjection Handle(AccountDetailQuery query)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var accountDetail = databaseContext.AccountDetails.SingleOrDefault(x => x.AccountId == query.AccountId);

                return accountDetail == null
                           ? null
                           : new AccountDetailProjection(accountDetail.AccountId,
                                                         accountDetail.ClientId,
                                                         accountDetail.AccountName,
                                                         accountDetail.Balance);
            }
        }

        private readonly string _nameOrConnectionString;
    }
}