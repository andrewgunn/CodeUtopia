using System;
using CodeUtopia.Events;

namespace Application.Events
{
    public class BorrowerCreatedEvent : DomainEvent
    {
        public BorrowerCreatedEvent(Guid borrowerId,
                                    int aggregateVersionNumber,
                                    string firstName,
                                    string lastName,
                                    string emailAddress)
            : base(borrowerId, aggregateVersionNumber)
        {
            _firstName = firstName;
            _lastName = lastName;
            _emailAddress = emailAddress;
        }

        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
        }

        private readonly string _emailAddress;

        private readonly string _firstName;

        private readonly string _lastName;
    }
}