using System;

namespace CodeUtopia.Bank.Commands.v1
{
    public class AssignNewBankCardCommand
    {
        public AssignNewBankCardCommand(Guid clientId, Guid bankCardId, Guid accountId)
        {
            _clientId = clientId;
            _bankCardId = bankCardId;
            _accountId = accountId;
        }

        public Guid AccountId
        {
            get
            {
                return _accountId;
            }
        }

        public Guid BankCardId
        {
            get
            {
                return _bankCardId;
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

        private readonly Guid _bankCardId;

        private readonly Guid _clientId;
    }
}