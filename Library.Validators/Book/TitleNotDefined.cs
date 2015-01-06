using CodeUtopia.Validators;

namespace Library.Validators.Book
{
    public class TitleNotDefined : IValidationError
    {
        public string Message
        {
            get
            {
                return "Title is not defined.";
            }
        }
    }
}