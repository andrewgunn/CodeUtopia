using CodeUtopia.Domain;

namespace BankingBackend.Mementoes.v1
{
    public class AccountMemento : IMemento
    {
        public AccountMemento(string accountName, decimal balance)
        {
            _accountName = accountName;
            _balance = balance;
        }

        public string AccountName
        {
            get
            {
                return _accountName;
            }
        }

        public decimal Balanace
        {
            get
            {
                return _balance;
            }
        }

        private readonly string _accountName;

        private readonly decimal _balance;
    }
}