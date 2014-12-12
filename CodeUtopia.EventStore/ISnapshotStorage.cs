using System;

namespace CodeUtopia.EventStore
{
    public interface ISnapshotStorage
    {
        ISnapshot GetLastSnapshot(Guid aggregateId);

        void SaveSnapshot(Guid aggregateId, int aggregateVersionNumber, object memento);
    }
}