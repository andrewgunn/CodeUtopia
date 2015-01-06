using System;

namespace CodeUtopia.ReadStore
{
    public interface IReadStoreRepository : IUnitOfWork
    {
        Aggregate GetAggregate(Guid aggregateId);

        void SaveAggregate(Aggregate aggregate);
    }
}