using System;
using CodeUtopia.Event;

namespace CodeUtopia.Bank.Events.v1.Client
{
    public class AccountAddedToClient : DomainEvent
    {
        public AccountAddedToClient(Guid aggregateId, int versionNumber, Guid accountId)
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