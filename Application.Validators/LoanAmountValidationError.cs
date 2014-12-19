using CodeUtopia.Validators;

namespace Application.Validators
{
    public class LoanAmountValidationError : IValidationError
    {
        public LoanAmountValidationError(decimal loanAmount)
        {
            _loanAmount = loanAmount;
            _message = string.Format("The loan amount is invalid.");
        }

        public decimal LoanAmount
        {
            get
            {
                return _loanAmount;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        private readonly decimal _loanAmount;

        private readonly string _message;
    }
}