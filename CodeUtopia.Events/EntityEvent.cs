using System;

namespace CodeUtopia.Events
{
    public abstract class EntityEvent : DomainEvent, IEntityEvent
    {
        protected EntityEvent(Guid aggregateId, int versionNumber, Guid entityId)
            : base(aggregateId, versionNumber)
        {
            _entityId = entityId;
        }

        public Guid EntityId
        {
            get
            {
                return _entityId;
            }
        }

        private readonly Guid _entityId;
    }
}