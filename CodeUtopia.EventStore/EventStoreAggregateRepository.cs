using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Domain;
using CodeUtopia.Events;
using CodeUtopia.Messaging;

namespace CodeUtopia.EventStore
{
    public class EventStoreAggregateRepository : IAggregateRepository
    {
        public EventStoreAggregateRepository(IEventStorage eventStorage, IBus bus)
        {
            _eventStorage = eventStorage;
            _bus = bus;

            _aggregates = new List<IAggregate>();
        }

        public void Add<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregate, new()
        {
            _aggregates.Add(aggregate);
        }

        public void Commit()
        {
            foreach (var aggregate in _aggregates)
            {
                var domainEvents = aggregate.GetChanges();

                if (domainEvents != null  && domainEvents.Any())
                { 
                    _eventStorage.SaveChanges(aggregate.AggregateId, domainEvents);
                }

                foreach (var domainEvent in aggregate.GetChanges())
                {
                    _bus.Publish(domainEvent);
                }

                aggregate.ClearChanges();
            }

            _aggregates.Clear();

            _eventStorage.Commit();

            _bus.Commit();
        }

        public TAggregate Get<TAggregate>(Guid aggregateId) where TAggregate : class, IAggregate, new()
        {
            var aggregate = LoadFromHistory<TAggregate>(aggregateId);

            Add(aggregate);

            return aggregate;
        }

        private TAggregate LoadFromHistory<TAggregate>(Guid aggregateId) where TAggregate : class, IAggregate, new()
        {
            var aggregate = new TAggregate();
            IReadOnlyCollection<IDomainEvent> domainEvents;
            var originator = aggregate as IOriginator;

            if (originator == null)
            {
                domainEvents = _eventStorage.GetAll(aggregateId);
            }
            else
            {
                var snapshot = _eventStorage.GetLastSnapshot(aggregateId);

                if (snapshot != null)
                {
                    originator.LoadFromMemento(snapshot.AggregateId, snapshot.AggregateVersionNumber, snapshot.Memento);
                }

                domainEvents = _eventStorage.GetAllSinceLastSnapshot(aggregateId);

                if (domainEvents.Count > 10 /* TODO Make this value configurable. */)
                {
                    _eventStorage.SaveSnapshot(aggregate.AggregateId, aggregate.AggregateVersionNumber, originator.CreateMemento());
                }
            }

            aggregate.LoadFromHistory(domainEvents);

            return aggregate;
        }

        public void Rollback()
        {
            _aggregates.Clear();

            _eventStorage.Rollback();

            _bus.Rollback();
        }

        private readonly List<IAggregate> _aggregates;

        private readonly IBus _bus;

        private readonly IEventStorage _eventStorage;
    }
}