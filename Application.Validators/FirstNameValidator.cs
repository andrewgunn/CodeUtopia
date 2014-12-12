using CodeUtopia.Validators;

namespace Application.Validators
{
    public class FirstNameValidator : Validator
    {
        public FirstNameValidator(string firstName)
        {
            AddValidationErrors(new StringNotNullOrWhiteSpaceValidator(firstName).ValidationErrors);
        }
    }
}