using System;
using CodeUtopia.Bank.ProjectionStore.Projections.ClientDetail;

namespace CodeUtopia.Bank.ProjectionStore.Queries
{
    public class ClientDetailQuery : IQuery<ClientDetailProjection>
    {
        public ClientDetailQuery(Guid clientId)
        {
            _clientId = clientId;
        }

        public Guid ClientId
        {
            get
            {
                return _clientId;
            }
        }

        private readonly Guid _clientId;
    }
}