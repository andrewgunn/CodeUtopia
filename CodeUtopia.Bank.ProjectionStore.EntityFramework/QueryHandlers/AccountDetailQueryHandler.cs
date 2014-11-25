using System.Linq;
using CodeUtopia.Bank.ProjectionStore.Projections.AccountDetail;
using CodeUtopia.Bank.ProjectionStore.Queries;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.QueryHandlers
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
                                                         accountDetail.AccountName,
                                                         accountDetail.Balance);
            }
        }

        private readonly string _nameOrConnectionString;
    }
}