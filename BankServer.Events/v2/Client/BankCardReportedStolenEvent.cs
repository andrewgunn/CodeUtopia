using System;
using BankServer.Events.v1.Client;

namespace BankServer.Events.v2.Client
{
    [Serializable]
    public class BankCardReportedStolenEvent : BankCardEvent
    {
        private readonly DateTime _stolenAt;

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
    }
}