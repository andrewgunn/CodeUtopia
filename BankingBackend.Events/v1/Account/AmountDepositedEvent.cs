using System;
using Newtonsoft.Json;

namespace BankingBackend.Events.v1.Account
{
    [Serializable]
    public class AmountDepositedEvent : AccountDomainEvent
    {
        [JsonConstructor]
        public AmountDepositedEvent(Guid accountId, int versionNumber, decimal balance, decimal amount)
            : base(accountId, versionNumber)
        {
            Balance = balance;
            Amount = amount;
        }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
    }
}