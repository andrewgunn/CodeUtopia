using CodeUtopia.Validators;

namespace Application.Validators
{
    public class LastNameValidator : Validator
    {
        public LastNameValidator(string lastName)
        {
            AddValidationErrors(new StringNotNullOrWhiteSpaceValidator(lastName).ValidationErrors);
        }
    }
}