using System;
using Newtonsoft.Json;

namespace BankingBackend.Commands.v1
{
    public class ReportStolenBankCardCommand
    {
        [JsonConstructor]
        public ReportStolenBankCardCommand(Guid clientId, Guid bankCardId)
        {
            ClientId = clientId;
            BankCardId = bankCardId;
        }

        public Guid BankCardId { get; set; }

        public Guid ClientId { get; set; }
    }
}