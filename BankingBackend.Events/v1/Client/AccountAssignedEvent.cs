using System;
using Newtonsoft.Json;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    public class AccountAssignedEvent : ClientDomainEvent
    {
        [JsonConstructor]
        public AccountAssignedEvent(Guid clientId, int versionNumber, Guid accountId)
            : base(clientId, versionNumber)
        {
            AccountId = accountId;
        }

        public Guid AccountId { get; set; }
    }
}