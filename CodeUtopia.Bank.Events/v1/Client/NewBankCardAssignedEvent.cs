using System;

namespace CodeUtopia.Bank.Events.v1.Client
{
    [Serializable]
    public class NewBankCardAssignedEvent : ClientDomainEvent
    {
        public NewBankCardAssignedEvent(Guid aggregateId, int versionNumber, Guid bankCardId, Guid accountId)
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

        public Guid BankCardId
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