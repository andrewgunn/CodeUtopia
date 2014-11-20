using System;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Events.v1.Client
{
    public class AccountAddedToClient : DomainEvent
    {
        public AccountAddedToClient(Guid aggregateId, IVersionNumberProvider versionNumberProvider, Guid accountId)
            : base(aggregateId, versionNumberProvider)
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