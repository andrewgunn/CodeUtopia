using CodeUtopia.Validators;

namespace Library.Validators.Book
{
    public class TitleNotDefined : IValidationError
    {
        public string Message
        {
            get
            {
                return "The title is not defined.";
            }
        }
    }
}