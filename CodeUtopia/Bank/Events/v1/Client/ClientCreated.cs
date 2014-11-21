using System;
using CodeUtopia.Event;

namespace CodeUtopia.Bank.Events.v1.Client
{
    public class ClientCreated : DomainEvent
    {
        public ClientCreated(Guid aggregateId, int versionNumber, string clientName)
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