using CodeUtopia.Validators;

namespace Application.Validators
{
    public class EmailAddressValidator : Validator
    {
        public EmailAddressValidator(string emailAddress)
        {
            AddValidationErrors(new StringNotNullOrWhiteSpaceValidator(emailAddress).ValidationErrors);
        }
    }
}