using System;
using CodeUtopia.Events;

namespace Application.Events
{
    public class ApplicationCreatedEvent : DomainEvent
    {
        public ApplicationCreatedEvent(Guid applicationId,
                                       int applicationVersionNumber,
                                       decimal loanAmount,
                                       int loanTermInMonths)
            : base(applicationId, applicationVersionNumber)
        {
            _loanAmount = loanAmount;
            _loanTermInMonths = loanTermInMonths;
        }

        public Guid ApplicationId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        public decimal LoanAmount
        {
            get
            {
                return _loanAmount;
            }
        }

        public int LoanTermInMonths
        {
            get
            {
                return _loanTermInMonths;
            }
        }

        private readonly decimal _loanAmount;

        private readonly int _loanTermInMonths;
    }
}