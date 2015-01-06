using System;
using System.Collections.Generic;
using CodeUtopia.ReadStore;
using Library.Frontend.ReadStore.Aggregate;

namespace Library.Frontend.ReadStore
{
    public class ReadStoreRepository : IReadStoreRepository
    {
        public ReadStoreRepository(IReadStoreDatabaseSettings readStoreDatabaseSettings)
        {
            _readStoreDatabaseSettings = readStoreDatabaseSettings;
            _aggregatesToCreate = new List<CodeUtopia.ReadStore.Aggregate>();
            _aggregatesToUpdate = new List<CodeUtopia.ReadStore.Aggregate>();
        }

        public void Commit()
        {
            using (var databaseContext = new ReadStoreContext(_readStoreDatabaseSettings))
            {
                foreach (var aggregate in _aggregatesToCreate)
                {
                    CreateAggregate(databaseContext, aggregate.AggregateId, aggregate.AggregateVersionNumber);
                }

                foreach (var aggregate in _aggregatesToUpdate)
                {
                    UpdateAggregate(databaseContext, aggregate.AggregateId, aggregate.AggregateVersionNumber);
                }

                databaseContext.SaveChanges();
            }

            _aggregatesToCreate.Clear();
            _aggregatesToUpdate.Clear();
        }

        private static void CreateAggregate(ReadStoreContext databaseContext,
                                            Guid aggregateId,
                                            int aggregateVersionNumber)
        {
            databaseContext.Aggregates.Add(new AggregateEntity
                                           {
                                               AggregateId = aggregateId,
                                               AggregateVersionNumber = aggregateVersionNumber
                                           });
        }

        public CodeUtopia.ReadStore.Aggregate GetAggregate(Guid aggregateId)
        {
            using (var databaseContext = new ReadStoreContext(_readStoreDatabaseSettings))
            {
                var aggregate = databaseContext.Aggregates.Find(aggregateId);

                return aggregate == null
                           ? null
                           : MapAndTrackAggregate(aggregate.AggregateId, aggregate.AggregateVersionNumber);
            }
        }

        private CodeUtopia.ReadStore.Aggregate MapAndTrackAggregate(Guid aggregateId, int aggregateVersionNumber)
        {
            var aggregate = new CodeUtopia.ReadStore.Aggregate
                            {
                                AggregateId = aggregateId,
                                AggregateVersionNumber = aggregateVersionNumber
                            };

            _aggregatesToUpdate.Add(aggregate);

            return aggregate;
        }

        public void Rollback()
        {
            _aggregatesToCreate.Clear();
            _aggregatesToUpdate.Clear();
        }

        public void SaveAggregate(CodeUtopia.ReadStore.Aggregate aggregate)
        {
            _aggregatesToCreate.Add(aggregate);
        }

        private static void UpdateAggregate(ReadStoreContext databaseContext,
                                            Guid aggregateId,
                                            int aggregateVersionNumber)
        {
            var aggregate = new AggregateEntity
                            {
                                AggregateId = aggregateId
                            };

            databaseContext.Aggregates.Attach(aggregate);

            aggregate.AggregateVersionNumber = aggregateVersionNumber;
        }

        private readonly List<CodeUtopia.ReadStore.Aggregate> _aggregatesToCreate;

        private readonly List<CodeUtopia.ReadStore.Aggregate> _aggregatesToUpdate;

        private readonly IReadStoreDatabaseSettings _readStoreDatabaseSettings;
    }
}