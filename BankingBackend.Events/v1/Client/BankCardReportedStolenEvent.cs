using System;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    [Obsolete]
    public class BankCardReportedStolenEvent : BankCardEvent
    {
        public BankCardReportedStolenEvent(Guid clientId, int versionNumber, Guid bankCardId)
            : base(clientId, versionNumber, bankCardId)
        {
        }
    }
}