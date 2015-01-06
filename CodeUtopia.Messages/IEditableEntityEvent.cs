using System;

namespace CodeUtopia.Messages
{
    public interface IEditableEntityEvent : IEntityEvent, IEditableDomainEvent
    {
        Guid EntityId { get; set; }
    }
}