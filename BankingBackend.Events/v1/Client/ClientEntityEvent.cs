using System;
using CodeUtopia.Events;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    public abstract class ClientEntityEvent : EntityEvent
    {
        protected ClientEntityEvent(Guid clientId, int versionNumber, Guid entityId)
            : base(clientId, versionNumber, entityId)
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