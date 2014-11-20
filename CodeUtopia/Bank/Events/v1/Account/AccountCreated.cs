using System;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Events.v1.Account
{
    public class AccountCreated : DomainEvent
    {
        public AccountCreated(Guid aggregateId,
                              IVersionNumberProvider versionNumberProvider,
                              Guid clientId,
                              string accountName)
            : base(aggregateId, versionNumberProvider)
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