using System;
using BankReporting.ProjectionStore.Projections.ClientDetail;
using CodeUtopia;

namespace BankReporting.ProjectionStore.Queries
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