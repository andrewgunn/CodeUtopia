using System;

namespace CodeUtopia.Event
{
    public interface IEntityEvent : IDomainEvent
    {
        Guid EntityId { get; }
    }
}