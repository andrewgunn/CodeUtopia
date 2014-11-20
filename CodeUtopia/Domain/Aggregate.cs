using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeUtopia.Domain
{
    public abstract class Aggregate : IAggregate, IVersionNumberProvider
    {
        protected Aggregate()
        {
            _eventHandlers = new Dictionary<Type, Action<IDomainEvent>>();
            _entities = new List<IEntity>();
            _appliedEvents = new List<IDomainEvent>();
        }

        protected void Apply(IDomainEvent domainEvent)
        {
            Handle(domainEvent);

            //domainEvent.AggregateId = AggregateId;
            //domainEvent.VersionNumber = GetNextVersionNumber();

            _appliedEvents.Add(domainEvent);

            VersionNumber = domainEvent.VersionNumber;
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
                                 .OrderBy(x => x.VersionNumber)
                                 .ToList();
        }

        private IEnumerable<IEntityEvent> GetEntityEvents()
        {
            return _entities.SelectMany(x => x.GetChanges());
        }

        private int GetNextVersionNumber()
        {
            return ((IVersionNumberProvider)this).GetNextVersionNumber();
        }

        int IVersionNumberProvider.GetNextVersionNumber()
        {
            return ++VersionNumber;
        }

        private void Handle(IDomainEvent domainEvent)
        {
            var eventType = domainEvent.GetType();
            Action<IDomainEvent> eventHandler;

            if (!_eventHandlers.TryGetValue(eventType, out eventHandler))
            {
                throw new EventHandlerNotRegisteredException(eventType);
            }

            eventHandler(domainEvent);
        }

        public bool IsInitialized()
        {
            return AggregateId == default(Guid);
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

            VersionNumber = domainEvents.Last()
                                        .VersionNumber;
        }

        void IAggregate.RegisterEntity(IEntity entity)
        {
            _entities.Add(entity);
        }

        protected void RegisterEventHandler<TDomainEvent>(Action<TDomainEvent> eventHandler)
            where TDomainEvent : class, IDomainEvent
        {
            _eventHandlers.Add(typeof(TDomainEvent), @event => eventHandler(@event as TDomainEvent));
        }

        public Guid AggregateId { get; protected set; }

        public int VersionNumber { get; protected set; }

        private readonly List<IDomainEvent> _appliedEvents;

        private readonly List<IEntity> _entities;

        private readonly Dictionary<Type, Action<IDomainEvent>> _eventHandlers;
    }
}