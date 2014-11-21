using System;
using CodeUtopia.Event;

namespace CodeUtopia.Bank.Events.v1.Client
{
    public class BankCardReportedStolen : EntityEvent
    {
        public BankCardReportedStolen(Guid aggregateId, int versionNumber, Guid entityId)
            : base(aggregateId, versionNumber, entityId)
        {
        }
    }
}