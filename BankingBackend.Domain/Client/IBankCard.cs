using System;

namespace BankingBackend.Domain.Client
{
    public interface IBankCard
    {
        [Obsolete]
        void ReportStolen();

        void ReportStolen(DateTime stolenAt);
    }
}