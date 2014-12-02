using System;
using Newtonsoft.Json;

namespace BankingBackend.Events.v1.Account
{
    [Serializable]
    public class AccountCreatedEvent : AccountDomainEvent
    {
        [JsonConstructor]
        public AccountCreatedEvent(Guid accountId, int versionNumber, Guid clientId, string accountName)
            : base(accountId, versionNumber)
        {
            ClientId = clientId;
            AccountName = accountName;
        }

        public string AccountName { get; set; }

        public Guid ClientId { get; set; }
    }
}