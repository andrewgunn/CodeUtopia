using System;
using CodeUtopia.Events;

namespace CodeUtopia.Bank.Events.v1.Account
{
    [Serializable]
    public abstract class AccountDomainEvent : DomainEvent
    {
        protected AccountDomainEvent(Guid aggregateId, int versionNumber)
            : base(aggregateId, versionNumber)
        {
        }

        public Guid AccountId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }
    }
}