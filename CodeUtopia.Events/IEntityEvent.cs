using System;

namespace CodeUtopia.Events
{
    public interface IEntityEvent : IDomainEvent
    {
        Guid EntityId { get; }
    }
}