using System;
using CodeUtopia.Events;

namespace CodeUtopia.Bank.Events.v1.Client
{
    [Serializable]
    public abstract class ClientDomainEvent : DomainEvent
    {
        protected ClientDomainEvent(Guid aggregateId, int versionNumber)
            : base(aggregateId, versionNumber)
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