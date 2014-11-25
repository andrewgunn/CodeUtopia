using System;

namespace CodeUtopia.Bank.Commands.v1
{
    public class DepositAmountCommand
    {
        public DepositAmountCommand(Guid accountId, decimal amount)
        {
            _accountId = accountId;
            _amount = amount;
        }

        public Guid AccountId
        {
            get
            {
                return _accountId;
            }
        }

        public decimal Amount
        {
            get
            {
                return _amount;
            }
        }

        private readonly Guid _accountId;

        private readonly decimal _amount;
    }
}