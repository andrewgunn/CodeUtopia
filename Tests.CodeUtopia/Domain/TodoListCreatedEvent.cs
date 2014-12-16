using System;
using CodeUtopia.Events;

namespace Tests.CodeUtopia.Domain
{
    public class TodoListCreatedEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public string Name { get; set; }
    }
}