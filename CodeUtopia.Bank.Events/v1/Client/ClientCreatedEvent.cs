using System;

namespace CodeUtopia.Bank.Events.v1.Client
{
    [Serializable]
    public class ClientCreatedEvent : ClientDomainEvent
    {
        public ClientCreatedEvent(Guid aggregateId, int versionNumber, string clientName)
            : base(aggregateId, versionNumber)
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