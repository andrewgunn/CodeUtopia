using System;
using System.Collections.Generic;

namespace CodeUtopia.EventStore.EntityFramework
{
    internal class Aggregate
    {
        public virtual ICollection<DomainEvent> DomainEvents { get; set; }

        public Guid Id { get; set; }

        public virtual ICollection<Snapshot> Snapshots { get; set; }

        public string Type { get; set; }

        public int VersionNumber { get; set; }
    }
}