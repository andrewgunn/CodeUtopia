using System;
using CodeUtopia.Messages;

namespace Tests.CodeUtopia.Domain
{
    public class TodoListCreatedEvent : IEditableDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public string Name { get; set; }
    }
}