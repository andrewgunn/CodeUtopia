using System;

namespace CodeUtopia.Bank.Domain.Account
{
    public class BalanceHasInsufficientFundsForWithdrawlException : Exception
    {
        public BalanceHasInsufficientFundsForWithdrawlException(Balance balance, Amount amount)
            : base("There isn't sufficient funds in the balance, {0:C}, for a withdrawl of {1:C}")
        {
        }
    }
}