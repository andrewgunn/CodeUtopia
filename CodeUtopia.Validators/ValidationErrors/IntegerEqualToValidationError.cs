namespace CodeUtopia.Validators.ValidationErrors
{
    public class IntegerEqualToValidationError : ValidationError
    {
        public IntegerEqualToValidationError(int value, int equalValue)
            : base(string.Format("The integer is equal to {0}.", equalValue))
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