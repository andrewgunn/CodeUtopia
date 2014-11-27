using System;

namespace BankingBackend.Domain.Client
{
    public class BankCardIsReportedStolenException : Exception
    {
        public BankCardIsReportedStolenException(Guid bankCardId)
            : base(string.Format("The bank card {0} has been reported stolen.", bankCardId))
        {
        }
    }
}