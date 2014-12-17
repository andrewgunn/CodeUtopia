using System;
using CodeUtopia.Events;

namespace Application.Events
{
    public class BorrowerAddedToApplicationEvent : IEditableDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public Guid BorrowerId { get; set; }

        public string EmailAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}