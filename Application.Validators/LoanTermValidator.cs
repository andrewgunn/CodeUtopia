using CodeUtopia.Validators;

namespace Application.Validators
{
    public class LoanTermValidator : Validator
    {
        public LoanTermValidator(int loanTerm)
        {
            AddValidationErrors(new IntegerEqualToValidator(loanTerm, 12).ValidationErrors);
            AddValidationErrors(new IntegerEqualToValidator(loanTerm, 18).ValidationErrors);
            AddValidationErrors(new IntegerEqualToValidator(loanTerm, 24).ValidationErrors);
            AddValidationErrors(new IntegerEqualToValidator(loanTerm, 36).ValidationErrors);
        }
    }
}