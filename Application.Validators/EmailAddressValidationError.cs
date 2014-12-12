using CodeUtopia.Validators;

namespace Application.Validators
{
    public class EmailAddressValidationError : IValidationError
    {
        public EmailAddressValidationError(string emailAddress)
        {
            _emailAddress = emailAddress;
            _message = string.Format("The email address is invalid.");
        }

        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        private readonly string _emailAddress;

        private readonly string _message;
    }
}