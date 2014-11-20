using System;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Events.v1.Account
{
    public class AmountDeposited : DomainEvent
    {
        public AmountDeposited(Guid aggregateId,
                               IVersionNumberProvider versionNumberProvider,
                               decimal balance,
                               decimal amount)
            : base(aggregateId, versionNumberProvider)
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