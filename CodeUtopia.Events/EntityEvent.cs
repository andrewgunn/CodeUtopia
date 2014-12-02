using System;

namespace CodeUtopia.Events
{
    [Serializable]
    public abstract class EntityEvent : DomainEvent, IEntityEvent
    {
        protected EntityEvent(Guid aggregateId, int versionNumber, Guid entityId)
            : base(aggregateId, versionNumber)
        {
            EntityId = entityId;
        }

        public Guid EntityId { get; set; }

    }
}