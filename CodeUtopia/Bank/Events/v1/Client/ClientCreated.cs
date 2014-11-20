using System;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Events.v1.Client
{
    public class ClientCreated : DomainEvent
    {
        public ClientCreated(Guid aggregateId, IVersionNumberProvider versionNumberProvider, string clientName)
            : base(aggregateId, versionNumberProvider)
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