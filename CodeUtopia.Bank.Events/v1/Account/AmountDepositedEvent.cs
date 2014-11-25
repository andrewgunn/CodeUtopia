using System;

namespace CodeUtopia.Bank.Events.v1.Account
{
    [Serializable]
    public class AmountDepositedEvent : AccountDomainEvent
    {
        public AmountDepositedEvent(Guid aggregateId, int versionNumber, decimal balance, decimal amount)
            : base(aggregateId, versionNumber)
        {
            _balance = balance;
            _amount = amount;
        }

        public decimal Amount
        {
            get
            {
                return _amount;
            }
        }

        public decimal Balance
        {
            get
            {
                return _balance;
            }
        }

        private readonly decimal _amount;

        private readonly decimal _balance;
    }
}