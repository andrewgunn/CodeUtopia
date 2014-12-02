using System;
using BankingBackend.Events.v1.Client;

namespace BankingBackend.Events.v2.Client
{
    [Serializable]
    public class BankCardReportedStolenEvent : BankCardEvent
    {
        public BankCardReportedStolenEvent(Guid clientId, int versionNumber, Guid bankCardId, DateTime stolenAt)
            : base(clientId, versionNumber, bankCardId)
        {
            StolenAt = stolenAt;
        }

        public DateTime StolenAt { get; set; }
    }
}