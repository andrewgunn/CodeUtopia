using CodeUtopia.Validators.ValidationErrors;

namespace CodeUtopia.Validators
{
    public class IntegerNotLessThanValidator : Validator
    {
        public IntegerNotLessThanValidator(int value, int minimumValue)
        {
            if (value < 0)
            {
                AddValidationError(new IntegerLessThanValidationError(value, minimumValue));
            }
        }
    }
}