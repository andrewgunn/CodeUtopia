using System;
using CodeUtopia.Events;

namespace Application.Events.v1
{
    public class ApplicationCreatedEvent : IEditableDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public decimal LoanAmount { get; set; }

        public int LoanTermInMonths { get; set; }
    }
}