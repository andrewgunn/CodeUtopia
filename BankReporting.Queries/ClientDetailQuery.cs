using System;
using BankReporting.Queries.Projections.ClientDetail;
using CodeUtopia;

namespace BankReporting.Queries
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