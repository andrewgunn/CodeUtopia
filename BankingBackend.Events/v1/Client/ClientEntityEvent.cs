using System;
using CodeUtopia.Events;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    public abstract class ClientEntityEvent : EntityEvent
    {
        protected ClientEntityEvent(Guid aggregateId, int versionNumber, Guid entityId)
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