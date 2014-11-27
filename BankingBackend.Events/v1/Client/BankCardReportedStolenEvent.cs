using System;
using CodeUtopia.Events;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    public class BankCardReportedStolenEvent : BankCardEvent
    {
        public BankCardReportedStolenEvent(Guid bankCardId, int versionNumber, Guid entityId)
            : base(bankCardId, versionNumber, entityId)
        {
        }
    }
}