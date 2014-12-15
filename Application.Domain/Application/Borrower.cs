using System;
using System.Collections.Generic;
using System.Linq;
using Application.Events;
using Application.Validators;
using CodeUtopia.Domain;
using CodeUtopia.Validators;

namespace Application.Domain.Application
{
    public class Borrower : Applicant
    {
        public Borrower(Guid applicationId, IVersionNumberProvider versionNumberProvider, Guid borrowerId, string firstName, string lastName, string emailAddress)
            : base(applicationId, versionNumberProvider, borrowerId)
        {
            _firstName = firstName;
            _lastName = lastName;
            _emailAddress = emailAddress;

            RegisterEventHandlers();

            var validationErrors = new List<IValidationError>
                                   {
                                       new FirstNameValidator().Validate(firstName),
                                       new LastNameValidator().Validate(lastName),
                                       new EmailAddressValidator().Validate(emailAddress)
                                   };

            if (validationErrors.Any())
            {
                throw new AggregateValidationErrorException(validationErrors);
            }
        }

        private void OnBorrowerCreatedEvent(BorrowerAddedToApplicationEvent borrowerCreatedEvent)
        {
            _firstName = borrowerCreatedEvent.FirstName;
            _lastName = borrowerCreatedEvent.LastName;
            _emailAddress = borrowerCreatedEvent.EmailAddress;
        }

        public void ChangeCurrentAddress()
        {

        }

        private void RegisterEventHandlers()
        {
        }

        private string _emailAddress;

        private string _firstName;

        private string _lastName;
    }
}