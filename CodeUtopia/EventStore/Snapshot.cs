using System;

namespace CodeUtopia.EventStore
{
    public abstract class Snapshot : ISnapshot
    {
        protected Snapshot(Guid aggregateId, int versionNumber, IMemento memento)
        {
            _aggregateId = aggregateId;
            _versionNumber = versionNumber;
            _memento = memento;
        }

        public Guid AggregateId
        {
            get
            {
                return _aggregateId;
            }
        }

        public IMemento Memento
        {
            get
            {
                return _memento;
            }
        }

        public int VersionNumber
        {
            get
            {
                return _versionNumber;
            }
        }

        private readonly Guid _aggregateId;

        private readonly IMemento _memento;

        private readonly int _versionNumber;
    }
}