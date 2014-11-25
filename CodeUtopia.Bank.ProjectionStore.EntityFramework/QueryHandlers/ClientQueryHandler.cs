using System.Linq;
using CodeUtopia.Bank.ProjectionStore.Projections.Client;
using CodeUtopia.Bank.ProjectionStore.Queries;

namespace CodeUtopia.Bank.ProjectionStore.EntityFramework.QueryHandlers
{
    public class ClientQueryHandler : IQueryHandler<ClientsQuery, ClientsProjection>
    {
        public ClientQueryHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public ClientsProjection Handle(ClientsQuery query)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var clients = databaseContext.Clients.ToList();

                var clientProjections = clients.Select(x => new ClientProjection(x.ClientId, x.ClientName))
                                               .ToList();

                return new ClientsProjection(clientProjections);
            }
        }

        private readonly string _nameOrConnectionString;
    }
}