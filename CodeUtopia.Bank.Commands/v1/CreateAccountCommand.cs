using System;

namespace CodeUtopia.Bank.Commands.v1
{
    public class CreateAccountCommand
    {
        public CreateAccountCommand(Guid accountId, Guid clientId, string accountName)
        {
            _accountId = accountId;
            _clientId = clientId;
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