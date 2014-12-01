using System;

namespace BankServer.Events.v1.Client
{
    [Serializable]
    public class ClientCreatedEvent : ClientDomainEvent
    {
        public ClientCreatedEvent(Guid clientId, int versionNumber, string clientName)
            : base(clientId, versionNumber)
        {
            _clientName = clientName;
        }

        public string ClientName
        {
            get
            {
                return _clientName;
            }
        }

        private readonly string _clientName;
    }
}