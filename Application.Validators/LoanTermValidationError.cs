using CodeUtopia.Validators;

namespace Application.Validators
{
    public class LoanTermValidationError : IValidationError
    {
        public LoanTermValidationError(int loanTerm)
        {
            _loanTerm = loanTerm;
            _message = string.Format("The loan term is invalid.");
        }

        public int LoanTerm
        {
            get
            {
                return _loanTerm;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        private readonly int _loanTerm;

        private readonly string _message;
    }
}