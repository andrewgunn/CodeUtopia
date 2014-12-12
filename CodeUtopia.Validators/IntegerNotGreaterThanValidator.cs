using CodeUtopia.Validators.ValidationErrors;

namespace CodeUtopia.Validators
{
    public class IntegerNotGreaterThanValidator : Validator
    {
        public IntegerNotGreaterThanValidator(int value, int maximumValue)
        {
            if (value > 0)
            {
                AddValidationError(new IntegerGreaterThanValidationError(value, maximumValue));
            }
        }
    }
}