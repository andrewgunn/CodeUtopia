using CodeUtopia.Validators.ValidationErrors;

namespace CodeUtopia.Validators
{
    public class IntegerNotEqualToValidator : Validator
    {
        public IntegerNotEqualToValidator(int value, int equalValue)
        {
            if (value == equalValue)
            {
                AddValidationError(new IntegerEqualToValidationError(value, equalValue));
            }
        }
    }
}