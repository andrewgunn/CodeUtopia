using System;
using Newtonsoft.Json;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    [Obsolete]
    public class BankCardReportedStolenEvent : BankCardEvent
    {
        [JsonConstructor]
        public BankCardReportedStolenEvent(Guid clientId, int versionNumber, Guid bankCardId)
            : base(clientId, versionNumber, bankCardId)
        {
        }
    }
}