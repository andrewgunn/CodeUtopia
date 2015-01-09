using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Transactions;
using CodeUtopia.Messages;

namespace CodeUtopia.WriteStore.EntityFramework
{
    public class EntityFrameworkWriteStore : IEventStorage
    {
        public EntityFrameworkWriteStore(IWriteStoreDatabaseSettings writeStoreDatabaseSettings, IFormatter formatter)
        {
            _writeStoreDatabaseSettings = writeStoreDatabaseSettings;
            _formatter = formatter;

            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (var databaseContext = new WriteStoreContext(writeStoreDatabaseSettings.ConnectionString))
                {
                    databaseContext.Database.Initialize(true);
                }
            }

            _databaseContext = new WriteStoreContext(writeStoreDatabaseSettings.ConnectionString);
        }

        public void Commit()
        {
            _databaseContext.SaveChanges();
        }

        private TType Deserialize<TType>(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return (TType)_formatter.Deserialize(memoryStream);
            }
        }

        public IReadOnlyCollection<IDomainEvent> GetEvents(int skip, int take)
        {
            return _databaseContext.DomainEvents.OrderBy(x => x.AggregateId)
                                   .ThenBy(x => x.AggregateVersionNumber)
                                   .Skip(skip)
                                   .Take(take)
                                   .ToList()
                                   .Select(x => Deserialize<IDomainEvent>(x.DomainEvent))
                                   .ToList();
        }

        public IReadOnlyCollection<IDomainEvent> GetEventsForAggregate(Guid aggregateId)
        {
            return _databaseContext.DomainEvents.Where(x => x.AggregateId == aggregateId)
                                   .OrderBy(x => x.AggregateVersionNumber)
                                   .ToList()
                                   .Select(x => Deserialize<IDomainEvent>(x.DomainEvent))
                                   .ToList();
        }

        public IReadOnlyCollection<IDomainEvent> GetEventsForAggregateSinceLastSnapshot(Guid aggregateId)
        {
            var snapshot = GetLastSnapshotForAggregate(aggregateId);

            var snapshotVersion = snapshot == null ? -1 : snapshot.AggregateVersionNumber;

            return
                _databaseContext.DomainEvents.Where(
                                                    x =>
                                                    x.AggregateId == aggregateId &&
                                                    x.AggregateVersionNumber > snapshotVersion)
                                .OrderBy(x => x.AggregateVersionNumber)
                                .ToList()
                                .Select(x => Deserialize<IDomainEvent>(x.DomainEvent))
                                .ToList();
        }

        public Snapshot GetLastSnapshotForAggregate(Guid aggregateId)
        {
            return _databaseContext.Snapshots.OrderByDescending(x => x.AggregateVersionNumber)
                                   .Where(x => x.AggregateId == aggregateId)
                                   .ToList()
                                   .Select(
                                           x =>
                                           new Snapshot(x.AggregateId,
                                                        x.AggregateVersionNumber,
                                                        Deserialize<object>(x.Memento)))
                                   .FirstOrDefault();
        }

        public void Rollback()
        {
            _databaseContext = new WriteStoreContext(_writeStoreDatabaseSettings.ConnectionString);
        }

        public void SaveEvents(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            if (domainEvents == null || !domainEvents.Any())
            {
                return;
            }

            foreach (var domainEvent in domainEvents.OrderBy(x => x.AggregateVersionNumber)
                                                    .ToList())
            {
                _databaseContext.DomainEvents.Add(new DomainEventEntity
                                                  {
                                                      AggregateId = domainEvent.AggregateId,
                                                      AggregateVersionNumber = domainEvent.AggregateVersionNumber,
                                                      DomainEventType = domainEvent.GetType()
                                                                                   .FullName,
                                                      DomainEvent = Serialize(domainEvent),
                                                  });
            }
        }

        public void SaveSnapshotForAggregate(Guid aggregateId, int aggregateVersionNumber, object memento)
        {
            var snapshot = _databaseContext.Snapshots.SingleOrDefault(x => x.AggregateId == aggregateId);

            if (snapshot != null)
            {
                _databaseContext.Entry(snapshot)
                                .State = EntityState.Deleted;
            }

            _databaseContext.Snapshots.Add(new SnapshotEntity
                                           {
                                               AggregateId = aggregateId,
                                               AggregateVersionNumber = aggregateVersionNumber,
                                               MementoType = memento.GetType()
                                                                    .FullName,
                                               Memento = Serialize(memento)
                                           });
        }

        private byte[] Serialize(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                _formatter.Serialize(memoryStream, obj);

                return memoryStream.ToArray();
            }
        }

        private WriteStoreContext _databaseContext;

        private readonly IWriteStoreDatabaseSettings _writeStoreDatabaseSettings;

        private readonly IFormatter _formatter;
    }
}