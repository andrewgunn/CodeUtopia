using System;

namespace CodeUtopia.Domain
{
    public abstract class EntityEvent : DomainEvent, IEntityEvent
    {
        protected EntityEvent(Guid aggregateId, IVersionNumberProvider versionNumberProvider, Guid entityId)
            : base(aggregateId, versionNumberProvider)
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