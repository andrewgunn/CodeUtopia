namespace BankServer.Domain.Account
{
    public class AccountName
    {
        public AccountName(string accountName)
        {
            _accountName = accountName;
        }

        public static implicit operator string(AccountName accountName)
        {
            return accountName._accountName;
        }

        public static implicit operator AccountName(string accountName)
        {
            return new AccountName(accountName);
        }

        private readonly string _accountName;
    }
}