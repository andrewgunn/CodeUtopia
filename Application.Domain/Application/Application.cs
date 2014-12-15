using System;
using System.Linq;
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

        public Borrower AddBorrower(Guid borrowerId, string firstName, string lastName, string emailAddress)
        {
            var borrower = new Borrower(AggregateId, this, borrowerId, firstName, lastName, emailAddress);

            Apply(new BorrowerAddedToApplicationEvent(AggregateId,
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

        public Borrower GetBorrower(Guid borrowerId)
        {
            return _borrowers.SingleOrDefault(x => x.EntityId == borrowerId);
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

        private void OnBorrowerAddedToApplicationEvent(BorrowerAddedToApplicationEvent borrowerAddedToApplicationEvent)
        {
            var borrower = new Borrower(ApplicationId,
                                        this,
                                        borrowerAddedToApplicationEvent.BorrowerId,
                                        borrowerAddedToApplicationEvent.FirstName,
                                        borrowerAddedToApplicationEvent.LastName,
                                        borrowerAddedToApplicationEvent.EmailAddress);

            _borrowers.Add(borrower);
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<ApplicationCreatedEvent>(OnApplicationCreateEvent);
            RegisterEventHandler<BorrowerAddedToApplicationEvent>(OnBorrowerAddedToApplicationEvent);
        }

        protected Guid ApplicationId
        {
            get
            {
                return AggregateId;
            }
        }

        private readonly EntityList<Borrower> _borrowers;

        private decimal _loanAmount;

        private int _loanTermInMonths;
    }
}