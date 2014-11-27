using System;
using CodeUtopia.Events;

namespace BankingBackend.Events.v1.Account
{
    [Serializable]
    public abstract class AccountEntityEvent : EntityEvent
    {
        protected AccountEntityEvent(Guid aggregateId, int versionNumber, Guid entityId)
            : base(aggregateId, versionNumber, entityId)
        {
        }

        public Guid ClientId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }
    }
}