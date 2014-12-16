using System;
using CodeUtopia.Events;

namespace Tests.CodeUtopia.Domain
{
    public class TodoListItemCompletedEvent : IEntityEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public Guid EntityId { get; set; }
    }
}