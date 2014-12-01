using System;

namespace BankServer.Commands.v1
{
    //[Obsolete]
    public class ReportStolenBankCardCommand
    {
        public ReportStolenBankCardCommand(Guid clientId, Guid bankCardId)
        {
            _clientId = clientId;
            _bankCardId = bankCardId;
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

        private readonly Guid _bankCardId;

        private readonly Guid _clientId;
    }
}