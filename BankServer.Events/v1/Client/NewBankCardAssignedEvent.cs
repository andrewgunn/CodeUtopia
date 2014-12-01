using System;

namespace BankServer.Events.v1.Client
{
    [Serializable]
    public class NewBankCardAssignedEvent : ClientDomainEvent
    {
        public NewBankCardAssignedEvent(Guid clientId, int versionNumber, Guid bankCardId, Guid accountId)
            : base(clientId, versionNumber)
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