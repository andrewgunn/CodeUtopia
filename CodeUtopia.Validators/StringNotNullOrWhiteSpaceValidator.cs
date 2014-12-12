using CodeUtopia.Validators.ValidationErrors;

namespace CodeUtopia.Validators
{
    public class StringNotNullOrWhiteSpaceValidator : Validator
    {
        public StringNotNullOrWhiteSpaceValidator(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                AddValidationError(new StringNullOrWhiteSpaceValidationError(value));
            }
        }
    }
}