using CodeUtopia.Validators;

namespace Application.Validators
{
    public class LastNameValidationError : IValidationError
    {
        public LastNameValidationError(string lastName)
        {
            _lastName = lastName;
            _message = string.Format("The last name is invalid.");
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        private readonly string _lastName;

        private readonly string _message;
    }
}