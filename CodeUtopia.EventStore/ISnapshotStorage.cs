using System;

namespace CodeUtopia.EventStore
{
    public interface ISnapshotStorage
    {
        ISnapshot GetLastSnapshotForAggregate(Guid aggregateId);

        void SaveSnapshotForAggregate(Guid aggregateId, int aggregateVersionNumber, object memento);
    }
}