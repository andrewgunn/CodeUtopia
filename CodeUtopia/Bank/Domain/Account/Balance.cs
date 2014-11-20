namespace CodeUtopia.Bank.Domain.Account
{
    public class Balance
    {
        public Balance()
        {
            _amount = new Amount(0);
        }

        private Balance(decimal decimalAmount)
        {
            _amount = new Amount(decimalAmount);
        }

        public Balance Deposit(Amount amount)
        {
            return new Balance(_amount.Add(amount));
        }

        public bool HasSufficientFundsForWithdrawl(Amount amount)
        {
            return new Amount(_amount).Substract(amount)
                                      .IsNegative();
        }

        public Balance Withdraw(Amount amount)
        {
            return new Balance(_amount.Substract(amount));
        }

        public static implicit operator decimal(Balance balance)
        {
            return balance._amount;
        }

        public static implicit operator Balance(decimal decimalAmount)
        {
            return new Balance(decimalAmount);
        }

        private readonly Amount _amount;
    }
}