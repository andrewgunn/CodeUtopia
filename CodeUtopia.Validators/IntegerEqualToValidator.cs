using CodeUtopia.Validators.ValidationErrors;

namespace CodeUtopia.Validators
{
    public class IntegerEqualToValidator : Validator
    {
        public IntegerEqualToValidator(int value, int equalValue)
        {
            if (value != equalValue)
            {
                AddValidationError(new IntegerNotEqualToValidationError(value, equalValue));
            }
        }
    }
}