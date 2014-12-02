using System;
using Newtonsoft.Json;

namespace BankingBackend.Commands.v1
{
    public class DepositAmountCommand
    {
        [JsonConstructor]
        public DepositAmountCommand(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }

        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }
}