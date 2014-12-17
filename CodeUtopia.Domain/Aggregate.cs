using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Events;

namespace CodeUtopia.Domain
{
    public abstract class Aggregate : IAggregate, IEntityTracker, IVersionNumberProvider
    {
        protected Aggregate()
        {
            _appliedEvents = new List<IDomainEvent>();
            _entities = new List<IEntity>();
            _eventHandlers = new Dictionary<Type, Action<IDomainEvent>>();
        }

        protected void Apply(IEditableDomainEvent domainEvent)
        {
            domainEvent.AggregateId = AggregateId;
            domainEvent.AggregateVersionNumber = GetNextVersionNumber();

            Handle(domainEvent);

            _appliedEvents.Add(domainEvent);
        }

        void IAggregate.ClearChanges()
        {
            _appliedEvents.Clear();
        }

        protected void EnsureIsInitialized()
        {
            if (!IsInitialized())
            {
                throw new AggregateNotInitializedException();
            }
        }

        IReadOnlyCollection<IDomainEvent> IAggregate.GetChanges()
        {
            return _appliedEvents.Concat(GetEntityEvents())
                                 .OrderBy(x => x.AggregateVersionNumber)
                                 .ToList();
        }

        private IEnumerable<IEntityEvent> GetEntityEvents()
        {
            return _entities.SelectMany(x => x.GetChanges());
        }

        private int GetNextVersionNumber()
        {
            return ++EventVersionNumber;
        }

        int IVersionNumberProvider.GetNextVersionNumber()
        {
            return GetNextVersionNumber();
        }

        private void Handle(IDomainEvent domainEvent)
        {
            var eventType = domainEvent.GetType();
            Action<IDomainEvent> eventHandler;

            if (!_eventHandlers.TryGetValue(eventType, out eventHandler))
            {
                throw new DomainEventHandlerNotRegisteredException(eventType);
            }

            eventHandler(domainEvent);

            AggregateVersionNumber = domainEvent.AggregateVersionNumber;
        }

        public bool IsInitialized()
        {
            return AggregateId != default(Guid);
        }

        void IAggregate.LoadFromHistory(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            if (!domainEvents.Any())
            {
                return;
            }

            foreach (var domainEvent in domainEvents)
            {
                Handle(domainEvent);
            }

            EventVersionNumber = AggregateVersionNumber;
        }

        protected void RegisterEventHandler<TDomainEvent>(Action<TDomainEvent> eventHandler)
            where TDomainEvent : class, IDomainEvent
        {
            _eventHandlers.Add(typeof(TDomainEvent), @event => eventHandler(@event as TDomainEvent));
        }

        void IEntityTracker.RegisterForTracking(IEntity entity)
        {
            _entities.Add(entity);
        }

        public Guid AggregateId { get; protected set; }

        public int AggregateVersionNumber { get; protected set; }

        public int EventVersionNumber { get; private set; }

        private readonly List<IDomainEvent> _appliedEvents;

        private readonly List<IEntity> _entities;

        private readonly Dictionary<Type, Action<IDomainEvent>> _eventHandlers;
    }
}