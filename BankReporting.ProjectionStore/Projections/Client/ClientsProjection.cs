using System.Collections.Generic;

namespace BankReporting.ProjectionStore.Projections.Client
{
    public class ClientsProjection
    {
        public ClientsProjection(IReadOnlyCollection<ClientProjection> clientProjection)
        {
            _clientProjection = clientProjection;
        }

        public IReadOnlyCollection<ClientProjection> ClientProjections
        {
            get
            {
                return _clientProjection;
            }
        }

        private readonly IReadOnlyCollection<ClientProjection> _clientProjection;
    }
}