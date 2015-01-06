using System;

namespace CodeUtopia.WriteStore
{
    public class Snapshot
    {
        public Snapshot(Guid aggregateId, int aggregateVersionNumber, object memento)
        {
            _aggregateId = aggregateId;
            _aggregateVersionNumber = aggregateVersionNumber;
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
                return _aggregateVersionNumber;
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

        private readonly int _aggregateVersionNumber;

        private readonly object _memento;
    }
}