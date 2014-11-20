using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeUtopia.Domain
{
    public interface IVersionNumberProvider
    {
        int GetNextVersionNumber();
    }

    public abstract class Entity : IEntity
    {
        protected Entity(Guid aggregateId, IVersionNumberProvider versionNumberProvider)
        {
            _aggregateId = aggregateId;
            VersionNumberProvider = versionNumberProvider;
            _eventHandlers = new Dictionary<Type, Action<IEntityEvent>>();
            _appliedEvents = new List<IEntityEvent>();
        }

        protected void Apply(IEntityEvent entityEvent)
        {
            Handle(entityEvent);

            //entityEvent.AggregateId = AggregateId;
            //entityEvent.VersionNumber = _versionNumberProvider.GetNextVersionNumber();
            //entityEvent.EntityId = EntityId;

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
            return _appliedEvents.OrderBy(x => x.VersionNumber);
        }

        private void Handle(IEntityEvent entityEvent)
        {
            var entityEventType = entityEvent.GetType();
            Action<IEntityEvent> entityEventHandler;

            if (!_eventHandlers.TryGetValue(entityEventType, out entityEventHandler))
            {
                throw new EventHandlerNotRegisteredException(entityEventType);
            }

            entityEventHandler(entityEvent);
        }

        public bool IsInitialized()
        {
            return EntityId == default(Guid);
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

        public Guid EntityId { get; protected set; }

        protected IVersionNumberProvider VersionNumberProvider { get; private set; }

        private readonly Guid _aggregateId;

        private readonly List<IEntityEvent> _appliedEvents;

        private readonly Dictionary<Type, Action<IEntityEvent>> _eventHandlers;
    }
}