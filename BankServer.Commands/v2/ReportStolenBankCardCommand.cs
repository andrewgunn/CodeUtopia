using System;

namespace BankServer.Commands.v2
{
    public class ReportStolenBankCardCommand
    {
        public ReportStolenBankCardCommand(Guid clientId, Guid bankCardId, DateTime stolenAt)
        {
            _clientId = clientId;
            _bankCardId = bankCardId;
            _stolenAt = stolenAt;
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

        public DateTime StolenAt
        {
            get
            {
                return _stolenAt;
            }
        }

        private readonly Guid _bankCardId;

        private readonly Guid _clientId;

        private readonly DateTime _stolenAt;
    }
}