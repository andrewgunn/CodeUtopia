using System;
using CodeUtopia.Events;

namespace Application.Events
{
    public class BorrowerAddedToApplicationEvent : DomainEvent
    {
        public BorrowerAddedToApplicationEvent(Guid applicationId,
                                               int aggregateVersionNumber,
                                               Guid borrowerId,
                                               string firstName,
                                               string lastName,
                                               string emailAddress)
            : base(applicationId, aggregateVersionNumber)
        {
            _borrowerId = borrowerId;
            _firstName = firstName;
            _lastName = lastName;
            _emailAddress = emailAddress;
        }

        public Guid ApplicationId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        public Guid BorrowerId
        {
            get
            {
                return _borrowerId;
            }
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

        private readonly Guid _borrowerId;

        private readonly string _emailAddress;

        private readonly string _firstName;

        private readonly string _lastName;
    }
}