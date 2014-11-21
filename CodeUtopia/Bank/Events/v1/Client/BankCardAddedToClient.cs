using System;
using CodeUtopia.Event;

namespace CodeUtopia.Bank.Events.v1.Client
{
    public class BankCardAddedToClient : DomainEvent
    {
        public BankCardAddedToClient(Guid aggregateId, int versionNumber, Guid bankCardId, Guid accountId)
            : base(aggregateId, versionNumber)
        {
            _bankCardId = bankCardId;
            _accountId = accountId;
        }

        public Guid AccountId
        {
            get
            {
                return _accountId;
            }
        }

        public Guid BankAccountId
        {
            get
            {
                return _bankCardId;
            }
        }

        private readonly Guid _accountId;

        private readonly Guid _bankCardId;
    }
}