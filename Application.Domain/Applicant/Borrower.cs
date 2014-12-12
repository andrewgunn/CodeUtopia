using System;
using System.Collections.Generic;
using Application.Events;
using Application.Validators;
using CodeUtopia.Validators;

namespace Application.Domain.Applicant
{
    public class Borrower : Applicant
    {
        public Borrower()
        {
            _firstName = new FirstName("");
            _lastName = new LastName("");
            _emailAddress = new EmailAddress("");

            RegisterEventHandlers();
        }

        private Borrower(Guid borrowerId, FirstName firstName, LastName lastName, EmailAddress emailAddress)
            : this()
        {
            var validationErrors = new List<IValidationError>
                                   {
                                       new FirstNameValidator().Validate(firstName),
                                       new LastNameValidator().Validate(lastName),
                                       new EmailAddressValidator().Validate(emailAddress)
                                   };

            //if (validationErrors.Any())
            //{
            //    throw new AggregateValidationErrorException(validationErrors);
            //}

            Apply(new BorrowerCreatedEvent(borrowerId, GetNextVersionNumber(), firstName, lastName, emailAddress));
        }

        public static Borrower Create(Guid borrowerId, FirstName firstName, LastName lastName, EmailAddress emailAddress)
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

        private EmailAddress _emailAddress;

        private FirstName _firstName;

        private LastName _lastName;
    }
}