namespace CodeUtopia.Validators
{
    public class IntegerInRangeValidator : Validator
    {
        public IntegerInRangeValidator(int value, int minimumValue, int maximumValue)
        {
            AddValidationErrors(new IntegerNotLessThanValidator(value, minimumValue).ValidationErrors);
            AddValidationErrors(new IntegerNotGreaterThanValidator(value, maximumValue).ValidationErrors);
        }
    }
}