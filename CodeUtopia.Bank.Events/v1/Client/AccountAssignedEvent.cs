using System;

namespace CodeUtopia.Bank.Events.v1.Client
{
    [Serializable]
    public class AccountAssignedEvent : ClientDomainEvent
    {
        public AccountAssignedEvent(Guid aggregateId, int versionNumber, Guid accountId)
            : base(aggregateId, versionNumber)
        {
            _accountId = accountId;
        }

        public Guid AccountId
        {
            get
            {
                return _accountId;
            }
        }

        private readonly Guid _accountId;
    }
}