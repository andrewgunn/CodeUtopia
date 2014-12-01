namespace BankServer.Domain.Account
{
    public class Amount
    {
        public Amount(decimal decimalAmount)
        {
            _decimalAmount = decimalAmount;
        }

        public Amount Add(Amount amount)
        {
            var newDecimalAmount = _decimalAmount + amount._decimalAmount;
            return new Amount(newDecimalAmount);
        }

        public bool IsNegative()
        {
            return _decimalAmount < 0;
        }

        public Amount Substract(Amount amount)
        {
            var newDecimalAmount = _decimalAmount - amount._decimalAmount;
            return new Amount(newDecimalAmount);
        }

        public static implicit operator decimal(Amount amount)
        {
            return amount._decimalAmount;
        }

        public static implicit operator Amount(decimal decimalAmount)
        {
            return new Amount(decimalAmount);
        }

        private readonly decimal _decimalAmount;
    }
}