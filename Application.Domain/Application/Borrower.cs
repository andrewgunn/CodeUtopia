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
        public Borrower(Guid applicationId,
                        IVersionNumberProvider versionNumberProvider,
                        Guid borrowerId,
                        string firstName,
                        string lastName,
                        string emailAddress)
            : base(applicationId, versionNumberProvider, borrowerId)
        {
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

            _firstName = firstName;
            _lastName = lastName;
            _emailAddress = emailAddress;

            RegisterEventHandlers();
        }

        public void MoveAddress(Address address)
        {
            Apply(new BorrowerMovedAddressEvent(ApplicationId,
                                                GetNextVersionNumber(),
                                                BorrowerId,
                                                address.HouseName,
                                                address.HouseNumber,
                                                address.Flat,
                                                address.Street,
                                                address.Street2,
                                                address.TownOrCity,
                                                address.District,
                                                address.County,
                                                address.Postcode));
        }

        private void OnMovedAddressEvent(BorrowerMovedAddressEvent borrowerMovedAddressEvent)
        {
            _previousAddresses.Add(_currentAddress);

            _currentAddress = new Address(borrowerMovedAddressEvent.HouseName,
                                          borrowerMovedAddressEvent.HouseNumber,
                                          borrowerMovedAddressEvent.Flat,
                                          borrowerMovedAddressEvent.Street,
                                          borrowerMovedAddressEvent.Street2,
                                          borrowerMovedAddressEvent.TownOrCity,
                                          borrowerMovedAddressEvent.District,
                                          borrowerMovedAddressEvent.County,
                                          borrowerMovedAddressEvent.Postcode);
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<BorrowerMovedAddressEvent>(OnMovedAddressEvent);
        }

        protected Guid ApplicationId
        {
            get
            {
                return AggregateId;
            }
        }

        protected Guid BorrowerId
        {
            get
            {
                return EntityId;
            }
        }

        private Address _currentAddress;

        private string _emailAddress;

        private string _firstName;

        private string _lastName;

        private List<Address> _previousAddresses;
    }
}