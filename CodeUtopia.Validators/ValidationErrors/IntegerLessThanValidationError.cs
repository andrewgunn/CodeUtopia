namespace CodeUtopia.Validators.ValidationErrors
{
    public class IntegerLessThanValidationError : ValidationError
    {
        public IntegerLessThanValidationError(int value, int minimumValue)
            : base(string.Format("The integer is less than {0}.", minimumValue))
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