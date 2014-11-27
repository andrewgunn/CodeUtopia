using System;
using CodeUtopia.Events;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    public abstract class ClientDomainEvent : DomainEvent
    {
        protected ClientDomainEvent(Guid clientId, int versionNumber)
            : base(clientId, versionNumber)
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