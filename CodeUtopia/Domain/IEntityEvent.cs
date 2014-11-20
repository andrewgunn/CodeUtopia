using System;

namespace CodeUtopia.Domain
{
    public interface IEntityEvent : IDomainEvent
    {
        Guid EntityId { get; }
    }
}