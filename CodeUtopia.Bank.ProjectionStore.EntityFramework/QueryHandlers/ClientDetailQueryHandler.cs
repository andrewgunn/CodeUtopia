using System.Data.Entity;
using System.Linq;
using CodeUtopia.Bank.ProjectionStore.Projections.ClientDetail;
using CodeUtopia.Bank.ProjectionStore.Queries;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.QueryHandlers
{
    public class ClientDetailQueryHandler : IQueryHandler<ClientDetailQuery, ClientDetailProjection>
    {
        public ClientDetailQueryHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public ClientDetailProjection Handle(ClientDetailQuery query)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var clientDetail = databaseContext.ClientDetails.AsNoTracking()
                                                  .Include(x => x.Accounts)
                                                  .Include(x => x.BankCards)
                                                  .SingleOrDefault(x => x.ClientId == query.ClientId);

                if (clientDetail == null)
                {
                    return null;
                }

                var accounts = clientDetail.Accounts.Select(x => new AccountProjection(x.AccountId, x.AccountName))
                                           .ToList();
                var bankCards = clientDetail.BankCards.Select(x => new BankCardProjection(x.BankCardId, x.AccountId))
                                            .ToList();

                return new ClientDetailProjection(clientDetail.ClientId, clientDetail.ClientName, accounts, bankCards);
            }
        }

        private readonly string _nameOrConnectionString;
    }
}