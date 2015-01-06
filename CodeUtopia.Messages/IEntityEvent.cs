using System;

namespace CodeUtopia.Messages
{
    public interface IEntityEvent : IDomainEvent
    {
        Guid EntityId { get; }
    }
}