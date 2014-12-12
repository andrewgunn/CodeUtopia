using System;
using CodeUtopia.Domain;

namespace CodeUtopia.EventStore
{
    public abstract class Snapshot : ISnapshot
    {
        protected Snapshot(Guid aggregateId, int versionNumber, object memento)
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

        public int AggregateVersionNumber
        {
            get
            {
                return _versionNumber;
            }
        }

        public object Memento
        {
            get
            {
                return _memento;
            }
        }

        private readonly Guid _aggregateId;

        private readonly object _memento;

        private readonly int _versionNumber;
    }
}