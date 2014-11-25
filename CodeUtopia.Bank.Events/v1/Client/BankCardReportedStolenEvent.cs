using System;

namespace CodeUtopia.Bank.Events.v1.Client
{
    [Serializable]
    public class BankCardReportedStolenEvent : ClientEntityEvent
    {
        public BankCardReportedStolenEvent(Guid aggregateId, int versionNumber, Guid entityId)
            : base(aggregateId, versionNumber, entityId)
        {
        }
    }
}