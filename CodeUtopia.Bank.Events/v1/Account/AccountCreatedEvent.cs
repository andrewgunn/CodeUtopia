using System;

namespace CodeUtopia.Bank.Events.v1.Account
{
    [Serializable]
    public class AccountCreatedEvent : AccountDomainEvent
    {
        public AccountCreatedEvent(Guid aggregateId, int versionNumber, Guid clientId, string accountName)
            : base(aggregateId, versionNumber)
        {
            _clientId = clientId;
            _accountName = accountName;
        }

        public string AccountName
        {
            get
            {
                return _accountName;
            }
        }

        public Guid ClientId
        {
            get
            {
                return _clientId;
            }
        }

        private readonly string _accountName;

        private readonly Guid _clientId;
    }
}