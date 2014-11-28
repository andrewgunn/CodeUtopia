using System;

namespace BankingBackend.Domain.Client
{
    public class BankCardAlreadyReportedStolenException : Exception
    {
        public BankCardAlreadyReportedStolenException(Guid bankCardId)
            : base(string.Format("The bank card {0} has already been reported stolen.", bankCardId))
        {
        }
    }
}