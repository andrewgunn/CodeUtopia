using System;

namespace CodeUtopia.Messages
{
    public interface IEditableDomainEvent : IDomainEvent
    {
        Guid AggregateId { set; }

        int AggregateVersionNumber { set; }
    }
}