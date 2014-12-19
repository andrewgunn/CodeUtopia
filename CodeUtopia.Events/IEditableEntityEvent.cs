using System;

namespace CodeUtopia.Events
{
    public interface IEditableEntityEvent : IEntityEvent, IEditableDomainEvent
    {
        Guid EntityId { get; set; }
    }
}