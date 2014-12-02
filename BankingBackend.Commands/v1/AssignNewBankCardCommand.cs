using System;
using Newtonsoft.Json;

namespace BankingBackend.Commands.v1
{
    public class AssignNewBankCardCommand
    {
        [JsonConstructor]
        public AssignNewBankCardCommand(Guid clientId, Guid bankCardId, Guid accountId)
        {
            ClientId = clientId;
            BankCardId = bankCardId;
            AccountId = accountId;
        }

        public Guid AccountId { get; set; }

        public Guid BankCardId { get; set; }

        public Guid ClientId { get; set; }
    }
}