using System;
using Application.Domain.Applicant;
using Application.Events;
using CodeUtopia.Domain;

namespace Application.Domain.Application
{
    public class Application : Aggregate, IOriginator
    {
        public Application()
        {
            RegisterEventHandlers();
        }

        private Application(Guid applicationId, decimal loanAmount, int loanTermInMonths)
        {
            Apply(new ApplicationCreatedEvent(applicationId, GetNextVersionNumber(), loanAmount, loanTermInMonths));
        }

        public Borrower AddBorrower(Guid borrowerId, FirstName firstName, LastName lastName, EmailAddress emailAddress)
        {
            var borrower = Borrower.Create(borrowerId, firstName, lastName, emailAddress);

            Apply(new BorrowerAddedEvent(AggregateId,
                                         GetNextVersionNumber(),
                                         borrowerId,
                                         firstName,
                                         lastName,
                                         emailAddress));

            return borrower;
        }

        public static Application Create(Guid applicationId, decimal loanAmount, int loanTermInMonths)
        {
            return new Application(applicationId, loanAmount, loanTermInMonths);
        }

        public object CreateMemento()
        {
            throw new NotImplementedException();
        }

        public void LoadFromMemento(Guid aggregateId, int aggregateVersionNumber, object memento)
        {
            throw new NotImplementedException();
        }

        private void OnApplicationCreateEvent(ApplicationCreatedEvent applicationCreatedEvent)
        {
            AggregateId = applicationCreatedEvent.AggregateId;
            _loanAmount = applicationCreatedEvent.LoanAmount;
            _loanTermInMonths = applicationCreatedEvent.LoanTermInMonths;
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<ApplicationCreatedEvent>(OnApplicationCreateEvent);
        }

        private decimal _loanAmount;

        private int _loanTermInMonths;
    }
}