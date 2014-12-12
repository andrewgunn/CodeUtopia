namespace CodeUtopia.Validators.ValidationErrors
{
    public class IntegerNotEqualToValidationError : ValidationError
    {
        public IntegerNotEqualToValidationError(int value, int equalValue)
            : base(string.Format("The integer is not equal to {0}.", equalValue))
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