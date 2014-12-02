using System;
using Newtonsoft.Json;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    public class ClientCreatedEvent : ClientDomainEvent
    {
        [JsonConstructor]
        public ClientCreatedEvent(Guid clientId, int versionNumber, string clientName)
            : base(clientId, versionNumber)
        {
            ClientName = clientName;
        }

        public string ClientName { get; set; }
    }
}