namespace CodeUtopia.Validators.ValidationErrors
{
    public class IntegerGreaterThanValidationError : ValidationError
    {
        public IntegerGreaterThanValidationError(int value, int maximumValue)
            : base(string.Format("The integer is greater than {0}.", maximumValue))
        {
            _value = value;
        }

        public int Value
        {
            get
            {
                return _value;
            }
        }

        private readonly int _value;
    }
}