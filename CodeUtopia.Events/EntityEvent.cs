using System;

namespace CodeUtopia.Events
{
    [Serializable]
    public abstract class EntityEvent : DomainEvent, IEntityEvent
    {
        protected EntityEvent(Guid aggregateId, int aggregateVersionNumber, Guid entityId)
            : base(aggregateId, aggregateVersionNumber)
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