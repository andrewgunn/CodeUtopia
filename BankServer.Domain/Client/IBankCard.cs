using System;

namespace BankServer.Domain.Client
{
    public interface IBankCard
    {
        [Obsolete]
        void ReportStolen();

        //void ReportStolen(DateTime stolenAt);
    }
}