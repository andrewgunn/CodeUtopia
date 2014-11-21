using System;
using CodeUtopia.Event;

namespace CodeUtopia.Bank.Events.v1.Account
{
    public class AmountWithdrawn : DomainEvent
    {
        public AmountWithdrawn(Guid aggregateId, int versionNumber, decimal balance, decimal amount)
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