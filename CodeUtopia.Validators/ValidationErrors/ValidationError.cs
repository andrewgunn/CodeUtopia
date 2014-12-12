namespace CodeUtopia.Validators.ValidationErrors
{
    public class ValidationError : IValidationError
    {
        public ValidationError()
            : this(null)
        {
        }

        public ValidationError(string message)
        {
            _message = message;
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        private readonly string _message;
    }
}