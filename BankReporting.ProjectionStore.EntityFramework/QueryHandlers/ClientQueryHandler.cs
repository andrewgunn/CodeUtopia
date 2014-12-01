using System.Linq;
using BankReporting.ProjectionStore.Projections.Client;
using BankReporting.ProjectionStore.Queries;
using CodeUtopia;

namespace BankReporting.ProjectionStore.EntityFramework.QueryHandlers
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