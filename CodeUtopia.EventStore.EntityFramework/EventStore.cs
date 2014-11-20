using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using CodeUtopia.Domain;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class EventStore : IEventStorage
    {
        public EventStore(string nameOrConnectionString, IFormatter formatter)
        {
            _nameOrConnectionString = nameOrConnectionString;
            _formatter = formatter;
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

        private Aggregate GetAggregate(IAggregate aggregate)
        {
            return _databaseContext.Aggregates.Find(aggregate.AggregateId) ??
                   _databaseContext.Aggregates.Add(new Aggregate
                                                   {
                                                       Id = aggregate.AggregateId,
                                                       VersionNumber = aggregate.VersionNumber,
                                                       Type = aggregate.GetType()
                                                                       .FullName
                                                   });
        }

        private int GetAggregateVersion(Guid aggregateId)
        {
            var aggregate = _databaseContext.Aggregates.Find(aggregateId);

            return aggregate == null ? 0 : aggregate.VersionNumber;
        }

        public IReadOnlyCollection<IDomainEvent> GetAll(Guid aggregateId)
        {
            return _databaseContext.DomainEvents.Where(x => x.AggregateId == aggregateId)
                                   .OrderBy(x => x.VersionNumber)
                                   .ToList()
                                   .Select(x => Deserialize<IDomainEvent>(x.Data))
                                   .ToList();
        }

        public IReadOnlyCollection<IDomainEvent> GetAllSinceLastSnapshot(Guid aggregateId)
        {
            var snapshot = GetLastSnapshot(aggregateId);

            var snapshotVersion = snapshot == null ? -1 : snapshot.VersionNumber;

            return
                _databaseContext.DomainEvents.Where(
                                                    x =>
                                                    x.AggregateId == aggregateId && x.VersionNumber > snapshotVersion)
                                .OrderBy(x => x.VersionNumber)
                                .ToList()
                                .Select(x => Deserialize<IDomainEvent>(x.Data))
                                .ToList();
        }

        public ISnapshot GetLastSnapshot(Guid aggregateId)
        {
            var snapshot = _databaseContext.Snapshots.LastOrDefault(x => x.AggregateId == aggregateId);

            return snapshot == null ? null : Deserialize<ISnapshot>(snapshot.Data);
        }

        public void Rollback()
        {
            _databaseContext = new DatabaseContext(_nameOrConnectionString);
        }

        public void SaveChanges(IAggregate aggregate)
        {
            var versionNumber = GetAggregateVersion(aggregate.AggregateId);

            if (versionNumber != aggregate.VersionNumber)
            {
                throw new ConcurrencyViolationException();
            }

            var entity = GetAggregate(aggregate);
            entity.VersionNumber = aggregate.VersionNumber;

            foreach (var domainEvent in aggregate.GetChanges())
            {
                entity.DomainEvents.Add(new DomainEvent
                                        {
                                            Id = Guid.NewGuid(),
                                            Data = Serialize(domainEvent),
                                            VersionNumber = domainEvent.VersionNumber
                                        });
            }
        }

        public void SaveSnapshot<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate, IOriginator
        {
            _databaseContext.Snapshots.Add(new Snapshot
                                           {
                                               Id = Guid.NewGuid(),
                                               AggregateId = aggregate.AggregateId,
                                               VersionNumber = aggregate.VersionNumber,
                                               Data = Serialize(aggregate.CreateMemento())
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

        private DatabaseContext _databaseContext;

        private readonly IFormatter _formatter;

        private readonly string _nameOrConnectionString;
    }
}