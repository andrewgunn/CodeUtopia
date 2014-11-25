using System;

namespace CodeUtopia.Bank.ProjectionStore.Projections.Client
{
    public class ClientProjection
    {
        public ClientProjection(Guid clientId, string clientName)
        {
            _clientId = clientId;
            _clientName = clientName;
        }

        public Guid ClientId
        {
            get
            {
                return _clientId;
            }
        }

        public string ClientName
        {
            get
            {
                return _clientName;
            }
        }

        private readonly Guid _clientId;

        private readonly string _clientName;
    }
}