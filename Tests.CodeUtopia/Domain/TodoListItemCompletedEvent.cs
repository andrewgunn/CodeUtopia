using System;
using CodeUtopia.Events;

namespace Tests.CodeUtopia.Domain
{
    public class TodoListItemCompletedEvent : IEditableEntityEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public Guid EntityId { get; set; }
    }
}