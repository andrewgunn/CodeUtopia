using System;

namespace BankingBackend.Commands.v1
{
    public class OpenNewAccountCommand
    {
        public OpenNewAccountCommand(Guid clientId, Guid accountId, string accountName)
        {
            _clientId = clientId;
            _accountId = accountId;
            _accountName = accountName;
        }

        public Guid AccountId
        {
            get
            {
                return _accountId;
            }
        }

        public string AccountName
        {
            get
            {
                return _accountName;
            }
        }

        public Guid ClientId
        {
            get
            {
                return _clientId;
            }
        }

        private readonly Guid _accountId;

        private readonly string _accountName;

        private readonly Guid _clientId;
    }
}