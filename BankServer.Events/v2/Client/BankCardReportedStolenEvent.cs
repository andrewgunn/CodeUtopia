using System;
using BankServer.Events.v1.Client;

namespace BankServer.Events.v2.Client
{
    [Serializable]
    public class BankCardReportedStolenEvent : BankCardEvent
    {
        public BankCardReportedStolenEvent(Guid clientId, int versionNumber, Guid bankCardId, DateTime stolenAt)
            : base(clientId, versionNumber, bankCardId)
        {
            _stolenAt = stolenAt;
        }

        public DateTime StolenAt
        {
            get
            {
                return _stolenAt;
            }
        }

        private readonly DateTime _stolenAt;
    }
}