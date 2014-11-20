using System;
using CodeUtopia.Domain;

namespace CodeUtopia.EventStore
{
    public interface ISnapshotStorage
    {
        ISnapshot GetLastSnapshot(Guid aggregateId);

        void SaveSnapshot<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate, IOriginator;
    }
}