using System;

namespace CodeUtopia.Events
{
    public interface IEditableDomainEvent : IDomainEvent
    {
        Guid AggregateId { set; }

        int AggregateVersionNumber { set; }
    }
}