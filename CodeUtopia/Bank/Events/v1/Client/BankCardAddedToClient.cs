using System;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Events.v1.Client
{
    public class BankCardAddedToClient : DomainEvent
    {
        public BankCardAddedToClient(Guid aggregateId,
                                     IVersionNumberProvider versionNumberProvider,
                                     Guid accountId,
                                     Guid bankCardId)
            : base(aggregateId, versionNumberProvider)
        {
            _accountId = accountId;
            _bankCardId = bankCardId;
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