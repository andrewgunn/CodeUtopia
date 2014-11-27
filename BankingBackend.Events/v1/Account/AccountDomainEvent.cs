using System;
using CodeUtopia.Events;

namespace BankingBackend.Events.v1.Account
{
    [Serializable]
    public abstract class AccountDomainEvent : DomainEvent
    {
        protected AccountDomainEvent(Guid accountId, int versionNumber)
            : base(accountId, versionNumber)
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