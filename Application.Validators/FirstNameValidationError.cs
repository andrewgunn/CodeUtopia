using CodeUtopia.Validators;

namespace Application.Validators
{
    public class FirstNameValidationError : IValidationError
    {
        public FirstNameValidationError(string firstName)
        {
            _firstName = firstName;
            _message = string.Format("The first name is invalid.");
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        private readonly string _firstName;

        private readonly string _message;
    }
}