using System;

namespace CodeUtopia.EventStore
{
    public interface ISnapshotStorage
    {
        Snapshot GetLastSnapshotForAggregate(Guid aggregateId);

        void SaveSnapshotForAggregate(Guid aggregateId, int aggregateVersionNumber, object memento);
    }
}