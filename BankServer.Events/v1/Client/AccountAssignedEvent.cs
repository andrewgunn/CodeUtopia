using System;

namespace BankServer.Events.v1.Client
{
    [Serializable]
    public class AccountAssignedEvent : ClientDomainEvent
    {
        public AccountAssignedEvent(Guid clientId, int versionNumber, Guid accountId, string accountName)
            : base(clientId, versionNumber)
        {
            _accountId = accountId;
            _accountName = accountName;
        }

        public Guid AccountId
        {
            get
            {
                return _accountId;
            }
        }

        public string AccountName
        {
            get { return _accountName; }
        }

        private readonly Guid _accountId;
        private readonly string _accountName;
    }
}