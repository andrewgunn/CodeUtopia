using System;
using Newtonsoft.Json;

namespace BankingBackend.Commands.v1
{
    public class OpenNewAccountCommand
    {
        [JsonConstructor]
        public OpenNewAccountCommand(Guid clientId, Guid accountId, string accountName)
        {
            ClientId = clientId;
            AccountId = accountId;
            AccountName = accountName;
        }

        public Guid AccountId { get; set; }

        public string AccountName { get; set; }

        public Guid ClientId { get; set; }
    }
}