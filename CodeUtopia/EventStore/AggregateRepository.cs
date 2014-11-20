using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Domain;

namespace CodeUtopia.EventStore
{
    public class AggregateRepository : IAggregateRepository
    {
        public AggregateRepository(IEventStorage eventStorage, IEventPublisher eventPublisher)
        {
            _eventStorage = eventStorage;
            _eventPublisher = eventPublisher;

            _aggregates = new List<IAggregate>();
        }

        public void Commit()
        {
            foreach (var aggregate in _aggregates)
            {
                _eventStorage.SaveChanges(aggregate);

                foreach (var domainEvent in aggregate.GetChanges())
                {
                    _eventPublisher.Publish(domainEvent);
                }

                aggregate.ClearChanges();
            }

            _aggregates.Clear();

            _eventStorage.Commit();
            _eventPublisher.Commit();
        }

        public TAggregate Get<TAggregate>(Guid aggregateId) where TAggregate : class, IAggregate, IOriginator, new()
        {
            var aggregate = LoadFromHistory<TAggregate>(aggregateId);

            RegisterForTracking(aggregate);

            return aggregate;
        }

        private TAggregate LoadFromHistory<TAggregate>(Guid aggregateId)
            where TAggregate : class, IAggregate, IOriginator, new()
        {
            var aggregate = new TAggregate();

            LoadFromLastSnapshotIfExists(aggregateId, aggregate);

            LoadFromHistorySinceLastSnapshot(aggregateId, aggregate);

            return aggregate;
        }

        private void LoadFromHistorySinceLastSnapshot(Guid aggregateId, IAggregate aggregate)
        {
            var domainEvents = _eventStorage.GetAllSinceLastSnapshot(aggregateId);

            if (!domainEvents.Any())
            {
                domainEvents = _eventStorage.GetAll(aggregateId);
            }

            aggregate.LoadFromHistory(domainEvents);
        }

        private void LoadFromLastSnapshotIfExists(Guid aggregateId, IOriginator aggregate)
        {
            var snapshot = _eventStorage.GetLastSnapshot(aggregateId);

            if (snapshot == null)
            {
                return;
            }

            aggregate.LoadFromMemento(snapshot.AggregateId, snapshot.VersionNumber, snapshot.Memento);
        }

        public void RegisterForTracking<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IOriginator, IAggregate, new()
        {
            _aggregates.Add(aggregate);
        }

        public void Rollback()
        {
            _aggregates.Clear();

            _eventStorage.Rollback();
            _eventPublisher.Rollback();
        }

        private readonly List<IAggregate> _aggregates;

        private readonly IEventPublisher _eventPublisher;

        private readonly IEventStorage _eventStorage;
    }
}