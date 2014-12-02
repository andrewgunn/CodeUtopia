using System;
using Newtonsoft.Json;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    public class NewBankCardAssignedEvent : ClientDomainEvent
    {
        [JsonConstructor]
        public NewBankCardAssignedEvent(Guid clientId, int versionNumber, Guid bankCardId, Guid accountId)
            : base(clientId, versionNumber)
        {
            BankCardId = bankCardId;
            AccountId = accountId;
        }

        public Guid AccountId { get; set; }

        public Guid BankCardId { get; set; }
    }
}