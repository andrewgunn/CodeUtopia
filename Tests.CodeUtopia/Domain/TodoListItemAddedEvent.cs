using System;
using CodeUtopia.Messages;

namespace Tests.CodeUtopia.Domain
{
    internal class TodoListItemAddedEvent : IEditableDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public string Description { get; set; }

        public Guid TodoListItemId { get; set; }
    }
}