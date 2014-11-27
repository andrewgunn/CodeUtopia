using System;

namespace BankingBackend.Events.v1.Client
{
    [Serializable]
    public class AccountAssignedEvent : ClientDomainEvent
    {
        public AccountAssignedEvent(Guid clientId, int versionNumber, Guid accountId)
            : base(clientId, versionNumber)
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