using System;
using CodeUtopia.Events;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    public class BankCardReportedStolenEvent : ClientEntityEvent
    {
        public BankCardReportedStolenEvent(Guid aggregateId, int versionNumber, Guid entityId)
            : base(aggregateId, versionNumber, entityId)
        {
        }

        public Guid BankCardId
        {
            get
            {
                return ((IEntityEvent)this).EntityId;
            }
        }
    }
}