using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Messages;

namespace CodeUtopia.Domain
{
    public abstract class Entity : IEntity
    {
        protected Entity(Guid aggregateId, IVersionNumberProvider versionNumberProvider, Guid entityId)
        {
            _aggregateId = aggregateId;
            _versionNumberProvider = versionNumberProvider;
            _entityId = entityId;

            _eventHandlers = new Dictionary<Type, Action<IEntityEvent>>();
            _appliedEvents = new List<IEntityEvent>();
        }

        protected void Apply(IEditableEntityEvent entityEvent)
        {
            entityEvent.AggregateId = AggregateId;
            entityEvent.AggregateVersionNumber = GetNextVersionNumber();
            entityEvent.EntityId = EntityId;

            Handle(entityEvent);

            _appliedEvents.Add(entityEvent);
        }

        public void ClearChanges()
        {
            _appliedEvents.Clear();
        }

        protected void EnsureIsInitialized()
        {
            if (!IsInitialized())
            {
                throw new EntityNotInitializedException();
            }
        }

        public IEnumerable<IEntityEvent> GetChanges()
        {
            return _appliedEvents.OrderBy(x => x.AggregateVersionNumber);
        }

        private int GetNextVersionNumber()
        {
            return _versionNumberProvider.GetNextVersionNumber();
        }

        private void Handle(IEntityEvent entityEvent)
        {
            var entityEventType = entityEvent.GetType();
            Action<IEntityEvent> entityEventHandler;

            if (!_eventHandlers.TryGetValue(entityEventType, out entityEventHandler))
            {
                throw new DomainEventHandlerNotRegisteredException(entityEventType);
            }

            entityEventHandler(entityEvent);
        }

        public bool IsInitialized()
        {
            return EntityId != default(Guid);
        }

        public void LoadFromHistory(IReadOnlyCollection<IEntityEvent> entityEvents)
        {
            if (!entityEvents.Any())
            {
                return;
            }

            foreach (var entityEvent in entityEvents)
            {
                Handle(entityEvent);
            }
        }

        protected void RegisterEventHandler<TEntityEvent>(Action<TEntityEvent> eventHandler)
            where TEntityEvent : class, IEntityEvent
        {
            _eventHandlers.Add(typeof(TEntityEvent), theEvent => eventHandler(theEvent as TEntityEvent));
        }

        public Guid AggregateId
        {
            get
            {
                return _aggregateId;
            }
        }

        public Guid EntityId
        {
            get
            {
                return _entityId;
            }
        }

        private readonly Guid _aggregateId;

        private readonly List<IEntityEvent> _appliedEvents;

        private readonly Guid _entityId;

        private readonly Dictionary<Type, Action<IEntityEvent>> _eventHandlers;

        private readonly IVersionNumberProvider _versionNumberProvider;
    }
}