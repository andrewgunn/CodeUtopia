using System;
using CodeUtopia.Domain;

namespace CodeUtopia.Bank.Events.v1.Client
{
    public class BankCardReportedStolen : EntityEvent
    {
        public BankCardReportedStolen(Guid aggregateId, IVersionNumberProvider versionNumberProvider, Guid entityId)
            : base(aggregateId, versionNumberProvider, entityId)
        {
        }
    }
}