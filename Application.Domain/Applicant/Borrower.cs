using System;
using Application.Events;

namespace Application.Domain.Applicant
{
    public class FirstName
    {
    }

    public class Borrower : Applicant
    {
        public Borrower()
        {
            RegisterEventHandlers();
        }

        private Borrower(Guid borrowerId, string firstName, string lastName, string emailAddress)
        {
            Apply(new BorrowerCreatedEvent(borrowerId, GetNextVersionNumber(), firstName, lastName, emailAddress));
        }

        public static Borrower Create(Guid borrowerId, string firstName, string lastName, string emailAddress)
        {
            return new Borrower(borrowerId, firstName, lastName, emailAddress);
        }

        private void OnBorrowerCreatedEvent(BorrowerCreatedEvent borrowerCreatedEvent)
        {
            _firstName = borrowerCreatedEvent.FirstName;
            _lastName = borrowerCreatedEvent.LastName;
            _emailAddress = borrowerCreatedEvent.EmailAddress;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<BorrowerCreatedEvent>(OnBorrowerCreatedEvent);
        }

        private string _emailAddress;

        private string _firstName;

        private string _lastName;
    }
}