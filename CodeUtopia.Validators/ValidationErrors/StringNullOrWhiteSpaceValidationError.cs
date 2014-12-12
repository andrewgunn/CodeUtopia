namespace CodeUtopia.Validators.ValidationErrors
{
    public class StringNullOrWhiteSpaceValidationError : ValidationError
    {
        public StringNullOrWhiteSpaceValidationError(string value)
            : base("The string is null or white space.")
        {
            _value = value;
        }

        public string Value
        {
            get
            {
                return _value;
            }
        }

        private readonly string _value;
    }
}